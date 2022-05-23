using rso.core;
using rso.game;
using rso.net;
using rso.unity;
using bb;
using System;
//using Unity.Advertisement.IosSupport;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public partial class Main : MonoBehaviour
{
    [SerializeField] CGlobal.EServer _Server = CGlobal.EServer.Internal;
    [SerializeField] bool _ServerAnchor = false;

    CLogControl _LogControl = null;
    [SerializeField] ProgressLoading _ProgressLoading = null;
    [SerializeField] CreatePopup _CreatePopup = null;
    [SerializeField] PopupSystem _SystemPopup = null;
    [SerializeField] RewardPopup _RewardPopup = null;
    [SerializeField] ScrollConfirmPopup _ScrollConfirmPopup = null;
    [SerializeField] ToolTipPopup _ToolTipPopup = null;
    [SerializeField] UpdateInfoPopup _UpdateInfoPopup = null;
    [SerializeField] LogPanel _LogPanel = null;
    [SerializeField] bool _viewLogPanel = false;
    [SerializeField] bool _viewTutorial = false;
    [SerializeField] IAPManager _IAPManager = null;
    [SerializeField] ADManager _ADManager = null;
    [SerializeField] bool _IsGoogleAdTest = false;
    [SerializeField] bool _IsGuest = false;
    [SerializeField] bool _IsDebug = false;

    CFPS _FPS = new CFPS();
    
    void LogCallback(string condition, string stackTrace, LogType type)
    {
        string log = String.Format("type={0} :: condition={1} :: stackTrace={2}\n", type.ToString(), condition, stackTrace);
        _LogPanel.AddLog(log);
    }
    void Awake()
    {
        Application.targetFrameRate = bb.global.c_FPS;
        Time.fixedDeltaTime = 1.0f / bb.global.c_FPS;
        Physics2D.gravity = new Vector2(0.0f, bb.global.c_Gravity);

        //로그 패널 초기화. CLogControl 보다 먼저 해주어야 됨.
        CGlobal.SetDebug(_LogPanel);
        CGlobal.LogPanel.init();
        CGlobal.ViewLogPanel = _viewLogPanel;
        CGlobal.LogPanel.ViewDebugPanel(_viewLogPanel);

#if UNITY_EDITOR || UNITY_STANDALONE
        _LogControl = new CLogControl(100, new long[] { bb.global.c_Ver_Main }, "../Logs/", LogCallback);
#else
        _LogControl = new CLogControl(100, new long[] { bb.global.c_Ver_Main }, "Logs/", LogCallback);
#endif

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

        var Client = new rso.game.CClient(new SVersion(bb.global.c_Ver_Main, CGlobal.MetaData.Checksum));
        Client.LinkFunc = Link;
        Client.LinkFailFunc = LinkFail;
        Client.UnLinkFunc = UnLink;
        Client.RecvFunc = Recv;
        Client.ErrorFunc = Error;
        Client.CheckFunc = Check;
        
        CGlobal.Init(
            Music,
            Sound,
            _Server,
            _ServerAnchor,
            Client,
            _CreatePopup,
            _SystemPopup,
            _RewardPopup,
            _ToolTipPopup,
            _ProgressLoading,
            _ScrollConfirmPopup,
            _UpdateInfoPopup,
            _IAPManager,
            _ADManager);

        CGlobal.NetControl.AddSendProto<SChatNetCs>((Int32)EProtoNetCs.Chat);
        CGlobal.NetControl.AddSendProto<SFCMTokenSetNetCs>((Int32)EProtoNetCs.FCMTokenSet);
        CGlobal.NetControl.AddSendProto<SFCMTokenUnsetNetCs>((Int32)EProtoNetCs.FCMTokenUnset);
        CGlobal.NetControl.AddSendProto<SFCMCanPushAtNightNetCs>((Int32)EProtoNetCs.FCMCanPushAtNight);
        CGlobal.NetControl.AddSendProto<SChangeLanguageNetCs>((Int32)EProtoNetCs.ChangeLanguage);
        CGlobal.NetControl.AddSendProto<SSelectCharNetCs>((Int32)EProtoNetCs.SelectChar);
        CGlobal.NetControl.AddSendProto<SSingleStartNetCs>((Int32)EProtoNetCs.SingleStart);
        CGlobal.NetControl.AddSendProto<SSingleEndNetCs>((Int32)EProtoNetCs.SingleEnd);
        CGlobal.NetControl.AddSendProto<SBattleJoinNetCs>((Int32)EProtoNetCs.BattleJoin);
        CGlobal.NetControl.AddSendProto<SBattleOutNetCs>((Int32)EProtoNetCs.BattleOut);
        CGlobal.NetControl.AddSendProto<SBattleTouchNetCs>((Int32)EProtoNetCs.BattleTouch);
        CGlobal.NetControl.AddSendProto<SBattlePushNetCs>((Int32)EProtoNetCs.BattlePush);
        CGlobal.NetControl.AddSendProto<SBattleIconNetCs>((Int32)EProtoNetCs.BattleIcon);
        CGlobal.NetControl.AddSendProto<SSingleBattleIconNetCs>((Int32)EProtoNetCs.SingleBattleIcon);
        CGlobal.NetControl.AddSendProto<SSingleBattleScoreNetCs>((Int32)EProtoNetCs.SingleBattleScore);
        CGlobal.NetControl.AddSendProto<SSingleBattleItemNetCs>((Int32)EProtoNetCs.SingleBattleItem);

        CGlobal.NetControl.AddSendProto<SRoomListNetCs>((Int32)EProtoNetCs.RoomList);
        CGlobal.NetControl.AddSendProto<SRoomCreateNetCs>((Int32)EProtoNetCs.RoomCreate);
        CGlobal.NetControl.AddSendProto<SRoomJoinNetCs>((Int32)EProtoNetCs.RoomJoin);
        CGlobal.NetControl.AddSendProto<SRoomOutNetCs>((Int32)EProtoNetCs.RoomOut);
        CGlobal.NetControl.AddSendProto<SRoomReadyNetCs>((Int32)EProtoNetCs.RoomReady);
        CGlobal.NetControl.AddSendProto<SRoomChatNetCs>((Int32)EProtoNetCs.RoomChat);
        CGlobal.NetControl.AddSendProto<SRoomNotiNetCs>((Int32)EProtoNetCs.RoomNoti);

        CGlobal.NetControl.AddSendProto<SGachaNetCs>((Int32)EProtoNetCs.Gacha);
        CGlobal.NetControl.AddSendProto<SGachaX10NetCs>((Int32)EProtoNetCs.GachaX10);
        CGlobal.NetControl.AddSendProto<SRankRewardNetCs>((Int32)EProtoNetCs.RankReward);
        CGlobal.NetControl.AddSendProto<SQuestRewardNetCs>((Int32)EProtoNetCs.QuestReward);
        CGlobal.NetControl.AddSendProto<SQuestNextNetCs>((Int32)EProtoNetCs.QuestNext);
        CGlobal.NetControl.AddSendProto<SQuestDailyCompleteRewardNetCs>((Int32)EProtoNetCs.QuestDailyCompleteReward);
        CGlobal.NetControl.AddSendProto<SChangeNickNetCs>((Int32)EProtoNetCs.ChangeNick);
        CGlobal.NetControl.AddSendProto<SBuyNetCs>((Int32)EProtoNetCs.Buy);
        CGlobal.NetControl.AddSendProto<SPurchaseNetCs>((Int32)EProtoNetCs.Purchase);
        CGlobal.NetControl.AddSendProto<SBuyCharNetCs>((Int32)EProtoNetCs.BuyChar);
        CGlobal.NetControl.AddSendProto<SCouponUseNetCs>((Int32)EProtoNetCs.CouponUse);
        CGlobal.NetControl.AddSendProto<SBuyPackageNetCs>((Int32)EProtoNetCs.BuyPackage);
        CGlobal.NetControl.AddSendProto<SDailyRewardNetCs>((Int32)EProtoNetCs.DailyReward);
        CGlobal.NetControl.AddSendProto<SIslandStartNetCs>((Int32)EProtoNetCs.IslandStart);
        CGlobal.NetControl.AddSendProto<SIslandEndNetCs>((Int32)EProtoNetCs.IslandEnd);
        CGlobal.NetControl.AddSendProto<STutorialRewardNetCs>((Int32)EProtoNetCs.TutorialReward);
        CGlobal.NetControl.AddSendProto<SRankingRewardInfoNetCs>((Int32)EProtoNetCs.RankingRewardInfo);
        CGlobal.NetControl.AddSendProto<SRankingRewardNetCs>((Int32)EProtoNetCs.RankingReward);

        CGlobal.NetControl.AddRecvProto<SRetNetSc>(EProtoNetSc.Ret, RetNetSc);
        CGlobal.NetControl.AddRecvProto<SMsgNetSc>(EProtoNetSc.Msg, MsgNetSc);
        CGlobal.NetControl.AddRecvProto<SLoginNetSc>(EProtoNetSc.Login, LoginNetSc);
        CGlobal.NetControl.AddRecvProto<SLobbyNetSc>(EProtoNetSc.Lobby, LobbyNetSc);
        CGlobal.NetControl.AddRecvProto<SChatNetSc>(EProtoNetSc.Chat, ChatNetSc);
        CGlobal.NetControl.AddRecvProto<SUserExpNetSc>(EProtoNetSc.UserExp, UserExpNetSc);
        CGlobal.NetControl.AddRecvProto<SResourcesNetSc>(EProtoNetSc.Resources, UserResourcesNetSc);
        CGlobal.NetControl.AddRecvProto<SSingleStartNetSc>(EProtoNetSc.SingleStart, SingleStartNetSc);
        CGlobal.NetControl.AddRecvProto<SSingleEndNetSc>(EProtoNetSc.SingleEnd, SingleEndNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleJoinNetSc>(EProtoNetSc.BattleJoin, BattleJoinNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleOutNetSc>(EProtoNetSc.BattleOut, BattleOutNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleBeginNetSc>(EProtoNetSc.BattleBegin, BattleBeginNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleMatchingNetSc>(EProtoNetSc.BattleMatching, BattleMatchingNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleStartNetSc>(EProtoNetSc.BattleStart, BattleStartNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleEndNetSc>(EProtoNetSc.BattleEnd, BattleEndNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleSyncNetSc>(EProtoNetSc.BattleSync, BattleSyncNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleTouchNetSc>(EProtoNetSc.BattleTouch, BattleTouchNetSc);
        CGlobal.NetControl.AddRecvProto<SBattlePushNetSc>(EProtoNetSc.BattlePush, BattlePushNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleIconNetSc>(EProtoNetSc.BattleIcon, BattleIconNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleLinkNetSc>(EProtoNetSc.BattleLink, BattleLinkNetSc);
        CGlobal.NetControl.AddRecvProto<SBattleUnLinkNetSc>(EProtoNetSc.BattleUnLink, BattleUnLinkNetSc);
        CGlobal.NetControl.AddRecvProto<SSingleBattleStartNetSc>(EProtoNetSc.SingleBattleStart, SingleBattleStartNetSc);
        CGlobal.NetControl.AddRecvProto<SSingleBattleScoreNetSc>(EProtoNetSc.SingleBattleScore, SingleBattleScoreNetSc);
        CGlobal.NetControl.AddRecvProto<SSingleBattleIconNetSc>(EProtoNetSc.SingleBattleIcon, SingleBattleIconNetSc);
        CGlobal.NetControl.AddRecvProto<SSingleBattleItemNetSc>(EProtoNetSc.SingleBattleItem, SingleBattleItemNetSc);
        CGlobal.NetControl.AddRecvProto<SSingleBattleEndNetSc>(EProtoNetSc.SingleBattleEnd, SingleBattleEndNetSc);

        CGlobal.NetControl.AddRecvProto<SRoomListNetSc>(EProtoNetSc.RoomList, RoomListNetSc);
        CGlobal.NetControl.AddRecvProto<SRoomChangeNetSc>(EProtoNetSc.RoomChange, RoomChangeNetSc);
        CGlobal.NetControl.AddRecvProto<SRoomCreateNetSc>(EProtoNetSc.RoomCreate, RoomCreateNetSc);
        CGlobal.NetControl.AddRecvProto<SRoomJoinNetSc>(EProtoNetSc.RoomJoin, RoomJoinNetSc);
        CGlobal.NetControl.AddRecvProto<SRoomJoinFailedNetSc>(EProtoNetSc.RoomJoinFailed, RoomJoinFailedNetSc);
        CGlobal.NetControl.AddRecvProto<SRoomOutNetSc>(EProtoNetSc.RoomOut, RoomOutNetSc);
        CGlobal.NetControl.AddRecvProto<SRoomOutFailedNetSc>(EProtoNetSc.RoomOutFailed, RoomOutFailedNetSc);
        CGlobal.NetControl.AddRecvProto<SRoomReadyNetSc>(EProtoNetSc.RoomReady, RoomReadyNetSc);
        CGlobal.NetControl.AddRecvProto<SRoomChatNetSc>(EProtoNetSc.RoomChat, RoomChatNetSc);
        CGlobal.NetControl.AddRecvProto<SRoomNotiNetSc>(EProtoNetSc.RoomNoti, RoomNotiNetSc);

        CGlobal.NetControl.AddRecvProto<SGachaNetSc>(EProtoNetSc.Gacha, GachaNetSc);
        CGlobal.NetControl.AddRecvProto<SGachaX10NetSc>(EProtoNetSc.GachaX10, GachaX10NetSc);
        CGlobal.NetControl.AddRecvProto<SGachaFailedNetSc>(EProtoNetSc.GachaFailed, GachaFailedNetSc);
        CGlobal.NetControl.AddRecvProto<SQuestGotNetSc>(EProtoNetSc.QuestGot, QuestGotNetSc);
        CGlobal.NetControl.AddRecvProto<SQuestDoneNetSc>(EProtoNetSc.QuestDone, QuestDoneNetSc);
        CGlobal.NetControl.AddRecvProto<SQuestRewardNetSc>(EProtoNetSc.QuestReward, QuestRewardNetSc);
        CGlobal.NetControl.AddRecvProto<SQuestNextNetSc>(EProtoNetSc.QuestNext, QuestNextNetSc);
        CGlobal.NetControl.AddRecvProto<SQuestDailyCompleteRewardNetSc>(EProtoNetSc.QuestDailyCompleteReward, QuestDailyCompleteRewardNetSc);
        CGlobal.NetControl.AddRecvProto<SChangeNickNetSc>(EProtoNetSc.ChangeNick, ChangeNickNetSc);
        CGlobal.NetControl.AddRecvProto<SChangeNickFailNetSc>(EProtoNetSc.ChangeNickFail, ChangeNickFailNetSc);
        CGlobal.NetControl.AddRecvProto<SSetPointNetSc>(EProtoNetSc.SetPoint, SetRankPoint);
        CGlobal.NetControl.AddRecvProto<SSetCharNetSc>(EProtoNetSc.SetChar, SetCharacter);
        CGlobal.NetControl.AddRecvProto<SUnsetCharNetSc>(EProtoNetSc.UnsetChar, DelCharacter);
        CGlobal.NetControl.AddRecvProto<SBuyNetSc>(EProtoNetSc.Buy, BuyNetSc);
        CGlobal.NetControl.AddRecvProto<SPurchaseNetSc>(EProtoNetSc.Purchase, PurchaseNetSc);
        CGlobal.NetControl.AddRecvProto<SBuyCharNetSc>(EProtoNetSc.BuyChar, BuyCharNetSc);
        CGlobal.NetControl.AddRecvProto<SCouponUseNetSc>(EProtoNetSc.CouponUse, CouponUseNetSc);
        CGlobal.NetControl.AddRecvProto<SCouponUseFailNetSc>(EProtoNetSc.CouponUseFail, CoupontFailNetSc);
        CGlobal.NetControl.AddRecvProto<SBuyPackageNetSc>(EProtoNetSc.BuyPackage, BuyPackageNetSc);
        CGlobal.NetControl.AddRecvProto<SDailyRewardNetSc>(EProtoNetSc.DailyReward, DailyRewardNetSc);
        CGlobal.NetControl.AddRecvProto<SDailyRewardFailNetSc>(EProtoNetSc.DailyRewardFail, DailyRewardFailNetSc);
        CGlobal.NetControl.AddRecvProto<SIslandStartNetSc>(EProtoNetSc.IslandStart, IslandStartNetSc);
        CGlobal.NetControl.AddRecvProto<SIslandEndNetSc>(EProtoNetSc.IslandEnd, IslandEndNetSc);
        CGlobal.NetControl.AddRecvProto<SRankingRewardInfoNetSc>(EProtoNetSc.RankingRewardInfo, RankingRewardInfoNetSc);
        CGlobal.NetControl.AddRecvProto<SRankingRewardNetSc>(EProtoNetSc.RankingReward, RankingRewardNetSc);
        CGlobal.NetControl.AddRecvProto<SRankingRewardFailNetSc>(EProtoNetSc.RankingRewardFail, RankingRewardFailNetSc);

        if (_viewTutorial == true)
            CGlobal.SetIsTutorial(false);

        CGlobal.IsGoogleAdTest = _IsGoogleAdTest;
        CGlobal.RedDotControl.SetReddotOn(RedDotControl.EReddotType.Shop);

        _CreatePopup.gameObject.SetActive(false);
        _SystemPopup.gameObject.SetActive(false);
        _RewardPopup.gameObject.SetActive(false);
        CGlobal.SceneSetNext(new CSceneIntro());
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
