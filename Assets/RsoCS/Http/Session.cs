using rso.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace rso.http
{
    public class CSession
    {
        public delegate void TDownloadChangedFunc(Int32 SessionIndex_, string ObjectName_, Int64 Received_, Int64 Total_);
        public delegate void TDownloadCompletedFunc(Int32 SessionIndex_, EHttpRet Ret_, string ObjectName_, Byte[] Buffer_);

        class _SObject
        {
            public Int32 Index = 0;
            public string Name = "";

            public _SObject(Int32 Index_, string Name_)
            {
                Index = Index_;
                Name = Name_;
            }
        }
        public class _SInObj : SInObj
        {
            public Int32 Index = 0;

            public _SInObj() :
                base("", "")
            {
            }
        }
        class _SOutChangedObj
        {
            public Int32 Index = 0;
            public Int64 Received = 0;
            public Int64 Total = 0;
        }
        class _SOutCompletedObj
        {
            public Int32 Index = 0;
            public EHttpRet Ret = EHttpRet.Null;
            public Byte[] Buffer = null;
        }

        Int32 _ObjectCounter = 0;
        WebClient _WebClient = new WebClient();
        List<_SObject> _Objects = new List<_SObject>();
        CLFQueueB<_SInObj> _InObjects = new CLFQueueB<_SInObj>();
        CLFQueueB<_SOutChangedObj> _OutChangedObjects = new CLFQueueB<_SOutChangedObj>();
        CLFQueueB<_SOutCompletedObj> _OutCompletedObjects = new CLFQueueB<_SOutCompletedObj>();
        _SInObj _InObj = null;
        bool _DownLoadDone = false;
        Int32 _SessionIndex;
        TDownloadChangedFunc _DownloadChangedFunc;
        TDownloadCompletedFunc _DownloadCompletedFunc;
        CThread _WorkerThread;
        void _DownloadProgressChangedCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            var Obj = _OutChangedObjects.GetPushBuf();
            if (Obj != null) // GetPushBuf() 실패하더라도 그냥 무시
            {
                Obj.Index = _InObj.Index;
                Obj.Received = e.BytesReceived;
                Obj.Total = e.TotalBytesToReceive;
                _OutChangedObjects.Push();
            }
        }
        void _DownloadDataCompletedCallback(object sender, DownloadDataCompletedEventArgs e)
        {
            var Obj = _OutCompletedObjects.GetPushBuf();
            if (Obj != null)
            {
                Obj.Index = _InObj.Index;
                Obj.Ret = EHttpRet.Ok;
                Obj.Buffer = e.Result;
                _OutCompletedObjects.Push();
            }

            _DownLoadDone = true;
        }
        void _Worker()
        {
            while (_WorkerThread.Exit == false)
            {
                for (_InObj = _InObjects.GetPopBuf();
                    _InObj != null;
                    _InObj = _InObjects.GetPopBuf())
                {
                    try
                    {
                        // Connect 
                        _DownLoadDone = false;
                        _WebClient.DownloadDataAsync(new System.Uri(_InObj.ServerName + _InObj.ObjectName));

                        while (!_DownLoadDone)
                            Thread.Sleep(1);
                    }
                    catch
                    {
                        var Obj = _OutCompletedObjects.GetPushBuf();
                        if (Obj != null)
                        {
                            Obj.Index = _InObj.Index;
                            Obj.Ret = EHttpRet.ObjectNotFound;
                            _OutCompletedObjects.Push();
                        }
                    }

                    _InObjects.Pop();
                }

                Thread.Sleep(1);
            }
        }

        public CSession(Int32 SessionIndex_, TDownloadChangedFunc DownloadChangedFunc_, TDownloadCompletedFunc DownloadCompletedFunc_)
        {
            _SessionIndex = SessionIndex_;
            _DownloadChangedFunc = DownloadChangedFunc_;
            _DownloadCompletedFunc = DownloadCompletedFunc_;
            _WorkerThread = new CThread(_Worker);
            _WebClient.DownloadProgressChanged += _DownloadProgressChangedCallback;
            _WebClient.DownloadDataCompleted += _DownloadDataCompletedCallback;
        }
        public void Push(SInObj Obj_)
        {
            _Objects.Add(new _SObject(_ObjectCounter, Obj_.ObjectName));

            try
            {
                var pBuf = _InObjects.GetPushBuf();
                pBuf.ServerName = Obj_.ServerName;
                pBuf.ObjectName = Obj_.ObjectName;
                pBuf.Index = _ObjectCounter;
                _InObjects.Push();
            }
            catch
            {
                _Objects.RemoveAt(_Objects.Count - 1);
            }

            ++_ObjectCounter;
        }
        public void Proc()
        {
            while (_Objects.Count > 0)
            {
                var pCompletedObj = _OutCompletedObjects.GetPopBuf();

                for (var pObj = _OutChangedObjects.GetPopBuf();
                    pObj != null;
                    pObj = _OutChangedObjects.GetPopBuf())
                {
                    if (pObj.Index != _Objects.First().Index)
                        break;

                    _DownloadChangedFunc(_SessionIndex, _Objects.First().Name, pObj.Received, pObj.Total);
                    _OutChangedObjects.Pop();
                }

                // pObj 를 _OutChangedObjects.get() 이후에 얻어올 경우 _OutChangedObjects 에 해당 Index의 잔여 Obj 가 남을 수 있기 때문에 이전에 get
                if (pCompletedObj == null) // pObj->Index 가 _Objects.front().Index 와 같음을 보장하므로 Index 비교는 하지 않음.
                    break;

                if (pCompletedObj.Index == _Objects.First().Index)
                {
                    _DownloadCompletedFunc(_SessionIndex, pCompletedObj.Ret, _Objects.First().Name, pCompletedObj.Buffer);
                    pCompletedObj.Buffer = null;
                    _OutCompletedObjects.Pop();
                }
                else
                {
                    _DownloadCompletedFunc(_SessionIndex, EHttpRet.NotEnoughMemory, _Objects.First().Name, null);
                }

                _Objects.RemoveAt(0);
            }
        }
        public void Dispose()
        {
            _WorkerThread.Dispose();
        }
    }
}