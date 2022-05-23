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
	using rso.net;
	using rso.game;
	using rso.physics;
	public enum ETrackingKey
	{
		apple_login,
		facebook_login,
		google_login,
		guest_login,
		guest_to_apple,
		guest_to_facebook,
		guest_to_google,
		ads_dailyquest_result,
		ads_quest,
		ads_result_dodge,
		ads_result_island,
		ads_shop_free,
		ads_shop_free_1,
		ads_shop_free_10,
		ads_shop_free_3,
		ads_shop_free_5,
		unblock_character,
		dailyquest_ad_view,
		quest_reward_complete_1,
		quest_reward_complete_3,
		quest_reward_complete_5,
		quest_reward_complete_7,
		quest_reward_complete_daily,
		earn_keycoin,
		earn_ruby_reward,
		cancel_multiplay,
		cancel_multiplay_startsingle,
		oneone_select,
		oneone_select_r2,
		oneone_select_total,
		singleplay_select_arrow,
		singleplay_select_arrow_r2,
		singleplay_select_arrow_total,
		singleplay_select_arrowM,
		singleplay_select_arrowM_r2,
		singleplay_select_arrowM_total,
		singleplay_select_island,
		singleplay_select_island_r2,
		singleplay_select_island_total,
		singleplay_select_islandM,
		singleplay_select_islandM_r2,
		singleplay_select_islandM_total,
		survival3pplay_select,
		survival3pplay_select_r2,
		survival3pplay_select_total,
		survivalplay_select,
		survivalplay_select_r2,
		survivalplay_select_total,
		teamplay_select_team2,
		teamplay_select_team2_r2,
		teamplay_select_team2_total,
		teamplay_select_teamB,
		teamplay_select_teamB_r2,
		teamplay_select_teamB_total,
		bronze_rank_1,
		bronze_rank_2,
		bronze_rank_3,
		bronze_rank_4,
		bronze_rank_5,
		champion_rank_1,
		champion_rank_2,
		champion_rank_3,
		champion_rank_4,
		champion_rank_5,
		dia_rank_1,
		dia_rank_2,
		dia_rank_3,
		dia_rank_4,
		dia_rank_5,
		gold_rank_1,
		gold_rank_2,
		gold_rank_3,
		gold_rank_4,
		gold_rank_5,
		silver_rank_1,
		silver_rank_2,
		silver_rank_3,
		silver_rank_4,
		silver_rank_5,
		unrank_1,
		unrank_2,
		unrank_3,
		unrank_4,
		unrank_5,
		weekly_ranking_reward,
		buy_gacha,
		gold_1050,
		gold_2900,
		gold_380,
		gold_6780,
		pay_characterbox,
		pay_package,
		spend_gold_gacha,
		spend_gold_singlecharge,
		spend_keycoin,
		spend_ruby_shop,
		title_end,
		tutorial_1,
		tutorial_2,
		tutorial_3,
		tutorial_4,
		Max,
		Null,
	}
	public enum EText
	{
		VsThree,
		LoginScene_MakeNickName,
		Global_Input_PlaceHolder,
		Global_Button_Make,
		Global_Button_Ok,
		Global_Button_Cancel,
		Global_Button_Confirm,
		Global_Popup_GameExit,
		Global_Grade_Normal,
		Global_Grade_Rare,
		Global_Grade_Epic,
		Global_Text_Ranking,
		Global_Text_Rank,
		LobbyScene_BonusStage,
		LobbyScene_Survival_Normal,
		LobbyScene_Team,
		LobbyScene_Play,
		CharacterSelectScene_Select,
		Character_Name_Kiwibird,
		Character_Name_Chick,
		Character_Name_Duckling,
		Character_Name_ShibaInu,
		Character_Name_Poodle,
		Character_Name_Turtle,
		Character_Name_Panda,
		Character_Name_Polarbear,
		Character_Name_Brownbear,
		Character_Name_Gorilla,
		Character_Name_YeTigorilla,
		Character_Name_Greenfrog,
		Character_Name_Hen,
		Character_Name_Rooster,
		Character_Name_Crowtit,
		Character_Name_Hummingbird,
		Character_Name_Gull,
		Character_Name_Macaw,
		Character_Name_Woodpecker,
		Character_Name_BigbilledBird,
		Character_Name_Eagle,
		Character_Name_Owl,
		Character_Name_Chimmy,
		Character_Name_Tata,
		Character_Name_Brown,
		Character_Name_Cony,
		ResultScene_Text_Win,
		ResultScene_Text_Lose,
		ResultScene_Text_Result,
		Character_Requirement_Kiwibird,
		CharacterSelectScene_Select_NotHave,
		MyInfoScene_MaxWinPoint,
		MyInfoScene_BestPlay,
		MyInfoScene_PersonalWin,
		MyInfoScene_TeamWin,
		LobbyScene_UserID,
		Global_Button_On,
		Global_Button_Off,
		OptionScene_Music,
		OptionScene_EffectSound,
		OptionScene_Invite_title,
		OptionScene_Facebook,
		OptionScene_Line,
		OptionScene_Kakao,
		OptionScene_Terms,
		OptionScene_Privacy,
		OptionScene_Language,
		OptionScene_Help,
		OptionScene_Setting,
		OptionScene_Vibe,
		OptionScene_LanguageText,
		Global_Popup_LanguageChanage,
		Quest_Team_Play,
		Quest_Team_Victory,
		Quest_Survival_Play,
		Quest_Survival_Victory,
		Quest_Single_Play,
		Quest_Ingame_ConsecutiveKill,
		Quest_Ingame_BalloonPopping,
		Quest_Ingame_Kill,
		Quest_Ingame_BestPlayer,
		Quest_Character_Gacha,
		Quest_Gacha_Common,
		Quest_Gacha_Ruby,
		QuestScene_Title,
		QuestScene_Refresh,
		QuestScene_Next,
		OptionScene_Language_English,
		OptionScene_Language_Korean,
		OptionScene_Language_France,
		OptionScene_Language_Germany,
		OptionScene_Language_Spain,
		OptionScene_Language_Italia,
		OptionScene_Language_ChinaCH,
		OptionScene_Language_ChinaTW,
		OptionScene_Language_Japan,
		OptionScene_Language_Portugal,
		OptionScene_Language_Russia,
		OptionScene_Language_Nederland,
		OptionScene_Language_Turkey,
		OptionScene_Language_Finland,
		OptionScene_Language_Malaysia,
		OptionScene_Language_Thailand,
		OptionScene_Language_Indonesia,
		OptionScene_Language_Vietnam,
		RankingRewardScene_Title,
		RankingRewardScene_Description,
		ShopScene_GachaText,
		ShopScene_GachaFailedText,
		Global_Text_Ticket,
		Global_Text_Gold,
		Global_Text_Dia,
		Global_Popup_ResourceNotEnough,
		Ranke_Unranked_5,
		Ranke_Unranked_4,
		Ranke_Unranked_3,
		Ranke_Unranked_2,
		Ranke_Unranked_1,
		Ranke_Bronze_5,
		Ranke_Bronze_4,
		Ranke_Bronze_3,
		Ranke_Bronze_2,
		Ranke_Bronze_1,
		Ranke_Silver_5,
		Ranke_Silver_4,
		Ranke_Silver_3,
		Ranke_Silver_2,
		Ranke_Silver_1,
		Ranke_Gold_5,
		Ranke_Gold_4,
		Ranke_Gold_3,
		Ranke_Gold_2,
		Ranke_Gold_1,
		Ranke_Diamond_5,
		Ranke_Diamond_4,
		Ranke_Diamond_3,
		Ranke_Diamond_2,
		Ranke_Diamond_1,
		Ranke_Champion_5,
		Ranke_Champion_4,
		Ranke_Champion_3,
		Ranke_Champion_2,
		Ranke_Champion_1,
		Global_Popup_AllStarCharacter,
		QuestScene_Shipping,
		QuestScene_ShippingComplete,
		LobbyScene_Survival,
		Global_Button_Receive,
		RankingRewardScene_Box,
		Character_Name_Trex,
		Character_Name_Crocodile,
		Character_Name_Tamerabbit,
		Character_Name_Hare,
		Character_Name_Deer,
		Character_Name_Reindeer,
		Character_Name_Milkcow,
		Character_Name_Bull,
		Character_Name_Horse,
		Character_Name_Zebra,
		Game_DoubleKill,
		Game_TripleKill,
		Game_QuadraKill,
		Game_LegendaryKill,
		Game_TimeOver,
		Tutorial_Title,
		Global_Button_Next,
		Global_Button_End,
		Global_Popup_Notice,
		Global_Popup_Confirm,
		SceneAccount_Term_Title,
		SceneAccount_Term_Description,
		SceneAccount_Term_TermOfUse,
		SceneAccount_Term_PrivacyPolicy,
		Global_Button_Agree,
		SceneAccount_Login_Title,
		Global_Popup_TermsOfUse_NeedAgree,
		Global_Popup_PrivacyPolicy_NeedAgree,
		Global_Popup_Terms_Cancel,
		CharacterScene_HaveCharacter,
		CharacterScene_NotHaveCharacter,
		RankingServer_Error_Connection,
		RankingServer_Error_Unlink,
		SceneMyInfo_Title,
		SceneLobby_BtnText_Character,
		SceneLobby_BtnText_Shop,
		SceneLobby_BtnText_Quest,
		SceneLobby_BtnText_Ranking,
		Character_Description_Kiwibird,
		Character_Description_Chick,
		Character_Description_Duckling,
		Character_Description_ShibaInu,
		Character_Description_Poodle,
		Character_Description_Turtle,
		Character_Description_Panda,
		Character_Description_Polarbear,
		Character_Description_Brownbear,
		Character_Description_Gorilla,
		Character_Description_YeTigorilla,
		Character_Description_Greenfrog,
		Character_Description_Hen,
		Character_Description_Rooster,
		Character_Description_Crowtit,
		Character_Description_Hummingbird,
		Character_Description_Gull,
		Character_Description_Macaw,
		Character_Description_Woodpecker,
		Character_Description_BigbilledBird,
		Character_Description_Eagle,
		Character_Description_Owl,
		Character_Description_Trex,
		Character_Description_Crocodile,
		Character_Description_Tamerabbit,
		Character_Description_Hare,
		Character_Description_Deer,
		Character_Description_Reindeer,
		Character_Description_Milkcow,
		Character_Description_Bull,
		Character_Description_Horse,
		Character_Description_Zebra,
		SceneMyInfo_NickNameChangeFailed,
		SceneAccount_LoginFailed,
		SceneAccount_LoginCancel,
		SceneAccount_ModuelInitFailed,
		GlobalPopup_Network_Unlink,
		GlobalPopup_Network_Error,
		GlobalPopup_Network_LinkFailed,
		SceneMyInfo_NickNameMakeFailed,
		ResultScene_Text_Draw,
		Global_Button_Change,
		SceneAccount_CautionText,
		Global_Button_QuickStart,
		Global_Button_AccountLink,
		SceneAccount_GuestLogin,
		GlobalPopup_InvalidNickLength,
		Game_FirstHit,
		Quest_BlowBalloon,
		Quest_PlayNormal,
		Quest_PlayRare,
		Quest_PlayEpic,
		Have_ForbiddenWord,
		SceneSetting_Notice,
		GlobalPopup_TermOfUse,
		SceneSetting_Push,
		SceneBattleEnd_BestPlayer,
		SceneLobby_SinglePlay,
		SceenSingle_Wave,
		SceenSingle_Time,
		SceenSingle_Bonus,
		SceneMyInfo_Uid_Copy,
		ShopScene_Gacha,
		ShopScene_Gold,
		ShopScene_Dia,
		ShopPopup_Text_Cancel,
		ShopPopup_Text_Error_Duplicate,
		ShopPopup_Text_Error_Code,
		ShopGoods_Gold1,
		ShopGoods_Gold2,
		ShopGoods_Gold3,
		ShopGoods_Gold4,
		ShopGoods_Dia1,
		ShopGoods_Dia2,
		ShopGoods_Dia3,
		ShopGoods_Dia4,
		ShopGoods_Dia5,
		ShopGoods_Dia6,
		Global_Text_CP,
		SceneCharacterList_UnLockRank,
		SceneCharacterList_UnLockGacha,
		SceneCharacterList_UnLockEvent,
		Character_Name_Fox,
		Character_Name_Cat,
		Character_Name_Tiger,
		Character_Name_Pig,
		Character_Name_Elephant,
		Character_Description_Fox,
		Character_Description_Cat,
		Character_Description_Tiger,
		Character_Description_Pig,
		Character_Description_Elephant,
		GlobalPopup_Text_InvalidVersion,
		GlobalPopup_Button_Update,
		GlobalPopup_Text_SingleCharge,
		GlobalPopup_Text_AdFailed,
		QuestScene_Text_AdBonus,
		QuestScene_Text_AdBonusAlarm,
		QuestScene_Text_GetReward,
		QuestScene_Text_Title,
		QuestScene_Text_DailyMission,
		QuestScene_Text_DailyMissionWating,
		QuestScene_Text_DailyMissionTimer,
		SceenSingle_Score,
		ShopPopup_Text_CharacterRate,
		RankingScene_Multi,
		QuestScene_DailyQuest_NotComplete,
		Quest_PlaySingle,
		Quest_RechargingSingle,
		Quest_SingleWave,
		Quest_SingleTime,
		Quest_SinglePlayGoldGet,
		Quest_SinglePlayScoreGet,
		Quest_SurvivalPlayRanking,
		Quest_RankPointGet,
		QuestScene_Text_Ad_Button,
		Character_Name_Werewolf,
		Character_Name_Mummy,
		Character_Name_Ghost,
		Character_Name_Witch,
		Character_Name_Jackol,
		Character_Name_Bat,
		Character_Description_Werewolf,
		Character_Description_Mummy,
		Character_Description_Ghost,
		Character_Description_Witch,
		Character_Description_Jackol,
		Character_Description_Bat,
		GlobalPopup_Text_ServerCheck,
		GlobalPopup_Text_NoneCp_Character,
		RankingScene_Text_RankPoint,
		RankingScene_Text_TOP100,
		RankingScene_Text_Point,
		ShopScene_Upscale_Gold,
		ShopScene_Upscale_Dia,
		NickNameChange_Description,
		RankingScene_Text_MyRanking,
		GlobalPopup_SameNick,
		ShopGacha_Event_Halloween,
		Firebase_Login_Duplicate,
		SceneSetting_Coupon_Title,
		SceneSetting_Coupon_Button_Send,
		SceneSetting_Coupon_Placeholder,
		SceneSetting_Coupon_Used,
		SceneSetting_Coupon_Not,
		SceneSetting_Coupon_Duplicate,
		SceneMatching_PlayerSearching,
		SceneMatching_ReadyPlayer,
		LobbyScene_Solo,
		Quest_SoloPlay,
		Quest_SoloVictory,
		Character_Status_Sky,
		Character_Status_Land,
		Character_Status_Stamina,
		Character_Status_Pump,
		SceenSingle_ExitPopup,
		SceenSingle_Replay,
		LobbyScene_SelectMode,
		LobbyScene_FlyAway,
		LobbyScene_Dodge,
		ShopScene_Upscale_DailyFree,
		ShopScene_Upscale_LimitedPackage,
		ShopScene_Upscale_Skin,
		ShopScene_Upscale_Balloon,
		ShopScene_Upscale_Hat,
		ShopScene_Popup_CheckBuy,
		ShopScene_Popup_CheckGacha,
		Tutorial_Text_Complete,
		Tutorial_Popup_Skip,
		SceneSetting_Tutorial_Title,
		Skin_Name_Kiwibird,
		Skin_Name_Chick,
		Skin_Name_Duckling,
		Skin_Name_ShibaInu,
		Skin_Name_Poodle,
		Skin_Name_Turtle,
		Skin_Name_Panda,
		Skin_Name_Polarbear,
		Skin_Name_BrownBear,
		Skin_Name_Gorilla,
		Skin_Name_YeTigorilla,
		Skin_Name_Greenfrog,
		Skin_Name_Hen,
		Skin_Name_Rooster,
		Skin_Name_Crowtit,
		Skin_Name_Hummingbird,
		Skin_Name_Gull,
		Skin_Name_Macaw,
		Skin_Name_Woodpecker,
		Skin_Name_BigbilledBird,
		Skin_Name_Eagle,
		Skin_Name_Owl,
		Skin_Name_Trex,
		Skin_Name_Crocodile,
		Skin_Name_Tamerabbit,
		Skin_Name_Hare,
		Skin_Name_Deer,
		Skin_Name_Reindeer,
		Skin_Name_Milkcow,
		Skin_Name_Bull,
		Skin_Name_Horse,
		Skin_Name_Zebra,
		Skin_Name_Fox,
		Skin_Name_Cat,
		Skin_Name_Tiger,
		Skin_Name_Pig,
		Skin_Name_Elephant,
		Skin_Name_Werewolf,
		Skin_Name_Mummy,
		Skin_Name_Ghost,
		Skin_Name_Witch,
		Skin_Name_Jackol,
		Skin_Name_Bat,
		Tutorial_Text_Start,
		OptionScene_Joypad,
		Character_Description_Bluemon,
		Character_Description_Lemon,
		Character_Description_Santa,
		Character_Description_Snowman,
		Character_Name_Santa,
		Character_Name_Snowman,
		Character_Name_Bluemon,
		Character_Name_Lemon,
		ShopScene_Popup_CheckFreeGacha,
		ShopScene_Popup_CheckAdGacha,
		SceneCharacterList_UnLockShop,
		ShopGoods_Package_Santa,
		ShopGoods_Package_Polarbear,
		ShopGoods_Package_Yetigorilla,
		ShopGoods_Package_Owl,
		ShopGoods_Package_Reindeer,
		ShopGoods_Package_Witch,
		Skin_Name_Santa,
		Skin_Name_Snowman,
		SceneShop_DayTimeText,
		SceneShop_TimeText,
		SceneShop_DailyOpen,
		SceneShop_DailyWait,
		SceneShop_Popup_DailyCountNot,
		MyInfoScene_SoloWin,
		MyInfoScene_DodgeScore,
		MyInfoScene_DodgeTime,
		MyInfoScene_FlyawayScore,
		MyInfoScene_FlyawayCount,
		MyInfoScene_IngameKill,
		MyInfoScene_ConsecutiveKill,
		MyInfoScene_BlowBalloon,
		Quest_IslandCount,
		Quest_PlayIsland,
		Quest_PlayIslandGoldGet,
		Quest_PlayIslandScoreGet,
		Popup_IslandAdReward,
		Popup_DodgeAdReward,
		Popup_DailyMissionAdReward,
		Popup_FreeBoxAdReward,
		Popup_QuestAdChange,
		RankingScene_Popup_Point,
		RankingScene_Popup_EqualPoint,
		RankingScene_Popup_Rate,
		RankingScene_Popup_Ranking,
		RankingScene_Popup_RankingPoint,
		RankingScene_Text_WeeklyRanking,
		RankingScene_Text_NoRecord,
		RankingScene_Popup_RewardMulti,
		RankingScene_Popup_RewardIsland,
		RankingScene_Popup_RewardDodge,
		RankingScene_Popup_RewardPopup,
		SceneSetting_NoticeText,
		GameTip_Island_Step01,
		GameTip_Island_Step02,
		GameTip_Island_Step03,
		GameTip_Island_Step04,
		GameTip_Island_Step05,
		GameTip_Island_Step06,
		GameTip_Island_Step07,
		Quest_CharacterName,
		MainLobby_MainInfoTip,
		MainLobby_MoneyTip,
		MainLobby_ModeTip,
		MainLobby_TrophyTip,
		Character_StatTip,
		Character_SkinTip,
		Mondey_KeyCoinTip,
		Character_KeyCoinBuy,
		Character_ListTip,
		Quest_ListTip,
		Quest_DailyMissionTip,
		RankReward_Tip,
		WeeklyRanking_Tip,
		Shop_PackageTip,
		PopupTitle_Tip,
		GameTip_Island_TipOff,
		Global_Popup_AllStarCharacterBuy,
		Global_Popup_GuestBuy,
		OptionScene_Language_India,
		SceneAccount_Term_View,
		RankingScene_Popup_TimeLine,
		RankingReward_Popup_Failed,
		Global_Button_Buy,
		SystemNotice_ServerHolding,
		SystemNotice_ServerUndergoing,
		SystemAlarm_ServerCheck,
		SystemAlarm_ServerTask,
		SystemAlarm_ServerEmergency,
		SystemAlarm_ServerMaintenance,
		SystemNotice_ServerEmergency,
		SystemNotice_ServerMaintenance,
		LobbyScene_3PSurvival,
		Quest_Survival3PPlay,
		Quest_Survival3PVictory,
		Global_Popup_News,
		Global_Popup_NewsText,
		Skin_Name_Raccoon,
		Skin_Name_Penguin,
		Skin_Name_Mouse,
		Skin_Name_Squirrel,
		Skin_Name_Hippo,
		Skin_Name_Walrus,
		Character_Name_Raccoon,
		Character_Name_Penguin,
		Character_Name_Mouse,
		Character_Name_Squirrel,
		Character_Name_Hippo,
		Character_Name_Walrus,
		Character_Description_Raccoon,
		Character_Description_Penguin,
		Character_Description_Mouse,
		Character_Description_Squirrel,
		Character_Description_Hippo,
		Character_Description_Walrus,
		LobbyScene_TeamSmall,
		Character_Name_Killerbee,
		Character_Name_Bee,
		Character_Name_Beetle,
		Character_Name_Mantis,
		Character_Name_Ladybugs,
		Character_Name_Stagbeetle,
		Character_Description_Killerbee,
		Character_Description_Bee,
		Character_Description_Beetle,
		Character_Description_Mantis,
		Character_Description_Ladybugs,
		Character_Description_Stagbeetle,
		Skin_Name_Killerbee,
		Skin_Name_Bee,
		Skin_Name_Beetle,
		Skin_Name_Mantis,
		Skin_Name_Ladybugs,
		Skin_Name_Stagbeetle,
		LobbyScene_FlyAwayRace,
		LobbyScene_DodgeArrowRace,
		Quest_MultiSinglePlay,
		Quest_MultiIslandPlay,
		ShopGoods_Package_Killerbee,
		ShopGoods_Package_Bee,
		ShopGoods_Package_Beetle,
		ShopGoods_Package_Mantis,
		ShopGoods_Package_Ladybugs,
		ShopGoods_Package_Stagbeetle,
		MuiltiItem_Name_Ink,
		MuiltiItem_Name_Scale,
		MuiltiItem_Name_Slow,
		LobbyScene_Event,
		LobbyScene_Event_Survival,
		LobbyScene_Event_Team,
		OptionScene_UpdatingInfo,
		ShopGoods_Dia7,
		Global_Button_Exit,
		LobbyScene_MultiPlay,
		MultiScene_Owner,
		MultiScene_Enter,
		MultiScene_Create,
		MultiScene_QuickEnter,
		MultiScene_Private,
		MultiScene_Individual,
		MultiScene_Team,
		MultiScene_Solo,
		MultiScene_Promote,
		MultiScene_Text_GameStart,
		MultiScne_Popup_SelectPublic,
		MultiScne_Popup_Public,
		MultiScne_Popup_Private,
		MultiScne_Popup_Password,
		MultiScne_Popup_Text_Password,
		MultiScne_Popup_SelectMode,
		MultiScne_Popup_SelectPlayMode,
		Character_Name_Uni_Kiwibird,
		Character_Name_Uni_Chick,
		Character_Name_Uni_Duckling,
		Character_Name_Uni_Poodle,
		Character_Name_Uni_Greenfrog,
		Character_Name_Uni_Crowtit,
		Character_Name_Uni_Hummingbird,
		Character_Name_Uni_Macaw,
		Character_Name_Uni_Tamerabbit,
		Character_Description_Uni_Kiwibird,
		Character_Description_Uni_Chick,
		Character_Description_Uni_Duckling,
		Character_Description_Uni_Poodle,
		Character_Description_Uni_Greenfrog,
		Character_Description_Uni_Crowtit,
		Character_Description_Uni_Hummingbird,
		Character_Description_Uni_Macaw,
		Character_Description_Uni_Tamerabbit,
		Character_Requirement_Uni_Kiwibird,
		Character_Requirement_Uni_Chick,
		Character_Requirement_Uni_Duckling,
		Character_Requirement_Uni_Poodle,
		Character_Requirement_Uni_Greenfrog,
		Character_Requirement_Uni_Crowtit,
		Character_Requirement_Uni_Hummingbird,
		Character_Requirement_Uni_Macaw,
		Character_Requirement_Uni_Tamerabbit,
		MultiScne_Popup_Info,
		MultiScene_All,
		MultiScene_Send,
		MultiScene_Chat,
		MultiScene_Popup_PwFailed,
		MultiScene_Popup_RoomFailed,
		MultiScene_Popup_Start,
		Global_Grade_Unique,
		MultiScene_Popup_ErrorQuickEnter,
		System_Chat_GameStart,
		MultiScene_Text_NoRoom,
		Global_Button_Back,
		Global_Text_CharacterInfo,
		CharacterSelectScene_Text_Tip,
		Ranking_Popup_TimeLine,
		Ranking_Popup_RewardInfo,
		CharacterSelectScene_Text_Tip2,
		ResultScene_Text_AutoExit,
		LobbyScene_Text_StartRooom,
		ShopScene_DailyFreeBox,
		ShopScene_DailyFreeBoxText,
		Global_Popup_CpNotEnough,
		ShopScene_GachaOverlapText,
		Account_LogIn_Apple,
		Account_LogIn_Google,
		Account_LogIn_FaceBook,
		Account_LogInCompleted_Apple,
		Account_LogInCompleted_Google,
		Account_LogInCompleted_FaceBook,
		Account_LogOut,
		Account_LogInCompleted,
		Account_LogOut_PopUp_Text,
		Account_SignIn_PopUp_Text,
		Character_Name_Fairy,
		Character_Name_Knight,
		Character_Name_Wizard,
		Character_Name_Princess,
		Character_Name_Dragon,
		Character_Description_Fairy,
		Character_Description_Knight,
		Character_Description_Wizard,
		Character_Description_Princess,
		Character_Description_Dragon,
		ShopGoods_Package_Fairy,
		ShopGoods_Package_Knight,
		ShopGoods_Package_Wizard,
		ShopGoods_Package_Princess,
		ShopGoods_Package_Dragon,
		Skin_Name_Fairy,
		Skin_Name_Knight,
		Skin_Name_Wizard,
		Skin_Name_Princess,
		Skin_Name_Dragon,
		Global_Popup_CloseService,
		NickNameAlreadyExists,
		Max,
		Null=-1,
	}
	public enum EShopType : Byte
	{
		Gacha,
		Gold,
		Dia,
		Max,
	}
	public enum EPlayMode
	{
		Solo,
		Team,
		Survival,
		SurvivalSmall,
		TeamSmall,
		IslandSolo,
		DodgeSolo,
		Island=10,
		Dodge,
		Max,
		Null=-1,
	}
	public enum EMultiItemType
	{
		Null,
		Ink,
		Scale,
		Slow,
		Max,
	}
	public enum EStatusType
	{
		Null,
		Sky,
		Land,
		Stamina,
		Pump,
		Max,
	}
	public class STextSet : SProto
	{
		public String[] Texts = new String[19];
		public STextSet()
		{
			for (int iTexts = 0; iTexts < Texts.Length; ++iTexts)
				Texts[iTexts] = string.Empty;
		}
		public STextSet(STextSet Obj_)
		{
			Texts = Obj_.Texts;
		}
		public STextSet(String[] Texts_)
		{
			Texts = Texts_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Texts);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Texts", ref Texts);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Texts);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Texts", Texts);
		}
		public void Set(STextSet Obj_)
		{
			Texts = Obj_.Texts;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Texts);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Texts, "Texts");
		}
	}
	public class STextMeta : SProto
	{
		public EText TextName = default(EText);
		public String[] Texts = new String[19];
		public STextMeta()
		{
			for (int iTexts = 0; iTexts < Texts.Length; ++iTexts)
				Texts[iTexts] = string.Empty;
		}
		public STextMeta(STextMeta Obj_)
		{
			TextName = Obj_.TextName;
			Texts = Obj_.Texts;
		}
		public STextMeta(EText TextName_, String[] Texts_)
		{
			TextName = TextName_;
			Texts = Texts_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref TextName);
			Stream_.Pop(ref Texts);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("TextName", ref TextName);
			Value_.Pop("Texts", ref Texts);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(TextName);
			Stream_.Push(Texts);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("TextName", TextName);
			Value_.Push("Texts", Texts);
		}
		public void Set(STextMeta Obj_)
		{
			TextName = Obj_.TextName;
			Texts = Obj_.Texts;
		}
		public override string StdName()
		{
			return 
				"bb.EText" + "," + 
				SEnumChecker.GetStdName(Texts);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(TextName, "TextName") + "," + 
				SEnumChecker.GetMemberName(Texts, "Texts");
		}
	}
	public class SGameRetMeta : SProto
	{
		public EGameRet GameRetName = default(EGameRet);
		public String[] Texts = new String[19];
		public SGameRetMeta()
		{
			for (int iTexts = 0; iTexts < Texts.Length; ++iTexts)
				Texts[iTexts] = string.Empty;
		}
		public SGameRetMeta(SGameRetMeta Obj_)
		{
			GameRetName = Obj_.GameRetName;
			Texts = Obj_.Texts;
		}
		public SGameRetMeta(EGameRet GameRetName_, String[] Texts_)
		{
			GameRetName = GameRetName_;
			Texts = Texts_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref GameRetName);
			Stream_.Pop(ref Texts);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("GameRetName", ref GameRetName);
			Value_.Pop("Texts", ref Texts);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(GameRetName);
			Stream_.Push(Texts);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("GameRetName", GameRetName);
			Value_.Push("Texts", Texts);
		}
		public void Set(SGameRetMeta Obj_)
		{
			GameRetName = Obj_.GameRetName;
			Texts = Obj_.Texts;
		}
		public override string StdName()
		{
			return 
				"rso.game.EGameRet" + "," + 
				SEnumChecker.GetStdName(Texts);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(GameRetName, "GameRetName") + "," + 
				SEnumChecker.GetMemberName(Texts, "Texts");
		}
	}
	public class SCharacterClientMeta : SCharacterMeta
	{
		public EText ETextName = default(EText);
		public EText ETextDescription = default(EText);
		public EText ETextRequirement = default(EText);
		public String PrefabName = string.Empty;
		public String DescriptionIcon = string.Empty;
		public String IconName = string.Empty;
		public EStatusType Post_Status = default(EStatusType);
		public Int32 SkyStatus = default(Int32);
		public Int32 LandStatus = default(Int32);
		public Int32 StaminaStatus = default(Int32);
		public Int32 PumpStatus = default(Int32);
		public SCharacterClientMeta()
		{
		}
		public SCharacterClientMeta(SCharacterClientMeta Obj_) : base(Obj_)
		{
			ETextName = Obj_.ETextName;
			ETextDescription = Obj_.ETextDescription;
			ETextRequirement = Obj_.ETextRequirement;
			PrefabName = Obj_.PrefabName;
			DescriptionIcon = Obj_.DescriptionIcon;
			IconName = Obj_.IconName;
			Post_Status = Obj_.Post_Status;
			SkyStatus = Obj_.SkyStatus;
			LandStatus = Obj_.LandStatus;
			StaminaStatus = Obj_.StaminaStatus;
			PumpStatus = Obj_.PumpStatus;
		}
		public SCharacterClientMeta(SCharacterMeta Super_, EText ETextName_, EText ETextDescription_, EText ETextRequirement_, String PrefabName_, String DescriptionIcon_, String IconName_, EStatusType Post_Status_, Int32 SkyStatus_, Int32 LandStatus_, Int32 StaminaStatus_, Int32 PumpStatus_) : base(Super_)
		{
			ETextName = ETextName_;
			ETextDescription = ETextDescription_;
			ETextRequirement = ETextRequirement_;
			PrefabName = PrefabName_;
			DescriptionIcon = DescriptionIcon_;
			IconName = IconName_;
			Post_Status = Post_Status_;
			SkyStatus = SkyStatus_;
			LandStatus = LandStatus_;
			StaminaStatus = StaminaStatus_;
			PumpStatus = PumpStatus_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref ETextName);
			Stream_.Pop(ref ETextDescription);
			Stream_.Pop(ref ETextRequirement);
			Stream_.Pop(ref PrefabName);
			Stream_.Pop(ref DescriptionIcon);
			Stream_.Pop(ref IconName);
			Stream_.Pop(ref Post_Status);
			Stream_.Pop(ref SkyStatus);
			Stream_.Pop(ref LandStatus);
			Stream_.Pop(ref StaminaStatus);
			Stream_.Pop(ref PumpStatus);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("ETextName", ref ETextName);
			Value_.Pop("ETextDescription", ref ETextDescription);
			Value_.Pop("ETextRequirement", ref ETextRequirement);
			Value_.Pop("PrefabName", ref PrefabName);
			Value_.Pop("DescriptionIcon", ref DescriptionIcon);
			Value_.Pop("IconName", ref IconName);
			Value_.Pop("Post_Status", ref Post_Status);
			Value_.Pop("SkyStatus", ref SkyStatus);
			Value_.Pop("LandStatus", ref LandStatus);
			Value_.Pop("StaminaStatus", ref StaminaStatus);
			Value_.Pop("PumpStatus", ref PumpStatus);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(ETextName);
			Stream_.Push(ETextDescription);
			Stream_.Push(ETextRequirement);
			Stream_.Push(PrefabName);
			Stream_.Push(DescriptionIcon);
			Stream_.Push(IconName);
			Stream_.Push(Post_Status);
			Stream_.Push(SkyStatus);
			Stream_.Push(LandStatus);
			Stream_.Push(StaminaStatus);
			Stream_.Push(PumpStatus);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("ETextName", ETextName);
			Value_.Push("ETextDescription", ETextDescription);
			Value_.Push("ETextRequirement", ETextRequirement);
			Value_.Push("PrefabName", PrefabName);
			Value_.Push("DescriptionIcon", DescriptionIcon);
			Value_.Push("IconName", IconName);
			Value_.Push("Post_Status", Post_Status);
			Value_.Push("SkyStatus", SkyStatus);
			Value_.Push("LandStatus", LandStatus);
			Value_.Push("StaminaStatus", StaminaStatus);
			Value_.Push("PumpStatus", PumpStatus);
		}
		public void Set(SCharacterClientMeta Obj_)
		{
			base.Set(Obj_);
			ETextName = Obj_.ETextName;
			ETextDescription = Obj_.ETextDescription;
			ETextRequirement = Obj_.ETextRequirement;
			PrefabName = Obj_.PrefabName;
			DescriptionIcon = Obj_.DescriptionIcon;
			IconName = Obj_.IconName;
			Post_Status = Obj_.Post_Status;
			SkyStatus = Obj_.SkyStatus;
			LandStatus = Obj_.LandStatus;
			StaminaStatus = Obj_.StaminaStatus;
			PumpStatus = Obj_.PumpStatus;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				"bb.EText" + "," + 
				"bb.EText" + "," + 
				"bb.EText" + "," + 
				SEnumChecker.GetStdName(PrefabName) + "," + 
				SEnumChecker.GetStdName(DescriptionIcon) + "," + 
				SEnumChecker.GetStdName(IconName) + "," + 
				"bb.EStatusType" + "," + 
				SEnumChecker.GetStdName(SkyStatus) + "," + 
				SEnumChecker.GetStdName(LandStatus) + "," + 
				SEnumChecker.GetStdName(StaminaStatus) + "," + 
				SEnumChecker.GetStdName(PumpStatus);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(ETextName, "ETextName") + "," + 
				SEnumChecker.GetMemberName(ETextDescription, "ETextDescription") + "," + 
				SEnumChecker.GetMemberName(ETextRequirement, "ETextRequirement") + "," + 
				SEnumChecker.GetMemberName(PrefabName, "PrefabName") + "," + 
				SEnumChecker.GetMemberName(DescriptionIcon, "DescriptionIcon") + "," + 
				SEnumChecker.GetMemberName(IconName, "IconName") + "," + 
				SEnumChecker.GetMemberName(Post_Status, "Post_Status") + "," + 
				SEnumChecker.GetMemberName(SkyStatus, "SkyStatus") + "," + 
				SEnumChecker.GetMemberName(LandStatus, "LandStatus") + "," + 
				SEnumChecker.GetMemberName(StaminaStatus, "StaminaStatus") + "," + 
				SEnumChecker.GetMemberName(PumpStatus, "PumpStatus");
		}
	}
	public class SCharacterGradeClientMeta : SCharacterGradeMeta
	{
		public EText ETextGradeName = default(EText);
		public Int32 ColorR = default(Int32);
		public Int32 ColorG = default(Int32);
		public Int32 ColorB = default(Int32);
		public SCharacterGradeClientMeta()
		{
		}
		public SCharacterGradeClientMeta(SCharacterGradeClientMeta Obj_) : base(Obj_)
		{
			ETextGradeName = Obj_.ETextGradeName;
			ColorR = Obj_.ColorR;
			ColorG = Obj_.ColorG;
			ColorB = Obj_.ColorB;
		}
		public SCharacterGradeClientMeta(SCharacterGradeMeta Super_, EText ETextGradeName_, Int32 ColorR_, Int32 ColorG_, Int32 ColorB_) : base(Super_)
		{
			ETextGradeName = ETextGradeName_;
			ColorR = ColorR_;
			ColorG = ColorG_;
			ColorB = ColorB_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref ETextGradeName);
			Stream_.Pop(ref ColorR);
			Stream_.Pop(ref ColorG);
			Stream_.Pop(ref ColorB);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("ETextGradeName", ref ETextGradeName);
			Value_.Pop("ColorR", ref ColorR);
			Value_.Pop("ColorG", ref ColorG);
			Value_.Pop("ColorB", ref ColorB);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(ETextGradeName);
			Stream_.Push(ColorR);
			Stream_.Push(ColorG);
			Stream_.Push(ColorB);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("ETextGradeName", ETextGradeName);
			Value_.Push("ColorR", ColorR);
			Value_.Push("ColorG", ColorG);
			Value_.Push("ColorB", ColorB);
		}
		public void Set(SCharacterGradeClientMeta Obj_)
		{
			base.Set(Obj_);
			ETextGradeName = Obj_.ETextGradeName;
			ColorR = Obj_.ColorR;
			ColorG = Obj_.ColorG;
			ColorB = Obj_.ColorB;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				"bb.EText" + "," + 
				SEnumChecker.GetStdName(ColorR) + "," + 
				SEnumChecker.GetStdName(ColorG) + "," + 
				SEnumChecker.GetStdName(ColorB);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(ETextGradeName, "ETextGradeName") + "," + 
				SEnumChecker.GetMemberName(ColorR, "ColorR") + "," + 
				SEnumChecker.GetMemberName(ColorG, "ColorG") + "," + 
				SEnumChecker.GetMemberName(ColorB, "ColorB");
		}
	}
	public class SGameOption : SProto
	{
		public Boolean IsVibe = default(Boolean);
		public Boolean IsMusic = default(Boolean);
		public Boolean IsSound = default(Boolean);
		public Boolean IsPush = default(Boolean);
		public Boolean IsPad = default(Boolean);
		public Boolean IsTutorial = default(Boolean);
		public ELanguage Language = default(ELanguage);
		public EPlayMode SelectMode = default(EPlayMode);
		public SGameOption()
		{
		}
		public SGameOption(SGameOption Obj_)
		{
			IsVibe = Obj_.IsVibe;
			IsMusic = Obj_.IsMusic;
			IsSound = Obj_.IsSound;
			IsPush = Obj_.IsPush;
			IsPad = Obj_.IsPad;
			IsTutorial = Obj_.IsTutorial;
			Language = Obj_.Language;
			SelectMode = Obj_.SelectMode;
		}
		public SGameOption(Boolean IsVibe_, Boolean IsMusic_, Boolean IsSound_, Boolean IsPush_, Boolean IsPad_, Boolean IsTutorial_, ELanguage Language_, EPlayMode SelectMode_)
		{
			IsVibe = IsVibe_;
			IsMusic = IsMusic_;
			IsSound = IsSound_;
			IsPush = IsPush_;
			IsPad = IsPad_;
			IsTutorial = IsTutorial_;
			Language = Language_;
			SelectMode = SelectMode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref IsVibe);
			Stream_.Pop(ref IsMusic);
			Stream_.Pop(ref IsSound);
			Stream_.Pop(ref IsPush);
			Stream_.Pop(ref IsPad);
			Stream_.Pop(ref IsTutorial);
			Stream_.Pop(ref Language);
			Stream_.Pop(ref SelectMode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("IsVibe", ref IsVibe);
			Value_.Pop("IsMusic", ref IsMusic);
			Value_.Pop("IsSound", ref IsSound);
			Value_.Pop("IsPush", ref IsPush);
			Value_.Pop("IsPad", ref IsPad);
			Value_.Pop("IsTutorial", ref IsTutorial);
			Value_.Pop("Language", ref Language);
			Value_.Pop("SelectMode", ref SelectMode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(IsVibe);
			Stream_.Push(IsMusic);
			Stream_.Push(IsSound);
			Stream_.Push(IsPush);
			Stream_.Push(IsPad);
			Stream_.Push(IsTutorial);
			Stream_.Push(Language);
			Stream_.Push(SelectMode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("IsVibe", IsVibe);
			Value_.Push("IsMusic", IsMusic);
			Value_.Push("IsSound", IsSound);
			Value_.Push("IsPush", IsPush);
			Value_.Push("IsPad", IsPad);
			Value_.Push("IsTutorial", IsTutorial);
			Value_.Push("Language", Language);
			Value_.Push("SelectMode", SelectMode);
		}
		public void Set(SGameOption Obj_)
		{
			IsVibe = Obj_.IsVibe;
			IsMusic = Obj_.IsMusic;
			IsSound = Obj_.IsSound;
			IsPush = Obj_.IsPush;
			IsPad = Obj_.IsPad;
			IsTutorial = Obj_.IsTutorial;
			Language = Obj_.Language;
			SelectMode = Obj_.SelectMode;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(IsVibe) + "," + 
				SEnumChecker.GetStdName(IsMusic) + "," + 
				SEnumChecker.GetStdName(IsSound) + "," + 
				SEnumChecker.GetStdName(IsPush) + "," + 
				SEnumChecker.GetStdName(IsPad) + "," + 
				SEnumChecker.GetStdName(IsTutorial) + "," + 
				"bb.ELanguage" + "," + 
				"bb.EPlayMode";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(IsVibe, "IsVibe") + "," + 
				SEnumChecker.GetMemberName(IsMusic, "IsMusic") + "," + 
				SEnumChecker.GetMemberName(IsSound, "IsSound") + "," + 
				SEnumChecker.GetMemberName(IsPush, "IsPush") + "," + 
				SEnumChecker.GetMemberName(IsPad, "IsPad") + "," + 
				SEnumChecker.GetMemberName(IsTutorial, "IsTutorial") + "," + 
				SEnumChecker.GetMemberName(Language, "Language") + "," + 
				SEnumChecker.GetMemberName(SelectMode, "SelectMode");
		}
	}
	public class SQuestClientMeta : SQuestMeta
	{
		public EText ETextName = default(EText);
		public String IconName = string.Empty;
		public SQuestClientMeta()
		{
		}
		public SQuestClientMeta(SQuestClientMeta Obj_) : base(Obj_)
		{
			ETextName = Obj_.ETextName;
			IconName = Obj_.IconName;
		}
		public SQuestClientMeta(SQuestMeta Super_, EText ETextName_, String IconName_) : base(Super_)
		{
			ETextName = ETextName_;
			IconName = IconName_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref ETextName);
			Stream_.Pop(ref IconName);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("ETextName", ref ETextName);
			Value_.Pop("IconName", ref IconName);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(ETextName);
			Stream_.Push(IconName);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("ETextName", ETextName);
			Value_.Push("IconName", IconName);
		}
		public void Set(SQuestClientMeta Obj_)
		{
			base.Set(Obj_);
			ETextName = Obj_.ETextName;
			IconName = Obj_.IconName;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				"bb.EText" + "," + 
				SEnumChecker.GetStdName(IconName);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(ETextName, "ETextName") + "," + 
				SEnumChecker.GetMemberName(IconName, "IconName");
		}
	}
	public class SShopMeta : SProto
	{
		public EText ETextName = default(EText);
		public String TextureName = string.Empty;
		public SShopMeta()
		{
		}
		public SShopMeta(SShopMeta Obj_)
		{
			ETextName = Obj_.ETextName;
			TextureName = Obj_.TextureName;
		}
		public SShopMeta(EText ETextName_, String TextureName_)
		{
			ETextName = ETextName_;
			TextureName = TextureName_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref ETextName);
			Stream_.Pop(ref TextureName);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("ETextName", ref ETextName);
			Value_.Pop("TextureName", ref TextureName);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(ETextName);
			Stream_.Push(TextureName);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("ETextName", ETextName);
			Value_.Push("TextureName", TextureName);
		}
		public void Set(SShopMeta Obj_)
		{
			ETextName = Obj_.ETextName;
			TextureName = Obj_.TextureName;
		}
		public override string StdName()
		{
			return 
				"bb.EText" + "," + 
				SEnumChecker.GetStdName(TextureName);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(ETextName, "ETextName") + "," + 
				SEnumChecker.GetMemberName(TextureName, "TextureName");
		}
	}
	public class SShopInGameMeta : SShopMeta
	{
		public Int32 Code = default(Int32);
		public EResource CostType = default(EResource);
		public Int32 CostValue = default(Int32);
		public Int32 RewardCode = default(Int32);
		public ETrackingKey AnalyticsKey = default(ETrackingKey);
		public SShopInGameMeta()
		{
		}
		public SShopInGameMeta(SShopInGameMeta Obj_) : base(Obj_)
		{
			Code = Obj_.Code;
			CostType = Obj_.CostType;
			CostValue = Obj_.CostValue;
			RewardCode = Obj_.RewardCode;
			AnalyticsKey = Obj_.AnalyticsKey;
		}
		public SShopInGameMeta(SShopMeta Super_, Int32 Code_, EResource CostType_, Int32 CostValue_, Int32 RewardCode_, ETrackingKey AnalyticsKey_) : base(Super_)
		{
			Code = Code_;
			CostType = CostType_;
			CostValue = CostValue_;
			RewardCode = RewardCode_;
			AnalyticsKey = AnalyticsKey_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref Code);
			Stream_.Pop(ref CostType);
			Stream_.Pop(ref CostValue);
			Stream_.Pop(ref RewardCode);
			Stream_.Pop(ref AnalyticsKey);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("Code", ref Code);
			Value_.Pop("CostType", ref CostType);
			Value_.Pop("CostValue", ref CostValue);
			Value_.Pop("RewardCode", ref RewardCode);
			Value_.Pop("AnalyticsKey", ref AnalyticsKey);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(Code);
			Stream_.Push(CostType);
			Stream_.Push(CostValue);
			Stream_.Push(RewardCode);
			Stream_.Push(AnalyticsKey);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("Code", Code);
			Value_.Push("CostType", CostType);
			Value_.Push("CostValue", CostValue);
			Value_.Push("RewardCode", RewardCode);
			Value_.Push("AnalyticsKey", AnalyticsKey);
		}
		public void Set(SShopInGameMeta Obj_)
		{
			base.Set(Obj_);
			Code = Obj_.Code;
			CostType = Obj_.CostType;
			CostValue = Obj_.CostValue;
			RewardCode = Obj_.RewardCode;
			AnalyticsKey = Obj_.AnalyticsKey;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(Code) + "," + 
				"bb.EResource" + "," + 
				SEnumChecker.GetStdName(CostValue) + "," + 
				SEnumChecker.GetStdName(RewardCode) + "," + 
				"bb.ETrackingKey";
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(CostType, "CostType") + "," + 
				SEnumChecker.GetMemberName(CostValue, "CostValue") + "," + 
				SEnumChecker.GetMemberName(RewardCode, "RewardCode") + "," + 
				SEnumChecker.GetMemberName(AnalyticsKey, "AnalyticsKey");
		}
	}
	public class SShopIAPMeta : SShopMeta
	{
		public String Pid = string.Empty;
		public Int32 DiaCount = default(Int32);
		public SShopIAPMeta()
		{
		}
		public SShopIAPMeta(SShopIAPMeta Obj_) : base(Obj_)
		{
			Pid = Obj_.Pid;
			DiaCount = Obj_.DiaCount;
		}
		public SShopIAPMeta(SShopMeta Super_, String Pid_, Int32 DiaCount_) : base(Super_)
		{
			Pid = Pid_;
			DiaCount = DiaCount_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref Pid);
			Stream_.Pop(ref DiaCount);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("Pid", ref Pid);
			Value_.Pop("DiaCount", ref DiaCount);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(Pid);
			Stream_.Push(DiaCount);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("Pid", Pid);
			Value_.Push("DiaCount", DiaCount);
		}
		public void Set(SShopIAPMeta Obj_)
		{
			base.Set(Obj_);
			Pid = Obj_.Pid;
			DiaCount = Obj_.DiaCount;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(Pid) + "," + 
				SEnumChecker.GetStdName(DiaCount);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(Pid, "Pid") + "," + 
				SEnumChecker.GetMemberName(DiaCount, "DiaCount");
		}
	}
	public class SGachaClientMeta : SGachaMeta
	{
		public String TextureName = string.Empty;
		public Boolean IsEvent = default(Boolean);
		public EText EventTextName = default(EText);
		public SGachaClientMeta()
		{
		}
		public SGachaClientMeta(SGachaClientMeta Obj_) : base(Obj_)
		{
			TextureName = Obj_.TextureName;
			IsEvent = Obj_.IsEvent;
			EventTextName = Obj_.EventTextName;
		}
		public SGachaClientMeta(SGachaMeta Super_, String TextureName_, Boolean IsEvent_, EText EventTextName_) : base(Super_)
		{
			TextureName = TextureName_;
			IsEvent = IsEvent_;
			EventTextName = EventTextName_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref TextureName);
			Stream_.Pop(ref IsEvent);
			Stream_.Pop(ref EventTextName);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("TextureName", ref TextureName);
			Value_.Pop("IsEvent", ref IsEvent);
			Value_.Pop("EventTextName", ref EventTextName);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(TextureName);
			Stream_.Push(IsEvent);
			Stream_.Push(EventTextName);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("TextureName", TextureName);
			Value_.Push("IsEvent", IsEvent);
			Value_.Push("EventTextName", EventTextName);
		}
		public void Set(SGachaClientMeta Obj_)
		{
			base.Set(Obj_);
			TextureName = Obj_.TextureName;
			IsEvent = Obj_.IsEvent;
			EventTextName = Obj_.EventTextName;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(TextureName) + "," + 
				SEnumChecker.GetStdName(IsEvent) + "," + 
				"bb.EText";
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(TextureName, "TextureName") + "," + 
				SEnumChecker.GetMemberName(IsEvent, "IsEvent") + "," + 
				SEnumChecker.GetMemberName(EventTextName, "EventTextName");
		}
	}
	public class SRankTierClientMeta : SRankTierMeta
	{
		public Int32 Level = default(Int32);
		public EText ETextName = default(EText);
		public String TextureName = string.Empty;
		public Single RankColorR = default(Single);
		public Single RankColorG = default(Single);
		public Single RankColorB = default(Single);
		public SRankTierClientMeta()
		{
		}
		public SRankTierClientMeta(SRankTierClientMeta Obj_) : base(Obj_)
		{
			Level = Obj_.Level;
			ETextName = Obj_.ETextName;
			TextureName = Obj_.TextureName;
			RankColorR = Obj_.RankColorR;
			RankColorG = Obj_.RankColorG;
			RankColorB = Obj_.RankColorB;
		}
		public SRankTierClientMeta(SRankTierMeta Super_, Int32 Level_, EText ETextName_, String TextureName_, Single RankColorR_, Single RankColorG_, Single RankColorB_) : base(Super_)
		{
			Level = Level_;
			ETextName = ETextName_;
			TextureName = TextureName_;
			RankColorR = RankColorR_;
			RankColorG = RankColorG_;
			RankColorB = RankColorB_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref Level);
			Stream_.Pop(ref ETextName);
			Stream_.Pop(ref TextureName);
			Stream_.Pop(ref RankColorR);
			Stream_.Pop(ref RankColorG);
			Stream_.Pop(ref RankColorB);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("Level", ref Level);
			Value_.Pop("ETextName", ref ETextName);
			Value_.Pop("TextureName", ref TextureName);
			Value_.Pop("RankColorR", ref RankColorR);
			Value_.Pop("RankColorG", ref RankColorG);
			Value_.Pop("RankColorB", ref RankColorB);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(Level);
			Stream_.Push(ETextName);
			Stream_.Push(TextureName);
			Stream_.Push(RankColorR);
			Stream_.Push(RankColorG);
			Stream_.Push(RankColorB);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("Level", Level);
			Value_.Push("ETextName", ETextName);
			Value_.Push("TextureName", TextureName);
			Value_.Push("RankColorR", RankColorR);
			Value_.Push("RankColorG", RankColorG);
			Value_.Push("RankColorB", RankColorB);
		}
		public void Set(SRankTierClientMeta Obj_)
		{
			base.Set(Obj_);
			Level = Obj_.Level;
			ETextName = Obj_.ETextName;
			TextureName = Obj_.TextureName;
			RankColorR = Obj_.RankColorR;
			RankColorG = Obj_.RankColorG;
			RankColorB = Obj_.RankColorB;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(Level) + "," + 
				"bb.EText" + "," + 
				SEnumChecker.GetStdName(TextureName) + "," + 
				SEnumChecker.GetStdName(RankColorR) + "," + 
				SEnumChecker.GetStdName(RankColorG) + "," + 
				SEnumChecker.GetStdName(RankColorB);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(Level, "Level") + "," + 
				SEnumChecker.GetMemberName(ETextName, "ETextName") + "," + 
				SEnumChecker.GetMemberName(TextureName, "TextureName") + "," + 
				SEnumChecker.GetMemberName(RankColorR, "RankColorR") + "," + 
				SEnumChecker.GetMemberName(RankColorG, "RankColorG") + "," + 
				SEnumChecker.GetMemberName(RankColorB, "RankColorB");
		}
	}
	public class SRankRewardViewMeta : SProto
	{
		public String TextureName = string.Empty;
		public EText ETextName = default(EText);
		public Int32 Count = default(Int32);
		public SRankRewardViewMeta()
		{
		}
		public SRankRewardViewMeta(SRankRewardViewMeta Obj_)
		{
			TextureName = Obj_.TextureName;
			ETextName = Obj_.ETextName;
			Count = Obj_.Count;
		}
		public SRankRewardViewMeta(String TextureName_, EText ETextName_, Int32 Count_)
		{
			TextureName = TextureName_;
			ETextName = ETextName_;
			Count = Count_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref TextureName);
			Stream_.Pop(ref ETextName);
			Stream_.Pop(ref Count);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("TextureName", ref TextureName);
			Value_.Pop("ETextName", ref ETextName);
			Value_.Pop("Count", ref Count);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(TextureName);
			Stream_.Push(ETextName);
			Stream_.Push(Count);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("TextureName", TextureName);
			Value_.Push("ETextName", ETextName);
			Value_.Push("Count", Count);
		}
		public void Set(SRankRewardViewMeta Obj_)
		{
			TextureName = Obj_.TextureName;
			ETextName = Obj_.ETextName;
			Count = Obj_.Count;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(TextureName) + "," + 
				"bb.EText" + "," + 
				SEnumChecker.GetStdName(Count);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(TextureName, "TextureName") + "," + 
				SEnumChecker.GetMemberName(ETextName, "ETextName") + "," + 
				SEnumChecker.GetMemberName(Count, "Count");
		}
	}
	public class SRankRewardViewPackMeta : SProto
	{
		public Int32 Code = default(Int32);
		public SRankRewardViewMeta RankRewardViewMeta = new SRankRewardViewMeta();
		public SRankRewardViewPackMeta()
		{
		}
		public SRankRewardViewPackMeta(SRankRewardViewPackMeta Obj_)
		{
			Code = Obj_.Code;
			RankRewardViewMeta = Obj_.RankRewardViewMeta;
		}
		public SRankRewardViewPackMeta(Int32 Code_, SRankRewardViewMeta RankRewardViewMeta_)
		{
			Code = Code_;
			RankRewardViewMeta = RankRewardViewMeta_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Code);
			Stream_.Pop(ref RankRewardViewMeta);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Code", ref Code);
			Value_.Pop("RankRewardViewMeta", ref RankRewardViewMeta);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Code);
			Stream_.Push(RankRewardViewMeta);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Code", Code);
			Value_.Push("RankRewardViewMeta", RankRewardViewMeta);
		}
		public void Set(SRankRewardViewPackMeta Obj_)
		{
			Code = Obj_.Code;
			RankRewardViewMeta.Set(Obj_.RankRewardViewMeta);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Code) + "," + 
				SEnumChecker.GetStdName(RankRewardViewMeta);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Code, "Code") + "," + 
				SEnumChecker.GetMemberName(RankRewardViewMeta, "RankRewardViewMeta");
		}
	}
	public class SBattleTypeMeta : SBattleType
	{
		public Boolean IsServival = default(Boolean);
		public Boolean IsSingle = default(Boolean);
		public SBattleTypeMeta()
		{
		}
		public SBattleTypeMeta(SBattleTypeMeta Obj_) : base(Obj_)
		{
			IsServival = Obj_.IsServival;
			IsSingle = Obj_.IsSingle;
		}
		public SBattleTypeMeta(SBattleType Super_, Boolean IsServival_, Boolean IsSingle_) : base(Super_)
		{
			IsServival = IsServival_;
			IsSingle = IsSingle_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref IsServival);
			Stream_.Pop(ref IsSingle);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("IsServival", ref IsServival);
			Value_.Pop("IsSingle", ref IsSingle);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(IsServival);
			Stream_.Push(IsSingle);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("IsServival", IsServival);
			Value_.Push("IsSingle", IsSingle);
		}
		public void Set(SBattleTypeMeta Obj_)
		{
			base.Set(Obj_);
			IsServival = Obj_.IsServival;
			IsSingle = Obj_.IsSingle;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(IsServival) + "," + 
				SEnumChecker.GetStdName(IsSingle);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(IsServival, "IsServival") + "," + 
				SEnumChecker.GetMemberName(IsSingle, "IsSingle");
		}
	}
	public class SSingleBalance : SProto
	{
		public Single MinSpeed = default(Single);
		public Single MaxSpeed = default(Single);
		public Single ShotDelay = default(Single);
		public Int32 TypeFix = default(Int32);
		public Int32 CountFix = default(Int32);
		public Single SpeedFix = default(Single);
		public Int32 Right = default(Int32);
		public Int32 Left = default(Int32);
		public Int32 Bottom = default(Int32);
		public Int32 Diagonal = default(Int32);
		public Int32 FixCount = default(Int32);
		public Int32 FixOnceCount = default(Int32);
		public Int32 BonusTerm = default(Int32);
		public Int32 BonusCointStart = default(Int32);
		public Int32 BonusCoinOnce = default(Int32);
		public Single BonusCoinSpeed = default(Single);
		public Single CoinShotDelay = default(Single);
		public Int32 WaveCountGold = default(Int32);
		public Int32 InitGold = default(Int32);
		public Int32 AddGold = default(Int32);
		public Int32 PlayCountMax = default(Int32);
		public Int32 ChargeCostGold = default(Int32);
		public Int32 ScoreFactorWave = default(Int32);
		public Int32 ScoreFactorTime = default(Int32);
		public Int32 ScoreFactorGold = default(Int32);
		public Int32 RefreshDurationMinute = default(Int32);
		public Int32 StageGoldCount = default(Int32);
		public Int32 StageGoldWave = default(Int32);
		public Int32 StageGoldCountMax = default(Int32);
		public String ItemPattern = string.Empty;
		public Single ShieldTime = default(Single);
		public Single StaminaTime = default(Single);
		public Int32 GoldBarCount = default(Int32);
		public SSingleBalance()
		{
		}
		public SSingleBalance(SSingleBalance Obj_)
		{
			MinSpeed = Obj_.MinSpeed;
			MaxSpeed = Obj_.MaxSpeed;
			ShotDelay = Obj_.ShotDelay;
			TypeFix = Obj_.TypeFix;
			CountFix = Obj_.CountFix;
			SpeedFix = Obj_.SpeedFix;
			Right = Obj_.Right;
			Left = Obj_.Left;
			Bottom = Obj_.Bottom;
			Diagonal = Obj_.Diagonal;
			FixCount = Obj_.FixCount;
			FixOnceCount = Obj_.FixOnceCount;
			BonusTerm = Obj_.BonusTerm;
			BonusCointStart = Obj_.BonusCointStart;
			BonusCoinOnce = Obj_.BonusCoinOnce;
			BonusCoinSpeed = Obj_.BonusCoinSpeed;
			CoinShotDelay = Obj_.CoinShotDelay;
			WaveCountGold = Obj_.WaveCountGold;
			InitGold = Obj_.InitGold;
			AddGold = Obj_.AddGold;
			PlayCountMax = Obj_.PlayCountMax;
			ChargeCostGold = Obj_.ChargeCostGold;
			ScoreFactorWave = Obj_.ScoreFactorWave;
			ScoreFactorTime = Obj_.ScoreFactorTime;
			ScoreFactorGold = Obj_.ScoreFactorGold;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
			StageGoldCount = Obj_.StageGoldCount;
			StageGoldWave = Obj_.StageGoldWave;
			StageGoldCountMax = Obj_.StageGoldCountMax;
			ItemPattern = Obj_.ItemPattern;
			ShieldTime = Obj_.ShieldTime;
			StaminaTime = Obj_.StaminaTime;
			GoldBarCount = Obj_.GoldBarCount;
		}
		public SSingleBalance(Single MinSpeed_, Single MaxSpeed_, Single ShotDelay_, Int32 TypeFix_, Int32 CountFix_, Single SpeedFix_, Int32 Right_, Int32 Left_, Int32 Bottom_, Int32 Diagonal_, Int32 FixCount_, Int32 FixOnceCount_, Int32 BonusTerm_, Int32 BonusCointStart_, Int32 BonusCoinOnce_, Single BonusCoinSpeed_, Single CoinShotDelay_, Int32 WaveCountGold_, Int32 InitGold_, Int32 AddGold_, Int32 PlayCountMax_, Int32 ChargeCostGold_, Int32 ScoreFactorWave_, Int32 ScoreFactorTime_, Int32 ScoreFactorGold_, Int32 RefreshDurationMinute_, Int32 StageGoldCount_, Int32 StageGoldWave_, Int32 StageGoldCountMax_, String ItemPattern_, Single ShieldTime_, Single StaminaTime_, Int32 GoldBarCount_)
		{
			MinSpeed = MinSpeed_;
			MaxSpeed = MaxSpeed_;
			ShotDelay = ShotDelay_;
			TypeFix = TypeFix_;
			CountFix = CountFix_;
			SpeedFix = SpeedFix_;
			Right = Right_;
			Left = Left_;
			Bottom = Bottom_;
			Diagonal = Diagonal_;
			FixCount = FixCount_;
			FixOnceCount = FixOnceCount_;
			BonusTerm = BonusTerm_;
			BonusCointStart = BonusCointStart_;
			BonusCoinOnce = BonusCoinOnce_;
			BonusCoinSpeed = BonusCoinSpeed_;
			CoinShotDelay = CoinShotDelay_;
			WaveCountGold = WaveCountGold_;
			InitGold = InitGold_;
			AddGold = AddGold_;
			PlayCountMax = PlayCountMax_;
			ChargeCostGold = ChargeCostGold_;
			ScoreFactorWave = ScoreFactorWave_;
			ScoreFactorTime = ScoreFactorTime_;
			ScoreFactorGold = ScoreFactorGold_;
			RefreshDurationMinute = RefreshDurationMinute_;
			StageGoldCount = StageGoldCount_;
			StageGoldWave = StageGoldWave_;
			StageGoldCountMax = StageGoldCountMax_;
			ItemPattern = ItemPattern_;
			ShieldTime = ShieldTime_;
			StaminaTime = StaminaTime_;
			GoldBarCount = GoldBarCount_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref MinSpeed);
			Stream_.Pop(ref MaxSpeed);
			Stream_.Pop(ref ShotDelay);
			Stream_.Pop(ref TypeFix);
			Stream_.Pop(ref CountFix);
			Stream_.Pop(ref SpeedFix);
			Stream_.Pop(ref Right);
			Stream_.Pop(ref Left);
			Stream_.Pop(ref Bottom);
			Stream_.Pop(ref Diagonal);
			Stream_.Pop(ref FixCount);
			Stream_.Pop(ref FixOnceCount);
			Stream_.Pop(ref BonusTerm);
			Stream_.Pop(ref BonusCointStart);
			Stream_.Pop(ref BonusCoinOnce);
			Stream_.Pop(ref BonusCoinSpeed);
			Stream_.Pop(ref CoinShotDelay);
			Stream_.Pop(ref WaveCountGold);
			Stream_.Pop(ref InitGold);
			Stream_.Pop(ref AddGold);
			Stream_.Pop(ref PlayCountMax);
			Stream_.Pop(ref ChargeCostGold);
			Stream_.Pop(ref ScoreFactorWave);
			Stream_.Pop(ref ScoreFactorTime);
			Stream_.Pop(ref ScoreFactorGold);
			Stream_.Pop(ref RefreshDurationMinute);
			Stream_.Pop(ref StageGoldCount);
			Stream_.Pop(ref StageGoldWave);
			Stream_.Pop(ref StageGoldCountMax);
			Stream_.Pop(ref ItemPattern);
			Stream_.Pop(ref ShieldTime);
			Stream_.Pop(ref StaminaTime);
			Stream_.Pop(ref GoldBarCount);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("MinSpeed", ref MinSpeed);
			Value_.Pop("MaxSpeed", ref MaxSpeed);
			Value_.Pop("ShotDelay", ref ShotDelay);
			Value_.Pop("TypeFix", ref TypeFix);
			Value_.Pop("CountFix", ref CountFix);
			Value_.Pop("SpeedFix", ref SpeedFix);
			Value_.Pop("Right", ref Right);
			Value_.Pop("Left", ref Left);
			Value_.Pop("Bottom", ref Bottom);
			Value_.Pop("Diagonal", ref Diagonal);
			Value_.Pop("FixCount", ref FixCount);
			Value_.Pop("FixOnceCount", ref FixOnceCount);
			Value_.Pop("BonusTerm", ref BonusTerm);
			Value_.Pop("BonusCointStart", ref BonusCointStart);
			Value_.Pop("BonusCoinOnce", ref BonusCoinOnce);
			Value_.Pop("BonusCoinSpeed", ref BonusCoinSpeed);
			Value_.Pop("CoinShotDelay", ref CoinShotDelay);
			Value_.Pop("WaveCountGold", ref WaveCountGold);
			Value_.Pop("InitGold", ref InitGold);
			Value_.Pop("AddGold", ref AddGold);
			Value_.Pop("PlayCountMax", ref PlayCountMax);
			Value_.Pop("ChargeCostGold", ref ChargeCostGold);
			Value_.Pop("ScoreFactorWave", ref ScoreFactorWave);
			Value_.Pop("ScoreFactorTime", ref ScoreFactorTime);
			Value_.Pop("ScoreFactorGold", ref ScoreFactorGold);
			Value_.Pop("RefreshDurationMinute", ref RefreshDurationMinute);
			Value_.Pop("StageGoldCount", ref StageGoldCount);
			Value_.Pop("StageGoldWave", ref StageGoldWave);
			Value_.Pop("StageGoldCountMax", ref StageGoldCountMax);
			Value_.Pop("ItemPattern", ref ItemPattern);
			Value_.Pop("ShieldTime", ref ShieldTime);
			Value_.Pop("StaminaTime", ref StaminaTime);
			Value_.Pop("GoldBarCount", ref GoldBarCount);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(MinSpeed);
			Stream_.Push(MaxSpeed);
			Stream_.Push(ShotDelay);
			Stream_.Push(TypeFix);
			Stream_.Push(CountFix);
			Stream_.Push(SpeedFix);
			Stream_.Push(Right);
			Stream_.Push(Left);
			Stream_.Push(Bottom);
			Stream_.Push(Diagonal);
			Stream_.Push(FixCount);
			Stream_.Push(FixOnceCount);
			Stream_.Push(BonusTerm);
			Stream_.Push(BonusCointStart);
			Stream_.Push(BonusCoinOnce);
			Stream_.Push(BonusCoinSpeed);
			Stream_.Push(CoinShotDelay);
			Stream_.Push(WaveCountGold);
			Stream_.Push(InitGold);
			Stream_.Push(AddGold);
			Stream_.Push(PlayCountMax);
			Stream_.Push(ChargeCostGold);
			Stream_.Push(ScoreFactorWave);
			Stream_.Push(ScoreFactorTime);
			Stream_.Push(ScoreFactorGold);
			Stream_.Push(RefreshDurationMinute);
			Stream_.Push(StageGoldCount);
			Stream_.Push(StageGoldWave);
			Stream_.Push(StageGoldCountMax);
			Stream_.Push(ItemPattern);
			Stream_.Push(ShieldTime);
			Stream_.Push(StaminaTime);
			Stream_.Push(GoldBarCount);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("MinSpeed", MinSpeed);
			Value_.Push("MaxSpeed", MaxSpeed);
			Value_.Push("ShotDelay", ShotDelay);
			Value_.Push("TypeFix", TypeFix);
			Value_.Push("CountFix", CountFix);
			Value_.Push("SpeedFix", SpeedFix);
			Value_.Push("Right", Right);
			Value_.Push("Left", Left);
			Value_.Push("Bottom", Bottom);
			Value_.Push("Diagonal", Diagonal);
			Value_.Push("FixCount", FixCount);
			Value_.Push("FixOnceCount", FixOnceCount);
			Value_.Push("BonusTerm", BonusTerm);
			Value_.Push("BonusCointStart", BonusCointStart);
			Value_.Push("BonusCoinOnce", BonusCoinOnce);
			Value_.Push("BonusCoinSpeed", BonusCoinSpeed);
			Value_.Push("CoinShotDelay", CoinShotDelay);
			Value_.Push("WaveCountGold", WaveCountGold);
			Value_.Push("InitGold", InitGold);
			Value_.Push("AddGold", AddGold);
			Value_.Push("PlayCountMax", PlayCountMax);
			Value_.Push("ChargeCostGold", ChargeCostGold);
			Value_.Push("ScoreFactorWave", ScoreFactorWave);
			Value_.Push("ScoreFactorTime", ScoreFactorTime);
			Value_.Push("ScoreFactorGold", ScoreFactorGold);
			Value_.Push("RefreshDurationMinute", RefreshDurationMinute);
			Value_.Push("StageGoldCount", StageGoldCount);
			Value_.Push("StageGoldWave", StageGoldWave);
			Value_.Push("StageGoldCountMax", StageGoldCountMax);
			Value_.Push("ItemPattern", ItemPattern);
			Value_.Push("ShieldTime", ShieldTime);
			Value_.Push("StaminaTime", StaminaTime);
			Value_.Push("GoldBarCount", GoldBarCount);
		}
		public void Set(SSingleBalance Obj_)
		{
			MinSpeed = Obj_.MinSpeed;
			MaxSpeed = Obj_.MaxSpeed;
			ShotDelay = Obj_.ShotDelay;
			TypeFix = Obj_.TypeFix;
			CountFix = Obj_.CountFix;
			SpeedFix = Obj_.SpeedFix;
			Right = Obj_.Right;
			Left = Obj_.Left;
			Bottom = Obj_.Bottom;
			Diagonal = Obj_.Diagonal;
			FixCount = Obj_.FixCount;
			FixOnceCount = Obj_.FixOnceCount;
			BonusTerm = Obj_.BonusTerm;
			BonusCointStart = Obj_.BonusCointStart;
			BonusCoinOnce = Obj_.BonusCoinOnce;
			BonusCoinSpeed = Obj_.BonusCoinSpeed;
			CoinShotDelay = Obj_.CoinShotDelay;
			WaveCountGold = Obj_.WaveCountGold;
			InitGold = Obj_.InitGold;
			AddGold = Obj_.AddGold;
			PlayCountMax = Obj_.PlayCountMax;
			ChargeCostGold = Obj_.ChargeCostGold;
			ScoreFactorWave = Obj_.ScoreFactorWave;
			ScoreFactorTime = Obj_.ScoreFactorTime;
			ScoreFactorGold = Obj_.ScoreFactorGold;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
			StageGoldCount = Obj_.StageGoldCount;
			StageGoldWave = Obj_.StageGoldWave;
			StageGoldCountMax = Obj_.StageGoldCountMax;
			ItemPattern = Obj_.ItemPattern;
			ShieldTime = Obj_.ShieldTime;
			StaminaTime = Obj_.StaminaTime;
			GoldBarCount = Obj_.GoldBarCount;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(MinSpeed) + "," + 
				SEnumChecker.GetStdName(MaxSpeed) + "," + 
				SEnumChecker.GetStdName(ShotDelay) + "," + 
				SEnumChecker.GetStdName(TypeFix) + "," + 
				SEnumChecker.GetStdName(CountFix) + "," + 
				SEnumChecker.GetStdName(SpeedFix) + "," + 
				SEnumChecker.GetStdName(Right) + "," + 
				SEnumChecker.GetStdName(Left) + "," + 
				SEnumChecker.GetStdName(Bottom) + "," + 
				SEnumChecker.GetStdName(Diagonal) + "," + 
				SEnumChecker.GetStdName(FixCount) + "," + 
				SEnumChecker.GetStdName(FixOnceCount) + "," + 
				SEnumChecker.GetStdName(BonusTerm) + "," + 
				SEnumChecker.GetStdName(BonusCointStart) + "," + 
				SEnumChecker.GetStdName(BonusCoinOnce) + "," + 
				SEnumChecker.GetStdName(BonusCoinSpeed) + "," + 
				SEnumChecker.GetStdName(CoinShotDelay) + "," + 
				SEnumChecker.GetStdName(WaveCountGold) + "," + 
				SEnumChecker.GetStdName(InitGold) + "," + 
				SEnumChecker.GetStdName(AddGold) + "," + 
				SEnumChecker.GetStdName(PlayCountMax) + "," + 
				SEnumChecker.GetStdName(ChargeCostGold) + "," + 
				SEnumChecker.GetStdName(ScoreFactorWave) + "," + 
				SEnumChecker.GetStdName(ScoreFactorTime) + "," + 
				SEnumChecker.GetStdName(ScoreFactorGold) + "," + 
				SEnumChecker.GetStdName(RefreshDurationMinute) + "," + 
				SEnumChecker.GetStdName(StageGoldCount) + "," + 
				SEnumChecker.GetStdName(StageGoldWave) + "," + 
				SEnumChecker.GetStdName(StageGoldCountMax) + "," + 
				SEnumChecker.GetStdName(ItemPattern) + "," + 
				SEnumChecker.GetStdName(ShieldTime) + "," + 
				SEnumChecker.GetStdName(StaminaTime) + "," + 
				SEnumChecker.GetStdName(GoldBarCount);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(MinSpeed, "MinSpeed") + "," + 
				SEnumChecker.GetMemberName(MaxSpeed, "MaxSpeed") + "," + 
				SEnumChecker.GetMemberName(ShotDelay, "ShotDelay") + "," + 
				SEnumChecker.GetMemberName(TypeFix, "TypeFix") + "," + 
				SEnumChecker.GetMemberName(CountFix, "CountFix") + "," + 
				SEnumChecker.GetMemberName(SpeedFix, "SpeedFix") + "," + 
				SEnumChecker.GetMemberName(Right, "Right") + "," + 
				SEnumChecker.GetMemberName(Left, "Left") + "," + 
				SEnumChecker.GetMemberName(Bottom, "Bottom") + "," + 
				SEnumChecker.GetMemberName(Diagonal, "Diagonal") + "," + 
				SEnumChecker.GetMemberName(FixCount, "FixCount") + "," + 
				SEnumChecker.GetMemberName(FixOnceCount, "FixOnceCount") + "," + 
				SEnumChecker.GetMemberName(BonusTerm, "BonusTerm") + "," + 
				SEnumChecker.GetMemberName(BonusCointStart, "BonusCointStart") + "," + 
				SEnumChecker.GetMemberName(BonusCoinOnce, "BonusCoinOnce") + "," + 
				SEnumChecker.GetMemberName(BonusCoinSpeed, "BonusCoinSpeed") + "," + 
				SEnumChecker.GetMemberName(CoinShotDelay, "CoinShotDelay") + "," + 
				SEnumChecker.GetMemberName(WaveCountGold, "WaveCountGold") + "," + 
				SEnumChecker.GetMemberName(InitGold, "InitGold") + "," + 
				SEnumChecker.GetMemberName(AddGold, "AddGold") + "," + 
				SEnumChecker.GetMemberName(PlayCountMax, "PlayCountMax") + "," + 
				SEnumChecker.GetMemberName(ChargeCostGold, "ChargeCostGold") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorWave, "ScoreFactorWave") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorTime, "ScoreFactorTime") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorGold, "ScoreFactorGold") + "," + 
				SEnumChecker.GetMemberName(RefreshDurationMinute, "RefreshDurationMinute") + "," + 
				SEnumChecker.GetMemberName(StageGoldCount, "StageGoldCount") + "," + 
				SEnumChecker.GetMemberName(StageGoldWave, "StageGoldWave") + "," + 
				SEnumChecker.GetMemberName(StageGoldCountMax, "StageGoldCountMax") + "," + 
				SEnumChecker.GetMemberName(ItemPattern, "ItemPattern") + "," + 
				SEnumChecker.GetMemberName(ShieldTime, "ShieldTime") + "," + 
				SEnumChecker.GetMemberName(StaminaTime, "StaminaTime") + "," + 
				SEnumChecker.GetMemberName(GoldBarCount, "GoldBarCount");
		}
	}
	public class SCheatMeta : SProto
	{
		public String Cheat = string.Empty;
		public String CheatCommand = string.Empty;
		public SCheatMeta()
		{
		}
		public SCheatMeta(SCheatMeta Obj_)
		{
			Cheat = Obj_.Cheat;
			CheatCommand = Obj_.CheatCommand;
		}
		public SCheatMeta(String Cheat_, String CheatCommand_)
		{
			Cheat = Cheat_;
			CheatCommand = CheatCommand_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Cheat);
			Stream_.Pop(ref CheatCommand);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Cheat", ref Cheat);
			Value_.Pop("CheatCommand", ref CheatCommand);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Cheat);
			Stream_.Push(CheatCommand);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Cheat", Cheat);
			Value_.Push("CheatCommand", CheatCommand);
		}
		public void Set(SCheatMeta Obj_)
		{
			Cheat = Obj_.Cheat;
			CheatCommand = Obj_.CheatCommand;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Cheat) + "," + 
				SEnumChecker.GetStdName(CheatCommand);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Cheat, "Cheat") + "," + 
				SEnumChecker.GetMemberName(CheatCommand, "CheatCommand");
		}
	}
	public class STrackingMetaParam : SProto
	{
		public String ParamKey = string.Empty;
		public Int32 ParamValue = default(Int32);
		public STrackingMetaParam()
		{
		}
		public STrackingMetaParam(STrackingMetaParam Obj_)
		{
			ParamKey = Obj_.ParamKey;
			ParamValue = Obj_.ParamValue;
		}
		public STrackingMetaParam(String ParamKey_, Int32 ParamValue_)
		{
			ParamKey = ParamKey_;
			ParamValue = ParamValue_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref ParamKey);
			Stream_.Pop(ref ParamValue);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("ParamKey", ref ParamKey);
			Value_.Pop("ParamValue", ref ParamValue);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(ParamKey);
			Stream_.Push(ParamValue);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("ParamKey", ParamKey);
			Value_.Push("ParamValue", ParamValue);
		}
		public void Set(STrackingMetaParam Obj_)
		{
			ParamKey = Obj_.ParamKey;
			ParamValue = Obj_.ParamValue;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(ParamKey) + "," + 
				SEnumChecker.GetStdName(ParamValue);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(ParamKey, "ParamKey") + "," + 
				SEnumChecker.GetMemberName(ParamValue, "ParamValue");
		}
	}
	public class STrackingMeta : SProto
	{
		public ETrackingKey ETrackingKey = default(ETrackingKey);
		public String EventKey = string.Empty;
		public STrackingMetaParam Param = new STrackingMetaParam();
		public STrackingMeta()
		{
		}
		public STrackingMeta(STrackingMeta Obj_)
		{
			ETrackingKey = Obj_.ETrackingKey;
			EventKey = Obj_.EventKey;
			Param = Obj_.Param;
		}
		public STrackingMeta(ETrackingKey ETrackingKey_, String EventKey_, STrackingMetaParam Param_)
		{
			ETrackingKey = ETrackingKey_;
			EventKey = EventKey_;
			Param = Param_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref ETrackingKey);
			Stream_.Pop(ref EventKey);
			Stream_.Pop(ref Param);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("ETrackingKey", ref ETrackingKey);
			Value_.Pop("EventKey", ref EventKey);
			Value_.Pop("Param", ref Param);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(ETrackingKey);
			Stream_.Push(EventKey);
			Stream_.Push(Param);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("ETrackingKey", ETrackingKey);
			Value_.Push("EventKey", EventKey);
			Value_.Push("Param", Param);
		}
		public void Set(STrackingMeta Obj_)
		{
			ETrackingKey = Obj_.ETrackingKey;
			EventKey = Obj_.EventKey;
			Param.Set(Obj_.Param);
		}
		public override string StdName()
		{
			return 
				"bb.ETrackingKey" + "," + 
				SEnumChecker.GetStdName(EventKey) + "," + 
				SEnumChecker.GetStdName(Param);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(ETrackingKey, "ETrackingKey") + "," + 
				SEnumChecker.GetMemberName(EventKey, "EventKey") + "," + 
				SEnumChecker.GetMemberName(Param, "Param");
		}
	}
	public class SGachaRewardClientMeta : SGachaRewardMeta
	{
		public Boolean Event = default(Boolean);
		public SGachaRewardClientMeta()
		{
		}
		public SGachaRewardClientMeta(SGachaRewardClientMeta Obj_) : base(Obj_)
		{
			Event = Obj_.Event;
		}
		public SGachaRewardClientMeta(SGachaRewardMeta Super_, Boolean Event_) : base(Super_)
		{
			Event = Event_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref Event);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("Event", ref Event);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(Event);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("Event", Event);
		}
		public void Set(SGachaRewardClientMeta Obj_)
		{
			base.Set(Obj_);
			Event = Obj_.Event;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(Event);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(Event, "Event");
		}
	}
	public class SSingleIslandBalance : SProto
	{
		public Single IslandVelocity = default(Single);
		public Single InitTermMin = default(Single);
		public Single InitTermMax = default(Single);
		public Single AddTerm = default(Single);
		public Single InitHeight = default(Single);
		public Single ExcludeHeight = default(Single);
		public Single AddHeight = default(Single);
		public Single AddExcludeHeight = default(Single);
		public Int32 StaminaTerm = default(Int32);
		public Int32 CoinTerm = default(Int32);
		public Int32 GoldBarTerm = default(Int32);
		public Int32 GoldBarCount = default(Int32);
		public Single StaminaBonus = default(Single);
		public Single StaminaMax = default(Single);
		public Single StaminaApple = default(Single);
		public Single StaminaMeat = default(Single);
		public Single StaminaChicken = default(Single);
		public Int32 BalanceCount = default(Int32);
		public Int32 IslandDownPercent = default(Int32);
		public Int32 IslandTypeRange = default(Int32);
		public Int32 WaveCountGold = default(Int32);
		public Int32 InitGold = default(Int32);
		public Int32 AddGold = default(Int32);
		public Single IslandInitTime = default(Single);
		public Single IslandAddTime = default(Single);
		public Single IslandTimeMin = default(Single);
		public Int32 SpikeIslandTerm = default(Int32);
		public Single IslandStamina = default(Single);
		public Int32 SpikeIslandBalanceCount = default(Int32);
		public Int32 ScoreFactorIsland = default(Int32);
		public Int32 ScoreFactorGold = default(Int32);
		public Int32 ChargeCostGold = default(Int32);
		public Int32 PlayCountMax = default(Int32);
		public Int32 RefreshDurationMinute = default(Int32);
		public String ItemPattern = string.Empty;
		public SSingleIslandBalance()
		{
		}
		public SSingleIslandBalance(SSingleIslandBalance Obj_)
		{
			IslandVelocity = Obj_.IslandVelocity;
			InitTermMin = Obj_.InitTermMin;
			InitTermMax = Obj_.InitTermMax;
			AddTerm = Obj_.AddTerm;
			InitHeight = Obj_.InitHeight;
			ExcludeHeight = Obj_.ExcludeHeight;
			AddHeight = Obj_.AddHeight;
			AddExcludeHeight = Obj_.AddExcludeHeight;
			StaminaTerm = Obj_.StaminaTerm;
			CoinTerm = Obj_.CoinTerm;
			GoldBarTerm = Obj_.GoldBarTerm;
			GoldBarCount = Obj_.GoldBarCount;
			StaminaBonus = Obj_.StaminaBonus;
			StaminaMax = Obj_.StaminaMax;
			StaminaApple = Obj_.StaminaApple;
			StaminaMeat = Obj_.StaminaMeat;
			StaminaChicken = Obj_.StaminaChicken;
			BalanceCount = Obj_.BalanceCount;
			IslandDownPercent = Obj_.IslandDownPercent;
			IslandTypeRange = Obj_.IslandTypeRange;
			WaveCountGold = Obj_.WaveCountGold;
			InitGold = Obj_.InitGold;
			AddGold = Obj_.AddGold;
			IslandInitTime = Obj_.IslandInitTime;
			IslandAddTime = Obj_.IslandAddTime;
			IslandTimeMin = Obj_.IslandTimeMin;
			SpikeIslandTerm = Obj_.SpikeIslandTerm;
			IslandStamina = Obj_.IslandStamina;
			SpikeIslandBalanceCount = Obj_.SpikeIslandBalanceCount;
			ScoreFactorIsland = Obj_.ScoreFactorIsland;
			ScoreFactorGold = Obj_.ScoreFactorGold;
			ChargeCostGold = Obj_.ChargeCostGold;
			PlayCountMax = Obj_.PlayCountMax;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
			ItemPattern = Obj_.ItemPattern;
		}
		public SSingleIslandBalance(Single IslandVelocity_, Single InitTermMin_, Single InitTermMax_, Single AddTerm_, Single InitHeight_, Single ExcludeHeight_, Single AddHeight_, Single AddExcludeHeight_, Int32 StaminaTerm_, Int32 CoinTerm_, Int32 GoldBarTerm_, Int32 GoldBarCount_, Single StaminaBonus_, Single StaminaMax_, Single StaminaApple_, Single StaminaMeat_, Single StaminaChicken_, Int32 BalanceCount_, Int32 IslandDownPercent_, Int32 IslandTypeRange_, Int32 WaveCountGold_, Int32 InitGold_, Int32 AddGold_, Single IslandInitTime_, Single IslandAddTime_, Single IslandTimeMin_, Int32 SpikeIslandTerm_, Single IslandStamina_, Int32 SpikeIslandBalanceCount_, Int32 ScoreFactorIsland_, Int32 ScoreFactorGold_, Int32 ChargeCostGold_, Int32 PlayCountMax_, Int32 RefreshDurationMinute_, String ItemPattern_)
		{
			IslandVelocity = IslandVelocity_;
			InitTermMin = InitTermMin_;
			InitTermMax = InitTermMax_;
			AddTerm = AddTerm_;
			InitHeight = InitHeight_;
			ExcludeHeight = ExcludeHeight_;
			AddHeight = AddHeight_;
			AddExcludeHeight = AddExcludeHeight_;
			StaminaTerm = StaminaTerm_;
			CoinTerm = CoinTerm_;
			GoldBarTerm = GoldBarTerm_;
			GoldBarCount = GoldBarCount_;
			StaminaBonus = StaminaBonus_;
			StaminaMax = StaminaMax_;
			StaminaApple = StaminaApple_;
			StaminaMeat = StaminaMeat_;
			StaminaChicken = StaminaChicken_;
			BalanceCount = BalanceCount_;
			IslandDownPercent = IslandDownPercent_;
			IslandTypeRange = IslandTypeRange_;
			WaveCountGold = WaveCountGold_;
			InitGold = InitGold_;
			AddGold = AddGold_;
			IslandInitTime = IslandInitTime_;
			IslandAddTime = IslandAddTime_;
			IslandTimeMin = IslandTimeMin_;
			SpikeIslandTerm = SpikeIslandTerm_;
			IslandStamina = IslandStamina_;
			SpikeIslandBalanceCount = SpikeIslandBalanceCount_;
			ScoreFactorIsland = ScoreFactorIsland_;
			ScoreFactorGold = ScoreFactorGold_;
			ChargeCostGold = ChargeCostGold_;
			PlayCountMax = PlayCountMax_;
			RefreshDurationMinute = RefreshDurationMinute_;
			ItemPattern = ItemPattern_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref IslandVelocity);
			Stream_.Pop(ref InitTermMin);
			Stream_.Pop(ref InitTermMax);
			Stream_.Pop(ref AddTerm);
			Stream_.Pop(ref InitHeight);
			Stream_.Pop(ref ExcludeHeight);
			Stream_.Pop(ref AddHeight);
			Stream_.Pop(ref AddExcludeHeight);
			Stream_.Pop(ref StaminaTerm);
			Stream_.Pop(ref CoinTerm);
			Stream_.Pop(ref GoldBarTerm);
			Stream_.Pop(ref GoldBarCount);
			Stream_.Pop(ref StaminaBonus);
			Stream_.Pop(ref StaminaMax);
			Stream_.Pop(ref StaminaApple);
			Stream_.Pop(ref StaminaMeat);
			Stream_.Pop(ref StaminaChicken);
			Stream_.Pop(ref BalanceCount);
			Stream_.Pop(ref IslandDownPercent);
			Stream_.Pop(ref IslandTypeRange);
			Stream_.Pop(ref WaveCountGold);
			Stream_.Pop(ref InitGold);
			Stream_.Pop(ref AddGold);
			Stream_.Pop(ref IslandInitTime);
			Stream_.Pop(ref IslandAddTime);
			Stream_.Pop(ref IslandTimeMin);
			Stream_.Pop(ref SpikeIslandTerm);
			Stream_.Pop(ref IslandStamina);
			Stream_.Pop(ref SpikeIslandBalanceCount);
			Stream_.Pop(ref ScoreFactorIsland);
			Stream_.Pop(ref ScoreFactorGold);
			Stream_.Pop(ref ChargeCostGold);
			Stream_.Pop(ref PlayCountMax);
			Stream_.Pop(ref RefreshDurationMinute);
			Stream_.Pop(ref ItemPattern);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("IslandVelocity", ref IslandVelocity);
			Value_.Pop("InitTermMin", ref InitTermMin);
			Value_.Pop("InitTermMax", ref InitTermMax);
			Value_.Pop("AddTerm", ref AddTerm);
			Value_.Pop("InitHeight", ref InitHeight);
			Value_.Pop("ExcludeHeight", ref ExcludeHeight);
			Value_.Pop("AddHeight", ref AddHeight);
			Value_.Pop("AddExcludeHeight", ref AddExcludeHeight);
			Value_.Pop("StaminaTerm", ref StaminaTerm);
			Value_.Pop("CoinTerm", ref CoinTerm);
			Value_.Pop("GoldBarTerm", ref GoldBarTerm);
			Value_.Pop("GoldBarCount", ref GoldBarCount);
			Value_.Pop("StaminaBonus", ref StaminaBonus);
			Value_.Pop("StaminaMax", ref StaminaMax);
			Value_.Pop("StaminaApple", ref StaminaApple);
			Value_.Pop("StaminaMeat", ref StaminaMeat);
			Value_.Pop("StaminaChicken", ref StaminaChicken);
			Value_.Pop("BalanceCount", ref BalanceCount);
			Value_.Pop("IslandDownPercent", ref IslandDownPercent);
			Value_.Pop("IslandTypeRange", ref IslandTypeRange);
			Value_.Pop("WaveCountGold", ref WaveCountGold);
			Value_.Pop("InitGold", ref InitGold);
			Value_.Pop("AddGold", ref AddGold);
			Value_.Pop("IslandInitTime", ref IslandInitTime);
			Value_.Pop("IslandAddTime", ref IslandAddTime);
			Value_.Pop("IslandTimeMin", ref IslandTimeMin);
			Value_.Pop("SpikeIslandTerm", ref SpikeIslandTerm);
			Value_.Pop("IslandStamina", ref IslandStamina);
			Value_.Pop("SpikeIslandBalanceCount", ref SpikeIslandBalanceCount);
			Value_.Pop("ScoreFactorIsland", ref ScoreFactorIsland);
			Value_.Pop("ScoreFactorGold", ref ScoreFactorGold);
			Value_.Pop("ChargeCostGold", ref ChargeCostGold);
			Value_.Pop("PlayCountMax", ref PlayCountMax);
			Value_.Pop("RefreshDurationMinute", ref RefreshDurationMinute);
			Value_.Pop("ItemPattern", ref ItemPattern);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(IslandVelocity);
			Stream_.Push(InitTermMin);
			Stream_.Push(InitTermMax);
			Stream_.Push(AddTerm);
			Stream_.Push(InitHeight);
			Stream_.Push(ExcludeHeight);
			Stream_.Push(AddHeight);
			Stream_.Push(AddExcludeHeight);
			Stream_.Push(StaminaTerm);
			Stream_.Push(CoinTerm);
			Stream_.Push(GoldBarTerm);
			Stream_.Push(GoldBarCount);
			Stream_.Push(StaminaBonus);
			Stream_.Push(StaminaMax);
			Stream_.Push(StaminaApple);
			Stream_.Push(StaminaMeat);
			Stream_.Push(StaminaChicken);
			Stream_.Push(BalanceCount);
			Stream_.Push(IslandDownPercent);
			Stream_.Push(IslandTypeRange);
			Stream_.Push(WaveCountGold);
			Stream_.Push(InitGold);
			Stream_.Push(AddGold);
			Stream_.Push(IslandInitTime);
			Stream_.Push(IslandAddTime);
			Stream_.Push(IslandTimeMin);
			Stream_.Push(SpikeIslandTerm);
			Stream_.Push(IslandStamina);
			Stream_.Push(SpikeIslandBalanceCount);
			Stream_.Push(ScoreFactorIsland);
			Stream_.Push(ScoreFactorGold);
			Stream_.Push(ChargeCostGold);
			Stream_.Push(PlayCountMax);
			Stream_.Push(RefreshDurationMinute);
			Stream_.Push(ItemPattern);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("IslandVelocity", IslandVelocity);
			Value_.Push("InitTermMin", InitTermMin);
			Value_.Push("InitTermMax", InitTermMax);
			Value_.Push("AddTerm", AddTerm);
			Value_.Push("InitHeight", InitHeight);
			Value_.Push("ExcludeHeight", ExcludeHeight);
			Value_.Push("AddHeight", AddHeight);
			Value_.Push("AddExcludeHeight", AddExcludeHeight);
			Value_.Push("StaminaTerm", StaminaTerm);
			Value_.Push("CoinTerm", CoinTerm);
			Value_.Push("GoldBarTerm", GoldBarTerm);
			Value_.Push("GoldBarCount", GoldBarCount);
			Value_.Push("StaminaBonus", StaminaBonus);
			Value_.Push("StaminaMax", StaminaMax);
			Value_.Push("StaminaApple", StaminaApple);
			Value_.Push("StaminaMeat", StaminaMeat);
			Value_.Push("StaminaChicken", StaminaChicken);
			Value_.Push("BalanceCount", BalanceCount);
			Value_.Push("IslandDownPercent", IslandDownPercent);
			Value_.Push("IslandTypeRange", IslandTypeRange);
			Value_.Push("WaveCountGold", WaveCountGold);
			Value_.Push("InitGold", InitGold);
			Value_.Push("AddGold", AddGold);
			Value_.Push("IslandInitTime", IslandInitTime);
			Value_.Push("IslandAddTime", IslandAddTime);
			Value_.Push("IslandTimeMin", IslandTimeMin);
			Value_.Push("SpikeIslandTerm", SpikeIslandTerm);
			Value_.Push("IslandStamina", IslandStamina);
			Value_.Push("SpikeIslandBalanceCount", SpikeIslandBalanceCount);
			Value_.Push("ScoreFactorIsland", ScoreFactorIsland);
			Value_.Push("ScoreFactorGold", ScoreFactorGold);
			Value_.Push("ChargeCostGold", ChargeCostGold);
			Value_.Push("PlayCountMax", PlayCountMax);
			Value_.Push("RefreshDurationMinute", RefreshDurationMinute);
			Value_.Push("ItemPattern", ItemPattern);
		}
		public void Set(SSingleIslandBalance Obj_)
		{
			IslandVelocity = Obj_.IslandVelocity;
			InitTermMin = Obj_.InitTermMin;
			InitTermMax = Obj_.InitTermMax;
			AddTerm = Obj_.AddTerm;
			InitHeight = Obj_.InitHeight;
			ExcludeHeight = Obj_.ExcludeHeight;
			AddHeight = Obj_.AddHeight;
			AddExcludeHeight = Obj_.AddExcludeHeight;
			StaminaTerm = Obj_.StaminaTerm;
			CoinTerm = Obj_.CoinTerm;
			GoldBarTerm = Obj_.GoldBarTerm;
			GoldBarCount = Obj_.GoldBarCount;
			StaminaBonus = Obj_.StaminaBonus;
			StaminaMax = Obj_.StaminaMax;
			StaminaApple = Obj_.StaminaApple;
			StaminaMeat = Obj_.StaminaMeat;
			StaminaChicken = Obj_.StaminaChicken;
			BalanceCount = Obj_.BalanceCount;
			IslandDownPercent = Obj_.IslandDownPercent;
			IslandTypeRange = Obj_.IslandTypeRange;
			WaveCountGold = Obj_.WaveCountGold;
			InitGold = Obj_.InitGold;
			AddGold = Obj_.AddGold;
			IslandInitTime = Obj_.IslandInitTime;
			IslandAddTime = Obj_.IslandAddTime;
			IslandTimeMin = Obj_.IslandTimeMin;
			SpikeIslandTerm = Obj_.SpikeIslandTerm;
			IslandStamina = Obj_.IslandStamina;
			SpikeIslandBalanceCount = Obj_.SpikeIslandBalanceCount;
			ScoreFactorIsland = Obj_.ScoreFactorIsland;
			ScoreFactorGold = Obj_.ScoreFactorGold;
			ChargeCostGold = Obj_.ChargeCostGold;
			PlayCountMax = Obj_.PlayCountMax;
			RefreshDurationMinute = Obj_.RefreshDurationMinute;
			ItemPattern = Obj_.ItemPattern;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(IslandVelocity) + "," + 
				SEnumChecker.GetStdName(InitTermMin) + "," + 
				SEnumChecker.GetStdName(InitTermMax) + "," + 
				SEnumChecker.GetStdName(AddTerm) + "," + 
				SEnumChecker.GetStdName(InitHeight) + "," + 
				SEnumChecker.GetStdName(ExcludeHeight) + "," + 
				SEnumChecker.GetStdName(AddHeight) + "," + 
				SEnumChecker.GetStdName(AddExcludeHeight) + "," + 
				SEnumChecker.GetStdName(StaminaTerm) + "," + 
				SEnumChecker.GetStdName(CoinTerm) + "," + 
				SEnumChecker.GetStdName(GoldBarTerm) + "," + 
				SEnumChecker.GetStdName(GoldBarCount) + "," + 
				SEnumChecker.GetStdName(StaminaBonus) + "," + 
				SEnumChecker.GetStdName(StaminaMax) + "," + 
				SEnumChecker.GetStdName(StaminaApple) + "," + 
				SEnumChecker.GetStdName(StaminaMeat) + "," + 
				SEnumChecker.GetStdName(StaminaChicken) + "," + 
				SEnumChecker.GetStdName(BalanceCount) + "," + 
				SEnumChecker.GetStdName(IslandDownPercent) + "," + 
				SEnumChecker.GetStdName(IslandTypeRange) + "," + 
				SEnumChecker.GetStdName(WaveCountGold) + "," + 
				SEnumChecker.GetStdName(InitGold) + "," + 
				SEnumChecker.GetStdName(AddGold) + "," + 
				SEnumChecker.GetStdName(IslandInitTime) + "," + 
				SEnumChecker.GetStdName(IslandAddTime) + "," + 
				SEnumChecker.GetStdName(IslandTimeMin) + "," + 
				SEnumChecker.GetStdName(SpikeIslandTerm) + "," + 
				SEnumChecker.GetStdName(IslandStamina) + "," + 
				SEnumChecker.GetStdName(SpikeIslandBalanceCount) + "," + 
				SEnumChecker.GetStdName(ScoreFactorIsland) + "," + 
				SEnumChecker.GetStdName(ScoreFactorGold) + "," + 
				SEnumChecker.GetStdName(ChargeCostGold) + "," + 
				SEnumChecker.GetStdName(PlayCountMax) + "," + 
				SEnumChecker.GetStdName(RefreshDurationMinute) + "," + 
				SEnumChecker.GetStdName(ItemPattern);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(IslandVelocity, "IslandVelocity") + "," + 
				SEnumChecker.GetMemberName(InitTermMin, "InitTermMin") + "," + 
				SEnumChecker.GetMemberName(InitTermMax, "InitTermMax") + "," + 
				SEnumChecker.GetMemberName(AddTerm, "AddTerm") + "," + 
				SEnumChecker.GetMemberName(InitHeight, "InitHeight") + "," + 
				SEnumChecker.GetMemberName(ExcludeHeight, "ExcludeHeight") + "," + 
				SEnumChecker.GetMemberName(AddHeight, "AddHeight") + "," + 
				SEnumChecker.GetMemberName(AddExcludeHeight, "AddExcludeHeight") + "," + 
				SEnumChecker.GetMemberName(StaminaTerm, "StaminaTerm") + "," + 
				SEnumChecker.GetMemberName(CoinTerm, "CoinTerm") + "," + 
				SEnumChecker.GetMemberName(GoldBarTerm, "GoldBarTerm") + "," + 
				SEnumChecker.GetMemberName(GoldBarCount, "GoldBarCount") + "," + 
				SEnumChecker.GetMemberName(StaminaBonus, "StaminaBonus") + "," + 
				SEnumChecker.GetMemberName(StaminaMax, "StaminaMax") + "," + 
				SEnumChecker.GetMemberName(StaminaApple, "StaminaApple") + "," + 
				SEnumChecker.GetMemberName(StaminaMeat, "StaminaMeat") + "," + 
				SEnumChecker.GetMemberName(StaminaChicken, "StaminaChicken") + "," + 
				SEnumChecker.GetMemberName(BalanceCount, "BalanceCount") + "," + 
				SEnumChecker.GetMemberName(IslandDownPercent, "IslandDownPercent") + "," + 
				SEnumChecker.GetMemberName(IslandTypeRange, "IslandTypeRange") + "," + 
				SEnumChecker.GetMemberName(WaveCountGold, "WaveCountGold") + "," + 
				SEnumChecker.GetMemberName(InitGold, "InitGold") + "," + 
				SEnumChecker.GetMemberName(AddGold, "AddGold") + "," + 
				SEnumChecker.GetMemberName(IslandInitTime, "IslandInitTime") + "," + 
				SEnumChecker.GetMemberName(IslandAddTime, "IslandAddTime") + "," + 
				SEnumChecker.GetMemberName(IslandTimeMin, "IslandTimeMin") + "," + 
				SEnumChecker.GetMemberName(SpikeIslandTerm, "SpikeIslandTerm") + "," + 
				SEnumChecker.GetMemberName(IslandStamina, "IslandStamina") + "," + 
				SEnumChecker.GetMemberName(SpikeIslandBalanceCount, "SpikeIslandBalanceCount") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorIsland, "ScoreFactorIsland") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorGold, "ScoreFactorGold") + "," + 
				SEnumChecker.GetMemberName(ChargeCostGold, "ChargeCostGold") + "," + 
				SEnumChecker.GetMemberName(PlayCountMax, "PlayCountMax") + "," + 
				SEnumChecker.GetMemberName(RefreshDurationMinute, "RefreshDurationMinute") + "," + 
				SEnumChecker.GetMemberName(ItemPattern, "ItemPattern");
		}
	}
	public class SSingleCharacterMove : SProto
	{
		public SPoint Pos = new SPoint();
		public SPoint Vel = new SPoint();
		public SSingleCharacterMove()
		{
		}
		public SSingleCharacterMove(SSingleCharacterMove Obj_)
		{
			Pos = Obj_.Pos;
			Vel = Obj_.Vel;
		}
		public SSingleCharacterMove(SPoint Pos_, SPoint Vel_)
		{
			Pos = Pos_;
			Vel = Vel_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Pos);
			Stream_.Pop(ref Vel);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Pos", ref Pos);
			Value_.Pop("Vel", ref Vel);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Pos);
			Stream_.Push(Vel);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Pos", Pos);
			Value_.Push("Vel", Vel);
		}
		public void Set(SSingleCharacterMove Obj_)
		{
			Pos.Set(Obj_.Pos);
			Vel.Set(Obj_.Vel);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Pos) + "," + 
				SEnumChecker.GetStdName(Vel);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Pos, "Pos") + "," + 
				SEnumChecker.GetMemberName(Vel, "Vel");
		}
	}
	public class SSingleCharacter : SProto
	{
		public SSingleCharacterMove Move = new SSingleCharacterMove();
		public Boolean IsGround = default(Boolean);
		public SByte Dir = default(SByte);
		public SByte BalloonCount = default(SByte);
		public SByte PumpCount = default(SByte);
		public Single Stamina = default(Single);
		public SByte Face = default(SByte);
		public SPoint Acc = new SPoint();
		public SSingleCharacter()
		{
		}
		public SSingleCharacter(SSingleCharacter Obj_)
		{
			Move = Obj_.Move;
			IsGround = Obj_.IsGround;
			Dir = Obj_.Dir;
			BalloonCount = Obj_.BalloonCount;
			PumpCount = Obj_.PumpCount;
			Stamina = Obj_.Stamina;
			Face = Obj_.Face;
			Acc = Obj_.Acc;
		}
		public SSingleCharacter(SSingleCharacterMove Move_, Boolean IsGround_, SByte Dir_, SByte BalloonCount_, SByte PumpCount_, Single Stamina_, SByte Face_, SPoint Acc_)
		{
			Move = Move_;
			IsGround = IsGround_;
			Dir = Dir_;
			BalloonCount = BalloonCount_;
			PumpCount = PumpCount_;
			Stamina = Stamina_;
			Face = Face_;
			Acc = Acc_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Move);
			Stream_.Pop(ref IsGround);
			Stream_.Pop(ref Dir);
			Stream_.Pop(ref BalloonCount);
			Stream_.Pop(ref PumpCount);
			Stream_.Pop(ref Stamina);
			Stream_.Pop(ref Face);
			Stream_.Pop(ref Acc);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Move", ref Move);
			Value_.Pop("IsGround", ref IsGround);
			Value_.Pop("Dir", ref Dir);
			Value_.Pop("BalloonCount", ref BalloonCount);
			Value_.Pop("PumpCount", ref PumpCount);
			Value_.Pop("Stamina", ref Stamina);
			Value_.Pop("Face", ref Face);
			Value_.Pop("Acc", ref Acc);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Move);
			Stream_.Push(IsGround);
			Stream_.Push(Dir);
			Stream_.Push(BalloonCount);
			Stream_.Push(PumpCount);
			Stream_.Push(Stamina);
			Stream_.Push(Face);
			Stream_.Push(Acc);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Move", Move);
			Value_.Push("IsGround", IsGround);
			Value_.Push("Dir", Dir);
			Value_.Push("BalloonCount", BalloonCount);
			Value_.Push("PumpCount", PumpCount);
			Value_.Push("Stamina", Stamina);
			Value_.Push("Face", Face);
			Value_.Push("Acc", Acc);
		}
		public void Set(SSingleCharacter Obj_)
		{
			Move.Set(Obj_.Move);
			IsGround = Obj_.IsGround;
			Dir = Obj_.Dir;
			BalloonCount = Obj_.BalloonCount;
			PumpCount = Obj_.PumpCount;
			Stamina = Obj_.Stamina;
			Face = Obj_.Face;
			Acc.Set(Obj_.Acc);
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Move) + "," + 
				SEnumChecker.GetStdName(IsGround) + "," + 
				SEnumChecker.GetStdName(Dir) + "," + 
				SEnumChecker.GetStdName(BalloonCount) + "," + 
				SEnumChecker.GetStdName(PumpCount) + "," + 
				SEnumChecker.GetStdName(Stamina) + "," + 
				SEnumChecker.GetStdName(Face) + "," + 
				SEnumChecker.GetStdName(Acc);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Move, "Move") + "," + 
				SEnumChecker.GetMemberName(IsGround, "IsGround") + "," + 
				SEnumChecker.GetMemberName(Dir, "Dir") + "," + 
				SEnumChecker.GetMemberName(BalloonCount, "BalloonCount") + "," + 
				SEnumChecker.GetMemberName(PumpCount, "PumpCount") + "," + 
				SEnumChecker.GetMemberName(Stamina, "Stamina") + "," + 
				SEnumChecker.GetMemberName(Face, "Face") + "," + 
				SEnumChecker.GetMemberName(Acc, "Acc");
		}
	}
	public class SSinglePlayer : SProto
	{
		public TUID UID = default(TUID);
		public String Nick = string.Empty;
		public String CountryCode = string.Empty;
		public TTeamCnt TeamIndex = default(TTeamCnt);
		public Int32 CharCode = default(Int32);
		public SSingleCharacter Character = new SSingleCharacter();
		public TimePoint InvulnerableEndTime = default(TimePoint);
		public SSinglePlayer()
		{
		}
		public SSinglePlayer(SSinglePlayer Obj_)
		{
			UID = Obj_.UID;
			Nick = Obj_.Nick;
			CountryCode = Obj_.CountryCode;
			TeamIndex = Obj_.TeamIndex;
			CharCode = Obj_.CharCode;
			Character = Obj_.Character;
			InvulnerableEndTime = Obj_.InvulnerableEndTime;
		}
		public SSinglePlayer(TUID UID_, String Nick_, String CountryCode_, TTeamCnt TeamIndex_, Int32 CharCode_, SSingleCharacter Character_, TimePoint InvulnerableEndTime_)
		{
			UID = UID_;
			Nick = Nick_;
			CountryCode = CountryCode_;
			TeamIndex = TeamIndex_;
			CharCode = CharCode_;
			Character = Character_;
			InvulnerableEndTime = InvulnerableEndTime_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref UID);
			Stream_.Pop(ref Nick);
			Stream_.Pop(ref CountryCode);
			Stream_.Pop(ref TeamIndex);
			Stream_.Pop(ref CharCode);
			Stream_.Pop(ref Character);
			Stream_.Pop(ref InvulnerableEndTime);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("UID", ref UID);
			Value_.Pop("Nick", ref Nick);
			Value_.Pop("CountryCode", ref CountryCode);
			Value_.Pop("TeamIndex", ref TeamIndex);
			Value_.Pop("CharCode", ref CharCode);
			Value_.Pop("Character", ref Character);
			Value_.Pop("InvulnerableEndTime", ref InvulnerableEndTime);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(UID);
			Stream_.Push(Nick);
			Stream_.Push(CountryCode);
			Stream_.Push(TeamIndex);
			Stream_.Push(CharCode);
			Stream_.Push(Character);
			Stream_.Push(InvulnerableEndTime);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("UID", UID);
			Value_.Push("Nick", Nick);
			Value_.Push("CountryCode", CountryCode);
			Value_.Push("TeamIndex", TeamIndex);
			Value_.Push("CharCode", CharCode);
			Value_.Push("Character", Character);
			Value_.Push("InvulnerableEndTime", InvulnerableEndTime);
		}
		public void Set(SSinglePlayer Obj_)
		{
			UID = Obj_.UID;
			Nick = Obj_.Nick;
			CountryCode = Obj_.CountryCode;
			TeamIndex = Obj_.TeamIndex;
			CharCode = Obj_.CharCode;
			Character.Set(Obj_.Character);
			InvulnerableEndTime = Obj_.InvulnerableEndTime;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(UID) + "," + 
				SEnumChecker.GetStdName(Nick) + "," + 
				SEnumChecker.GetStdName(CountryCode) + "," + 
				SEnumChecker.GetStdName(TeamIndex) + "," + 
				SEnumChecker.GetStdName(CharCode) + "," + 
				SEnumChecker.GetStdName(Character) + "," + 
				SEnumChecker.GetStdName(InvulnerableEndTime);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(UID, "UID") + "," + 
				SEnumChecker.GetMemberName(Nick, "Nick") + "," + 
				SEnumChecker.GetMemberName(CountryCode, "CountryCode") + "," + 
				SEnumChecker.GetMemberName(TeamIndex, "TeamIndex") + "," + 
				SEnumChecker.GetMemberName(CharCode, "CharCode") + "," + 
				SEnumChecker.GetMemberName(Character, "Character") + "," + 
				SEnumChecker.GetMemberName(InvulnerableEndTime, "InvulnerableEndTime");
		}
	}
	public class SShopPackageClientMeta : SShopPackageServerMeta
	{
		public EText ETextName = default(EText);
		public String TextureName = string.Empty;
		public SShopPackageClientMeta()
		{
		}
		public SShopPackageClientMeta(SShopPackageClientMeta Obj_) : base(Obj_)
		{
			ETextName = Obj_.ETextName;
			TextureName = Obj_.TextureName;
		}
		public SShopPackageClientMeta(SShopPackageServerMeta Super_, EText ETextName_, String TextureName_) : base(Super_)
		{
			ETextName = ETextName_;
			TextureName = TextureName_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref ETextName);
			Stream_.Pop(ref TextureName);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("ETextName", ref ETextName);
			Value_.Pop("TextureName", ref TextureName);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(ETextName);
			Stream_.Push(TextureName);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("ETextName", ETextName);
			Value_.Push("TextureName", TextureName);
		}
		public void Set(SShopPackageClientMeta Obj_)
		{
			base.Set(Obj_);
			ETextName = Obj_.ETextName;
			TextureName = Obj_.TextureName;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				"bb.EText" + "," + 
				SEnumChecker.GetStdName(TextureName);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(ETextName, "ETextName") + "," + 
				SEnumChecker.GetMemberName(TextureName, "TextureName");
		}
	}
	public class SServerAlarmMeta : SProto
	{
		public String Key = string.Empty;
		public EText ETextName = default(EText);
		public SServerAlarmMeta()
		{
		}
		public SServerAlarmMeta(SServerAlarmMeta Obj_)
		{
			Key = Obj_.Key;
			ETextName = Obj_.ETextName;
		}
		public SServerAlarmMeta(String Key_, EText ETextName_)
		{
			Key = Key_;
			ETextName = ETextName_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Key);
			Stream_.Pop(ref ETextName);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Key", ref Key);
			Value_.Pop("ETextName", ref ETextName);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Key);
			Stream_.Push(ETextName);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Key", Key);
			Value_.Push("ETextName", ETextName);
		}
		public void Set(SServerAlarmMeta Obj_)
		{
			Key = Obj_.Key;
			ETextName = Obj_.ETextName;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(Key) + "," + 
				"bb.EText";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Key, "Key") + "," + 
				SEnumChecker.GetMemberName(ETextName, "ETextName");
		}
	}
	public class SShopPackageDateMeta : SProto
	{
		public Int32 DateId = default(Int32);
		public String BeginTime = string.Empty;
		public String EndTime = string.Empty;
		public Int32 PackageCode = default(Int32);
		public SShopPackageDateMeta()
		{
		}
		public SShopPackageDateMeta(SShopPackageDateMeta Obj_)
		{
			DateId = Obj_.DateId;
			BeginTime = Obj_.BeginTime;
			EndTime = Obj_.EndTime;
			PackageCode = Obj_.PackageCode;
		}
		public SShopPackageDateMeta(Int32 DateId_, String BeginTime_, String EndTime_, Int32 PackageCode_)
		{
			DateId = DateId_;
			BeginTime = BeginTime_;
			EndTime = EndTime_;
			PackageCode = PackageCode_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref DateId);
			Stream_.Pop(ref BeginTime);
			Stream_.Pop(ref EndTime);
			Stream_.Pop(ref PackageCode);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("DateId", ref DateId);
			Value_.Pop("BeginTime", ref BeginTime);
			Value_.Pop("EndTime", ref EndTime);
			Value_.Pop("PackageCode", ref PackageCode);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(DateId);
			Stream_.Push(BeginTime);
			Stream_.Push(EndTime);
			Stream_.Push(PackageCode);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("DateId", DateId);
			Value_.Push("BeginTime", BeginTime);
			Value_.Push("EndTime", EndTime);
			Value_.Push("PackageCode", PackageCode);
		}
		public void Set(SShopPackageDateMeta Obj_)
		{
			DateId = Obj_.DateId;
			BeginTime = Obj_.BeginTime;
			EndTime = Obj_.EndTime;
			PackageCode = Obj_.PackageCode;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(DateId) + "," + 
				SEnumChecker.GetStdName(BeginTime) + "," + 
				SEnumChecker.GetStdName(EndTime) + "," + 
				SEnumChecker.GetStdName(PackageCode);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(DateId, "DateId") + "," + 
				SEnumChecker.GetMemberName(BeginTime, "BeginTime") + "," + 
				SEnumChecker.GetMemberName(EndTime, "EndTime") + "," + 
				SEnumChecker.GetMemberName(PackageCode, "PackageCode");
		}
	}
	public class SMultiIslandBalance : SProto
	{
		public Single IslandVelocity = default(Single);
		public Single InitTermMin = default(Single);
		public Single InitTermMax = default(Single);
		public Single AddTerm = default(Single);
		public Single InitHeight = default(Single);
		public Single ExcludeHeight = default(Single);
		public Single AddHeight = default(Single);
		public Single AddExcludeHeight = default(Single);
		public Int32 StaminaTerm = default(Int32);
		public Single StaminaBonus = default(Single);
		public Single StaminaMax = default(Single);
		public Single StaminaApple = default(Single);
		public Single StaminaMeat = default(Single);
		public Single StaminaChicken = default(Single);
		public String ItemPattern = string.Empty;
		public Int32 BalanceCount = default(Int32);
		public Int32 IslandDownPercent = default(Int32);
		public Int32 IslandTypeRange = default(Int32);
		public Int32 WaveCountGold = default(Int32);
		public Int32 InitGold = default(Int32);
		public Int32 AddGold = default(Int32);
		public Single IslandInitTime = default(Single);
		public Single IslandAddTime = default(Single);
		public Single IslandTimeMin = default(Single);
		public Int32 SpikeIslandTerm = default(Int32);
		public Single IslandStamina = default(Single);
		public Int32 SpikeIslandBalanceCount = default(Int32);
		public Int32 ScoreFactorIsland = default(Int32);
		public Int32 PointTerm = default(Int32);
		public Int32 ScoreFactorPoint = default(Int32);
		public Int32 ScoreFactorLanding = default(Int32);
		public Single StaminaRegen = default(Single);
		public Single RegenDelay = default(Single);
		public SMultiIslandBalance()
		{
		}
		public SMultiIslandBalance(SMultiIslandBalance Obj_)
		{
			IslandVelocity = Obj_.IslandVelocity;
			InitTermMin = Obj_.InitTermMin;
			InitTermMax = Obj_.InitTermMax;
			AddTerm = Obj_.AddTerm;
			InitHeight = Obj_.InitHeight;
			ExcludeHeight = Obj_.ExcludeHeight;
			AddHeight = Obj_.AddHeight;
			AddExcludeHeight = Obj_.AddExcludeHeight;
			StaminaTerm = Obj_.StaminaTerm;
			StaminaBonus = Obj_.StaminaBonus;
			StaminaMax = Obj_.StaminaMax;
			StaminaApple = Obj_.StaminaApple;
			StaminaMeat = Obj_.StaminaMeat;
			StaminaChicken = Obj_.StaminaChicken;
			ItemPattern = Obj_.ItemPattern;
			BalanceCount = Obj_.BalanceCount;
			IslandDownPercent = Obj_.IslandDownPercent;
			IslandTypeRange = Obj_.IslandTypeRange;
			WaveCountGold = Obj_.WaveCountGold;
			InitGold = Obj_.InitGold;
			AddGold = Obj_.AddGold;
			IslandInitTime = Obj_.IslandInitTime;
			IslandAddTime = Obj_.IslandAddTime;
			IslandTimeMin = Obj_.IslandTimeMin;
			SpikeIslandTerm = Obj_.SpikeIslandTerm;
			IslandStamina = Obj_.IslandStamina;
			SpikeIslandBalanceCount = Obj_.SpikeIslandBalanceCount;
			ScoreFactorIsland = Obj_.ScoreFactorIsland;
			PointTerm = Obj_.PointTerm;
			ScoreFactorPoint = Obj_.ScoreFactorPoint;
			ScoreFactorLanding = Obj_.ScoreFactorLanding;
			StaminaRegen = Obj_.StaminaRegen;
			RegenDelay = Obj_.RegenDelay;
		}
		public SMultiIslandBalance(Single IslandVelocity_, Single InitTermMin_, Single InitTermMax_, Single AddTerm_, Single InitHeight_, Single ExcludeHeight_, Single AddHeight_, Single AddExcludeHeight_, Int32 StaminaTerm_, Single StaminaBonus_, Single StaminaMax_, Single StaminaApple_, Single StaminaMeat_, Single StaminaChicken_, String ItemPattern_, Int32 BalanceCount_, Int32 IslandDownPercent_, Int32 IslandTypeRange_, Int32 WaveCountGold_, Int32 InitGold_, Int32 AddGold_, Single IslandInitTime_, Single IslandAddTime_, Single IslandTimeMin_, Int32 SpikeIslandTerm_, Single IslandStamina_, Int32 SpikeIslandBalanceCount_, Int32 ScoreFactorIsland_, Int32 PointTerm_, Int32 ScoreFactorPoint_, Int32 ScoreFactorLanding_, Single StaminaRegen_, Single RegenDelay_)
		{
			IslandVelocity = IslandVelocity_;
			InitTermMin = InitTermMin_;
			InitTermMax = InitTermMax_;
			AddTerm = AddTerm_;
			InitHeight = InitHeight_;
			ExcludeHeight = ExcludeHeight_;
			AddHeight = AddHeight_;
			AddExcludeHeight = AddExcludeHeight_;
			StaminaTerm = StaminaTerm_;
			StaminaBonus = StaminaBonus_;
			StaminaMax = StaminaMax_;
			StaminaApple = StaminaApple_;
			StaminaMeat = StaminaMeat_;
			StaminaChicken = StaminaChicken_;
			ItemPattern = ItemPattern_;
			BalanceCount = BalanceCount_;
			IslandDownPercent = IslandDownPercent_;
			IslandTypeRange = IslandTypeRange_;
			WaveCountGold = WaveCountGold_;
			InitGold = InitGold_;
			AddGold = AddGold_;
			IslandInitTime = IslandInitTime_;
			IslandAddTime = IslandAddTime_;
			IslandTimeMin = IslandTimeMin_;
			SpikeIslandTerm = SpikeIslandTerm_;
			IslandStamina = IslandStamina_;
			SpikeIslandBalanceCount = SpikeIslandBalanceCount_;
			ScoreFactorIsland = ScoreFactorIsland_;
			PointTerm = PointTerm_;
			ScoreFactorPoint = ScoreFactorPoint_;
			ScoreFactorLanding = ScoreFactorLanding_;
			StaminaRegen = StaminaRegen_;
			RegenDelay = RegenDelay_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref IslandVelocity);
			Stream_.Pop(ref InitTermMin);
			Stream_.Pop(ref InitTermMax);
			Stream_.Pop(ref AddTerm);
			Stream_.Pop(ref InitHeight);
			Stream_.Pop(ref ExcludeHeight);
			Stream_.Pop(ref AddHeight);
			Stream_.Pop(ref AddExcludeHeight);
			Stream_.Pop(ref StaminaTerm);
			Stream_.Pop(ref StaminaBonus);
			Stream_.Pop(ref StaminaMax);
			Stream_.Pop(ref StaminaApple);
			Stream_.Pop(ref StaminaMeat);
			Stream_.Pop(ref StaminaChicken);
			Stream_.Pop(ref ItemPattern);
			Stream_.Pop(ref BalanceCount);
			Stream_.Pop(ref IslandDownPercent);
			Stream_.Pop(ref IslandTypeRange);
			Stream_.Pop(ref WaveCountGold);
			Stream_.Pop(ref InitGold);
			Stream_.Pop(ref AddGold);
			Stream_.Pop(ref IslandInitTime);
			Stream_.Pop(ref IslandAddTime);
			Stream_.Pop(ref IslandTimeMin);
			Stream_.Pop(ref SpikeIslandTerm);
			Stream_.Pop(ref IslandStamina);
			Stream_.Pop(ref SpikeIslandBalanceCount);
			Stream_.Pop(ref ScoreFactorIsland);
			Stream_.Pop(ref PointTerm);
			Stream_.Pop(ref ScoreFactorPoint);
			Stream_.Pop(ref ScoreFactorLanding);
			Stream_.Pop(ref StaminaRegen);
			Stream_.Pop(ref RegenDelay);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("IslandVelocity", ref IslandVelocity);
			Value_.Pop("InitTermMin", ref InitTermMin);
			Value_.Pop("InitTermMax", ref InitTermMax);
			Value_.Pop("AddTerm", ref AddTerm);
			Value_.Pop("InitHeight", ref InitHeight);
			Value_.Pop("ExcludeHeight", ref ExcludeHeight);
			Value_.Pop("AddHeight", ref AddHeight);
			Value_.Pop("AddExcludeHeight", ref AddExcludeHeight);
			Value_.Pop("StaminaTerm", ref StaminaTerm);
			Value_.Pop("StaminaBonus", ref StaminaBonus);
			Value_.Pop("StaminaMax", ref StaminaMax);
			Value_.Pop("StaminaApple", ref StaminaApple);
			Value_.Pop("StaminaMeat", ref StaminaMeat);
			Value_.Pop("StaminaChicken", ref StaminaChicken);
			Value_.Pop("ItemPattern", ref ItemPattern);
			Value_.Pop("BalanceCount", ref BalanceCount);
			Value_.Pop("IslandDownPercent", ref IslandDownPercent);
			Value_.Pop("IslandTypeRange", ref IslandTypeRange);
			Value_.Pop("WaveCountGold", ref WaveCountGold);
			Value_.Pop("InitGold", ref InitGold);
			Value_.Pop("AddGold", ref AddGold);
			Value_.Pop("IslandInitTime", ref IslandInitTime);
			Value_.Pop("IslandAddTime", ref IslandAddTime);
			Value_.Pop("IslandTimeMin", ref IslandTimeMin);
			Value_.Pop("SpikeIslandTerm", ref SpikeIslandTerm);
			Value_.Pop("IslandStamina", ref IslandStamina);
			Value_.Pop("SpikeIslandBalanceCount", ref SpikeIslandBalanceCount);
			Value_.Pop("ScoreFactorIsland", ref ScoreFactorIsland);
			Value_.Pop("PointTerm", ref PointTerm);
			Value_.Pop("ScoreFactorPoint", ref ScoreFactorPoint);
			Value_.Pop("ScoreFactorLanding", ref ScoreFactorLanding);
			Value_.Pop("StaminaRegen", ref StaminaRegen);
			Value_.Pop("RegenDelay", ref RegenDelay);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(IslandVelocity);
			Stream_.Push(InitTermMin);
			Stream_.Push(InitTermMax);
			Stream_.Push(AddTerm);
			Stream_.Push(InitHeight);
			Stream_.Push(ExcludeHeight);
			Stream_.Push(AddHeight);
			Stream_.Push(AddExcludeHeight);
			Stream_.Push(StaminaTerm);
			Stream_.Push(StaminaBonus);
			Stream_.Push(StaminaMax);
			Stream_.Push(StaminaApple);
			Stream_.Push(StaminaMeat);
			Stream_.Push(StaminaChicken);
			Stream_.Push(ItemPattern);
			Stream_.Push(BalanceCount);
			Stream_.Push(IslandDownPercent);
			Stream_.Push(IslandTypeRange);
			Stream_.Push(WaveCountGold);
			Stream_.Push(InitGold);
			Stream_.Push(AddGold);
			Stream_.Push(IslandInitTime);
			Stream_.Push(IslandAddTime);
			Stream_.Push(IslandTimeMin);
			Stream_.Push(SpikeIslandTerm);
			Stream_.Push(IslandStamina);
			Stream_.Push(SpikeIslandBalanceCount);
			Stream_.Push(ScoreFactorIsland);
			Stream_.Push(PointTerm);
			Stream_.Push(ScoreFactorPoint);
			Stream_.Push(ScoreFactorLanding);
			Stream_.Push(StaminaRegen);
			Stream_.Push(RegenDelay);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("IslandVelocity", IslandVelocity);
			Value_.Push("InitTermMin", InitTermMin);
			Value_.Push("InitTermMax", InitTermMax);
			Value_.Push("AddTerm", AddTerm);
			Value_.Push("InitHeight", InitHeight);
			Value_.Push("ExcludeHeight", ExcludeHeight);
			Value_.Push("AddHeight", AddHeight);
			Value_.Push("AddExcludeHeight", AddExcludeHeight);
			Value_.Push("StaminaTerm", StaminaTerm);
			Value_.Push("StaminaBonus", StaminaBonus);
			Value_.Push("StaminaMax", StaminaMax);
			Value_.Push("StaminaApple", StaminaApple);
			Value_.Push("StaminaMeat", StaminaMeat);
			Value_.Push("StaminaChicken", StaminaChicken);
			Value_.Push("ItemPattern", ItemPattern);
			Value_.Push("BalanceCount", BalanceCount);
			Value_.Push("IslandDownPercent", IslandDownPercent);
			Value_.Push("IslandTypeRange", IslandTypeRange);
			Value_.Push("WaveCountGold", WaveCountGold);
			Value_.Push("InitGold", InitGold);
			Value_.Push("AddGold", AddGold);
			Value_.Push("IslandInitTime", IslandInitTime);
			Value_.Push("IslandAddTime", IslandAddTime);
			Value_.Push("IslandTimeMin", IslandTimeMin);
			Value_.Push("SpikeIslandTerm", SpikeIslandTerm);
			Value_.Push("IslandStamina", IslandStamina);
			Value_.Push("SpikeIslandBalanceCount", SpikeIslandBalanceCount);
			Value_.Push("ScoreFactorIsland", ScoreFactorIsland);
			Value_.Push("PointTerm", PointTerm);
			Value_.Push("ScoreFactorPoint", ScoreFactorPoint);
			Value_.Push("ScoreFactorLanding", ScoreFactorLanding);
			Value_.Push("StaminaRegen", StaminaRegen);
			Value_.Push("RegenDelay", RegenDelay);
		}
		public void Set(SMultiIslandBalance Obj_)
		{
			IslandVelocity = Obj_.IslandVelocity;
			InitTermMin = Obj_.InitTermMin;
			InitTermMax = Obj_.InitTermMax;
			AddTerm = Obj_.AddTerm;
			InitHeight = Obj_.InitHeight;
			ExcludeHeight = Obj_.ExcludeHeight;
			AddHeight = Obj_.AddHeight;
			AddExcludeHeight = Obj_.AddExcludeHeight;
			StaminaTerm = Obj_.StaminaTerm;
			StaminaBonus = Obj_.StaminaBonus;
			StaminaMax = Obj_.StaminaMax;
			StaminaApple = Obj_.StaminaApple;
			StaminaMeat = Obj_.StaminaMeat;
			StaminaChicken = Obj_.StaminaChicken;
			ItemPattern = Obj_.ItemPattern;
			BalanceCount = Obj_.BalanceCount;
			IslandDownPercent = Obj_.IslandDownPercent;
			IslandTypeRange = Obj_.IslandTypeRange;
			WaveCountGold = Obj_.WaveCountGold;
			InitGold = Obj_.InitGold;
			AddGold = Obj_.AddGold;
			IslandInitTime = Obj_.IslandInitTime;
			IslandAddTime = Obj_.IslandAddTime;
			IslandTimeMin = Obj_.IslandTimeMin;
			SpikeIslandTerm = Obj_.SpikeIslandTerm;
			IslandStamina = Obj_.IslandStamina;
			SpikeIslandBalanceCount = Obj_.SpikeIslandBalanceCount;
			ScoreFactorIsland = Obj_.ScoreFactorIsland;
			PointTerm = Obj_.PointTerm;
			ScoreFactorPoint = Obj_.ScoreFactorPoint;
			ScoreFactorLanding = Obj_.ScoreFactorLanding;
			StaminaRegen = Obj_.StaminaRegen;
			RegenDelay = Obj_.RegenDelay;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(IslandVelocity) + "," + 
				SEnumChecker.GetStdName(InitTermMin) + "," + 
				SEnumChecker.GetStdName(InitTermMax) + "," + 
				SEnumChecker.GetStdName(AddTerm) + "," + 
				SEnumChecker.GetStdName(InitHeight) + "," + 
				SEnumChecker.GetStdName(ExcludeHeight) + "," + 
				SEnumChecker.GetStdName(AddHeight) + "," + 
				SEnumChecker.GetStdName(AddExcludeHeight) + "," + 
				SEnumChecker.GetStdName(StaminaTerm) + "," + 
				SEnumChecker.GetStdName(StaminaBonus) + "," + 
				SEnumChecker.GetStdName(StaminaMax) + "," + 
				SEnumChecker.GetStdName(StaminaApple) + "," + 
				SEnumChecker.GetStdName(StaminaMeat) + "," + 
				SEnumChecker.GetStdName(StaminaChicken) + "," + 
				SEnumChecker.GetStdName(ItemPattern) + "," + 
				SEnumChecker.GetStdName(BalanceCount) + "," + 
				SEnumChecker.GetStdName(IslandDownPercent) + "," + 
				SEnumChecker.GetStdName(IslandTypeRange) + "," + 
				SEnumChecker.GetStdName(WaveCountGold) + "," + 
				SEnumChecker.GetStdName(InitGold) + "," + 
				SEnumChecker.GetStdName(AddGold) + "," + 
				SEnumChecker.GetStdName(IslandInitTime) + "," + 
				SEnumChecker.GetStdName(IslandAddTime) + "," + 
				SEnumChecker.GetStdName(IslandTimeMin) + "," + 
				SEnumChecker.GetStdName(SpikeIslandTerm) + "," + 
				SEnumChecker.GetStdName(IslandStamina) + "," + 
				SEnumChecker.GetStdName(SpikeIslandBalanceCount) + "," + 
				SEnumChecker.GetStdName(ScoreFactorIsland) + "," + 
				SEnumChecker.GetStdName(PointTerm) + "," + 
				SEnumChecker.GetStdName(ScoreFactorPoint) + "," + 
				SEnumChecker.GetStdName(ScoreFactorLanding) + "," + 
				SEnumChecker.GetStdName(StaminaRegen) + "," + 
				SEnumChecker.GetStdName(RegenDelay);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(IslandVelocity, "IslandVelocity") + "," + 
				SEnumChecker.GetMemberName(InitTermMin, "InitTermMin") + "," + 
				SEnumChecker.GetMemberName(InitTermMax, "InitTermMax") + "," + 
				SEnumChecker.GetMemberName(AddTerm, "AddTerm") + "," + 
				SEnumChecker.GetMemberName(InitHeight, "InitHeight") + "," + 
				SEnumChecker.GetMemberName(ExcludeHeight, "ExcludeHeight") + "," + 
				SEnumChecker.GetMemberName(AddHeight, "AddHeight") + "," + 
				SEnumChecker.GetMemberName(AddExcludeHeight, "AddExcludeHeight") + "," + 
				SEnumChecker.GetMemberName(StaminaTerm, "StaminaTerm") + "," + 
				SEnumChecker.GetMemberName(StaminaBonus, "StaminaBonus") + "," + 
				SEnumChecker.GetMemberName(StaminaMax, "StaminaMax") + "," + 
				SEnumChecker.GetMemberName(StaminaApple, "StaminaApple") + "," + 
				SEnumChecker.GetMemberName(StaminaMeat, "StaminaMeat") + "," + 
				SEnumChecker.GetMemberName(StaminaChicken, "StaminaChicken") + "," + 
				SEnumChecker.GetMemberName(ItemPattern, "ItemPattern") + "," + 
				SEnumChecker.GetMemberName(BalanceCount, "BalanceCount") + "," + 
				SEnumChecker.GetMemberName(IslandDownPercent, "IslandDownPercent") + "," + 
				SEnumChecker.GetMemberName(IslandTypeRange, "IslandTypeRange") + "," + 
				SEnumChecker.GetMemberName(WaveCountGold, "WaveCountGold") + "," + 
				SEnumChecker.GetMemberName(InitGold, "InitGold") + "," + 
				SEnumChecker.GetMemberName(AddGold, "AddGold") + "," + 
				SEnumChecker.GetMemberName(IslandInitTime, "IslandInitTime") + "," + 
				SEnumChecker.GetMemberName(IslandAddTime, "IslandAddTime") + "," + 
				SEnumChecker.GetMemberName(IslandTimeMin, "IslandTimeMin") + "," + 
				SEnumChecker.GetMemberName(SpikeIslandTerm, "SpikeIslandTerm") + "," + 
				SEnumChecker.GetMemberName(IslandStamina, "IslandStamina") + "," + 
				SEnumChecker.GetMemberName(SpikeIslandBalanceCount, "SpikeIslandBalanceCount") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorIsland, "ScoreFactorIsland") + "," + 
				SEnumChecker.GetMemberName(PointTerm, "PointTerm") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorPoint, "ScoreFactorPoint") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorLanding, "ScoreFactorLanding") + "," + 
				SEnumChecker.GetMemberName(StaminaRegen, "StaminaRegen") + "," + 
				SEnumChecker.GetMemberName(RegenDelay, "RegenDelay");
		}
	}
	public class SMultiBalance : SProto
	{
		public Single MinSpeed = default(Single);
		public Single MaxSpeed = default(Single);
		public Single ShotDelay = default(Single);
		public Int32 TypeFix = default(Int32);
		public Int32 CountFix = default(Int32);
		public Single SpeedFix = default(Single);
		public Int32 Right = default(Int32);
		public Int32 Left = default(Int32);
		public Int32 Bottom = default(Int32);
		public Int32 Diagonal = default(Int32);
		public Int32 FixCount = default(Int32);
		public Int32 FixOnceCount = default(Int32);
		public Int32 ScoreFactorWave = default(Int32);
		public String ItemPattern = string.Empty;
		public Single TermTimeItem = default(Single);
		public Single ShieldTime = default(Single);
		public Single StaminaTime = default(Single);
		public Int32 ScoreFactorPoint = default(Int32);
		public Single TermTimePoint = default(Single);
		public Single RegenDelay = default(Single);
		public SMultiBalance()
		{
		}
		public SMultiBalance(SMultiBalance Obj_)
		{
			MinSpeed = Obj_.MinSpeed;
			MaxSpeed = Obj_.MaxSpeed;
			ShotDelay = Obj_.ShotDelay;
			TypeFix = Obj_.TypeFix;
			CountFix = Obj_.CountFix;
			SpeedFix = Obj_.SpeedFix;
			Right = Obj_.Right;
			Left = Obj_.Left;
			Bottom = Obj_.Bottom;
			Diagonal = Obj_.Diagonal;
			FixCount = Obj_.FixCount;
			FixOnceCount = Obj_.FixOnceCount;
			ScoreFactorWave = Obj_.ScoreFactorWave;
			ItemPattern = Obj_.ItemPattern;
			TermTimeItem = Obj_.TermTimeItem;
			ShieldTime = Obj_.ShieldTime;
			StaminaTime = Obj_.StaminaTime;
			ScoreFactorPoint = Obj_.ScoreFactorPoint;
			TermTimePoint = Obj_.TermTimePoint;
			RegenDelay = Obj_.RegenDelay;
		}
		public SMultiBalance(Single MinSpeed_, Single MaxSpeed_, Single ShotDelay_, Int32 TypeFix_, Int32 CountFix_, Single SpeedFix_, Int32 Right_, Int32 Left_, Int32 Bottom_, Int32 Diagonal_, Int32 FixCount_, Int32 FixOnceCount_, Int32 ScoreFactorWave_, String ItemPattern_, Single TermTimeItem_, Single ShieldTime_, Single StaminaTime_, Int32 ScoreFactorPoint_, Single TermTimePoint_, Single RegenDelay_)
		{
			MinSpeed = MinSpeed_;
			MaxSpeed = MaxSpeed_;
			ShotDelay = ShotDelay_;
			TypeFix = TypeFix_;
			CountFix = CountFix_;
			SpeedFix = SpeedFix_;
			Right = Right_;
			Left = Left_;
			Bottom = Bottom_;
			Diagonal = Diagonal_;
			FixCount = FixCount_;
			FixOnceCount = FixOnceCount_;
			ScoreFactorWave = ScoreFactorWave_;
			ItemPattern = ItemPattern_;
			TermTimeItem = TermTimeItem_;
			ShieldTime = ShieldTime_;
			StaminaTime = StaminaTime_;
			ScoreFactorPoint = ScoreFactorPoint_;
			TermTimePoint = TermTimePoint_;
			RegenDelay = RegenDelay_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref MinSpeed);
			Stream_.Pop(ref MaxSpeed);
			Stream_.Pop(ref ShotDelay);
			Stream_.Pop(ref TypeFix);
			Stream_.Pop(ref CountFix);
			Stream_.Pop(ref SpeedFix);
			Stream_.Pop(ref Right);
			Stream_.Pop(ref Left);
			Stream_.Pop(ref Bottom);
			Stream_.Pop(ref Diagonal);
			Stream_.Pop(ref FixCount);
			Stream_.Pop(ref FixOnceCount);
			Stream_.Pop(ref ScoreFactorWave);
			Stream_.Pop(ref ItemPattern);
			Stream_.Pop(ref TermTimeItem);
			Stream_.Pop(ref ShieldTime);
			Stream_.Pop(ref StaminaTime);
			Stream_.Pop(ref ScoreFactorPoint);
			Stream_.Pop(ref TermTimePoint);
			Stream_.Pop(ref RegenDelay);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("MinSpeed", ref MinSpeed);
			Value_.Pop("MaxSpeed", ref MaxSpeed);
			Value_.Pop("ShotDelay", ref ShotDelay);
			Value_.Pop("TypeFix", ref TypeFix);
			Value_.Pop("CountFix", ref CountFix);
			Value_.Pop("SpeedFix", ref SpeedFix);
			Value_.Pop("Right", ref Right);
			Value_.Pop("Left", ref Left);
			Value_.Pop("Bottom", ref Bottom);
			Value_.Pop("Diagonal", ref Diagonal);
			Value_.Pop("FixCount", ref FixCount);
			Value_.Pop("FixOnceCount", ref FixOnceCount);
			Value_.Pop("ScoreFactorWave", ref ScoreFactorWave);
			Value_.Pop("ItemPattern", ref ItemPattern);
			Value_.Pop("TermTimeItem", ref TermTimeItem);
			Value_.Pop("ShieldTime", ref ShieldTime);
			Value_.Pop("StaminaTime", ref StaminaTime);
			Value_.Pop("ScoreFactorPoint", ref ScoreFactorPoint);
			Value_.Pop("TermTimePoint", ref TermTimePoint);
			Value_.Pop("RegenDelay", ref RegenDelay);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(MinSpeed);
			Stream_.Push(MaxSpeed);
			Stream_.Push(ShotDelay);
			Stream_.Push(TypeFix);
			Stream_.Push(CountFix);
			Stream_.Push(SpeedFix);
			Stream_.Push(Right);
			Stream_.Push(Left);
			Stream_.Push(Bottom);
			Stream_.Push(Diagonal);
			Stream_.Push(FixCount);
			Stream_.Push(FixOnceCount);
			Stream_.Push(ScoreFactorWave);
			Stream_.Push(ItemPattern);
			Stream_.Push(TermTimeItem);
			Stream_.Push(ShieldTime);
			Stream_.Push(StaminaTime);
			Stream_.Push(ScoreFactorPoint);
			Stream_.Push(TermTimePoint);
			Stream_.Push(RegenDelay);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("MinSpeed", MinSpeed);
			Value_.Push("MaxSpeed", MaxSpeed);
			Value_.Push("ShotDelay", ShotDelay);
			Value_.Push("TypeFix", TypeFix);
			Value_.Push("CountFix", CountFix);
			Value_.Push("SpeedFix", SpeedFix);
			Value_.Push("Right", Right);
			Value_.Push("Left", Left);
			Value_.Push("Bottom", Bottom);
			Value_.Push("Diagonal", Diagonal);
			Value_.Push("FixCount", FixCount);
			Value_.Push("FixOnceCount", FixOnceCount);
			Value_.Push("ScoreFactorWave", ScoreFactorWave);
			Value_.Push("ItemPattern", ItemPattern);
			Value_.Push("TermTimeItem", TermTimeItem);
			Value_.Push("ShieldTime", ShieldTime);
			Value_.Push("StaminaTime", StaminaTime);
			Value_.Push("ScoreFactorPoint", ScoreFactorPoint);
			Value_.Push("TermTimePoint", TermTimePoint);
			Value_.Push("RegenDelay", RegenDelay);
		}
		public void Set(SMultiBalance Obj_)
		{
			MinSpeed = Obj_.MinSpeed;
			MaxSpeed = Obj_.MaxSpeed;
			ShotDelay = Obj_.ShotDelay;
			TypeFix = Obj_.TypeFix;
			CountFix = Obj_.CountFix;
			SpeedFix = Obj_.SpeedFix;
			Right = Obj_.Right;
			Left = Obj_.Left;
			Bottom = Obj_.Bottom;
			Diagonal = Obj_.Diagonal;
			FixCount = Obj_.FixCount;
			FixOnceCount = Obj_.FixOnceCount;
			ScoreFactorWave = Obj_.ScoreFactorWave;
			ItemPattern = Obj_.ItemPattern;
			TermTimeItem = Obj_.TermTimeItem;
			ShieldTime = Obj_.ShieldTime;
			StaminaTime = Obj_.StaminaTime;
			ScoreFactorPoint = Obj_.ScoreFactorPoint;
			TermTimePoint = Obj_.TermTimePoint;
			RegenDelay = Obj_.RegenDelay;
		}
		public override string StdName()
		{
			return 
				SEnumChecker.GetStdName(MinSpeed) + "," + 
				SEnumChecker.GetStdName(MaxSpeed) + "," + 
				SEnumChecker.GetStdName(ShotDelay) + "," + 
				SEnumChecker.GetStdName(TypeFix) + "," + 
				SEnumChecker.GetStdName(CountFix) + "," + 
				SEnumChecker.GetStdName(SpeedFix) + "," + 
				SEnumChecker.GetStdName(Right) + "," + 
				SEnumChecker.GetStdName(Left) + "," + 
				SEnumChecker.GetStdName(Bottom) + "," + 
				SEnumChecker.GetStdName(Diagonal) + "," + 
				SEnumChecker.GetStdName(FixCount) + "," + 
				SEnumChecker.GetStdName(FixOnceCount) + "," + 
				SEnumChecker.GetStdName(ScoreFactorWave) + "," + 
				SEnumChecker.GetStdName(ItemPattern) + "," + 
				SEnumChecker.GetStdName(TermTimeItem) + "," + 
				SEnumChecker.GetStdName(ShieldTime) + "," + 
				SEnumChecker.GetStdName(StaminaTime) + "," + 
				SEnumChecker.GetStdName(ScoreFactorPoint) + "," + 
				SEnumChecker.GetStdName(TermTimePoint) + "," + 
				SEnumChecker.GetStdName(RegenDelay);
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(MinSpeed, "MinSpeed") + "," + 
				SEnumChecker.GetMemberName(MaxSpeed, "MaxSpeed") + "," + 
				SEnumChecker.GetMemberName(ShotDelay, "ShotDelay") + "," + 
				SEnumChecker.GetMemberName(TypeFix, "TypeFix") + "," + 
				SEnumChecker.GetMemberName(CountFix, "CountFix") + "," + 
				SEnumChecker.GetMemberName(SpeedFix, "SpeedFix") + "," + 
				SEnumChecker.GetMemberName(Right, "Right") + "," + 
				SEnumChecker.GetMemberName(Left, "Left") + "," + 
				SEnumChecker.GetMemberName(Bottom, "Bottom") + "," + 
				SEnumChecker.GetMemberName(Diagonal, "Diagonal") + "," + 
				SEnumChecker.GetMemberName(FixCount, "FixCount") + "," + 
				SEnumChecker.GetMemberName(FixOnceCount, "FixOnceCount") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorWave, "ScoreFactorWave") + "," + 
				SEnumChecker.GetMemberName(ItemPattern, "ItemPattern") + "," + 
				SEnumChecker.GetMemberName(TermTimeItem, "TermTimeItem") + "," + 
				SEnumChecker.GetMemberName(ShieldTime, "ShieldTime") + "," + 
				SEnumChecker.GetMemberName(StaminaTime, "StaminaTime") + "," + 
				SEnumChecker.GetMemberName(ScoreFactorPoint, "ScoreFactorPoint") + "," + 
				SEnumChecker.GetMemberName(TermTimePoint, "TermTimePoint") + "," + 
				SEnumChecker.GetMemberName(RegenDelay, "RegenDelay");
		}
	}
	public class SMultiItem : SProto
	{
		public EMultiItemType ItemType = default(EMultiItemType);
		public EText Description = default(EText);
		public SMultiItem()
		{
		}
		public SMultiItem(SMultiItem Obj_)
		{
			ItemType = Obj_.ItemType;
			Description = Obj_.Description;
		}
		public SMultiItem(EMultiItemType ItemType_, EText Description_)
		{
			ItemType = ItemType_;
			Description = Description_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref ItemType);
			Stream_.Pop(ref Description);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("ItemType", ref ItemType);
			Value_.Pop("Description", ref Description);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(ItemType);
			Stream_.Push(Description);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("ItemType", ItemType);
			Value_.Push("Description", Description);
		}
		public void Set(SMultiItem Obj_)
		{
			ItemType = Obj_.ItemType;
			Description = Obj_.Description;
		}
		public override string StdName()
		{
			return 
				"bb.EMultiItemType" + "," + 
				"bb.EText";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(ItemType, "ItemType") + "," + 
				SEnumChecker.GetMemberName(Description, "Description");
		}
	}
	public class SMultiItemDodge : SMultiItem
	{
		public Single MultiDodgeTime = default(Single);
		public Single MultiDodgeValue = default(Single);
		public Int32 MultiDodgeRand = default(Int32);
		public SMultiItemDodge()
		{
		}
		public SMultiItemDodge(SMultiItemDodge Obj_) : base(Obj_)
		{
			MultiDodgeTime = Obj_.MultiDodgeTime;
			MultiDodgeValue = Obj_.MultiDodgeValue;
			MultiDodgeRand = Obj_.MultiDodgeRand;
		}
		public SMultiItemDodge(SMultiItem Super_, Single MultiDodgeTime_, Single MultiDodgeValue_, Int32 MultiDodgeRand_) : base(Super_)
		{
			MultiDodgeTime = MultiDodgeTime_;
			MultiDodgeValue = MultiDodgeValue_;
			MultiDodgeRand = MultiDodgeRand_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref MultiDodgeTime);
			Stream_.Pop(ref MultiDodgeValue);
			Stream_.Pop(ref MultiDodgeRand);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("MultiDodgeTime", ref MultiDodgeTime);
			Value_.Pop("MultiDodgeValue", ref MultiDodgeValue);
			Value_.Pop("MultiDodgeRand", ref MultiDodgeRand);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(MultiDodgeTime);
			Stream_.Push(MultiDodgeValue);
			Stream_.Push(MultiDodgeRand);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("MultiDodgeTime", MultiDodgeTime);
			Value_.Push("MultiDodgeValue", MultiDodgeValue);
			Value_.Push("MultiDodgeRand", MultiDodgeRand);
		}
		public void Set(SMultiItemDodge Obj_)
		{
			base.Set(Obj_);
			MultiDodgeTime = Obj_.MultiDodgeTime;
			MultiDodgeValue = Obj_.MultiDodgeValue;
			MultiDodgeRand = Obj_.MultiDodgeRand;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(MultiDodgeTime) + "," + 
				SEnumChecker.GetStdName(MultiDodgeValue) + "," + 
				SEnumChecker.GetStdName(MultiDodgeRand);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(MultiDodgeTime, "MultiDodgeTime") + "," + 
				SEnumChecker.GetMemberName(MultiDodgeValue, "MultiDodgeValue") + "," + 
				SEnumChecker.GetMemberName(MultiDodgeRand, "MultiDodgeRand");
		}
	}
	public class SMultiItemIsland : SMultiItem
	{
		public Single MultiIslandTime = default(Single);
		public Single MultiIslandValue = default(Single);
		public Int32 MultiIslandRand = default(Int32);
		public SMultiItemIsland()
		{
		}
		public SMultiItemIsland(SMultiItemIsland Obj_) : base(Obj_)
		{
			MultiIslandTime = Obj_.MultiIslandTime;
			MultiIslandValue = Obj_.MultiIslandValue;
			MultiIslandRand = Obj_.MultiIslandRand;
		}
		public SMultiItemIsland(SMultiItem Super_, Single MultiIslandTime_, Single MultiIslandValue_, Int32 MultiIslandRand_) : base(Super_)
		{
			MultiIslandTime = MultiIslandTime_;
			MultiIslandValue = MultiIslandValue_;
			MultiIslandRand = MultiIslandRand_;
		}
		public override void Push(CStream Stream_)
		{
			base.Push(Stream_);
			Stream_.Pop(ref MultiIslandTime);
			Stream_.Pop(ref MultiIslandValue);
			Stream_.Pop(ref MultiIslandRand);
		}
		public override void Push(JsonDataObject Value_)
		{
			base.Push(Value_);
			Value_.Pop("MultiIslandTime", ref MultiIslandTime);
			Value_.Pop("MultiIslandValue", ref MultiIslandValue);
			Value_.Pop("MultiIslandRand", ref MultiIslandRand);
		}
		public override void Pop(CStream Stream_)
		{
			base.Pop(Stream_);
			Stream_.Push(MultiIslandTime);
			Stream_.Push(MultiIslandValue);
			Stream_.Push(MultiIslandRand);
		}
		public override void Pop(JsonDataObject Value_)
		{
			base.Pop(Value_);
			Value_.Push("MultiIslandTime", MultiIslandTime);
			Value_.Push("MultiIslandValue", MultiIslandValue);
			Value_.Push("MultiIslandRand", MultiIslandRand);
		}
		public void Set(SMultiItemIsland Obj_)
		{
			base.Set(Obj_);
			MultiIslandTime = Obj_.MultiIslandTime;
			MultiIslandValue = Obj_.MultiIslandValue;
			MultiIslandRand = Obj_.MultiIslandRand;
		}
		public override string StdName()
		{
			return 
				base.StdName() + "," + 
				SEnumChecker.GetStdName(MultiIslandTime) + "," + 
				SEnumChecker.GetStdName(MultiIslandValue) + "," + 
				SEnumChecker.GetStdName(MultiIslandRand);
		}
		public override string MemberName()
		{
			return 
				base.MemberName() + "," + 
				SEnumChecker.GetMemberName(MultiIslandTime, "MultiIslandTime") + "," + 
				SEnumChecker.GetMemberName(MultiIslandValue, "MultiIslandValue") + "," + 
				SEnumChecker.GetMemberName(MultiIslandRand, "MultiIslandRand");
		}
	}
	public class SMultiItemMeta : SProto
	{
		public EMultiItemType ItemType = default(EMultiItemType);
		public Single MultiDodgeTime = default(Single);
		public Single MultiDodgeValue = default(Single);
		public Int32 MultiDodgeRand = default(Int32);
		public Single MultiIslandTime = default(Single);
		public Single MultiIslandValue = default(Single);
		public Int32 MultiIslandRand = default(Int32);
		public EText Description = default(EText);
		public SMultiItemMeta()
		{
		}
		public SMultiItemMeta(SMultiItemMeta Obj_)
		{
			ItemType = Obj_.ItemType;
			MultiDodgeTime = Obj_.MultiDodgeTime;
			MultiDodgeValue = Obj_.MultiDodgeValue;
			MultiDodgeRand = Obj_.MultiDodgeRand;
			MultiIslandTime = Obj_.MultiIslandTime;
			MultiIslandValue = Obj_.MultiIslandValue;
			MultiIslandRand = Obj_.MultiIslandRand;
			Description = Obj_.Description;
		}
		public SMultiItemMeta(EMultiItemType ItemType_, Single MultiDodgeTime_, Single MultiDodgeValue_, Int32 MultiDodgeRand_, Single MultiIslandTime_, Single MultiIslandValue_, Int32 MultiIslandRand_, EText Description_)
		{
			ItemType = ItemType_;
			MultiDodgeTime = MultiDodgeTime_;
			MultiDodgeValue = MultiDodgeValue_;
			MultiDodgeRand = MultiDodgeRand_;
			MultiIslandTime = MultiIslandTime_;
			MultiIslandValue = MultiIslandValue_;
			MultiIslandRand = MultiIslandRand_;
			Description = Description_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref ItemType);
			Stream_.Pop(ref MultiDodgeTime);
			Stream_.Pop(ref MultiDodgeValue);
			Stream_.Pop(ref MultiDodgeRand);
			Stream_.Pop(ref MultiIslandTime);
			Stream_.Pop(ref MultiIslandValue);
			Stream_.Pop(ref MultiIslandRand);
			Stream_.Pop(ref Description);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("ItemType", ref ItemType);
			Value_.Pop("MultiDodgeTime", ref MultiDodgeTime);
			Value_.Pop("MultiDodgeValue", ref MultiDodgeValue);
			Value_.Pop("MultiDodgeRand", ref MultiDodgeRand);
			Value_.Pop("MultiIslandTime", ref MultiIslandTime);
			Value_.Pop("MultiIslandValue", ref MultiIslandValue);
			Value_.Pop("MultiIslandRand", ref MultiIslandRand);
			Value_.Pop("Description", ref Description);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(ItemType);
			Stream_.Push(MultiDodgeTime);
			Stream_.Push(MultiDodgeValue);
			Stream_.Push(MultiDodgeRand);
			Stream_.Push(MultiIslandTime);
			Stream_.Push(MultiIslandValue);
			Stream_.Push(MultiIslandRand);
			Stream_.Push(Description);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("ItemType", ItemType);
			Value_.Push("MultiDodgeTime", MultiDodgeTime);
			Value_.Push("MultiDodgeValue", MultiDodgeValue);
			Value_.Push("MultiDodgeRand", MultiDodgeRand);
			Value_.Push("MultiIslandTime", MultiIslandTime);
			Value_.Push("MultiIslandValue", MultiIslandValue);
			Value_.Push("MultiIslandRand", MultiIslandRand);
			Value_.Push("Description", Description);
		}
		public void Set(SMultiItemMeta Obj_)
		{
			ItemType = Obj_.ItemType;
			MultiDodgeTime = Obj_.MultiDodgeTime;
			MultiDodgeValue = Obj_.MultiDodgeValue;
			MultiDodgeRand = Obj_.MultiDodgeRand;
			MultiIslandTime = Obj_.MultiIslandTime;
			MultiIslandValue = Obj_.MultiIslandValue;
			MultiIslandRand = Obj_.MultiIslandRand;
			Description = Obj_.Description;
		}
		public override string StdName()
		{
			return 
				"bb.EMultiItemType" + "," + 
				SEnumChecker.GetStdName(MultiDodgeTime) + "," + 
				SEnumChecker.GetStdName(MultiDodgeValue) + "," + 
				SEnumChecker.GetStdName(MultiDodgeRand) + "," + 
				SEnumChecker.GetStdName(MultiIslandTime) + "," + 
				SEnumChecker.GetStdName(MultiIslandValue) + "," + 
				SEnumChecker.GetStdName(MultiIslandRand) + "," + 
				"bb.EText";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(ItemType, "ItemType") + "," + 
				SEnumChecker.GetMemberName(MultiDodgeTime, "MultiDodgeTime") + "," + 
				SEnumChecker.GetMemberName(MultiDodgeValue, "MultiDodgeValue") + "," + 
				SEnumChecker.GetMemberName(MultiDodgeRand, "MultiDodgeRand") + "," + 
				SEnumChecker.GetMemberName(MultiIslandTime, "MultiIslandTime") + "," + 
				SEnumChecker.GetMemberName(MultiIslandValue, "MultiIslandValue") + "," + 
				SEnumChecker.GetMemberName(MultiIslandRand, "MultiIslandRand") + "," + 
				SEnumChecker.GetMemberName(Description, "Description");
		}
	}
	public class SModeEventMeta : SProto
	{
		public EPlayMode Mode = default(EPlayMode);
		public Int32 BeginHour = default(Int32);
		public Int32 BeginMin = default(Int32);
		public Int32 BeginSec = default(Int32);
		public Int32 EndHour = default(Int32);
		public Int32 EndMin = default(Int32);
		public Int32 EndSec = default(Int32);
		public EText ETextName = default(EText);
		public SModeEventMeta()
		{
		}
		public SModeEventMeta(SModeEventMeta Obj_)
		{
			Mode = Obj_.Mode;
			BeginHour = Obj_.BeginHour;
			BeginMin = Obj_.BeginMin;
			BeginSec = Obj_.BeginSec;
			EndHour = Obj_.EndHour;
			EndMin = Obj_.EndMin;
			EndSec = Obj_.EndSec;
			ETextName = Obj_.ETextName;
		}
		public SModeEventMeta(EPlayMode Mode_, Int32 BeginHour_, Int32 BeginMin_, Int32 BeginSec_, Int32 EndHour_, Int32 EndMin_, Int32 EndSec_, EText ETextName_)
		{
			Mode = Mode_;
			BeginHour = BeginHour_;
			BeginMin = BeginMin_;
			BeginSec = BeginSec_;
			EndHour = EndHour_;
			EndMin = EndMin_;
			EndSec = EndSec_;
			ETextName = ETextName_;
		}
		public override void Push(CStream Stream_)
		{
			Stream_.Pop(ref Mode);
			Stream_.Pop(ref BeginHour);
			Stream_.Pop(ref BeginMin);
			Stream_.Pop(ref BeginSec);
			Stream_.Pop(ref EndHour);
			Stream_.Pop(ref EndMin);
			Stream_.Pop(ref EndSec);
			Stream_.Pop(ref ETextName);
		}
		public override void Push(JsonDataObject Value_)
		{
			Value_.Pop("Mode", ref Mode);
			Value_.Pop("BeginHour", ref BeginHour);
			Value_.Pop("BeginMin", ref BeginMin);
			Value_.Pop("BeginSec", ref BeginSec);
			Value_.Pop("EndHour", ref EndHour);
			Value_.Pop("EndMin", ref EndMin);
			Value_.Pop("EndSec", ref EndSec);
			Value_.Pop("ETextName", ref ETextName);
		}
		public override void Pop(CStream Stream_)
		{
			Stream_.Push(Mode);
			Stream_.Push(BeginHour);
			Stream_.Push(BeginMin);
			Stream_.Push(BeginSec);
			Stream_.Push(EndHour);
			Stream_.Push(EndMin);
			Stream_.Push(EndSec);
			Stream_.Push(ETextName);
		}
		public override void Pop(JsonDataObject Value_)
		{
			Value_.Push("Mode", Mode);
			Value_.Push("BeginHour", BeginHour);
			Value_.Push("BeginMin", BeginMin);
			Value_.Push("BeginSec", BeginSec);
			Value_.Push("EndHour", EndHour);
			Value_.Push("EndMin", EndMin);
			Value_.Push("EndSec", EndSec);
			Value_.Push("ETextName", ETextName);
		}
		public void Set(SModeEventMeta Obj_)
		{
			Mode = Obj_.Mode;
			BeginHour = Obj_.BeginHour;
			BeginMin = Obj_.BeginMin;
			BeginSec = Obj_.BeginSec;
			EndHour = Obj_.EndHour;
			EndMin = Obj_.EndMin;
			EndSec = Obj_.EndSec;
			ETextName = Obj_.ETextName;
		}
		public override string StdName()
		{
			return 
				"bb.EPlayMode" + "," + 
				SEnumChecker.GetStdName(BeginHour) + "," + 
				SEnumChecker.GetStdName(BeginMin) + "," + 
				SEnumChecker.GetStdName(BeginSec) + "," + 
				SEnumChecker.GetStdName(EndHour) + "," + 
				SEnumChecker.GetStdName(EndMin) + "," + 
				SEnumChecker.GetStdName(EndSec) + "," + 
				"bb.EText";
		}
		public override string MemberName()
		{
			return 
				SEnumChecker.GetMemberName(Mode, "Mode") + "," + 
				SEnumChecker.GetMemberName(BeginHour, "BeginHour") + "," + 
				SEnumChecker.GetMemberName(BeginMin, "BeginMin") + "," + 
				SEnumChecker.GetMemberName(BeginSec, "BeginSec") + "," + 
				SEnumChecker.GetMemberName(EndHour, "EndHour") + "," + 
				SEnumChecker.GetMemberName(EndMin, "EndMin") + "," + 
				SEnumChecker.GetMemberName(EndSec, "EndSec") + "," + 
				SEnumChecker.GetMemberName(ETextName, "ETextName");
		}
	}
}
