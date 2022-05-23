using TCode = System.Int32;
using System;
using System.Collections.Generic;
using rso.core;


namespace rso
{
	namespace gameutil
	{
		public enum EOS : Byte
		{
			Android,
			iOS,
			Max,
			Null,
		}
		public class SIndexCode : SProto
		{
			public Int64 Index = default(Int64);
			public TCode Code = default(TCode);
			public SIndexCode()
			{
			}
			public SIndexCode(SIndexCode Obj_)
			{
				Index = Obj_.Index;
				Code = Obj_.Code;
			}
			public SIndexCode(Int64 Index_, TCode Code_)
			{
				Index = Index_;
				Code = Code_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Index);
				Stream_.Pop(ref Code);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Index", ref Index);
				Value_.Pop("Code", ref Code);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Index);
				Stream_.Push(Code);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Index", Index);
				Value_.Push("Code", Code);
			}
			public void Set(SIndexCode Obj_)
			{
				Index = Obj_.Index;
				Code = Obj_.Code;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Index) + "," + 
					SEnumChecker.GetStdName(Code);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Index, "Index") + "," + 
					SEnumChecker.GetMemberName(Code, "Code");
			}
		}
		public class SIndexCodeCnt : SIndexCode
		{
			public Int32 Cnt = default(Int32);
			public SIndexCodeCnt()
			{
			}
			public SIndexCodeCnt(SIndexCodeCnt Obj_) : base(Obj_)
			{
				Cnt = Obj_.Cnt;
			}
			public SIndexCodeCnt(SIndexCode Super_, Int32 Cnt_) : base(Super_)
			{
				Cnt = Cnt_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref Cnt);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("Cnt", ref Cnt);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(Cnt);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("Cnt", Cnt);
			}
			public void Set(SIndexCodeCnt Obj_)
			{
				base.Set(Obj_);
				Cnt = Obj_.Cnt;
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(Cnt);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(Cnt, "Cnt");
			}
		}
		public class STimeBoost : SProto
		{
			public TimePoint EndTime = default(TimePoint);
			public Double Speed = default(Double);
			public STimeBoost()
			{
			}
			public STimeBoost(STimeBoost Obj_)
			{
				EndTime = Obj_.EndTime;
				Speed = Obj_.Speed;
			}
			public STimeBoost(TimePoint EndTime_, Double Speed_)
			{
				EndTime = EndTime_;
				Speed = Speed_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref EndTime);
				Stream_.Pop(ref Speed);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("EndTime", ref EndTime);
				Value_.Pop("Speed", ref Speed);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(EndTime);
				Stream_.Push(Speed);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("EndTime", EndTime);
				Value_.Push("Speed", Speed);
			}
			public void Set(STimeBoost Obj_)
			{
				EndTime = Obj_.EndTime;
				Speed = Obj_.Speed;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(EndTime) + "," + 
					SEnumChecker.GetStdName(Speed);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(EndTime, "EndTime") + "," + 
					SEnumChecker.GetMemberName(Speed, "Speed");
			}
		}
	}
}
