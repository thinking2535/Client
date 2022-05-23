using TSize = System.Int32;
using TCheckSum = System.UInt64;
using TUID = System.Int64;
using TPeerCnt = System.UInt32;
using TLongIP = System.UInt32;
using TPort = System.UInt16;
using TPacketSeq = System.UInt64;
using TSessionCode = System.Int64;
using SRangeUID = rso.net.SRangeKey<System.Int64>;
using TProtoSeq = System.UInt64;
using System;
using System.Collections.Generic;
using rso.core;


namespace rso
{
	using rso.net;
	namespace mobile
	{
		public enum EProtoCs
		{
			Link,
			ReLink,
			UnLink,
			Ack,
			ReSend,
			UserProto,
			Max,
			Null,
		}
		public enum EProtoSc
		{
			Link,
			ReLink,
			UnLink,
			Ack,
			ReSend,
			UserProto,
			Max,
			Null,
		}
		public class SHeaderCs : SProto
		{
			public EProtoCs Proto = default(EProtoCs);
			public SHeaderCs()
			{
			}
			public SHeaderCs(SHeaderCs Obj_)
			{
				Proto = Obj_.Proto;
			}
			public SHeaderCs(EProtoCs Proto_)
			{
				Proto = Proto_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Proto);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Proto", ref Proto);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Proto);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Proto", Proto);
			}
			public void Set(SHeaderCs Obj_)
			{
				Proto = Obj_.Proto;
			}
			public override string StdName()
			{
				return 
					"rso.mobile.EProtoCs";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Proto, "Proto");
			}
		}
		public class SHeaderSc : SProto
		{
			public EProtoSc Proto = default(EProtoSc);
			public SHeaderSc()
			{
			}
			public SHeaderSc(SHeaderSc Obj_)
			{
				Proto = Obj_.Proto;
			}
			public SHeaderSc(EProtoSc Proto_)
			{
				Proto = Proto_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Proto);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Proto", ref Proto);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Proto);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Proto", Proto);
			}
			public void Set(SHeaderSc Obj_)
			{
				Proto = Obj_.Proto;
			}
			public override string StdName()
			{
				return 
					"rso.mobile.EProtoSc";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Proto, "Proto");
			}
		}
		public class SLinkCs : SProto
		{
			public override void Push(CStream Stream_)
			{
			}
			public override void Push(JsonDataObject Value_)
			{
			}
			public override void Pop(CStream Stream_)
			{
			}
			public override void Pop(JsonDataObject Value_)
			{
			}
			public override string StdName()
			{
				return "";
			}
			public override string MemberName()
			{
				return "";
			}
		}
		public class SLinkSc : SProto
		{
			public SKey ServerExtKey = new SKey();
			public SLinkSc()
			{
			}
			public SLinkSc(SLinkSc Obj_)
			{
				ServerExtKey = Obj_.ServerExtKey;
			}
			public SLinkSc(SKey ServerExtKey_)
			{
				ServerExtKey = ServerExtKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ServerExtKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ServerExtKey", ref ServerExtKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ServerExtKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ServerExtKey", ServerExtKey);
			}
			public void Set(SLinkSc Obj_)
			{
				ServerExtKey.Set(Obj_.ServerExtKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ServerExtKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ServerExtKey, "ServerExtKey");
			}
		}
		public class SReLinkCs : SProto
		{
			public SKey ServerExtKey = new SKey();
			public TProtoSeq ClientProtoSeqMustRecv = default(TProtoSeq);
			public SReLinkCs()
			{
			}
			public SReLinkCs(SReLinkCs Obj_)
			{
				ServerExtKey = Obj_.ServerExtKey;
				ClientProtoSeqMustRecv = Obj_.ClientProtoSeqMustRecv;
			}
			public SReLinkCs(SKey ServerExtKey_, TProtoSeq ClientProtoSeqMustRecv_)
			{
				ServerExtKey = ServerExtKey_;
				ClientProtoSeqMustRecv = ClientProtoSeqMustRecv_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ServerExtKey);
				Stream_.Pop(ref ClientProtoSeqMustRecv);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ServerExtKey", ref ServerExtKey);
				Value_.Pop("ClientProtoSeqMustRecv", ref ClientProtoSeqMustRecv);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ServerExtKey);
				Stream_.Push(ClientProtoSeqMustRecv);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ServerExtKey", ServerExtKey);
				Value_.Push("ClientProtoSeqMustRecv", ClientProtoSeqMustRecv);
			}
			public void Set(SReLinkCs Obj_)
			{
				ServerExtKey.Set(Obj_.ServerExtKey);
				ClientProtoSeqMustRecv = Obj_.ClientProtoSeqMustRecv;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ServerExtKey) + "," + 
					SEnumChecker.GetStdName(ClientProtoSeqMustRecv);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ServerExtKey, "ServerExtKey") + "," + 
					SEnumChecker.GetMemberName(ClientProtoSeqMustRecv, "ClientProtoSeqMustRecv");
			}
		}
		public class SReLinkSc : SProto
		{
			public TProtoSeq ServerProtoSeqMustRecv = default(TProtoSeq);
			public SReLinkSc()
			{
			}
			public SReLinkSc(SReLinkSc Obj_)
			{
				ServerProtoSeqMustRecv = Obj_.ServerProtoSeqMustRecv;
			}
			public SReLinkSc(TProtoSeq ServerProtoSeqMustRecv_)
			{
				ServerProtoSeqMustRecv = ServerProtoSeqMustRecv_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ServerProtoSeqMustRecv);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ServerProtoSeqMustRecv", ref ServerProtoSeqMustRecv);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ServerProtoSeqMustRecv);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ServerProtoSeqMustRecv", ServerProtoSeqMustRecv);
			}
			public void Set(SReLinkSc Obj_)
			{
				ServerProtoSeqMustRecv = Obj_.ServerProtoSeqMustRecv;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ServerProtoSeqMustRecv);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ServerProtoSeqMustRecv, "ServerProtoSeqMustRecv");
			}
		}
		public class SUnLinkCs : SProto
		{
			public override void Push(CStream Stream_)
			{
			}
			public override void Push(JsonDataObject Value_)
			{
			}
			public override void Pop(CStream Stream_)
			{
			}
			public override void Pop(JsonDataObject Value_)
			{
			}
			public override string StdName()
			{
				return "";
			}
			public override string MemberName()
			{
				return "";
			}
		}
		public class SUnLinkSc : SProto
		{
			public override void Push(CStream Stream_)
			{
			}
			public override void Push(JsonDataObject Value_)
			{
			}
			public override void Pop(CStream Stream_)
			{
			}
			public override void Pop(JsonDataObject Value_)
			{
			}
			public override string StdName()
			{
				return "";
			}
			public override string MemberName()
			{
				return "";
			}
		}
	}
}
