using bb;
using rso.core;
using rso.net;
using rso.unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MoneyUIScene
{
    [SerializeField] RankingRewardPopup _rankingRewardPopupPrefab;

    InputTouch _Input = new InputTouch();

    Vector3 _StartPos;
    Vector3 _EndPos;
    bool _IsSizeSet = false;
    float _MoveSpeed = 120.0f;
    bool _IsEvent = false;

    private float BeforeAngle = 0.0f;

    GameObject _InvalidDisconnectNoticePopup = null;
    public BtnRedDot BtnCharacterSelect = null;
    public BtnRedDot BtnRanking = null;
    public BtnRedDot BtnShop = null;
    public BtnRedDot BtnQuest = null;
    public BtnRedDot BtnRank = null;
    public UIUserCharacter UserCharacter = null;
    public UIUserInfo UserInfo = null;
    public Button BtnRankingRewardEffect = null;
    public Image EventBG = null;
    public Text EventText = null;

    [SerializeField] internal SingleBattleJoinButton _FlyAwayBattleJoinButton;
    [SerializeField] internal SingleBattleJoinButton _ArrowDodgeBattleJoinButton;
    [SerializeField] internal MultiBattleJoinButton _MultiBattleJoinButton;

    public new async void init()
    {
        base.init();

        _FlyAwayBattleJoinButton.Init(FlyAwayBattleJoin, CGlobal.MetaData.getText(EText.LobbyScene_FlyAway));
        _ArrowDodgeBattleJoinButton.Init(ArrowDodgeBattleJoin, CGlobal.MetaData.getText(EText.LobbyScene_Dodge));
        _MultiBattleJoinButton.Init(BattleJoin);

        _Input.add(new InputTouch.Scroller((Vector2) => { return true; }, _ScrollCallback));

        CGlobal.MusicStop();
        CGlobal.MusicPlayNormal();

        foreach (var i in CGlobal.LoginNetSc.Quests)
        {
            if (CGlobal.MetaData.questDatas[i.Value.Code].completeCount <= i.Value.Count && i.Value.CoolEndTime.Ticks <= 0)
            {
                CGlobal.RedDotControl.SetReddotOn(RedDotControl.EReddotType.Quest);
                break;
            }
        }

        //래드 닷 관련 작업.
        var LobbyBtn = BtnCharacterSelect;
        LobbyBtn.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Character));
        LobbyBtn = BtnRanking;
        LobbyBtn.SetRedDot(false);
        LobbyBtn = BtnShop;
        LobbyBtn.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Shop));
        LobbyBtn = BtnQuest;
        LobbyBtn.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Quest));
        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Shop);

        EventBG.gameObject.SetActive(false);

        UserInfo.InitUserInfo();

        MakeCharacter();

        for (Int32 i = 0; i < CGlobal.MetaData.RankRewards.Count; ++i)
        {
            if (CGlobal.MetaData.RankRewards[i].Meta.point <= CGlobal.LoginNetSc.User.PointBest && i == (CGlobal.LoginNetSc.User.NextRewardRankIndex))
            {
                CGlobal.RedDotControl.SetReddotOn(RedDotControl.EReddotType.Rank);
                break;
            }
        }
        LobbyBtn = BtnRank;
        LobbyBtn.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Rank));

        // delete me show update popup
        //if (GlobalVariable.haveSeenUpdateInfoPopup == false)
        //{
        //    var IsUpdatePopup = PlayerPrefs.GetInt(CGlobal.UpdateInfoKey(), 0);
        //    if (IsUpdatePopup == 0)
        //    {
        //        GlobalVariable.haveSeenUpdateInfoPopup = true;
        //        await pushUpdateInfoPopup(true);
        //    }
        //}

        if (CGlobal.CheckQuestRedDot)
        {
            QuestRedDotCheck();
            CGlobal.CheckQuestRedDot = false;
        }

        CGlobal.NetControl.Send(new SRankingRewardInfoNetCs());
        CGlobal.ProgressCircle.Activate();

        Int32 isEnteredTutorialScene = PlayerPrefs.HasKey(nameof(isEnteredTutorialScene)) ? PlayerPrefs.GetInt(nameof(isEnteredTutorialScene)) : 0;
        if (!CGlobal.LoginNetSc.User.TutorialReward && isEnteredTutorialScene == 0)
        {
            PlayerPrefs.SetInt(nameof(isEnteredTutorialScene), 1);
            await pushNoticePopup(false, EText.Tutorial_Text_Start);
            CGlobal.pushBattleTutorialScene();
        }
    }
    protected override void Update()
    {
        base.Update();

        if (CGlobal.LoginNetSc != null)
        {
            SetDodgeChargeTimeCount();
            SetIslandChargeTimeCount();
        }

        if (CGlobal.NetRanking != null)
            CGlobal.NetRanking.Proc();

        _Input.update();

        if (_IsEvent)
        {
            if (_IsSizeSet)
            {
                float Widht = (EventText.rectTransform.sizeDelta.x) * -1;
                _StartPos = EventText.transform.localPosition;
                _EndPos = new Vector3(Widht, 0.0f, 0.0f);
                _IsSizeSet = false;
            }
            EventText.transform.localPosition = Vector3.MoveTowards(EventText.transform.localPosition, _EndPos, _MoveSpeed * Time.deltaTime);
            if (EventText.transform.localPosition == _EndPos)
            {
                EventText.transform.localPosition = _StartPos;
            }
        }
    }
    protected override void OnDestroy()
    {
        if (_InvalidDisconnectNoticePopup != null)
            GameObject.Destroy(_InvalidDisconnectNoticePopup);

        if (CGlobal.NetRanking != null)
        {
            CGlobal.NetRanking.Dispose();
            CGlobal.NetRanking = null;
        }

        base.OnDestroy();
    }
    protected override async Task<bool> _backButtonPressed()
    {
        if (await base._backButtonPressed())
            return true;

        if (_InvalidDisconnectNoticePopup != null && _InvalidDisconnectNoticePopup.activeSelf)
        {
            _InvalidDisconnectNoticePopup.SetActive(false);
            return true;
        }

        return false;
    }
    public void FlyAwayBattleJoin()
    {
        Main.FlyAwayBattleJoin();
    }
    public void ArrowDodgeBattleJoin()
    {
        Main.ArrowDodgeBattleJoin();
    }
    public async void BattleJoin()
    {
        if (!CGlobal.LoginNetSc.User.InvalidDisconnectInfo.CanMatchable(CGlobal.GetServerTimePoint()))
        {
            if (_InvalidDisconnectNoticePopup == null)
                _InvalidDisconnectNoticePopup = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefabs/UI/popup/InvalidDisconnectNoticePopup"), new Vector3(0, 0, 0), Quaternion.identity);
            else
                _InvalidDisconnectNoticePopup.SetActive(true);
        }
        else if (!CGlobal.doesHaveCost(CGlobal.MetaData.ConfigMeta.BattleCostType, CGlobal.MetaData.ConfigMeta.BattleCostValue))
        {
            await CGlobal.curScene.pushNoticePopup(true, EText.Global_Popup_MultiPlayCountNotEnough);
        }
        else
        {
            CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
            CGlobal.NetControl.Send(new SMultiBattleJoinNetCs(new SBattleType(2, 1)));
        }
    }
    void SetDodgeChargeTimeCount()
    {
        var SingleData = CGlobal.GetSinglePlayCountLeftTime();
        _ArrowDodgeBattleJoinButton.SetCountLeftAndTimeLeft(SingleData.Item1, CGlobal.MetaData.arrowDodgeConfigMeta.PlayCountMax, SingleData.Item2);
    }
    void SetIslandChargeTimeCount()
    {
        var SingleData = CGlobal.GetSingleIslandPlayCountLeftTime();
        _FlyAwayBattleJoinButton.SetCountLeftAndTimeLeft(SingleData.Item1, CGlobal.MetaData.arrowDodgeConfigMeta.PlayCountMax, SingleData.Item2);
    }
    public void CharacterSelectView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.pushCharacterListScene();
    }
    public async void MyInfoView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        await pushAccountInfoPopup();
    }
    public async void RankingView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        if (CGlobal.NetRanking == null)
            CGlobal.NetRanking = new rso.balance.CClient(RankingLink, RankingLinkFail, RankingUnLink, RankingRecv);

        if (CGlobal.RankingLogon())
        {
            if (TimePoint.Now > CGlobal.PreventRequestRankingEndTimePoint)
            {
                CGlobal.PreventRequestRankingEndTimePoint = TimePoint.FromDateTime(TimePoint.Now.ToDateTime().AddSeconds(10));
                CGlobal.SendRequestRanking();
                CGlobal.ProgressCircle.Activate();
            }
            else
            {
                await pushRankingPopup();
            }
        }
        else if (!CGlobal.NetRanking.IsConnected(0)) // 연결완료된것도 아니고, 연결중도 아니면
        {
            CGlobal.NetRanking.Connect(
                0,
                CGlobal.Device.GetDataPath() + CGlobal.RankingServerNamePort.Name + "_" + CGlobal.RankingServerNamePort.Port.ToString() + "_" + "Data/",
                CGlobal.RankingServerNamePort);

            CGlobal.ProgressCircle.Deactivate();
        }
    }
    public void ShopView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.pushShopScene();
    }
    public async void QuestView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Quest);
        await pushQuestPopup();
        BtnQuest.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Quest));
    }
    public void ClipBoardUIDCopy()
    {
        //EditorGUIUtility.systemCopyBuffer = UserID.text;
    }
    public void RankRewardView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.pushRankingRewardScene();
    }
    public void RankingRewardView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.ProgressCircle.Activate();
        CGlobal.NetControl.Send(new SRankingRewardNetCs());
        AnalyticsManager.TrackingEvent(ETrackingKey.weekly_ranking_reward);
    }
    void RankingLink(CKey Key_)
    {
        CGlobal.ProgressCircle.Deactivate();
        CGlobal.RankingLogin(Key_);
        CGlobal.SendRequestRanking();
    }
    async void RankingLinkFail(System.UInt32 PeerNum_, ENetRet NetRet_)
    {
        CGlobal.ProgressCircle.Deactivate();

        await pushNoticePopup(true, EText.RankingServer_Error_Connection, NetRet_);
    }
    void RankingUnLink(CKey Key_, ENetRet NetRet_)
    {
        CGlobal.RankingLogout();
        CGlobal.ProgressCircle.Deactivate();
    }
    async void RankingRecv(CKey Key_, CStream Stream_)
    {
        Int32 ProtoNum = 0;
        Stream_.Pop(ref ProtoNum);

        switch ((EProtoRankingNetSc)ProtoNum)
        {
            case EProtoRankingNetSc.RequestRanking:
                {
                    CGlobal.ProgressCircle.Deactivate();

                    CGlobal.Ranking = new SRanking();
                    CGlobal.Ranking.Push(Stream_);

                    await pushRankingPopup();
                }
                break;
        }
    }
    void _ScrollCallback(InputTouch.TouchState State_, Vector2 DownPos_, Vector2 Diff_, Vector2 Delta_)
    {
        if (State_ == InputTouch.TouchState.move)
        {
            var NowPos = DownPos_ + Diff_;
            var WorldPointNow = camera.ScreenToWorldPoint(NowPos);
            var WorldPointDown = camera.ScreenToWorldPoint(DownPos_);
            var WorldDistance = WorldPointNow.x - WorldPointDown.x;

            var Angle = (WorldDistance / CGlobal.CircleRound * 360.0f);
            UserCharacter.transform.localEulerAngles = new Vector3(0.0f, UserCharacter.transform.localEulerAngles.y - (Angle - BeforeAngle), 0.0f);
            BeforeAngle = Angle;
        }
        else if (State_ == InputTouch.TouchState.up)
        {
            BeforeAngle = 0.0f;
        }
    }
    public override void UpdateResources()
    {
        CGlobal.MoneyUI.UpdateResources();
    }
    public void MakeCharacter()
    {
        UserCharacter.DeleteCharacter();
        UserCharacter.MakeCharacter(CGlobal.LoginNetSc.User.SelectedCharCode);
        UserInfo.CharacterChange();
    }
    public void QuestRedDotCheck()
    {
        foreach (var i in CGlobal.LoginNetSc.Quests)
        {
            if (CGlobal.MetaData.questDatas[i.Value.Code].completeCount <= i.Value.Count && i.Value.CoolEndTime.Ticks <= 0)
            {
                CGlobal.RedDotControl.SetReddotOn(RedDotControl.EReddotType.Quest);
                break;
            }
        }

        BtnQuest.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Quest));
    }
    public void SetRankingReward(SRankingRewardInfoNetSc proto)
    {
        var isInRanking = false;
        foreach (var i in proto.RankingArray)
        {
            if (i != -1)
            {
                isInRanking = true;
                break;
            }
        }

        if (!isInRanking)
            return;

        if (CGlobal.LoginNetSc.User.RankingRewardedCounter >= proto.Counter)
            return;

        BtnRankingRewardEffect.gameObject.SetActive(true);
    }
    public async void GetRankingReward(Int32[] myRankingArray)
    {
        var popup = UnityEngine.GameObject.Instantiate(_rankingRewardPopupPrefab);
        popup.init(myRankingArray);
        await _dialogController.push(popup, camera);

        BtnRankingRewardEffect.gameObject.SetActive(false);

        UpdateResources();
    }
    public void SetNickname(string nickname)
    {
        _dialogController.pop();

        var accountInfoPopup = _dialogController.peek() as AccountInfoPopup;
        if (accountInfoPopup == null)
            return;

        accountInfoPopup.SetNickname(nickname);
        UserInfo.InitUserInfo();
    }
    public void gotQuests(Dictionary<Byte, SQuestBase> questsGot)
    {
        var questPopup = _dialogController.peek() as QuestPopup;
        if (questPopup == null)
            return;

        questPopup.gotQuests(questsGot);
    }
    public void updateQuest(Byte slotIndex, SQuestBase newQuest)
    {
        var questPopup = _dialogController.peek() as QuestPopup;
        if (questPopup == null)
            return;

        questPopup.updateQuest(slotIndex, newQuest);
    }
    public void doneQuest(Byte slotIndex)
    {
        var questPopup = _dialogController.peek() as QuestPopup;
        if (questPopup == null)
            return;

        questPopup.updateCount(slotIndex);
    }
    public void rewardQuest(SQuestBase questBase, SQuestRewardNetSc proto)
    {
        var questPopup = _dialogController.peek() as QuestPopup;
        if (questPopup == null)
            return;

        questPopup.rewardQuest(questBase, proto);
    }
}
