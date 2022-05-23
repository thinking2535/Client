using rso.Base;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace rso
{
    namespace net
    {
        public class CConnectTimeOut
        {
            class _CConnect
            {
                public Socket Socket;
                public Int32 ConnectTick;

                public _CConnect(Socket Socket_, Int32 ConnectTick_)
                {
                    Socket = Socket_;
                    ConnectTick = ConnectTick_;
                }
            }

            Int32 _TimeOutTicks = 0;
            CList<_CConnect> _Connects = new CList<_CConnect>();

            public CConnectTimeOut(Int32 TimeOutSec_)
            {
                _TimeOutTicks = TimeOutSec_ * 1000;
            }
            public bool Add(Int32 PeerNum_, Socket Socket_)
            {
                return _Connects.AddAt(PeerNum_, new _CConnect(Socket_, Environment.TickCount));
            }
            public void Remove(Int32 PeerNum_)
            {
                _Connects.Remove(PeerNum_);
            }
            public void Proc()
            {
                var NowTick = Environment.TickCount;

                for (var it = _Connects.Begin(); it;)
                {
                    var itCheck = it;
                    it.MoveNext();

                    if ((NowTick - itCheck.Data.ConnectTick) < _TimeOutTicks)
                        break;

                    itCheck.Data.Socket.Close();

                    _Connects.Remove(itCheck);
                }
            }
        }
    }
}