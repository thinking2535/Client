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
using System;
using System.Collections.Generic;
using rso.core;


namespace bb
{
	using rso.net;
	using rso.game;
	using rso.physics;
	using rso.gameutil;
	public enum EProtoNetCs
	{
		Chat,
		FCMTokenSet,
		FCMTokenUnset,
		FCMCanPushAtNight,
		ChangeLanguage,
		Buy,
		BuyChar,
		BuyPackage,
		Purchase,
		DailyReward,
		SelectChar,
		SingleStart,
		SingleEnd,
		IslandStart,
		IslandEnd,
		BattleJoin,
		BattleOut,
		BattleTouch,
		BattlePush,
		BattleIcon,
		SingleBattleScore,
		SingleBattleIcon,
		SingleBattleItem,
		RoomList,
		RoomCreate,
		RoomJoin,
		RoomOut,
		RoomReady,
		RoomChat,
		RoomNoti,
		Gacha,
		GachaX10,
		RankReward,
		QuestReward,
		QuestNext,
		QuestDailyCompleteReward,
		ChangeNick,
		CouponUse,
		TutorialReward,
		RankingRewardInfo,
		RankingReward,
		Max,
		Null,
	}
	public enum EProtoNetSc
	{
		Ret,
		Msg,
		Login,
		Lobby,
		Chat,
		UserExp,
		Resources,
		SetPoint,
		SetChar,
		UnsetChar,
		Buy,
		BuyChar,
		BuyPackage,
		Purchase,
		DailyReward,
		DailyRewardFail,
		SingleStart,
		SingleEnd,
		IslandStart,
		IslandEnd,
		BattleJoin,
		BattleOut,
		BattleBegin,
		BattleMatching,
		BattleStart,
		BattleEnd,
		BattleSync,
		BattleTouch,
		BattlePush,
		BattleIcon,
		BattleLink,
		BattleUnLink,
		SingleBattleStart,
		SingleBattleScore,
		SingleBattleIcon,
		SingleBattleItem,
		SingleBattleEnd,
		RoomList,
		RoomChange,
		RoomCreate,
		RoomJoin,
		RoomJoinFailed,
		RoomOut,
		RoomOutFailed,
		RoomReady,
		RoomChat,
		RoomNoti,
		Gacha,
		GachaX10,
		GachaFailed,
		QuestGot,
		QuestDone,
		QuestReward,
		QuestNext,
		QuestDailyCompleteReward,
		ChangeNick,
		ChangeNickFail,
		CouponUse,
		CouponUseFail,
		RankingRewardInfo,
		RankingReward,
		RankingRewardFail,
		Max,
	}
	public enum ERet
	{
		Ok,
		InvalidTime,
		UserDoesNotExist,
		SameCanNotPushAtNight,
		SameCode,
		InvalidProtocol,
		InvalidLanguage,
		InvalidShopID,
		InvalidGoodsID,
		InvalidCharCode,
		ReceiptCheckFail,
		SPError,
		NotBattleJoining,
		AlreadyBattleJoining,
		AlreadyInBattle,
		AlreadyHave,
		InvalidBattleType,
		MatchInsertFail,
		BattleBeginFail,
		NotEnoughMoney,
		NoMoreNewCharacter,
		NickLengthUnderMin,
		NickLengthOverMax,
		CouponAlreadyUsed,
		CouponInvalid,
		RankingServerOffLine,
		RankingNoReward,
		RankingRewarded,
		RankingRewardFail,
		Max,
		Null,
	}
	public enum ELanguage : Byte
	{
		English,
		Korean,
		France,
		Germany,
		Spain,
		Italia,
		ChinaCH,
		ChinaTW,
		Japan,
		Portugal,
		Russia,
		Nederland,
		Turkey,
		Finland,
		Malaysia,
		Thailand,
		Indonesia,
		Vietnam,
		India,
		Max,
	}
	public enum EGrade : Byte
	{
		Normal,
		Rare,
		Epic,
		Unique,
		Max,
	}
	public enum EGameMode
	{
		Single,
		Solo,
		Survival,
		Team,
		SurvivalSmall,
		TeamSmall,
		IslandSolo,
		DodgeSolo,
		Max,
		Null,
	}
	public enum ERoomState : Byte
	{
		RoomWait,
		RoomAllReady,
		RoomPlay,
		RoomEndWait,
		RoomEmpty,
	}
	public enum ERankingType
	{
		Multi,
		Single,
		Island,
		Max,
		Null,
	}
	public class SUserLoginOption : SProto
	{
		public EOS OS = default(EOS);
		public SUserLoginOption()
		{
		}
		public SUserLoginOption(SUserLoginOption Obj_)
		{
			OS = Obj_.OS;
		}
		public SUserLoginOption(EOS OS_)
		{
			OS = OS_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref OS);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("OS", ref OS);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(OS);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("OS", OS);
		}
		public void Set(SUserLoginOption Obj_)
		{
			OS = Obj_.OS;
		}
		public override string StdName()
		{
			return 
				"rso.gameutil.EOS";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(OS, "OS");
		}
	}
	public class SUserCreateOption : SProto
	{
		public SUserLoginOption LoginOption = new SUserLoginOption();
		public ELanguage Language = default(ELanguage);
		public SUserCreateOption()
		{
		}
		public SUserCreateOption(SUserCreateOption Obj_)
		{
			LoginOption = Obj_.LoginOption;
			Language = Obj_.Language;
		}
		public SUserCreateOption(SUserLoginOption LoginOption_, ELanguage Language_)
		{
			LoginOption = LoginOption_;
			Language = Language_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref LoginOption);
			Stream_.Pop(ref Language);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("LoginOption", ref LoginOption);
			Value_.Pop("Language", ref Language);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(LoginOption);
			Stream_.Push(Language);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("LoginOption", LoginOption);
			Value_.Push("Language", Language);
		}
		public void Set(SUserCreateOption Obj_)
		{
			LoginOption.Set(Obj_.LoginOption);
			Language = Obj_.Language;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(LoginOption) + "," + 
				"bb.ELanguage";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(LoginOption, "LoginOption") + "," + 
				SEnumChecker.GetMemberName(Language, "Language");
		}
	}
	public enum EResource
	{
		Ticket,
		Gold,
		Dia,
		CP,
		DiaPaid,
		Max,
		Null,
	}
	public enum ERewardType : Byte
	{
		Resource_Ticket,
		Resource_Gold,
		Resource_Dia,
		Resource_CP,
		Character,
		Max,
	}
	public enum EQuestType
	{
		TeamPlay,
		TeamVictory,
		SurvivalPlay,
		SurvivalVictory,
		IngameConsecutiveKill,
		IngameBalloonPopping,
		IngameKill,
		IngameBestPlayer,
		GachaRuby,
		BlowBalloon,
		PlayNormal,
		PlayRare,
		PlayEpic,
		PlaySingle,
		SingleTime,
		SinglePlayGoldGet,
		SinglePlayScoreGet,
		PlayIsland,
		IslandCount,
		IslandGoldGet,
		IslandScoreGet,
		SurvivalPlayRanking,
		RankPointGet,
		SoloPlay,
		SoloVictory,
		Survival3PPlay,
		Survival3PVictory,
		MultiSinglePlay,
		MultiIslandPlay,
		Max,
		Null=-1,
	}
	public class SQuestSlotIndexCount : SProto
	{
		public TQuestSlotIndex SlotIndex = default(TQuestSlotIndex);
		public Int32 Count = default(Int32);
		public SQuestSlotIndexCount()
		{
		}
		public SQuestSlotIndexCount(SQuestSlotIndexCount Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
			Count = Obj_.Count;
		}
		public SQuestSlotIndexCount(TQuestSlotIndex SlotIndex_, Int32 Count_)
		{
			SlotIndex = SlotIndex_;
			Count = Count_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref SlotIndex);
			Stream_.Pop(ref Count);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("SlotIndex", ref SlotIndex);
			Value_.Pop("Count", ref Count);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(SlotIndex);
			Stream_.Push(Count);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("SlotIndex", SlotIndex);
			Value_.Push("Count", Count);
		}
		public void Set(SQuestSlotIndexCount Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
			Count = Obj_.Count;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(SlotIndex) + "," + 
				SEnumChecker.GetStdName(Count);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(SlotIndex, "SlotIndex") + "," + 
				SEnumChecker.GetMemberName(Count, "Count");
		}
	}
	public class SRetNetSc : SProto
	{
		public ERet Ret = default(ERet);
		public SRetNetSc()
		{
		}
		public SRetNetSc(SRetNetSc Obj_)
		{
			Ret = Obj_.Ret;
		}
		public SRetNetSc(ERet Ret_)
		{
			Ret = Ret_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Ret);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Ret", ref Ret);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Ret);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Ret", Ret);
		}
		public void Set(SRetNetSc Obj_)
		{
			Ret = Obj_.Ret;
		}
		public override string StdName()
		{
			return 
				"bb.ERet";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Ret, "Ret");
		}
	}
	public class SMsgNetSc : SProto
	{
		public String Msg = string.Empty;
		public SMsgNetSc()
		{
		}
		public SMsgNetSc(SMsgNetSc Obj_)
		{
			Msg = Obj_.Msg;
		}
		public SMsgNetSc(String Msg_)
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
		public void Set(SMsgNetSc Obj_)
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
	public class SResourceTypeData : SProto
	{
		public EResource Type = default(EResource);
		public TResource Data = default(TResource);
		public SResourceTypeData()
		{
		}
		public SResourceTypeData(SResourceTypeData Obj_)
		{
			Type = Obj_.Type;
			Data = Obj_.Data;
		}
		public SResourceTypeData(EResource Type_, TResource Data_)
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
		public void Set(SResourceTypeData Obj_)
		{
			Type = Obj_.Type;
			Data = Obj_.Data;
		}
		public override string StdName()
		{
			return 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(Data);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Type, "Type") + "," + 
				SEnumChecker.GetMemberName(Data, "Data");
		}
	}
	public class SUserCore : SProto
	{
		public TResource[] Resources = new TResource[5];
		public SByte Debug = default(SByte);
		public Int32 SelectedCharCode = default(Int32);
		public Int32 SinglePlayCount = default(Int32);
		public TimePoint SingleRefreshTime = default(TimePoint);
		public Int32 IslandPlayCount = default(Int32);
		public TimePoint IslandRefreshTime = default(TimePoint);
		public TimePoint QuestDailyCompleteRefreshTime = default(TimePoint);
		public Int32 ChangeNickFreeCount = default(Int32);
		public TimePoint DailyRewardExpiredTime = default(TimePoint);
		public Int32 DailyRewardCountLeft = default(Int32);
		public SUserCore()
		{
			for (int iResources = 0; iResources < Resources.Length; ++iResources)
				Resources[iResources] = default(TResource);
		}
		public SUserCore(SUserCore Obj_)
		{
			Resources = Obj_.Resources;
			Debug = Obj_.Debug;
			SelectedCharCode = Obj_.SelectedCharCode;
			SinglePlayCount = Obj_.SinglePlayCount;
			SingleRefreshTime = Obj_.SingleRefreshTime;
			IslandPlayCount = Obj_.IslandPlayCount;
			IslandRefreshTime = Obj_.IslandRefreshTime;
			QuestDailyCompleteRefreshTime = Obj_.QuestDailyCompleteRefreshTime;
			ChangeNickFreeCount = Obj_.ChangeNickFreeCount;
			DailyRewardExpiredTime = Obj_.DailyRewardExpiredTime;
			DailyRewardCountLeft = Obj_.DailyRewardCountLeft;
		}
		public SUserCore(TResource[] Resources_, SByte Debug_, Int32 SelectedCharCode_, Int32 SinglePlayCount_, TimePoint SingleRefreshTime_, Int32 IslandPlayCount_, TimePoint IslandRefreshTime_, TimePoint QuestDailyCompleteRefreshTime_, Int32 ChangeNickFreeCount_, TimePoint DailyRewardExpiredTime_, Int32 DailyRewardCountLeft_)
		{
			Resources = Resources_;
			Debug = Debug_;
			SelectedCharCode = SelectedCharCode_;
			SinglePlayCount = SinglePlayCount_;
			SingleRefreshTime = SingleRefreshTime_;
			IslandPlayCount = IslandPlayCount_;
			IslandRefreshTime = IslandRefreshTime_;
			QuestDailyCompleteRefreshTime = QuestDailyCompleteRefreshTime_;
			ChangeNickFreeCount = ChangeNickFreeCount_;
			DailyRewardExpiredTime = DailyRewardExpiredTime_;
			DailyRewardCountLeft = DailyRewardCountLeft_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Resources);
			Stream_.Pop(ref Debug);
			Stream_.Pop(ref SelectedCharCode);
			Stream_.Pop(ref SinglePlayCount);
			Stream_.Pop(ref SingleRefreshTime);
			Stream_.Pop(ref IslandPlayCount);
			Stream_.Pop(ref IslandRefreshTime);
			Stream_.Pop(ref QuestDailyCompleteRefreshTime);
			Stream_.Pop(ref ChangeNickFreeCount);
			Stream_.Pop(ref DailyRewardExpiredTime);
			Stream_.Pop(ref DailyRewardCountLeft);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Resources", ref Resources);
			Value_.Pop("Debug", ref Debug);
			Value_.Pop("SelectedCharCode", ref SelectedCharCode);
			Value_.Pop("SinglePlayCount", ref SinglePlayCount);
			Value_.Pop("SingleRefreshTime", ref SingleRefreshTime);
			Value_.Pop("IslandPlayCount", ref IslandPlayCount);
			Value_.Pop("IslandRefreshTime", ref IslandRefreshTime);
			Value_.Pop("QuestDailyCompleteRefreshTime", ref QuestDailyCompleteRefreshTime);
			Value_.Pop("ChangeNickFreeCount", ref ChangeNickFreeCount);
			Value_.Pop("DailyRewardExpiredTime", ref DailyRewardExpiredTime);
			Value_.Pop("DailyRewardCountLeft", ref DailyRewardCountLeft);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Resources);
			Stream_.Push(Debug);
			Stream_.Push(SelectedCharCode);
			Stream_.Push(SinglePlayCount);
			Stream_.Push(SingleRefreshTime);
			Stream_.Push(IslandPlayCount);
			Stream_.Push(IslandRefreshTime);
			Stream_.Push(QuestDailyCompleteRefreshTime);
			Stream_.Push(ChangeNickFreeCount);
			Stream_.Push(DailyRewardExpiredTime);
			Stream_.Push(DailyRewardCountLeft);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Resources", Resources);
			Value_.Push("Debug", Debug);
			Value_.Push("SelectedCharCode", SelectedCharCode);
			Value_.Push("SinglePlayCount", SinglePlayCount);
			Value_.Push("SingleRefreshTime", SingleRefreshTime);
			Value_.Push("IslandPlayCount", IslandPlayCount);
			Value_.Push("IslandRefreshTime", IslandRefreshTime);
			Value_.Push("QuestDailyCompleteRefreshTime", QuestDailyCompleteRefreshTime);
			Value_.Push("ChangeNickFreeCount", ChangeNickFreeCount);
			Value_.Push("DailyRewardExpiredTime", DailyRewardExpiredTime);
			Value_.Push("DailyRewardCountLeft", DailyRewardCountLeft);
		}
		public void Set(SUserCore Obj_)
		{
			Resources = Obj_.Resources;
			Debug = Obj_.Debug;
			SelectedCharCode = Obj_.SelectedCharCode;
			SinglePlayCount = Obj_.SinglePlayCount;
			SingleRefreshTime = Obj_.SingleRefreshTime;
			IslandPlayCount = Obj_.IslandPlayCount;
			IslandRefreshTime = Obj_.IslandRefreshTime;
			QuestDailyCompleteRefreshTime = Obj_.QuestDailyCompleteRefreshTime;
			ChangeNickFreeCount = Obj_.ChangeNickFreeCount;
			DailyRewardExpiredTime = Obj_.DailyRewardExpiredTime;
			DailyRewardCountLeft = Obj_.DailyRewardCountLeft;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Resources) + "," + 
				SEnumChecker.GetStdName(Debug) + "," + 
				SEnumChecker.GetStdName(SelectedCharCode) + "," + 
				SEnumChecker.GetStdName(SinglePlayCount) + "," + 
				SEnumChecker.GetStdName(SingleRefreshTime) + "," + 
				SEnumChecker.GetStdName(IslandPlayCount) + "," + 
				SEnumChecker.GetStdName(IslandRefreshTime) + "," + 
				SEnumChecker.GetStdName(QuestDailyCompleteRefreshTime) + "," + 
				SEnumChecker.GetStdName(ChangeNickFreeCount) + "," + 
				SEnumChecker.GetStdName(DailyRewardExpiredTime) + "," + 
				SEnumChecker.GetStdName(DailyRewardCountLeft);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Resources, "Resources") + "," + 
				SEnumChecker.GetMemberName(Debug, "Debug") + "," + 
				SEnumChecker.GetMemberName(SelectedCharCode, "SelectedCharCode") + "," + 
				SEnumChecker.GetMemberName(SinglePlayCount, "SinglePlayCount") + "," + 
				SEnumChecker.GetMemberName(SingleRefreshTime, "SingleRefreshTime") + "," + 
				SEnumChecker.GetMemberName(IslandPlayCount, "IslandPlayCount") + "," + 
				SEnumChecker.GetMemberName(IslandRefreshTime, "IslandRefreshTime") + "," + 
				SEnumChecker.GetMemberName(QuestDailyCompleteRefreshTime, "QuestDailyCompleteRefreshTime") + "," + 
				SEnumChecker.GetMemberName(ChangeNickFreeCount, "ChangeNickFreeCount") + "," + 
				SEnumChecker.GetMemberName(DailyRewardExpiredTime, "DailyRewardExpiredTime") + "," + 
				SEnumChecker.GetMemberName(DailyRewardCountLeft, "DailyRewardCountLeft");
		}
	}
	public class SUserBase : SUserCore
	{
		public TExp Exp = default(TExp);
		public Boolean CanPushAtNight = default(Boolean);
		public Int32 Point = default(Int32);
		public Int32 PointBest = default(Int32);
		public Int32 LastGotRewardRankIndex = default(Int32);
		public Int32 WinCountSolo = default(Int32);
		public Int32 LoseCountSolo = default(Int32);
		public Int32 WinCountSurvival = default(Int32);
		public Int32 LoseCountSurvival = default(Int32);
		public Int32 WinCountMulti = default(Int32);
		public Int32 LoseCountMulti = default(Int32);
		public Int32 BattlePointBest = default(Int32);
		public Int32 SinglePointBest = default(Int32);
		public Int32 IslandPointBest = default(Int32);
		public Int32 SingleSecondBest = default(Int32);
		public Int32 IslandPassedCountBest = default(Int32);
		public Int32 KillTotal = default(Int32);
		public Int32 ChainKillTotal = default(Int32);
		public Int32 BlowBalloonTotal = default(Int32);
		public Int32 QuestDailyCompleteCount = default(Int32);
		public Boolean TutorialReward = default(Boolean);
		public Boolean SingleStarted = default(Boolean);
		public Boolean IslandStarted = default(Boolean);
		public Int64 RankingRewardedCounter = default(Int64);
		public TNick NewNick = string.Empty;
		public SUserBase()
		{
		}
		public SUserBase(SUserBase Obj_) : base(Obj_)
		{
			Exp = Obj_.Exp;
			CanPushAtNight = Obj_.CanPushAtNight;
			Point = Obj_.Point;
			PointBest = Obj_.PointBest;
			LastGotRewardRankIndex = Obj_.LastGotRewardRankIndex;
			WinCountSolo = Obj_.WinCountSolo;
			LoseCountSolo = Obj_.LoseCountSolo;
			WinCountSurvival = Obj_.WinCountSurvival;
			LoseCountSurvival = Obj_.LoseCountSurvival;
			WinCountMulti = Obj_.WinCountMulti;
			LoseCountMulti = Obj_.LoseCountMulti;
			BattlePointBest = Obj_.BattlePointBest;
			SinglePointBest = Obj_.SinglePointBest;
			IslandPointBest = Obj_.IslandPointBest;
			SingleSecondBest = Obj_.SingleSecondBest;
			IslandPassedCountBest = Obj_.IslandPassedCountBest;
			KillTotal = Obj_.KillTotal;
			ChainKillTotal = Obj_.ChainKillTotal;
			BlowBalloonTotal = Obj_.BlowBalloonTotal;
			QuestDailyCompleteCount = Obj_.QuestDailyCompleteCount;
			TutorialReward = Obj_.TutorialReward;
			SingleStarted = Obj_.SingleStarted;
			IslandStarted = Obj_.IslandStarted;
			RankingRewardedCounter = Obj_.RankingRewardedCounter;
			NewNick = Obj_.NewNick;
		}
		public SUserBase(SUserCore Super_, TExp Exp_, Boolean CanPushAtNight_, Int32 Point_, Int32 PointBest_, Int32 LastGotRewardRankIndex_, Int32 WinCountSolo_, Int32 LoseCountSolo_, Int32 WinCountSurvival_, Int32 LoseCountSurvival_, Int32 WinCountMulti_, Int32 LoseCountMulti_, Int32 BattlePointBest_, Int32 SinglePointBest_, Int32 IslandPointBest_, Int32 SingleSecondBest_, Int32 IslandPassedCountBest_, Int32 KillTotal_, Int32 ChainKillTotal_, Int32 BlowBalloonTotal_, Int32 QuestDailyCompleteCount_, Boolean TutorialReward_, Boolean SingleStarted_, Boolean IslandStarted_, Int64 RankingRewardedCounter_, TNick NewNick_) : base(Super_)
		{
			Exp = Exp_;
			CanPushAtNight = CanPushAtNight_;
			Point = Point_;
			PointBest = PointBest_;
			LastGotRewardRankIndex = LastGotRewardRankIndex_;
			WinCountSolo = WinCountSolo_;
			LoseCountSolo = LoseCountSolo_;
			WinCountSurvival = WinCountSurvival_;
			LoseCountSurvival = LoseCountSurvival_;
			WinCountMulti = WinCountMulti_;
			LoseCountMulti = LoseCountMulti_;
			BattlePointBest = BattlePointBest_;
			SinglePointBest = SinglePointBest_;
			IslandPointBest = IslandPointBest_;
			SingleSecondBest = SingleSecondBest_;
			IslandPassedCountBest = IslandPassedCountBest_;
			KillTotal = KillTotal_;
			ChainKillTotal = ChainKillTotal_;
			BlowBalloonTotal = BlowBalloonTotal_;
			QuestDailyCompleteCount = QuestDailyCompleteCount_;
			TutorialReward = TutorialReward_;
			SingleStarted = SingleStarted_;
			IslandStarted = IslandStarted_;
			RankingRewardedCounter = RankingRewardedCounter_;
			NewNick = NewNick_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref Exp);
			Stream_.Pop(ref CanPushAtNight);
			Stream_.Pop(ref Point);
			Stream_.Pop(ref PointBest);
			Stream_.Pop(ref LastGotRewardRankIndex);
			Stream_.Pop(ref WinCountSolo);
			Stream_.Pop(ref LoseCountSolo);
			Stream_.Pop(ref WinCountSurvival);
			Stream_.Pop(ref LoseCountSurvival);
			Stream_.Pop(ref WinCountMulti);
			Stream_.Pop(ref LoseCountMulti);
			Stream_.Pop(ref BattlePointBest);
			Stream_.Pop(ref SinglePointBest);
			Stream_.Pop(ref IslandPointBest);
			Stream_.Pop(ref SingleSecondBest);
			Stream_.Pop(ref IslandPassedCountBest);
			Stream_.Pop(ref KillTotal);
			Stream_.Pop(ref ChainKillTotal);
			Stream_.Pop(ref BlowBalloonTotal);
			Stream_.Pop(ref QuestDailyCompleteCount);
			Stream_.Pop(ref TutorialReward);
			Stream_.Pop(ref SingleStarted);
			Stream_.Pop(ref IslandStarted);
			Stream_.Pop(ref RankingRewardedCounter);
			Stream_.Pop(ref NewNick);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("Exp", ref Exp);
			Value_.Pop("CanPushAtNight", ref CanPushAtNight);
			Value_.Pop("Point", ref Point);
			Value_.Pop("PointBest", ref PointBest);
			Value_.Pop("LastGotRewardRankIndex", ref LastGotRewardRankIndex);
			Value_.Pop("WinCountSolo", ref WinCountSolo);
			Value_.Pop("LoseCountSolo", ref LoseCountSolo);
			Value_.Pop("WinCountSurvival", ref WinCountSurvival);
			Value_.Pop("LoseCountSurvival", ref LoseCountSurvival);
			Value_.Pop("WinCountMulti", ref WinCountMulti);
			Value_.Pop("LoseCountMulti", ref LoseCountMulti);
			Value_.Pop("BattlePointBest", ref BattlePointBest);
			Value_.Pop("SinglePointBest", ref SinglePointBest);
			Value_.Pop("IslandPointBest", ref IslandPointBest);
			Value_.Pop("SingleSecondBest", ref SingleSecondBest);
			Value_.Pop("IslandPassedCountBest", ref IslandPassedCountBest);
			Value_.Pop("KillTotal", ref KillTotal);
			Value_.Pop("ChainKillTotal", ref ChainKillTotal);
			Value_.Pop("BlowBalloonTotal", ref BlowBalloonTotal);
			Value_.Pop("QuestDailyCompleteCount", ref QuestDailyCompleteCount);
			Value_.Pop("TutorialReward", ref TutorialReward);
			Value_.Pop("SingleStarted", ref SingleStarted);
			Value_.Pop("IslandStarted", ref IslandStarted);
			Value_.Pop("RankingRewardedCounter", ref RankingRewardedCounter);
			Value_.Pop("NewNick", ref NewNick);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(Exp);
			Stream_.Push(CanPushAtNight);
			Stream_.Push(Point);
			Stream_.Push(PointBest);
			Stream_.Push(LastGotRewardRankIndex);
			Stream_.Push(WinCountSolo);
			Stream_.Push(LoseCountSolo);
			Stream_.Push(WinCountSurvival);
			Stream_.Push(LoseCountSurvival);
			Stream_.Push(WinCountMulti);
			Stream_.Push(LoseCountMulti);
			Stream_.Push(BattlePointBest);
			Stream_.Push(SinglePointBest);
			Stream_.Push(IslandPointBest);
			Stream_.Push(SingleSecondBest);
			Stream_.Push(IslandPassedCountBest);
			Stream_.Push(KillTotal);
			Stream_.Push(ChainKillTotal);
			Stream_.Push(BlowBalloonTotal);
			Stream_.Push(QuestDailyCompleteCount);
			Stream_.Push(TutorialReward);
			Stream_.Push(SingleStarted);
			Stream_.Push(IslandStarted);
			Stream_.Push(RankingRewardedCounter);
			Stream_.Push(NewNick);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("Exp", Exp);
			Value_.Push("CanPushAtNight", CanPushAtNight);
			Value_.Push("Point", Point);
			Value_.Push("PointBest", PointBest);
			Value_.Push("LastGotRewardRankIndex", LastGotRewardRankIndex);
			Value_.Push("WinCountSolo", WinCountSolo);
			Value_.Push("LoseCountSolo", LoseCountSolo);
			Value_.Push("WinCountSurvival", WinCountSurvival);
			Value_.Push("LoseCountSurvival", LoseCountSurvival);
			Value_.Push("WinCountMulti", WinCountMulti);
			Value_.Push("LoseCountMulti", LoseCountMulti);
			Value_.Push("BattlePointBest", BattlePointBest);
			Value_.Push("SinglePointBest", SinglePointBest);
			Value_.Push("IslandPointBest", IslandPointBest);
			Value_.Push("SingleSecondBest", SingleSecondBest);
			Value_.Push("IslandPassedCountBest", IslandPassedCountBest);
			Value_.Push("KillTotal", KillTotal);
			Value_.Push("ChainKillTotal", ChainKillTotal);
			Value_.Push("BlowBalloonTotal", BlowBalloonTotal);
			Value_.Push("QuestDailyCompleteCount", QuestDailyCompleteCount);
			Value_.Push("TutorialReward", TutorialReward);
			Value_.Push("SingleStarted", SingleStarted);
			Value_.Push("IslandStarted", IslandStarted);
			Value_.Push("RankingRewardedCounter", RankingRewardedCounter);
			Value_.Push("NewNick", NewNick);
		}
		public void Set(SUserBase Obj_)
		{
			base.Set(Obj_);
			Exp = Obj_.Exp;
			CanPushAtNight = Obj_.CanPushAtNight;
			Point = Obj_.Point;
			PointBest = Obj_.PointBest;
			LastGotRewardRankIndex = Obj_.LastGotRewardRankIndex;
			WinCountSolo = Obj_.WinCountSolo;
			LoseCountSolo = Obj_.LoseCountSolo;
			WinCountSurvival = Obj_.WinCountSurvival;
			LoseCountSurvival = Obj_.LoseCountSurvival;
			WinCountMulti = Obj_.WinCountMulti;
			LoseCountMulti = Obj_.LoseCountMulti;
			BattlePointBest = Obj_.BattlePointBest;
			SinglePointBest = Obj_.SinglePointBest;
			IslandPointBest = Obj_.IslandPointBest;
			SingleSecondBest = Obj_.SingleSecondBest;
			IslandPassedCountBest = Obj_.IslandPassedCountBest;
			KillTotal = Obj_.KillTotal;
			ChainKillTotal = Obj_.ChainKillTotal;
			BlowBalloonTotal = Obj_.BlowBalloonTotal;
			QuestDailyCompleteCount = Obj_.QuestDailyCompleteCount;
			TutorialReward = Obj_.TutorialReward;
			SingleStarted = Obj_.SingleStarted;
			IslandStarted = Obj_.IslandStarted;
			RankingRewardedCounter = Obj_.RankingRewardedCounter;
			NewNick = Obj_.NewNick;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(Exp) + "," + 
				SEnumChecker.GetStdName(CanPushAtNight) + "," + 
				SEnumChecker.GetStdName(Point) + "," + 
				SEnumChecker.GetStdName(PointBest) + "," + 
				SEnumChecker.GetStdName(LastGotRewardRankIndex) + "," + 
				SEnumChecker.GetStdName(WinCountSolo) + "," + 
				SEnumChecker.GetStdName(LoseCountSolo) + "," + 
				SEnumChecker.GetStdName(WinCountSurvival) + "," + 
				SEnumChecker.GetStdName(LoseCountSurvival) + "," + 
				SEnumChecker.GetStdName(WinCountMulti) + "," + 
				SEnumChecker.GetStdName(LoseCountMulti) + "," + 
				SEnumChecker.GetStdName(BattlePointBest) + "," + 
				SEnumChecker.GetStdName(SinglePointBest) + "," + 
				SEnumChecker.GetStdName(IslandPointBest) + "," + 
				SEnumChecker.GetStdName(SingleSecondBest) + "," + 
				SEnumChecker.GetStdName(IslandPassedCountBest) + "," + 
				SEnumChecker.GetStdName(KillTotal) + "," + 
				SEnumChecker.GetStdName(ChainKillTotal) + "," + 
				SEnumChecker.GetStdName(BlowBalloonTotal) + "," + 
				SEnumChecker.GetStdName(QuestDailyCompleteCount) + "," + 
				SEnumChecker.GetStdName(TutorialReward) + "," + 
				SEnumChecker.GetStdName(SingleStarted) + "," + 
				SEnumChecker.GetStdName(IslandStarted) + "," + 
				SEnumChecker.GetStdName(RankingRewardedCounter) + "," + 
				SEnumChecker.GetStdName(NewNick);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(Exp, "Exp") + "," + 
				SEnumChecker.GetMemberName(CanPushAtNight, "CanPushAtNight") + "," + 
				SEnumChecker.GetMemberName(Point, "Point") + "," + 
				SEnumChecker.GetMemberName(PointBest, "PointBest") + "," + 
				SEnumChecker.GetMemberName(LastGotRewardRankIndex, "LastGotRewardRankIndex") + "," + 
				SEnumChecker.GetMemberName(WinCountSolo, "WinCountSolo") + "," + 
				SEnumChecker.GetMemberName(LoseCountSolo, "LoseCountSolo") + "," + 
				SEnumChecker.GetMemberName(WinCountSurvival, "WinCountSurvival") + "," + 
				SEnumChecker.GetMemberName(LoseCountSurvival, "LoseCountSurvival") + "," + 
				SEnumChecker.GetMemberName(WinCountMulti, "WinCountMulti") + "," + 
				SEnumChecker.GetMemberName(LoseCountMulti, "LoseCountMulti") + "," + 
				SEnumChecker.GetMemberName(BattlePointBest, "BattlePointBest") + "," + 
				SEnumChecker.GetMemberName(SinglePointBest, "SinglePointBest") + "," + 
				SEnumChecker.GetMemberName(IslandPointBest, "IslandPointBest") + "," + 
				SEnumChecker.GetMemberName(SingleSecondBest, "SingleSecondBest") + "," + 
				SEnumChecker.GetMemberName(IslandPassedCountBest, "IslandPassedCountBest") + "," + 
				SEnumChecker.GetMemberName(KillTotal, "KillTotal") + "," + 
				SEnumChecker.GetMemberName(ChainKillTotal, "ChainKillTotal") + "," + 
				SEnumChecker.GetMemberName(BlowBalloonTotal, "BlowBalloonTotal") + "," + 
				SEnumChecker.GetMemberName(QuestDailyCompleteCount, "QuestDailyCompleteCount") + "," + 
				SEnumChecker.GetMemberName(TutorialReward, "TutorialReward") + "," + 
				SEnumChecker.GetMemberName(SingleStarted, "SingleStarted") + "," + 
				SEnumChecker.GetMemberName(IslandStarted, "IslandStarted") + "," + 
				SEnumChecker.GetMemberName(RankingRewardedCounter, "RankingRewardedCounter") + "," + 
				SEnumChecker.GetMemberName(NewNick, "NewNick");
		}
	}
	public class SUserNet : SUserBase
	{
		public String CountryCode = string.Empty;
		public SUserNet()
		{
		}
		public SUserNet(SUserNet Obj_) : base(Obj_)
		{
			CountryCode = Obj_.CountryCode;
		}
		public SUserNet(SUserBase Super_, String CountryCode_) : base(Super_)
		{
			CountryCode = CountryCode_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref CountryCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("CountryCode", ref CountryCode);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(CountryCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("CountryCode", CountryCode);
		}
		public void Set(SUserNet Obj_)
		{
			base.Set(Obj_);
			CountryCode = Obj_.CountryCode;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(CountryCode);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(CountryCode, "CountryCode");
		}
	}
	public class SQuestBase : SProto
	{
		public Int32 Code = default(Int32);
		public Int32 Count = default(Int32);
		public TimePoint CoolEndTime = default(TimePoint);
		public SQuestBase()
		{
		}
		public SQuestBase(SQuestBase Obj_)
		{
			Code = Obj_.Code;
			Count = Obj_.Count;
			CoolEndTime = Obj_.CoolEndTime;
		}
		public SQuestBase(Int32 Code_, Int32 Count_, TimePoint CoolEndTime_)
		{
			Code = Code_;
			Count = Count_;
			CoolEndTime = CoolEndTime_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
			Stream_.Pop(ref Count);
			Stream_.Pop(ref CoolEndTime);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
			Value_.Pop("Count", ref Count);
			Value_.Pop("CoolEndTime", ref CoolEndTime);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
			Stream_.Push(Count);
			Stream_.Push(CoolEndTime);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
			Value_.Push("Count", Count);
			Value_.Push("CoolEndTime", CoolEndTime);
		}
		public void Set(SQuestBase Obj_)
		{
			Code = Obj_.Code;
			Count = Obj_.Count;
			CoolEndTime = Obj_.CoolEndTime;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code) + "," + 
				SEnumChecker.GetStdName(Count) + "," + 
				SEnumChecker.GetStdName(CoolEndTime);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(Count, "Count") + "," + 
				SEnumChecker.GetMemberName(CoolEndTime, "CoolEndTime");
		}
	}
	public class SLoginNetSc : SProto
	{
		public SUserNet User = new SUserNet();
		public TChars Chars = new TChars();
		public TimePoint ServerTime = default(TimePoint);
		public TQuestDBs Quests = new TQuestDBs();
		public TPackages Packages = new TPackages();
		public Int32 RoomIdx = default(Int32);
		public SLoginNetSc()
		{
		}
		public SLoginNetSc(SLoginNetSc Obj_)
		{
			User = Obj_.User;
			Chars = Obj_.Chars;
			ServerTime = Obj_.ServerTime;
			Quests = Obj_.Quests;
			Packages = Obj_.Packages;
			RoomIdx = Obj_.RoomIdx;
		}
		public SLoginNetSc(SUserNet User_, TChars Chars_, TimePoint ServerTime_, TQuestDBs Quests_, TPackages Packages_, Int32 RoomIdx_)
		{
			User = User_;
			Chars = Chars_;
			ServerTime = ServerTime_;
			Quests = Quests_;
			Packages = Packages_;
			RoomIdx = RoomIdx_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref User);
			Stream_.Pop(ref Chars);
			Stream_.Pop(ref ServerTime);
			Stream_.Pop(ref Quests);
			Stream_.Pop(ref Packages);
			Stream_.Pop(ref RoomIdx);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("User", ref User);
			Value_.Pop("Chars", ref Chars);
			Value_.Pop("ServerTime", ref ServerTime);
			Value_.Pop("Quests", ref Quests);
			Value_.Pop("Packages", ref Packages);
			Value_.Pop("RoomIdx", ref RoomIdx);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(User);
			Stream_.Push(Chars);
			Stream_.Push(ServerTime);
			Stream_.Push(Quests);
			Stream_.Push(Packages);
			Stream_.Push(RoomIdx);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("User", User);
			Value_.Push("Chars", Chars);
			Value_.Push("ServerTime", ServerTime);
			Value_.Push("Quests", Quests);
			Value_.Push("Packages", Packages);
			Value_.Push("RoomIdx", RoomIdx);
		}
		public void Set(SLoginNetSc Obj_)
		{
			User.Set(Obj_.User);
			Chars = Obj_.Chars;
			ServerTime = Obj_.ServerTime;
			Quests = Obj_.Quests;
			Packages = Obj_.Packages;
			RoomIdx = Obj_.RoomIdx;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(User) + "," + 
				SEnumChecker.GetStdName(Chars) + "," + 
				SEnumChecker.GetStdName(ServerTime) + "," + 
				SEnumChecker.GetStdName(Quests) + "," + 
				SEnumChecker.GetStdName(Packages) + "," + 
				SEnumChecker.GetStdName(RoomIdx);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(User, "User") + "," + 
				SEnumChecker.GetMemberName(Chars, "Chars") + "," + 
				SEnumChecker.GetMemberName(ServerTime, "ServerTime") + "," + 
				SEnumChecker.GetMemberName(Quests, "Quests") + "," + 
				SEnumChecker.GetMemberName(Packages, "Packages") + "," + 
				SEnumChecker.GetMemberName(RoomIdx, "RoomIdx");
		}
	}
	public class SLobbyNetSc : SProto
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
	public class SUserExpNetSc : SProto
	{
		public TExp Exp = default(TExp);
		public SUserExpNetSc()
		{
		}
		public SUserExpNetSc(SUserExpNetSc Obj_)
		{
			Exp = Obj_.Exp;
		}
		public SUserExpNetSc(TExp Exp_)
		{
			Exp = Exp_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Exp);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Exp", ref Exp);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Exp);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Exp", Exp);
		}
		public void Set(SUserExpNetSc Obj_)
		{
			Exp = Obj_.Exp;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Exp);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Exp, "Exp");
		}
	}
	public class SResourcesNetSc : SProto
	{
		public TResource[] Resources = new TResource[5];
		public SResourcesNetSc()
		{
			for (int iResources = 0; iResources < Resources.Length; ++iResources)
				Resources[iResources] = default(TResource);
		}
		public SResourcesNetSc(SResourcesNetSc Obj_)
		{
			Resources = Obj_.Resources;
		}
		public SResourcesNetSc(TResource[] Resources_)
		{
			Resources = Resources_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Resources);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Resources", ref Resources);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Resources);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Resources", Resources);
		}
		public void Set(SResourcesNetSc Obj_)
		{
			Resources = Obj_.Resources;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Resources);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Resources, "Resources");
		}
	}
	public class SSetPointNetSc : SProto
	{
		public Int32 Point = default(Int32);
		public SSetPointNetSc()
		{
		}
		public SSetPointNetSc(SSetPointNetSc Obj_)
		{
			Point = Obj_.Point;
		}
		public SSetPointNetSc(Int32 Point_)
		{
			Point = Point_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Point);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Point", ref Point);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Point);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Point", Point);
		}
		public void Set(SSetPointNetSc Obj_)
		{
			Point = Obj_.Point;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Point);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Point, "Point");
		}
	}
	public class SSetCharNetSc : SProto
	{
		public List<Int32> CharCodes = new List<Int32>();
		public SSetCharNetSc()
		{
		}
		public SSetCharNetSc(SSetCharNetSc Obj_)
		{
			CharCodes = Obj_.CharCodes;
		}
		public SSetCharNetSc(List<Int32> CharCodes_)
		{
			CharCodes = CharCodes_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref CharCodes);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("CharCodes", ref CharCodes);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(CharCodes);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("CharCodes", CharCodes);
		}
		public void Set(SSetCharNetSc Obj_)
		{
			CharCodes = Obj_.CharCodes;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(CharCodes);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(CharCodes, "CharCodes");
		}
	}
	public class SUnsetCharNetSc : SProto
	{
		public List<Int32> CharCodes = new List<Int32>();
		public SUnsetCharNetSc()
		{
		}
		public SUnsetCharNetSc(SUnsetCharNetSc Obj_)
		{
			CharCodes = Obj_.CharCodes;
		}
		public SUnsetCharNetSc(List<Int32> CharCodes_)
		{
			CharCodes = CharCodes_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref CharCodes);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("CharCodes", ref CharCodes);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(CharCodes);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("CharCodes", CharCodes);
		}
		public void Set(SUnsetCharNetSc Obj_)
		{
			CharCodes = Obj_.CharCodes;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(CharCodes);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(CharCodes, "CharCodes");
		}
	}
	public class SChatNetCs : SProto
	{
		public String Msg = string.Empty;
		public SChatNetCs()
		{
		}
		public SChatNetCs(SChatNetCs Obj_)
		{
			Msg = Obj_.Msg;
		}
		public SChatNetCs(String Msg_)
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
		public void Set(SChatNetCs Obj_)
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
	public class SChatNetSc : SProto
	{
		public String Msg = string.Empty;
		public SChatNetSc()
		{
		}
		public SChatNetSc(SChatNetSc Obj_)
		{
			Msg = Obj_.Msg;
		}
		public SChatNetSc(String Msg_)
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
		public void Set(SChatNetSc Obj_)
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
	public class SFCMTokenSetNetCs : SProto
	{
		public String Token = string.Empty;
		public SFCMTokenSetNetCs()
		{
		}
		public SFCMTokenSetNetCs(SFCMTokenSetNetCs Obj_)
		{
			Token = Obj_.Token;
		}
		public SFCMTokenSetNetCs(String Token_)
		{
			Token = Token_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Token);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Token", ref Token);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Token);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Token", Token);
		}
		public void Set(SFCMTokenSetNetCs Obj_)
		{
			Token = Obj_.Token;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Token);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Token, "Token");
		}
	}
	public class SFCMTokenUnsetNetCs : SProto
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
	public class SFCMCanPushAtNightNetCs : SProto
	{
		public Boolean CanPush = default(Boolean);
		public SFCMCanPushAtNightNetCs()
		{
		}
		public SFCMCanPushAtNightNetCs(SFCMCanPushAtNightNetCs Obj_)
		{
			CanPush = Obj_.CanPush;
		}
		public SFCMCanPushAtNightNetCs(Boolean CanPush_)
		{
			CanPush = CanPush_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref CanPush);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("CanPush", ref CanPush);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(CanPush);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("CanPush", CanPush);
		}
		public void Set(SFCMCanPushAtNightNetCs Obj_)
		{
			CanPush = Obj_.CanPush;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(CanPush);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(CanPush, "CanPush");
		}
	}
	public class SChangeLanguageNetCs : SProto
	{
		public ELanguage Language = default(ELanguage);
		public SChangeLanguageNetCs()
		{
		}
		public SChangeLanguageNetCs(SChangeLanguageNetCs Obj_)
		{
			Language = Obj_.Language;
		}
		public SChangeLanguageNetCs(ELanguage Language_)
		{
			Language = Language_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Language);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Language", ref Language);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Language);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Language", Language);
		}
		public void Set(SChangeLanguageNetCs Obj_)
		{
			Language = Obj_.Language;
		}
		public override string StdName()
		{
			return 
				"bb.ELanguage";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Language, "Language");
		}
	}
	public class SBuyNetCs : SProto
	{
		public Int32 Code = default(Int32);
		public SBuyNetCs()
		{
		}
		public SBuyNetCs(SBuyNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public SBuyNetCs(Int32 Code_)
		{
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
		}
		public void Set(SBuyNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SBuyNetSc : SProto
	{
		public Int32 Code = default(Int32);
		public SBuyNetSc()
		{
		}
		public SBuyNetSc(SBuyNetSc Obj_)
		{
			Code = Obj_.Code;
		}
		public SBuyNetSc(Int32 Code_)
		{
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
		}
		public void Set(SBuyNetSc Obj_)
		{
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SBuyCharNetCs : SProto
	{
		public Int32 Code = default(Int32);
		public SBuyCharNetCs()
		{
		}
		public SBuyCharNetCs(SBuyCharNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public SBuyCharNetCs(Int32 Code_)
		{
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
		}
		public void Set(SBuyCharNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SBuyCharNetSc : SProto
	{
		public Int32 Code = default(Int32);
		public SBuyCharNetSc()
		{
		}
		public SBuyCharNetSc(SBuyCharNetSc Obj_)
		{
			Code = Obj_.Code;
		}
		public SBuyCharNetSc(Int32 Code_)
		{
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
		}
		public void Set(SBuyCharNetSc Obj_)
		{
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SBuyPackageNetCs : SProto
	{
		public Int32 Code = default(Int32);
		public SBuyPackageNetCs()
		{
		}
		public SBuyPackageNetCs(SBuyPackageNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public SBuyPackageNetCs(Int32 Code_)
		{
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
		}
		public void Set(SBuyPackageNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SBuyPackageNetSc : SProto
	{
		public Int32 Code = default(Int32);
		public SBuyPackageNetSc()
		{
		}
		public SBuyPackageNetSc(SBuyPackageNetSc Obj_)
		{
			Code = Obj_.Code;
		}
		public SBuyPackageNetSc(Int32 Code_)
		{
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
		}
		public void Set(SBuyPackageNetSc Obj_)
		{
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SPurchaseNetCs : SProto
	{
		public String ProductID = string.Empty;
		public String PurchaseToken = string.Empty;
		public SPurchaseNetCs()
		{
		}
		public SPurchaseNetCs(SPurchaseNetCs Obj_)
		{
			ProductID = Obj_.ProductID;
			PurchaseToken = Obj_.PurchaseToken;
		}
		public SPurchaseNetCs(String ProductID_, String PurchaseToken_)
		{
			ProductID = ProductID_;
			PurchaseToken = PurchaseToken_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref ProductID);
			Stream_.Pop(ref PurchaseToken);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("ProductID", ref ProductID);
			Value_.Pop("PurchaseToken", ref PurchaseToken);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(ProductID);
			Stream_.Push(PurchaseToken);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("ProductID", ProductID);
			Value_.Push("PurchaseToken", PurchaseToken);
		}
		public void Set(SPurchaseNetCs Obj_)
		{
			ProductID = Obj_.ProductID;
			PurchaseToken = Obj_.PurchaseToken;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(ProductID) + "," + 
				SEnumChecker.GetStdName(PurchaseToken);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(ProductID, "ProductID") + "," + 
				SEnumChecker.GetMemberName(PurchaseToken, "PurchaseToken");
		}
	}
	public class SPurchaseNetSc : SProto
	{
		public String ProductID = string.Empty;
		public TResource DiaPaidAdded = default(TResource);
		public SPurchaseNetSc()
		{
		}
		public SPurchaseNetSc(SPurchaseNetSc Obj_)
		{
			ProductID = Obj_.ProductID;
			DiaPaidAdded = Obj_.DiaPaidAdded;
		}
		public SPurchaseNetSc(String ProductID_, TResource DiaPaidAdded_)
		{
			ProductID = ProductID_;
			DiaPaidAdded = DiaPaidAdded_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref ProductID);
			Stream_.Pop(ref DiaPaidAdded);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("ProductID", ref ProductID);
			Value_.Pop("DiaPaidAdded", ref DiaPaidAdded);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(ProductID);
			Stream_.Push(DiaPaidAdded);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("ProductID", ProductID);
			Value_.Push("DiaPaidAdded", DiaPaidAdded);
		}
		public void Set(SPurchaseNetSc Obj_)
		{
			ProductID = Obj_.ProductID;
			DiaPaidAdded = Obj_.DiaPaidAdded;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(ProductID) + "," + 
				SEnumChecker.GetStdName(DiaPaidAdded);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(ProductID, "ProductID") + "," + 
				SEnumChecker.GetMemberName(DiaPaidAdded, "DiaPaidAdded");
		}
	}
	public class SDailyRewardNetCs : SProto
	{
		public Boolean IsWatchedAd = default(Boolean);
		public SDailyRewardNetCs()
		{
		}
		public SDailyRewardNetCs(SDailyRewardNetCs Obj_)
		{
			IsWatchedAd = Obj_.IsWatchedAd;
		}
		public SDailyRewardNetCs(Boolean IsWatchedAd_)
		{
			IsWatchedAd = IsWatchedAd_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref IsWatchedAd);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("IsWatchedAd", ref IsWatchedAd);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(IsWatchedAd);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("IsWatchedAd", IsWatchedAd);
		}
		public void Set(SDailyRewardNetCs Obj_)
		{
			IsWatchedAd = Obj_.IsWatchedAd;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(IsWatchedAd);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(IsWatchedAd, "IsWatchedAd");
		}
	}
	public class SDailyRewardNetSc : SProto
	{
		public SResourceTypeData Reward = new SResourceTypeData();
		public TimePoint ExpiredTime = default(TimePoint);
		public Int32 CountLeft = default(Int32);
		public SDailyRewardNetSc()
		{
		}
		public SDailyRewardNetSc(SDailyRewardNetSc Obj_)
		{
			Reward = Obj_.Reward;
			ExpiredTime = Obj_.ExpiredTime;
			CountLeft = Obj_.CountLeft;
		}
		public SDailyRewardNetSc(SResourceTypeData Reward_, TimePoint ExpiredTime_, Int32 CountLeft_)
		{
			Reward = Reward_;
			ExpiredTime = ExpiredTime_;
			CountLeft = CountLeft_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Reward);
			Stream_.Pop(ref ExpiredTime);
			Stream_.Pop(ref CountLeft);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Reward", ref Reward);
			Value_.Pop("ExpiredTime", ref ExpiredTime);
			Value_.Pop("CountLeft", ref CountLeft);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Reward);
			Stream_.Push(ExpiredTime);
			Stream_.Push(CountLeft);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Reward", Reward);
			Value_.Push("ExpiredTime", ExpiredTime);
			Value_.Push("CountLeft", CountLeft);
		}
		public void Set(SDailyRewardNetSc Obj_)
		{
			Reward.Set(Obj_.Reward);
			ExpiredTime = Obj_.ExpiredTime;
			CountLeft = Obj_.CountLeft;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Reward) + "," + 
				SEnumChecker.GetStdName(ExpiredTime) + "," + 
				SEnumChecker.GetStdName(CountLeft);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Reward, "Reward") + "," + 
				SEnumChecker.GetMemberName(ExpiredTime, "ExpiredTime") + "," + 
				SEnumChecker.GetMemberName(CountLeft, "CountLeft");
		}
	}
	public class SDailyRewardFailNetSc : SProto
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
	public class SSelectCharNetCs : SProto
	{
		public Int32 Code = default(Int32);
		public SSelectCharNetCs()
		{
		}
		public SSelectCharNetCs(SSelectCharNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public SSelectCharNetCs(Int32 Code_)
		{
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
		}
		public void Set(SSelectCharNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SSingleStartNetCs : SProto
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
	public class SSingleStartNetSc : SProto
	{
		public TResource GoldCost = default(TResource);
		public Int32 PlayCount = default(Int32);
		public TimePoint RefreshTime = default(TimePoint);
		public TDoneQuests DoneQuests = new TDoneQuests();
		public SSingleStartNetSc()
		{
		}
		public SSingleStartNetSc(SSingleStartNetSc Obj_)
		{
			GoldCost = Obj_.GoldCost;
			PlayCount = Obj_.PlayCount;
			RefreshTime = Obj_.RefreshTime;
			DoneQuests = Obj_.DoneQuests;
		}
		public SSingleStartNetSc(TResource GoldCost_, Int32 PlayCount_, TimePoint RefreshTime_, TDoneQuests DoneQuests_)
		{
			GoldCost = GoldCost_;
			PlayCount = PlayCount_;
			RefreshTime = RefreshTime_;
			DoneQuests = DoneQuests_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref GoldCost);
			Stream_.Pop(ref PlayCount);
			Stream_.Pop(ref RefreshTime);
			Stream_.Pop(ref DoneQuests);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("GoldCost", ref GoldCost);
			Value_.Pop("PlayCount", ref PlayCount);
			Value_.Pop("RefreshTime", ref RefreshTime);
			Value_.Pop("DoneQuests", ref DoneQuests);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(GoldCost);
			Stream_.Push(PlayCount);
			Stream_.Push(RefreshTime);
			Stream_.Push(DoneQuests);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("GoldCost", GoldCost);
			Value_.Push("PlayCount", PlayCount);
			Value_.Push("RefreshTime", RefreshTime);
			Value_.Push("DoneQuests", DoneQuests);
		}
		public void Set(SSingleStartNetSc Obj_)
		{
			GoldCost = Obj_.GoldCost;
			PlayCount = Obj_.PlayCount;
			RefreshTime = Obj_.RefreshTime;
			DoneQuests = Obj_.DoneQuests;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(GoldCost) + "," + 
				SEnumChecker.GetStdName(PlayCount) + "," + 
				SEnumChecker.GetStdName(RefreshTime) + "," + 
				SEnumChecker.GetStdName(DoneQuests);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(GoldCost, "GoldCost") + "," + 
				SEnumChecker.GetMemberName(PlayCount, "PlayCount") + "," + 
				SEnumChecker.GetMemberName(RefreshTime, "RefreshTime") + "," + 
				SEnumChecker.GetMemberName(DoneQuests, "DoneQuests");
		}
	}
	public class SSingleEndNetCs : SProto
	{
		public Int32 Wave = default(Int32);
		public Int32 Second = default(Int32);
		public TResource Gold = default(TResource);
		public TResource GoldAdded = default(TResource);
		public TResource DiaAdded = default(TResource);
		public SSingleEndNetCs()
		{
		}
		public SSingleEndNetCs(SSingleEndNetCs Obj_)
		{
			Wave = Obj_.Wave;
			Second = Obj_.Second;
			Gold = Obj_.Gold;
			GoldAdded = Obj_.GoldAdded;
			DiaAdded = Obj_.DiaAdded;
		}
		public SSingleEndNetCs(Int32 Wave_, Int32 Second_, TResource Gold_, TResource GoldAdded_, TResource DiaAdded_)
		{
			Wave = Wave_;
			Second = Second_;
			Gold = Gold_;
			GoldAdded = GoldAdded_;
			DiaAdded = DiaAdded_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Wave);
			Stream_.Pop(ref Second);
			Stream_.Pop(ref Gold);
			Stream_.Pop(ref GoldAdded);
			Stream_.Pop(ref DiaAdded);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Wave", ref Wave);
			Value_.Pop("Second", ref Second);
			Value_.Pop("Gold", ref Gold);
			Value_.Pop("GoldAdded", ref GoldAdded);
			Value_.Pop("DiaAdded", ref DiaAdded);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Wave);
			Stream_.Push(Second);
			Stream_.Push(Gold);
			Stream_.Push(GoldAdded);
			Stream_.Push(DiaAdded);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Wave", Wave);
			Value_.Push("Second", Second);
			Value_.Push("Gold", Gold);
			Value_.Push("GoldAdded", GoldAdded);
			Value_.Push("DiaAdded", DiaAdded);
		}
		public void Set(SSingleEndNetCs Obj_)
		{
			Wave = Obj_.Wave;
			Second = Obj_.Second;
			Gold = Obj_.Gold;
			GoldAdded = Obj_.GoldAdded;
			DiaAdded = Obj_.DiaAdded;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Wave) + "," + 
				SEnumChecker.GetStdName(Second) + "," + 
				SEnumChecker.GetStdName(Gold) + "," + 
				SEnumChecker.GetStdName(GoldAdded) + "," + 
				SEnumChecker.GetStdName(DiaAdded);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Wave, "Wave") + "," + 
				SEnumChecker.GetMemberName(Second, "Second") + "," + 
				SEnumChecker.GetMemberName(Gold, "Gold") + "," + 
				SEnumChecker.GetMemberName(GoldAdded, "GoldAdded") + "," + 
				SEnumChecker.GetMemberName(DiaAdded, "DiaAdded");
		}
	}
	public class SSingleEndNetSc : SProto
	{
		public TDoneQuests DoneQuests = new TDoneQuests();
		public SSingleEndNetSc()
		{
		}
		public SSingleEndNetSc(SSingleEndNetSc Obj_)
		{
			DoneQuests = Obj_.DoneQuests;
		}
		public SSingleEndNetSc(TDoneQuests DoneQuests_)
		{
			DoneQuests = DoneQuests_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref DoneQuests);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("DoneQuests", ref DoneQuests);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(DoneQuests);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("DoneQuests", DoneQuests);
		}
		public void Set(SSingleEndNetSc Obj_)
		{
			DoneQuests = Obj_.DoneQuests;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(DoneQuests);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(DoneQuests, "DoneQuests");
		}
	}
	public class SIslandStartNetCs : SProto
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
	public class SIslandStartNetSc : SProto
	{
		public TResource GoldCost = default(TResource);
		public Int32 PlayCount = default(Int32);
		public TimePoint RefreshTime = default(TimePoint);
		public TDoneQuests DoneQuests = new TDoneQuests();
		public SIslandStartNetSc()
		{
		}
		public SIslandStartNetSc(SIslandStartNetSc Obj_)
		{
			GoldCost = Obj_.GoldCost;
			PlayCount = Obj_.PlayCount;
			RefreshTime = Obj_.RefreshTime;
			DoneQuests = Obj_.DoneQuests;
		}
		public SIslandStartNetSc(TResource GoldCost_, Int32 PlayCount_, TimePoint RefreshTime_, TDoneQuests DoneQuests_)
		{
			GoldCost = GoldCost_;
			PlayCount = PlayCount_;
			RefreshTime = RefreshTime_;
			DoneQuests = DoneQuests_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref GoldCost);
			Stream_.Pop(ref PlayCount);
			Stream_.Pop(ref RefreshTime);
			Stream_.Pop(ref DoneQuests);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("GoldCost", ref GoldCost);
			Value_.Pop("PlayCount", ref PlayCount);
			Value_.Pop("RefreshTime", ref RefreshTime);
			Value_.Pop("DoneQuests", ref DoneQuests);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(GoldCost);
			Stream_.Push(PlayCount);
			Stream_.Push(RefreshTime);
			Stream_.Push(DoneQuests);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("GoldCost", GoldCost);
			Value_.Push("PlayCount", PlayCount);
			Value_.Push("RefreshTime", RefreshTime);
			Value_.Push("DoneQuests", DoneQuests);
		}
		public void Set(SIslandStartNetSc Obj_)
		{
			GoldCost = Obj_.GoldCost;
			PlayCount = Obj_.PlayCount;
			RefreshTime = Obj_.RefreshTime;
			DoneQuests = Obj_.DoneQuests;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(GoldCost) + "," + 
				SEnumChecker.GetStdName(PlayCount) + "," + 
				SEnumChecker.GetStdName(RefreshTime) + "," + 
				SEnumChecker.GetStdName(DoneQuests);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(GoldCost, "GoldCost") + "," + 
				SEnumChecker.GetMemberName(PlayCount, "PlayCount") + "," + 
				SEnumChecker.GetMemberName(RefreshTime, "RefreshTime") + "," + 
				SEnumChecker.GetMemberName(DoneQuests, "DoneQuests");
		}
	}
	public class SIslandEndNetCs : SProto
	{
		public Int32 PassedIslandCount = default(Int32);
		public TResource Gold = default(TResource);
		public TResource GoldAdded = default(TResource);
		public TResource DiaAdded = default(TResource);
		public SIslandEndNetCs()
		{
		}
		public SIslandEndNetCs(SIslandEndNetCs Obj_)
		{
			PassedIslandCount = Obj_.PassedIslandCount;
			Gold = Obj_.Gold;
			GoldAdded = Obj_.GoldAdded;
			DiaAdded = Obj_.DiaAdded;
		}
		public SIslandEndNetCs(Int32 PassedIslandCount_, TResource Gold_, TResource GoldAdded_, TResource DiaAdded_)
		{
			PassedIslandCount = PassedIslandCount_;
			Gold = Gold_;
			GoldAdded = GoldAdded_;
			DiaAdded = DiaAdded_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref PassedIslandCount);
			Stream_.Pop(ref Gold);
			Stream_.Pop(ref GoldAdded);
			Stream_.Pop(ref DiaAdded);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("PassedIslandCount", ref PassedIslandCount);
			Value_.Pop("Gold", ref Gold);
			Value_.Pop("GoldAdded", ref GoldAdded);
			Value_.Pop("DiaAdded", ref DiaAdded);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(PassedIslandCount);
			Stream_.Push(Gold);
			Stream_.Push(GoldAdded);
			Stream_.Push(DiaAdded);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("PassedIslandCount", PassedIslandCount);
			Value_.Push("Gold", Gold);
			Value_.Push("GoldAdded", GoldAdded);
			Value_.Push("DiaAdded", DiaAdded);
		}
		public void Set(SIslandEndNetCs Obj_)
		{
			PassedIslandCount = Obj_.PassedIslandCount;
			Gold = Obj_.Gold;
			GoldAdded = Obj_.GoldAdded;
			DiaAdded = Obj_.DiaAdded;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(PassedIslandCount) + "," + 
				SEnumChecker.GetStdName(Gold) + "," + 
				SEnumChecker.GetStdName(GoldAdded) + "," + 
				SEnumChecker.GetStdName(DiaAdded);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(PassedIslandCount, "PassedIslandCount") + "," + 
				SEnumChecker.GetMemberName(Gold, "Gold") + "," + 
				SEnumChecker.GetMemberName(GoldAdded, "GoldAdded") + "," + 
				SEnumChecker.GetMemberName(DiaAdded, "DiaAdded");
		}
	}
	public class SIslandEndNetSc : SProto
	{
		public TDoneQuests DoneQuests = new TDoneQuests();
		public SIslandEndNetSc()
		{
		}
		public SIslandEndNetSc(SIslandEndNetSc Obj_)
		{
			DoneQuests = Obj_.DoneQuests;
		}
		public SIslandEndNetSc(TDoneQuests DoneQuests_)
		{
			DoneQuests = DoneQuests_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref DoneQuests);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("DoneQuests", ref DoneQuests);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(DoneQuests);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("DoneQuests", DoneQuests);
		}
		public void Set(SIslandEndNetSc Obj_)
		{
			DoneQuests = Obj_.DoneQuests;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(DoneQuests);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(DoneQuests, "DoneQuests");
		}
	}
	public class SBattleType : SProto
	{
		public EGameMode GameMode = default(EGameMode);
		public SByte MemberCount = default(SByte);
		public TTeamCnt TeamCount = default(TTeamCnt);
		public SBattleType()
		{
		}
		public SBattleType(SBattleType Obj_)
		{
			GameMode = Obj_.GameMode;
			MemberCount = Obj_.MemberCount;
			TeamCount = Obj_.TeamCount;
		}
		public SBattleType(EGameMode GameMode_, SByte MemberCount_, TTeamCnt TeamCount_)
		{
			GameMode = GameMode_;
			MemberCount = MemberCount_;
			TeamCount = TeamCount_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref GameMode);
			Stream_.Pop(ref MemberCount);
			Stream_.Pop(ref TeamCount);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("GameMode", ref GameMode);
			Value_.Pop("MemberCount", ref MemberCount);
			Value_.Pop("TeamCount", ref TeamCount);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(GameMode);
			Stream_.Push(MemberCount);
			Stream_.Push(TeamCount);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("GameMode", GameMode);
			Value_.Push("MemberCount", MemberCount);
			Value_.Push("TeamCount", TeamCount);
		}
		public void Set(SBattleType Obj_)
		{
			GameMode = Obj_.GameMode;
			MemberCount = Obj_.MemberCount;
			TeamCount = Obj_.TeamCount;
		}
		public override string StdName()
		{
			return 
				"bb.EGameMode" + "," + 
				SEnumChecker.GetStdName(MemberCount) + "," + 
				SEnumChecker.GetStdName(TeamCount);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(GameMode, "GameMode") + "," + 
				SEnumChecker.GetMemberName(MemberCount, "MemberCount") + "," + 
				SEnumChecker.GetMemberName(TeamCount, "TeamCount");
		}
	}
	public class SBattleRecord : SProto
	{
		public Int32 TotalKillCount = default(Int32);
		public Int32 TotalBalloonHitCount = default(Int32);
		public SBattleRecord()
		{
		}
		public SBattleRecord(SBattleRecord Obj_)
		{
			TotalKillCount = Obj_.TotalKillCount;
			TotalBalloonHitCount = Obj_.TotalBalloonHitCount;
		}
		public SBattleRecord(Int32 TotalKillCount_, Int32 TotalBalloonHitCount_)
		{
			TotalKillCount = TotalKillCount_;
			TotalBalloonHitCount = TotalBalloonHitCount_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref TotalKillCount);
			Stream_.Pop(ref TotalBalloonHitCount);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("TotalKillCount", ref TotalKillCount);
			Value_.Pop("TotalBalloonHitCount", ref TotalBalloonHitCount);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(TotalKillCount);
			Stream_.Push(TotalBalloonHitCount);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("TotalKillCount", TotalKillCount);
			Value_.Push("TotalBalloonHitCount", TotalBalloonHitCount);
		}
		public void Set(SBattleRecord Obj_)
		{
			TotalKillCount = Obj_.TotalKillCount;
			TotalBalloonHitCount = Obj_.TotalBalloonHitCount;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(TotalKillCount) + "," + 
				SEnumChecker.GetStdName(TotalBalloonHitCount);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(TotalKillCount, "TotalKillCount") + "," + 
				SEnumChecker.GetMemberName(TotalBalloonHitCount, "TotalBalloonHitCount");
		}
	}
	public class SBattleJoinNetCs : SProto
	{
		public SBattleType BattleType = new SBattleType();
		public SBattleJoinNetCs()
		{
		}
		public SBattleJoinNetCs(SBattleJoinNetCs Obj_)
		{
			BattleType = Obj_.BattleType;
		}
		public SBattleJoinNetCs(SBattleType BattleType_)
		{
			BattleType = BattleType_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref BattleType);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("BattleType", ref BattleType);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(BattleType);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("BattleType", BattleType);
		}
		public void Set(SBattleJoinNetCs Obj_)
		{
			BattleType.Set(Obj_.BattleType);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(BattleType);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(BattleType, "BattleType");
		}
	}
	public class SBattleJoinNetSc : SProto
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
	public class SBattleOutNetCs : SProto
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
	public class SBattleOutNetSc : SProto
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
	public class SPumpInfo : SProto
	{
		public SByte Count = default(SByte);
		public SByte CountTo = default(SByte);
		public Single Scale = default(Single);
		public SPumpInfo()
		{
		}
		public SPumpInfo(SPumpInfo Obj_)
		{
			Count = Obj_.Count;
			CountTo = Obj_.CountTo;
			Scale = Obj_.Scale;
		}
		public SPumpInfo(SByte Count_, SByte CountTo_, Single Scale_)
		{
			Count = Count_;
			CountTo = CountTo_;
			Scale = Scale_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Count);
			Stream_.Pop(ref CountTo);
			Stream_.Pop(ref Scale);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Count", ref Count);
			Value_.Pop("CountTo", ref CountTo);
			Value_.Pop("Scale", ref Scale);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Count);
			Stream_.Push(CountTo);
			Stream_.Push(Scale);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Count", Count);
			Value_.Push("CountTo", CountTo);
			Value_.Push("Scale", Scale);
		}
		public void Set(SPumpInfo Obj_)
		{
			Count = Obj_.Count;
			CountTo = Obj_.CountTo;
			Scale = Obj_.Scale;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Count) + "," + 
				SEnumChecker.GetStdName(CountTo) + "," + 
				SEnumChecker.GetStdName(Scale);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Count, "Count") + "," + 
				SEnumChecker.GetMemberName(CountTo, "CountTo") + "," + 
				SEnumChecker.GetMemberName(Scale, "Scale");
		}
	}
	public class SParachuteInfo : SProto
	{
		public Single Scale = default(Single);
		public Single Velocity = default(Single);
		public SParachuteInfo()
		{
		}
		public SParachuteInfo(SParachuteInfo Obj_)
		{
			Scale = Obj_.Scale;
			Velocity = Obj_.Velocity;
		}
		public SParachuteInfo(Single Scale_, Single Velocity_)
		{
			Scale = Scale_;
			Velocity = Velocity_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Scale);
			Stream_.Pop(ref Velocity);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Scale", ref Scale);
			Value_.Pop("Velocity", ref Velocity);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Scale);
			Stream_.Push(Velocity);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Scale", Scale);
			Value_.Push("Velocity", Velocity);
		}
		public void Set(SParachuteInfo Obj_)
		{
			Scale = Obj_.Scale;
			Velocity = Obj_.Velocity;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Scale) + "," + 
				SEnumChecker.GetStdName(Velocity);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Scale, "Scale") + "," + 
				SEnumChecker.GetMemberName(Velocity, "Velocity");
		}
	}
	public class SStaminaInfo : SProto
	{
		public Int64 LastUsedTick = default(Int64);
		public Single Stamina = default(Single);
		public SStaminaInfo()
		{
		}
		public SStaminaInfo(SStaminaInfo Obj_)
		{
			LastUsedTick = Obj_.LastUsedTick;
			Stamina = Obj_.Stamina;
		}
		public SStaminaInfo(Int64 LastUsedTick_, Single Stamina_)
		{
			LastUsedTick = LastUsedTick_;
			Stamina = Stamina_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref LastUsedTick);
			Stream_.Pop(ref Stamina);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("LastUsedTick", ref LastUsedTick);
			Value_.Pop("Stamina", ref Stamina);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(LastUsedTick);
			Stream_.Push(Stamina);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("LastUsedTick", LastUsedTick);
			Value_.Push("Stamina", Stamina);
		}
		public void Set(SStaminaInfo Obj_)
		{
			LastUsedTick = Obj_.LastUsedTick;
			Stamina = Obj_.Stamina;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(LastUsedTick) + "," + 
				SEnumChecker.GetStdName(Stamina);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(LastUsedTick, "LastUsedTick") + "," + 
				SEnumChecker.GetMemberName(Stamina, "Stamina");
		}
	}
	public class SBattlePlayer : SProto
	{
		public TUID UID = default(TUID);
		public String Nick = string.Empty;
		public String CountryCode = string.Empty;
		public TTeamCnt TeamIndex = default(TTeamCnt);
		public Int32 CharCode = default(Int32);
		public Int32 Point = default(Int32);
		public SBattlePlayer()
		{
		}
		public SBattlePlayer(SBattlePlayer Obj_)
		{
			UID = Obj_.UID;
			Nick = Obj_.Nick;
			CountryCode = Obj_.CountryCode;
			TeamIndex = Obj_.TeamIndex;
			CharCode = Obj_.CharCode;
			Point = Obj_.Point;
		}
		public SBattlePlayer(TUID UID_, String Nick_, String CountryCode_, TTeamCnt TeamIndex_, Int32 CharCode_, Int32 Point_)
		{
			UID = UID_;
			Nick = Nick_;
			CountryCode = CountryCode_;
			TeamIndex = TeamIndex_;
			CharCode = CharCode_;
			Point = Point_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref UID);
			Stream_.Pop(ref Nick);
			Stream_.Pop(ref CountryCode);
			Stream_.Pop(ref TeamIndex);
			Stream_.Pop(ref CharCode);
			Stream_.Pop(ref Point);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("UID", ref UID);
			Value_.Pop("Nick", ref Nick);
			Value_.Pop("CountryCode", ref CountryCode);
			Value_.Pop("TeamIndex", ref TeamIndex);
			Value_.Pop("CharCode", ref CharCode);
			Value_.Pop("Point", ref Point);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(UID);
			Stream_.Push(Nick);
			Stream_.Push(CountryCode);
			Stream_.Push(TeamIndex);
			Stream_.Push(CharCode);
			Stream_.Push(Point);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("UID", UID);
			Value_.Push("Nick", Nick);
			Value_.Push("CountryCode", CountryCode);
			Value_.Push("TeamIndex", TeamIndex);
			Value_.Push("CharCode", CharCode);
			Value_.Push("Point", Point);
		}
		public void Set(SBattlePlayer Obj_)
		{
			UID = Obj_.UID;
			Nick = Obj_.Nick;
			CountryCode = Obj_.CountryCode;
			TeamIndex = Obj_.TeamIndex;
			CharCode = Obj_.CharCode;
			Point = Obj_.Point;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(UID) + "," + 
				SEnumChecker.GetStdName(Nick) + "," + 
				SEnumChecker.GetStdName(CountryCode) + "," + 
				SEnumChecker.GetStdName(TeamIndex) + "," + 
				SEnumChecker.GetStdName(CharCode) + "," + 
				SEnumChecker.GetStdName(Point);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(UID, "UID") + "," + 
				SEnumChecker.GetMemberName(Nick, "Nick") + "," + 
				SEnumChecker.GetMemberName(CountryCode, "CountryCode") + "," + 
				SEnumChecker.GetMemberName(TeamIndex, "TeamIndex") + "," + 
				SEnumChecker.GetMemberName(CharCode, "CharCode") + "," + 
				SEnumChecker.GetMemberName(Point, "Point");
		}
	}
	public class SCharacter : SProto
	{
		public Boolean IsGround = default(Boolean);
		public SByte Dir = default(SByte);
		public SByte BalloonCount = default(SByte);
		public SPumpInfo PumpInfo = new SPumpInfo();
		public SParachuteInfo ParachuteInfo = new SParachuteInfo();
		public SStaminaInfo StaminaInfo = new SStaminaInfo();
		public SByte Face = default(SByte);
		public SPoint Acc = new SPoint();
		public Int64 InvulnerableEndTick = default(Int64);
		public Int32 ChainKillCount = default(Int32);
		public Int64 LastKillTick = default(Int64);
		public Int64 RegenTick = default(Int64);
		public SCharacter()
		{
		}
		public SCharacter(SCharacter Obj_)
		{
			IsGround = Obj_.IsGround;
			Dir = Obj_.Dir;
			BalloonCount = Obj_.BalloonCount;
			PumpInfo = Obj_.PumpInfo;
			ParachuteInfo = Obj_.ParachuteInfo;
			StaminaInfo = Obj_.StaminaInfo;
			Face = Obj_.Face;
			Acc = Obj_.Acc;
			InvulnerableEndTick = Obj_.InvulnerableEndTick;
			ChainKillCount = Obj_.ChainKillCount;
			LastKillTick = Obj_.LastKillTick;
			RegenTick = Obj_.RegenTick;
		}
		public SCharacter(Boolean IsGround_, SByte Dir_, SByte BalloonCount_, SPumpInfo PumpInfo_, SParachuteInfo ParachuteInfo_, SStaminaInfo StaminaInfo_, SByte Face_, SPoint Acc_, Int64 InvulnerableEndTick_, Int32 ChainKillCount_, Int64 LastKillTick_, Int64 RegenTick_)
		{
			IsGround = IsGround_;
			Dir = Dir_;
			BalloonCount = BalloonCount_;
			PumpInfo = PumpInfo_;
			ParachuteInfo = ParachuteInfo_;
			StaminaInfo = StaminaInfo_;
			Face = Face_;
			Acc = Acc_;
			InvulnerableEndTick = InvulnerableEndTick_;
			ChainKillCount = ChainKillCount_;
			LastKillTick = LastKillTick_;
			RegenTick = RegenTick_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref IsGround);
			Stream_.Pop(ref Dir);
			Stream_.Pop(ref BalloonCount);
			Stream_.Pop(ref PumpInfo);
			Stream_.Pop(ref ParachuteInfo);
			Stream_.Pop(ref StaminaInfo);
			Stream_.Pop(ref Face);
			Stream_.Pop(ref Acc);
			Stream_.Pop(ref InvulnerableEndTick);
			Stream_.Pop(ref ChainKillCount);
			Stream_.Pop(ref LastKillTick);
			Stream_.Pop(ref RegenTick);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("IsGround", ref IsGround);
			Value_.Pop("Dir", ref Dir);
			Value_.Pop("BalloonCount", ref BalloonCount);
			Value_.Pop("PumpInfo", ref PumpInfo);
			Value_.Pop("ParachuteInfo", ref ParachuteInfo);
			Value_.Pop("StaminaInfo", ref StaminaInfo);
			Value_.Pop("Face", ref Face);
			Value_.Pop("Acc", ref Acc);
			Value_.Pop("InvulnerableEndTick", ref InvulnerableEndTick);
			Value_.Pop("ChainKillCount", ref ChainKillCount);
			Value_.Pop("LastKillTick", ref LastKillTick);
			Value_.Pop("RegenTick", ref RegenTick);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(IsGround);
			Stream_.Push(Dir);
			Stream_.Push(BalloonCount);
			Stream_.Push(PumpInfo);
			Stream_.Push(ParachuteInfo);
			Stream_.Push(StaminaInfo);
			Stream_.Push(Face);
			Stream_.Push(Acc);
			Stream_.Push(InvulnerableEndTick);
			Stream_.Push(ChainKillCount);
			Stream_.Push(LastKillTick);
			Stream_.Push(RegenTick);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("IsGround", IsGround);
			Value_.Push("Dir", Dir);
			Value_.Push("BalloonCount", BalloonCount);
			Value_.Push("PumpInfo", PumpInfo);
			Value_.Push("ParachuteInfo", ParachuteInfo);
			Value_.Push("StaminaInfo", StaminaInfo);
			Value_.Push("Face", Face);
			Value_.Push("Acc", Acc);
			Value_.Push("InvulnerableEndTick", InvulnerableEndTick);
			Value_.Push("ChainKillCount", ChainKillCount);
			Value_.Push("LastKillTick", LastKillTick);
			Value_.Push("RegenTick", RegenTick);
		}
		public void Set(SCharacter Obj_)
		{
			IsGround = Obj_.IsGround;
			Dir = Obj_.Dir;
			BalloonCount = Obj_.BalloonCount;
			PumpInfo.Set(Obj_.PumpInfo);
			ParachuteInfo.Set(Obj_.ParachuteInfo);
			StaminaInfo.Set(Obj_.StaminaInfo);
			Face = Obj_.Face;
			Acc.Set(Obj_.Acc);
			InvulnerableEndTick = Obj_.InvulnerableEndTick;
			ChainKillCount = Obj_.ChainKillCount;
			LastKillTick = Obj_.LastKillTick;
			RegenTick = Obj_.RegenTick;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(IsGround) + "," + 
				SEnumChecker.GetStdName(Dir) + "," + 
				SEnumChecker.GetStdName(BalloonCount) + "," + 
				SEnumChecker.GetStdName(PumpInfo) + "," + 
				SEnumChecker.GetStdName(ParachuteInfo) + "," + 
				SEnumChecker.GetStdName(StaminaInfo) + "," + 
				SEnumChecker.GetStdName(Face) + "," + 
				SEnumChecker.GetStdName(Acc) + "," + 
				SEnumChecker.GetStdName(InvulnerableEndTick) + "," + 
				SEnumChecker.GetStdName(ChainKillCount) + "," + 
				SEnumChecker.GetStdName(LastKillTick) + "," + 
				SEnumChecker.GetStdName(RegenTick);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(IsGround, "IsGround") + "," + 
				SEnumChecker.GetMemberName(Dir, "Dir") + "," + 
				SEnumChecker.GetMemberName(BalloonCount, "BalloonCount") + "," + 
				SEnumChecker.GetMemberName(PumpInfo, "PumpInfo") + "," + 
				SEnumChecker.GetMemberName(ParachuteInfo, "ParachuteInfo") + "," + 
				SEnumChecker.GetMemberName(StaminaInfo, "StaminaInfo") + "," + 
				SEnumChecker.GetMemberName(Face, "Face") + "," + 
				SEnumChecker.GetMemberName(Acc, "Acc") + "," + 
				SEnumChecker.GetMemberName(InvulnerableEndTick, "InvulnerableEndTick") + "," + 
				SEnumChecker.GetMemberName(ChainKillCount, "ChainKillCount") + "," + 
				SEnumChecker.GetMemberName(LastKillTick, "LastKillTick") + "," + 
				SEnumChecker.GetMemberName(RegenTick, "RegenTick");
		}
	}
	public class SCharacterNet : SCharacter
	{
		public SPoint Pos = new SPoint();
		public SPoint Vel = new SPoint();
		public SCharacterNet()
		{
		}
		public SCharacterNet(SCharacterNet Obj_) : base(Obj_)
		{
			Pos = Obj_.Pos;
			Vel = Obj_.Vel;
		}
		public SCharacterNet(SCharacter Super_, SPoint Pos_, SPoint Vel_) : base(Super_)
		{
			Pos = Pos_;
			Vel = Vel_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref Pos);
			Stream_.Pop(ref Vel);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("Pos", ref Pos);
			Value_.Pop("Vel", ref Vel);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(Pos);
			Stream_.Push(Vel);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("Pos", Pos);
			Value_.Push("Vel", Vel);
		}
		public void Set(SCharacterNet Obj_)
		{
			base.Set(Obj_);
			Pos.Set(Obj_.Pos);
			Vel.Set(Obj_.Vel);
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(Pos) + "," + 
				SEnumChecker.GetStdName(Vel);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(Pos, "Pos") + "," + 
				SEnumChecker.GetMemberName(Vel, "Vel");
		}
	}
	public class STeamBattleInfo : SProto
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
	public class SBattle : SProto
	{
		public SBattleType BattleType = new SBattleType();
		public Int32 MapIndex = default(Int32);
		public SBattle()
		{
		}
		public SBattle(SBattle Obj_)
		{
			BattleType = Obj_.BattleType;
			MapIndex = Obj_.MapIndex;
		}
		public SBattle(SBattleType BattleType_, Int32 MapIndex_)
		{
			BattleType = BattleType_;
			MapIndex = MapIndex_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref BattleType);
			Stream_.Pop(ref MapIndex);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("BattleType", ref BattleType);
			Value_.Pop("MapIndex", ref MapIndex);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(BattleType);
			Stream_.Push(MapIndex);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("BattleType", BattleType);
			Value_.Push("MapIndex", MapIndex);
		}
		public void Set(SBattle Obj_)
		{
			BattleType.Set(Obj_.BattleType);
			MapIndex = Obj_.MapIndex;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(BattleType) + "," + 
				SEnumChecker.GetStdName(MapIndex);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(BattleType, "BattleType") + "," + 
				SEnumChecker.GetMemberName(MapIndex, "MapIndex");
		}
	}
	public class SBattleBeginNetSc : SBattle
	{
		public List<SBattlePlayer> Players = new List<SBattlePlayer>();
		public SBattleRecord Record = new SBattleRecord();
		public List<SCharacterNet> Characters = new List<SCharacterNet>();
		public TimePoint EndTime = default(TimePoint);
		public Int64 Tick = default(Int64);
		public List<SStructMovePosition> StructMovePositions = new List<SStructMovePosition>();
		public SBattleBeginNetSc()
		{
		}
		public SBattleBeginNetSc(SBattleBeginNetSc Obj_) : base(Obj_)
		{
			Players = Obj_.Players;
			Record = Obj_.Record;
			Characters = Obj_.Characters;
			EndTime = Obj_.EndTime;
			Tick = Obj_.Tick;
			StructMovePositions = Obj_.StructMovePositions;
		}
		public SBattleBeginNetSc(SBattle Super_, List<SBattlePlayer> Players_, SBattleRecord Record_, List<SCharacterNet> Characters_, TimePoint EndTime_, Int64 Tick_, List<SStructMovePosition> StructMovePositions_) : base(Super_)
		{
			Players = Players_;
			Record = Record_;
			Characters = Characters_;
			EndTime = EndTime_;
			Tick = Tick_;
			StructMovePositions = StructMovePositions_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref Players);
			Stream_.Pop(ref Record);
			Stream_.Pop(ref Characters);
			Stream_.Pop(ref EndTime);
			Stream_.Pop(ref Tick);
			Stream_.Pop(ref StructMovePositions);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("Players", ref Players);
			Value_.Pop("Record", ref Record);
			Value_.Pop("Characters", ref Characters);
			Value_.Pop("EndTime", ref EndTime);
			Value_.Pop("Tick", ref Tick);
			Value_.Pop("StructMovePositions", ref StructMovePositions);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(Players);
			Stream_.Push(Record);
			Stream_.Push(Characters);
			Stream_.Push(EndTime);
			Stream_.Push(Tick);
			Stream_.Push(StructMovePositions);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("Players", Players);
			Value_.Push("Record", Record);
			Value_.Push("Characters", Characters);
			Value_.Push("EndTime", EndTime);
			Value_.Push("Tick", Tick);
			Value_.Push("StructMovePositions", StructMovePositions);
		}
		public void Set(SBattleBeginNetSc Obj_)
		{
			base.Set(Obj_);
			Players = Obj_.Players;
			Record.Set(Obj_.Record);
			Characters = Obj_.Characters;
			EndTime = Obj_.EndTime;
			Tick = Obj_.Tick;
			StructMovePositions = Obj_.StructMovePositions;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(Players) + "," + 
				SEnumChecker.GetStdName(Record) + "," + 
				SEnumChecker.GetStdName(Characters) + "," + 
				SEnumChecker.GetStdName(EndTime) + "," + 
				SEnumChecker.GetStdName(Tick) + "," + 
				SEnumChecker.GetStdName(StructMovePositions);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(Players, "Players") + "," + 
				SEnumChecker.GetMemberName(Record, "Record") + "," + 
				SEnumChecker.GetMemberName(Characters, "Characters") + "," + 
				SEnumChecker.GetMemberName(EndTime, "EndTime") + "," + 
				SEnumChecker.GetMemberName(Tick, "Tick") + "," + 
				SEnumChecker.GetMemberName(StructMovePositions, "StructMovePositions");
		}
	}
	public class SBattleMatchingNetSc : SProto
	{
		public Int32 UserCount = default(Int32);
		public SBattleMatchingNetSc()
		{
		}
		public SBattleMatchingNetSc(SBattleMatchingNetSc Obj_)
		{
			UserCount = Obj_.UserCount;
		}
		public SBattleMatchingNetSc(Int32 UserCount_)
		{
			UserCount = UserCount_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref UserCount);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("UserCount", ref UserCount);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(UserCount);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("UserCount", UserCount);
		}
		public void Set(SBattleMatchingNetSc Obj_)
		{
			UserCount = Obj_.UserCount;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(UserCount);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(UserCount, "UserCount");
		}
	}
	public class SBattleStartNetSc : SProto
	{
		public TimePoint EndTime = default(TimePoint);
		public SBattleStartNetSc()
		{
		}
		public SBattleStartNetSc(SBattleStartNetSc Obj_)
		{
			EndTime = Obj_.EndTime;
		}
		public SBattleStartNetSc(TimePoint EndTime_)
		{
			EndTime = EndTime_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref EndTime);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("EndTime", ref EndTime);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(EndTime);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("EndTime", EndTime);
		}
		public void Set(SBattleStartNetSc Obj_)
		{
			EndTime = Obj_.EndTime;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(EndTime);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(EndTime, "EndTime");
		}
	}
	public class SSingleBattleStartNetSc : SProto
	{
		public TimePoint EndTime = default(TimePoint);
		public SSingleBattleStartNetSc()
		{
		}
		public SSingleBattleStartNetSc(SSingleBattleStartNetSc Obj_)
		{
			EndTime = Obj_.EndTime;
		}
		public SSingleBattleStartNetSc(TimePoint EndTime_)
		{
			EndTime = EndTime_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref EndTime);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("EndTime", ref EndTime);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(EndTime);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("EndTime", EndTime);
		}
		public void Set(SSingleBattleStartNetSc Obj_)
		{
			EndTime = Obj_.EndTime;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(EndTime);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(EndTime, "EndTime");
		}
	}
	public class SSingleBattleIconNetCs : SProto
	{
		public Int32 Code = default(Int32);
		public SSingleBattleIconNetCs()
		{
		}
		public SSingleBattleIconNetCs(SSingleBattleIconNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public SSingleBattleIconNetCs(Int32 Code_)
		{
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
		}
		public void Set(SSingleBattleIconNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SSingleBattleIconNetSc : SProto
	{
		public Int32 PlayerIndex = default(Int32);
		public Int32 Code = default(Int32);
		public SSingleBattleIconNetSc()
		{
		}
		public SSingleBattleIconNetSc(SSingleBattleIconNetSc Obj_)
		{
			PlayerIndex = Obj_.PlayerIndex;
			Code = Obj_.Code;
		}
		public SSingleBattleIconNetSc(Int32 PlayerIndex_, Int32 Code_)
		{
			PlayerIndex = PlayerIndex_;
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref PlayerIndex);
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("PlayerIndex", ref PlayerIndex);
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(PlayerIndex);
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("PlayerIndex", PlayerIndex);
			Value_.Push("Code", Code);
		}
		public void Set(SSingleBattleIconNetSc Obj_)
		{
			PlayerIndex = Obj_.PlayerIndex;
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(PlayerIndex) + "," + 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(PlayerIndex, "PlayerIndex") + "," + 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SSingleBattleItemNetCs : SProto
	{
		public Int32 Code = default(Int32);
		public SSingleBattleItemNetCs()
		{
		}
		public SSingleBattleItemNetCs(SSingleBattleItemNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public SSingleBattleItemNetCs(Int32 Code_)
		{
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
		}
		public void Set(SSingleBattleItemNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SSingleBattleItemNetSc : SProto
	{
		public Int32 PlayerIndex = default(Int32);
		public Int32 Code = default(Int32);
		public SSingleBattleItemNetSc()
		{
		}
		public SSingleBattleItemNetSc(SSingleBattleItemNetSc Obj_)
		{
			PlayerIndex = Obj_.PlayerIndex;
			Code = Obj_.Code;
		}
		public SSingleBattleItemNetSc(Int32 PlayerIndex_, Int32 Code_)
		{
			PlayerIndex = PlayerIndex_;
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref PlayerIndex);
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("PlayerIndex", ref PlayerIndex);
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(PlayerIndex);
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("PlayerIndex", PlayerIndex);
			Value_.Push("Code", Code);
		}
		public void Set(SSingleBattleItemNetSc Obj_)
		{
			PlayerIndex = Obj_.PlayerIndex;
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(PlayerIndex) + "," + 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(PlayerIndex, "PlayerIndex") + "," + 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SBattleEndPlayer : SProto
	{
		public Int32 AddedPoint = default(Int32);
		public Int32 AddedGold = default(Int32);
		public Int32 BattlePoint = default(Int32);
		public SBattleEndPlayer()
		{
		}
		public SBattleEndPlayer(SBattleEndPlayer Obj_)
		{
			AddedPoint = Obj_.AddedPoint;
			AddedGold = Obj_.AddedGold;
			BattlePoint = Obj_.BattlePoint;
		}
		public SBattleEndPlayer(Int32 AddedPoint_, Int32 AddedGold_, Int32 BattlePoint_)
		{
			AddedPoint = AddedPoint_;
			AddedGold = AddedGold_;
			BattlePoint = BattlePoint_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref AddedPoint);
			Stream_.Pop(ref AddedGold);
			Stream_.Pop(ref BattlePoint);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("AddedPoint", ref AddedPoint);
			Value_.Pop("AddedGold", ref AddedGold);
			Value_.Pop("BattlePoint", ref BattlePoint);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(AddedPoint);
			Stream_.Push(AddedGold);
			Stream_.Push(BattlePoint);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("AddedPoint", AddedPoint);
			Value_.Push("AddedGold", AddedGold);
			Value_.Push("BattlePoint", BattlePoint);
		}
		public void Set(SBattleEndPlayer Obj_)
		{
			AddedPoint = Obj_.AddedPoint;
			AddedGold = Obj_.AddedGold;
			BattlePoint = Obj_.BattlePoint;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(AddedPoint) + "," + 
				SEnumChecker.GetStdName(AddedGold) + "," + 
				SEnumChecker.GetStdName(BattlePoint);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(AddedPoint, "AddedPoint") + "," + 
				SEnumChecker.GetMemberName(AddedGold, "AddedGold") + "," + 
				SEnumChecker.GetMemberName(BattlePoint, "BattlePoint");
		}
	}
	public class SBattleEndNetSc : SProto
	{
		public List<SBattleEndPlayer> BattleEndPlayers = new List<SBattleEndPlayer>();
		public TTeamBattleInfos TeamBattleInfos = new TTeamBattleInfos();
		public TDoneQuests DoneQuests = new TDoneQuests();
		public SBattleEndNetSc()
		{
		}
		public SBattleEndNetSc(SBattleEndNetSc Obj_)
		{
			BattleEndPlayers = Obj_.BattleEndPlayers;
			TeamBattleInfos = Obj_.TeamBattleInfos;
			DoneQuests = Obj_.DoneQuests;
		}
		public SBattleEndNetSc(List<SBattleEndPlayer> BattleEndPlayers_, TTeamBattleInfos TeamBattleInfos_, TDoneQuests DoneQuests_)
		{
			BattleEndPlayers = BattleEndPlayers_;
			TeamBattleInfos = TeamBattleInfos_;
			DoneQuests = DoneQuests_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref BattleEndPlayers);
			Stream_.Pop(ref TeamBattleInfos);
			Stream_.Pop(ref DoneQuests);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("BattleEndPlayers", ref BattleEndPlayers);
			Value_.Pop("TeamBattleInfos", ref TeamBattleInfos);
			Value_.Pop("DoneQuests", ref DoneQuests);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(BattleEndPlayers);
			Stream_.Push(TeamBattleInfos);
			Stream_.Push(DoneQuests);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("BattleEndPlayers", BattleEndPlayers);
			Value_.Push("TeamBattleInfos", TeamBattleInfos);
			Value_.Push("DoneQuests", DoneQuests);
		}
		public void Set(SBattleEndNetSc Obj_)
		{
			BattleEndPlayers = Obj_.BattleEndPlayers;
			TeamBattleInfos = Obj_.TeamBattleInfos;
			DoneQuests = Obj_.DoneQuests;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(BattleEndPlayers) + "," + 
				SEnumChecker.GetStdName(TeamBattleInfos) + "," + 
				SEnumChecker.GetStdName(DoneQuests);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(BattleEndPlayers, "BattleEndPlayers") + "," + 
				SEnumChecker.GetMemberName(TeamBattleInfos, "TeamBattleInfos") + "," + 
				SEnumChecker.GetMemberName(DoneQuests, "DoneQuests");
		}
	}
	public class SSingleBattleEndNetSc : SBattleEndNetSc
	{
		public EGameMode GameMode = default(EGameMode);
		public SSingleBattleEndNetSc()
		{
		}
		public SSingleBattleEndNetSc(SSingleBattleEndNetSc Obj_) : base(Obj_)
		{
			GameMode = Obj_.GameMode;
		}
		public SSingleBattleEndNetSc(SBattleEndNetSc Super_, EGameMode GameMode_) : base(Super_)
		{
			GameMode = GameMode_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref GameMode);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("GameMode", ref GameMode);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(GameMode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("GameMode", GameMode);
		}
		public void Set(SSingleBattleEndNetSc Obj_)
		{
			base.Set(Obj_);
			GameMode = Obj_.GameMode;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				"bb.EGameMode";
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(GameMode, "GameMode");
		}
	}
	public class SBattleSyncNetSc : SProto
	{
		public Int64 Tick = default(Int64);
		public SBattleSyncNetSc()
		{
		}
		public SBattleSyncNetSc(SBattleSyncNetSc Obj_)
		{
			Tick = Obj_.Tick;
		}
		public SBattleSyncNetSc(Int64 Tick_)
		{
			Tick = Tick_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Tick);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Tick", ref Tick);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Tick);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Tick", Tick);
		}
		public void Set(SBattleSyncNetSc Obj_)
		{
			Tick = Obj_.Tick;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Tick);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Tick, "Tick");
		}
	}
	public class SBattleTouchNetCs : SProto
	{
		public SByte Dir = default(SByte);
		public SBattleTouchNetCs()
		{
		}
		public SBattleTouchNetCs(SBattleTouchNetCs Obj_)
		{
			Dir = Obj_.Dir;
		}
		public SBattleTouchNetCs(SByte Dir_)
		{
			Dir = Dir_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Dir);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Dir", ref Dir);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Dir);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Dir", Dir);
		}
		public void Set(SBattleTouchNetCs Obj_)
		{
			Dir = Obj_.Dir;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Dir);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Dir, "Dir");
		}
	}
	public class SBattleTouchNetSc : SProto
	{
		public Int64 Tick = default(Int64);
		public Int32 PlayerIndex = default(Int32);
		public SByte Dir = default(SByte);
		public SBattleTouchNetSc()
		{
		}
		public SBattleTouchNetSc(SBattleTouchNetSc Obj_)
		{
			Tick = Obj_.Tick;
			PlayerIndex = Obj_.PlayerIndex;
			Dir = Obj_.Dir;
		}
		public SBattleTouchNetSc(Int64 Tick_, Int32 PlayerIndex_, SByte Dir_)
		{
			Tick = Tick_;
			PlayerIndex = PlayerIndex_;
			Dir = Dir_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Tick);
			Stream_.Pop(ref PlayerIndex);
			Stream_.Pop(ref Dir);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Tick", ref Tick);
			Value_.Pop("PlayerIndex", ref PlayerIndex);
			Value_.Pop("Dir", ref Dir);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Tick);
			Stream_.Push(PlayerIndex);
			Stream_.Push(Dir);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Tick", Tick);
			Value_.Push("PlayerIndex", PlayerIndex);
			Value_.Push("Dir", Dir);
		}
		public void Set(SBattleTouchNetSc Obj_)
		{
			Tick = Obj_.Tick;
			PlayerIndex = Obj_.PlayerIndex;
			Dir = Obj_.Dir;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Tick) + "," + 
				SEnumChecker.GetStdName(PlayerIndex) + "," + 
				SEnumChecker.GetStdName(Dir);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Tick, "Tick") + "," + 
				SEnumChecker.GetMemberName(PlayerIndex, "PlayerIndex") + "," + 
				SEnumChecker.GetMemberName(Dir, "Dir");
		}
	}
	public class SBattlePushNetCs : SProto
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
	public class SBattlePushNetSc : SProto
	{
		public Int64 Tick = default(Int64);
		public Int32 PlayerIndex = default(Int32);
		public SBattlePushNetSc()
		{
		}
		public SBattlePushNetSc(SBattlePushNetSc Obj_)
		{
			Tick = Obj_.Tick;
			PlayerIndex = Obj_.PlayerIndex;
		}
		public SBattlePushNetSc(Int64 Tick_, Int32 PlayerIndex_)
		{
			Tick = Tick_;
			PlayerIndex = PlayerIndex_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Tick);
			Stream_.Pop(ref PlayerIndex);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Tick", ref Tick);
			Value_.Pop("PlayerIndex", ref PlayerIndex);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Tick);
			Stream_.Push(PlayerIndex);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Tick", Tick);
			Value_.Push("PlayerIndex", PlayerIndex);
		}
		public void Set(SBattlePushNetSc Obj_)
		{
			Tick = Obj_.Tick;
			PlayerIndex = Obj_.PlayerIndex;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Tick) + "," + 
				SEnumChecker.GetStdName(PlayerIndex);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Tick, "Tick") + "," + 
				SEnumChecker.GetMemberName(PlayerIndex, "PlayerIndex");
		}
	}
	public class SBattleIconNetCs : SProto
	{
		public Int32 Code = default(Int32);
		public SBattleIconNetCs()
		{
		}
		public SBattleIconNetCs(SBattleIconNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public SBattleIconNetCs(Int32 Code_)
		{
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
		}
		public void Set(SBattleIconNetCs Obj_)
		{
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SBattleIconNetSc : SProto
	{
		public Int32 PlayerIndex = default(Int32);
		public Int32 Code = default(Int32);
		public SBattleIconNetSc()
		{
		}
		public SBattleIconNetSc(SBattleIconNetSc Obj_)
		{
			PlayerIndex = Obj_.PlayerIndex;
			Code = Obj_.Code;
		}
		public SBattleIconNetSc(Int32 PlayerIndex_, Int32 Code_)
		{
			PlayerIndex = PlayerIndex_;
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref PlayerIndex);
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("PlayerIndex", ref PlayerIndex);
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(PlayerIndex);
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("PlayerIndex", PlayerIndex);
			Value_.Push("Code", Code);
		}
		public void Set(SBattleIconNetSc Obj_)
		{
			PlayerIndex = Obj_.PlayerIndex;
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(PlayerIndex) + "," + 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(PlayerIndex, "PlayerIndex") + "," + 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SSingleBattleScoreNetCs : SProto
	{
		public Int32 Score = default(Int32);
		public SSingleBattleScoreNetCs()
		{
		}
		public SSingleBattleScoreNetCs(SSingleBattleScoreNetCs Obj_)
		{
			Score = Obj_.Score;
		}
		public SSingleBattleScoreNetCs(Int32 Score_)
		{
			Score = Score_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Score);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Score", ref Score);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Score);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Score", Score);
		}
		public void Set(SSingleBattleScoreNetCs Obj_)
		{
			Score = Obj_.Score;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Score);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Score, "Score");
		}
	}
	public class SSingleBattleScoreNetSc : SProto
	{
		public Int32 PlayerIndex = default(Int32);
		public Int32 Score = default(Int32);
		public SSingleBattleScoreNetSc()
		{
		}
		public SSingleBattleScoreNetSc(SSingleBattleScoreNetSc Obj_)
		{
			PlayerIndex = Obj_.PlayerIndex;
			Score = Obj_.Score;
		}
		public SSingleBattleScoreNetSc(Int32 PlayerIndex_, Int32 Score_)
		{
			PlayerIndex = PlayerIndex_;
			Score = Score_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref PlayerIndex);
			Stream_.Pop(ref Score);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("PlayerIndex", ref PlayerIndex);
			Value_.Pop("Score", ref Score);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(PlayerIndex);
			Stream_.Push(Score);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("PlayerIndex", PlayerIndex);
			Value_.Push("Score", Score);
		}
		public void Set(SSingleBattleScoreNetSc Obj_)
		{
			PlayerIndex = Obj_.PlayerIndex;
			Score = Obj_.Score;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(PlayerIndex) + "," + 
				SEnumChecker.GetStdName(Score);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(PlayerIndex, "PlayerIndex") + "," + 
				SEnumChecker.GetMemberName(Score, "Score");
		}
	}
	public class SBattleLinkNetSc : SProto
	{
		public Int64 Tick = default(Int64);
		public Int32 PlayerIndex = default(Int32);
		public SBattleLinkNetSc()
		{
		}
		public SBattleLinkNetSc(SBattleLinkNetSc Obj_)
		{
			Tick = Obj_.Tick;
			PlayerIndex = Obj_.PlayerIndex;
		}
		public SBattleLinkNetSc(Int64 Tick_, Int32 PlayerIndex_)
		{
			Tick = Tick_;
			PlayerIndex = PlayerIndex_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Tick);
			Stream_.Pop(ref PlayerIndex);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Tick", ref Tick);
			Value_.Pop("PlayerIndex", ref PlayerIndex);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Tick);
			Stream_.Push(PlayerIndex);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Tick", Tick);
			Value_.Push("PlayerIndex", PlayerIndex);
		}
		public void Set(SBattleLinkNetSc Obj_)
		{
			Tick = Obj_.Tick;
			PlayerIndex = Obj_.PlayerIndex;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Tick) + "," + 
				SEnumChecker.GetStdName(PlayerIndex);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Tick, "Tick") + "," + 
				SEnumChecker.GetMemberName(PlayerIndex, "PlayerIndex");
		}
	}
	public class SBattleUnLinkNetSc : SProto
	{
		public Int64 Tick = default(Int64);
		public Int32 PlayerIndex = default(Int32);
		public SBattleUnLinkNetSc()
		{
		}
		public SBattleUnLinkNetSc(SBattleUnLinkNetSc Obj_)
		{
			Tick = Obj_.Tick;
			PlayerIndex = Obj_.PlayerIndex;
		}
		public SBattleUnLinkNetSc(Int64 Tick_, Int32 PlayerIndex_)
		{
			Tick = Tick_;
			PlayerIndex = PlayerIndex_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Tick);
			Stream_.Pop(ref PlayerIndex);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Tick", ref Tick);
			Value_.Pop("PlayerIndex", ref PlayerIndex);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Tick);
			Stream_.Push(PlayerIndex);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Tick", Tick);
			Value_.Push("PlayerIndex", PlayerIndex);
		}
		public void Set(SBattleUnLinkNetSc Obj_)
		{
			Tick = Obj_.Tick;
			PlayerIndex = Obj_.PlayerIndex;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Tick) + "," + 
				SEnumChecker.GetStdName(PlayerIndex);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Tick, "Tick") + "," + 
				SEnumChecker.GetMemberName(PlayerIndex, "PlayerIndex");
		}
	}
	public class SRankingUserCore : SProto
	{
		public String Nick = string.Empty;
		public Int32 CharCode = default(Int32);
		public String CountryCode = string.Empty;
		public SRankingUserCore()
		{
		}
		public SRankingUserCore(SRankingUserCore Obj_)
		{
			Nick = Obj_.Nick;
			CharCode = Obj_.CharCode;
			CountryCode = Obj_.CountryCode;
		}
		public SRankingUserCore(String Nick_, Int32 CharCode_, String CountryCode_)
		{
			Nick = Nick_;
			CharCode = CharCode_;
			CountryCode = CountryCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Nick);
			Stream_.Pop(ref CharCode);
			Stream_.Pop(ref CountryCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Nick", ref Nick);
			Value_.Pop("CharCode", ref CharCode);
			Value_.Pop("CountryCode", ref CountryCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Nick);
			Stream_.Push(CharCode);
			Stream_.Push(CountryCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Nick", Nick);
			Value_.Push("CharCode", CharCode);
			Value_.Push("CountryCode", CountryCode);
		}
		public void Set(SRankingUserCore Obj_)
		{
			Nick = Obj_.Nick;
			CharCode = Obj_.CharCode;
			CountryCode = Obj_.CountryCode;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Nick) + "," + 
				SEnumChecker.GetStdName(CharCode) + "," + 
				SEnumChecker.GetStdName(CountryCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Nick, "Nick") + "," + 
				SEnumChecker.GetMemberName(CharCode, "CharCode") + "," + 
				SEnumChecker.GetMemberName(CountryCode, "CountryCode");
		}
	}
	public class SRankingUser : SRankingUserCore
	{
		public TUID UID = default(TUID);
		public Int32 Point = default(Int32);
		public SRankingUser()
		{
		}
		public SRankingUser(SRankingUser Obj_) : base(Obj_)
		{
			UID = Obj_.UID;
			Point = Obj_.Point;
		}
		public SRankingUser(SRankingUserCore Super_, TUID UID_, Int32 Point_) : base(Super_)
		{
			UID = UID_;
			Point = Point_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref UID);
			Stream_.Pop(ref Point);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("UID", ref UID);
			Value_.Pop("Point", ref Point);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(UID);
			Stream_.Push(Point);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("UID", UID);
			Value_.Push("Point", Point);
		}
		public void Set(SRankingUser Obj_)
		{
			base.Set(Obj_);
			UID = Obj_.UID;
			Point = Obj_.Point;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(UID) + "," + 
				SEnumChecker.GetStdName(Point);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(UID, "UID") + "," + 
				SEnumChecker.GetMemberName(Point, "Point");
		}
	}
	public class SRankingUserSingleCore : SRankingUserCore
	{
		public Int32 Wave = default(Int32);
		public Int32 Second = default(Int32);
		public Int32 Gold = default(Int32);
		public SRankingUserSingleCore()
		{
		}
		public SRankingUserSingleCore(SRankingUserSingleCore Obj_) : base(Obj_)
		{
			Wave = Obj_.Wave;
			Second = Obj_.Second;
			Gold = Obj_.Gold;
		}
		public SRankingUserSingleCore(SRankingUserCore Super_, Int32 Wave_, Int32 Second_, Int32 Gold_) : base(Super_)
		{
			Wave = Wave_;
			Second = Second_;
			Gold = Gold_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref Wave);
			Stream_.Pop(ref Second);
			Stream_.Pop(ref Gold);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("Wave", ref Wave);
			Value_.Pop("Second", ref Second);
			Value_.Pop("Gold", ref Gold);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(Wave);
			Stream_.Push(Second);
			Stream_.Push(Gold);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("Wave", Wave);
			Value_.Push("Second", Second);
			Value_.Push("Gold", Gold);
		}
		public void Set(SRankingUserSingleCore Obj_)
		{
			base.Set(Obj_);
			Wave = Obj_.Wave;
			Second = Obj_.Second;
			Gold = Obj_.Gold;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(Wave) + "," + 
				SEnumChecker.GetStdName(Second) + "," + 
				SEnumChecker.GetStdName(Gold);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(Wave, "Wave") + "," + 
				SEnumChecker.GetMemberName(Second, "Second") + "," + 
				SEnumChecker.GetMemberName(Gold, "Gold");
		}
	}
	public class SRankingUserSingle : SRankingUserSingleCore
	{
		public TUID UID = default(TUID);
		public Int32 Point = default(Int32);
		public SRankingUserSingle()
		{
		}
		public SRankingUserSingle(SRankingUserSingle Obj_) : base(Obj_)
		{
			UID = Obj_.UID;
			Point = Obj_.Point;
		}
		public SRankingUserSingle(SRankingUserSingleCore Super_, TUID UID_, Int32 Point_) : base(Super_)
		{
			UID = UID_;
			Point = Point_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref UID);
			Stream_.Pop(ref Point);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("UID", ref UID);
			Value_.Pop("Point", ref Point);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(UID);
			Stream_.Push(Point);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("UID", UID);
			Value_.Push("Point", Point);
		}
		public void Set(SRankingUserSingle Obj_)
		{
			base.Set(Obj_);
			UID = Obj_.UID;
			Point = Obj_.Point;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(UID) + "," + 
				SEnumChecker.GetStdName(Point);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(UID, "UID") + "," + 
				SEnumChecker.GetMemberName(Point, "Point");
		}
	}
	public class SRankingUserIslandCore : SRankingUserCore
	{
		public Int32 PassedIslandCount = default(Int32);
		public Int32 Gold = default(Int32);
		public SRankingUserIslandCore()
		{
		}
		public SRankingUserIslandCore(SRankingUserIslandCore Obj_) : base(Obj_)
		{
			PassedIslandCount = Obj_.PassedIslandCount;
			Gold = Obj_.Gold;
		}
		public SRankingUserIslandCore(SRankingUserCore Super_, Int32 PassedIslandCount_, Int32 Gold_) : base(Super_)
		{
			PassedIslandCount = PassedIslandCount_;
			Gold = Gold_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref PassedIslandCount);
			Stream_.Pop(ref Gold);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("PassedIslandCount", ref PassedIslandCount);
			Value_.Pop("Gold", ref Gold);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(PassedIslandCount);
			Stream_.Push(Gold);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("PassedIslandCount", PassedIslandCount);
			Value_.Push("Gold", Gold);
		}
		public void Set(SRankingUserIslandCore Obj_)
		{
			base.Set(Obj_);
			PassedIslandCount = Obj_.PassedIslandCount;
			Gold = Obj_.Gold;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(PassedIslandCount) + "," + 
				SEnumChecker.GetStdName(Gold);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(PassedIslandCount, "PassedIslandCount") + "," + 
				SEnumChecker.GetMemberName(Gold, "Gold");
		}
	}
	public class SRankingUserIsland : SRankingUserIslandCore
	{
		public TUID UID = default(TUID);
		public Int32 Point = default(Int32);
		public SRankingUserIsland()
		{
		}
		public SRankingUserIsland(SRankingUserIsland Obj_) : base(Obj_)
		{
			UID = Obj_.UID;
			Point = Obj_.Point;
		}
		public SRankingUserIsland(SRankingUserIslandCore Super_, TUID UID_, Int32 Point_) : base(Super_)
		{
			UID = UID_;
			Point = Point_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref UID);
			Stream_.Pop(ref Point);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("UID", ref UID);
			Value_.Pop("Point", ref Point);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(UID);
			Stream_.Push(Point);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("UID", UID);
			Value_.Push("Point", Point);
		}
		public void Set(SRankingUserIsland Obj_)
		{
			base.Set(Obj_);
			UID = Obj_.UID;
			Point = Obj_.Point;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(UID) + "," + 
				SEnumChecker.GetStdName(Point);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(UID, "UID") + "," + 
				SEnumChecker.GetMemberName(Point, "Point");
		}
	}
	public class SRanking : SProto
	{
		public TRankingUsers RankingUsers = new TRankingUsers();
		public TRankingUserSingles RankingUserSingles = new TRankingUserSingles();
		public TRankingUserIslands RankingUserIslands = new TRankingUserIslands();
		public SRanking()
		{
		}
		public SRanking(SRanking Obj_)
		{
			RankingUsers = Obj_.RankingUsers;
			RankingUserSingles = Obj_.RankingUserSingles;
			RankingUserIslands = Obj_.RankingUserIslands;
		}
		public SRanking(TRankingUsers RankingUsers_, TRankingUserSingles RankingUserSingles_, TRankingUserIslands RankingUserIslands_)
		{
			RankingUsers = RankingUsers_;
			RankingUserSingles = RankingUserSingles_;
			RankingUserIslands = RankingUserIslands_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref RankingUsers);
			Stream_.Pop(ref RankingUserSingles);
			Stream_.Pop(ref RankingUserIslands);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("RankingUsers", ref RankingUsers);
			Value_.Pop("RankingUserSingles", ref RankingUserSingles);
			Value_.Pop("RankingUserIslands", ref RankingUserIslands);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(RankingUsers);
			Stream_.Push(RankingUserSingles);
			Stream_.Push(RankingUserIslands);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("RankingUsers", RankingUsers);
			Value_.Push("RankingUserSingles", RankingUserSingles);
			Value_.Push("RankingUserIslands", RankingUserIslands);
		}
		public void Set(SRanking Obj_)
		{
			RankingUsers = Obj_.RankingUsers;
			RankingUserSingles = Obj_.RankingUserSingles;
			RankingUserIslands = Obj_.RankingUserIslands;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(RankingUsers) + "," + 
				SEnumChecker.GetStdName(RankingUserSingles) + "," + 
				SEnumChecker.GetStdName(RankingUserIslands);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(RankingUsers, "RankingUsers") + "," + 
				SEnumChecker.GetMemberName(RankingUserSingles, "RankingUserSingles") + "," + 
				SEnumChecker.GetMemberName(RankingUserIslands, "RankingUserIslands");
		}
	}
	public class SRankingUserPointMin : SProto
	{
		public Int32 UserPointMin = default(Int32);
		public Int32 UserPointMinSingle = default(Int32);
		public Int32 UserPointMinIsland = default(Int32);
		public SRankingUserPointMin()
		{
		}
		public SRankingUserPointMin(SRankingUserPointMin Obj_)
		{
			UserPointMin = Obj_.UserPointMin;
			UserPointMinSingle = Obj_.UserPointMinSingle;
			UserPointMinIsland = Obj_.UserPointMinIsland;
		}
		public SRankingUserPointMin(Int32 UserPointMin_, Int32 UserPointMinSingle_, Int32 UserPointMinIsland_)
		{
			UserPointMin = UserPointMin_;
			UserPointMinSingle = UserPointMinSingle_;
			UserPointMinIsland = UserPointMinIsland_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref UserPointMin);
			Stream_.Pop(ref UserPointMinSingle);
			Stream_.Pop(ref UserPointMinIsland);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("UserPointMin", ref UserPointMin);
			Value_.Pop("UserPointMinSingle", ref UserPointMinSingle);
			Value_.Pop("UserPointMinIsland", ref UserPointMinIsland);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(UserPointMin);
			Stream_.Push(UserPointMinSingle);
			Stream_.Push(UserPointMinIsland);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("UserPointMin", UserPointMin);
			Value_.Push("UserPointMinSingle", UserPointMinSingle);
			Value_.Push("UserPointMinIsland", UserPointMinIsland);
		}
		public void Set(SRankingUserPointMin Obj_)
		{
			UserPointMin = Obj_.UserPointMin;
			UserPointMinSingle = Obj_.UserPointMinSingle;
			UserPointMinIsland = Obj_.UserPointMinIsland;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(UserPointMin) + "," + 
				SEnumChecker.GetStdName(UserPointMinSingle) + "," + 
				SEnumChecker.GetStdName(UserPointMinIsland);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(UserPointMin, "UserPointMin") + "," + 
				SEnumChecker.GetMemberName(UserPointMinSingle, "UserPointMinSingle") + "," + 
				SEnumChecker.GetMemberName(UserPointMinIsland, "UserPointMinIsland");
		}
	}
	public enum EProtoRankingNetCs
	{
		RequestRanking,
		Max,
	}
	public enum EProtoRankingNetSc
	{
		RequestRanking,
		Max,
	}
	public class SGachaNetCs : SProto
	{
		public Int32 GachaIndex = default(Int32);
		public SGachaNetCs()
		{
		}
		public SGachaNetCs(SGachaNetCs Obj_)
		{
			GachaIndex = Obj_.GachaIndex;
		}
		public SGachaNetCs(Int32 GachaIndex_)
		{
			GachaIndex = GachaIndex_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref GachaIndex);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("GachaIndex", ref GachaIndex);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(GachaIndex);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("GachaIndex", GachaIndex);
		}
		public void Set(SGachaNetCs Obj_)
		{
			GachaIndex = Obj_.GachaIndex;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(GachaIndex);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(GachaIndex, "GachaIndex");
		}
	}
	public class SGachaX10NetCs : SProto
	{
		public Int32 GachaIndex = default(Int32);
		public SGachaX10NetCs()
		{
		}
		public SGachaX10NetCs(SGachaX10NetCs Obj_)
		{
			GachaIndex = Obj_.GachaIndex;
		}
		public SGachaX10NetCs(Int32 GachaIndex_)
		{
			GachaIndex = GachaIndex_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref GachaIndex);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("GachaIndex", ref GachaIndex);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(GachaIndex);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("GachaIndex", GachaIndex);
		}
		public void Set(SGachaX10NetCs Obj_)
		{
			GachaIndex = Obj_.GachaIndex;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(GachaIndex);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(GachaIndex, "GachaIndex");
		}
	}
	public class SGachaNetSc : SProto
	{
		public TResource[] Cost = new TResource[5];
		public Int32 Index = default(Int32);
		public Int32 CharCode = default(Int32);
		public SGachaNetSc()
		{
			for (int iCost = 0; iCost < Cost.Length; ++iCost)
				Cost[iCost] = default(TResource);
		}
		public SGachaNetSc(SGachaNetSc Obj_)
		{
			Cost = Obj_.Cost;
			Index = Obj_.Index;
			CharCode = Obj_.CharCode;
		}
		public SGachaNetSc(TResource[] Cost_, Int32 Index_, Int32 CharCode_)
		{
			Cost = Cost_;
			Index = Index_;
			CharCode = CharCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Cost);
			Stream_.Pop(ref Index);
			Stream_.Pop(ref CharCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Cost", ref Cost);
			Value_.Pop("Index", ref Index);
			Value_.Pop("CharCode", ref CharCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Cost);
			Stream_.Push(Index);
			Stream_.Push(CharCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Cost", Cost);
			Value_.Push("Index", Index);
			Value_.Push("CharCode", CharCode);
		}
		public void Set(SGachaNetSc Obj_)
		{
			Cost = Obj_.Cost;
			Index = Obj_.Index;
			CharCode = Obj_.CharCode;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Cost) + "," + 
				SEnumChecker.GetStdName(Index) + "," + 
				SEnumChecker.GetStdName(CharCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Cost, "Cost") + "," + 
				SEnumChecker.GetMemberName(Index, "Index") + "," + 
				SEnumChecker.GetMemberName(CharCode, "CharCode");
		}
	}
	public class SGachaX10NetSc : SProto
	{
		public TResource[] Cost = new TResource[5];
		public Int32 Index = default(Int32);
		public List<Int32> CharCodeList = new List<Int32>();
		public TResource[] Refund = new TResource[5];
		public SGachaX10NetSc()
		{
			for (int iCost = 0; iCost < Cost.Length; ++iCost)
				Cost[iCost] = default(TResource);
			for (int iRefund = 0; iRefund < Refund.Length; ++iRefund)
				Refund[iRefund] = default(TResource);
		}
		public SGachaX10NetSc(SGachaX10NetSc Obj_)
		{
			Cost = Obj_.Cost;
			Index = Obj_.Index;
			CharCodeList = Obj_.CharCodeList;
			Refund = Obj_.Refund;
		}
		public SGachaX10NetSc(TResource[] Cost_, Int32 Index_, List<Int32> CharCodeList_, TResource[] Refund_)
		{
			Cost = Cost_;
			Index = Index_;
			CharCodeList = CharCodeList_;
			Refund = Refund_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Cost);
			Stream_.Pop(ref Index);
			Stream_.Pop(ref CharCodeList);
			Stream_.Pop(ref Refund);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Cost", ref Cost);
			Value_.Pop("Index", ref Index);
			Value_.Pop("CharCodeList", ref CharCodeList);
			Value_.Pop("Refund", ref Refund);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Cost);
			Stream_.Push(Index);
			Stream_.Push(CharCodeList);
			Stream_.Push(Refund);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Cost", Cost);
			Value_.Push("Index", Index);
			Value_.Push("CharCodeList", CharCodeList);
			Value_.Push("Refund", Refund);
		}
		public void Set(SGachaX10NetSc Obj_)
		{
			Cost = Obj_.Cost;
			Index = Obj_.Index;
			CharCodeList = Obj_.CharCodeList;
			Refund = Obj_.Refund;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Cost) + "," + 
				SEnumChecker.GetStdName(Index) + "," + 
				SEnumChecker.GetStdName(CharCodeList) + "," + 
				SEnumChecker.GetStdName(Refund);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Cost, "Cost") + "," + 
				SEnumChecker.GetMemberName(Index, "Index") + "," + 
				SEnumChecker.GetMemberName(CharCodeList, "CharCodeList") + "," + 
				SEnumChecker.GetMemberName(Refund, "Refund");
		}
	}
	public class SGachaFailedNetSc : SGachaNetSc
	{
		public TResource[] Refund = new TResource[5];
		public SGachaFailedNetSc()
		{
			for (int iRefund = 0; iRefund < Refund.Length; ++iRefund)
				Refund[iRefund] = default(TResource);
		}
		public SGachaFailedNetSc(SGachaFailedNetSc Obj_) : base(Obj_)
		{
			Refund = Obj_.Refund;
		}
		public SGachaFailedNetSc(SGachaNetSc Super_, TResource[] Refund_) : base(Super_)
		{
			Refund = Refund_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref Refund);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("Refund", ref Refund);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(Refund);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("Refund", Refund);
		}
		public void Set(SGachaFailedNetSc Obj_)
		{
			base.Set(Obj_);
			Refund = Obj_.Refund;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(Refund);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(Refund, "Refund");
		}
	}
	public class SRankRewardNetCs : SProto
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
	public class SQuestSlotIndexCode : SProto
	{
		public TQuestSlotIndex SlotIndex = default(TQuestSlotIndex);
		public Int32 Code = default(Int32);
		public SQuestSlotIndexCode()
		{
		}
		public SQuestSlotIndexCode(SQuestSlotIndexCode Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
			Code = Obj_.Code;
		}
		public SQuestSlotIndexCode(TQuestSlotIndex SlotIndex_, Int32 Code_)
		{
			SlotIndex = SlotIndex_;
			Code = Code_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref SlotIndex);
			Stream_.Pop(ref Code);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("SlotIndex", ref SlotIndex);
			Value_.Pop("Code", ref Code);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(SlotIndex);
			Stream_.Push(Code);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("SlotIndex", SlotIndex);
			Value_.Push("Code", Code);
		}
		public void Set(SQuestSlotIndexCode Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
			Code = Obj_.Code;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(SlotIndex) + "," + 
				SEnumChecker.GetStdName(Code);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(SlotIndex, "SlotIndex") + "," + 
				SEnumChecker.GetMemberName(Code, "Code");
		}
	}
	public class SQuestGotNetSc : SProto
	{
		public TQuestSlotIndexCodes Quests = new TQuestSlotIndexCodes();
		public SQuestGotNetSc()
		{
		}
		public SQuestGotNetSc(SQuestGotNetSc Obj_)
		{
			Quests = Obj_.Quests;
		}
		public SQuestGotNetSc(TQuestSlotIndexCodes Quests_)
		{
			Quests = Quests_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Quests);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Quests", ref Quests);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Quests);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Quests", Quests);
		}
		public void Set(SQuestGotNetSc Obj_)
		{
			Quests = Obj_.Quests;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Quests);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Quests, "Quests");
		}
	}
	public class SQuestDoneNetSc : SProto
	{
		public TQuestSlotIndex SlotIndex = default(TQuestSlotIndex);
		public Int32 Count = default(Int32);
		public SQuestDoneNetSc()
		{
		}
		public SQuestDoneNetSc(SQuestDoneNetSc Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
			Count = Obj_.Count;
		}
		public SQuestDoneNetSc(TQuestSlotIndex SlotIndex_, Int32 Count_)
		{
			SlotIndex = SlotIndex_;
			Count = Count_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref SlotIndex);
			Stream_.Pop(ref Count);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("SlotIndex", ref SlotIndex);
			Value_.Pop("Count", ref Count);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(SlotIndex);
			Stream_.Push(Count);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("SlotIndex", SlotIndex);
			Value_.Push("Count", Count);
		}
		public void Set(SQuestDoneNetSc Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
			Count = Obj_.Count;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(SlotIndex) + "," + 
				SEnumChecker.GetStdName(Count);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(SlotIndex, "SlotIndex") + "," + 
				SEnumChecker.GetMemberName(Count, "Count");
		}
	}
	public class SQuestRewardNetCs : SProto
	{
		public TQuestSlotIndex SlotIndex = default(TQuestSlotIndex);
		public SQuestRewardNetCs()
		{
		}
		public SQuestRewardNetCs(SQuestRewardNetCs Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
		}
		public SQuestRewardNetCs(TQuestSlotIndex SlotIndex_)
		{
			SlotIndex = SlotIndex_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref SlotIndex);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("SlotIndex", ref SlotIndex);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(SlotIndex);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("SlotIndex", SlotIndex);
		}
		public void Set(SQuestRewardNetCs Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(SlotIndex);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(SlotIndex, "SlotIndex");
		}
	}
	public class SQuestRewardNetSc : SProto
	{
		public TQuestSlotIndex SlotIndex = default(TQuestSlotIndex);
		public TimePoint CoolEndTime = default(TimePoint);
		public Int32 DailyCompleteCount = default(Int32);
		public TimePoint DailyCompleteRefreshTime = default(TimePoint);
		public SQuestRewardNetSc()
		{
		}
		public SQuestRewardNetSc(SQuestRewardNetSc Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
			CoolEndTime = Obj_.CoolEndTime;
			DailyCompleteCount = Obj_.DailyCompleteCount;
			DailyCompleteRefreshTime = Obj_.DailyCompleteRefreshTime;
		}
		public SQuestRewardNetSc(TQuestSlotIndex SlotIndex_, TimePoint CoolEndTime_, Int32 DailyCompleteCount_, TimePoint DailyCompleteRefreshTime_)
		{
			SlotIndex = SlotIndex_;
			CoolEndTime = CoolEndTime_;
			DailyCompleteCount = DailyCompleteCount_;
			DailyCompleteRefreshTime = DailyCompleteRefreshTime_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref SlotIndex);
			Stream_.Pop(ref CoolEndTime);
			Stream_.Pop(ref DailyCompleteCount);
			Stream_.Pop(ref DailyCompleteRefreshTime);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("SlotIndex", ref SlotIndex);
			Value_.Pop("CoolEndTime", ref CoolEndTime);
			Value_.Pop("DailyCompleteCount", ref DailyCompleteCount);
			Value_.Pop("DailyCompleteRefreshTime", ref DailyCompleteRefreshTime);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(SlotIndex);
			Stream_.Push(CoolEndTime);
			Stream_.Push(DailyCompleteCount);
			Stream_.Push(DailyCompleteRefreshTime);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("SlotIndex", SlotIndex);
			Value_.Push("CoolEndTime", CoolEndTime);
			Value_.Push("DailyCompleteCount", DailyCompleteCount);
			Value_.Push("DailyCompleteRefreshTime", DailyCompleteRefreshTime);
		}
		public void Set(SQuestRewardNetSc Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
			CoolEndTime = Obj_.CoolEndTime;
			DailyCompleteCount = Obj_.DailyCompleteCount;
			DailyCompleteRefreshTime = Obj_.DailyCompleteRefreshTime;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(SlotIndex) + "," + 
				SEnumChecker.GetStdName(CoolEndTime) + "," + 
				SEnumChecker.GetStdName(DailyCompleteCount) + "," + 
				SEnumChecker.GetStdName(DailyCompleteRefreshTime);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(SlotIndex, "SlotIndex") + "," + 
				SEnumChecker.GetMemberName(CoolEndTime, "CoolEndTime") + "," + 
				SEnumChecker.GetMemberName(DailyCompleteCount, "DailyCompleteCount") + "," + 
				SEnumChecker.GetMemberName(DailyCompleteRefreshTime, "DailyCompleteRefreshTime");
		}
	}
	public class SQuestNextNetCs : SProto
	{
		public TQuestSlotIndex SlotIndex = default(TQuestSlotIndex);
		public SQuestNextNetCs()
		{
		}
		public SQuestNextNetCs(SQuestNextNetCs Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
		}
		public SQuestNextNetCs(TQuestSlotIndex SlotIndex_)
		{
			SlotIndex = SlotIndex_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref SlotIndex);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("SlotIndex", ref SlotIndex);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(SlotIndex);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("SlotIndex", SlotIndex);
		}
		public void Set(SQuestNextNetCs Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(SlotIndex);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(SlotIndex, "SlotIndex");
		}
	}
	public class SQuestNextNetSc : SProto
	{
		public TQuestSlotIndex SlotIndex = default(TQuestSlotIndex);
		public Int32 NewCode = default(Int32);
		public SQuestNextNetSc()
		{
		}
		public SQuestNextNetSc(SQuestNextNetSc Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
			NewCode = Obj_.NewCode;
		}
		public SQuestNextNetSc(TQuestSlotIndex SlotIndex_, Int32 NewCode_)
		{
			SlotIndex = SlotIndex_;
			NewCode = NewCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref SlotIndex);
			Stream_.Pop(ref NewCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("SlotIndex", ref SlotIndex);
			Value_.Pop("NewCode", ref NewCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(SlotIndex);
			Stream_.Push(NewCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("SlotIndex", SlotIndex);
			Value_.Push("NewCode", NewCode);
		}
		public void Set(SQuestNextNetSc Obj_)
		{
			SlotIndex = Obj_.SlotIndex;
			NewCode = Obj_.NewCode;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(SlotIndex) + "," + 
				SEnumChecker.GetStdName(NewCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(SlotIndex, "SlotIndex") + "," + 
				SEnumChecker.GetMemberName(NewCode, "NewCode");
		}
	}
	public class SQuestDailyCompleteRewardNetCs : SProto
	{
		public Boolean WatchAd = default(Boolean);
		public SQuestDailyCompleteRewardNetCs()
		{
		}
		public SQuestDailyCompleteRewardNetCs(SQuestDailyCompleteRewardNetCs Obj_)
		{
			WatchAd = Obj_.WatchAd;
		}
		public SQuestDailyCompleteRewardNetCs(Boolean WatchAd_)
		{
			WatchAd = WatchAd_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref WatchAd);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("WatchAd", ref WatchAd);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(WatchAd);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("WatchAd", WatchAd);
		}
		public void Set(SQuestDailyCompleteRewardNetCs Obj_)
		{
			WatchAd = Obj_.WatchAd;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(WatchAd);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(WatchAd, "WatchAd");
		}
	}
	public class SQuestDailyCompleteRewardNetSc : SProto
	{
		public Boolean WatchAd = default(Boolean);
		public TimePoint RefreshTime = default(TimePoint);
		public SQuestDailyCompleteRewardNetSc()
		{
		}
		public SQuestDailyCompleteRewardNetSc(SQuestDailyCompleteRewardNetSc Obj_)
		{
			WatchAd = Obj_.WatchAd;
			RefreshTime = Obj_.RefreshTime;
		}
		public SQuestDailyCompleteRewardNetSc(Boolean WatchAd_, TimePoint RefreshTime_)
		{
			WatchAd = WatchAd_;
			RefreshTime = RefreshTime_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref WatchAd);
			Stream_.Pop(ref RefreshTime);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("WatchAd", ref WatchAd);
			Value_.Pop("RefreshTime", ref RefreshTime);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(WatchAd);
			Stream_.Push(RefreshTime);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("WatchAd", WatchAd);
			Value_.Push("RefreshTime", RefreshTime);
		}
		public void Set(SQuestDailyCompleteRewardNetSc Obj_)
		{
			WatchAd = Obj_.WatchAd;
			RefreshTime = Obj_.RefreshTime;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(WatchAd) + "," + 
				SEnumChecker.GetStdName(RefreshTime);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(WatchAd, "WatchAd") + "," + 
				SEnumChecker.GetMemberName(RefreshTime, "RefreshTime");
		}
	}
	public class SChangeNickNetCs : SProto
	{
		public String Nick = string.Empty;
		public SChangeNickNetCs()
		{
		}
		public SChangeNickNetCs(SChangeNickNetCs Obj_)
		{
			Nick = Obj_.Nick;
		}
		public SChangeNickNetCs(String Nick_)
		{
			Nick = Nick_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Nick);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Nick", ref Nick);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Nick);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Nick", Nick);
		}
		public void Set(SChangeNickNetCs Obj_)
		{
			Nick = Obj_.Nick;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Nick);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Nick, "Nick");
		}
	}
	public class SChangeNickNetSc : SProto
	{
		public String Nick = string.Empty;
		public SChangeNickNetSc()
		{
		}
		public SChangeNickNetSc(SChangeNickNetSc Obj_)
		{
			Nick = Obj_.Nick;
		}
		public SChangeNickNetSc(String Nick_)
		{
			Nick = Nick_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Nick);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Nick", ref Nick);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Nick);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Nick", Nick);
		}
		public void Set(SChangeNickNetSc Obj_)
		{
			Nick = Obj_.Nick;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Nick);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Nick, "Nick");
		}
	}
	public class SChangeNickFailNetSc : SProto
	{
		public EGameRet GameRet = default(EGameRet);
		public String ForbiddenWord = string.Empty;
		public SChangeNickFailNetSc()
		{
		}
		public SChangeNickFailNetSc(SChangeNickFailNetSc Obj_)
		{
			GameRet = Obj_.GameRet;
			ForbiddenWord = Obj_.ForbiddenWord;
		}
		public SChangeNickFailNetSc(EGameRet GameRet_, String ForbiddenWord_)
		{
			GameRet = GameRet_;
			ForbiddenWord = ForbiddenWord_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref GameRet);
			Stream_.Pop(ref ForbiddenWord);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("GameRet", ref GameRet);
			Value_.Pop("ForbiddenWord", ref ForbiddenWord);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(GameRet);
			Stream_.Push(ForbiddenWord);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("GameRet", GameRet);
			Value_.Push("ForbiddenWord", ForbiddenWord);
		}
		public void Set(SChangeNickFailNetSc Obj_)
		{
			GameRet = Obj_.GameRet;
			ForbiddenWord = Obj_.ForbiddenWord;
		}
		public override string StdName()
		{
			return 
				"rso.game.EGameRet" + "," + 
				SEnumChecker.GetStdName(ForbiddenWord);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(GameRet, "GameRet") + "," + 
				SEnumChecker.GetMemberName(ForbiddenWord, "ForbiddenWord");
		}
	}
	public class SNickPoint : SProto
	{
		public Int32 Point = default(Int32);
		public SNickPoint()
		{
		}
		public SNickPoint(SNickPoint Obj_)
		{
			Point = Obj_.Point;
		}
		public SNickPoint(Int32 Point_)
		{
			Point = Point_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Point);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Point", ref Point);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Point);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Point", Point);
		}
		public void Set(SNickPoint Obj_)
		{
			Point = Obj_.Point;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Point);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Point, "Point");
		}
	}
	public class SCheckIDNetSc : SProto
	{
		public List<SNickPoint> Datas = new List<SNickPoint>();
		public SCheckIDNetSc()
		{
		}
		public SCheckIDNetSc(SCheckIDNetSc Obj_)
		{
			Datas = Obj_.Datas;
		}
		public SCheckIDNetSc(List<SNickPoint> Datas_)
		{
			Datas = Datas_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Datas);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Datas", ref Datas);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Datas);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Datas", Datas);
		}
		public void Set(SCheckIDNetSc Obj_)
		{
			Datas = Obj_.Datas;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Datas);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Datas, "Datas");
		}
	}
	public class SCouponUseNetCs : SProto
	{
		public String Key = string.Empty;
		public SCouponUseNetCs()
		{
		}
		public SCouponUseNetCs(SCouponUseNetCs Obj_)
		{
			Key = Obj_.Key;
		}
		public SCouponUseNetCs(String Key_)
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
		public void Set(SCouponUseNetCs Obj_)
		{
			Key = Obj_.Key;
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
	public class SRewardDB : SProto
	{
		public TUID UID = default(TUID);
		public TResource[] ResourcesLeft = new TResource[5];
		public List<Int32> CharsAdded = new List<Int32>();
		public SRewardDB()
		{
			for (int iResourcesLeft = 0; iResourcesLeft < ResourcesLeft.Length; ++iResourcesLeft)
				ResourcesLeft[iResourcesLeft] = default(TResource);
		}
		public SRewardDB(SRewardDB Obj_)
		{
			UID = Obj_.UID;
			ResourcesLeft = Obj_.ResourcesLeft;
			CharsAdded = Obj_.CharsAdded;
		}
		public SRewardDB(TUID UID_, TResource[] ResourcesLeft_, List<Int32> CharsAdded_)
		{
			UID = UID_;
			ResourcesLeft = ResourcesLeft_;
			CharsAdded = CharsAdded_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref UID);
			Stream_.Pop(ref ResourcesLeft);
			Stream_.Pop(ref CharsAdded);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("UID", ref UID);
			Value_.Pop("ResourcesLeft", ref ResourcesLeft);
			Value_.Pop("CharsAdded", ref CharsAdded);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(UID);
			Stream_.Push(ResourcesLeft);
			Stream_.Push(CharsAdded);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("UID", UID);
			Value_.Push("ResourcesLeft", ResourcesLeft);
			Value_.Push("CharsAdded", CharsAdded);
		}
		public void Set(SRewardDB Obj_)
		{
			UID = Obj_.UID;
			ResourcesLeft = Obj_.ResourcesLeft;
			CharsAdded = Obj_.CharsAdded;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(UID) + "," + 
				SEnumChecker.GetStdName(ResourcesLeft) + "," + 
				SEnumChecker.GetStdName(CharsAdded);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(UID, "UID") + "," + 
				SEnumChecker.GetMemberName(ResourcesLeft, "ResourcesLeft") + "," + 
				SEnumChecker.GetMemberName(CharsAdded, "CharsAdded");
		}
	}
	public class SCouponUseNetSc : SRewardDB
	{
		public SCouponUseNetSc()
		{
		}
		public SCouponUseNetSc(SCouponUseNetSc Obj_) : base(Obj_)
		{
		}
		public SCouponUseNetSc(SRewardDB Super_) : base(Super_)
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
		public void Set(SCouponUseNetSc Obj_)
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
	public class SCouponUseFailNetSc : SProto
	{
		public ERet Ret = default(ERet);
		public SCouponUseFailNetSc()
		{
		}
		public SCouponUseFailNetSc(SCouponUseFailNetSc Obj_)
		{
			Ret = Obj_.Ret;
		}
		public SCouponUseFailNetSc(ERet Ret_)
		{
			Ret = Ret_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Ret);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Ret", ref Ret);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Ret);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Ret", Ret);
		}
		public void Set(SCouponUseFailNetSc Obj_)
		{
			Ret = Obj_.Ret;
		}
		public override string StdName()
		{
			return 
				"bb.ERet";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Ret, "Ret");
		}
	}
	public class STutorialRewardNetCs : SProto
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
	public class SRankingRewardInfoNetCs : SProto
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
	public class SRankingRewardInfoNetSc : SProto
	{
		public Int64 Counter = default(Int64);
		public Int32 Ranking = default(Int32);
		public Int32 RankingSingle = default(Int32);
		public Int32 RankingIsland = default(Int32);
		public SRankingRewardInfoNetSc()
		{
		}
		public SRankingRewardInfoNetSc(SRankingRewardInfoNetSc Obj_)
		{
			Counter = Obj_.Counter;
			Ranking = Obj_.Ranking;
			RankingSingle = Obj_.RankingSingle;
			RankingIsland = Obj_.RankingIsland;
		}
		public SRankingRewardInfoNetSc(Int64 Counter_, Int32 Ranking_, Int32 RankingSingle_, Int32 RankingIsland_)
		{
			Counter = Counter_;
			Ranking = Ranking_;
			RankingSingle = RankingSingle_;
			RankingIsland = RankingIsland_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Counter);
			Stream_.Pop(ref Ranking);
			Stream_.Pop(ref RankingSingle);
			Stream_.Pop(ref RankingIsland);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Counter", ref Counter);
			Value_.Pop("Ranking", ref Ranking);
			Value_.Pop("RankingSingle", ref RankingSingle);
			Value_.Pop("RankingIsland", ref RankingIsland);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Counter);
			Stream_.Push(Ranking);
			Stream_.Push(RankingSingle);
			Stream_.Push(RankingIsland);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Counter", Counter);
			Value_.Push("Ranking", Ranking);
			Value_.Push("RankingSingle", RankingSingle);
			Value_.Push("RankingIsland", RankingIsland);
		}
		public void Set(SRankingRewardInfoNetSc Obj_)
		{
			Counter = Obj_.Counter;
			Ranking = Obj_.Ranking;
			RankingSingle = Obj_.RankingSingle;
			RankingIsland = Obj_.RankingIsland;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Counter) + "," + 
				SEnumChecker.GetStdName(Ranking) + "," + 
				SEnumChecker.GetStdName(RankingSingle) + "," + 
				SEnumChecker.GetStdName(RankingIsland);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Counter, "Counter") + "," + 
				SEnumChecker.GetMemberName(Ranking, "Ranking") + "," + 
				SEnumChecker.GetMemberName(RankingSingle, "RankingSingle") + "," + 
				SEnumChecker.GetMemberName(RankingIsland, "RankingIsland");
		}
	}
	public class SRankingRewardNetCs : SProto
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
	public class SRankingRewardNetSc : SProto
	{
		public Int64 Counter = default(Int64);
		public Int32 RewardCode = default(Int32);
		public Int32 RewardCodeSingle = default(Int32);
		public Int32 RewardCodeIsland = default(Int32);
		public SRankingRewardNetSc()
		{
		}
		public SRankingRewardNetSc(SRankingRewardNetSc Obj_)
		{
			Counter = Obj_.Counter;
			RewardCode = Obj_.RewardCode;
			RewardCodeSingle = Obj_.RewardCodeSingle;
			RewardCodeIsland = Obj_.RewardCodeIsland;
		}
		public SRankingRewardNetSc(Int64 Counter_, Int32 RewardCode_, Int32 RewardCodeSingle_, Int32 RewardCodeIsland_)
		{
			Counter = Counter_;
			RewardCode = RewardCode_;
			RewardCodeSingle = RewardCodeSingle_;
			RewardCodeIsland = RewardCodeIsland_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Counter);
			Stream_.Pop(ref RewardCode);
			Stream_.Pop(ref RewardCodeSingle);
			Stream_.Pop(ref RewardCodeIsland);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Counter", ref Counter);
			Value_.Pop("RewardCode", ref RewardCode);
			Value_.Pop("RewardCodeSingle", ref RewardCodeSingle);
			Value_.Pop("RewardCodeIsland", ref RewardCodeIsland);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Counter);
			Stream_.Push(RewardCode);
			Stream_.Push(RewardCodeSingle);
			Stream_.Push(RewardCodeIsland);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Counter", Counter);
			Value_.Push("RewardCode", RewardCode);
			Value_.Push("RewardCodeSingle", RewardCodeSingle);
			Value_.Push("RewardCodeIsland", RewardCodeIsland);
		}
		public void Set(SRankingRewardNetSc Obj_)
		{
			Counter = Obj_.Counter;
			RewardCode = Obj_.RewardCode;
			RewardCodeSingle = Obj_.RewardCodeSingle;
			RewardCodeIsland = Obj_.RewardCodeIsland;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Counter) + "," + 
				SEnumChecker.GetStdName(RewardCode) + "," + 
				SEnumChecker.GetStdName(RewardCodeSingle) + "," + 
				SEnumChecker.GetStdName(RewardCodeIsland);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Counter, "Counter") + "," + 
				SEnumChecker.GetMemberName(RewardCode, "RewardCode") + "," + 
				SEnumChecker.GetMemberName(RewardCodeSingle, "RewardCodeSingle") + "," + 
				SEnumChecker.GetMemberName(RewardCodeIsland, "RewardCodeIsland");
		}
	}
	public class SRankingRewardFailNetSc : SProto
	{
		public ERet Ret = default(ERet);
		public SRankingRewardFailNetSc()
		{
		}
		public SRankingRewardFailNetSc(SRankingRewardFailNetSc Obj_)
		{
			Ret = Obj_.Ret;
		}
		public SRankingRewardFailNetSc(ERet Ret_)
		{
			Ret = Ret_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Ret);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Ret", ref Ret);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Ret);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Ret", Ret);
		}
		public void Set(SRankingRewardFailNetSc Obj_)
		{
			Ret = Obj_.Ret;
		}
		public override string StdName()
		{
			return 
				"bb.ERet";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Ret, "Ret");
		}
	}
	public class SRoomInfo : SProto
	{
		public EGameMode Mode = default(EGameMode);
		public Int32 RoomIdx = default(Int32);
		public TUID MasterUID = default(TUID);
		public String MasterUser = string.Empty;
		public String Password = string.Empty;
		public Int32 UserCount = default(Int32);
		public ERoomState State = default(ERoomState);
		public SRoomInfo()
		{
		}
		public SRoomInfo(SRoomInfo Obj_)
		{
			Mode = Obj_.Mode;
			RoomIdx = Obj_.RoomIdx;
			MasterUID = Obj_.MasterUID;
			MasterUser = Obj_.MasterUser;
			Password = Obj_.Password;
			UserCount = Obj_.UserCount;
			State = Obj_.State;
		}
		public SRoomInfo(EGameMode Mode_, Int32 RoomIdx_, TUID MasterUID_, String MasterUser_, String Password_, Int32 UserCount_, ERoomState State_)
		{
			Mode = Mode_;
			RoomIdx = RoomIdx_;
			MasterUID = MasterUID_;
			MasterUser = MasterUser_;
			Password = Password_;
			UserCount = UserCount_;
			State = State_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Mode);
			Stream_.Pop(ref RoomIdx);
			Stream_.Pop(ref MasterUID);
			Stream_.Pop(ref MasterUser);
			Stream_.Pop(ref Password);
			Stream_.Pop(ref UserCount);
			Stream_.Pop(ref State);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Mode", ref Mode);
			Value_.Pop("RoomIdx", ref RoomIdx);
			Value_.Pop("MasterUID", ref MasterUID);
			Value_.Pop("MasterUser", ref MasterUser);
			Value_.Pop("Password", ref Password);
			Value_.Pop("UserCount", ref UserCount);
			Value_.Pop("State", ref State);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Mode);
			Stream_.Push(RoomIdx);
			Stream_.Push(MasterUID);
			Stream_.Push(MasterUser);
			Stream_.Push(Password);
			Stream_.Push(UserCount);
			Stream_.Push(State);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Mode", Mode);
			Value_.Push("RoomIdx", RoomIdx);
			Value_.Push("MasterUID", MasterUID);
			Value_.Push("MasterUser", MasterUser);
			Value_.Push("Password", Password);
			Value_.Push("UserCount", UserCount);
			Value_.Push("State", State);
		}
		public void Set(SRoomInfo Obj_)
		{
			Mode = Obj_.Mode;
			RoomIdx = Obj_.RoomIdx;
			MasterUID = Obj_.MasterUID;
			MasterUser = Obj_.MasterUser;
			Password = Obj_.Password;
			UserCount = Obj_.UserCount;
			State = Obj_.State;
		}
		public override string StdName()
		{
			return 
				"bb.EGameMode" + "," + 
				SEnumChecker.GetStdName(RoomIdx) + "," + 
				SEnumChecker.GetStdName(MasterUID) + "," + 
				SEnumChecker.GetStdName(MasterUser) + "," + 
				SEnumChecker.GetStdName(Password) + "," + 
				SEnumChecker.GetStdName(UserCount) + "," + 
				"bb.ERoomState";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Mode, "Mode") + "," + 
				SEnumChecker.GetMemberName(RoomIdx, "RoomIdx") + "," + 
				SEnumChecker.GetMemberName(MasterUID, "MasterUID") + "," + 
				SEnumChecker.GetMemberName(MasterUser, "MasterUser") + "," + 
				SEnumChecker.GetMemberName(Password, "Password") + "," + 
				SEnumChecker.GetMemberName(UserCount, "UserCount") + "," + 
				SEnumChecker.GetMemberName(State, "State");
		}
	}
	public class SRoomChangeNetSc : SProto
	{
		public Int32 RoomIdx = default(Int32);
		public SRoomInfo RoomInfo = new SRoomInfo();
		public Boolean IsEmpty = default(Boolean);
		public SRoomChangeNetSc()
		{
		}
		public SRoomChangeNetSc(SRoomChangeNetSc Obj_)
		{
			RoomIdx = Obj_.RoomIdx;
			RoomInfo = Obj_.RoomInfo;
			IsEmpty = Obj_.IsEmpty;
		}
		public SRoomChangeNetSc(Int32 RoomIdx_, SRoomInfo RoomInfo_, Boolean IsEmpty_)
		{
			RoomIdx = RoomIdx_;
			RoomInfo = RoomInfo_;
			IsEmpty = IsEmpty_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref RoomIdx);
			Stream_.Pop(ref RoomInfo);
			Stream_.Pop(ref IsEmpty);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("RoomIdx", ref RoomIdx);
			Value_.Pop("RoomInfo", ref RoomInfo);
			Value_.Pop("IsEmpty", ref IsEmpty);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(RoomIdx);
			Stream_.Push(RoomInfo);
			Stream_.Push(IsEmpty);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("RoomIdx", RoomIdx);
			Value_.Push("RoomInfo", RoomInfo);
			Value_.Push("IsEmpty", IsEmpty);
		}
		public void Set(SRoomChangeNetSc Obj_)
		{
			RoomIdx = Obj_.RoomIdx;
			RoomInfo.Set(Obj_.RoomInfo);
			IsEmpty = Obj_.IsEmpty;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(RoomIdx) + "," + 
				SEnumChecker.GetStdName(RoomInfo) + "," + 
				SEnumChecker.GetStdName(IsEmpty);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(RoomIdx, "RoomIdx") + "," + 
				SEnumChecker.GetMemberName(RoomInfo, "RoomInfo") + "," + 
				SEnumChecker.GetMemberName(IsEmpty, "IsEmpty");
		}
	}
	public class SRoomListNetCs : SProto
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
	public class SRoomListNetSc : SProto
	{
		public SRooms RoomList = new SRooms();
		public SRoomListNetSc()
		{
		}
		public SRoomListNetSc(SRoomListNetSc Obj_)
		{
			RoomList = Obj_.RoomList;
		}
		public SRoomListNetSc(SRooms RoomList_)
		{
			RoomList = RoomList_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref RoomList);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("RoomList", ref RoomList);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(RoomList);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("RoomList", RoomList);
		}
		public void Set(SRoomListNetSc Obj_)
		{
			RoomList = Obj_.RoomList;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(RoomList);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(RoomList, "RoomList");
		}
	}
	public class SRoomCreateNetCs : SProto
	{
		public EGameMode Mode = default(EGameMode);
		public String Password = string.Empty;
		public SRoomCreateNetCs()
		{
		}
		public SRoomCreateNetCs(SRoomCreateNetCs Obj_)
		{
			Mode = Obj_.Mode;
			Password = Obj_.Password;
		}
		public SRoomCreateNetCs(EGameMode Mode_, String Password_)
		{
			Mode = Mode_;
			Password = Password_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Mode);
			Stream_.Pop(ref Password);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Mode", ref Mode);
			Value_.Pop("Password", ref Password);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Mode);
			Stream_.Push(Password);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Mode", Mode);
			Value_.Push("Password", Password);
		}
		public void Set(SRoomCreateNetCs Obj_)
		{
			Mode = Obj_.Mode;
			Password = Obj_.Password;
		}
		public override string StdName()
		{
			return 
				"bb.EGameMode" + "," + 
				SEnumChecker.GetStdName(Password);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Mode, "Mode") + "," + 
				SEnumChecker.GetMemberName(Password, "Password");
		}
	}
	public class SRoomCreateNetSc : SProto
	{
		public SRoomInfo RoomInfo = new SRoomInfo();
		public SRoomCreateNetSc()
		{
		}
		public SRoomCreateNetSc(SRoomCreateNetSc Obj_)
		{
			RoomInfo = Obj_.RoomInfo;
		}
		public SRoomCreateNetSc(SRoomInfo RoomInfo_)
		{
			RoomInfo = RoomInfo_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref RoomInfo);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("RoomInfo", ref RoomInfo);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(RoomInfo);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("RoomInfo", RoomInfo);
		}
		public void Set(SRoomCreateNetSc Obj_)
		{
			RoomInfo.Set(Obj_.RoomInfo);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(RoomInfo);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(RoomInfo, "RoomInfo");
		}
	}
	public class SRoomJoinNetCs : SProto
	{
		public Int32 RoomIdx = default(Int32);
		public SRoomJoinNetCs()
		{
		}
		public SRoomJoinNetCs(SRoomJoinNetCs Obj_)
		{
			RoomIdx = Obj_.RoomIdx;
		}
		public SRoomJoinNetCs(Int32 RoomIdx_)
		{
			RoomIdx = RoomIdx_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref RoomIdx);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("RoomIdx", ref RoomIdx);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(RoomIdx);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("RoomIdx", RoomIdx);
		}
		public void Set(SRoomJoinNetCs Obj_)
		{
			RoomIdx = Obj_.RoomIdx;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(RoomIdx);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(RoomIdx, "RoomIdx");
		}
	}
	public class SRoomJoinNetSc : SProto
	{
		public SRoomInfo RoomInfo = new SRoomInfo();
		public SRoomJoinNetSc()
		{
		}
		public SRoomJoinNetSc(SRoomJoinNetSc Obj_)
		{
			RoomInfo = Obj_.RoomInfo;
		}
		public SRoomJoinNetSc(SRoomInfo RoomInfo_)
		{
			RoomInfo = RoomInfo_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref RoomInfo);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("RoomInfo", ref RoomInfo);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(RoomInfo);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("RoomInfo", RoomInfo);
		}
		public void Set(SRoomJoinNetSc Obj_)
		{
			RoomInfo.Set(Obj_.RoomInfo);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(RoomInfo);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(RoomInfo, "RoomInfo");
		}
	}
	public class SRoomJoinFailedNetSc : SProto
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
	public class SRoomOutNetCs : SProto
	{
		public Int32 RoomIdx = default(Int32);
		public SRoomOutNetCs()
		{
		}
		public SRoomOutNetCs(SRoomOutNetCs Obj_)
		{
			RoomIdx = Obj_.RoomIdx;
		}
		public SRoomOutNetCs(Int32 RoomIdx_)
		{
			RoomIdx = RoomIdx_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref RoomIdx);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("RoomIdx", ref RoomIdx);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(RoomIdx);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("RoomIdx", RoomIdx);
		}
		public void Set(SRoomOutNetCs Obj_)
		{
			RoomIdx = Obj_.RoomIdx;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(RoomIdx);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(RoomIdx, "RoomIdx");
		}
	}
	public class SRoomOutFailedNetSc : SProto
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
	public class SRoomOutNetSc : SProto
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
	public class SRoomReadyNetCs : SProto
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
	public class SRoomReadyNetSc : SProto
	{
		public EGameMode Mode = default(EGameMode);
		public SRoomReadyNetSc()
		{
		}
		public SRoomReadyNetSc(SRoomReadyNetSc Obj_)
		{
			Mode = Obj_.Mode;
		}
		public SRoomReadyNetSc(EGameMode Mode_)
		{
			Mode = Mode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Mode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Mode", ref Mode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Mode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Mode", Mode);
		}
		public void Set(SRoomReadyNetSc Obj_)
		{
			Mode = Obj_.Mode;
		}
		public override string StdName()
		{
			return 
				"bb.EGameMode";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Mode, "Mode");
		}
	}
	public class SRoomChatNetCs : SProto
	{
		public Int32 RoomIdx = default(Int32);
		public String Message = string.Empty;
		public SRoomChatNetCs()
		{
		}
		public SRoomChatNetCs(SRoomChatNetCs Obj_)
		{
			RoomIdx = Obj_.RoomIdx;
			Message = Obj_.Message;
		}
		public SRoomChatNetCs(Int32 RoomIdx_, String Message_)
		{
			RoomIdx = RoomIdx_;
			Message = Message_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref RoomIdx);
			Stream_.Pop(ref Message);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("RoomIdx", ref RoomIdx);
			Value_.Pop("Message", ref Message);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(RoomIdx);
			Stream_.Push(Message);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("RoomIdx", RoomIdx);
			Value_.Push("Message", Message);
		}
		public void Set(SRoomChatNetCs Obj_)
		{
			RoomIdx = Obj_.RoomIdx;
			Message = Obj_.Message;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(RoomIdx) + "," + 
				SEnumChecker.GetStdName(Message);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(RoomIdx, "RoomIdx") + "," + 
				SEnumChecker.GetMemberName(Message, "Message");
		}
	}
	public class SRoomChatNetSc : SProto
	{
		public String UserNick = string.Empty;
		public String Message = string.Empty;
		public SRoomChatNetSc()
		{
		}
		public SRoomChatNetSc(SRoomChatNetSc Obj_)
		{
			UserNick = Obj_.UserNick;
			Message = Obj_.Message;
		}
		public SRoomChatNetSc(String UserNick_, String Message_)
		{
			UserNick = UserNick_;
			Message = Message_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref UserNick);
			Stream_.Pop(ref Message);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("UserNick", ref UserNick);
			Value_.Pop("Message", ref Message);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(UserNick);
			Stream_.Push(Message);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("UserNick", UserNick);
			Value_.Push("Message", Message);
		}
		public void Set(SRoomChatNetSc Obj_)
		{
			UserNick = Obj_.UserNick;
			Message = Obj_.Message;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(UserNick) + "," + 
				SEnumChecker.GetStdName(Message);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(UserNick, "UserNick") + "," + 
				SEnumChecker.GetMemberName(Message, "Message");
		}
	}
	public class SRoomNotiNetCs : SProto
	{
		public Int32 RoomIdx = default(Int32);
		public SRoomNotiNetCs()
		{
		}
		public SRoomNotiNetCs(SRoomNotiNetCs Obj_)
		{
			RoomIdx = Obj_.RoomIdx;
		}
		public SRoomNotiNetCs(Int32 RoomIdx_)
		{
			RoomIdx = RoomIdx_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref RoomIdx);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("RoomIdx", ref RoomIdx);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(RoomIdx);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("RoomIdx", RoomIdx);
		}
		public void Set(SRoomNotiNetCs Obj_)
		{
			RoomIdx = Obj_.RoomIdx;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(RoomIdx);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(RoomIdx, "RoomIdx");
		}
	}
	public class SRoomNotiNetSc : SProto
	{
		public SRoomInfo RoomInfo = new SRoomInfo();
		public SRoomNotiNetSc()
		{
		}
		public SRoomNotiNetSc(SRoomNotiNetSc Obj_)
		{
			RoomInfo = Obj_.RoomInfo;
		}
		public SRoomNotiNetSc(SRoomInfo RoomInfo_)
		{
			RoomInfo = RoomInfo_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref RoomInfo);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("RoomInfo", ref RoomInfo);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(RoomInfo);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("RoomInfo", RoomInfo);
		}
		public void Set(SRoomNotiNetSc Obj_)
		{
			RoomInfo.Set(Obj_.RoomInfo);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(RoomInfo);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(RoomInfo, "RoomInfo");
		}
	}
	public partial class global
	{
		public const Single c_2_ScreenWidth = c_ScreenWidth*2.0f;
		public const Single c_AccExt = c_Factor;
		public const Single c_AirResistance = 1.0f;
		public const SByte c_BalloonCountForPump = 1;
		public const SByte c_BalloonCountForRegen = 2;
		public const Single c_BalloonHeight = 0.27f;
		public const Single c_BalloonLocalScale = 0.5f;
		public const Single c_BalloonOffsetY = 0.5f;
		public const Single c_BalloonWidth = 0.2f;
		public const Int64 c_BattleStartDelayMilliSec = 2000;
		public const Int64 c_ChainKillDelayTickCount = 50000000;
		public const Single c_ContactOffset = 0.0001f;
		public const Single c_DefaultVel = c_Factor*0.22f;
		public const Single c_DieUpVel = 0.7f;
		public const Int32 c_FPS = 60;
		public const Single c_Factor = c_ScreenWidth*0.75f;
		public const Single c_FlapOnAcc = c_Factor*0.25f;
		public const Single c_GameHeight = 3.5f;
		public const Single c_GhostSpeed = 3.0f;
		public const Single c_Gravity = -c_FlapOnAcc;
		public const Single c_GravityDeadRatio = 2.0f;
		public const Single c_GravityParachuteRatio = 0.5f;
		public const Single c_GroundResistance = 0.1f;
		public const Single c_LandXDragPerFrame = 1.0f/c_FPS;
		public const Int32 c_MaxPlayer = 6;
		public const Single c_MaxVelDeadY = c_DefaultVel*2.0f;
		public const Single c_MaxVelParachuteX = c_DefaultVel*1.2f;
		public const Single c_MaxVelParachuteY = c_DefaultVel*0.5f;
		public const Int64 c_NetworkTickBuffer = c_NetworkTickSync+500000;
		public const Int64 c_NetworkTickSync = 500000;
		public const Single c_OnePumpDuration = 0.4f;
		public const Single c_ParachuteAccX = c_FlapOnAcc;
		public const Single c_ParachuteHeight = 0.25f;
		public const Single c_ParachuteLocalScale = 0.5f;
		public const Single c_ParachuteOffsetY = 0.37f;
		public const Single c_ParachuteWidth = 0.45f;
		public const Single c_PlayerHeight = 0.150337f;
		public const Single c_PlayerOffsetY = c_PlayerHeight*0.5f;
		public const Single c_PlayerWidth = 0.1258713f;
		public const SByte c_PumpCountForBalloon = 6;
		public const Int32 c_QuestCnt_Max = 5;
		public const Int64 c_RegenDelayTickCount = 20000000;
		public const Single c_ScreenCenterX = 3.448f;
		public const Single c_ScreenCenterY = c_ScreenHeight*0.5f;
		public const Single c_ScreenHeight = 3.5f;
		public const Single c_ScreenHeight_2 = c_ScreenHeight*0.5f;
		public const Single c_ScreenWidth = 3.448f;
		public const Single c_ScreenWidth_2 = c_ScreenWidth*0.5f;
		public const TVer c_Ver_Main = 38;
	}
}
