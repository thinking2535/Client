using rso.Base;
using rso.core;
using rso.net;
using System;

namespace rso
{
    namespace mobile
    {
        // RsoCPP 와 통일 위함
        using TPeerCnt = UInt32;
        using TProtoSeq = UInt64;
        using TSendProtos = CList<CStream>;
        public class CNet
        {
            public class SPeerExt
            {
                public CKey Key;
                public CKey NetKey;
                public DateTime CloseTime = DateTime.MinValue;
                public TProtoSeq ProtoSeqMustRecv = 0;
                public TProtoSeq ProtoSeqFirstForSendProtos = 0; // SendProtos 의 첫번째 프로토콜 번호
                public TSendProtos SendProtos = new TSendProtos();

                public SPeerExt(CKey Key_)
                {
                    Key = Key_;
                }
                public void Send(CStream Stream_)
                {
                    CStream Stream = new CStream();
                    Stream.Push(Stream_);
                    SendProtos.Add(Stream);
                }
                public void Send(Int64 Data_)
                {
                    CStream Stream = new CStream();
                    Stream.Push(Data_);
                    SendProtos.Add(Stream);
                }
                public void Send(SProto Proto_)
                {
                    CStream Stream = new CStream();
                    Stream.Push(Proto_);
                    SendProtos.Add(Stream);
                }
                public void Send(SProto Proto_, SProto Proto2_)
                {
                    CStream Stream = new CStream();
                    Stream.Push(Proto_);
                    Stream.Push(Proto2_);
                    SendProtos.Add(Stream);
                }
                public void Send(SProto Proto_, Int32 ProtoNum_, SProto Proto2_)
                {
                    CStream Stream = new CStream();
                    Stream.Push(Proto_);
                    Stream.Push(ProtoNum_);
                    Stream.Push(Proto2_);
                    SendProtos.Add(Stream);
                }
                public void RecvAck()
                {
                    var it = SendProtos.Begin();
                    it.Data.Clear();
                    SendProtos.Remove(it);
                    ++ProtoSeqFirstForSendProtos;
                }
                public bool DoesWillClose()
				{
                    return (CloseTime != DateTime.MinValue);
				}
				public void WillClose(TimeSpan WaitDuration_)
				{
					CloseTime = DateTime.Now + WaitDuration_;
				}
            }

            protected TPeerCnt _PeerCounter = 0;
            protected CPeriod _PeriodUnLinked = new CPeriod(TimeSpan.FromMilliseconds(1000));
            protected TimeSpan _KeepConnectDuration;
            protected TLinkFunc _LinkFunc;
            protected TUnLinkFunc _UnLinkFunc;
            protected TRecvFunc _RecvFunc;

			public CNet(TimeSpan KeepConnectDuration_, TLinkFunc LinkFunc_, TUnLinkFunc UnLinkFunc_, TRecvFunc RecvFunc_)
            {
                _KeepConnectDuration = KeepConnectDuration_;
                _LinkFunc = LinkFunc_;
                _UnLinkFunc = UnLinkFunc_;
                _RecvFunc = RecvFunc_;
            }
        }
    }
}