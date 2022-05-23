using bb;
using rso.core;
using rso.game;
using rso.gameutil;
using rso.net;
using rso.unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TUID = System.Int64;
using TQuestPair = System.Collections.Generic.KeyValuePair<System.Byte, bb.SQuestBase>;

public enum EPlayList
{
    Normal,
    Battle,
    Island,
    Dodge,
    Max
}
public enum ESound
{
    Bounce,
    Combo,
    Countdown,
    Down,
    Flap,
    GameEnd,
    GameStart,
    Lose,
    Parachute,
    Pop,
    Pump,
    Salmon,
    StairGames,
    Walk,
    Win,
    DoubleKill,
    RankUp,
    Cancel,
    Ok,
    Error,
    Popup,
    GetGold,
    GetChar,
    Gacha_bounce,
    Gacha_openani,
    Gacha_shoot,
    Gacha_Start,
    single_Coin,
    single_Dia,
    getitem_2,
    getitem_3,
    browken_A2,
    item_Pickup,
    Max
}

public class ExceptionRet : Exception
{
    ERet _Ret;
    public ExceptionRet()
    {
    }
    public ExceptionRet(ERet Ret_) :
        base(string.Format("ExceptionRet[{0}]", Ret_.ToString()))
    {
        _Ret = Ret_;
    }
    public ExceptionRet(ERet Ret_, Exception innerException) :
        base(string.Format("ExceptionRet[{0}]", Ret_.ToString()), innerException)
    {
        _Ret = Ret_;
    }
    public ERet GetRet()
    {
        return _Ret;
    }
}

public static class CGlobal
{
    public struct _SServer
    {
        public CNamePort Game;
        public CNamePort Ranking;

        public _SServer(CNamePort Game_, CNamePort Ranking_)
        {
            Game = Game_;
            Ranking = Ranking_;
        }
    }
    public static CNamePort GameIPPort
    {
        get
        {
            return Servers[(Int32)Server].Game;
        }
    }
    public static CNamePort RankingIPPort
    {
        get
        {
            return Servers[(Int32)Server].Ranking;
        }
    }
    public static readonly string c_PropName = "Prop";

    public static readonly string c_LayerDefault = "Default";
    public static readonly string c_LayerUI = "UI";
    public static readonly string c_TagGround = "Ground";
    public static readonly string c_TagPlayer = "Player";
    public static readonly string c_TagBalloon = "Balloon";
    public static readonly string c_TagParachute = "Parachute";
    public static readonly string c_TagTrap = "Trap";
    public static readonly string c_TagCoin = "Coin";
    public static readonly string c_TagDia = "Dia";
    public static readonly string c_TagItem = "Item";
    public static readonly string c_TagWater = "Water";
    public static readonly string c_TagShield = "Shield";
    public static readonly string c_TagGoldBar = "GoldBar";
    public static readonly string c_TagPoint = "Point";
    public static readonly string c_TagInk = "Ink";
    public static readonly string c_TagScale = "Scale";
    public static readonly string c_TagSlow = "Slow";

    //재화 아이콘 Textrue
    public static readonly string c_TextureTicket = "GUI/Contents/img_icon_coin";
    public static readonly string c_TextureGold = "GUI/Contents/img_icon_coin";
    public static readonly string c_TextureDia = "GUI/Contents/img_icon_gem";
    public static readonly string c_TextureCP = "GUI/Contents/img_icon_keyWing";

    public static readonly Color NameColorRed = new Color(239.0f / 255.0f, 22.0f / 255.0f, 129.0f / 255.0f);
    public static readonly Color NameColorBlue = new Color(40.0f / 255.0f, 122.0f / 255.0f, 241.0f / 255.0f);
    public static readonly Color NameColorGreen = new Color(43.0f / 255.0f, 43.0f / 255.0f, 43.0f / 255.0f);

    public static readonly Color Grey = new Color(0.2f, 0.2f, 0.2f);
    public static readonly string[] PanelBGTextrues = {
        "GUI/Common/img_bg_9charcterSlotBgGreen",
        "GUI/Common/img_bg_9charcterSlotBgBlue",
        "GUI/Common/img_bg_9charcterSlotBgPurple",
        "GUI/Common/img_bg_9charcterSlotBgYellow" };
    public static readonly string[] PanelKeyBGTextrues = {
        "GUI/Common/img_bg_chKeyBgGreen",
        "GUI/Common/img_bg_chKeyBgBlue",
        "GUI/Common/img_bg_chKeyBgPurple",
        "GUI/Common/img_bg_chKeyBgYellow" };
    public static readonly string[] CharInfoBGTextrues = { "Gacha/TM_GachaBG01_2", "Gacha/TM_GachaBG01", "Gacha/TM_GachaBG01_1", "Gacha/TM_GachaBG01_1" };
    public static readonly Int32 MaxStatusStarsCount = 10;

    public static readonly float OrthographicSize = 0.96975f;

    public static float c_Balloon2Width = 0.17f;
    public static float c_Balloon2Height = 0.12f;
    public static float c_Balloon2Width_2 = c_Balloon2Width * 0.5f;
    public static float c_Balloon1Width = 0.088f;
    public static float c_Balloon1Height = 0.121f;
    public static float c_Balloon1Width_2 = c_Balloon1Width * 0.5f;
    public static float c_BalloonGap = 0.037f;
    public static float c_PlayerWidth = 0.118f;
    public static float c_PlayerHeight = 0.169f;
    public static float c_PlayerWidth_2 = c_PlayerWidth * 0.5f;

    //캐릭터의 둘레길이 (반지금을 3으로 기준.)
    public static readonly float CircleRound = 6.0f * Mathf.PI;

    public static COptionJson<SGameOption> GameOption;
    public static CMetaData MetaData { get; private set; } = new CMetaData();
    static CAudioMusic _Music = null;
    public static CAudioSound Sound { get; private set; } = null;
    private static CVibrationControl Vibe = null;

    public static CNetworkControl NetControl = null;
    static CKey _RankingKey = null;
    public static SRanking Ranking = null;

    public static rso.balance.CClient NetRanking = null;
    static CSceneControl _SceneControl = null;
    public static TUID UID = 0;
    public static string NickName = "";
    public static SLoginNetSc LoginNetSc { get; private set; } = null;
    static TimeSpan TimeOffset;
    public static CreatePopup CreatePopup { get; private set; } = null;
    public static PopupSystem SystemPopup { get; private set; } = null;
    public static RewardPopup RewardPopup { get; private set; } = null;
    public static ToolTipPopup ToolTipPopup { get; private set; } = null;
    public static ProgressLoading ProgressLoading { get; private set; } = null;
    public static ScrollConfirmPopup ScrollConfirmPopup { get; private set; } = null;
    public static UpdateInfoPopup UpdateInfoPopup { get; private set; } = null;
    public static TimePoint RankingTimePoint = new TimePoint();

    public static RedDotControl RedDotControl = new RedDotControl();

    //인앱 결제 클래스.
    public static IAPManager IAPManager = null;

    //광고 모듈 클래스.
    public static ADManager ADManager = null;

    public static bool IsDebugMode = false;
    public static bool ServerAnchor = false;

    public static bool IsMatchingCancel = false;

    public static SRoomInfo MyRoomInfo = null;
    public static Dictionary<Int32, SRoomInfo> RoomDictionary = null;

    public static Int32 IntroTitle = 0;

    public enum RankingType
    {
        Multi,
        Single,
        Island,
        Max,
        Null,
    };

    //검수 서버 여부.
    public static bool IsTestServer = false;

    //게임 모드 선택 변수.
    public static EPlayMode PlayMode = EPlayMode.Null;

    //디버그 패널 임시
    public static LogPanel LogPanel { get; private set; } = null;
    public static bool ViewLogPanel = false;
    public static bool IsGoogleAdTest = false;
    public static void SetDebug(LogPanel logPanel_)
    {
        LogPanel = logPanel_;
    }
    public enum EServer
    {
        Test,
        Internal,
        Dev,
        AWSTest,
    }

    public static EServer Server = EServer.Internal;
    public static _SServer[] Servers = null;

    //디버그 패널 임시
    public static bool WillClose = false;
    public static bool IsGuest = false;

    public static string PrefsKey_UpdateInfo = "UpdateInfo";
    public static bool IsUpdateInfoPopup = false;

    public static void Init(CAudioMusic Music_, CAudioSound Sound_, EServer Server_, bool ServerAnchor_, rso.game.CClient Net_, CreatePopup CreatePopup_, PopupSystem Popup_, RewardPopup RewardPopup_, ToolTipPopup ToolTipPopup_, ProgressLoading ProgressLoading_, ScrollConfirmPopup ScrollConfirmPopup_, UpdateInfoPopup UpdateInfoPopup_, IAPManager IAPManager_, ADManager ADManager_)
    {
#if UNITY_EDITOR
        var DataPath = Application.dataPath + "/../";
#else
        var DataPath = Application.persistentDataPath + "/";
#endif
        string FilePath = DataPath + "GameOption.ini";
        try
        {
            GameOption = new COptionJson<SGameOption>(FilePath, false);
        }
        catch
        {
            GameOption = new COptionJson<SGameOption>(FilePath, new SGameOption(true, true, true, true, true, false, GetSystemLanguage(Application.systemLanguage), EPlayMode.Null));
        }
        _Music = Music_;
        Sound = Sound_;

        SetIsVibe(GameOption.Data.IsVibe);
        SetIsMusic(GameOption.Data.IsMusic);
        SetIsSound(GameOption.Data.IsSound);
        SetIsPush(GameOption.Data.IsPush);
        SetIsPad(GameOption.Data.IsPad);
        SetIsTutorial(GameOption.Data.IsTutorial);
        SetLanguage(GameOption.Data.Language);
        SetPlayMode(GameOption.Data.SelectMode);

        Vibe = new CVibrationControl(
            new CVibrationControl.SVibration[] {
                new CVibrationControl.SVibration(
                    new long[]{ 0, 20 }, -1
                    )
            });

        Servers = new _SServer[Enum.GetValues(typeof(EServer)).Length];
        Servers[(Int32)EServer.Test] = new _SServer(new CNamePort("192.168.0.211", 40130), new CNamePort("192.168.0.211", 45020));
        Servers[(Int32)EServer.Internal] = new _SServer(new CNamePort("115.90.183.62", 30130), new CNamePort("115.90.183.62", 35020));
        Servers[(Int32)EServer.Dev] = new _SServer(new CNamePort("127.0.0.1", 30130), new CNamePort("127.0.0.1", 35020));
        Servers[(Int32)EServer.AWSTest] = new _SServer(new CNamePort("ec2-3-39-170-246.ap-northeast-2.compute.amazonaws.com", 30130), new CNamePort("ec2-3-39-170-246.ap-northeast-2.compute.amazonaws.com", 35020));

        foreach (var i in Servers)
            Debug.Assert(i.Game != null, "Input All Servers' NamePort");

        Server = Server_;
        ServerAnchor = ServerAnchor_;

        NetControl = new CNetworkControl(Net_);
        _SceneControl = new CSceneControl();
        CreatePopup = CreatePopup_;
        SystemPopup = Popup_;
        RewardPopup = RewardPopup_;
        ToolTipPopup = ToolTipPopup_;
        ProgressLoading = ProgressLoading_;
        ScrollConfirmPopup = ScrollConfirmPopup_;
        UpdateInfoPopup = UpdateInfoPopup_;
        IAPManager = IAPManager_;
        ADManager = ADManager_;

        IsTestServer = false;
#if UNITY_EDITOR
        IsDebugMode = true;
#endif
    }
    public static TScene GetScene<TScene>() where TScene : CScene
    {
        return _SceneControl.CurScene as TScene;
    }
    public static void SceneSetNext(CScene Scene_)
    {
        _SceneControl.SetNext(Scene_);
    }
    public static void SceneSetNextForce(CScene Scene_)
    {
        _SceneControl.SetNextForce(Scene_);
    }
    public static void Dispose()
    {
        Logout();
        NetControl.Dispose();

        if (_SceneControl != null)
            _SceneControl = null;

        if (MetaData != null)
            MetaData = null;
    }
    public static void Update()
    {
        NetControl.Update();

        while (!_SceneControl.HaveNext())
        {
            if (!NetControl.Call())
                break;
        }

        if (!_SceneControl.Update()) // return false : _SceneControl 에 등록된 Scene이 없으며, 이는 앱이 종료절차가 완료 되었다는 것을 의미.
            rso.unity.CBase.ApplicationQuit();

        _Music.Update();
    }
    public static void Create(string NickName_)
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        CStream Stream = new CStream();
        Stream.Push(new SUserCreateOption(new SUserLoginOption(rso.unity.CBase.GetOS()), ELanguage.English));
#if UNITY_EDITOR
        var DataPath = Application.dataPath + "/../";
#else
        var DataPath = Application.persistentDataPath + "/";
#endif
        CGlobal.NetControl.Create(CGlobal.GameIPPort, Guid.NewGuid().ToString(), NickName_, 0, Stream, DataPath + CGlobal.GameIPPort.Name + "_" + CGlobal.GameIPPort.Port.ToString() + "_" + "Data/");

    }
    public static void Link(TUID UID_, string Nick_)
    {
        UID = UID_;
        NickName = Nick_;
        WillClose = false;
        IAPManager.InitializePurchasing();
    }
    public static void Login(SLoginNetSc Proto_)
    {
        LoginNetSc = Proto_;
        if (LoginNetSc.RoomIdx != -1)
        {
            MyRoomInfo = new SRoomInfo();
            MyRoomInfo.RoomIdx = LoginNetSc.RoomIdx;
        }
        TimeOffset = Proto_.ServerTime - TimePoint.Now;
    }
    public static void Logout()
    {
        _Music.Stop();
        LoginNetSc = null;
        UID = 0;
    }
    public static void RankingLogin(CKey Key_)
    {
        _RankingKey = Key_;
    }
    public static void RankingLogout()
    {
        _RankingKey = null;
    }
    public static bool RankingLogon()
    {
        return (_RankingKey != null);
    }
    public static void MusicPlayNormal()
    {
        _Music.SetPlayList((Int32)EPlayList.Normal);
        _Music.Play();
    }
    public static void MusicPlayBattle()
    {
        _Music.SetPlayList((Int32)EPlayList.Battle);
        _Music.Play();
    }
    public static void MusicPlayIsland()
    {
        _Music.SetPlayList((Int32)EPlayList.Island);
        _Music.Play();
    }
    public static void MusicPlayDodge()
    {
        _Music.SetPlayList((Int32)EPlayList.Dodge);
        _Music.Play();
    }
    public static void MusicStop()
    {
        _Music.Stop();
    }
    public static TimePoint GetServerTimePoint()
    {
        return TimePoint.Now + TimeOffset;
    }
    public static void SetIsMusic(bool value)
    {
        _Music.mute = !value;
        GameOption.Data.IsMusic = value;
        GameOption.Save();
    }
    public static void SetIsSound(bool value)
    {
        Sound.mute = !value;
        GameOption.Data.IsSound = value;
        GameOption.Save();
    }
    public static void SetIsVibe(bool value)
    {
        GameOption.Data.IsVibe = value;
        GameOption.Save();
    }
    public static void SetIsPush(bool value)
    {
        GameOption.Data.IsPush = value;
        GameOption.Save();
    }
    public static void SetIsPad(bool value)
    {
        GameOption.Data.IsPad = value;
        GameOption.Save();
    }
    public static void VibeOn(Int32 VibrationIndex_)
    {
        //if (GameOption.Data.IsVibe)
        //    Vibe.On(VibrationIndex_);
    }
    public static void SetLanguage(ELanguage Language_)
    {
        MetaData.ChangeLanguage(Language_);
        GameOption.Data.Language = Language_;
        GameOption.Save();
    }
    public static void SetPlayMode(EPlayMode value)
    {
        PlayMode = value;
        GameOption.Data.SelectMode = value;
        GameOption.Save();
    }
    public static void SetIsTutorial(bool value)
    {
        GameOption.Data.IsTutorial = value;
        GameOption.Save();
    }
    public static string GetPlayModeString(EPlayMode value)
    {
        switch(value)
        {
            case EPlayMode.Solo:
                return MetaData.GetText(EText.LobbyScene_Solo);
            case EPlayMode.Team:
                return MetaData.GetText(EText.LobbyScene_Team);
            case EPlayMode.Survival:
                return MetaData.GetText(EText.LobbyScene_Survival);
            case EPlayMode.SurvivalSmall:
                return MetaData.GetText(EText.LobbyScene_3PSurvival);
            case EPlayMode.TeamSmall:
                return MetaData.GetText(EText.LobbyScene_TeamSmall);
            case EPlayMode.IslandSolo:
                return MetaData.GetText(EText.LobbyScene_FlyAwayRace);
            case EPlayMode.DodgeSolo:
                return MetaData.GetText(EText.LobbyScene_DodgeArrowRace);
            case EPlayMode.Island:
                return MetaData.GetText(EText.LobbyScene_FlyAway);
            case EPlayMode.Dodge:
                return MetaData.GetText(EText.LobbyScene_Dodge);
            default:
                return MetaData.GetText(EText.LobbyScene_SelectMode);
        }
    }
    public static string GetPlayModeADImage(EGameMode value)
    {
        switch(value)
        {
            case EGameMode.Solo:            return "img_chatSlot1on1";
            case EGameMode.TeamSmall:       return "img_chatSlot2on2";
            case EGameMode.Team:            return "img_chatSlot3on3";
            case EGameMode.SurvivalSmall:   return "img_chatSlot3Survival";
            case EGameMode.Survival:        return "img_chatSlot6Survival";
            case EGameMode.DodgeSolo:       return "img_chatSlotArrow";
            case EGameMode.IslandSolo:      return "img_chatSlotLand";
            default:
                return "";
        }
    }
    public static string GetPlayModeTitle(EGameMode value)
    {
        switch (value)
        {
            case EGameMode.Solo:
                return MetaData.GetText(EText.MultiScene_Solo);
            case EGameMode.Team:
                return MetaData.GetText(EText.LobbyScene_Team);
            case EGameMode.Survival:
                return MetaData.GetText(EText.LobbyScene_Survival);
            case EGameMode.SurvivalSmall:
                return MetaData.GetText(EText.LobbyScene_3PSurvival);
            case EGameMode.TeamSmall:
                return MetaData.GetText(EText.LobbyScene_TeamSmall);
            case EGameMode.IslandSolo:
                return MetaData.GetText(EText.LobbyScene_FlyAwayRace);
            case EGameMode.DodgeSolo:
                return MetaData.GetText(EText.LobbyScene_DodgeArrowRace);
            default:
                return "";
        }
    }
    public static string GetPlayModeType(EGameMode value)
    {
        switch (value)
        {
            case EGameMode.Solo:
            case EGameMode.IslandSolo:
            case EGameMode.DodgeSolo:
                return MetaData.GetText(EText.LobbyScene_Solo);
            case EGameMode.Survival:
            case EGameMode.SurvivalSmall:
                return MetaData.GetText(EText.MultiScene_Individual);
            case EGameMode.Team:
            case EGameMode.TeamSmall:
                return MetaData.GetText(EText.MultiScene_Team);
            default:
                return "";
        }
    }
    public static string GetResourcesIconFile(EResource Resource_)
    {
        switch(Resource_)
        {
            case EResource.Ticket: return c_TextureTicket;
            case EResource.Gold: return c_TextureGold;
            case EResource.Dia: return c_TextureDia;
            case EResource.CP: return c_TextureCP;
            case EResource.DiaPaid: return c_TextureDia;
            default:
                return "";
        }
    }
    public static string GetResourcesLimitedIconFile(EResource Resource_)
    {
        switch (Resource_)
        {
            case EResource.Ticket: return c_TextureTicket;
            case EResource.Gold: return c_TextureGold;
            case EResource.Dia: return c_TextureDia;
            case EResource.CP: return c_TextureCP;
            case EResource.DiaPaid: return c_TextureDia;
            default:
                return "";
        }
    }
    public static string GetResourcesIconFile(ERewardType RewardType_)
    {
        switch (RewardType_)
        {
            case ERewardType.Resource_Ticket: return c_TextureTicket;
            case ERewardType.Resource_Gold: return c_TextureGold;
            case ERewardType.Resource_Dia: return c_TextureDia;
            case ERewardType.Resource_CP: return c_TextureCP;
            default:
                return "";
        }
    }
    public static string GetResourcesText(EResource Resource_)
    {
        switch(Resource_)
        {
            case EResource.Ticket: return MetaData.GetText(EText.Global_Text_Ticket);
            case EResource.Gold: return MetaData.GetText(EText.Global_Text_Gold);
            case EResource.Dia: return MetaData.GetText(EText.Global_Text_Dia);
            case EResource.CP: return MetaData.GetText(EText.Global_Text_CP);
            case EResource.DiaPaid: return MetaData.GetText(EText.Global_Text_Dia);
            default:
                return "";
        }
    }
    public static ERewardType GetResourcesToRewardType(EResource Resource_)
    {
        switch (Resource_)
        {
            case EResource.Ticket: return ERewardType.Resource_Ticket;
            case EResource.Gold: return ERewardType.Resource_Gold;
            case EResource.Dia: 
            case EResource.DiaPaid: return ERewardType.Resource_Dia;
            case EResource.CP: return ERewardType.Resource_CP;
            default:
                return ERewardType.Max;
        }
    }
    public static bool HaveCost(EResource Resource_, Int32 Value_)
    {
        return LoginNetSc.User.Resources.HaveCost(Resource_, Value_);
    }
    public static void ShowResourceNotEnough(EResource Resource_)
    {
        if (Resource_ == EResource.CP)
            SystemPopup.ShowPopup(MetaData.GetText(EText.Global_Popup_CpNotEnough), PopupSystem.PopupType.Confirm, null, true);
        else
            SystemPopup.ShowPopup(string.Format(MetaData.GetText(EText.Global_Popup_ResourceNotEnough), GetResourcesText(Resource_)),PopupSystem.PopupType.Confirm, null, true);
    }
    public static void ShowHaveForbiddenWord(string ForbiddenWord_)
    {
        SystemPopup.ShowPopup(string.Format(MetaData.GetText(EText.Have_ForbiddenWord), ForbiddenWord_), PopupSystem.PopupType.Confirm, null, true);
    }
    public static Tuple<EResource,Int32> ToOneResource(this Int32[] Resources_)
    {
        for (Int32 i = 0; i < (Int32)EResource.Max; ++i)
        {
            if(Resources_[i] > 0)
                return new Tuple<EResource, Int32>((EResource)i, Resources_[i]);
        }

        return new Tuple<EResource, Int32>(EResource.Gold, 0);
    }
    public static void AddUserBattleEnd(Int32 AddPoint_)
    {
        LoginNetSc.User.Point += AddPoint_;
        if (LoginNetSc.User.Point < 0)
            LoginNetSc.User.Point = 0;
        if (LoginNetSc.User.Point > LoginNetSc.User.PointBest)
            LoginNetSc.User.PointBest = LoginNetSc.User.Point;
    }
    public static void SubResources(Int32[] Resources_)
    {
        LoginNetSc.User.Resources.SubResources(Resources_);
    }
    public static Int32[] GetReward(List<SRewardMeta> RewardMetas_)
    {
        Int32[] RewardResource = new Int32[(Int32)EResource.Max];
        for (var i = 0; i < RewardMetas_.Count; ++i)
        {
            switch(RewardMetas_[i].Type)
            {
                case ERewardType.Character:
                    if (!CGlobal.LoginNetSc.Chars.Contains(RewardMetas_[i].Data))
                    {
                        CGlobal.LoginNetSc.Chars.Add(RewardMetas_[i].Data);
                        CGlobal.RedDotControl.SetReddotChar(RewardMetas_[i].Data);
                    }
                    else
                    {
                        RewardResource[(Int32)EResource.CP] = CGlobal.MetaData.Chars[RewardMetas_[i].Data].CPRefund;
                    }
                    break;
                case ERewardType.Resource_Ticket:
                    RewardResource[(Int32)EResource.Ticket] = RewardMetas_[i].Data;
                    break;
                case ERewardType.Resource_Gold:
                    RewardResource[(Int32)EResource.Gold] = RewardMetas_[i].Data;
                    break;
                case ERewardType.Resource_Dia:
                    RewardResource[(Int32)EResource.Dia] = RewardMetas_[i].Data;
                    break;
                case ERewardType.Resource_CP:
                    RewardResource[(Int32)EResource.CP] = RewardMetas_[i].Data;
                    break;
                default:
                    Debug.Log("GetReward Reward Type Error !!!");
                    break;
            }
        }
        return RewardResource;
    }
    public static string HaveForbiddenWord(string String_)
    {
        foreach (var i in MetaData.ForbiddenWords)
        {
            if (String_.IndexOf(i) >= 0)
                return i;
        }

        return "";
    }
    private static ELanguage GetSystemLanguage(SystemLanguage Language_)
    {
        ELanguage language = ELanguage.English;

        switch(Language_)
        {
            case SystemLanguage.Korean:             language = ELanguage.Korean; break;
            case SystemLanguage.Japanese:           language = ELanguage.Japan; break;
            case SystemLanguage.ChineseSimplified:  language = ELanguage.ChinaCH; break;
            case SystemLanguage.ChineseTraditional: language = ELanguage.ChinaTW; break;
            case SystemLanguage.French:             language = ELanguage.France; break;
            case SystemLanguage.German:             language = ELanguage.Germany; break;
            case SystemLanguage.Spanish:            language = ELanguage.Spain; break;
            case SystemLanguage.Italian:            language = ELanguage.Italia; break;
            case SystemLanguage.Portuguese:         language = ELanguage.Portugal; break;
            case SystemLanguage.Russian:            language = ELanguage.Russia; break;
            case SystemLanguage.Dutch:              language = ELanguage.Nederland; break;
            case SystemLanguage.Turkish:            language = ELanguage.Turkey; break;
            case SystemLanguage.Finnish:            language = ELanguage.Finland; break;
            case SystemLanguage.Thai:               language = ELanguage.Thailand; break;
            case SystemLanguage.Indonesian:         language = ELanguage.Indonesia; break;
            case SystemLanguage.Vietnamese:         language = ELanguage.Vietnam; break;

            case SystemLanguage.English:
            default:                                language = ELanguage.English; break;
        }

        return language;
    }
    public static TQuestPair GetUserQuestInfo(Byte SlotIndex_)
    {
        return LoginNetSc.Quests.GetPair(SlotIndex_);
    }
    public static string VersionText()
    {
        ulong checkSum = MetaData.Checksum % 10000;
        return string.Format("{0}.{1}",Application.version,checkSum);
    }
    public static string UpdateInfoKey()
    {
        return string.Format("{0}_{1}", PrefsKey_UpdateInfo, VersionText());
    }
    public static Tuple<Int32, TimeSpan> GetSinglePlayCountLeftTime()
    {
        var NewSingleCount = LoginNetSc.User.SinglePlayCount;
        var NewSingleRefreshTime = LoginNetSc.User.SingleRefreshTime;
        var Now = GetServerTimePoint();

        if (NewSingleCount < MetaData.SingleBalanceMeta.PlayCountMax)
        {
            Int64 UnitMinutes = TimeSpan.FromMinutes(MetaData.SingleBalanceMeta.RefreshDurationMinute).Ticks / 600000000;
            Int64 ElapsedMinutes = ((Now - NewSingleRefreshTime).Ticks / 600000000);
            Int32 ElapsedCount = (Int32)(ElapsedMinutes / UnitMinutes);
            NewSingleCount += ElapsedCount;

            if (NewSingleCount > MetaData.SingleBalanceMeta.PlayCountMax)
                NewSingleCount = MetaData.SingleBalanceMeta.PlayCountMax;

            if (NewSingleCount >= MetaData.SingleBalanceMeta.PlayCountMax)
                NewSingleRefreshTime = Now;
            else
                NewSingleRefreshTime += TimeSpan.FromMinutes(UnitMinutes * ElapsedCount);
        }
        else if (NewSingleCount >= MetaData.SingleBalanceMeta.PlayCountMax)
        {
            NewSingleRefreshTime = Now;
        }

        return new Tuple<Int32, TimeSpan>(NewSingleCount, NewSingleRefreshTime + TimeSpan.FromMinutes(MetaData.SingleBalanceMeta.RefreshDurationMinute) - Now);
    }
    public static Tuple<Int32, TimeSpan> GetSingleIslandPlayCountLeftTime()
    {
        var NewSingleCount = LoginNetSc.User.IslandPlayCount;
        var NewSingleRefreshTime = LoginNetSc.User.IslandRefreshTime;
        var Now = GetServerTimePoint();

        if (NewSingleCount < MetaData.SingleIslandBalanceMeta.PlayCountMax)
        {
            Int64 UnitMinutes = TimeSpan.FromMinutes(MetaData.SingleIslandBalanceMeta.RefreshDurationMinute).Ticks / 600000000;
            Int64 ElapsedMinutes = ((Now - NewSingleRefreshTime).Ticks / 600000000);
            Int32 ElapsedCount = (Int32)(ElapsedMinutes / UnitMinutes);
            NewSingleCount += ElapsedCount;

            if (NewSingleCount > MetaData.SingleIslandBalanceMeta.PlayCountMax)
                NewSingleCount = MetaData.SingleIslandBalanceMeta.PlayCountMax;

            if (NewSingleCount >= MetaData.SingleIslandBalanceMeta.PlayCountMax)
                NewSingleRefreshTime = Now;
            else
                NewSingleRefreshTime += TimeSpan.FromMinutes(UnitMinutes * ElapsedCount);
        }
        else if (NewSingleCount >= MetaData.SingleIslandBalanceMeta.PlayCountMax)
        {
            NewSingleRefreshTime = Now;
        }

        return new Tuple<Int32, TimeSpan>(NewSingleCount, NewSingleRefreshTime + TimeSpan.FromMinutes(MetaData.SingleBalanceMeta.RefreshDurationMinute) - Now);
    }
    public static string GetRankingRewardImagePath(Int32 Ranking_)
    {
        switch(Ranking_)
        {
            case 1:
                return "RankingTrophy_1st";
            case 2:
                return "RankingTrophy_2nd";
            case 3:
                return "RankingTrophy_3rd";
        }
        return "";
    }
    public static string GetCharStatusIcon(EStatusType Status_)
    {
        switch(Status_)
        {
            case EStatusType.Sky:
                return "img_icon_statusAir";
            case EStatusType.Stamina:
                return "img_icon_statusEnergy";
            case EStatusType.Pump:
                return "img_icon_statusBlowing";
            case EStatusType.Land:
                return "img_icon_statusLand";
        }
        return "";
    }
    public static void PushStatusLanguageOn()
    {
        switch (CGlobal.GameOption.Data.Language)
        {
            case ELanguage.English:
            case ELanguage.France:
            case ELanguage.Germany:
            case ELanguage.Spain:
            case ELanguage.Italia:
            case ELanguage.Japan:
            case ELanguage.Portugal:
            case ELanguage.Nederland:
            case ELanguage.Turkey:
            case ELanguage.Finland:
            case ELanguage.Malaysia:
            case ELanguage.Thailand:
            case ELanguage.Indonesia:
            case ELanguage.Vietnam:
            case ELanguage.India:
                Firebase.Messaging.FirebaseMessaging.SubscribeAsync("Notification_English");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_Korean");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_ChinaCH");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_ChinaTW");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_Russia");
                break;
            case ELanguage.Korean:
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_English");
                Firebase.Messaging.FirebaseMessaging.SubscribeAsync("Notification_Korean");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_ChinaCH");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_ChinaTW");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_Russia");
                break;
            case ELanguage.ChinaCH:
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_English");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_Korean");
                Firebase.Messaging.FirebaseMessaging.SubscribeAsync("Notification_ChinaCH");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_ChinaTW");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_Russia");
                break;
            case ELanguage.ChinaTW:
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_English");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_Korean");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_ChinaCH");
                Firebase.Messaging.FirebaseMessaging.SubscribeAsync("Notification_ChinaTW");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_Russia");
                break;
            case ELanguage.Russia:
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_English");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_Korean");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_ChinaCH");
                Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_ChinaTW");
                Firebase.Messaging.FirebaseMessaging.SubscribeAsync("Notification_Russia");
                break;
        }
    }
    public static void PushStatusLanguageOff()
    {
        Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_English");
        Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_Korean");
        Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_ChinaCH");
        Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_ChinaTW");
        Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification_Russia");
    }

    public static void SendRequestRanking()
    {
        NetRanking.Send(0, (Int32)EProtoRankingNetCs.RequestRanking);
    }
}
