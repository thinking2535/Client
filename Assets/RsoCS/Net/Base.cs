using System;
using System.Linq;
using System.Net;
using rso.Base;

namespace rso
{
    namespace net
    {
        using TLongIP = UInt32;
        using TPort = UInt16;
        using TPeerCnt = UInt32;
        public class CKey : SKey
        {
            public CKey() :
                base(CBase.c_PeerCnt_Null, 0)
            {
            }
            public CKey(TPeerCnt PeerNum_, TPeerCnt PeerCounter_)
            {
                PeerNum = PeerNum_;
                PeerCounter = PeerCounter_;
            }
            public CKey(SKey Key_) :
                base(Key_)
            {
            }
            public CKey(Int64 Value_) :
                base((TPeerCnt)(Value_ & 0x00000000FFFFFFFF), (TPeerCnt)(Value_ >> 32))
            {
            }
            public Int64 Value
            {
                get
                {
                    return (((Int64)PeerCounter << 32) + PeerNum);
                }
            }
            public static implicit operator bool(CKey Key_)
            {
                return (Key_ != null && Key_.PeerNum != CBase.c_PeerCnt_Null);
            }
            public override bool Equals(object Obj_)
            {
                var Key = Obj_ as CKey;
                if (Key == null)
                    return false;

                return (PeerNum == Key.PeerNum && PeerCounter == Key.PeerCounter);
            }
            public override Int32 GetHashCode()
            {
                return unchecked((int)(PeerNum ^ PeerCounter));
            }
            public override string ToString()
            {
                return "[" + PeerNum.ToString() + "," + PeerCounter.ToString() + "]";
            }
        }
        public class CNamePort : SNamePort
        {
            public CNamePort()
            {
            }
            public CNamePort(SNamePort NamePort_) :
                base(NamePort_)
            {
            }
            public CNamePort(string Name_, TPort Port_) :
                base(Name_, Port_)
            {
            }
            public CNamePort(TPort Port_) :
                base("0.0.0.0", Port_)
            {
            }
            public CNamePort(IPEndPoint IPEndPoint_)
            {
                Name = IPEndPoint_.Address.ToString();
                Port = Convert.ToUInt16(IPEndPoint_.Port);
            }
            public static implicit operator bool(CNamePort NamePort_)
            {
                return (NamePort_.Port != 0);
            }
            public void Clear()
            {
                Name = string.Empty;
                Port = 0;
            }
            public override bool Equals(object Object_)
            {
                CNamePort NamePort = Object_ as CNamePort;
                if (NamePort == null)
                    return false;

                return (Name == NamePort.Name && Port == NamePort.Port);
            }
            public override Int32 GetHashCode()
            {
                return (Name.GetHashCode() ^ Port);
            }
            public IPAddress[] GetIPAddresses()
            {
                return Dns.GetHostAddresses(Name);
            }
        }
        public class ExceptionExtern : Exception
        {
            public ExceptionExtern()
            {
            }
            public ExceptionExtern(string Msg_) :
                base(Msg_)
            {
            }
        }
        public class CGlobal
        {
            public static TLongIP GetLongIPByName(String Name_)
            {
                try
                {
                    var LongIPs = Dns.GetHostAddresses(Name_);
                    if (LongIPs.Count() == 0)
                        return 0;

                    Byte[] AddressBytes = LongIPs.First().GetAddressBytes();
                    if (AddressBytes.Length != 4)
                        throw new Exception("Not an IPv4 address");

                    return (TLongIP)IPAddress.NetworkToHostOrder(BitConverter.ToInt32(AddressBytes, 0));
                }
                catch
                {
                    return 0;
                }
            }
        }
        public interface INet
        {
            void Dispose();
            bool IsLinked(TPeerCnt PeerNum_);
            void Close(TPeerCnt PeerNum_);
            bool Close(CKey Key_);
            void CloseAll();
            void WillClose(TPeerCnt PeerNum_, TimeSpan WaitDuration_);
            bool WillClose(CKey Key_, TimeSpan WaitDuration_);
            TPeerCnt GetPeerCnt();
            TimeSpan Latency(TPeerCnt PeerNum_);
            void Proc();
        }
        public interface IClient
        {
            bool IsConnecting(TPeerCnt PeerNum_);
            CKey Connect(CNamePort NamePort_, TPeerCnt PeerNum_);
            CKey Connect(CNamePort NamePort_);
        }
    }
}