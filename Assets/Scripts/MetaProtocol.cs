using TSize = System.Int32;
using TCheckSum = System.UInt64;
using TUID = System.Int64;
using TPeerCnt = System.UInt32;
using TLongIP = System.UInt32;
using TPort = System.UInt16;
using TPacketSeq = System.UInt64;
using TSessionCode = System.Int64;
using SRangeUID = rso.net.SRangeKey<System.Int64>;
using TVer = System.SByte;
using TID = System.String;
using TNick = System.String;
using TMessage = System.String;
using TState = System.Byte;
using TServerNets = System.Collections.Generic.HashSet<rso.game.SServerNet>;
using TMasterNets = System.Collections.Generic.List<rso.game.SMasterNet>;
using TFriendDBs = System.Collections.Generic.Dictionary<System.Int64,rso.game.SFriendDB>;
using TFriends = System.Collections.Generic.Dictionary<System.Int64,rso.game.SFriend>;
using TUIDFriendInfos = System.Collections.Generic.List<rso.game.SUIDFriendInfo>;
using TFriendInfos = System.Collections.Generic.Dictionary<System.Int64,rso.game.SFriendInfo>;
using TCode = System.Int32;
using TIndex = System.Int64;
using TLevel = System.Int32;
using THP = System.Int32;
using TSlotNo = System.SByte;
using TExp = System.Int32;
using TRank = System.Int32;
using TTeamCnt = System.SByte;
using TQuestSlotIndex = System.Byte;
using TResource = System.Int32;
using TDoneQuests = System.Collections.Generic.List<bb.SQuestSlotIndexCount>;
using TChars = System.Collections.Generic.HashSet<System.Int32>;
using TQuestDBs = System.Collections.Generic.Dictionary<System.Byte,bb.SQuestBase>;
using TPackages = System.Collections.Generic.HashSet<System.Int32>;
using TTeamBattleInfos = System.Collections.Generic.List<bb.STeamBattleInfo>;
using TRankingUsers = System.Collections.Generic.List<bb.SRankingUser>;
using TRankingUserSingles = System.Collections.Generic.List<bb.SRankingUserSingle>;
using TRankingUserIslands = System.Collections.Generic.List<bb.SRankingUserIsland>;
using TQuestSlotIndexCodes = System.Collections.Generic.List<bb.SQuestSlotIndexCode>;
using TRankingRewards = System.Collections.Generic.Dictionary<System.Int64,System.Int32>;
using SRooms = System.Collections.Generic.Dictionary<System.Int32,bb.SRoomInfo>;
using TPoses = System.Collections.Generic.List<rso.physics.SPoint>;
using System;
using System.Collections.Generic;
using rso.core;


namespace bb
{
	using rso.physics;
	public enum ECashItemType
	{
		ResourcesPack,
		Max,
		Null,
	}
	public enum EGoodsItemType
	{
		AvatarColor,
		Max,
		Null,
	}
	public enum ERank : Byte
	{
		Unranked,
		Bronze,
		Silver,
		Gold,
		Diamond,
		Champion,
		Max,
	}
	public class SServerConfigMeta : SProto
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
	public class SStructure : SRectCollider2D
	{
		public SPoint LocalPosition = new SPoint();
		public SStructure()
		{
		}
		public SStructure(SStructure Obj_) : base(Obj_)
		{
			LocalPosition = Obj_.LocalPosition;
		}
		public SStructure(SRectCollider2D Super_, SPoint LocalPosition_) : base(Super_)
		{
			LocalPosition = LocalPosition_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref LocalPosition);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("LocalPosition", ref LocalPosition);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(LocalPosition);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("LocalPosition", LocalPosition);
		}
		public void Set(SStructure Obj_)
		{
			base.Set(Obj_);
			LocalPosition.Set(Obj_.LocalPosition);
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(LocalPosition);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(LocalPosition, "LocalPosition");
		}
	}
	public class SStructureMove : SProto
	{
		public List<SRectCollider2D> Colliders = new List<SRectCollider2D>();
		public SPoint BeginPos = new SPoint();
		public SPoint EndPos = new SPoint();
		public Single Velocity = default(Single);
		public Single Delay = default(Single);
		public SStructureMove()
		{
		}
		public SStructureMove(SStructureMove Obj_)
		{
			Colliders = Obj_.Colliders;
			BeginPos = Obj_.BeginPos;
			EndPos = Obj_.EndPos;
			Velocity = Obj_.Velocity;
			Delay = Obj_.Delay;
		}
		public SStructureMove(List<SRectCollider2D> Colliders_, SPoint BeginPos_, SPoint EndPos_, Single Velocity_, Single Delay_)
		{
			Colliders = Colliders_;
			BeginPos = BeginPos_;
			EndPos = EndPos_;
			Velocity = Velocity_;
			Delay = Delay_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Colliders);
			Stream_.Pop(ref BeginPos);
			Stream_.Pop(ref EndPos);
			Stream_.Pop(ref Velocity);
			Stream_.Pop(ref Delay);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Colliders", ref Colliders);
			Value_.Pop("BeginPos", ref BeginPos);
			Value_.Pop("EndPos", ref EndPos);
			Value_.Pop("Velocity", ref Velocity);
			Value_.Pop("Delay", ref Delay);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Colliders);
			Stream_.Push(BeginPos);
			Stream_.Push(EndPos);
			Stream_.Push(Velocity);
			Stream_.Push(Delay);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Colliders", Colliders);
			Value_.Push("BeginPos", BeginPos);
			Value_.Push("EndPos", EndPos);
			Value_.Push("Velocity", Velocity);
			Value_.Push("Delay", Delay);
		}
		public void Set(SStructureMove Obj_)
		{
			Colliders = Obj_.Colliders;
			BeginPos.Set(Obj_.BeginPos);
			EndPos.Set(Obj_.EndPos);
			Velocity = Obj_.Velocity;
			Delay = Obj_.Delay;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Colliders) + "," + 
				SEnumChecker.GetStdName(BeginPos) + "," + 
				SEnumChecker.GetStdName(EndPos) + "," + 
				SEnumChecker.GetStdName(Velocity) + "," + 
				SEnumChecker.GetStdName(Delay);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Colliders, "Colliders") + "," + 
				SEnumChecker.GetMemberName(BeginPos, "BeginPos") + "," + 
				SEnumChecker.GetMemberName(EndPos, "EndPos") + "," + 
				SEnumChecker.GetMemberName(Velocity, "Velocity") + "," + 
				SEnumChecker.GetMemberName(Delay, "Delay");
		}
	}
	public class SConfigMeta : SProto
	{
		public Int32 BattleDurationSec = default(Int32);
		public Int32 BattleOneOnOneDurationSec = default(Int32);
		public Int32 GhostDelaySec = default(Int32);
		public Int32 InvulnerableDurationSec = default(Int32);
		public Int32 BalloonHitPoint = default(Int32);
		public Int32 ParachuteHitPoint = default(Int32);
		public Int32 FirstBalloonHitPoint = default(Int32);
		public Int32 QuestCoolMinutes = default(Int32);
		public TResource QuestNextCostGold = default(TResource);
		public Int32 ChangeNickFreeCount = default(Int32);
		public TResource ChangeNickCostDia = default(TResource);
		public TResource TutorialRewardDia = default(TResource);
		public SConfigMeta()
		{
		}
		public SConfigMeta(SConfigMeta Obj_)
		{
			BattleDurationSec = Obj_.BattleDurationSec;
			BattleOneOnOneDurationSec = Obj_.BattleOneOnOneDurationSec;
			GhostDelaySec = Obj_.GhostDelaySec;
			InvulnerableDurationSec = Obj_.InvulnerableDurationSec;
			BalloonHitPoint = Obj_.BalloonHitPoint;
			ParachuteHitPoint = Obj_.ParachuteHitPoint;
			FirstBalloonHitPoint = Obj_.FirstBalloonHitPoint;
			QuestCoolMinutes = Obj_.QuestCoolMinutes;
			QuestNextCostGold = Obj_.QuestNextCostGold;
			ChangeNickFreeCount = Obj_.ChangeNickFreeCount;
			ChangeNickCostDia = Obj_.ChangeNickCostDia;
			TutorialRewardDia = Obj_.TutorialRewardDia;
		}
		public SConfigMeta(Int32 BattleDurationSec_, Int32 BattleOneOnOneDurationSec_, Int32 GhostDelaySec_, Int32 InvulnerableDurationSec_, Int32 BalloonHitPoint_, Int32 ParachuteHitPoint_, Int32 FirstBalloonHitPoint_, Int32 QuestCoolMinutes_, TResource QuestNextCostGold_, Int32 ChangeNickFreeCount_, TResource ChangeNickCostDia_, TResource TutorialRewardDia_)
		{
			BattleDurationSec = BattleDurationSec_;
			BattleOneOnOneDurationSec = BattleOneOnOneDurationSec_;
			GhostDelaySec = GhostDelaySec_;
			InvulnerableDurationSec = InvulnerableDurationSec_;
			BalloonHitPoint = BalloonHitPoint_;
			ParachuteHitPoint = ParachuteHitPoint_;
			FirstBalloonHitPoint = FirstBalloonHitPoint_;
			QuestCoolMinutes = QuestCoolMinutes_;
			QuestNextCostGold = QuestNextCostGold_;
			ChangeNickFreeCount = ChangeNickFreeCount_;
			ChangeNickCostDia = ChangeNickCostDia_;
			TutorialRewardDia = TutorialRewardDia_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref BattleDurationSec);
			Stream_.Pop(ref BattleOneOnOneDurationSec);
			Stream_.Pop(ref GhostDelaySec);
			Stream_.Pop(ref InvulnerableDurationSec);
			Stream_.Pop(ref BalloonHitPoint);
			Stream_.Pop(ref ParachuteHitPoint);
			Stream_.Pop(ref FirstBalloonHitPoint);
			Stream_.Pop(ref QuestCoolMinutes);
			Stream_.Pop(ref QuestNextCostGold);
			Stream_.Pop(ref ChangeNickFreeCount);
			Stream_.Pop(ref ChangeNickCostDia);
			Stream_.Pop(ref TutorialRewardDia);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("BattleDurationSec", ref BattleDurationSec);
			Value_.Pop("BattleOneOnOneDurationSec", ref BattleOneOnOneDurationSec);
			Value_.Pop("GhostDelaySec", ref GhostDelaySec);
			Value_.Pop("InvulnerableDurationSec", ref InvulnerableDurationSec);
			Value_.Pop("BalloonHitPoint", ref BalloonHitPoint);
			Value_.Pop("ParachuteHitPoint", ref ParachuteHitPoint);
			Value_.Pop("FirstBalloonHitPoint", ref FirstBalloonHitPoint);
			Value_.Pop("QuestCoolMinutes", ref QuestCoolMinutes);
			Value_.Pop("QuestNextCostGold", ref QuestNextCostGold);
			Value_.Pop("ChangeNickFreeCount", ref ChangeNickFreeCount);
			Value_.Pop("ChangeNickCostDia", ref ChangeNickCostDia);
			Value_.Pop("TutorialRewardDia", ref TutorialRewardDia);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(BattleDurationSec);
			Stream_.Push(BattleOneOnOneDurationSec);
			Stream_.Push(GhostDelaySec);
			Stream_.Push(InvulnerableDurationSec);
			Stream_.Push(BalloonHitPoint);
			Stream_.Push(ParachuteHitPoint);
			Stream_.Push(FirstBalloonHitPoint);
			Stream_.Push(QuestCoolMinutes);
			Stream_.Push(QuestNextCostGold);
			Stream_.Push(ChangeNickFreeCount);
			Stream_.Push(ChangeNickCostDia);
			Stream_.Push(TutorialRewardDia);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("BattleDurationSec", BattleDurationSec);
			Value_.Push("BattleOneOnOneDurationSec", BattleOneOnOneDurationSec);
			Value_.Push("GhostDelaySec", GhostDelaySec);
			Value_.Push("InvulnerableDurationSec", InvulnerableDurationSec);
			Value_.Push("BalloonHitPoint", BalloonHitPoint);
			Value_.Push("ParachuteHitPoint", ParachuteHitPoint);
			Value_.Push("FirstBalloonHitPoint", FirstBalloonHitPoint);
			Value_.Push("QuestCoolMinutes", QuestCoolMinutes);
			Value_.Push("QuestNextCostGold", QuestNextCostGold);
			Value_.Push("ChangeNickFreeCount", ChangeNickFreeCount);
			Value_.Push("ChangeNickCostDia", ChangeNickCostDia);
			Value_.Push("TutorialRewardDia", TutorialRewardDia);
		}
		public void Set(SConfigMeta Obj_)
		{
			BattleDurationSec = Obj_.BattleDurationSec;
			BattleOneOnOneDurationSec = Obj_.BattleOneOnOneDurationSec;
			GhostDelaySec = Obj_.GhostDelaySec;
			InvulnerableDurationSec = Obj_.InvulnerableDurationSec;
			BalloonHitPoint = Obj_.BalloonHitPoint;
			ParachuteHitPoint = Obj_.ParachuteHitPoint;
			FirstBalloonHitPoint = Obj_.FirstBalloonHitPoint;
			QuestCoolMinutes = Obj_.QuestCoolMinutes;
			QuestNextCostGold = Obj_.QuestNextCostGold;
			ChangeNickFreeCount = Obj_.ChangeNickFreeCount;
			ChangeNickCostDia = Obj_.ChangeNickCostDia;
			TutorialRewardDia = Obj_.TutorialRewardDia;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(BattleDurationSec) + "," + 
				SEnumChecker.GetStdName(BattleOneOnOneDurationSec) + "," + 
				SEnumChecker.GetStdName(GhostDelaySec) + "," + 
				SEnumChecker.GetStdName(InvulnerableDurationSec) + "," + 
				SEnumChecker.GetStdName(BalloonHitPoint) + "," + 
				SEnumChecker.GetStdName(ParachuteHitPoint) + "," + 
				SEnumChecker.GetStdName(FirstBalloonHitPoint) + "," + 
				SEnumChecker.GetStdName(QuestCoolMinutes) + "," + 
				SEnumChecker.GetStdName(QuestNextCostGold) + "," + 
				SEnumChecker.GetStdName(ChangeNickFreeCount) + "," + 
				SEnumChecker.GetStdName(ChangeNickCostDia) + "," + 
				SEnumChecker.GetStdName(TutorialRewardDia);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(BattleDurationSec, "BattleDurationSec") + "," + 
				SEnumChecker.GetMemberName(BattleOneOnOneDurationSec, "BattleOneOnOneDurationSec") + "," + 
				SEnumChecker.GetMemberName(GhostDelaySec, "GhostDelaySec") + "," + 
				SEnumChecker.GetMemberName(InvulnerableDurationSec, "InvulnerableDurationSec") + "," + 
				SEnumChecker.GetMemberName(BalloonHitPoint, "BalloonHitPoint") + "," + 
				SEnumChecker.GetMemberName(ParachuteHitPoint, "ParachuteHitPoint") + "," + 
				SEnumChecker.GetMemberName(FirstBalloonHitPoint, "FirstBalloonHitPoint") + "," + 
				SEnumChecker.GetMemberName(QuestCoolMinutes, "QuestCoolMinutes") + "," + 
				SEnumChecker.GetMemberName(QuestNextCostGold, "QuestNextCostGold") + "," + 
				SEnumChecker.GetMemberName(ChangeNickFreeCount, "ChangeNickFreeCount") + "," + 
				SEnumChecker.GetMemberName(ChangeNickCostDia, "ChangeNickCostDia") + "," + 
				SEnumChecker.GetMemberName(TutorialRewardDia, "TutorialRewardDia");
		}
	}
	public class SForbiddenWordMeta : SProto
	{
		public String Word = string.Empty;
		public SForbiddenWordMeta()
		{
		}
		public SForbiddenWordMeta(SForbiddenWordMeta Obj_)
		{
			Word = Obj_.Word;
		}
		public SForbiddenWordMeta(String Word_)
		{
			Word = Word_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Word);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Word", ref Word);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Word);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Word", Word);
		}
		public void Set(SForbiddenWordMeta Obj_)
		{
			Word = Obj_.Word;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Word);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Word, "Word");
		}
	}
	public class SShopConfigServerMeta : SProto
	{
		public Int32 DailyRewardDurationMinute = default(Int32);
		public Int32 DailyRewardFreeCount = default(Int32);
		public Int32 DailyRewardAdCount = default(Int32);
		public SShopConfigServerMeta()
		{
		}
		public SShopConfigServerMeta(SShopConfigServerMeta Obj_)
		{
			DailyRewardDurationMinute = Obj_.DailyRewardDurationMinute;
			DailyRewardFreeCount = Obj_.DailyRewardFreeCount;
			DailyRewardAdCount = Obj_.DailyRewardAdCount;
		}
		public SShopConfigServerMeta(Int32 DailyRewardDurationMinute_, Int32 DailyRewardFreeCount_, Int32 DailyRewardAdCount_)
		{
			DailyRewardDurationMinute = DailyRewardDurationMinute_;
			DailyRewardFreeCount = DailyRewardFreeCount_;
			DailyRewardAdCount = DailyRewardAdCount_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref DailyRewardDurationMinute);
			Stream_.Pop(ref DailyRewardFreeCount);
			Stream_.Pop(ref DailyRewardAdCount);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("DailyRewardDurationMinute", ref DailyRewardDurationMinute);
			Value_.Pop("DailyRewardFreeCount", ref DailyRewardFreeCount);
			Value_.Pop("DailyRewardAdCount", ref DailyRewardAdCount);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(DailyRewardDurationMinute);
			Stream_.Push(DailyRewardFreeCount);
			Stream_.Push(DailyRewardAdCount);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("DailyRewardDurationMinute", DailyRewardDurationMinute);
			Value_.Push("DailyRewardFreeCount", DailyRewardFreeCount);
			Value_.Push("DailyRewardAdCount", DailyRewardAdCount);
		}
		public void Set(SShopConfigServerMeta Obj_)
		{
			DailyRewardDurationMinute = Obj_.DailyRewardDurationMinute;
			DailyRewardFreeCount = Obj_.DailyRewardFreeCount;
			DailyRewardAdCount = Obj_.DailyRewardAdCount;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(DailyRewardDurationMinute) + "," + 
				SEnumChecker.GetStdName(DailyRewardFreeCount) + "," + 
				SEnumChecker.GetStdName(DailyRewardAdCount);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(DailyRewardDurationMinute, "DailyRewardDurationMinute") + "," + 
				SEnumChecker.GetMemberName(DailyRewardFreeCount, "DailyRewardFreeCount") + "," + 
				SEnumChecker.GetMemberName(DailyRewardAdCount, "DailyRewardAdCount");
		}
	}
	public class SShopServerMeta : SProto
	{
		public Int32 Code = default(Int32);
		public EResource CostType = default(EResource);
		public TResource CostValue = default(TResource);
		public Int32 RewardCode = default(Int32);
		public SShopServerMeta()
		{
		}
		public SShopServerMeta(SShopServerMeta Obj_)
		{
			Code = Obj_.Code;
			CostType = Obj_.CostType;
			CostValue = Obj_.CostValue;
			RewardCode = Obj_.RewardCode;
		}
		public SShopServerMeta(Int32 Code_, EResource CostType_, TResource CostValue_, Int32 RewardCode_)
		{
			Code = Code_;
			CostType = CostType_;
			CostValue = CostValue_;
			RewardCode = RewardCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
			Stream_.Pop(ref CostType);
			Stream_.Pop(ref CostValue);
			Stream_.Pop(ref RewardCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
			Value_.Pop("CostType", ref CostType);
			Value_.Pop("CostValue", ref CostValue);
			Value_.Pop("RewardCode", ref RewardCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
			Stream_.Push(CostType);
			Stream_.Push(CostValue);
			Stream_.Push(RewardCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
			Value_.Push("CostType", CostType);
			Value_.Push("CostValue", CostValue);
			Value_.Push("RewardCode", RewardCode);
		}
		public void Set(SShopServerMeta Obj_)
		{
			Code = Obj_.Code;
			CostType = Obj_.CostType;
			CostValue = Obj_.CostValue;
			RewardCode = Obj_.RewardCode;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code) + "," + 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(CostValue) + "," + 
				SEnumChecker.GetStdName(RewardCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(CostType, "CostType") + "," + 
				SEnumChecker.GetMemberName(CostValue, "CostValue") + "," + 
				SEnumChecker.GetMemberName(RewardCode, "RewardCode");
		}
	}
	public class SShopPackageServerMeta : SProto
	{
		public Int32 Code = default(Int32);
		public EResource CostType = default(EResource);
		public TResource CostValue = default(TResource);
		public Int32 RewardCode = default(Int32);
		public SShopPackageServerMeta()
		{
		}
		public SShopPackageServerMeta(SShopPackageServerMeta Obj_)
		{
			Code = Obj_.Code;
			CostType = Obj_.CostType;
			CostValue = Obj_.CostValue;
			RewardCode = Obj_.RewardCode;
		}
		public SShopPackageServerMeta(Int32 Code_, EResource CostType_, TResource CostValue_, Int32 RewardCode_)
		{
			Code = Code_;
			CostType = CostType_;
			CostValue = CostValue_;
			RewardCode = RewardCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
			Stream_.Pop(ref CostType);
			Stream_.Pop(ref CostValue);
			Stream_.Pop(ref RewardCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
			Value_.Pop("CostType", ref CostType);
			Value_.Pop("CostValue", ref CostValue);
			Value_.Pop("RewardCode", ref RewardCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
			Stream_.Push(CostType);
			Stream_.Push(CostValue);
			Stream_.Push(RewardCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
			Value_.Push("CostType", CostType);
			Value_.Push("CostValue", CostValue);
			Value_.Push("RewardCode", RewardCode);
		}
		public void Set(SShopPackageServerMeta Obj_)
		{
			Code = Obj_.Code;
			CostType = Obj_.CostType;
			CostValue = Obj_.CostValue;
			RewardCode = Obj_.RewardCode;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code) + "," + 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(CostValue) + "," + 
				SEnumChecker.GetStdName(RewardCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(CostType, "CostType") + "," + 
				SEnumChecker.GetMemberName(CostValue, "CostValue") + "," + 
				SEnumChecker.GetMemberName(RewardCode, "RewardCode");
		}
	}
	public class SShopDailyRewardServerMeta : SProto
	{
		public Double Probability = default(Double);
		public EResource RewardType = default(EResource);
		public TResource RewardValue = default(TResource);
		public SShopDailyRewardServerMeta()
		{
		}
		public SShopDailyRewardServerMeta(SShopDailyRewardServerMeta Obj_)
		{
			Probability = Obj_.Probability;
			RewardType = Obj_.RewardType;
			RewardValue = Obj_.RewardValue;
		}
		public SShopDailyRewardServerMeta(Double Probability_, EResource RewardType_, TResource RewardValue_)
		{
			Probability = Probability_;
			RewardType = RewardType_;
			RewardValue = RewardValue_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Probability);
			Stream_.Pop(ref RewardType);
			Stream_.Pop(ref RewardValue);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Probability", ref Probability);
			Value_.Pop("RewardType", ref RewardType);
			Value_.Pop("RewardValue", ref RewardValue);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Probability);
			Stream_.Push(RewardType);
			Stream_.Push(RewardValue);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Probability", Probability);
			Value_.Push("RewardType", RewardType);
			Value_.Push("RewardValue", RewardValue);
		}
		public void Set(SShopDailyRewardServerMeta Obj_)
		{
			Probability = Obj_.Probability;
			RewardType = Obj_.RewardType;
			RewardValue = Obj_.RewardValue;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Probability) + "," + 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(RewardValue);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Probability, "Probability") + "," + 
				SEnumChecker.GetMemberName(RewardType, "RewardType") + "," + 
				SEnumChecker.GetMemberName(RewardValue, "RewardValue");
		}
	}
	public class SShopCashServerMeta : SProto
	{
		public String Pid = string.Empty;
		public TResource DiaCount = default(TResource);
		public SShopCashServerMeta()
		{
		}
		public SShopCashServerMeta(SShopCashServerMeta Obj_)
		{
			Pid = Obj_.Pid;
			DiaCount = Obj_.DiaCount;
		}
		public SShopCashServerMeta(String Pid_, TResource DiaCount_)
		{
			Pid = Pid_;
			DiaCount = DiaCount_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Pid);
			Stream_.Pop(ref DiaCount);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Pid", ref Pid);
			Value_.Pop("DiaCount", ref DiaCount);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Pid);
			Stream_.Push(DiaCount);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Pid", Pid);
			Value_.Push("DiaCount", DiaCount);
		}
		public void Set(SShopCashServerMeta Obj_)
		{
			Pid = Obj_.Pid;
			DiaCount = Obj_.DiaCount;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Pid) + "," + 
				SEnumChecker.GetStdName(DiaCount);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Pid, "Pid") + "," + 
				SEnumChecker.GetMemberName(DiaCount, "DiaCount");
		}
	}
	public class SCharacterMeta : SProto
	{
		public Int32 Code = default(Int32);
		public EGrade Grade = default(EGrade);
		public EResource Cost_Type = default(EResource);
		public TResource Price = default(TResource);
		public TResource CPRefund = default(TResource);
		public Single MaxVelUp = default(Single);
		public Single MaxVelDown = default(Single);
		public Single MaxVelXGround = default(Single);
		public Single MaxVelXAir = default(Single);
		public Single FlapDeltaVelX = default(Single);
		public Single FlapDeltaVelUp = default(Single);
		public Single RunAcc = default(Single);
		public Single StaminaMax = default(Single);
		public Single StaminaPerTick = default(Single);
		public Int32 StaminaRegenDelay = default(Int32);
		public Single PumpSpeed = default(Single);
		public SCharacterMeta()
		{
		}
		public SCharacterMeta(SCharacterMeta Obj_)
		{
			Code = Obj_.Code;
			Grade = Obj_.Grade;
			Cost_Type = Obj_.Cost_Type;
			Price = Obj_.Price;
			CPRefund = Obj_.CPRefund;
			MaxVelUp = Obj_.MaxVelUp;
			MaxVelDown = Obj_.MaxVelDown;
			MaxVelXGround = Obj_.MaxVelXGround;
			MaxVelXAir = Obj_.MaxVelXAir;
			FlapDeltaVelX = Obj_.FlapDeltaVelX;
			FlapDeltaVelUp = Obj_.FlapDeltaVelUp;
			RunAcc = Obj_.RunAcc;
			StaminaMax = Obj_.StaminaMax;
			StaminaPerTick = Obj_.StaminaPerTick;
			StaminaRegenDelay = Obj_.StaminaRegenDelay;
			PumpSpeed = Obj_.PumpSpeed;
		}
		public SCharacterMeta(Int32 Code_, EGrade Grade_, EResource Cost_Type_, TResource Price_, TResource CPRefund_, Single MaxVelUp_, Single MaxVelDown_, Single MaxVelXGround_, Single MaxVelXAir_, Single FlapDeltaVelX_, Single FlapDeltaVelUp_, Single RunAcc_, Single StaminaMax_, Single StaminaPerTick_, Int32 StaminaRegenDelay_, Single PumpSpeed_)
		{
			Code = Code_;
			Grade = Grade_;
			Cost_Type = Cost_Type_;
			Price = Price_;
			CPRefund = CPRefund_;
			MaxVelUp = MaxVelUp_;
			MaxVelDown = MaxVelDown_;
			MaxVelXGround = MaxVelXGround_;
			MaxVelXAir = MaxVelXAir_;
			FlapDeltaVelX = FlapDeltaVelX_;
			FlapDeltaVelUp = FlapDeltaVelUp_;
			RunAcc = RunAcc_;
			StaminaMax = StaminaMax_;
			StaminaPerTick = StaminaPerTick_;
			StaminaRegenDelay = StaminaRegenDelay_;
			PumpSpeed = PumpSpeed_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
			Stream_.Pop(ref Grade);
			Stream_.Pop(ref Cost_Type);
			Stream_.Pop(ref Price);
			Stream_.Pop(ref CPRefund);
			Stream_.Pop(ref MaxVelUp);
			Stream_.Pop(ref MaxVelDown);
			Stream_.Pop(ref MaxVelXGround);
			Stream_.Pop(ref MaxVelXAir);
			Stream_.Pop(ref FlapDeltaVelX);
			Stream_.Pop(ref FlapDeltaVelUp);
			Stream_.Pop(ref RunAcc);
			Stream_.Pop(ref StaminaMax);
			Stream_.Pop(ref StaminaPerTick);
			Stream_.Pop(ref StaminaRegenDelay);
			Stream_.Pop(ref PumpSpeed);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
			Value_.Pop("Grade", ref Grade);
			Value_.Pop("Cost_Type", ref Cost_Type);
			Value_.Pop("Price", ref Price);
			Value_.Pop("CPRefund", ref CPRefund);
			Value_.Pop("MaxVelUp", ref MaxVelUp);
			Value_.Pop("MaxVelDown", ref MaxVelDown);
			Value_.Pop("MaxVelXGround", ref MaxVelXGround);
			Value_.Pop("MaxVelXAir", ref MaxVelXAir);
			Value_.Pop("FlapDeltaVelX", ref FlapDeltaVelX);
			Value_.Pop("FlapDeltaVelUp", ref FlapDeltaVelUp);
			Value_.Pop("RunAcc", ref RunAcc);
			Value_.Pop("StaminaMax", ref StaminaMax);
			Value_.Pop("StaminaPerTick", ref StaminaPerTick);
			Value_.Pop("StaminaRegenDelay", ref StaminaRegenDelay);
			Value_.Pop("PumpSpeed", ref PumpSpeed);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
			Stream_.Push(Grade);
			Stream_.Push(Cost_Type);
			Stream_.Push(Price);
			Stream_.Push(CPRefund);
			Stream_.Push(MaxVelUp);
			Stream_.Push(MaxVelDown);
			Stream_.Push(MaxVelXGround);
			Stream_.Push(MaxVelXAir);
			Stream_.Push(FlapDeltaVelX);
			Stream_.Push(FlapDeltaVelUp);
			Stream_.Push(RunAcc);
			Stream_.Push(StaminaMax);
			Stream_.Push(StaminaPerTick);
			Stream_.Push(StaminaRegenDelay);
			Stream_.Push(PumpSpeed);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
			Value_.Push("Grade", Grade);
			Value_.Push("Cost_Type", Cost_Type);
			Value_.Push("Price", Price);
			Value_.Push("CPRefund", CPRefund);
			Value_.Push("MaxVelUp", MaxVelUp);
			Value_.Push("MaxVelDown", MaxVelDown);
			Value_.Push("MaxVelXGround", MaxVelXGround);
			Value_.Push("MaxVelXAir", MaxVelXAir);
			Value_.Push("FlapDeltaVelX", FlapDeltaVelX);
			Value_.Push("FlapDeltaVelUp", FlapDeltaVelUp);
			Value_.Push("RunAcc", RunAcc);
			Value_.Push("StaminaMax", StaminaMax);
			Value_.Push("StaminaPerTick", StaminaPerTick);
			Value_.Push("StaminaRegenDelay", StaminaRegenDelay);
			Value_.Push("PumpSpeed", PumpSpeed);
		}
		public void Set(SCharacterMeta Obj_)
		{
			Code = Obj_.Code;
			Grade = Obj_.Grade;
			Cost_Type = Obj_.Cost_Type;
			Price = Obj_.Price;
			CPRefund = Obj_.CPRefund;
			MaxVelUp = Obj_.MaxVelUp;
			MaxVelDown = Obj_.MaxVelDown;
			MaxVelXGround = Obj_.MaxVelXGround;
			MaxVelXAir = Obj_.MaxVelXAir;
			FlapDeltaVelX = Obj_.FlapDeltaVelX;
			FlapDeltaVelUp = Obj_.FlapDeltaVelUp;
			RunAcc = Obj_.RunAcc;
			StaminaMax = Obj_.StaminaMax;
			StaminaPerTick = Obj_.StaminaPerTick;
			StaminaRegenDelay = Obj_.StaminaRegenDelay;
			PumpSpeed = Obj_.PumpSpeed;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code) + "," + 
				"bb.EGrade" + "," + 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(Price) + "," + 
				SEnumChecker.GetStdName(CPRefund) + "," + 
				SEnumChecker.GetStdName(MaxVelUp) + "," + 
				SEnumChecker.GetStdName(MaxVelDown) + "," + 
				SEnumChecker.GetStdName(MaxVelXGround) + "," + 
				SEnumChecker.GetStdName(MaxVelXAir) + "," + 
				SEnumChecker.GetStdName(FlapDeltaVelX) + "," + 
				SEnumChecker.GetStdName(FlapDeltaVelUp) + "," + 
				SEnumChecker.GetStdName(RunAcc) + "," + 
				SEnumChecker.GetStdName(StaminaMax) + "," + 
				SEnumChecker.GetStdName(StaminaPerTick) + "," + 
				SEnumChecker.GetStdName(StaminaRegenDelay) + "," + 
				SEnumChecker.GetStdName(PumpSpeed);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(Grade, "Grade") + "," + 
				SEnumChecker.GetMemberName(Cost_Type, "Cost_Type") + "," + 
				SEnumChecker.GetMemberName(Price, "Price") + "," + 
				SEnumChecker.GetMemberName(CPRefund, "CPRefund") + "," + 
				SEnumChecker.GetMemberName(MaxVelUp, "MaxVelUp") + "," + 
				SEnumChecker.GetMemberName(MaxVelDown, "MaxVelDown") + "," + 
				SEnumChecker.GetMemberName(MaxVelXGround, "MaxVelXGround") + "," + 
				SEnumChecker.GetMemberName(MaxVelXAir, "MaxVelXAir") + "," + 
				SEnumChecker.GetMemberName(FlapDeltaVelX, "FlapDeltaVelX") + "," + 
				SEnumChecker.GetMemberName(FlapDeltaVelUp, "FlapDeltaVelUp") + "," + 
				SEnumChecker.GetMemberName(RunAcc, "RunAcc") + "," + 
				SEnumChecker.GetMemberName(StaminaMax, "StaminaMax") + "," + 
				SEnumChecker.GetMemberName(StaminaPerTick, "StaminaPerTick") + "," + 
				SEnumChecker.GetMemberName(StaminaRegenDelay, "StaminaRegenDelay") + "," + 
				SEnumChecker.GetMemberName(PumpSpeed, "PumpSpeed");
		}
	}
	public class SCharacterServerMeta : SCharacterMeta
	{
		public Boolean Default = default(Boolean);
		public SCharacterServerMeta()
		{
		}
		public SCharacterServerMeta(SCharacterServerMeta Obj_) : base(Obj_)
		{
			Default = Obj_.Default;
		}
		public SCharacterServerMeta(SCharacterMeta Super_, Boolean Default_) : base(Super_)
		{
			Default = Default_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref Default);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("Default", ref Default);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(Default);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("Default", Default);
		}
		public void Set(SCharacterServerMeta Obj_)
		{
			base.Set(Obj_);
			Default = Obj_.Default;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(Default);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(Default, "Default");
		}
	}
	public class SCharacterGradeMeta : SProto
	{
		public EGrade Grade = default(EGrade);
		public SCharacterGradeMeta()
		{
		}
		public SCharacterGradeMeta(SCharacterGradeMeta Obj_)
		{
			Grade = Obj_.Grade;
		}
		public SCharacterGradeMeta(EGrade Grade_)
		{
			Grade = Grade_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Grade);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Grade", ref Grade);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Grade);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Grade", Grade);
		}
		public void Set(SCharacterGradeMeta Obj_)
		{
			Grade = Obj_.Grade;
		}
		public override string StdName()
		{
			return 
				"bb.EGrade";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Grade, "Grade");
		}
	}
	public class SPlayerPos : SProto
	{
		public TPoses Poses = new TPoses();
		public SPlayerPos()
		{
		}
		public SPlayerPos(SPlayerPos Obj_)
		{
			Poses = Obj_.Poses;
		}
		public SPlayerPos(TPoses Poses_)
		{
			Poses = Poses_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Poses);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Poses", ref Poses);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Poses);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Poses", Poses);
		}
		public void Set(SPlayerPos Obj_)
		{
			Poses = Obj_.Poses;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Poses);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Poses, "Poses");
		}
	}
	public class SMapMulti : SProto
	{
		public String PrefabName = string.Empty;
		public Dictionary<TTeamCnt,SPlayerPos> PlayerPoses = new Dictionary<TTeamCnt,SPlayerPos>();
		public SPoint PropPosition = new SPoint();
		public List<SStructure> Structures = new List<SStructure>();
		public List<SStructureMove> StructureMoves = new List<SStructureMove>();
		public SMapMulti()
		{
		}
		public SMapMulti(SMapMulti Obj_)
		{
			PrefabName = Obj_.PrefabName;
			PlayerPoses = Obj_.PlayerPoses;
			PropPosition = Obj_.PropPosition;
			Structures = Obj_.Structures;
			StructureMoves = Obj_.StructureMoves;
		}
		public SMapMulti(String PrefabName_, Dictionary<TTeamCnt,SPlayerPos> PlayerPoses_, SPoint PropPosition_, List<SStructure> Structures_, List<SStructureMove> StructureMoves_)
		{
			PrefabName = PrefabName_;
			PlayerPoses = PlayerPoses_;
			PropPosition = PropPosition_;
			Structures = Structures_;
			StructureMoves = StructureMoves_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref PrefabName);
			Stream_.Pop(ref PlayerPoses);
			Stream_.Pop(ref PropPosition);
			Stream_.Pop(ref Structures);
			Stream_.Pop(ref StructureMoves);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("PrefabName", ref PrefabName);
			Value_.Pop("PlayerPoses", ref PlayerPoses);
			Value_.Pop("PropPosition", ref PropPosition);
			Value_.Pop("Structures", ref Structures);
			Value_.Pop("StructureMoves", ref StructureMoves);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(PrefabName);
			Stream_.Push(PlayerPoses);
			Stream_.Push(PropPosition);
			Stream_.Push(Structures);
			Stream_.Push(StructureMoves);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("PrefabName", PrefabName);
			Value_.Push("PlayerPoses", PlayerPoses);
			Value_.Push("PropPosition", PropPosition);
			Value_.Push("Structures", Structures);
			Value_.Push("StructureMoves", StructureMoves);
		}
		public void Set(SMapMulti Obj_)
		{
			PrefabName = Obj_.PrefabName;
			PlayerPoses = Obj_.PlayerPoses;
			PropPosition.Set(Obj_.PropPosition);
			Structures = Obj_.Structures;
			StructureMoves = Obj_.StructureMoves;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(PrefabName) + "," + 
				SEnumChecker.GetStdName(PlayerPoses) + "," + 
				SEnumChecker.GetStdName(PropPosition) + "," + 
				SEnumChecker.GetStdName(Structures) + "," + 
				SEnumChecker.GetStdName(StructureMoves);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(PrefabName, "PrefabName") + "," + 
				SEnumChecker.GetMemberName(PlayerPoses, "PlayerPoses") + "," + 
				SEnumChecker.GetMemberName(PropPosition, "PropPosition") + "," + 
				SEnumChecker.GetMemberName(Structures, "Structures") + "," + 
				SEnumChecker.GetMemberName(StructureMoves, "StructureMoves");
		}
	}
	public class SMapMeta : SProto
	{
		public List<SMapMulti> MapOneOnOnes = new List<SMapMulti>();
		public List<SMapMulti> MapMulties = new List<SMapMulti>();
		public SMapMeta()
		{
		}
		public SMapMeta(SMapMeta Obj_)
		{
			MapOneOnOnes = Obj_.MapOneOnOnes;
			MapMulties = Obj_.MapMulties;
		}
		public SMapMeta(List<SMapMulti> MapOneOnOnes_, List<SMapMulti> MapMulties_)
		{
			MapOneOnOnes = MapOneOnOnes_;
			MapMulties = MapMulties_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref MapOneOnOnes);
			Stream_.Pop(ref MapMulties);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("MapOneOnOnes", ref MapOneOnOnes);
			Value_.Pop("MapMulties", ref MapMulties);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(MapOneOnOnes);
			Stream_.Push(MapMulties);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("MapOneOnOnes", MapOneOnOnes);
			Value_.Push("MapMulties", MapMulties);
		}
		public void Set(SMapMeta Obj_)
		{
			MapOneOnOnes = Obj_.MapOneOnOnes;
			MapMulties = Obj_.MapMulties;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(MapOneOnOnes) + "," + 
				SEnumChecker.GetStdName(MapMulties);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(MapOneOnOnes, "MapOneOnOnes") + "," + 
				SEnumChecker.GetMemberName(MapMulties, "MapMulties");
		}
	}
	public class SSingleMeta : SProto
	{
		public Int32 PlayCountMax = default(Int32);
		public TResource ChargeCostGold = default(TResource);
		public Int32 ScoreFactorWave = default(Int32);
		public Int32 ScoreFactorTime = default(Int32);
		public Int32 ScoreFactorGold = default(Int32);
		public Int32 RefreshDurationMinute = default(Int32);
		public SSingleMeta()
		{
		}
		public SSingleMeta(SSingleMeta Obj_)
		{
			PlayCountMax = Obj_.PlayCountMax;
			ChargeCostGold = Obj_.ChargeCostGold;
			ScoreFactorWave = Obj_.ScoreFactorWave;
			ScoreFactorTime = Obj_.ScoreFactorTime;
			ScoreFactorGold = Obj_.ScoreFactorGold;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
		}
		public SSingleMeta(Int32 PlayCountMax_, TResource ChargeCostGold_, Int32 ScoreFactorWave_, Int32 ScoreFactorTime_, Int32 ScoreFactorGold_, Int32 RefreshDurationMinute_)
		{
			PlayCountMax = PlayCountMax_;
			ChargeCostGold = ChargeCostGold_;
			ScoreFactorWave = ScoreFactorWave_;
			ScoreFactorTime = ScoreFactorTime_;
			ScoreFactorGold = ScoreFactorGold_;
			RefreshDurationMinute = RefreshDurationMinute_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref PlayCountMax);
			Stream_.Pop(ref ChargeCostGold);
			Stream_.Pop(ref ScoreFactorWave);
			Stream_.Pop(ref ScoreFactorTime);
			Stream_.Pop(ref ScoreFactorGold);
			Stream_.Pop(ref RefreshDurationMinute);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("PlayCountMax", ref PlayCountMax);
			Value_.Pop("ChargeCostGold", ref ChargeCostGold);
			Value_.Pop("ScoreFactorWave", ref ScoreFactorWave);
			Value_.Pop("ScoreFactorTime", ref ScoreFactorTime);
			Value_.Pop("ScoreFactorGold", ref ScoreFactorGold);
			Value_.Pop("RefreshDurationMinute", ref RefreshDurationMinute);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(PlayCountMax);
			Stream_.Push(ChargeCostGold);
			Stream_.Push(ScoreFactorWave);
			Stream_.Push(ScoreFactorTime);
			Stream_.Push(ScoreFactorGold);
			Stream_.Push(RefreshDurationMinute);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("PlayCountMax", PlayCountMax);
			Value_.Push("ChargeCostGold", ChargeCostGold);
			Value_.Push("ScoreFactorWave", ScoreFactorWave);
			Value_.Push("ScoreFactorTime", ScoreFactorTime);
			Value_.Push("ScoreFactorGold", ScoreFactorGold);
			Value_.Push("RefreshDurationMinute", RefreshDurationMinute);
		}
		public void Set(SSingleMeta Obj_)
		{
			PlayCountMax = Obj_.PlayCountMax;
			ChargeCostGold = Obj_.ChargeCostGold;
			ScoreFactorWave = Obj_.ScoreFactorWave;
			ScoreFactorTime = Obj_.ScoreFactorTime;
			ScoreFactorGold = Obj_.ScoreFactorGold;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(PlayCountMax) + "," + 
				SEnumChecker.GetStdName(ChargeCostGold) + "," + 
				SEnumChecker.GetStdName(ScoreFactorWave) + "," + 
				SEnumChecker.GetStdName(ScoreFactorTime) + "," + 
				SEnumChecker.GetStdName(ScoreFactorGold) + "," + 
				SEnumChecker.GetStdName(RefreshDurationMinute);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(PlayCountMax, "PlayCountMax") + "," + 
				SEnumChecker.GetMemberName(ChargeCostGold, "ChargeCostGold") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorWave, "ScoreFactorWave") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorTime, "ScoreFactorTime") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorGold, "ScoreFactorGold") + "," + 
				SEnumChecker.GetMemberName(RefreshDurationMinute, "RefreshDurationMinute");
		}
	}
	public class SIslandMeta : SProto
	{
		public Int32 PlayCountMax = default(Int32);
		public TResource ChargeCostGold = default(TResource);
		public Int32 ScoreFactorIsland = default(Int32);
		public Int32 ScoreFactorGold = default(Int32);
		public Int32 RefreshDurationMinute = default(Int32);
		public SIslandMeta()
		{
		}
		public SIslandMeta(SIslandMeta Obj_)
		{
			PlayCountMax = Obj_.PlayCountMax;
			ChargeCostGold = Obj_.ChargeCostGold;
			ScoreFactorIsland = Obj_.ScoreFactorIsland;
			ScoreFactorGold = Obj_.ScoreFactorGold;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
		}
		public SIslandMeta(Int32 PlayCountMax_, TResource ChargeCostGold_, Int32 ScoreFactorIsland_, Int32 ScoreFactorGold_, Int32 RefreshDurationMinute_)
		{
			PlayCountMax = PlayCountMax_;
			ChargeCostGold = ChargeCostGold_;
			ScoreFactorIsland = ScoreFactorIsland_;
			ScoreFactorGold = ScoreFactorGold_;
			RefreshDurationMinute = RefreshDurationMinute_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref PlayCountMax);
			Stream_.Pop(ref ChargeCostGold);
			Stream_.Pop(ref ScoreFactorIsland);
			Stream_.Pop(ref ScoreFactorGold);
			Stream_.Pop(ref RefreshDurationMinute);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("PlayCountMax", ref PlayCountMax);
			Value_.Pop("ChargeCostGold", ref ChargeCostGold);
			Value_.Pop("ScoreFactorIsland", ref ScoreFactorIsland);
			Value_.Pop("ScoreFactorGold", ref ScoreFactorGold);
			Value_.Pop("RefreshDurationMinute", ref RefreshDurationMinute);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(PlayCountMax);
			Stream_.Push(ChargeCostGold);
			Stream_.Push(ScoreFactorIsland);
			Stream_.Push(ScoreFactorGold);
			Stream_.Push(RefreshDurationMinute);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("PlayCountMax", PlayCountMax);
			Value_.Push("ChargeCostGold", ChargeCostGold);
			Value_.Push("ScoreFactorIsland", ScoreFactorIsland);
			Value_.Push("ScoreFactorGold", ScoreFactorGold);
			Value_.Push("RefreshDurationMinute", RefreshDurationMinute);
		}
		public void Set(SIslandMeta Obj_)
		{
			PlayCountMax = Obj_.PlayCountMax;
			ChargeCostGold = Obj_.ChargeCostGold;
			ScoreFactorIsland = Obj_.ScoreFactorIsland;
			ScoreFactorGold = Obj_.ScoreFactorGold;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(PlayCountMax) + "," + 
				SEnumChecker.GetStdName(ChargeCostGold) + "," + 
				SEnumChecker.GetStdName(ScoreFactorIsland) + "," + 
				SEnumChecker.GetStdName(ScoreFactorGold) + "," + 
				SEnumChecker.GetStdName(RefreshDurationMinute);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(PlayCountMax, "PlayCountMax") + "," + 
				SEnumChecker.GetMemberName(ChargeCostGold, "ChargeCostGold") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorIsland, "ScoreFactorIsland") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorGold, "ScoreFactorGold") + "," + 
				SEnumChecker.GetMemberName(RefreshDurationMinute, "RefreshDurationMinute");
		}
	}
	public class SGachaMeta : SProto
	{
		public EResource CostResource = default(EResource);
		public Int32 CostValue = default(Int32);
		public EResource TenCostResource = default(EResource);
		public Int32 TenCostValue = default(Int32);
		public Int32 RewardCode = default(Int32);
		public SGachaMeta()
		{
		}
		public SGachaMeta(SGachaMeta Obj_)
		{
			CostResource = Obj_.CostResource;
			CostValue = Obj_.CostValue;
			TenCostResource = Obj_.TenCostResource;
			TenCostValue = Obj_.TenCostValue;
			RewardCode = Obj_.RewardCode;
		}
		public SGachaMeta(EResource CostResource_, Int32 CostValue_, EResource TenCostResource_, Int32 TenCostValue_, Int32 RewardCode_)
		{
			CostResource = CostResource_;
			CostValue = CostValue_;
			TenCostResource = TenCostResource_;
			TenCostValue = TenCostValue_;
			RewardCode = RewardCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref CostResource);
			Stream_.Pop(ref CostValue);
			Stream_.Pop(ref TenCostResource);
			Stream_.Pop(ref TenCostValue);
			Stream_.Pop(ref RewardCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("CostResource", ref CostResource);
			Value_.Pop("CostValue", ref CostValue);
			Value_.Pop("TenCostResource", ref TenCostResource);
			Value_.Pop("TenCostValue", ref TenCostValue);
			Value_.Pop("RewardCode", ref RewardCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(CostResource);
			Stream_.Push(CostValue);
			Stream_.Push(TenCostResource);
			Stream_.Push(TenCostValue);
			Stream_.Push(RewardCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("CostResource", CostResource);
			Value_.Push("CostValue", CostValue);
			Value_.Push("TenCostResource", TenCostResource);
			Value_.Push("TenCostValue", TenCostValue);
			Value_.Push("RewardCode", RewardCode);
		}
		public void Set(SGachaMeta Obj_)
		{
			CostResource = Obj_.CostResource;
			CostValue = Obj_.CostValue;
			TenCostResource = Obj_.TenCostResource;
			TenCostValue = Obj_.TenCostValue;
			RewardCode = Obj_.RewardCode;
		}
		public override string StdName()
		{
			return 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(CostValue) + "," + 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(TenCostValue) + "," + 
				SEnumChecker.GetStdName(RewardCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(CostResource, "CostResource") + "," + 
				SEnumChecker.GetMemberName(CostValue, "CostValue") + "," + 
				SEnumChecker.GetMemberName(TenCostResource, "TenCostResource") + "," + 
				SEnumChecker.GetMemberName(TenCostValue, "TenCostValue") + "," + 
				SEnumChecker.GetMemberName(RewardCode, "RewardCode");
		}
	}
	public class SGachaGradeMeta : SProto
	{
		public EGrade Grade = default(EGrade);
		public Double Probability = default(Double);
		public SGachaGradeMeta()
		{
		}
		public SGachaGradeMeta(SGachaGradeMeta Obj_)
		{
			Grade = Obj_.Grade;
			Probability = Obj_.Probability;
		}
		public SGachaGradeMeta(EGrade Grade_, Double Probability_)
		{
			Grade = Grade_;
			Probability = Probability_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Grade);
			Stream_.Pop(ref Probability);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Grade", ref Grade);
			Value_.Pop("Probability", ref Probability);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Grade);
			Stream_.Push(Probability);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Grade", Grade);
			Value_.Push("Probability", Probability);
		}
		public void Set(SGachaGradeMeta Obj_)
		{
			Grade = Obj_.Grade;
			Probability = Obj_.Probability;
		}
		public override string StdName()
		{
			return 
				"bb.EGrade" + "," + 
				SEnumChecker.GetStdName(Probability);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Grade, "Grade") + "," + 
				SEnumChecker.GetMemberName(Probability, "Probability");
		}
	}
	public class SGachaRewardMeta : SProto
	{
		public Int32 Code = default(Int32);
		public Double Probability = default(Double);
		public Int32 CharCode = default(Int32);
		public SGachaRewardMeta()
		{
		}
		public SGachaRewardMeta(SGachaRewardMeta Obj_)
		{
			Code = Obj_.Code;
			Probability = Obj_.Probability;
			CharCode = Obj_.CharCode;
		}
		public SGachaRewardMeta(Int32 Code_, Double Probability_, Int32 CharCode_)
		{
			Code = Code_;
			Probability = Probability_;
			CharCode = CharCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
			Stream_.Pop(ref Probability);
			Stream_.Pop(ref CharCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
			Value_.Pop("Probability", ref Probability);
			Value_.Pop("CharCode", ref CharCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
			Stream_.Push(Probability);
			Stream_.Push(CharCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
			Value_.Push("Probability", Probability);
			Value_.Push("CharCode", CharCode);
		}
		public void Set(SGachaRewardMeta Obj_)
		{
			Code = Obj_.Code;
			Probability = Obj_.Probability;
			CharCode = Obj_.CharCode;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code) + "," + 
				SEnumChecker.GetStdName(Probability) + "," + 
				SEnumChecker.GetStdName(CharCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(Probability, "Probability") + "," + 
				SEnumChecker.GetMemberName(CharCode, "CharCode");
		}
	}
	public class SRewardMeta : SProto
	{
		public ERewardType Type = default(ERewardType);
		public Int32 Data = default(Int32);
		public SRewardMeta()
		{
		}
		public SRewardMeta(SRewardMeta Obj_)
		{
			Type = Obj_.Type;
			Data = Obj_.Data;
		}
		public SRewardMeta(ERewardType Type_, Int32 Data_)
		{
			Type = Type_;
			Data = Data_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Type);
			Stream_.Pop(ref Data);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Type", ref Type);
			Value_.Pop("Data", ref Data);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Type);
			Stream_.Push(Data);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Type", Type);
			Value_.Push("Data", Data);
		}
		public void Set(SRewardMeta Obj_)
		{
			Type = Obj_.Type;
			Data = Obj_.Data;
		}
		public override string StdName()
		{
			return 
				"bb.ERewardType" + "," + 
				SEnumChecker.GetStdName(Data);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Type, "Type") + "," + 
				SEnumChecker.GetMemberName(Data, "Data");
		}
	}
	public class SKeyRewardMeta : SProto
	{
		public Int32 Code = default(Int32);
		public SRewardMeta Reward = new SRewardMeta();
		public SKeyRewardMeta()
		{
		}
		public SKeyRewardMeta(SKeyRewardMeta Obj_)
		{
			Code = Obj_.Code;
			Reward = Obj_.Reward;
		}
		public SKeyRewardMeta(Int32 Code_, SRewardMeta Reward_)
		{
			Code = Code_;
			Reward = Reward_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
			Stream_.Pop(ref Reward);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
			Value_.Pop("Reward", ref Reward);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
			Stream_.Push(Reward);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
			Value_.Push("Reward", Reward);
		}
		public void Set(SKeyRewardMeta Obj_)
		{
			Code = Obj_.Code;
			Reward.Set(Obj_.Reward);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code) + "," + 
				SEnumChecker.GetStdName(Reward);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(Reward, "Reward");
		}
	}
	public class SRankMeta : SProto
	{
		public ERank Rank = default(ERank);
		public SRankMeta()
		{
		}
		public SRankMeta(SRankMeta Obj_)
		{
			Rank = Obj_.Rank;
		}
		public SRankMeta(ERank Rank_)
		{
			Rank = Rank_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Rank);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Rank", ref Rank);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Rank);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Rank", Rank);
		}
		public void Set(SRankMeta Obj_)
		{
			Rank = Obj_.Rank;
		}
		public override string StdName()
		{
			return 
				"bb.ERank";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Rank, "Rank");
		}
	}
	public class SRankTierMeta : SProto
	{
		public ERank Rank = default(ERank);
		public Int32 Tier = default(Int32);
		public Int32 MinPoint = default(Int32);
		public SRankTierMeta()
		{
		}
		public SRankTierMeta(SRankTierMeta Obj_)
		{
			Rank = Obj_.Rank;
			Tier = Obj_.Tier;
			MinPoint = Obj_.MinPoint;
		}
		public SRankTierMeta(ERank Rank_, Int32 Tier_, Int32 MinPoint_)
		{
			Rank = Rank_;
			Tier = Tier_;
			MinPoint = MinPoint_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Rank);
			Stream_.Pop(ref Tier);
			Stream_.Pop(ref MinPoint);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Rank", ref Rank);
			Value_.Pop("Tier", ref Tier);
			Value_.Pop("MinPoint", ref MinPoint);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Rank);
			Stream_.Push(Tier);
			Stream_.Push(MinPoint);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Rank", Rank);
			Value_.Push("Tier", Tier);
			Value_.Push("MinPoint", MinPoint);
		}
		public void Set(SRankTierMeta Obj_)
		{
			Rank = Obj_.Rank;
			Tier = Obj_.Tier;
			MinPoint = Obj_.MinPoint;
		}
		public override string StdName()
		{
			return 
				"bb.ERank" + "," + 
				SEnumChecker.GetStdName(Tier) + "," + 
				SEnumChecker.GetStdName(MinPoint);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Rank, "Rank") + "," + 
				SEnumChecker.GetMemberName(Tier, "Tier") + "," + 
				SEnumChecker.GetMemberName(MinPoint, "MinPoint");
		}
	}
	public class SRankRewardMeta : SProto
	{
		public Int32 Point = default(Int32);
		public Int32 RewardCode = default(Int32);
		public SRankRewardMeta()
		{
		}
		public SRankRewardMeta(SRankRewardMeta Obj_)
		{
			Point = Obj_.Point;
			RewardCode = Obj_.RewardCode;
		}
		public SRankRewardMeta(Int32 Point_, Int32 RewardCode_)
		{
			Point = Point_;
			RewardCode = RewardCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Point);
			Stream_.Pop(ref RewardCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Point", ref Point);
			Value_.Pop("RewardCode", ref RewardCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Point);
			Stream_.Push(RewardCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Point", Point);
			Value_.Push("RewardCode", RewardCode);
		}
		public void Set(SRankRewardMeta Obj_)
		{
			Point = Obj_.Point;
			RewardCode = Obj_.RewardCode;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Point) + "," + 
				SEnumChecker.GetStdName(RewardCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Point, "Point") + "," + 
				SEnumChecker.GetMemberName(RewardCode, "RewardCode");
		}
	}
	public class SQuestMeta : SProto
	{
		public EQuestType QuestType = default(EQuestType);
		public Int32 Code = default(Int32);
		public Int32 RequirmentCount = default(Int32);
		public String Param = string.Empty;
		public String Operator = string.Empty;
		public Int32 RewardCode = default(Int32);
		public SQuestMeta()
		{
		}
		public SQuestMeta(SQuestMeta Obj_)
		{
			QuestType = Obj_.QuestType;
			Code = Obj_.Code;
			RequirmentCount = Obj_.RequirmentCount;
			Param = Obj_.Param;
			Operator = Obj_.Operator;
			RewardCode = Obj_.RewardCode;
		}
		public SQuestMeta(EQuestType QuestType_, Int32 Code_, Int32 RequirmentCount_, String Param_, String Operator_, Int32 RewardCode_)
		{
			QuestType = QuestType_;
			Code = Code_;
			RequirmentCount = RequirmentCount_;
			Param = Param_;
			Operator = Operator_;
			RewardCode = RewardCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref QuestType);
			Stream_.Pop(ref Code);
			Stream_.Pop(ref RequirmentCount);
			Stream_.Pop(ref Param);
			Stream_.Pop(ref Operator);
			Stream_.Pop(ref RewardCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("QuestType", ref QuestType);
			Value_.Pop("Code", ref Code);
			Value_.Pop("RequirmentCount", ref RequirmentCount);
			Value_.Pop("Param", ref Param);
			Value_.Pop("Operator", ref Operator);
			Value_.Pop("RewardCode", ref RewardCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(QuestType);
			Stream_.Push(Code);
			Stream_.Push(RequirmentCount);
			Stream_.Push(Param);
			Stream_.Push(Operator);
			Stream_.Push(RewardCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("QuestType", QuestType);
			Value_.Push("Code", Code);
			Value_.Push("RequirmentCount", RequirmentCount);
			Value_.Push("Param", Param);
			Value_.Push("Operator", Operator);
			Value_.Push("RewardCode", RewardCode);
		}
		public void Set(SQuestMeta Obj_)
		{
			QuestType = Obj_.QuestType;
			Code = Obj_.Code;
			RequirmentCount = Obj_.RequirmentCount;
			Param = Obj_.Param;
			Operator = Obj_.Operator;
			RewardCode = Obj_.RewardCode;
		}
		public override string StdName()
		{
			return 
				"bb.EQuestType" + "," + 
				SEnumChecker.GetStdName(Code) + "," + 
				SEnumChecker.GetStdName(RequirmentCount) + "," + 
				SEnumChecker.GetStdName(Param) + "," + 
				SEnumChecker.GetStdName(Operator) + "," + 
				SEnumChecker.GetStdName(RewardCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(QuestType, "QuestType") + "," + 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(RequirmentCount, "RequirmentCount") + "," + 
				SEnumChecker.GetMemberName(Param, "Param") + "," + 
				SEnumChecker.GetMemberName(Operator, "Operator") + "," + 
				SEnumChecker.GetMemberName(RewardCode, "RewardCode");
		}
	}
	public class SQuestDailyCompleteMeta : SProto
	{
		public Int32 RequirmentCount = default(Int32);
		public Int32 RewardCode = default(Int32);
		public Int32 RefreshMinutes = default(Int32);
		public SQuestDailyCompleteMeta()
		{
		}
		public SQuestDailyCompleteMeta(SQuestDailyCompleteMeta Obj_)
		{
			RequirmentCount = Obj_.RequirmentCount;
			RewardCode = Obj_.RewardCode;
			RefreshMinutes = Obj_.RefreshMinutes;
		}
		public SQuestDailyCompleteMeta(Int32 RequirmentCount_, Int32 RewardCode_, Int32 RefreshMinutes_)
		{
			RequirmentCount = RequirmentCount_;
			RewardCode = RewardCode_;
			RefreshMinutes = RefreshMinutes_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref RequirmentCount);
			Stream_.Pop(ref RewardCode);
			Stream_.Pop(ref RefreshMinutes);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("RequirmentCount", ref RequirmentCount);
			Value_.Pop("RewardCode", ref RewardCode);
			Value_.Pop("RefreshMinutes", ref RefreshMinutes);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(RequirmentCount);
			Stream_.Push(RewardCode);
			Stream_.Push(RefreshMinutes);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("RequirmentCount", RequirmentCount);
			Value_.Push("RewardCode", RewardCode);
			Value_.Push("RefreshMinutes", RefreshMinutes);
		}
		public void Set(SQuestDailyCompleteMeta Obj_)
		{
			RequirmentCount = Obj_.RequirmentCount;
			RewardCode = Obj_.RewardCode;
			RefreshMinutes = Obj_.RefreshMinutes;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(RequirmentCount) + "," + 
				SEnumChecker.GetStdName(RewardCode) + "," + 
				SEnumChecker.GetStdName(RefreshMinutes);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(RequirmentCount, "RequirmentCount") + "," + 
				SEnumChecker.GetMemberName(RewardCode, "RewardCode") + "," + 
				SEnumChecker.GetMemberName(RefreshMinutes, "RefreshMinutes");
		}
	}
	public class SBattleRewardMeta : SProto
	{
		public EGameMode GameMode = default(EGameMode);
		public TTeamCnt TeamCount = default(TTeamCnt);
		public Int32 TeamMemberCount = default(Int32);
		public Int32 AddGold = default(Int32);
		public Int32 Unranked = default(Int32);
		public Int32 Bronze = default(Int32);
		public Int32 Silver = default(Int32);
		public Int32 Gold = default(Int32);
		public Int32 Diamond = default(Int32);
		public Int32 Champion = default(Int32);
		public SBattleRewardMeta()
		{
		}
		public SBattleRewardMeta(SBattleRewardMeta Obj_)
		{
			GameMode = Obj_.GameMode;
			TeamCount = Obj_.TeamCount;
			TeamMemberCount = Obj_.TeamMemberCount;
			AddGold = Obj_.AddGold;
			Unranked = Obj_.Unranked;
			Bronze = Obj_.Bronze;
			Silver = Obj_.Silver;
			Gold = Obj_.Gold;
			Diamond = Obj_.Diamond;
			Champion = Obj_.Champion;
		}
		public SBattleRewardMeta(EGameMode GameMode_, TTeamCnt TeamCount_, Int32 TeamMemberCount_, Int32 AddGold_, Int32 Unranked_, Int32 Bronze_, Int32 Silver_, Int32 Gold_, Int32 Diamond_, Int32 Champion_)
		{
			GameMode = GameMode_;
			TeamCount = TeamCount_;
			TeamMemberCount = TeamMemberCount_;
			AddGold = AddGold_;
			Unranked = Unranked_;
			Bronze = Bronze_;
			Silver = Silver_;
			Gold = Gold_;
			Diamond = Diamond_;
			Champion = Champion_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref GameMode);
			Stream_.Pop(ref TeamCount);
			Stream_.Pop(ref TeamMemberCount);
			Stream_.Pop(ref AddGold);
			Stream_.Pop(ref Unranked);
			Stream_.Pop(ref Bronze);
			Stream_.Pop(ref Silver);
			Stream_.Pop(ref Gold);
			Stream_.Pop(ref Diamond);
			Stream_.Pop(ref Champion);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("GameMode", ref GameMode);
			Value_.Pop("TeamCount", ref TeamCount);
			Value_.Pop("TeamMemberCount", ref TeamMemberCount);
			Value_.Pop("AddGold", ref AddGold);
			Value_.Pop("Unranked", ref Unranked);
			Value_.Pop("Bronze", ref Bronze);
			Value_.Pop("Silver", ref Silver);
			Value_.Pop("Gold", ref Gold);
			Value_.Pop("Diamond", ref Diamond);
			Value_.Pop("Champion", ref Champion);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(GameMode);
			Stream_.Push(TeamCount);
			Stream_.Push(TeamMemberCount);
			Stream_.Push(AddGold);
			Stream_.Push(Unranked);
			Stream_.Push(Bronze);
			Stream_.Push(Silver);
			Stream_.Push(Gold);
			Stream_.Push(Diamond);
			Stream_.Push(Champion);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("GameMode", GameMode);
			Value_.Push("TeamCount", TeamCount);
			Value_.Push("TeamMemberCount", TeamMemberCount);
			Value_.Push("AddGold", AddGold);
			Value_.Push("Unranked", Unranked);
			Value_.Push("Bronze", Bronze);
			Value_.Push("Silver", Silver);
			Value_.Push("Gold", Gold);
			Value_.Push("Diamond", Diamond);
			Value_.Push("Champion", Champion);
		}
		public void Set(SBattleRewardMeta Obj_)
		{
			GameMode = Obj_.GameMode;
			TeamCount = Obj_.TeamCount;
			TeamMemberCount = Obj_.TeamMemberCount;
			AddGold = Obj_.AddGold;
			Unranked = Obj_.Unranked;
			Bronze = Obj_.Bronze;
			Silver = Obj_.Silver;
			Gold = Obj_.Gold;
			Diamond = Obj_.Diamond;
			Champion = Obj_.Champion;
		}
		public override string StdName()
		{
			return 
				"bb.EGameMode" + "," + 
				SEnumChecker.GetStdName(TeamCount) + "," + 
				SEnumChecker.GetStdName(TeamMemberCount) + "," + 
				SEnumChecker.GetStdName(AddGold) + "," + 
				SEnumChecker.GetStdName(Unranked) + "," + 
				SEnumChecker.GetStdName(Bronze) + "," + 
				SEnumChecker.GetStdName(Silver) + "," + 
				SEnumChecker.GetStdName(Gold) + "," + 
				SEnumChecker.GetStdName(Diamond) + "," + 
				SEnumChecker.GetStdName(Champion);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(GameMode, "GameMode") + "," + 
				SEnumChecker.GetMemberName(TeamCount, "TeamCount") + "," + 
				SEnumChecker.GetMemberName(TeamMemberCount, "TeamMemberCount") + "," + 
				SEnumChecker.GetMemberName(AddGold, "AddGold") + "," + 
				SEnumChecker.GetMemberName(Unranked, "Unranked") + "," + 
				SEnumChecker.GetMemberName(Bronze, "Bronze") + "," + 
				SEnumChecker.GetMemberName(Silver, "Silver") + "," + 
				SEnumChecker.GetMemberName(Gold, "Gold") + "," + 
				SEnumChecker.GetMemberName(Diamond, "Diamond") + "," + 
				SEnumChecker.GetMemberName(Champion, "Champion");
		}
	}
	public class SCouponMeta : SProto
	{
		public Int32 Code = default(Int32);
		public Int32 StartYear = default(Int32);
		public Int32 StartMonth = default(Int32);
		public Int32 StartDay = default(Int32);
		public Int32 StartHour = default(Int32);
		public Int32 EndYear = default(Int32);
		public Int32 EndMonth = default(Int32);
		public Int32 EndDay = default(Int32);
		public Int32 EndHour = default(Int32);
		public Int32 RewardCode = default(Int32);
		public SCouponMeta()
		{
		}
		public SCouponMeta(SCouponMeta Obj_)
		{
			Code = Obj_.Code;
			StartYear = Obj_.StartYear;
			StartMonth = Obj_.StartMonth;
			StartDay = Obj_.StartDay;
			StartHour = Obj_.StartHour;
			EndYear = Obj_.EndYear;
			EndMonth = Obj_.EndMonth;
			EndDay = Obj_.EndDay;
			EndHour = Obj_.EndHour;
			RewardCode = Obj_.RewardCode;
		}
		public SCouponMeta(Int32 Code_, Int32 StartYear_, Int32 StartMonth_, Int32 StartDay_, Int32 StartHour_, Int32 EndYear_, Int32 EndMonth_, Int32 EndDay_, Int32 EndHour_, Int32 RewardCode_)
		{
			Code = Code_;
			StartYear = StartYear_;
			StartMonth = StartMonth_;
			StartDay = StartDay_;
			StartHour = StartHour_;
			EndYear = EndYear_;
			EndMonth = EndMonth_;
			EndDay = EndDay_;
			EndHour = EndHour_;
			RewardCode = RewardCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
			Stream_.Pop(ref StartYear);
			Stream_.Pop(ref StartMonth);
			Stream_.Pop(ref StartDay);
			Stream_.Pop(ref StartHour);
			Stream_.Pop(ref EndYear);
			Stream_.Pop(ref EndMonth);
			Stream_.Pop(ref EndDay);
			Stream_.Pop(ref EndHour);
			Stream_.Pop(ref RewardCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
			Value_.Pop("StartYear", ref StartYear);
			Value_.Pop("StartMonth", ref StartMonth);
			Value_.Pop("StartDay", ref StartDay);
			Value_.Pop("StartHour", ref StartHour);
			Value_.Pop("EndYear", ref EndYear);
			Value_.Pop("EndMonth", ref EndMonth);
			Value_.Pop("EndDay", ref EndDay);
			Value_.Pop("EndHour", ref EndHour);
			Value_.Pop("RewardCode", ref RewardCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
			Stream_.Push(StartYear);
			Stream_.Push(StartMonth);
			Stream_.Push(StartDay);
			Stream_.Push(StartHour);
			Stream_.Push(EndYear);
			Stream_.Push(EndMonth);
			Stream_.Push(EndDay);
			Stream_.Push(EndHour);
			Stream_.Push(RewardCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
			Value_.Push("StartYear", StartYear);
			Value_.Push("StartMonth", StartMonth);
			Value_.Push("StartDay", StartDay);
			Value_.Push("StartHour", StartHour);
			Value_.Push("EndYear", EndYear);
			Value_.Push("EndMonth", EndMonth);
			Value_.Push("EndDay", EndDay);
			Value_.Push("EndHour", EndHour);
			Value_.Push("RewardCode", RewardCode);
		}
		public void Set(SCouponMeta Obj_)
		{
			Code = Obj_.Code;
			StartYear = Obj_.StartYear;
			StartMonth = Obj_.StartMonth;
			StartDay = Obj_.StartDay;
			StartHour = Obj_.StartHour;
			EndYear = Obj_.EndYear;
			EndMonth = Obj_.EndMonth;
			EndDay = Obj_.EndDay;
			EndHour = Obj_.EndHour;
			RewardCode = Obj_.RewardCode;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code) + "," + 
				SEnumChecker.GetStdName(StartYear) + "," + 
				SEnumChecker.GetStdName(StartMonth) + "," + 
				SEnumChecker.GetStdName(StartDay) + "," + 
				SEnumChecker.GetStdName(StartHour) + "," + 
				SEnumChecker.GetStdName(EndYear) + "," + 
				SEnumChecker.GetStdName(EndMonth) + "," + 
				SEnumChecker.GetStdName(EndDay) + "," + 
				SEnumChecker.GetStdName(EndHour) + "," + 
				SEnumChecker.GetStdName(RewardCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(StartYear, "StartYear") + "," + 
				SEnumChecker.GetMemberName(StartMonth, "StartMonth") + "," + 
				SEnumChecker.GetMemberName(StartDay, "StartDay") + "," + 
				SEnumChecker.GetMemberName(StartHour, "StartHour") + "," + 
				SEnumChecker.GetMemberName(EndYear, "EndYear") + "," + 
				SEnumChecker.GetMemberName(EndMonth, "EndMonth") + "," + 
				SEnumChecker.GetMemberName(EndDay, "EndDay") + "," + 
				SEnumChecker.GetMemberName(EndHour, "EndHour") + "," + 
				SEnumChecker.GetMemberName(RewardCode, "RewardCode");
		}
	}
	public class SCouponKeyMeta : SProto
	{
		public String Key = string.Empty;
		public Int32 Code = default(Int32);
		public SCouponKeyMeta()
		{
		}
		public SCouponKeyMeta(SCouponKeyMeta Obj_)
		{
			Key = Obj_.Key;
			Code = Obj_.Code;
		}
		public SCouponKeyMeta(String Key_, Int32 Code_)
		{
			Key = Key_;
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Key);
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Key", ref Key);
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Key);
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Key", Key);
			Value_.Push("Code", Code);
		}
		public void Set(SCouponKeyMeta Obj_)
		{
			Key = Obj_.Key;
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Key) + "," + 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Key, "Key") + "," + 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SRankingConfigMeta : SProto
	{
		public Int32 PeriodMinutes = default(Int32);
		public String BaseTime = string.Empty;
		public SRankingConfigMeta()
		{
		}
		public SRankingConfigMeta(SRankingConfigMeta Obj_)
		{
			PeriodMinutes = Obj_.PeriodMinutes;
			BaseTime = Obj_.BaseTime;
		}
		public SRankingConfigMeta(Int32 PeriodMinutes_, String BaseTime_)
		{
			PeriodMinutes = PeriodMinutes_;
			BaseTime = BaseTime_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref PeriodMinutes);
			Stream_.Pop(ref BaseTime);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("PeriodMinutes", ref PeriodMinutes);
			Value_.Pop("BaseTime", ref BaseTime);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(PeriodMinutes);
			Stream_.Push(BaseTime);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("PeriodMinutes", PeriodMinutes);
			Value_.Push("BaseTime", BaseTime);
		}
		public void Set(SRankingConfigMeta Obj_)
		{
			PeriodMinutes = Obj_.PeriodMinutes;
			BaseTime = Obj_.BaseTime;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(PeriodMinutes) + "," + 
				SEnumChecker.GetStdName(BaseTime);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(PeriodMinutes, "PeriodMinutes") + "," + 
				SEnumChecker.GetMemberName(BaseTime, "BaseTime");
		}
	}
	public class SRankingRewardMeta : SProto
	{
		public String Mode = string.Empty;
		public Int32 End = default(Int32);
		public Int32 RewardCode = default(Int32);
		public SRankingRewardMeta()
		{
		}
		public SRankingRewardMeta(SRankingRewardMeta Obj_)
		{
			Mode = Obj_.Mode;
			End = Obj_.End;
			RewardCode = Obj_.RewardCode;
		}
		public SRankingRewardMeta(String Mode_, Int32 End_, Int32 RewardCode_)
		{
			Mode = Mode_;
			End = End_;
			RewardCode = RewardCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Mode);
			Stream_.Pop(ref End);
			Stream_.Pop(ref RewardCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Mode", ref Mode);
			Value_.Pop("End", ref End);
			Value_.Pop("RewardCode", ref RewardCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Mode);
			Stream_.Push(End);
			Stream_.Push(RewardCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Mode", Mode);
			Value_.Push("End", End);
			Value_.Push("RewardCode", RewardCode);
		}
		public void Set(SRankingRewardMeta Obj_)
		{
			Mode = Obj_.Mode;
			End = Obj_.End;
			RewardCode = Obj_.RewardCode;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Mode) + "," + 
				SEnumChecker.GetStdName(End) + "," + 
				SEnumChecker.GetStdName(RewardCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Mode, "Mode") + "," + 
				SEnumChecker.GetMemberName(End, "End") + "," + 
				SEnumChecker.GetMemberName(RewardCode, "RewardCode");
		}
	}
}
