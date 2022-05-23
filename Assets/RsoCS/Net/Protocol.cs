using TSize = System.Int32;
using TCheckSum = System.UInt64;
using TUID = System.Int64;
using TPeerCnt = System.UInt32;
using TLongIP = System.UInt32;
using TPort = System.UInt16;
using TPacketSeq = System.UInt64;
using TSessionCode = System.Int64;
using SRangeUID = rso.net.SRangeKey<System.Int64>;
using System;
using System.Collections.Generic;
using rso.core;


namespace rso
{
	namespace net
	{
		public enum ENetRet
		{
			Ok,
			UserClose,
			HeartBeatFail,
			KeepConnectTimeOut,
			RecvError,
			ConnectFail,
			CertifyFail,
			SystemError,
			SocketError,
			InvalidPacket,
			Max,
			Null,
		}
		public enum EPacketType : SByte
		{
			Ping,
			Pong,
			User,
			Max,
			Null,
		}
		public enum ERecvState
		{
			NoData,
			PingPong,
			UserData,
			Error,
			Max,
			Null,
		}
		public class SKey : SProto
		{
			public TPeerCnt PeerNum = default(TPeerCnt);
			public TPeerCnt PeerCounter = default(TPeerCnt);
			public SKey()
			{
			}
			public SKey(SKey Obj_)
			{
				PeerNum = Obj_.PeerNum;
				PeerCounter = Obj_.PeerCounter;
			}
			public SKey(TPeerCnt PeerNum_, TPeerCnt PeerCounter_)
			{
				PeerNum = PeerNum_;
				PeerCounter = PeerCounter_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref PeerNum);
				Stream_.Pop(ref PeerCounter);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("PeerNum", ref PeerNum);
				Value_.Pop("PeerCounter", ref PeerCounter);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(PeerNum);
				Stream_.Push(PeerCounter);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("PeerNum", PeerNum);
				Value_.Push("PeerCounter", PeerCounter);
			}
			public void Set(SKey Obj_)
			{
				PeerNum = Obj_.PeerNum;
				PeerCounter = Obj_.PeerCounter;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(PeerNum) + "," + 
					SEnumChecker.GetStdName(PeerCounter);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(PeerNum, "PeerNum") + "," + 
					SEnumChecker.GetMemberName(PeerCounter, "PeerCounter");
			}
		}
		public class SHeader : SProto
		{
			public TSize Size = default(TSize);
			public TCheckSum CheckSum = default(TCheckSum);
			public TPacketSeq SendPacketSeq = default(TPacketSeq);
			public SHeader()
			{
			}
			public SHeader(SHeader Obj_)
			{
				Size = Obj_.Size;
				CheckSum = Obj_.CheckSum;
				SendPacketSeq = Obj_.SendPacketSeq;
			}
			public SHeader(TSize Size_, TCheckSum CheckSum_, TPacketSeq SendPacketSeq_)
			{
				Size = Size_;
				CheckSum = CheckSum_;
				SendPacketSeq = SendPacketSeq_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Size);
				Stream_.Pop(ref CheckSum);
				Stream_.Pop(ref SendPacketSeq);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Size", ref Size);
				Value_.Pop("CheckSum", ref CheckSum);
				Value_.Pop("SendPacketSeq", ref SendPacketSeq);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Size);
				Stream_.Push(CheckSum);
				Stream_.Push(SendPacketSeq);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Size", Size);
				Value_.Push("CheckSum", CheckSum);
				Value_.Push("SendPacketSeq", SendPacketSeq);
			}
			public void Set(SHeader Obj_)
			{
				Size = Obj_.Size;
				CheckSum = Obj_.CheckSum;
				SendPacketSeq = Obj_.SendPacketSeq;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Size) + "," + 
					SEnumChecker.GetStdName(CheckSum) + "," + 
					SEnumChecker.GetStdName(SendPacketSeq);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Size, "Size") + "," + 
					SEnumChecker.GetMemberName(CheckSum, "CheckSum") + "," + 
					SEnumChecker.GetMemberName(SendPacketSeq, "SendPacketSeq");
			}
		}
		public class SHeader2 : SProto
		{
			public EPacketType PacketType = default(EPacketType);
			public TPacketSeq RecvPacketSeq = default(TPacketSeq);
			public SHeader2()
			{
			}
			public SHeader2(SHeader2 Obj_)
			{
				PacketType = Obj_.PacketType;
				RecvPacketSeq = Obj_.RecvPacketSeq;
			}
			public SHeader2(EPacketType PacketType_, TPacketSeq RecvPacketSeq_)
			{
				PacketType = PacketType_;
				RecvPacketSeq = RecvPacketSeq_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref PacketType);
				Stream_.Pop(ref RecvPacketSeq);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("PacketType", ref PacketType);
				Value_.Pop("RecvPacketSeq", ref RecvPacketSeq);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(PacketType);
				Stream_.Push(RecvPacketSeq);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("PacketType", PacketType);
				Value_.Push("RecvPacketSeq", RecvPacketSeq);
			}
			public void Set(SHeader2 Obj_)
			{
				PacketType = Obj_.PacketType;
				RecvPacketSeq = Obj_.RecvPacketSeq;
			}
			public override string StdName()
			{
				return 
					"rso.net.EPacketType" + "," + 
					SEnumChecker.GetStdName(RecvPacketSeq);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(PacketType, "PacketType") + "," + 
					SEnumChecker.GetMemberName(RecvPacketSeq, "RecvPacketSeq");
			}
		}
		public class SNamePort : SProto
		{
			public String Name = string.Empty;
			public TPort Port = default(TPort);
			public SNamePort()
			{
			}
			public SNamePort(SNamePort Obj_)
			{
				Name = Obj_.Name;
				Port = Obj_.Port;
			}
			public SNamePort(String Name_, TPort Port_)
			{
				Name = Name_;
				Port = Port_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Name);
				Stream_.Pop(ref Port);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Name", ref Name);
				Value_.Pop("Port", ref Port);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Name);
				Stream_.Push(Port);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Name", Name);
				Value_.Push("Port", Port);
			}
			public void Set(SNamePort Obj_)
			{
				Name = Obj_.Name;
				Port = Obj_.Port;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Name) + "," + 
					SEnumChecker.GetStdName(Port);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Name, "Name") + "," + 
					SEnumChecker.GetMemberName(Port, "Port");
			}
		}
		public class SCountryCodeMinuteCountOffset : SProto
		{
			public String CountryCode = string.Empty;
			public Int32 MinuteCountOffset = default(Int32);
			public SCountryCodeMinuteCountOffset()
			{
			}
			public SCountryCodeMinuteCountOffset(SCountryCodeMinuteCountOffset Obj_)
			{
				CountryCode = Obj_.CountryCode;
				MinuteCountOffset = Obj_.MinuteCountOffset;
			}
			public SCountryCodeMinuteCountOffset(String CountryCode_, Int32 MinuteCountOffset_)
			{
				CountryCode = CountryCode_;
				MinuteCountOffset = MinuteCountOffset_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref CountryCode);
				Stream_.Pop(ref MinuteCountOffset);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("CountryCode", ref CountryCode);
				Value_.Pop("MinuteCountOffset", ref MinuteCountOffset);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(CountryCode);
				Stream_.Push(MinuteCountOffset);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("CountryCode", CountryCode);
				Value_.Push("MinuteCountOffset", MinuteCountOffset);
			}
			public void Set(SCountryCodeMinuteCountOffset Obj_)
			{
				CountryCode = Obj_.CountryCode;
				MinuteCountOffset = Obj_.MinuteCountOffset;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(CountryCode) + "," + 
					SEnumChecker.GetStdName(MinuteCountOffset);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(CountryCode, "CountryCode") + "," + 
					SEnumChecker.GetMemberName(MinuteCountOffset, "MinuteCountOffset");
			}
		}
		public class SRangeKey<TType> : SProto
		{
			public TType MinValue = default(TType);
			public TType MaxValue = default(TType);
			public SRangeKey()
			{
			}
			public SRangeKey(SRangeKey<TType> Obj_)
			{
				MinValue = Obj_.MinValue;
				MaxValue = Obj_.MaxValue;
			}
			public SRangeKey(TType MinValue_, TType MaxValue_)
			{
				MinValue = MinValue_;
				MaxValue = MaxValue_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref MinValue);
				Stream_.Pop(ref MaxValue);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("MinValue", ref MinValue);
				Value_.Pop("MaxValue", ref MaxValue);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(MinValue);
				Stream_.Push(MaxValue);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("MinValue", MinValue);
				Value_.Push("MaxValue", MaxValue);
			}
			public void Set(SRangeKey<TType> Obj_)
			{
				MinValue = Obj_.MinValue;
				MaxValue = Obj_.MaxValue;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(MinValue) + "," + 
					SEnumChecker.GetStdName(MaxValue);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(MinValue, "MinValue") + "," + 
					SEnumChecker.GetMemberName(MaxValue, "MaxValue");
			}
		}
	}
}
