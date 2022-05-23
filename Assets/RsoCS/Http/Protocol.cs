using TSize = System.Int32;
using TCheckSum = System.UInt64;
using TUID = System.Int64;
using TPeerCnt = System.UInt32;
using TLongIP = System.UInt32;
using TPort = System.UInt16;
using TPacketSeq = System.UInt64;
using TSessionCode = System.Int64;
using SRangeUID = rso.net.SRangeKey<System.Int64>;
using TVersion = System.Int32;
using TUpdateFiles = System.Collections.Generic.Dictionary<System.String,System.Boolean>;
using TFiles = System.Collections.Generic.Dictionary<System.String,rso.patch.SFile>;
using System;
using System.Collections.Generic;
using rso.core;


namespace rso
{
	using rso.net;
	namespace patch
	{
		public enum EProto
		{
			UdData,
			UdUpdate,
			CsLogin,
			ScPatchData,
			AmLogin,
			MaPatchData,
			AmUpdate,
			Max,
			Null,
		}
		public class SHeader : SProto
		{
			public EProto Proto = default(EProto);
			public SHeader()
			{
			}
			public SHeader(SHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public SHeader(EProto Proto_)
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
			public void Set(SHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public override string StdName()
			{
				return 
					"rso.patch.EProto";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Proto, "Proto");
			}
		}
		public class SFile : SProto
		{
			public Boolean IsAdded = default(Boolean);
			public TVersion SubVersion = default(TVersion);
			public SFile()
			{
			}
			public SFile(SFile Obj_)
			{
				IsAdded = Obj_.IsAdded;
				SubVersion = Obj_.SubVersion;
			}
			public SFile(Boolean IsAdded_, TVersion SubVersion_)
			{
				IsAdded = IsAdded_;
				SubVersion = SubVersion_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref IsAdded);
				Stream_.Pop(ref SubVersion);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("IsAdded", ref IsAdded);
				Value_.Pop("SubVersion", ref SubVersion);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(IsAdded);
				Stream_.Push(SubVersion);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("IsAdded", IsAdded);
				Value_.Push("SubVersion", SubVersion);
			}
			public void Set(SFile Obj_)
			{
				IsAdded = Obj_.IsAdded;
				SubVersion = Obj_.SubVersion;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(IsAdded) + "," + 
					SEnumChecker.GetStdName(SubVersion);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(IsAdded, "IsAdded") + "," + 
					SEnumChecker.GetMemberName(SubVersion, "SubVersion");
			}
		}
		public class SFiles : SProto
		{
			public TFiles Files = new TFiles();
			public SFiles()
			{
			}
			public SFiles(SFiles Obj_)
			{
				Files = Obj_.Files;
			}
			public SFiles(TFiles Files_)
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
			public void Set(SFiles Obj_)
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
		public class SServerData : SProto
		{
			public TVersion MainVersion = default(TVersion);
			public List<SFiles> Patches = new List<SFiles>();
			public SServerData()
			{
			}
			public SServerData(SServerData Obj_)
			{
				MainVersion = Obj_.MainVersion;
				Patches = Obj_.Patches;
			}
			public SServerData(TVersion MainVersion_, List<SFiles> Patches_)
			{
				MainVersion = MainVersion_;
				Patches = Patches_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref MainVersion);
				Stream_.Pop(ref Patches);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("MainVersion", ref MainVersion);
				Value_.Pop("Patches", ref Patches);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(MainVersion);
				Stream_.Push(Patches);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("MainVersion", MainVersion);
				Value_.Push("Patches", Patches);
			}
			public void Set(SServerData Obj_)
			{
				MainVersion = Obj_.MainVersion;
				Patches = Obj_.Patches;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(MainVersion) + "," + 
					SEnumChecker.GetStdName(Patches);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(MainVersion, "MainVersion") + "," + 
					SEnumChecker.GetMemberName(Patches, "Patches");
			}
		}
		public class SVersion : SProto
		{
			public TVersion Main = default(TVersion);
			public TVersion Sub = default(TVersion);
			public SVersion()
			{
			}
			public SVersion(SVersion Obj_)
			{
				Main = Obj_.Main;
				Sub = Obj_.Sub;
			}
			public SVersion(TVersion Main_, TVersion Sub_)
			{
				Main = Main_;
				Sub = Sub_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Main);
				Stream_.Pop(ref Sub);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Main", ref Main);
				Value_.Pop("Sub", ref Sub);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Main);
				Stream_.Push(Sub);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Main", Main);
				Value_.Push("Sub", Sub);
			}
			public void Set(SVersion Obj_)
			{
				Main = Obj_.Main;
				Sub = Obj_.Sub;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Main) + "," + 
					SEnumChecker.GetStdName(Sub);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Main, "Main") + "," + 
					SEnumChecker.GetMemberName(Sub, "Sub");
			}
		}
		public class SUpdateData : SProto
		{
			public Boolean IsReset = default(Boolean);
			public TUpdateFiles Files = new TUpdateFiles();
			public SUpdateData()
			{
			}
			public SUpdateData(SUpdateData Obj_)
			{
				IsReset = Obj_.IsReset;
				Files = Obj_.Files;
			}
			public SUpdateData(Boolean IsReset_, TUpdateFiles Files_)
			{
				IsReset = IsReset_;
				Files = Files_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref IsReset);
				Stream_.Pop(ref Files);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("IsReset", ref IsReset);
				Value_.Pop("Files", ref Files);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(IsReset);
				Stream_.Push(Files);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("IsReset", IsReset);
				Value_.Push("Files", Files);
			}
			public void Set(SUpdateData Obj_)
			{
				IsReset = Obj_.IsReset;
				Files = Obj_.Files;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(IsReset) + "," + 
					SEnumChecker.GetStdName(Files);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(IsReset, "IsReset") + "," + 
					SEnumChecker.GetMemberName(Files, "Files");
			}
		}
		public class SPatchData : SProto
		{
			public SVersion Version = new SVersion();
			public TFiles Files = new TFiles();
			public SPatchData()
			{
			}
			public SPatchData(SPatchData Obj_)
			{
				Version = Obj_.Version;
				Files = Obj_.Files;
			}
			public SPatchData(SVersion Version_, TFiles Files_)
			{
				Version = Version_;
				Files = Files_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Version);
				Stream_.Pop(ref Files);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Version", ref Version);
				Value_.Pop("Files", ref Files);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Version);
				Stream_.Push(Files);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Version", Version);
				Value_.Push("Files", Files);
			}
			public void Set(SPatchData Obj_)
			{
				Version.Set(Obj_.Version);
				Files = Obj_.Files;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Version) + "," + 
					SEnumChecker.GetStdName(Files);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Version, "Version") + "," + 
					SEnumChecker.GetMemberName(Files, "Files");
			}
		}
		public class SCsLogin : SProto
		{
			public SVersion Version = new SVersion();
			public SCsLogin()
			{
			}
			public SCsLogin(SCsLogin Obj_)
			{
				Version = Obj_.Version;
			}
			public SCsLogin(SVersion Version_)
			{
				Version = Version_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Version);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Version", ref Version);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Version);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Version", Version);
			}
			public void Set(SCsLogin Obj_)
			{
				Version.Set(Obj_.Version);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Version);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Version, "Version");
			}
		}
		public class SAmLogin : SProto
		{
			public String ID = string.Empty;
			public String PW = string.Empty;
			public SVersion Version = new SVersion();
			public SAmLogin()
			{
			}
			public SAmLogin(SAmLogin Obj_)
			{
				ID = Obj_.ID;
				PW = Obj_.PW;
				Version = Obj_.Version;
			}
			public SAmLogin(String ID_, String PW_, SVersion Version_)
			{
				ID = ID_;
				PW = PW_;
				Version = Version_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ID);
				Stream_.Pop(ref PW);
				Stream_.Pop(ref Version);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ID", ref ID);
				Value_.Pop("PW", ref PW);
				Value_.Pop("Version", ref Version);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ID);
				Stream_.Push(PW);
				Stream_.Push(Version);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ID", ID);
				Value_.Push("PW", PW);
				Value_.Push("Version", Version);
			}
			public void Set(SAmLogin Obj_)
			{
				ID = Obj_.ID;
				PW = Obj_.PW;
				Version.Set(Obj_.Version);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ID) + "," + 
					SEnumChecker.GetStdName(PW) + "," + 
					SEnumChecker.GetStdName(Version);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ID, "ID") + "," + 
					SEnumChecker.GetMemberName(PW, "PW") + "," + 
					SEnumChecker.GetMemberName(Version, "Version");
			}
		}
	}
}
