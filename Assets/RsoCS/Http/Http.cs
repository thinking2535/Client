using System;
using System.Collections.Generic;

namespace rso.http
{
    using TDownloadChangedFunc = CSession.TDownloadChangedFunc;
    using TDownloadCompletedFunc = CSession.TDownloadCompletedFunc;
    public class CHttp
    {
        List<CSession> _Sessions = new List<CSession>();
        Int32 _CurThreadNum = 0;
        TDownloadChangedFunc _DownloadChangedFunc;
        TDownloadCompletedFunc _DownloadCompletedFunc;

        public CHttp(Int32 SessionCnt_, TDownloadChangedFunc DownloadChangedFunc_, TDownloadCompletedFunc DownloadCompletedFunc_)
        {
            _DownloadChangedFunc = DownloadChangedFunc_;
            _DownloadCompletedFunc = DownloadCompletedFunc_;

            if (SessionCnt_ <= 0)
                throw new Exception("Invalid Session Count");

            _Sessions.Capacity = SessionCnt_;

            for (Int32 i = 0; i < SessionCnt_; ++i)
                _Sessions.Add(new CSession(i, _DownloadChangedFunc, _DownloadCompletedFunc));
        }
        public void Push(SInObj Obj_)
        {
            ++_CurThreadNum;
            _CurThreadNum %= _Sessions.Count;
            _Sessions[_CurThreadNum].Push(Obj_);
        }
        public void Proc()
        {
            for (Int32 i = 0; i < _Sessions.Count; ++i)
                _Sessions[i].Proc();
        }
        public void Dispose()
        {
            for (Int32 i = 0; i < _Sessions.Count; ++i)
                _Sessions[i].Dispose();
        }
    }
}