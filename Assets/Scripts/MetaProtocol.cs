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
using TForbiddenWords = System.Collections.Generic.List<System.String>;
using TRankingUsers = System.Collections.Generic.List<bb.SRankingUser>;
using TRankings = System.Collections.Generic.Dictionary<System.Int64,System.Int32>;
using TResource = System.Int32;
using TDoneQuests = System.Collections.Generic.List<bb.SQuestSlotIndexCount>;
using TChars = System.Collections.Generic.HashSet<System.Int32>;
using TQuestDBs = System.Collections.Generic.Dictionary<System.Byte,bb.SQuestBase>;
using TQuestSlotIndexCodes = System.Collections.Generic.List<bb.SQuestSlotIndexCode>;
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
		public Int32 ChangeNickFreeCount = default(Int32);
		public EResource ChangeNickCostType = default(EResource);
		public TResource ChangeNickCostValue = default(TResource);
		public EResource TutorialRewardType = default(EResource);
		public TResource TutorialRewardValue = default(TResource);
		public TResource MaxTicket = default(TResource);
		public EResource BattleCostType = default(EResource);
		public TResource BattleCostValue = default(TResource);
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
			ChangeNickFreeCount = Obj_.ChangeNickFreeCount;
			ChangeNickCostType = Obj_.ChangeNickCostType;
			ChangeNickCostValue = Obj_.ChangeNickCostValue;
			TutorialRewardType = Obj_.TutorialRewardType;
			TutorialRewardValue = Obj_.TutorialRewardValue;
			MaxTicket = Obj_.MaxTicket;
			BattleCostType = Obj_.BattleCostType;
			BattleCostValue = Obj_.BattleCostValue;
		}
		public SConfigMeta(Int32 BattleDurationSec_, Int32 BattleOneOnOneDurationSec_, Int32 GhostDelaySec_, Int32 InvulnerableDurationSec_, Int32 BalloonHitPoint_, Int32 ParachuteHitPoint_, Int32 FirstBalloonHitPoint_, Int32 ChangeNickFreeCount_, EResource ChangeNickCostType_, TResource ChangeNickCostValue_, EResource TutorialRewardType_, TResource TutorialRewardValue_, TResource MaxTicket_, EResource BattleCostType_, TResource BattleCostValue_)
		{
			BattleDurationSec = BattleDurationSec_;
			BattleOneOnOneDurationSec = BattleOneOnOneDurationSec_;
			GhostDelaySec = GhostDelaySec_;
			InvulnerableDurationSec = InvulnerableDurationSec_;
			BalloonHitPoint = BalloonHitPoint_;
			ParachuteHitPoint = ParachuteHitPoint_;
			FirstBalloonHitPoint = FirstBalloonHitPoint_;
			ChangeNickFreeCount = ChangeNickFreeCount_;
			ChangeNickCostType = ChangeNickCostType_;
			ChangeNickCostValue = ChangeNickCostValue_;
			TutorialRewardType = TutorialRewardType_;
			TutorialRewardValue = TutorialRewardValue_;
			MaxTicket = MaxTicket_;
			BattleCostType = BattleCostType_;
			BattleCostValue = BattleCostValue_;
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
			Stream_.Pop(ref ChangeNickFreeCount);
			Stream_.Pop(ref ChangeNickCostType);
			Stream_.Pop(ref ChangeNickCostValue);
			Stream_.Pop(ref TutorialRewardType);
			Stream_.Pop(ref TutorialRewardValue);
			Stream_.Pop(ref MaxTicket);
			Stream_.Pop(ref BattleCostType);
			Stream_.Pop(ref BattleCostValue);
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
			Value_.Pop("ChangeNickFreeCount", ref ChangeNickFreeCount);
			Value_.Pop("ChangeNickCostType", ref ChangeNickCostType);
			Value_.Pop("ChangeNickCostValue", ref ChangeNickCostValue);
			Value_.Pop("TutorialRewardType", ref TutorialRewardType);
			Value_.Pop("TutorialRewardValue", ref TutorialRewardValue);
			Value_.Pop("MaxTicket", ref MaxTicket);
			Value_.Pop("BattleCostType", ref BattleCostType);
			Value_.Pop("BattleCostValue", ref BattleCostValue);
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
			Stream_.Push(ChangeNickFreeCount);
			Stream_.Push(ChangeNickCostType);
			Stream_.Push(ChangeNickCostValue);
			Stream_.Push(TutorialRewardType);
			Stream_.Push(TutorialRewardValue);
			Stream_.Push(MaxTicket);
			Stream_.Push(BattleCostType);
			Stream_.Push(BattleCostValue);
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
			Value_.Push("ChangeNickFreeCount", ChangeNickFreeCount);
			Value_.Push("ChangeNickCostType", ChangeNickCostType);
			Value_.Push("ChangeNickCostValue", ChangeNickCostValue);
			Value_.Push("TutorialRewardType", TutorialRewardType);
			Value_.Push("TutorialRewardValue", TutorialRewardValue);
			Value_.Push("MaxTicket", MaxTicket);
			Value_.Push("BattleCostType", BattleCostType);
			Value_.Push("BattleCostValue", BattleCostValue);
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
			ChangeNickFreeCount = Obj_.ChangeNickFreeCount;
			ChangeNickCostType = Obj_.ChangeNickCostType;
			ChangeNickCostValue = Obj_.ChangeNickCostValue;
			TutorialRewardType = Obj_.TutorialRewardType;
			TutorialRewardValue = Obj_.TutorialRewardValue;
			MaxTicket = Obj_.MaxTicket;
			BattleCostType = Obj_.BattleCostType;
			BattleCostValue = Obj_.BattleCostValue;
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
				SEnumChecker.GetStdName(ChangeNickFreeCount) + "," + 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(ChangeNickCostValue) + "," + 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(TutorialRewardValue) + "," + 
				SEnumChecker.GetStdName(MaxTicket) + "," + 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(BattleCostValue);
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
				SEnumChecker.GetMemberName(ChangeNickFreeCount, "ChangeNickFreeCount") + "," + 
				SEnumChecker.GetMemberName(ChangeNickCostType, "ChangeNickCostType") + "," + 
				SEnumChecker.GetMemberName(ChangeNickCostValue, "ChangeNickCostValue") + "," + 
				SEnumChecker.GetMemberName(TutorialRewardType, "TutorialRewardType") + "," + 
				SEnumChecker.GetMemberName(TutorialRewardValue, "TutorialRewardValue") + "," + 
				SEnumChecker.GetMemberName(MaxTicket, "MaxTicket") + "," + 
				SEnumChecker.GetMemberName(BattleCostType, "BattleCostType") + "," + 
				SEnumChecker.GetMemberName(BattleCostValue, "BattleCostValue");
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
	public class ExchangeValue : SProto
	{
		public EResource costResourceType = default(EResource);
		public Double rate = default(Double);
		public ExchangeValue()
		{
		}
		public ExchangeValue(ExchangeValue Obj_)
		{
			costResourceType = Obj_.costResourceType;
			rate = Obj_.rate;
		}
		public ExchangeValue(EResource costResourceType_, Double rate_)
		{
			costResourceType = costResourceType_;
			rate = rate_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref costResourceType);
			Stream_.Pop(ref rate);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("costResourceType", ref costResourceType);
			Value_.Pop("rate", ref rate);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(costResourceType);
			Stream_.Push(rate);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("costResourceType", costResourceType);
			Value_.Push("rate", rate);
		}
		public void Set(ExchangeValue Obj_)
		{
			costResourceType = Obj_.costResourceType;
			rate = Obj_.rate;
		}
		public override string StdName()
		{
			return 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(rate);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(costResourceType, "costResourceType") + "," + 
				SEnumChecker.GetMemberName(rate, "rate");
		}
	}
	public class ShopExchangeMeta : SProto
	{
		public EResource targetResourceType = default(EResource);
		public ExchangeValue exchangeValue = new ExchangeValue();
		public ShopExchangeMeta()
		{
		}
		public ShopExchangeMeta(ShopExchangeMeta Obj_)
		{
			targetResourceType = Obj_.targetResourceType;
			exchangeValue = Obj_.exchangeValue;
		}
		public ShopExchangeMeta(EResource targetResourceType_, ExchangeValue exchangeValue_)
		{
			targetResourceType = targetResourceType_;
			exchangeValue = exchangeValue_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref targetResourceType);
			Stream_.Pop(ref exchangeValue);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("targetResourceType", ref targetResourceType);
			Value_.Pop("exchangeValue", ref exchangeValue);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(targetResourceType);
			Stream_.Push(exchangeValue);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("targetResourceType", targetResourceType);
			Value_.Push("exchangeValue", exchangeValue);
		}
		public void Set(ShopExchangeMeta Obj_)
		{
			targetResourceType = Obj_.targetResourceType;
			exchangeValue.Set(Obj_.exchangeValue);
		}
		public override string StdName()
		{
			return 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(exchangeValue);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(targetResourceType, "targetResourceType") + "," + 
				SEnumChecker.GetMemberName(exchangeValue, "exchangeValue");
		}
	}
	public class CharacterTypeMeta : SProto
	{
		public EGrade grade = default(EGrade);
		public Int32 subGrade = default(Int32);
		public String howToGet = string.Empty;
		public TResource CostValue = default(TResource);
		public EResource RefundType = default(EResource);
		public TResource RefundValue = default(TResource);
		public Single MaxVelAir = default(Single);
		public Single MaxVelXGround = default(Single);
		public Single StaminaMax = default(Single);
		public Single PumpSpeed = default(Single);
		public Single Weight = default(Single);
		public CharacterTypeMeta()
		{
		}
		public CharacterTypeMeta(CharacterTypeMeta Obj_)
		{
			grade = Obj_.grade;
			subGrade = Obj_.subGrade;
			howToGet = Obj_.howToGet;
			CostValue = Obj_.CostValue;
			RefundType = Obj_.RefundType;
			RefundValue = Obj_.RefundValue;
			MaxVelAir = Obj_.MaxVelAir;
			MaxVelXGround = Obj_.MaxVelXGround;
			StaminaMax = Obj_.StaminaMax;
			PumpSpeed = Obj_.PumpSpeed;
			Weight = Obj_.Weight;
		}
		public CharacterTypeMeta(EGrade grade_, Int32 subGrade_, String howToGet_, TResource CostValue_, EResource RefundType_, TResource RefundValue_, Single MaxVelAir_, Single MaxVelXGround_, Single StaminaMax_, Single PumpSpeed_, Single Weight_)
		{
			grade = grade_;
			subGrade = subGrade_;
			howToGet = howToGet_;
			CostValue = CostValue_;
			RefundType = RefundType_;
			RefundValue = RefundValue_;
			MaxVelAir = MaxVelAir_;
			MaxVelXGround = MaxVelXGround_;
			StaminaMax = StaminaMax_;
			PumpSpeed = PumpSpeed_;
			Weight = Weight_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref grade);
			Stream_.Pop(ref subGrade);
			Stream_.Pop(ref howToGet);
			Stream_.Pop(ref CostValue);
			Stream_.Pop(ref RefundType);
			Stream_.Pop(ref RefundValue);
			Stream_.Pop(ref MaxVelAir);
			Stream_.Pop(ref MaxVelXGround);
			Stream_.Pop(ref StaminaMax);
			Stream_.Pop(ref PumpSpeed);
			Stream_.Pop(ref Weight);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("grade", ref grade);
			Value_.Pop("subGrade", ref subGrade);
			Value_.Pop("howToGet", ref howToGet);
			Value_.Pop("CostValue", ref CostValue);
			Value_.Pop("RefundType", ref RefundType);
			Value_.Pop("RefundValue", ref RefundValue);
			Value_.Pop("MaxVelAir", ref MaxVelAir);
			Value_.Pop("MaxVelXGround", ref MaxVelXGround);
			Value_.Pop("StaminaMax", ref StaminaMax);
			Value_.Pop("PumpSpeed", ref PumpSpeed);
			Value_.Pop("Weight", ref Weight);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(grade);
			Stream_.Push(subGrade);
			Stream_.Push(howToGet);
			Stream_.Push(CostValue);
			Stream_.Push(RefundType);
			Stream_.Push(RefundValue);
			Stream_.Push(MaxVelAir);
			Stream_.Push(MaxVelXGround);
			Stream_.Push(StaminaMax);
			Stream_.Push(PumpSpeed);
			Stream_.Push(Weight);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("grade", grade);
			Value_.Push("subGrade", subGrade);
			Value_.Push("howToGet", howToGet);
			Value_.Push("CostValue", CostValue);
			Value_.Push("RefundType", RefundType);
			Value_.Push("RefundValue", RefundValue);
			Value_.Push("MaxVelAir", MaxVelAir);
			Value_.Push("MaxVelXGround", MaxVelXGround);
			Value_.Push("StaminaMax", StaminaMax);
			Value_.Push("PumpSpeed", PumpSpeed);
			Value_.Push("Weight", Weight);
		}
		public void Set(CharacterTypeMeta Obj_)
		{
			grade = Obj_.grade;
			subGrade = Obj_.subGrade;
			howToGet = Obj_.howToGet;
			CostValue = Obj_.CostValue;
			RefundType = Obj_.RefundType;
			RefundValue = Obj_.RefundValue;
			MaxVelAir = Obj_.MaxVelAir;
			MaxVelXGround = Obj_.MaxVelXGround;
			StaminaMax = Obj_.StaminaMax;
			PumpSpeed = Obj_.PumpSpeed;
			Weight = Obj_.Weight;
		}
		public override string StdName()
		{
			return 
				"bb.EGrade" + "," + 
				SEnumChecker.GetStdName(subGrade) + "," + 
				SEnumChecker.GetStdName(howToGet) + "," + 
				SEnumChecker.GetStdName(CostValue) + "," + 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(RefundValue) + "," + 
				SEnumChecker.GetStdName(MaxVelAir) + "," + 
				SEnumChecker.GetStdName(MaxVelXGround) + "," + 
				SEnumChecker.GetStdName(StaminaMax) + "," + 
				SEnumChecker.GetStdName(PumpSpeed) + "," + 
				SEnumChecker.GetStdName(Weight);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(grade, "grade") + "," + 
				SEnumChecker.GetMemberName(subGrade, "subGrade") + "," + 
				SEnumChecker.GetMemberName(howToGet, "howToGet") + "," + 
				SEnumChecker.GetMemberName(CostValue, "CostValue") + "," + 
				SEnumChecker.GetMemberName(RefundType, "RefundType") + "," + 
				SEnumChecker.GetMemberName(RefundValue, "RefundValue") + "," + 
				SEnumChecker.GetMemberName(MaxVelAir, "MaxVelAir") + "," + 
				SEnumChecker.GetMemberName(MaxVelXGround, "MaxVelXGround") + "," + 
				SEnumChecker.GetMemberName(StaminaMax, "StaminaMax") + "," + 
				SEnumChecker.GetMemberName(PumpSpeed, "PumpSpeed") + "," + 
				SEnumChecker.GetMemberName(Weight, "Weight");
		}
	}
	public class CharacterTypeKeyValueMeta : SProto
	{
		public String type = string.Empty;
		public CharacterTypeMeta value = new CharacterTypeMeta();
		public CharacterTypeKeyValueMeta()
		{
		}
		public CharacterTypeKeyValueMeta(CharacterTypeKeyValueMeta Obj_)
		{
			type = Obj_.type;
			value = Obj_.value;
		}
		public CharacterTypeKeyValueMeta(String type_, CharacterTypeMeta value_)
		{
			type = type_;
			value = value_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref type);
			Stream_.Pop(ref value);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("type", ref type);
			Value_.Pop("value", ref value);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(type);
			Stream_.Push(value);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("type", type);
			Value_.Push("value", value);
		}
		public void Set(CharacterTypeKeyValueMeta Obj_)
		{
			type = Obj_.type;
			value.Set(Obj_.value);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(type) + "," + 
				SEnumChecker.GetStdName(value);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(type, "type") + "," + 
				SEnumChecker.GetMemberName(value, "value");
		}
	}
	public class CharacterMeta : SProto
	{
		public Int32 Code = default(Int32);
		public String type = string.Empty;
		public Boolean isDefault = default(Boolean);
		public CharacterMeta()
		{
		}
		public CharacterMeta(CharacterMeta Obj_)
		{
			Code = Obj_.Code;
			type = Obj_.type;
			isDefault = Obj_.isDefault;
		}
		public CharacterMeta(Int32 Code_, String type_, Boolean isDefault_)
		{
			Code = Code_;
			type = type_;
			isDefault = isDefault_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
			Stream_.Pop(ref type);
			Stream_.Pop(ref isDefault);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
			Value_.Pop("type", ref type);
			Value_.Pop("isDefault", ref isDefault);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
			Stream_.Push(type);
			Stream_.Push(isDefault);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
			Value_.Push("type", type);
			Value_.Push("isDefault", isDefault);
		}
		public void Set(CharacterMeta Obj_)
		{
			Code = Obj_.Code;
			type = Obj_.type;
			isDefault = Obj_.isDefault;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code) + "," + 
				SEnumChecker.GetStdName(type) + "," + 
				SEnumChecker.GetStdName(isDefault);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(type, "type") + "," + 
				SEnumChecker.GetMemberName(isDefault, "isDefault");
		}
	}
	public class SMultiMap : SPoint
	{
		public Int32 index = default(Int32);
		public String PrefabName = string.Empty;
		public TPoses Poses = new TPoses();
		public List<SBoxCollider2D> Structures = new List<SBoxCollider2D>();
		public List<SStructureMove> StructureMoves = new List<SStructureMove>();
		public SMultiMap()
		{
		}
		public SMultiMap(SMultiMap Obj_) : base(Obj_)
		{
			index = Obj_.index;
			PrefabName = Obj_.PrefabName;
			Poses = Obj_.Poses;
			Structures = Obj_.Structures;
			StructureMoves = Obj_.StructureMoves;
		}
		public SMultiMap(SPoint Super_, Int32 index_, String PrefabName_, TPoses Poses_, List<SBoxCollider2D> Structures_, List<SStructureMove> StructureMoves_) : base(Super_)
		{
			index = index_;
			PrefabName = PrefabName_;
			Poses = Poses_;
			Structures = Structures_;
			StructureMoves = StructureMoves_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref index);
			Stream_.Pop(ref PrefabName);
			Stream_.Pop(ref Poses);
			Stream_.Pop(ref Structures);
			Stream_.Pop(ref StructureMoves);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("index", ref index);
			Value_.Pop("PrefabName", ref PrefabName);
			Value_.Pop("Poses", ref Poses);
			Value_.Pop("Structures", ref Structures);
			Value_.Pop("StructureMoves", ref StructureMoves);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(index);
			Stream_.Push(PrefabName);
			Stream_.Push(Poses);
			Stream_.Push(Structures);
			Stream_.Push(StructureMoves);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("index", index);
			Value_.Push("PrefabName", PrefabName);
			Value_.Push("Poses", Poses);
			Value_.Push("Structures", Structures);
			Value_.Push("StructureMoves", StructureMoves);
		}
		public void Set(SMultiMap Obj_)
		{
			base.Set(Obj_);
			index = Obj_.index;
			PrefabName = Obj_.PrefabName;
			Poses = Obj_.Poses;
			Structures = Obj_.Structures;
			StructureMoves = Obj_.StructureMoves;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(index) + "," + 
				SEnumChecker.GetStdName(PrefabName) + "," + 
				SEnumChecker.GetStdName(Poses) + "," + 
				SEnumChecker.GetStdName(Structures) + "," + 
				SEnumChecker.GetStdName(StructureMoves);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(index, "index") + "," + 
				SEnumChecker.GetMemberName(PrefabName, "PrefabName") + "," + 
				SEnumChecker.GetMemberName(Poses, "Poses") + "," + 
				SEnumChecker.GetMemberName(Structures, "Structures") + "," + 
				SEnumChecker.GetMemberName(StructureMoves, "StructureMoves");
		}
	}
	public class SPrefabNameCollider : SProto
	{
		public String PrefabName = string.Empty;
		public SRectCollider2D Collider = new SRectCollider2D();
		public SPrefabNameCollider()
		{
		}
		public SPrefabNameCollider(SPrefabNameCollider Obj_)
		{
			PrefabName = Obj_.PrefabName;
			Collider = Obj_.Collider;
		}
		public SPrefabNameCollider(String PrefabName_, SRectCollider2D Collider_)
		{
			PrefabName = PrefabName_;
			Collider = Collider_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref PrefabName);
			Stream_.Pop(ref Collider);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("PrefabName", ref PrefabName);
			Value_.Pop("Collider", ref Collider);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(PrefabName);
			Stream_.Push(Collider);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("PrefabName", PrefabName);
			Value_.Push("Collider", Collider);
		}
		public void Set(SPrefabNameCollider Obj_)
		{
			PrefabName = Obj_.PrefabName;
			Collider.Set(Obj_.Collider);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(PrefabName) + "," + 
				SEnumChecker.GetStdName(Collider);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(PrefabName, "PrefabName") + "," + 
				SEnumChecker.GetMemberName(Collider, "Collider");
		}
	}
	public class SArrowDodgeMap : SPoint
	{
		public String PrefabName = string.Empty;
		public List<SBoxCollider2D> Structures = new List<SBoxCollider2D>();
		public SArrowDodgeMap()
		{
		}
		public SArrowDodgeMap(SArrowDodgeMap Obj_) : base(Obj_)
		{
			PrefabName = Obj_.PrefabName;
			Structures = Obj_.Structures;
		}
		public SArrowDodgeMap(SPoint Super_, String PrefabName_, List<SBoxCollider2D> Structures_) : base(Super_)
		{
			PrefabName = PrefabName_;
			Structures = Structures_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref PrefabName);
			Stream_.Pop(ref Structures);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("PrefabName", ref PrefabName);
			Value_.Pop("Structures", ref Structures);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(PrefabName);
			Stream_.Push(Structures);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("PrefabName", PrefabName);
			Value_.Push("Structures", Structures);
		}
		public void Set(SArrowDodgeMap Obj_)
		{
			base.Set(Obj_);
			PrefabName = Obj_.PrefabName;
			Structures = Obj_.Structures;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(PrefabName) + "," + 
				SEnumChecker.GetStdName(Structures);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(PrefabName, "PrefabName") + "," + 
				SEnumChecker.GetMemberName(Structures, "Structures");
		}
	}
	public class SArrowDodgeMapInfo : SProto
	{
		public List<SArrowDodgeMap> Maps = new List<SArrowDodgeMap>();
		public SPrefabNameCollider Arrow = new SPrefabNameCollider();
		public SPrefabNameCollider Coin = new SPrefabNameCollider();
		public SPrefabNameCollider GoldBar = new SPrefabNameCollider();
		public SPrefabNameCollider Shield = new SPrefabNameCollider();
		public SPrefabNameCollider Stamina = new SPrefabNameCollider();
		public SArrowDodgeMapInfo()
		{
		}
		public SArrowDodgeMapInfo(SArrowDodgeMapInfo Obj_)
		{
			Maps = Obj_.Maps;
			Arrow = Obj_.Arrow;
			Coin = Obj_.Coin;
			GoldBar = Obj_.GoldBar;
			Shield = Obj_.Shield;
			Stamina = Obj_.Stamina;
		}
		public SArrowDodgeMapInfo(List<SArrowDodgeMap> Maps_, SPrefabNameCollider Arrow_, SPrefabNameCollider Coin_, SPrefabNameCollider GoldBar_, SPrefabNameCollider Shield_, SPrefabNameCollider Stamina_)
		{
			Maps = Maps_;
			Arrow = Arrow_;
			Coin = Coin_;
			GoldBar = GoldBar_;
			Shield = Shield_;
			Stamina = Stamina_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Maps);
			Stream_.Pop(ref Arrow);
			Stream_.Pop(ref Coin);
			Stream_.Pop(ref GoldBar);
			Stream_.Pop(ref Shield);
			Stream_.Pop(ref Stamina);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Maps", ref Maps);
			Value_.Pop("Arrow", ref Arrow);
			Value_.Pop("Coin", ref Coin);
			Value_.Pop("GoldBar", ref GoldBar);
			Value_.Pop("Shield", ref Shield);
			Value_.Pop("Stamina", ref Stamina);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Maps);
			Stream_.Push(Arrow);
			Stream_.Push(Coin);
			Stream_.Push(GoldBar);
			Stream_.Push(Shield);
			Stream_.Push(Stamina);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Maps", Maps);
			Value_.Push("Arrow", Arrow);
			Value_.Push("Coin", Coin);
			Value_.Push("GoldBar", GoldBar);
			Value_.Push("Shield", Shield);
			Value_.Push("Stamina", Stamina);
		}
		public void Set(SArrowDodgeMapInfo Obj_)
		{
			Maps = Obj_.Maps;
			Arrow.Set(Obj_.Arrow);
			Coin.Set(Obj_.Coin);
			GoldBar.Set(Obj_.GoldBar);
			Shield.Set(Obj_.Shield);
			Stamina.Set(Obj_.Stamina);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Maps) + "," + 
				SEnumChecker.GetStdName(Arrow) + "," + 
				SEnumChecker.GetStdName(Coin) + "," + 
				SEnumChecker.GetStdName(GoldBar) + "," + 
				SEnumChecker.GetStdName(Shield) + "," + 
				SEnumChecker.GetStdName(Stamina);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Maps, "Maps") + "," + 
				SEnumChecker.GetMemberName(Arrow, "Arrow") + "," + 
				SEnumChecker.GetMemberName(Coin, "Coin") + "," + 
				SEnumChecker.GetMemberName(GoldBar, "GoldBar") + "," + 
				SEnumChecker.GetMemberName(Shield, "Shield") + "," + 
				SEnumChecker.GetMemberName(Stamina, "Stamina");
		}
	}
	public class SFlyAwayMap : SPoint
	{
		public String PrefabName = string.Empty;
		public List<SBoxCollider2D> Structures = new List<SBoxCollider2D>();
		public List<SBoxCollider2D> deadZones = new List<SBoxCollider2D>();
		public SBoxCollider2D ocean = new SBoxCollider2D();
		public SFlyAwayMap()
		{
		}
		public SFlyAwayMap(SFlyAwayMap Obj_) : base(Obj_)
		{
			PrefabName = Obj_.PrefabName;
			Structures = Obj_.Structures;
			deadZones = Obj_.deadZones;
			ocean = Obj_.ocean;
		}
		public SFlyAwayMap(SPoint Super_, String PrefabName_, List<SBoxCollider2D> Structures_, List<SBoxCollider2D> deadZones_, SBoxCollider2D ocean_) : base(Super_)
		{
			PrefabName = PrefabName_;
			Structures = Structures_;
			deadZones = deadZones_;
			ocean = ocean_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref PrefabName);
			Stream_.Pop(ref Structures);
			Stream_.Pop(ref deadZones);
			Stream_.Pop(ref ocean);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("PrefabName", ref PrefabName);
			Value_.Pop("Structures", ref Structures);
			Value_.Pop("deadZones", ref deadZones);
			Value_.Pop("ocean", ref ocean);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(PrefabName);
			Stream_.Push(Structures);
			Stream_.Push(deadZones);
			Stream_.Push(ocean);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("PrefabName", PrefabName);
			Value_.Push("Structures", Structures);
			Value_.Push("deadZones", deadZones);
			Value_.Push("ocean", ocean);
		}
		public void Set(SFlyAwayMap Obj_)
		{
			base.Set(Obj_);
			PrefabName = Obj_.PrefabName;
			Structures = Obj_.Structures;
			deadZones = Obj_.deadZones;
			ocean.Set(Obj_.ocean);
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(PrefabName) + "," + 
				SEnumChecker.GetStdName(Structures) + "," + 
				SEnumChecker.GetStdName(deadZones) + "," + 
				SEnumChecker.GetStdName(ocean);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(PrefabName, "PrefabName") + "," + 
				SEnumChecker.GetMemberName(Structures, "Structures") + "," + 
				SEnumChecker.GetMemberName(deadZones, "deadZones") + "," + 
				SEnumChecker.GetMemberName(ocean, "ocean");
		}
	}
	public class SFlyAwayMapInfo : SProto
	{
		public List<SFlyAwayMap> Maps = new List<SFlyAwayMap>();
		public List<SPrefabNameCollider> Lands = new List<SPrefabNameCollider>();
		public SPrefabNameCollider Coin = new SPrefabNameCollider();
		public SPrefabNameCollider GoldBar = new SPrefabNameCollider();
		public SPrefabNameCollider Apple = new SPrefabNameCollider();
		public SPrefabNameCollider Meat = new SPrefabNameCollider();
		public SPrefabNameCollider Chicken = new SPrefabNameCollider();
		public SFlyAwayMapInfo()
		{
		}
		public SFlyAwayMapInfo(SFlyAwayMapInfo Obj_)
		{
			Maps = Obj_.Maps;
			Lands = Obj_.Lands;
			Coin = Obj_.Coin;
			GoldBar = Obj_.GoldBar;
			Apple = Obj_.Apple;
			Meat = Obj_.Meat;
			Chicken = Obj_.Chicken;
		}
		public SFlyAwayMapInfo(List<SFlyAwayMap> Maps_, List<SPrefabNameCollider> Lands_, SPrefabNameCollider Coin_, SPrefabNameCollider GoldBar_, SPrefabNameCollider Apple_, SPrefabNameCollider Meat_, SPrefabNameCollider Chicken_)
		{
			Maps = Maps_;
			Lands = Lands_;
			Coin = Coin_;
			GoldBar = GoldBar_;
			Apple = Apple_;
			Meat = Meat_;
			Chicken = Chicken_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Maps);
			Stream_.Pop(ref Lands);
			Stream_.Pop(ref Coin);
			Stream_.Pop(ref GoldBar);
			Stream_.Pop(ref Apple);
			Stream_.Pop(ref Meat);
			Stream_.Pop(ref Chicken);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Maps", ref Maps);
			Value_.Pop("Lands", ref Lands);
			Value_.Pop("Coin", ref Coin);
			Value_.Pop("GoldBar", ref GoldBar);
			Value_.Pop("Apple", ref Apple);
			Value_.Pop("Meat", ref Meat);
			Value_.Pop("Chicken", ref Chicken);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Maps);
			Stream_.Push(Lands);
			Stream_.Push(Coin);
			Stream_.Push(GoldBar);
			Stream_.Push(Apple);
			Stream_.Push(Meat);
			Stream_.Push(Chicken);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Maps", Maps);
			Value_.Push("Lands", Lands);
			Value_.Push("Coin", Coin);
			Value_.Push("GoldBar", GoldBar);
			Value_.Push("Apple", Apple);
			Value_.Push("Meat", Meat);
			Value_.Push("Chicken", Chicken);
		}
		public void Set(SFlyAwayMapInfo Obj_)
		{
			Maps = Obj_.Maps;
			Lands = Obj_.Lands;
			Coin.Set(Obj_.Coin);
			GoldBar.Set(Obj_.GoldBar);
			Apple.Set(Obj_.Apple);
			Meat.Set(Obj_.Meat);
			Chicken.Set(Obj_.Chicken);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Maps) + "," + 
				SEnumChecker.GetStdName(Lands) + "," + 
				SEnumChecker.GetStdName(Coin) + "," + 
				SEnumChecker.GetStdName(GoldBar) + "," + 
				SEnumChecker.GetStdName(Apple) + "," + 
				SEnumChecker.GetStdName(Meat) + "," + 
				SEnumChecker.GetStdName(Chicken);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Maps, "Maps") + "," + 
				SEnumChecker.GetMemberName(Lands, "Lands") + "," + 
				SEnumChecker.GetMemberName(Coin, "Coin") + "," + 
				SEnumChecker.GetMemberName(GoldBar, "GoldBar") + "," + 
				SEnumChecker.GetMemberName(Apple, "Apple") + "," + 
				SEnumChecker.GetMemberName(Meat, "Meat") + "," + 
				SEnumChecker.GetMemberName(Chicken, "Chicken");
		}
	}
	public class SMapMeta : SProto
	{
		public List<SMultiMap> OneOnOneMaps = new List<SMultiMap>();
		public SArrowDodgeMapInfo ArrowDodgeMapInfo = new SArrowDodgeMapInfo();
		public SFlyAwayMapInfo FlyAwayMapInfo = new SFlyAwayMapInfo();
		public SMapMeta()
		{
		}
		public SMapMeta(SMapMeta Obj_)
		{
			OneOnOneMaps = Obj_.OneOnOneMaps;
			ArrowDodgeMapInfo = Obj_.ArrowDodgeMapInfo;
			FlyAwayMapInfo = Obj_.FlyAwayMapInfo;
		}
		public SMapMeta(List<SMultiMap> OneOnOneMaps_, SArrowDodgeMapInfo ArrowDodgeMapInfo_, SFlyAwayMapInfo FlyAwayMapInfo_)
		{
			OneOnOneMaps = OneOnOneMaps_;
			ArrowDodgeMapInfo = ArrowDodgeMapInfo_;
			FlyAwayMapInfo = FlyAwayMapInfo_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref OneOnOneMaps);
			Stream_.Pop(ref ArrowDodgeMapInfo);
			Stream_.Pop(ref FlyAwayMapInfo);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("OneOnOneMaps", ref OneOnOneMaps);
			Value_.Pop("ArrowDodgeMapInfo", ref ArrowDodgeMapInfo);
			Value_.Pop("FlyAwayMapInfo", ref FlyAwayMapInfo);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(OneOnOneMaps);
			Stream_.Push(ArrowDodgeMapInfo);
			Stream_.Push(FlyAwayMapInfo);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("OneOnOneMaps", OneOnOneMaps);
			Value_.Push("ArrowDodgeMapInfo", ArrowDodgeMapInfo);
			Value_.Push("FlyAwayMapInfo", FlyAwayMapInfo);
		}
		public void Set(SMapMeta Obj_)
		{
			OneOnOneMaps = Obj_.OneOnOneMaps;
			ArrowDodgeMapInfo.Set(Obj_.ArrowDodgeMapInfo);
			FlyAwayMapInfo.Set(Obj_.FlyAwayMapInfo);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(OneOnOneMaps) + "," + 
				SEnumChecker.GetStdName(ArrowDodgeMapInfo) + "," + 
				SEnumChecker.GetStdName(FlyAwayMapInfo);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(OneOnOneMaps, "OneOnOneMaps") + "," + 
				SEnumChecker.GetMemberName(ArrowDodgeMapInfo, "ArrowDodgeMapInfo") + "," + 
				SEnumChecker.GetMemberName(FlyAwayMapInfo, "FlyAwayMapInfo");
		}
	}
	public class SRankTierMeta : SProto
	{
		public ERank Rank = default(ERank);
		public Int32 Tier = default(Int32);
		public Int32 MaxPoint = default(Int32);
		public SRankTierMeta()
		{
		}
		public SRankTierMeta(SRankTierMeta Obj_)
		{
			Rank = Obj_.Rank;
			Tier = Obj_.Tier;
			MaxPoint = Obj_.MaxPoint;
		}
		public SRankTierMeta(ERank Rank_, Int32 Tier_, Int32 MaxPoint_)
		{
			Rank = Rank_;
			Tier = Tier_;
			MaxPoint = MaxPoint_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Rank);
			Stream_.Pop(ref Tier);
			Stream_.Pop(ref MaxPoint);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Rank", ref Rank);
			Value_.Pop("Tier", ref Tier);
			Value_.Pop("MaxPoint", ref MaxPoint);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Rank);
			Stream_.Push(Tier);
			Stream_.Push(MaxPoint);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Rank", Rank);
			Value_.Push("Tier", Tier);
			Value_.Push("MaxPoint", MaxPoint);
		}
		public void Set(SRankTierMeta Obj_)
		{
			Rank = Obj_.Rank;
			Tier = Obj_.Tier;
			MaxPoint = Obj_.MaxPoint;
		}
		public override string StdName()
		{
			return 
				"bb.ERank" + "," + 
				SEnumChecker.GetStdName(Tier) + "," + 
				SEnumChecker.GetStdName(MaxPoint);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Rank, "Rank") + "," + 
				SEnumChecker.GetMemberName(Tier, "Tier") + "," + 
				SEnumChecker.GetMemberName(MaxPoint, "MaxPoint");
		}
	}
	public class SRankRewardMeta : SProto
	{
		public Int32 point = default(Int32);
		public String rewardType = string.Empty;
		public Int32 rewardValue = default(Int32);
		public SRankRewardMeta()
		{
		}
		public SRankRewardMeta(SRankRewardMeta Obj_)
		{
			point = Obj_.point;
			rewardType = Obj_.rewardType;
			rewardValue = Obj_.rewardValue;
		}
		public SRankRewardMeta(Int32 point_, String rewardType_, Int32 rewardValue_)
		{
			point = point_;
			rewardType = rewardType_;
			rewardValue = rewardValue_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref point);
			Stream_.Pop(ref rewardType);
			Stream_.Pop(ref rewardValue);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("point", ref point);
			Value_.Pop("rewardType", ref rewardType);
			Value_.Pop("rewardValue", ref rewardValue);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(point);
			Stream_.Push(rewardType);
			Stream_.Push(rewardValue);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("point", point);
			Value_.Push("rewardType", rewardType);
			Value_.Push("rewardValue", rewardValue);
		}
		public void Set(SRankRewardMeta Obj_)
		{
			point = Obj_.point;
			rewardType = Obj_.rewardType;
			rewardValue = Obj_.rewardValue;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(point) + "," + 
				SEnumChecker.GetStdName(rewardType) + "," + 
				SEnumChecker.GetStdName(rewardValue);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(point, "point") + "," + 
				SEnumChecker.GetMemberName(rewardType, "rewardType") + "," + 
				SEnumChecker.GetMemberName(rewardValue, "rewardValue");
		}
	}
	public class SQuestMeta : SProto
	{
		public EQuestType QuestType = default(EQuestType);
		public Int32 Code = default(Int32);
		public Int32 unitCompleteCount = default(Int32);
		public Int32 completeCount = default(Int32);
		public String rewardType = string.Empty;
		public Int32 rewardValue = default(Int32);
		public SQuestMeta()
		{
		}
		public SQuestMeta(SQuestMeta Obj_)
		{
			QuestType = Obj_.QuestType;
			Code = Obj_.Code;
			unitCompleteCount = Obj_.unitCompleteCount;
			completeCount = Obj_.completeCount;
			rewardType = Obj_.rewardType;
			rewardValue = Obj_.rewardValue;
		}
		public SQuestMeta(EQuestType QuestType_, Int32 Code_, Int32 unitCompleteCount_, Int32 completeCount_, String rewardType_, Int32 rewardValue_)
		{
			QuestType = QuestType_;
			Code = Code_;
			unitCompleteCount = unitCompleteCount_;
			completeCount = completeCount_;
			rewardType = rewardType_;
			rewardValue = rewardValue_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref QuestType);
			Stream_.Pop(ref Code);
			Stream_.Pop(ref unitCompleteCount);
			Stream_.Pop(ref completeCount);
			Stream_.Pop(ref rewardType);
			Stream_.Pop(ref rewardValue);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("QuestType", ref QuestType);
			Value_.Pop("Code", ref Code);
			Value_.Pop("unitCompleteCount", ref unitCompleteCount);
			Value_.Pop("completeCount", ref completeCount);
			Value_.Pop("rewardType", ref rewardType);
			Value_.Pop("rewardValue", ref rewardValue);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(QuestType);
			Stream_.Push(Code);
			Stream_.Push(unitCompleteCount);
			Stream_.Push(completeCount);
			Stream_.Push(rewardType);
			Stream_.Push(rewardValue);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("QuestType", QuestType);
			Value_.Push("Code", Code);
			Value_.Push("unitCompleteCount", unitCompleteCount);
			Value_.Push("completeCount", completeCount);
			Value_.Push("rewardType", rewardType);
			Value_.Push("rewardValue", rewardValue);
		}
		public void Set(SQuestMeta Obj_)
		{
			QuestType = Obj_.QuestType;
			Code = Obj_.Code;
			unitCompleteCount = Obj_.unitCompleteCount;
			completeCount = Obj_.completeCount;
			rewardType = Obj_.rewardType;
			rewardValue = Obj_.rewardValue;
		}
		public override string StdName()
		{
			return 
				"bb.EQuestType" + "," + 
				SEnumChecker.GetStdName(Code) + "," + 
				SEnumChecker.GetStdName(unitCompleteCount) + "," + 
				SEnumChecker.GetStdName(completeCount) + "," + 
				SEnumChecker.GetStdName(rewardType) + "," + 
				SEnumChecker.GetStdName(rewardValue);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(QuestType, "QuestType") + "," + 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(unitCompleteCount, "unitCompleteCount") + "," + 
				SEnumChecker.GetMemberName(completeCount, "completeCount") + "," + 
				SEnumChecker.GetMemberName(rewardType, "rewardType") + "," + 
				SEnumChecker.GetMemberName(rewardValue, "rewardValue");
		}
	}
	public class QuestConfigMeta : SProto
	{
		public Minutes coolMinutes = default(Minutes);
		public Int32 dailyRequirementCount = default(Int32);
		public String dailyRewardType = string.Empty;
		public Int32 dailyRewardValue = default(Int32);
		public Minutes dailyRefreshMinutes = default(Minutes);
		public QuestConfigMeta()
		{
		}
		public QuestConfigMeta(QuestConfigMeta Obj_)
		{
			coolMinutes = Obj_.coolMinutes;
			dailyRequirementCount = Obj_.dailyRequirementCount;
			dailyRewardType = Obj_.dailyRewardType;
			dailyRewardValue = Obj_.dailyRewardValue;
			dailyRefreshMinutes = Obj_.dailyRefreshMinutes;
		}
		public QuestConfigMeta(Minutes coolMinutes_, Int32 dailyRequirementCount_, String dailyRewardType_, Int32 dailyRewardValue_, Minutes dailyRefreshMinutes_)
		{
			coolMinutes = coolMinutes_;
			dailyRequirementCount = dailyRequirementCount_;
			dailyRewardType = dailyRewardType_;
			dailyRewardValue = dailyRewardValue_;
			dailyRefreshMinutes = dailyRefreshMinutes_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref coolMinutes);
			Stream_.Pop(ref dailyRequirementCount);
			Stream_.Pop(ref dailyRewardType);
			Stream_.Pop(ref dailyRewardValue);
			Stream_.Pop(ref dailyRefreshMinutes);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("coolMinutes", ref coolMinutes);
			Value_.Pop("dailyRequirementCount", ref dailyRequirementCount);
			Value_.Pop("dailyRewardType", ref dailyRewardType);
			Value_.Pop("dailyRewardValue", ref dailyRewardValue);
			Value_.Pop("dailyRefreshMinutes", ref dailyRefreshMinutes);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(coolMinutes);
			Stream_.Push(dailyRequirementCount);
			Stream_.Push(dailyRewardType);
			Stream_.Push(dailyRewardValue);
			Stream_.Push(dailyRefreshMinutes);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("coolMinutes", coolMinutes);
			Value_.Push("dailyRequirementCount", dailyRequirementCount);
			Value_.Push("dailyRewardType", dailyRewardType);
			Value_.Push("dailyRewardValue", dailyRewardValue);
			Value_.Push("dailyRefreshMinutes", dailyRefreshMinutes);
		}
		public void Set(QuestConfigMeta Obj_)
		{
			coolMinutes = Obj_.coolMinutes;
			dailyRequirementCount = Obj_.dailyRequirementCount;
			dailyRewardType = Obj_.dailyRewardType;
			dailyRewardValue = Obj_.dailyRewardValue;
			dailyRefreshMinutes = Obj_.dailyRefreshMinutes;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(coolMinutes) + "," + 
				SEnumChecker.GetStdName(dailyRequirementCount) + "," + 
				SEnumChecker.GetStdName(dailyRewardType) + "," + 
				SEnumChecker.GetStdName(dailyRewardValue) + "," + 
				SEnumChecker.GetStdName(dailyRefreshMinutes);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(coolMinutes, "coolMinutes") + "," + 
				SEnumChecker.GetMemberName(dailyRequirementCount, "dailyRequirementCount") + "," + 
				SEnumChecker.GetMemberName(dailyRewardType, "dailyRewardType") + "," + 
				SEnumChecker.GetMemberName(dailyRewardValue, "dailyRewardValue") + "," + 
				SEnumChecker.GetMemberName(dailyRefreshMinutes, "dailyRefreshMinutes");
		}
	}
	public class MultiBattleConfigMeta : SProto
	{
		public Int32 DisconnectableSeconds = default(Int32);
		public Int32 PunishMinutesForDisconnect = default(Int32);
		public TResource rewardDiaValue = default(TResource);
		public Int32 eloDiffPoint = default(Int32);
		public Double eloDiffWinRatio = default(Double);
		public Double eloKWeight = default(Double);
		public MultiBattleConfigMeta()
		{
		}
		public MultiBattleConfigMeta(MultiBattleConfigMeta Obj_)
		{
			DisconnectableSeconds = Obj_.DisconnectableSeconds;
			PunishMinutesForDisconnect = Obj_.PunishMinutesForDisconnect;
			rewardDiaValue = Obj_.rewardDiaValue;
			eloDiffPoint = Obj_.eloDiffPoint;
			eloDiffWinRatio = Obj_.eloDiffWinRatio;
			eloKWeight = Obj_.eloKWeight;
		}
		public MultiBattleConfigMeta(Int32 DisconnectableSeconds_, Int32 PunishMinutesForDisconnect_, TResource rewardDiaValue_, Int32 eloDiffPoint_, Double eloDiffWinRatio_, Double eloKWeight_)
		{
			DisconnectableSeconds = DisconnectableSeconds_;
			PunishMinutesForDisconnect = PunishMinutesForDisconnect_;
			rewardDiaValue = rewardDiaValue_;
			eloDiffPoint = eloDiffPoint_;
			eloDiffWinRatio = eloDiffWinRatio_;
			eloKWeight = eloKWeight_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref DisconnectableSeconds);
			Stream_.Pop(ref PunishMinutesForDisconnect);
			Stream_.Pop(ref rewardDiaValue);
			Stream_.Pop(ref eloDiffPoint);
			Stream_.Pop(ref eloDiffWinRatio);
			Stream_.Pop(ref eloKWeight);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("DisconnectableSeconds", ref DisconnectableSeconds);
			Value_.Pop("PunishMinutesForDisconnect", ref PunishMinutesForDisconnect);
			Value_.Pop("rewardDiaValue", ref rewardDiaValue);
			Value_.Pop("eloDiffPoint", ref eloDiffPoint);
			Value_.Pop("eloDiffWinRatio", ref eloDiffWinRatio);
			Value_.Pop("eloKWeight", ref eloKWeight);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(DisconnectableSeconds);
			Stream_.Push(PunishMinutesForDisconnect);
			Stream_.Push(rewardDiaValue);
			Stream_.Push(eloDiffPoint);
			Stream_.Push(eloDiffWinRatio);
			Stream_.Push(eloKWeight);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("DisconnectableSeconds", DisconnectableSeconds);
			Value_.Push("PunishMinutesForDisconnect", PunishMinutesForDisconnect);
			Value_.Push("rewardDiaValue", rewardDiaValue);
			Value_.Push("eloDiffPoint", eloDiffPoint);
			Value_.Push("eloDiffWinRatio", eloDiffWinRatio);
			Value_.Push("eloKWeight", eloKWeight);
		}
		public void Set(MultiBattleConfigMeta Obj_)
		{
			DisconnectableSeconds = Obj_.DisconnectableSeconds;
			PunishMinutesForDisconnect = Obj_.PunishMinutesForDisconnect;
			rewardDiaValue = Obj_.rewardDiaValue;
			eloDiffPoint = Obj_.eloDiffPoint;
			eloDiffWinRatio = Obj_.eloDiffWinRatio;
			eloKWeight = Obj_.eloKWeight;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(DisconnectableSeconds) + "," + 
				SEnumChecker.GetStdName(PunishMinutesForDisconnect) + "," + 
				SEnumChecker.GetStdName(rewardDiaValue) + "," + 
				SEnumChecker.GetStdName(eloDiffPoint) + "," + 
				SEnumChecker.GetStdName(eloDiffWinRatio) + "," + 
				SEnumChecker.GetStdName(eloKWeight);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(DisconnectableSeconds, "DisconnectableSeconds") + "," + 
				SEnumChecker.GetMemberName(PunishMinutesForDisconnect, "PunishMinutesForDisconnect") + "," + 
				SEnumChecker.GetMemberName(rewardDiaValue, "rewardDiaValue") + "," + 
				SEnumChecker.GetMemberName(eloDiffPoint, "eloDiffPoint") + "," + 
				SEnumChecker.GetMemberName(eloDiffWinRatio, "eloDiffWinRatio") + "," + 
				SEnumChecker.GetMemberName(eloKWeight, "eloKWeight");
		}
	}
	public class MultiBattleDiaRewardMeta : SProto
	{
		public EResource diaType = default(EResource);
		public Double ratio = default(Double);
		public MultiBattleDiaRewardMeta()
		{
		}
		public MultiBattleDiaRewardMeta(MultiBattleDiaRewardMeta Obj_)
		{
			diaType = Obj_.diaType;
			ratio = Obj_.ratio;
		}
		public MultiBattleDiaRewardMeta(EResource diaType_, Double ratio_)
		{
			diaType = diaType_;
			ratio = ratio_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref diaType);
			Stream_.Pop(ref ratio);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("diaType", ref diaType);
			Value_.Pop("ratio", ref ratio);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(diaType);
			Stream_.Push(ratio);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("diaType", diaType);
			Value_.Push("ratio", ratio);
		}
		public void Set(MultiBattleDiaRewardMeta Obj_)
		{
			diaType = Obj_.diaType;
			ratio = Obj_.ratio;
		}
		public override string StdName()
		{
			return 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(ratio);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(diaType, "diaType") + "," + 
				SEnumChecker.GetMemberName(ratio, "ratio");
		}
	}
	public class SMultiMatchDeniedDurationMeta : SProto
	{
		public Int32 DisconnectedCount = default(Int32);
		public Int32 DeniedSeconds = default(Int32);
		public SMultiMatchDeniedDurationMeta()
		{
		}
		public SMultiMatchDeniedDurationMeta(SMultiMatchDeniedDurationMeta Obj_)
		{
			DisconnectedCount = Obj_.DisconnectedCount;
			DeniedSeconds = Obj_.DeniedSeconds;
		}
		public SMultiMatchDeniedDurationMeta(Int32 DisconnectedCount_, Int32 DeniedSeconds_)
		{
			DisconnectedCount = DisconnectedCount_;
			DeniedSeconds = DeniedSeconds_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref DisconnectedCount);
			Stream_.Pop(ref DeniedSeconds);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("DisconnectedCount", ref DisconnectedCount);
			Value_.Pop("DeniedSeconds", ref DeniedSeconds);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(DisconnectedCount);
			Stream_.Push(DeniedSeconds);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("DisconnectedCount", DisconnectedCount);
			Value_.Push("DeniedSeconds", DeniedSeconds);
		}
		public void Set(SMultiMatchDeniedDurationMeta Obj_)
		{
			DisconnectedCount = Obj_.DisconnectedCount;
			DeniedSeconds = Obj_.DeniedSeconds;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(DisconnectedCount) + "," + 
				SEnumChecker.GetStdName(DeniedSeconds);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(DisconnectedCount, "DisconnectedCount") + "," + 
				SEnumChecker.GetMemberName(DeniedSeconds, "DeniedSeconds");
		}
	}
	public class ArrowDodgeConfigMeta : SProto
	{
		public Int32 ArrowDodgePoint = default(Int32);
		public Int32 ArrowGetPoint = default(Int32);
		public Int64 ItemDurationTick = default(Int64);
		public Int64 ItemRegenPeriodTick = default(Int64);
		public Int32 maxItemCount = default(Int32);
		public Int32 PlayCountMax = default(Int32);
		public TResource ChargeCostGold = default(TResource);
		public Int32 RefreshDurationMinute = default(Int32);
		public Int32 scorePerGold = default(Int32);
		public ArrowDodgeConfigMeta()
		{
		}
		public ArrowDodgeConfigMeta(ArrowDodgeConfigMeta Obj_)
		{
			ArrowDodgePoint = Obj_.ArrowDodgePoint;
			ArrowGetPoint = Obj_.ArrowGetPoint;
			ItemDurationTick = Obj_.ItemDurationTick;
			ItemRegenPeriodTick = Obj_.ItemRegenPeriodTick;
			maxItemCount = Obj_.maxItemCount;
			PlayCountMax = Obj_.PlayCountMax;
			ChargeCostGold = Obj_.ChargeCostGold;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
			scorePerGold = Obj_.scorePerGold;
		}
		public ArrowDodgeConfigMeta(Int32 ArrowDodgePoint_, Int32 ArrowGetPoint_, Int64 ItemDurationTick_, Int64 ItemRegenPeriodTick_, Int32 maxItemCount_, Int32 PlayCountMax_, TResource ChargeCostGold_, Int32 RefreshDurationMinute_, Int32 scorePerGold_)
		{
			ArrowDodgePoint = ArrowDodgePoint_;
			ArrowGetPoint = ArrowGetPoint_;
			ItemDurationTick = ItemDurationTick_;
			ItemRegenPeriodTick = ItemRegenPeriodTick_;
			maxItemCount = maxItemCount_;
			PlayCountMax = PlayCountMax_;
			ChargeCostGold = ChargeCostGold_;
			RefreshDurationMinute = RefreshDurationMinute_;
			scorePerGold = scorePerGold_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref ArrowDodgePoint);
			Stream_.Pop(ref ArrowGetPoint);
			Stream_.Pop(ref ItemDurationTick);
			Stream_.Pop(ref ItemRegenPeriodTick);
			Stream_.Pop(ref maxItemCount);
			Stream_.Pop(ref PlayCountMax);
			Stream_.Pop(ref ChargeCostGold);
			Stream_.Pop(ref RefreshDurationMinute);
			Stream_.Pop(ref scorePerGold);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("ArrowDodgePoint", ref ArrowDodgePoint);
			Value_.Pop("ArrowGetPoint", ref ArrowGetPoint);
			Value_.Pop("ItemDurationTick", ref ItemDurationTick);
			Value_.Pop("ItemRegenPeriodTick", ref ItemRegenPeriodTick);
			Value_.Pop("maxItemCount", ref maxItemCount);
			Value_.Pop("PlayCountMax", ref PlayCountMax);
			Value_.Pop("ChargeCostGold", ref ChargeCostGold);
			Value_.Pop("RefreshDurationMinute", ref RefreshDurationMinute);
			Value_.Pop("scorePerGold", ref scorePerGold);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(ArrowDodgePoint);
			Stream_.Push(ArrowGetPoint);
			Stream_.Push(ItemDurationTick);
			Stream_.Push(ItemRegenPeriodTick);
			Stream_.Push(maxItemCount);
			Stream_.Push(PlayCountMax);
			Stream_.Push(ChargeCostGold);
			Stream_.Push(RefreshDurationMinute);
			Stream_.Push(scorePerGold);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("ArrowDodgePoint", ArrowDodgePoint);
			Value_.Push("ArrowGetPoint", ArrowGetPoint);
			Value_.Push("ItemDurationTick", ItemDurationTick);
			Value_.Push("ItemRegenPeriodTick", ItemRegenPeriodTick);
			Value_.Push("maxItemCount", maxItemCount);
			Value_.Push("PlayCountMax", PlayCountMax);
			Value_.Push("ChargeCostGold", ChargeCostGold);
			Value_.Push("RefreshDurationMinute", RefreshDurationMinute);
			Value_.Push("scorePerGold", scorePerGold);
		}
		public void Set(ArrowDodgeConfigMeta Obj_)
		{
			ArrowDodgePoint = Obj_.ArrowDodgePoint;
			ArrowGetPoint = Obj_.ArrowGetPoint;
			ItemDurationTick = Obj_.ItemDurationTick;
			ItemRegenPeriodTick = Obj_.ItemRegenPeriodTick;
			maxItemCount = Obj_.maxItemCount;
			PlayCountMax = Obj_.PlayCountMax;
			ChargeCostGold = Obj_.ChargeCostGold;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
			scorePerGold = Obj_.scorePerGold;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(ArrowDodgePoint) + "," + 
				SEnumChecker.GetStdName(ArrowGetPoint) + "," + 
				SEnumChecker.GetStdName(ItemDurationTick) + "," + 
				SEnumChecker.GetStdName(ItemRegenPeriodTick) + "," + 
				SEnumChecker.GetStdName(maxItemCount) + "," + 
				SEnumChecker.GetStdName(PlayCountMax) + "," + 
				SEnumChecker.GetStdName(ChargeCostGold) + "," + 
				SEnumChecker.GetStdName(RefreshDurationMinute) + "," + 
				SEnumChecker.GetStdName(scorePerGold);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(ArrowDodgePoint, "ArrowDodgePoint") + "," + 
				SEnumChecker.GetMemberName(ArrowGetPoint, "ArrowGetPoint") + "," + 
				SEnumChecker.GetMemberName(ItemDurationTick, "ItemDurationTick") + "," + 
				SEnumChecker.GetMemberName(ItemRegenPeriodTick, "ItemRegenPeriodTick") + "," + 
				SEnumChecker.GetMemberName(maxItemCount, "maxItemCount") + "," + 
				SEnumChecker.GetMemberName(PlayCountMax, "PlayCountMax") + "," + 
				SEnumChecker.GetMemberName(ChargeCostGold, "ChargeCostGold") + "," + 
				SEnumChecker.GetMemberName(RefreshDurationMinute, "RefreshDurationMinute") + "," + 
				SEnumChecker.GetMemberName(scorePerGold, "scorePerGold");
		}
	}
	public class SArrowDodgeItemMeta : SProto
	{
		public EArrowDodgeItemType ItemType = default(EArrowDodgeItemType);
		public UInt32 CreateWeight = default(UInt32);
		public TResource AddedGold = default(TResource);
		public SArrowDodgeItemMeta()
		{
		}
		public SArrowDodgeItemMeta(SArrowDodgeItemMeta Obj_)
		{
			ItemType = Obj_.ItemType;
			CreateWeight = Obj_.CreateWeight;
			AddedGold = Obj_.AddedGold;
		}
		public SArrowDodgeItemMeta(EArrowDodgeItemType ItemType_, UInt32 CreateWeight_, TResource AddedGold_)
		{
			ItemType = ItemType_;
			CreateWeight = CreateWeight_;
			AddedGold = AddedGold_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref ItemType);
			Stream_.Pop(ref CreateWeight);
			Stream_.Pop(ref AddedGold);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("ItemType", ref ItemType);
			Value_.Pop("CreateWeight", ref CreateWeight);
			Value_.Pop("AddedGold", ref AddedGold);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(ItemType);
			Stream_.Push(CreateWeight);
			Stream_.Push(AddedGold);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("ItemType", ItemType);
			Value_.Push("CreateWeight", CreateWeight);
			Value_.Push("AddedGold", AddedGold);
		}
		public void Set(SArrowDodgeItemMeta Obj_)
		{
			ItemType = Obj_.ItemType;
			CreateWeight = Obj_.CreateWeight;
			AddedGold = Obj_.AddedGold;
		}
		public override string StdName()
		{
			return 
				"bb.EArrowDodgeItemType" + "," + 
				SEnumChecker.GetStdName(CreateWeight) + "," + 
				SEnumChecker.GetStdName(AddedGold);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(ItemType, "ItemType") + "," + 
				SEnumChecker.GetMemberName(CreateWeight, "CreateWeight") + "," + 
				SEnumChecker.GetMemberName(AddedGold, "AddedGold");
		}
	}
	public class FlyAwayConfigMeta : SProto
	{
		public Int32 maxComboMultiplier = default(Int32);
		public Single landingAddedStamina = default(Single);
		public Int32 PlayCountMax = default(Int32);
		public TResource ChargeCostGold = default(TResource);
		public Int32 RefreshDurationMinute = default(Int32);
		public Int32 scorePerGold = default(Int32);
		public FlyAwayConfigMeta()
		{
		}
		public FlyAwayConfigMeta(FlyAwayConfigMeta Obj_)
		{
			maxComboMultiplier = Obj_.maxComboMultiplier;
			landingAddedStamina = Obj_.landingAddedStamina;
			PlayCountMax = Obj_.PlayCountMax;
			ChargeCostGold = Obj_.ChargeCostGold;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
			scorePerGold = Obj_.scorePerGold;
		}
		public FlyAwayConfigMeta(Int32 maxComboMultiplier_, Single landingAddedStamina_, Int32 PlayCountMax_, TResource ChargeCostGold_, Int32 RefreshDurationMinute_, Int32 scorePerGold_)
		{
			maxComboMultiplier = maxComboMultiplier_;
			landingAddedStamina = landingAddedStamina_;
			PlayCountMax = PlayCountMax_;
			ChargeCostGold = ChargeCostGold_;
			RefreshDurationMinute = RefreshDurationMinute_;
			scorePerGold = scorePerGold_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref maxComboMultiplier);
			Stream_.Pop(ref landingAddedStamina);
			Stream_.Pop(ref PlayCountMax);
			Stream_.Pop(ref ChargeCostGold);
			Stream_.Pop(ref RefreshDurationMinute);
			Stream_.Pop(ref scorePerGold);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("maxComboMultiplier", ref maxComboMultiplier);
			Value_.Pop("landingAddedStamina", ref landingAddedStamina);
			Value_.Pop("PlayCountMax", ref PlayCountMax);
			Value_.Pop("ChargeCostGold", ref ChargeCostGold);
			Value_.Pop("RefreshDurationMinute", ref RefreshDurationMinute);
			Value_.Pop("scorePerGold", ref scorePerGold);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(maxComboMultiplier);
			Stream_.Push(landingAddedStamina);
			Stream_.Push(PlayCountMax);
			Stream_.Push(ChargeCostGold);
			Stream_.Push(RefreshDurationMinute);
			Stream_.Push(scorePerGold);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("maxComboMultiplier", maxComboMultiplier);
			Value_.Push("landingAddedStamina", landingAddedStamina);
			Value_.Push("PlayCountMax", PlayCountMax);
			Value_.Push("ChargeCostGold", ChargeCostGold);
			Value_.Push("RefreshDurationMinute", RefreshDurationMinute);
			Value_.Push("scorePerGold", scorePerGold);
		}
		public void Set(FlyAwayConfigMeta Obj_)
		{
			maxComboMultiplier = Obj_.maxComboMultiplier;
			landingAddedStamina = Obj_.landingAddedStamina;
			PlayCountMax = Obj_.PlayCountMax;
			ChargeCostGold = Obj_.ChargeCostGold;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
			scorePerGold = Obj_.scorePerGold;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(maxComboMultiplier) + "," + 
				SEnumChecker.GetStdName(landingAddedStamina) + "," + 
				SEnumChecker.GetStdName(PlayCountMax) + "," + 
				SEnumChecker.GetStdName(ChargeCostGold) + "," + 
				SEnumChecker.GetStdName(RefreshDurationMinute) + "," + 
				SEnumChecker.GetStdName(scorePerGold);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(maxComboMultiplier, "maxComboMultiplier") + "," + 
				SEnumChecker.GetMemberName(landingAddedStamina, "landingAddedStamina") + "," + 
				SEnumChecker.GetMemberName(PlayCountMax, "PlayCountMax") + "," + 
				SEnumChecker.GetMemberName(ChargeCostGold, "ChargeCostGold") + "," + 
				SEnumChecker.GetMemberName(RefreshDurationMinute, "RefreshDurationMinute") + "," + 
				SEnumChecker.GetMemberName(scorePerGold, "scorePerGold");
		}
	}
	public class SFlyAwayItemMeta : SProto
	{
		public EFlyAwayItemType ItemType = default(EFlyAwayItemType);
		public UInt32 StaminaCreateWeight = default(UInt32);
		public TResource AddedGold = default(TResource);
		public Single AddedStamina = default(Single);
		public SFlyAwayItemMeta()
		{
		}
		public SFlyAwayItemMeta(SFlyAwayItemMeta Obj_)
		{
			ItemType = Obj_.ItemType;
			StaminaCreateWeight = Obj_.StaminaCreateWeight;
			AddedGold = Obj_.AddedGold;
			AddedStamina = Obj_.AddedStamina;
		}
		public SFlyAwayItemMeta(EFlyAwayItemType ItemType_, UInt32 StaminaCreateWeight_, TResource AddedGold_, Single AddedStamina_)
		{
			ItemType = ItemType_;
			StaminaCreateWeight = StaminaCreateWeight_;
			AddedGold = AddedGold_;
			AddedStamina = AddedStamina_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref ItemType);
			Stream_.Pop(ref StaminaCreateWeight);
			Stream_.Pop(ref AddedGold);
			Stream_.Pop(ref AddedStamina);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("ItemType", ref ItemType);
			Value_.Pop("StaminaCreateWeight", ref StaminaCreateWeight);
			Value_.Pop("AddedGold", ref AddedGold);
			Value_.Pop("AddedStamina", ref AddedStamina);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(ItemType);
			Stream_.Push(StaminaCreateWeight);
			Stream_.Push(AddedGold);
			Stream_.Push(AddedStamina);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("ItemType", ItemType);
			Value_.Push("StaminaCreateWeight", StaminaCreateWeight);
			Value_.Push("AddedGold", AddedGold);
			Value_.Push("AddedStamina", AddedStamina);
		}
		public void Set(SFlyAwayItemMeta Obj_)
		{
			ItemType = Obj_.ItemType;
			StaminaCreateWeight = Obj_.StaminaCreateWeight;
			AddedGold = Obj_.AddedGold;
			AddedStamina = Obj_.AddedStamina;
		}
		public override string StdName()
		{
			return 
				"bb.EFlyAwayItemType" + "," + 
				SEnumChecker.GetStdName(StaminaCreateWeight) + "," + 
				SEnumChecker.GetStdName(AddedGold) + "," + 
				SEnumChecker.GetStdName(AddedStamina);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(ItemType, "ItemType") + "," + 
				SEnumChecker.GetMemberName(StaminaCreateWeight, "StaminaCreateWeight") + "," + 
				SEnumChecker.GetMemberName(AddedGold, "AddedGold") + "," + 
				SEnumChecker.GetMemberName(AddedStamina, "AddedStamina");
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
		public String rewardType = string.Empty;
		public Int32 rewardValue = default(Int32);
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
			rewardType = Obj_.rewardType;
			rewardValue = Obj_.rewardValue;
		}
		public SCouponMeta(Int32 Code_, Int32 StartYear_, Int32 StartMonth_, Int32 StartDay_, Int32 StartHour_, Int32 EndYear_, Int32 EndMonth_, Int32 EndDay_, Int32 EndHour_, String rewardType_, Int32 rewardValue_)
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
			rewardType = rewardType_;
			rewardValue = rewardValue_;
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
			Stream_.Pop(ref rewardType);
			Stream_.Pop(ref rewardValue);
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
			Value_.Pop("rewardType", ref rewardType);
			Value_.Pop("rewardValue", ref rewardValue);
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
			Stream_.Push(rewardType);
			Stream_.Push(rewardValue);
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
			Value_.Push("rewardType", rewardType);
			Value_.Push("rewardValue", rewardValue);
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
			rewardType = Obj_.rewardType;
			rewardValue = Obj_.rewardValue;
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
				SEnumChecker.GetStdName(rewardType) + "," + 
				SEnumChecker.GetStdName(rewardValue);
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
				SEnumChecker.GetMemberName(rewardType, "rewardType") + "," + 
				SEnumChecker.GetMemberName(rewardValue, "rewardValue");
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
		public String rewardType = string.Empty;
		public Int32 rewardValue = default(Int32);
		public SRankingRewardMeta()
		{
		}
		public SRankingRewardMeta(SRankingRewardMeta Obj_)
		{
			Mode = Obj_.Mode;
			End = Obj_.End;
			rewardType = Obj_.rewardType;
			rewardValue = Obj_.rewardValue;
		}
		public SRankingRewardMeta(String Mode_, Int32 End_, String rewardType_, Int32 rewardValue_)
		{
			Mode = Mode_;
			End = End_;
			rewardType = rewardType_;
			rewardValue = rewardValue_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Mode);
			Stream_.Pop(ref End);
			Stream_.Pop(ref rewardType);
			Stream_.Pop(ref rewardValue);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Mode", ref Mode);
			Value_.Pop("End", ref End);
			Value_.Pop("rewardType", ref rewardType);
			Value_.Pop("rewardValue", ref rewardValue);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Mode);
			Stream_.Push(End);
			Stream_.Push(rewardType);
			Stream_.Push(rewardValue);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Mode", Mode);
			Value_.Push("End", End);
			Value_.Push("rewardType", rewardType);
			Value_.Push("rewardValue", rewardValue);
		}
		public void Set(SRankingRewardMeta Obj_)
		{
			Mode = Obj_.Mode;
			End = Obj_.End;
			rewardType = Obj_.rewardType;
			rewardValue = Obj_.rewardValue;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Mode) + "," + 
				SEnumChecker.GetStdName(End) + "," + 
				SEnumChecker.GetStdName(rewardType) + "," + 
				SEnumChecker.GetStdName(rewardValue);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Mode, "Mode") + "," + 
				SEnumChecker.GetMemberName(End, "End") + "," + 
				SEnumChecker.GetMemberName(rewardType, "rewardType") + "," + 
				SEnumChecker.GetMemberName(rewardValue, "rewardValue");
		}
	}
}
