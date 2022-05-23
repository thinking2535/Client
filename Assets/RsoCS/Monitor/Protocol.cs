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
	namespace monitor
	{
		using rso.net;
		public enum ECsProto
		{
			Login,
			UserProto,
			ToAgent,
			Max,
			Nul,
		}
		public class SCsHeader : SProto
		{
			public ECsProto Proto = default(ECsProto);
			public List<SKey> AgentKeys = new List<SKey>();
			public SCsHeader()
			{
			}
			public SCsHeader(SCsHeader Obj_)
			{
				Proto = Obj_.Proto;
				AgentKeys = Obj_.AgentKeys;
			}
			public SCsHeader(ECsProto Proto_, List<SKey> AgentKeys_)
			{
				Proto = Proto_;
				AgentKeys = AgentKeys_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Proto);
				Stream_.Pop(ref AgentKeys);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Proto", ref Proto);
				Value_.Pop("AgentKeys", ref AgentKeys);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Proto);
				Stream_.Push(AgentKeys);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Proto", Proto);
				Value_.Push("AgentKeys", AgentKeys);
			}
			public void Set(SCsHeader Obj_)
			{
				Proto = Obj_.Proto;
				AgentKeys = Obj_.AgentKeys;
			}
			public override string StdName()
			{
				return 
					"rso.monitor.ECsProto" + "," + 
					SEnumChecker.GetStdName(AgentKeys);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Proto, "Proto") + "," + 
					SEnumChecker.GetMemberName(AgentKeys, "AgentKeys");
			}
		}
		public class SCsLogin : SProto
		{
			public String ID = string.Empty;
			public String PW = string.Empty;
			public SCsLogin()
			{
			}
			public SCsLogin(SCsLogin Obj_)
			{
				ID = Obj_.ID;
				PW = Obj_.PW;
			}
			public SCsLogin(String ID_, String PW_)
			{
				ID = ID_;
				PW = PW_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ID);
				Stream_.Pop(ref PW);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ID", ref ID);
				Value_.Pop("PW", ref PW);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ID);
				Stream_.Push(PW);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ID", ID);
				Value_.Push("PW", PW);
			}
			public void Set(SCsLogin Obj_)
			{
				ID = Obj_.ID;
				PW = Obj_.PW;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ID) + "," + 
					SEnumChecker.GetStdName(PW);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ID, "ID") + "," + 
					SEnumChecker.GetMemberName(PW, "PW");
			}
		}
		public enum EScProto
		{
			AgentOn,
			AgentOff,
			ProcOn,
			ProcOff,
			AgentInfos,
			AgentOption,
			AgentStat,
			ProcStat,
			Notify,
			Max,
			Null,
		}
		public class SAgentOption : SProto
		{
			public Boolean KeepAlive = default(Boolean);
			public SAgentOption()
			{
			}
			public SAgentOption(SAgentOption Obj_)
			{
				KeepAlive = Obj_.KeepAlive;
			}
			public SAgentOption(Boolean KeepAlive_)
			{
				KeepAlive = KeepAlive_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref KeepAlive);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("KeepAlive", ref KeepAlive);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(KeepAlive);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("KeepAlive", KeepAlive);
			}
			public void Set(SAgentOption Obj_)
			{
				KeepAlive = Obj_.KeepAlive;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(KeepAlive);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(KeepAlive, "KeepAlive");
			}
		}
		public class SAgent : SProto
		{
			public String Name = string.Empty;
			public SAgentOption Option = new SAgentOption();
			public String Stat = string.Empty;
			public SAgent()
			{
			}
			public SAgent(SAgent Obj_)
			{
				Name = Obj_.Name;
				Option = Obj_.Option;
				Stat = Obj_.Stat;
			}
			public SAgent(String Name_, SAgentOption Option_, String Stat_)
			{
				Name = Name_;
				Option = Option_;
				Stat = Stat_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Name);
				Stream_.Pop(ref Option);
				Stream_.Pop(ref Stat);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Name", ref Name);
				Value_.Pop("Option", ref Option);
				Value_.Pop("Stat", ref Stat);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Name);
				Stream_.Push(Option);
				Stream_.Push(Stat);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Name", Name);
				Value_.Push("Option", Option);
				Value_.Push("Stat", Stat);
			}
			public void Set(SAgent Obj_)
			{
				Name = Obj_.Name;
				Option.Set(Obj_.Option);
				Stat = Obj_.Stat;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Name) + "," + 
					SEnumChecker.GetStdName(Option) + "," + 
					SEnumChecker.GetStdName(Stat);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Name, "Name") + "," + 
					SEnumChecker.GetMemberName(Option, "Option") + "," + 
					SEnumChecker.GetMemberName(Stat, "Stat");
			}
		}
		public class SProc : SProto
		{
			public String Name = string.Empty;
			public String Stat = string.Empty;
			public SProc()
			{
			}
			public SProc(SProc Obj_)
			{
				Name = Obj_.Name;
				Stat = Obj_.Stat;
			}
			public SProc(String Name_, String Stat_)
			{
				Name = Name_;
				Stat = Stat_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Name);
				Stream_.Pop(ref Stat);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Name", ref Name);
				Value_.Pop("Stat", ref Stat);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Name);
				Stream_.Push(Stat);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Name", Name);
				Value_.Push("Stat", Stat);
			}
			public void Set(SProc Obj_)
			{
				Name = Obj_.Name;
				Stat = Obj_.Stat;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Name) + "," + 
					SEnumChecker.GetStdName(Stat);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Name, "Name") + "," + 
					SEnumChecker.GetMemberName(Stat, "Stat");
			}
		}
		public class SAgentInfo : SProto
		{
			public SKey Key = new SKey();
			public SAgent Agent = new SAgent();
			public SProc Proc = new SProc();
			public SAgentInfo()
			{
			}
			public SAgentInfo(SAgentInfo Obj_)
			{
				Key = Obj_.Key;
				Agent = Obj_.Agent;
				Proc = Obj_.Proc;
			}
			public SAgentInfo(SKey Key_, SAgent Agent_, SProc Proc_)
			{
				Key = Key_;
				Agent = Agent_;
				Proc = Proc_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Key);
				Stream_.Pop(ref Agent);
				Stream_.Pop(ref Proc);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Key", ref Key);
				Value_.Pop("Agent", ref Agent);
				Value_.Pop("Proc", ref Proc);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Key);
				Stream_.Push(Agent);
				Stream_.Push(Proc);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Key", Key);
				Value_.Push("Agent", Agent);
				Value_.Push("Proc", Proc);
			}
			public void Set(SAgentInfo Obj_)
			{
				Key.Set(Obj_.Key);
				Agent.Set(Obj_.Agent);
				Proc.Set(Obj_.Proc);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Key) + "," + 
					SEnumChecker.GetStdName(Agent) + "," + 
					SEnumChecker.GetStdName(Proc);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Key, "Key") + "," + 
					SEnumChecker.GetMemberName(Agent, "Agent") + "," + 
					SEnumChecker.GetMemberName(Proc, "Proc");
			}
		}
		public class SScHeader : SProto
		{
			public EScProto Proto = default(EScProto);
			public SScHeader()
			{
			}
			public SScHeader(SScHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public SScHeader(EScProto Proto_)
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
			public void Set(SScHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public override string StdName()
			{
				return 
					"rso.monitor.EScProto";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Proto, "Proto");
			}
		}
		public class SScAgentOn : SAgent
		{
			public SKey Key = new SKey();
			public SScAgentOn()
			{
			}
			public SScAgentOn(SScAgentOn Obj_) : base(Obj_)
			{
				Key = Obj_.Key;
			}
			public SScAgentOn(SAgent Super_, SKey Key_) : base(Super_)
			{
				Key = Key_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref Key);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("Key", ref Key);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(Key);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("Key", Key);
			}
			public void Set(SScAgentOn Obj_)
			{
				base.Set(Obj_);
				Key.Set(Obj_.Key);
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(Key);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(Key, "Key");
			}
		}
		public class SScAgentOff : SProto
		{
			public SKey Key = new SKey();
			public SScAgentOff()
			{
			}
			public SScAgentOff(SScAgentOff Obj_)
			{
				Key = Obj_.Key;
			}
			public SScAgentOff(SKey Key_)
			{
				Key = Key_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Key);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Key", ref Key);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Key);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Key", Key);
			}
			public void Set(SScAgentOff Obj_)
			{
				Key.Set(Obj_.Key);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Key);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Key, "Key");
			}
		}
		public class SScProcOn : SProto
		{
			public SKey Key = new SKey();
			public SProc Proc = new SProc();
			public SScProcOn()
			{
			}
			public SScProcOn(SScProcOn Obj_)
			{
				Key = Obj_.Key;
				Proc = Obj_.Proc;
			}
			public SScProcOn(SKey Key_, SProc Proc_)
			{
				Key = Key_;
				Proc = Proc_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Key);
				Stream_.Pop(ref Proc);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Key", ref Key);
				Value_.Pop("Proc", ref Proc);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Key);
				Stream_.Push(Proc);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Key", Key);
				Value_.Push("Proc", Proc);
			}
			public void Set(SScProcOn Obj_)
			{
				Key.Set(Obj_.Key);
				Proc.Set(Obj_.Proc);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Key) + "," + 
					SEnumChecker.GetStdName(Proc);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Key, "Key") + "," + 
					SEnumChecker.GetMemberName(Proc, "Proc");
			}
		}
		public class SScProcOff : SProto
		{
			public SKey Key = new SKey();
			public SScProcOff()
			{
			}
			public SScProcOff(SScProcOff Obj_)
			{
				Key = Obj_.Key;
			}
			public SScProcOff(SKey Key_)
			{
				Key = Key_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Key);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Key", ref Key);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Key);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Key", Key);
			}
			public void Set(SScProcOff Obj_)
			{
				Key.Set(Obj_.Key);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Key);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Key, "Key");
			}
		}
		public class SScAgentInfos : SProto
		{
			public List<SAgentInfo> Infos = new List<SAgentInfo>();
			public SScAgentInfos()
			{
			}
			public SScAgentInfos(SScAgentInfos Obj_)
			{
				Infos = Obj_.Infos;
			}
			public SScAgentInfos(List<SAgentInfo> Infos_)
			{
				Infos = Infos_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Infos);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Infos", ref Infos);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Infos);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Infos", Infos);
			}
			public void Set(SScAgentInfos Obj_)
			{
				Infos = Obj_.Infos;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Infos);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Infos, "Infos");
			}
		}
		public class SKeyData : SProto
		{
			public String Key = string.Empty;
			public String Data = string.Empty;
			public SKeyData()
			{
			}
			public SKeyData(SKeyData Obj_)
			{
				Key = Obj_.Key;
				Data = Obj_.Data;
			}
			public SKeyData(String Key_, String Data_)
			{
				Key = Key_;
				Data = Data_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Key);
				Stream_.Pop(ref Data);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Key", ref Key);
				Value_.Pop("Data", ref Data);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Key);
				Stream_.Push(Data);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Key", Key);
				Value_.Push("Data", Data);
			}
			public void Set(SKeyData Obj_)
			{
				Key = Obj_.Key;
				Data = Obj_.Data;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Key) + "," + 
					SEnumChecker.GetStdName(Data);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Key, "Key") + "," + 
					SEnumChecker.GetMemberName(Data, "Data");
			}
		}
		public class SScAgentOption : SProto
		{
			public SKey AgentKey = new SKey();
			public SAgentOption Option = new SAgentOption();
			public SScAgentOption()
			{
			}
			public SScAgentOption(SScAgentOption Obj_)
			{
				AgentKey = Obj_.AgentKey;
				Option = Obj_.Option;
			}
			public SScAgentOption(SKey AgentKey_, SAgentOption Option_)
			{
				AgentKey = AgentKey_;
				Option = Option_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref AgentKey);
				Stream_.Pop(ref Option);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("AgentKey", ref AgentKey);
				Value_.Pop("Option", ref Option);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(AgentKey);
				Stream_.Push(Option);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("AgentKey", AgentKey);
				Value_.Push("Option", Option);
			}
			public void Set(SScAgentOption Obj_)
			{
				AgentKey.Set(Obj_.AgentKey);
				Option.Set(Obj_.Option);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(AgentKey) + "," + 
					SEnumChecker.GetStdName(Option);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(AgentKey, "AgentKey") + "," + 
					SEnumChecker.GetMemberName(Option, "Option");
			}
		}
		public class SScAgentStat : SProto
		{
			public SKey AgentKey = new SKey();
			public SKeyData KeyData = new SKeyData();
			public SScAgentStat()
			{
			}
			public SScAgentStat(SScAgentStat Obj_)
			{
				AgentKey = Obj_.AgentKey;
				KeyData = Obj_.KeyData;
			}
			public SScAgentStat(SKey AgentKey_, SKeyData KeyData_)
			{
				AgentKey = AgentKey_;
				KeyData = KeyData_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref AgentKey);
				Stream_.Pop(ref KeyData);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("AgentKey", ref AgentKey);
				Value_.Pop("KeyData", ref KeyData);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(AgentKey);
				Stream_.Push(KeyData);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("AgentKey", AgentKey);
				Value_.Push("KeyData", KeyData);
			}
			public void Set(SScAgentStat Obj_)
			{
				AgentKey.Set(Obj_.AgentKey);
				KeyData.Set(Obj_.KeyData);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(AgentKey) + "," + 
					SEnumChecker.GetStdName(KeyData);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(AgentKey, "AgentKey") + "," + 
					SEnumChecker.GetMemberName(KeyData, "KeyData");
			}
		}
		public class SScProcStat : SProto
		{
			public SKey AgentKey = new SKey();
			public SKeyData KeyData = new SKeyData();
			public SScProcStat()
			{
			}
			public SScProcStat(SScProcStat Obj_)
			{
				AgentKey = Obj_.AgentKey;
				KeyData = Obj_.KeyData;
			}
			public SScProcStat(SKey AgentKey_, SKeyData KeyData_)
			{
				AgentKey = AgentKey_;
				KeyData = KeyData_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref AgentKey);
				Stream_.Pop(ref KeyData);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("AgentKey", ref AgentKey);
				Value_.Pop("KeyData", ref KeyData);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(AgentKey);
				Stream_.Push(KeyData);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("AgentKey", AgentKey);
				Value_.Push("KeyData", KeyData);
			}
			public void Set(SScProcStat Obj_)
			{
				AgentKey.Set(Obj_.AgentKey);
				KeyData.Set(Obj_.KeyData);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(AgentKey) + "," + 
					SEnumChecker.GetStdName(KeyData);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(AgentKey, "AgentKey") + "," + 
					SEnumChecker.GetMemberName(KeyData, "KeyData");
			}
		}
		public class SScNotify : SProto
		{
			public String Msg = string.Empty;
			public SScNotify()
			{
			}
			public SScNotify(SScNotify Obj_)
			{
				Msg = Obj_.Msg;
			}
			public SScNotify(String Msg_)
			{
				Msg = Msg_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Msg);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Msg", ref Msg);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Msg);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Msg", Msg);
			}
			public void Set(SScNotify Obj_)
			{
				Msg = Obj_.Msg;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Msg);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Msg, "Msg");
			}
		}
		public enum ESaProto
		{
			FileSend,
			KeepAlive,
			RunProcess,
			KillProcess,
			ShellCommand,
			UserProto,
			ToApp,
			Max,
			Null,
		}
		public class SSaHeader : SProto
		{
			public ESaProto Proto = default(ESaProto);
			public SSaHeader()
			{
			}
			public SSaHeader(SSaHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public SSaHeader(ESaProto Proto_)
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
			public void Set(SSaHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public override string StdName()
			{
				return 
					"rso.monitor.ESaProto";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Proto, "Proto");
			}
		}
		public class SFileInfo : SProto
		{
			public String PathName = string.Empty;
			public CStream Stream = new CStream();
			public SFileInfo()
			{
			}
			public SFileInfo(SFileInfo Obj_)
			{
				PathName = Obj_.PathName;
				Stream = Obj_.Stream;
			}
			public SFileInfo(String PathName_, CStream Stream_)
			{
				PathName = PathName_;
				Stream = Stream_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref PathName);
				Stream_.Pop(ref Stream);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("PathName", ref PathName);
				Value_.Pop("Stream", ref Stream);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(PathName);
				Stream_.Push(Stream);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("PathName", PathName);
				Value_.Push("Stream", Stream);
			}
			public void Set(SFileInfo Obj_)
			{
				PathName = Obj_.PathName;
				Stream.Set(Obj_.Stream);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(PathName) + "," + 
					SEnumChecker.GetStdName(Stream);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(PathName, "PathName") + "," + 
					SEnumChecker.GetMemberName(Stream, "Stream");
			}
		}
		public class SSaFileSend : SProto
		{
			public List<SFileInfo> Files = new List<SFileInfo>();
			public SSaFileSend()
			{
			}
			public SSaFileSend(SSaFileSend Obj_)
			{
				Files = Obj_.Files;
			}
			public SSaFileSend(List<SFileInfo> Files_)
			{
				Files = Files_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Files);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Files", ref Files);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Files);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Files", Files);
			}
			public void Set(SSaFileSend Obj_)
			{
				Files = Obj_.Files;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Files);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Files, "Files");
			}
		}
		public class SSaKeepAlive : SProto
		{
			public Boolean On = default(Boolean);
			public SSaKeepAlive()
			{
			}
			public SSaKeepAlive(SSaKeepAlive Obj_)
			{
				On = Obj_.On;
			}
			public SSaKeepAlive(Boolean On_)
			{
				On = On_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref On);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("On", ref On);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(On);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("On", On);
			}
			public void Set(SSaKeepAlive Obj_)
			{
				On = Obj_.On;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(On);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(On, "On");
			}
		}
		public class SSaRunProcess : SProto
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
		public class SSaKillProcess : SProto
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
		public class SSaShellCommand : SProto
		{
			public String Command = string.Empty;
			public SSaShellCommand()
			{
			}
			public SSaShellCommand(SSaShellCommand Obj_)
			{
				Command = Obj_.Command;
			}
			public SSaShellCommand(String Command_)
			{
				Command = Command_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Command);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Command", ref Command);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Command);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Command", Command);
			}
			public void Set(SSaShellCommand Obj_)
			{
				Command = Obj_.Command;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Command);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Command, "Command");
			}
		}
		public enum EAsProto
		{
			AgentOn,
			ProcOn,
			ProcOff,
			AgentOption,
			AgentStat,
			ProcStat,
			NotifyToClient,
			Max,
			Null,
		}
		public class SAsHeader : SProto
		{
			public EAsProto Proto = default(EAsProto);
			public SAsHeader()
			{
			}
			public SAsHeader(SAsHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public SAsHeader(EAsProto Proto_)
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
			public void Set(SAsHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public override string StdName()
			{
				return 
					"rso.monitor.EAsProto";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Proto, "Proto");
			}
		}
		public class SAsAgentOn : SAgent
		{
			public SAsAgentOn()
			{
			}
			public SAsAgentOn(SAsAgentOn Obj_) : base(Obj_)
			{
			}
			public SAsAgentOn(SAgent Super_) : base(Super_)
			{
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
			}
			public void Set(SAsAgentOn Obj_)
			{
				base.Set(Obj_);
			}
			public override string StdName()
			{
				return 
					base.StdName();
			}
			public override string MemberName()
			{
				return 
					base.MemberName();
			}
		}
		public class SAsProcOn : SProc
		{
			public SAsProcOn()
			{
			}
			public SAsProcOn(SAsProcOn Obj_) : base(Obj_)
			{
			}
			public SAsProcOn(SProc Super_) : base(Super_)
			{
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
			}
			public void Set(SAsProcOn Obj_)
			{
				base.Set(Obj_);
			}
			public override string StdName()
			{
				return 
					base.StdName();
			}
			public override string MemberName()
			{
				return 
					base.MemberName();
			}
		}
		public class SAsProcOff : SProto
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
		public class SAsAgentOption : SProto
		{
			public SAgentOption Option = new SAgentOption();
			public SAsAgentOption()
			{
			}
			public SAsAgentOption(SAsAgentOption Obj_)
			{
				Option = Obj_.Option;
			}
			public SAsAgentOption(SAgentOption Option_)
			{
				Option = Option_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Option);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Option", ref Option);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Option);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Option", Option);
			}
			public void Set(SAsAgentOption Obj_)
			{
				Option.Set(Obj_.Option);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Option);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Option, "Option");
			}
		}
		public class SAsAgentStat : SKeyData
		{
			public SAsAgentStat()
			{
			}
			public SAsAgentStat(SAsAgentStat Obj_) : base(Obj_)
			{
			}
			public SAsAgentStat(SKeyData Super_) : base(Super_)
			{
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
			}
			public void Set(SAsAgentStat Obj_)
			{
				base.Set(Obj_);
			}
			public override string StdName()
			{
				return 
					base.StdName();
			}
			public override string MemberName()
			{
				return 
					base.MemberName();
			}
		}
		public class SAsProcStat : SKeyData
		{
			public SAsProcStat()
			{
			}
			public SAsProcStat(SAsProcStat Obj_) : base(Obj_)
			{
			}
			public SAsProcStat(SKeyData Super_) : base(Super_)
			{
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
			}
			public void Set(SAsProcStat Obj_)
			{
				base.Set(Obj_);
			}
			public override string StdName()
			{
				return 
					base.StdName();
			}
			public override string MemberName()
			{
				return 
					base.MemberName();
			}
		}
		public enum EApProto
		{
			Stop,
			Message,
			UserProto,
			Max,
			Null,
		}
		public class SApHeader : SProto
		{
			public EApProto Proto = default(EApProto);
			public SApHeader()
			{
			}
			public SApHeader(SApHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public SApHeader(EApProto Proto_)
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
			public void Set(SApHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public override string StdName()
			{
				return 
					"rso.monitor.EApProto";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Proto, "Proto");
			}
		}
		public class SApStop : SProto
		{
			public String Message = string.Empty;
			public Int32 SecondLeft = default(Int32);
			public SApStop()
			{
			}
			public SApStop(SApStop Obj_)
			{
				Message = Obj_.Message;
				SecondLeft = Obj_.SecondLeft;
			}
			public SApStop(String Message_, Int32 SecondLeft_)
			{
				Message = Message_;
				SecondLeft = SecondLeft_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Message);
				Stream_.Pop(ref SecondLeft);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Message", ref Message);
				Value_.Pop("SecondLeft", ref SecondLeft);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Message);
				Stream_.Push(SecondLeft);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Message", Message);
				Value_.Push("SecondLeft", SecondLeft);
			}
			public void Set(SApStop Obj_)
			{
				Message = Obj_.Message;
				SecondLeft = Obj_.SecondLeft;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Message) + "," + 
					SEnumChecker.GetStdName(SecondLeft);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Message, "Message") + "," + 
					SEnumChecker.GetMemberName(SecondLeft, "SecondLeft");
			}
		}
		public class SApMessage : SProto
		{
			public String Message = string.Empty;
			public SApMessage()
			{
			}
			public SApMessage(SApMessage Obj_)
			{
				Message = Obj_.Message;
			}
			public SApMessage(String Message_)
			{
				Message = Message_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Message);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Message", ref Message);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Message);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Message", Message);
			}
			public void Set(SApMessage Obj_)
			{
				Message = Obj_.Message;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Message);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Message, "Message");
			}
		}
		public enum EPaProto
		{
			ProcOn,
			Stat,
			NotifyToClient,
			Max,
			Null,
		}
		public class SPaHeader : SProto
		{
			public EPaProto Proto = default(EPaProto);
			public SPaHeader()
			{
			}
			public SPaHeader(SPaHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public SPaHeader(EPaProto Proto_)
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
			public void Set(SPaHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public override string StdName()
			{
				return 
					"rso.monitor.EPaProto";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Proto, "Proto");
			}
		}
		public class SPaProcOn : SProc
		{
			public SPaProcOn()
			{
			}
			public SPaProcOn(SPaProcOn Obj_) : base(Obj_)
			{
			}
			public SPaProcOn(SProc Super_) : base(Super_)
			{
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
			}
			public void Set(SPaProcOn Obj_)
			{
				base.Set(Obj_);
			}
			public override string StdName()
			{
				return 
					base.StdName();
			}
			public override string MemberName()
			{
				return 
					base.MemberName();
			}
		}
		public class SPaStat : SKeyData
		{
			public SPaStat()
			{
			}
			public SPaStat(SPaStat Obj_) : base(Obj_)
			{
			}
			public SPaStat(SKeyData Super_) : base(Super_)
			{
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
			}
			public void Set(SPaStat Obj_)
			{
				base.Set(Obj_);
			}
			public override string StdName()
			{
				return 
					base.StdName();
			}
			public override string MemberName()
			{
				return 
					base.MemberName();
			}
		}
		public class SNotifyToClient : SProto
		{
			public SKey Key = new SKey();
			public String Msg = string.Empty;
			public SNotifyToClient()
			{
			}
			public SNotifyToClient(SNotifyToClient Obj_)
			{
				Key = Obj_.Key;
				Msg = Obj_.Msg;
			}
			public SNotifyToClient(SKey Key_, String Msg_)
			{
				Key = Key_;
				Msg = Msg_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Key);
				Stream_.Pop(ref Msg);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Key", ref Key);
				Value_.Pop("Msg", ref Msg);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Key);
				Stream_.Push(Msg);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Key", Key);
				Value_.Push("Msg", Msg);
			}
			public void Set(SNotifyToClient Obj_)
			{
				Key.Set(Obj_.Key);
				Msg = Obj_.Msg;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Key) + "," + 
					SEnumChecker.GetStdName(Msg);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Key, "Key") + "," + 
					SEnumChecker.GetMemberName(Msg, "Msg");
			}
		}
	}
}
