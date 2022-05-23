using bb;
using rso.core;
using rso.game;
using rso.net;
using System;
using System.Collections.Generic;
using TPeerCnt = System.UInt32;
using TUID = System.Int64;

public class CNetworkControl
{
    public delegate void TRecvCallback(CKey Key_, SProto Proto_);
    struct _SRecvCallbackProto
    {
        TRecvCallback _RecvCallback;
        CKey _Key;
        SProto _Proto;
        public _SRecvCallbackProto(TRecvCallback RecvCallback_, CKey Key_, SProto Proto_)
        {
            _RecvCallback = RecvCallback_;
            _Key = Key_;
            _Proto = Proto_;
        }
        public void Call()
        {
            _RecvCallback(_Key, _Proto);
        }
    }
    Queue<_SRecvCallbackProto> _RecvCallbackProtos = new Queue<_SRecvCallbackProto>();
    rso.game.CClient _Net = null;
    CClientBinder _Binder = null;

    public CNetworkControl(rso.game.CClient Net_)
    {
        _Net = Net_;
        _Binder = new CClientBinder(_Net);
    }
    public void Dispose()
    {
        if (_Net != null)
        {
            _Net.Dispose();
            _Net = null;
        }

        if (_Binder != null)
            _Binder = null;
    }
    public void Update()
    {
        _Net.Proc();
    }
    public bool Call()
    {
        if (_RecvCallbackProtos.Count > 0)
            _RecvCallbackProtos.Dequeue().Call();

        return _RecvCallbackProtos.Count > 0;
    }
    public void Create(CNamePort NamePort_,string ID_, string Nick_, TUID SubUID_, CStream Stream_, string DataPath_)
    {
        _Net.Create(0, DataPath_, NamePort_, ID_, Nick_, SubUID_, 0, Stream_);
    }
    public bool Login(CNamePort NamePort_, string ID_, TUID SubUID_, CStream Stream_, string DataPath_)
    {
        return _Net.Login(0, DataPath_, NamePort_, ID_, SubUID_, Stream_);
    }
    public void Logout()
    {
        _Net.Logout();
    }
    public void Recv(CKey Key_, Int32 ProtoNum_, CStream Stream_)
    {
        _Binder.Recv(Key_, ProtoNum_, Stream_);
    }
    public void AddSendProto<TProto>(Int32 ProtoNum_)
    {
        _Binder.AddSendProto<TProto>(ProtoNum_);
    }
    public void AddRecvProto<TProto>(EProtoNetSc Proto_, TRecvCallback RecvCallback_) where TProto : SProto, new()
    {
        _Binder.AddRecvProto(
            (Int32)Proto_,
            (CKey Key_, CStream Stream_) =>
            {
                var Proto = new TProto();
                Proto.Push(Stream_);

                _RecvCallbackProtos.Enqueue(new _SRecvCallbackProto(RecvCallback_, Key_, Proto));
            });
    }
    public void Send<_TCsProto>(_TCsProto Proto_) where _TCsProto : SProto
    {
        _Binder.Send(Proto_);
    }
    public TimeSpan Latency(TPeerCnt PeerNum_)
    {
        return _Net.Latency(PeerNum_);
    }
    public bool IsLinked(TPeerCnt PeerNum_)
    {
        return _Net.IsLinked(PeerNum_);
    }
}