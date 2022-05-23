using System;
using rso.core;
using rso.Base;
using rso.net;

#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
using UnityEngine;
#endif

namespace rso
{
    namespace game
    {
        using TPeerCnt = UInt32;

        public class CClientBinder
        {
            CClient _Net;
            BinderSend<Int32> _BinderSend = new BinderSend<Int32>();
            CList<net.TRecvFunc> _BinderRecv = new CList<net.TRecvFunc>();
            public CClientBinder(CClient Net_)
            {
                _Net = Net_;
            }
            public void AddSendProto<TProto>(Int32 ProtoNum_)
            {
                _BinderSend.Bind<TProto>(ProtoNum_);
            }
            public bool AddRecvProto(Int32 ProtoNum_, TRecvFunc RecvFunc_)
            {
                return _BinderRecv.AddAt(ProtoNum_, RecvFunc_);
            }
            public void Send<_TCsProto>(TPeerCnt PeerNum_, _TCsProto Proto_) where _TCsProto : SProto
            {
                _Net.Send(PeerNum_, _BinderSend.Get<_TCsProto>(), Proto_);
            }
            public void Send<_TCsProto>(_TCsProto Proto_) where _TCsProto : SProto
            {
                _Net.Send(0, _BinderSend.Get<_TCsProto>(), Proto_);
            }
            public void Recv(CKey Key_, Int32 ProtoNum_, CStream Stream_)
            {
                var itRecv = _BinderRecv.Get(ProtoNum_);
                if (!itRecv)
                    return;

                itRecv.Data(Key_, Stream_);
            }
        }
    }
}
