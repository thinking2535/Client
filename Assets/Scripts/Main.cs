using bb;
using rso.unity;
using System;
using UnityEngine;

public partial class Main : MonoBehaviour
{
    [SerializeField] public CreatePopup createPopupPrefab;
    [SerializeField] public UpdateInfoPopup updateInfoPopupPrefab;
    [SerializeField] public RewardPopup rewardPopupPrefab;
    [SerializeField] public ScrollConfirmPopup scrollConfirmPopupPrefab;
    [SerializeField] public CharacterInfoPopup characterInfoPopupPrefab;
    [SerializeField] public SettingPopup settingPopupPrefab;
    [SerializeField] public LanguagePopup languagePopupPrefab;
    [SerializeField] public CouponPopup couponPopupPrefab;
    [SerializeField] public ToolTipPopup toolTipPopupPrefab;
    [SerializeField] public MessagePopup messagePopupPrefab;
    [SerializeField] public BuyingResourcePopup buyingResourcePopupPrefab;
    [SerializeField] public AccountInfoPopup accountInfoPopupPrefab;
    [SerializeField] public ChangingNicknamePopup changingNicknamePopupPrefab;
    [SerializeField] public QuestPopup questPopupPrefab;
    [SerializeField] public RankingPopup rankingPopupPrefab;

    [SerializeField] public IntroScene introScenePrefab;
    [SerializeField] public TutorialScene battleTutorialScenePrefab;
    [SerializeField] public LobbyScene lobbyScenePrefab;
    [SerializeField] public CharacterListScene characterListScenePrefab;
    [SerializeField] public ShopScene shopScenePrefab;
    [SerializeField] public RankingRewardScene rankingRewardScenePrefab;
    [SerializeField] public MultiMatchingScene multiMatchingScenePrefab;
    [SerializeField] public MultiBattleEndScene multiBattleEndScenePrefab;
    [SerializeField] public MultiBattleEndDrawScene multiBattleEndDrawScenePrefab;
    [SerializeField] public MultiBattleEndInvalidScene multiBattleEndInvalidScenePrefab;

    [SerializeField] CGlobal.EServer _Server = CGlobal.EServer.Internal;
    [SerializeField] bool _ServerAnchor = false;

    CLogControl LogControl = null;
    [SerializeField] bool _IsGuest = false;
    [SerializeField] bool _IsDebug = false;

    CFPS _FPS = new CFPS();

    void LogCallback(string condition, string stackTrace, LogType type)
    {
    }
    void Awake()
    {
        GlobalVariable.main = this;

        Application.targetFrameRate = bb.global.c_FPS;
        Time.fixedDeltaTime = 1.0f / bb.global.c_FPS;
        Physics2D.gravity = new Vector2(0.0f, bb.global.c_Gravity);

        LogControl = new CLogControl(new long[] { bb.global.c_Ver_Main }, "Logs/", LogCallback);

        string[][] MusicFiles = new string[(Int32)EPlayList.Max][];
        MusicFiles[(Int32)EPlayList.Normal] = new string[] {"Musics/Lobby"};
        MusicFiles[(Int32)EPlayList.Battle] = new string[] {
            "Musics/Stage1",
            "Musics/Stage2",
            "Musics/Stage3",
            "Musics/Stage4",
            "Musics/Stage5"
        };
        MusicFiles[(Int32)EPlayList.Island] = new string[] { "Musics/ISLAND_bgm" };
        MusicFiles[(Int32)EPlayList.Dodge] = new string[] { "Musics/DODGE_bgm" };

        for (Int32 i = 0; i < (Int32)EPlayList.Max; ++i)
            Debug.Assert(MusicFiles[i] != null, "MusicFiles must fill");

        var Music = new CAudioMusic(gameObject.AddComponent<AudioSource>(), MusicFiles);
        Music.SetRandom(true);
        Music.repeatOne = true;

        string[] SoundFiles = new string[(Int32)ESound.Max];
        // Init String
        for (Int32 i = 0; i < (Int32)ESound.Max; ++i)
            SoundFiles[i] = "Sounds/" + ((ESound)i).ToString();

        var Sound = new CAudioSound(gameObject.AddComponent<AudioSource>(), SoundFiles);

        AnalyticsManager.DailyInit();

        CGlobal.Init(
            Music,
            Sound,
            _Server,
            _ServerAnchor,
            Link, LinkFail, UnLink, Recv, Error, Check);

        CGlobal.NetControl.AddSendProto<SChatNetCs>((Int32)EProtoNetCs.Chat);
        CGlobal.NetControl.AddSendProto<SFCMTokenSetNetCs>((Int32)EProtoNetCs.FCMTokenSet);
        CGlobal.NetControl.AddSendProto<SFCMTokenUnsetNetCs>((Int32)EProtoNetCs.FCMTokenUnset);
        CGlobal.NetControl.AddSendProto<SFCMCanPushAtNightNetCs>((Int32)EProtoNetCs.FCMCanPushAtNight);
        CGlobal.NetControl.AddSendProto<SChangeLanguageNetCs>((Int32)EProtoNetCs.ChangeLanguage);
        CGlobal.NetControl.AddSendProto<SSelectCharNetCs>((Int32)EProtoNetCs.SelectChar);

        CGlobal.NetControl.AddSendProto<SBattleTouchNetCs>((Int32)EProtoNetCs.BattleTouch);
        CGlobal.NetControl.AddSendProto<SBattlePushNetCs>((Int32)EProtoNetCs.BattlePush);

        CGlobal.NetControl.AddSendProto<SMultiBattleJoinNetCs>((Int32)EProtoNetCs.MultiBattleJoin);
        CGlobal.NetControl.AddSendProto<SMultiBattleOutNetCs>((Int32)EProtoNetCs.MultiBattleOut);
        CGlobal.NetControl.AddSendProto<SMultiBattleIconNetCs>((Int32)EProtoNetCs.MultiBattleIcon);

        CGlobal.NetControl.AddSendProto<SArrowDodgeBattleJoinNetCs>((Int32)EProtoNetCs.ArrowDodgeBattleJoin);
        CGlobal.NetControl.AddSendProto<SArrowDodgeBattleEndNetCs>((Int32)EProtoNetCs.ArrowDodgeBattleEnd);
        CGlobal.NetControl.AddSendProto<SFlyAwayBattleJoinNetCs>((Int32)EProtoNetCs.FlyAwayBattleJoin);
        CGlobal.NetControl.AddSendProto<SFlyAwayBattleEndNetCs>((Int32)EProtoNetCs.FlyAwayBattleEnd);

        CGlobal.NetControl.AddSendProto<SRankRewardNetCs>((Int32)EProtoNetCs.RankReward);
        CGlobal.NetControl.AddSendProto<SQuestRewardNetCs>((Int32)EProtoNetCs.QuestReward);
        CGlobal.NetControl.AddSendProto<SQuestDailyCompleteRewardNetCs>((Int32)EProtoNetCs.QuestDailyCompleteReward);
        CGlobal.NetControl.AddSendProto<SChangeNickNetCs>((Int32)EProtoNetCs.ChangeNick);
        CGlobal.NetControl.AddSendProto<SBuyNetCs>((Int32)EProtoNetCs.Buy);
        CGlobal.NetControl.AddSendProto<SBuyCharNetCs>((Int32)EProtoNetCs.BuyChar);
        CGlobal.NetControl.AddSendProto<SBuyResourceNetCs>((Int32)EProtoNetCs.BuyResource);
        CGlobal.NetControl.AddSendProto<SCouponUseNetCs>((Int32)EProtoNetCs.CouponUse);
        CGlobal.NetControl.AddSendProto<STutorialRewardNetCs>((Int32)EProtoNetCs.TutorialReward);
        CGlobal.NetControl.AddSendProto<SRankingRewardInfoNetCs>((Int32)EProtoNetCs.RankingRewardInfo);
        CGlobal.NetControl.AddSendProto<SRankingRewardNetCs>((Int32)EProtoNetCs.RankingReward);

        CGlobal.NetControl.AddRecvProto<SRetNetSc>(EProtoNetSc.Ret, RetNetSc);
        CGlobal.NetControl.AddRecvProto<SMsgNetSc>(EProtoNetSc.Msg, MsgNetSc);
        CGlobal.NetControl.AddRecvProto<SLoginNetSc>(EProtoNetSc.Login, LoginNetSc);
        CGlobal.NetControl.AddRecvProto<SLobbyNetSc>(EProtoNetSc.Lobby, LobbyNetSc);
        CGlobal.NetControl.AddRecvProto<SChatNetSc>(EProtoNetSc.Chat, ChatNetSc);
        CGlobal.NetControl.AddRecvProto<SUserExpNetSc>(EProtoNetSc.UserExp, ExpNetSc);
        CGlobal.NetControl.AddRecvProto<SResourcesNetSc>(EProtoNetSc.Resources, ResourcesNetSc);

        CGlobal.NetControl.AddRecvProto<SBattleSyncNetSc>(EProtoNetSc.BattleSync, BattleSyncNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleDirectNetSc>(EProtoNetSc.BattleDirect, BattleDirectNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleFlapNetSc>(EProtoNetSc.BattleFlap, BattleFlapNetSc);
        CGlobal.NetControl.AddRecvProto<SBattlePumpNetSc>(EProtoNetSc.BattlePump, BattlePumpNetSc);

        CGlobal.NetControl.AddRecvProto<SMultiBattleJoinNetSc>(EProtoNetSc.MultiBattleJoin, MultiBattleJoinNetSc);
        CGlobal.NetControl.AddRecvProto<SMultiBattleOutNetSc>(EProtoNetSc.MultiBattleOut, MultiBattleOutNetSc);
        CGlobal.NetControl.AddRecvProto<SMultiBattleBeginNetSc>(EProtoNetSc.MultiBattleBegin, MultiBattleBeginNetSc);
        CGlobal.NetControl.AddRecvProto<SMultiBattleStartNetSc>(EProtoNetSc.MultiBattleStart, MultiBattleStartNetSc);
        CGlobal.NetControl.AddRecvProto<SMultiBattleEndNetSc>(EProtoNetSc.MultiBattleEnd, MultiBattleEndNetSc);
        CGlobal.NetControl.AddRecvProto<SMultiBattleEndDrawNetSc>(EProtoNetSc.MultiBattleEndDraw, MultiBattleEndDrawNetSc);
        CGlobal.NetControl.AddRecvProto<SMultiBattleEndInvalidNetSc>(EProtoNetSc.MultiBattleEndInvalid, MultiBattleEndInvalidNetSc);
        CGlobal.NetControl.AddRecvProto<SMultiBattleIconNetSc>(EProtoNetSc.MultiBattleIcon, MultiBattleIconNetSc);
        CGlobal.NetControl.AddRecvProto<SMultiBattleLinkNetSc>(EProtoNetSc.MultiBattleLink, MultiBattleLinkNetSc);
        CGlobal.NetControl.AddRecvProto<SMultiBattleUnLinkNetSc>(EProtoNetSc.MultiBattleUnLink, MultiBattleUnLinkNetSc);
        CGlobal.NetControl.AddRecvProto<SInvalidDisconnectInfoNetSc>(EProtoNetSc.InvalidDisconnectInfo, InvalidDisconnectInfoNetSc);

        CGlobal.NetControl.AddRecvProto<SArrowDodgeBattleJoinNetSc>(EProtoNetSc.ArrowDodgeBattleJoin, ArrowDodgeBattleJoinNetSc);
        CGlobal.NetControl.AddRecvProto<SArrowDodgeBattleBeginNetSc>(EProtoNetSc.ArrowDodgeBattleBegin, ArrowDodgeBattleBeginNetSc);
        CGlobal.NetControl.AddRecvProto<SArrowDodgeBattleStartNetSc>(EProtoNetSc.ArrowDodgeBattleStart, ArrowDodgeBattleStartNetSc);
        CGlobal.NetControl.AddRecvProto<SArrowDodgeBattleEndNetSc>(EProtoNetSc.ArrowDodgeBattleEnd, ArrowDodgeBattleEndNetSc);
        CGlobal.NetControl.AddRecvProto<SFlyAwayBattleJoinNetSc>(EProtoNetSc.FlyAwayBattleJoin, FlyAwayBattleJoinNetSc);
        CGlobal.NetControl.AddRecvProto<SFlyAwayBattleBeginNetSc>(EProtoNetSc.FlyAwayBattleBegin, FlyAwayBattleBeginNetSc);
        CGlobal.NetControl.AddRecvProto<SFlyAwayBattleStartNetSc>(EProtoNetSc.FlyAwayBattleStart, FlyAwayBattleStartNetSc);
        CGlobal.NetControl.AddRecvProto<SFlyAwayBattleEndNetSc>(EProtoNetSc.FlyAwayBattleEnd, FlyAwayBattleEndNetSc);

        CGlobal.NetControl.AddRecvProto<SRankRewardNetSc>(EProtoNetSc.RankReward, RankRewardNetSc);
        CGlobal.NetControl.AddRecvProto<SQuestGotNetSc>(EProtoNetSc.QuestGot, QuestGotNetSc);
        CGlobal.NetControl.AddRecvProto<SQuestSetNetSc>(EProtoNetSc.QuestSet, QuestSetNetSc);
        CGlobal.NetControl.AddRecvProto<SQuestDoneNetSc>(EProtoNetSc.QuestDone, QuestDoneNetSc);
        CGlobal.NetControl.AddRecvProto<SQuestRewardNetSc>(EProtoNetSc.QuestReward, QuestRewardNetSc);
        CGlobal.NetControl.AddRecvProto<SQuestDailyCompleteRewardNetSc>(EProtoNetSc.QuestDailyCompleteReward, QuestDailyCompleteRewardNetSc);
        CGlobal.NetControl.AddRecvProto<SChangeNickNetSc>(EProtoNetSc.ChangeNick, ChangeNickNetSc);
        CGlobal.NetControl.AddRecvProto<SChangeNickFailNetSc>(EProtoNetSc.ChangeNickFail, ChangeNickFailNetSc);
        CGlobal.NetControl.AddRecvProto<SSetPointNetSc>(EProtoNetSc.SetPoint, SetPoint);
        CGlobal.NetControl.AddRecvProto<SSetCharNetSc>(EProtoNetSc.SetChar, SetCharacter);
        CGlobal.NetControl.AddRecvProto<SUnsetCharNetSc>(EProtoNetSc.UnsetChar, DelCharacter);
        CGlobal.NetControl.AddRecvProto<SBuyNetSc>(EProtoNetSc.Buy, BuyNetSc);
        CGlobal.NetControl.AddRecvProto<SBuyCharNetSc>(EProtoNetSc.BuyChar, BuyCharNetSc);
        CGlobal.NetControl.AddRecvProto<SCouponUseNetSc>(EProtoNetSc.CouponUse, CouponUseNetSc);
        CGlobal.NetControl.AddRecvProto<SCouponUseFailNetSc>(EProtoNetSc.CouponUseFail, CoupontFailNetSc);
        CGlobal.NetControl.AddRecvProto<SBuyResourceNetSc>(EProtoNetSc.BuyResource, BuyResourceNetSc);
        CGlobal.NetControl.AddRecvProto<SRankingRewardInfoNetSc>(EProtoNetSc.RankingRewardInfo, RankingRewardInfoNetSc);
        CGlobal.NetControl.AddRecvProto<SRankingRewardNetSc>(EProtoNetSc.RankingReward, RankingRewardNetSc);
        CGlobal.NetControl.AddRecvProto<SRankingRewardFailNetSc>(EProtoNetSc.RankingRewardFail, RankingRewardFailNetSc);

        CGlobal.RedDotControl.SetReddotOn(RedDotControl.EReddotType.Shop);

#if UNITY_EDITOR
        CGlobal.IsGuest = _IsGuest;
        CGlobal.IsDebugMode = _IsDebug;
#endif
    }
    void Update()
    {
        CGlobal.Update();

        if (CGlobal.IsDebugMode)
            _FPS.Set();
    }
    private void OnGUI()
    {
        if (CGlobal.IsDebugMode)
        {
            GUI.Label(new Rect(60, 200, 100, 20), "FPS : " + _FPS.FPS.ToString());
            GUI.Label(new Rect(60, 230, 1000, 20), "Latency : " + CGlobal.NetControl.Latency(0).TotalMilliseconds.ToString());
        }
    }
    private void OnApplicationQuit()
    {
        CGlobal.WillClose = true;
        CGlobal.Dispose();
    }
}
