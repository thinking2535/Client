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
using static rso.game.CClient;
using System.Linq;
using rso.Base;
using TDoneQuests = System.Collections.Generic.List<bb.SQuestSlotIndexCount>;
using System.IO;
using System.Threading.Tasks;
using TResource = System.Int32;

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
    single_Coin,
    single_Dia,
    getitem_2,
    getitem_3,
    browken_A2,
    item_Pickup,
    Max
}

// rso todo GlobalVariable, GlobalFunction 및 또다른 클래스로 분리할것.
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
public class SResourceInfo
{
    public Sprite sprite;
    public EText TextName;
    Sprite[] _bigSprites;
    double _multiplierForBigSprite;
    Int32 _maxValueForBigSprite;
    public SResourceInfo(string spritePath, EText TextName_, string[] bigSpritePaths, Int32 maxValueForBigSprite)
    {
        sprite = Resources.Load<Sprite>(spritePath);
        TextName = TextName_;

        _bigSprites = new Sprite[bigSpritePaths.Length];
        for (Int32 i = 0; i < _bigSprites.Length; ++i)
            _bigSprites[i] = Resources.Load<Sprite>(bigSpritePaths[i]);

        _maxValueForBigSprite = maxValueForBigSprite;
        _multiplierForBigSprite = _maxValueForBigSprite / (Math.Pow(_bigSprites.Length, 2.0));
    }
    public Sprite getBigSprite(Int32 value)
    {
        if (_bigSprites.Length == 0)
            return sprite;
        else if (value <= 0)
            return _bigSprites.First();
        else if (value >= _maxValueForBigSprite)
            return _bigSprites.Last();

        var index = (Int32)Math.Pow(value / _multiplierForBigSprite, 0.5);
        if (index >= _bigSprites.Length)
            index = _bigSprites.Length - 1;

        return _bigSprites[index];
    }
    public string GetText()
    {
        return CGlobal.MetaData.getText(TextName);
    }
}
public class SRankingTypeInfo
{
    public EText TextName;
    public string GetText()
    {
        return CGlobal.MetaData.getText(TextName);
    }
    public SRankingTypeInfo(EText TextName_)
    {
        TextName = TextName_;
    }
}
public class GradeInfo
{
    public Sprite sprite { get; private set; }
    public Sprite characterBackgroundSprite { get; private set; }
    public Sprite shopCharacterBackgroundSprite { get; private set; }
    public Color color { get; private set; }
    public EText textName { get; private set; }
    public Int32 subGradeCount = 0;

    public GradeInfo(string spritePath, string characterBackgroundSpritePath, string shopCharacterBackgroundSpritePath, Color color, EText textName)
    {
        this.sprite = Resources.Load<Sprite>(spritePath);
        this.characterBackgroundSprite = Resources.Load<Sprite>(characterBackgroundSpritePath);
        this.shopCharacterBackgroundSprite = Resources.Load<Sprite>(shopCharacterBackgroundSpritePath);
        this.color = color;
        this.textName = textName;
    }
}
public static class CGlobal
{
    public struct SServerURLPort
    {
        public string ServerUrl;
        public UInt16 GameServerPort;
        public UInt16 RankingServerPort;

        public SServerURLPort(string ServerUrl_, UInt16 GameServerPort_, UInt16 RankingServerPort_)
        {
            ServerUrl = ServerUrl_;
            GameServerPort = GameServerPort_;
            RankingServerPort = RankingServerPort_;
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

    public static readonly string sortingLayerMoneyUI = "MoneyUI";
    public static readonly string sortingLayerPopup = "Popup";

    public static readonly Color NameColorRed = new Color(239.0f / 255.0f, 22.0f / 255.0f, 129.0f / 255.0f);
    public static readonly Color NameColorBlue = new Color(40.0f / 255.0f, 122.0f / 255.0f, 241.0f / 255.0f);
    public static readonly Color NameColorGreen = new Color(43.0f / 255.0f, 43.0f / 255.0f, 43.0f / 255.0f);

    public static readonly Color Grey = new Color(0.2f, 0.2f, 0.2f);
    public static readonly string[] PanelKeyBGTextrues = {
        "GUI/Common/img_bg_chKeyBgGreen",
        "GUI/Common/img_bg_chKeyBgBlue",
        "GUI/Common/img_bg_chKeyBgPurple",
        "GUI/Common/img_bg_chKeyBgYellow" };

    //캐릭터의 둘레길이
    public static readonly float CircleRound = Mathf.PI;
    public static COptionJson<SGameOption> GameOption;
    public static CMetaData MetaData { get; private set; }
    static CAudioMusic _Music = null;
    public static CAudioSound Sound { get; private set; } = null;
    private static CVibrationControl Vibe = null;

    public static CNetworkControl NetControl = null;
    static CKey _RankingKey = null;
    public static SRanking Ranking = null;

    public static rso.balance.CClient NetRanking = null;
    public static TUID UID = 0;
    public static string NickName = "";
    public static SLoginNetSc LoginNetSc { get; private set; } = null;
    public static Int32 Point { get { return LoginNetSc.User.Point; } }
    static TimeSpan TimeOffset;
    public static ConsoleWindow ConsoleWindow;
    public static ProgressCircle ProgressCircle { get; private set; } = null;
    public static MoneyUI MoneyUI { get; private set; } = null;
    public static SceneController sceneController;
    public static BaseScene curScene => sceneController.curScene as BaseScene;
    public static TimePoint PreventRequestRankingEndTimePoint = new TimePoint();

    public static RedDotControl RedDotControl;
    public static bool CheckQuestRedDot = false;

    public static bool IsDebugMode = false;
    public static bool ServerAnchor = false;

    public static bool IsWarnedNeedToLinkAccount = false;

    public enum EServer
    {
        Test,
        Internal,
        Dev,
        AWSTest,
    }

    static SServerURLPort[] _ServerURLPorts;
    public static SServerURLPort SelectedServerURLPort { get; private set; }
    public static CNamePort GameServerNamePort;
    public static CNamePort RankingServerNamePort;

    public static bool WillClose = false;
    public static bool IsGuest = false;

    public static string PrefsKey_UpdateInfo = "UpdateInfo";

    static SResourceInfo[] _ResourceInfos = new SResourceInfo[(Int32)EResource.Max];
    public static SResourceInfo GetResourceInfo(EResource Resource_)
    {
        return _ResourceInfos[(Int32)Resource_];
    }
    public static Sprite GetResourceSprite(EResource Resource_)
    {
        return GetResourceInfo(Resource_).sprite;
    }
    public static Sprite GetResourceBigSprite(EResource Resource_, Int32 value)
    {
        return GetResourceInfo(Resource_).getBigSprite(value);
    }
    public static string GetResourceText(EResource Resource_)
    {
        return GetResourceInfo(Resource_).GetText();
    }
    public static Sprite GetFlagSprite(String CountryCode_)
    {
        if (CountryCode_.Length > 0)
            return Resources.Load<Sprite>("Flag/" + CountryCode_);
        else
            return Resources.Load<Sprite>("Flag/unknown");
    }
    public static SResourceInfo nftInfo;
    public static SRankingTypeInfo[] RankingTypeInfos { get; private set; } = new SRankingTypeInfo[(Int32)ERankingType.Max];
    public static SRankingTypeInfo GetRarnkingTypeInfo(ERankingType RankingType_)
    {
        return RankingTypeInfos[(Int32)RankingType_];
    }
    static GradeInfo[] _gradeInfos = new GradeInfo[(Int32)EGrade.Max];
    public static GradeInfo getGradeInfo(EGrade grade)
    {
        return _gradeInfos[(Int32)grade];
    }
    static void ConsoleEditEnd(string Text_)
    {
        NetControl.Send(new SChatNetCs(Text_));
    }
    public static CDevice Device;

    public static void Init(
        CAudioMusic Music_,
        CAudioSound Sound_,
        EServer Server_,
        bool ServerAnchor_,
        rso.game.CClient.TLinkFunc LinkFunc_,
        rso.game.CClient.TLinkFailFunc LinkFailFunc_,
        rso.game.CClient.TUnLinkFunc UnLinkFunc_,
        TRecvFunc RecvFunc_,
        TErrorFunc ErrorFunc_,
        TCheckFunc CheckFunc_)
    {
#if UNITY_EDITOR
        Device = new CEditor();
#elif UNITY_ANDROID
        Device = new CAndroid();
#elif UNITY_IOS
        Device = new CIOS();
#else
        throw new Exception("Invalid Device");
#endif

        _ResourceInfos[(Int32)EResource.Ticket] = new SResourceInfo(
            "GUI/Contents/img_icon_Ticket01",
            EText.Global_Text_Ticket,
            new string[] { },
            0);
        _ResourceInfos[(Int32)EResource.Gold] = new SResourceInfo(
            "GUI/Contents/img_icon_coin",
            EText.Global_Text_Gold,
            new string[] { "Textures/GoldGoods_1", "Textures/GoldGoods_2", "Textures/GoldGoods_3", "Textures/GoldGoods_4", "Textures/GoldGoods_5", "Textures/GoldGoods_6" },
            10000);
        _ResourceInfos[(Int32)EResource.Dia00] = new SResourceInfo(
            "GUI/Contents/img_icon_gem01",
            EText.Global_Text_Dia00,
            new string[] { },
            0);
        _ResourceInfos[(Int32)EResource.Dia01] = new SResourceInfo(
            "GUI/Contents/img_icon_gem02",
            EText.Global_Text_Dia01,
            new string[] { },
            0);
        _ResourceInfos[(Int32)EResource.Dia02] = new SResourceInfo(
            "GUI/Contents/img_icon_gem03",
            EText.Global_Text_Dia02,
            new string[] { },
            0);
        _ResourceInfos[(Int32)EResource.Dia03] = new SResourceInfo(
            "GUI/Contents/img_icon_gem04",
            EText.Global_Text_Dia03,
            new string[] { },
            0);

        for (Int32 i= 0; i < (Int32)EResource.Max; ++i)
            Debug.Assert(_ResourceInfos[i] != null);

        {
            nftInfo = new SResourceInfo("GUI/Common/img_icon_NFT", EText.NFT, new string[] { }, 0);
        }


        {
            RankingTypeInfos[(Int32)ERankingType.Multi] = new SRankingTypeInfo(EText.RankingScene_Multi);
            RankingTypeInfos[(Int32)ERankingType.Single] = new SRankingTypeInfo(EText.LobbyScene_Dodge);
            RankingTypeInfos[(Int32)ERankingType.Island] = new SRankingTypeInfo(EText.LobbyScene_FlyAway);

            for (Int32 i = 0; i < (Int32)ERankingType.Max; ++i)
                Debug.Assert(RankingTypeInfos[i] != null);
        }

        {
            _gradeInfos[(Int32)EGrade.Normal] = new GradeInfo(
                "Gacha/TM_GachaBG01_2",
                "GUI/Common/img_bg_9charcterSlotBgGreen",
                "GUI/Contents/img_bg_9shopBgGreen",
                new Color(184.0f/255.0f, 212.0f/255.0f, 127.0f/255.0f),
                EText.Global_Grade_Normal);
            _gradeInfos[(Int32)EGrade.Rare] = new GradeInfo(
                "Gacha/TM_GachaBG01",
                "GUI/Common/img_bg_9charcterSlotBgBlue",
                "GUI/Contents/img_bg_9shopBgBlue",
                new Color(94.0f / 255.0f, 170.0f / 255.0f, 255.0f / 255.0f),
                EText.Global_Grade_Rare);
            _gradeInfos[(Int32)EGrade.Epic] = new GradeInfo(
                "Gacha/TM_GachaBG01_1",
                "GUI/Common/img_bg_9charcterSlotBgPurple",
                "GUI/Contents/img_bg_9shopBgPurple",
                new Color(208.0f / 255.0f, 106.0f / 255.0f, 255.0f / 255.0f),
                EText.Global_Grade_Epic);
            _gradeInfos[(Int32)EGrade.Unique] = new GradeInfo(
                "Gacha/TM_GachaBG01_3",
                "GUI/Common/img_bg_9charcterSlotBgYellow",
                "GUI/Contents/img_bg_9shopBgYellow",
                new Color(246.0f / 255.0f, 206.0f / 255.0f, 122.0f / 255.0f),
                EText.Global_Grade_Unique);

        for (Int32 i = 0; i < (Int32)EGrade.Max; ++i)
                Debug.Assert(_gradeInfos[i] != null);
        }

        string FilePath = Device.GetDataPath() + "GameOption.ini";
        try
        {
            GameOption = new COptionJson<SGameOption>(FilePath, false);
        }
        catch
        {
            GameOption = new COptionJson<SGameOption>(FilePath, new SGameOption(true, true, true, true, true, false, GetSystemLanguage(Application.systemLanguage)));
        }
        MetaData = new CMetaData();
        _Music = Music_;
        Sound = Sound_;

        Vibe = new CVibrationControl(
            new CVibrationControl.SVibration[] {
                new CVibrationControl.SVibration(
                    new long[]{ 0, 20 }, -1
                    )
            });

        _ServerURLPorts = new SServerURLPort[Enum.GetValues(typeof(EServer)).Length];
        _ServerURLPorts[(Int32)EServer.Test] = new SServerURLPort("file://" + Directory.GetCurrentDirectory() + "/ServerInfo/Test_ServerInfo.txt", 40130, 45020);
        _ServerURLPorts[(Int32)EServer.Internal] = new SServerURLPort("file://" + Directory.GetCurrentDirectory() + "/ServerInfo/Internal_ServerInfo.txt", 30130, 35020);
        _ServerURLPorts[(Int32)EServer.Dev] = new SServerURLPort("file://" + Directory.GetCurrentDirectory() + "/ServerInfo/Dev_ServerInfo.txt", 30130, 35020);
        //_ServerURLPorts[(Int32)EServer.AWSTest] = new SServerURLPort("https://any-balloon.s3.ap-northeast-2.amazonaws.com/ServerInfo.txt", 30130, 35020);
        _ServerURLPorts[(Int32)EServer.AWSTest] = new SServerURLPort("", 30130, 35020);

        foreach (var i in _ServerURLPorts)
            Debug.Assert(i.ServerUrl != null, "Input All Servers");

        SelectedServerURLPort = _ServerURLPorts[(Int32)Server_];
        ServerAnchor = ServerAnchor_;

        var Client = new rso.game.CClient(new SVersion(bb.global.c_Ver_Main, MetaData.Checksum));
        Client.LinkFunc = LinkFunc_;
        Client.LinkFailFunc = LinkFailFunc_;
        Client.UnLinkFunc = UnLinkFunc_;
        Client.RecvFunc = RecvFunc_;
        Client.ErrorFunc = ErrorFunc_;
        Client.CheckFunc = CheckFunc_;

        NetControl = new CNetworkControl(Client);

        {
            ConsoleWindow = UnityEngine.Object.Instantiate(Resources.Load<ConsoleWindow>("Prefabs/UI/ConsoleWindow"), Vector3.zero, Quaternion.identity);
            ConsoleWindow.Init(ConsoleEditEnd, "aaa", "type \"/help\" to show cheat commands");
        }

        {
            ProgressCircle = UnityEngine.Object.Instantiate(Resources.Load<ProgressCircle>("Prefabs/UI/ProgressCircle"), Vector3.zero, Quaternion.identity);
        }

        {
            MoneyUI = UnityEngine.Object.Instantiate(Resources.Load<MoneyUI>("Prefabs/UI/MoneyUI"), Vector3.zero, Quaternion.identity);
        }

        RedDotControl = new RedDotControl();
        sceneController = new SceneController(_createIntroScene);

#if UNITY_EDITOR
        IsDebugMode = true;
#endif
    }
    public static TScene GetScene<TScene>() where TScene : BaseScene
    {
        return sceneController.curScene as TScene;
    }
    public static TScene UpdateResourcesAndGetScene<TScene>() where TScene : BaseScene
    {
        var BaseScene = sceneController.curScene as BaseScene;
        BaseScene.UpdateResources();

        return BaseScene as TScene;
    }
    public static void UpdateResourcesCurrentScene()
    {
        var BaseScene = sceneController.curScene as BaseScene;
        BaseScene.UpdateResources();
    }
    public static void clearAndPushIntroScene()
    {
        sceneController.clearAndPush(_createIntroScene);
    }
    static Scene _createIntroScene()
    {
        var scene = SceneController.create(GlobalVariable.main.introScenePrefab);
        scene.init();
        return scene;
    }
    public static void pushBattleTutorialScene()
    {
        sceneController.push(_createBattleTutorialScene);
    }
    static Scene _createBattleTutorialScene()
    {
        var scene = SceneController.create(GlobalVariable.main.battleTutorialScenePrefab);
        scene.init();
        return scene;
    }
    public static void setLobbyScene()
    {
        sceneController.set(
            () =>
            {
                var scene = SceneController.create(GlobalVariable.main.lobbyScenePrefab);
                scene.init();
                return scene;
            });
    }
    public static void pushCharacterListScene()
    {
        sceneController.push(_createCharacterListScene);
    }
    public static void setCharacterListScene()
    {
        sceneController.set(_createCharacterListScene);
    }
    static Scene _createCharacterListScene()
    {
        var scene = SceneController.create(GlobalVariable.main.characterListScenePrefab);
        scene.init();
        return scene;
    }
    public static void pushShopScene()
    {
        sceneController.push(
            () =>
            {
                var scene = SceneController.create(GlobalVariable.main.shopScenePrefab);
                scene.init();
                return scene;
            });
    }
    public static void pushRankingRewardScene()
    {
        sceneController.push(
            () =>
            {
                var scene = SceneController.create(GlobalVariable.main.rankingRewardScenePrefab);
                scene.init();
                return scene;
            });
    }
    public static void setMultiMatchingScene()
    {
        sceneController.set(
            () =>
            {
                var scene = SceneController.create(GlobalVariable.main.multiMatchingScenePrefab);
                scene.init();
                return scene;
            });
    }
    public static void Dispose()
    {
        Logout();
        NetControl.Dispose();

        if (sceneController != null)
        {
            sceneController.clear();
            sceneController = null;
        }

        if (MetaData != null)
            MetaData = null;
    }
    public static void Update()
    {
        NetControl.Update();
        _Music.Update();
    }
    public static void Create(string NickName_)
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        CStream Stream = new CStream();
        Stream.Push(new SUserCreateOption(new SUserLoginOption(CUnity.GetOS()), ELanguage.English));

        CGlobal.ProgressCircle.Activate();

        CGlobal.NetControl.Create(
            CGlobal.GameServerNamePort,
            Guid.NewGuid().ToString(),
            NickName_,
            0,
            Stream,
            CGlobal.Device.GetDataPath() + CGlobal.GameServerNamePort.Name + "_" + CGlobal.GameServerNamePort.Port.ToString() + "_" + "Data/");
    }
    public static void Link(TUID UID_, string NickName_)
    {
        UID = UID_;
        NickName = NickName_;
        WillClose = false;
    }
    public static void Login(SLoginNetSc Proto_)
    {
        LoginNetSc = Proto_;
        TimeOffset = Proto_.ServerTime - TimePoint.Now;
        MoneyUI.UpdateResourcesImmediately();
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
        GameOption.Data.Language = Language_;
        GameOption.Save();
    }
    public static void SetIsTutorial(bool value)
    {
        GameOption.Data.IsTutorial = value;
        GameOption.Save();
    }
    public static Int32[] getEmptyResources()
    {
        return new Int32[(Int32)EResource.Max];
    }
    public static Int32[] getFullResources()
    {
        var resources = getEmptyResources();

        for (var i = 0; i < resources.Length; ++i)
            resources[i] = Int32.MaxValue;

        return resources;
    }
    public static TResource getResourceFreeSpace(TResource currentResource, EResource resourceType)
    {
        return getResourceFreeSpace(currentResource, (Int32)resourceType);
    }
    public static TResource getResourceFreeSpace(TResource currentResource, Int32 index)
    {
        return CGlobal.MetaData.MaxResources[index] - currentResource;
    }
    public static bool doesHaveCost(SResourceTypeData cost)
    {
        return LoginNetSc.User.Resources.doesHaveCost(cost);
    }
    public static bool doesHaveCost(EResource costType, Int32 costValue)
    {
        return LoginNetSc.User.Resources.doesHaveCost(costType, costValue);
    }
    public static bool doesHaveCost(Int32[] cost)
    {
        return LoginNetSc.User.Resources.doesHaveCost(cost);
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
    public static void AddResources(Int32[] Resources_)
    {
        LoginNetSc.User.Resources.AddResources(Resources_);
    }
    public static void AddResource(SResourceTypeData resourceTypeData)
    {
        LoginNetSc.User.Resources.AddResource(resourceTypeData);
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

        if (NewSingleCount < MetaData.arrowDodgeConfigMeta.PlayCountMax)
        {
            Int64 UnitMinutes = TimeSpan.FromMinutes(MetaData.arrowDodgeConfigMeta.RefreshDurationMinute).Ticks / 600000000;
            Int64 ElapsedMinutes = ((Now - NewSingleRefreshTime).Ticks / 600000000);
            Int32 ElapsedCount = (Int32)(ElapsedMinutes / UnitMinutes);
            NewSingleCount += ElapsedCount;

            if (NewSingleCount > MetaData.arrowDodgeConfigMeta.PlayCountMax)
                NewSingleCount = MetaData.arrowDodgeConfigMeta.PlayCountMax;

            if (NewSingleCount >= MetaData.arrowDodgeConfigMeta.PlayCountMax)
                NewSingleRefreshTime = Now;
            else
                NewSingleRefreshTime += TimeSpan.FromMinutes(UnitMinutes * ElapsedCount);
        }
        else if (NewSingleCount >= MetaData.arrowDodgeConfigMeta.PlayCountMax)
        {
            NewSingleRefreshTime = Now;
        }

        return new Tuple<Int32, TimeSpan>(NewSingleCount, NewSingleRefreshTime + TimeSpan.FromMinutes(MetaData.arrowDodgeConfigMeta.RefreshDurationMinute) - Now);
    }
    public static Tuple<Int32, TimeSpan> GetSingleIslandPlayCountLeftTime()
    {
        var NewSingleCount = LoginNetSc.User.IslandPlayCount;
        var NewSingleRefreshTime = LoginNetSc.User.IslandRefreshTime;
        var Now = GetServerTimePoint();

        if (NewSingleCount < MetaData.flyAwayConfigMeta.PlayCountMax)
        {
            Int64 UnitMinutes = TimeSpan.FromMinutes(MetaData.flyAwayConfigMeta.RefreshDurationMinute).Ticks / 600000000;
            Int64 ElapsedMinutes = ((Now - NewSingleRefreshTime).Ticks / 600000000);
            Int32 ElapsedCount = (Int32)(ElapsedMinutes / UnitMinutes);
            NewSingleCount += ElapsedCount;

            if (NewSingleCount > MetaData.flyAwayConfigMeta.PlayCountMax)
                NewSingleCount = MetaData.flyAwayConfigMeta.PlayCountMax;

            if (NewSingleCount >= MetaData.flyAwayConfigMeta.PlayCountMax)
                NewSingleRefreshTime = Now;
            else
                NewSingleRefreshTime += TimeSpan.FromMinutes(UnitMinutes * ElapsedCount);
        }
        else if (NewSingleCount >= MetaData.flyAwayConfigMeta.PlayCountMax)
        {
            NewSingleRefreshTime = Now;
        }

        return new Tuple<Int32, TimeSpan>(NewSingleCount, NewSingleRefreshTime + TimeSpan.FromMinutes(MetaData.flyAwayConfigMeta.RefreshDurationMinute) - Now);
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
            default:
                throw new Exception();
        }
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
            default:
                throw new Exception();
        }
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
    public static CPadSimulator getJoypadSimulator(InputTouch.FJoypad fTouched_, InputTouch.FButton fPushed_, float activeRange, Vector2 defaultPosition)
    {
        return new CPadSimulator(fTouched_, fPushed_, activeRange, defaultPosition);
    }
    public static Tuple<string, float> GetPointGaugeStringAndXScale(Int32 RankPoint_, KeyValuePair<Int32, SRankTierClientMeta> CurrentRankKeyValuePair_)
    {
        string String;
        float XScale;
        if (RankPoint_ < CurrentRankKeyValuePair_.Key)
        {
            String = string.Format("{0}/{1}", RankPoint_, CurrentRankKeyValuePair_.Value.MaxPoint);
            float PrevMaxPoint = 0;
            try
            {
                PrevMaxPoint = CGlobal.MetaData.RankTiers.TakeWhile(kvp => kvp.Key < CurrentRankKeyValuePair_.Value.MaxPoint).Last().Value.MaxPoint;
            }
            catch
            {
            }

            XScale = (float)(RankPoint_ - PrevMaxPoint) / (float)(CurrentRankKeyValuePair_.Value.MaxPoint - PrevMaxPoint);
        }
        else
        {
            String = string.Format("{0}", RankPoint_);
            XScale = 1.0f;
        }

        return new Tuple<string, float>(String, XScale);
    }
    public static void UpdateQuest(TDoneQuests DoneQuests_)
    {
        foreach (var i in DoneQuests_)
            CGlobal.LoginNetSc.Quests[i.SlotIndex].Count = i.Count;
    }
    public static CUnitReward CharCodeToUnitReward(Int32 CharCode_)
    {
        return new CUnitRewardCharacter(MetaData.Characters[CharCode_], CGlobal.LoginNetSc.doesHaveCharacter(CharCode_));
    }
    public static List<CUnitReward> CharCodesToUnitRewards(List<Int32> CharCodes_)
    {
        var UnitRewards = new List<CUnitReward>();

        foreach (var i in CharCodes_)
            UnitRewards.Add(CharCodeToUnitReward(i));

        return UnitRewards;
    }
    public static SCharacterMeta GetSelectCharacterMeta()
    {
        return MetaData.Characters[LoginNetSc.User.SelectedCharCode];
    }
    public static void Quit()
    {
        WillClose = true;
        NetControl.Logout();
        CUnity.ApplicationQuit();
    }
}
