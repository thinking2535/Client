using Firebase.Analytics;
using rso.core;
using rso.net;
using bb;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    public MoneyUI MoneyUILayer = null;
    public BtnRedDot BtnCharacterSelect = null;
    public BtnRedDot BtnRanking = null;
    public BtnRedDot BtnShop = null;
    public BtnRedDot BtnQuest = null;
    public BtnRedDot BtnRank = null;
    public UIUserCharacter UserCharacter = null;
    public UIUserInfo UserInfo = null;
    public Camera MainCamera = null;
    public CharacterInfoPopup CharacterInfoPopup = null;
    public Button BtnRankingRewardEffect = null;
    public RankingRewardPopup RankingRewardPopup = null;
    public RankPointInfoPopup RankPointInfoPopup = null;
    public Image EventBG = null;
    public Text EventText = null;
    public Text DodgePlayCountText = null;
    public Text IslandPlayCountText = null;
    public Text DodgePlayTimeText = null;
    public Text IslandPlayTimeText = null;
    public GameObject RoomADParant = null;
    public RoomInfoPopup RoomInfoPopup = null;
    public GameObject RoomADScroll = null;
    public GameObject ADListBig = null;
    public GameObject ADListSmall = null;
    public Vector2 ADListBigSize = Vector2.zero;
    public Vector2 ADListSmallSize = Vector2.zero;

    public PopupMyInfo MyInfo = null;
    public PopupRanking Ranking = null;
    public PopupQuest Quest = null;

    public void ADListSizeChange()
    {
        if(ADListBig.activeSelf)
        {
            ADListBig.SetActive(false);
            ADListSmall.SetActive(true);
            RoomADScroll.GetComponent<RectTransform>().sizeDelta = ADListBigSize;
        }
        else
        {
            ADListBig.SetActive(true);
            ADListSmall.SetActive(false);
            RoomADScroll.GetComponent<RectTransform>().sizeDelta = ADListSmallSize;
        }
    }

    public void BattleJoin(sbyte TeamPlayer_, sbyte Team_, EGameMode Mode_)
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.NetControl.Send(new SBattleJoinNetCs(new SBattleType(Mode_, TeamPlayer_, Team_)));
    }
    public void BattleJoinIslandSolo()
    {
        BattleJoin(1, 2, EGameMode.IslandSolo);
        //AnalyticsManager.AddPlaySurvivalCount();
    }
    public void BattleJoinDodgeSolo()
    {
        BattleJoin(1, 2, EGameMode.DodgeSolo);
        //AnalyticsManager.AddPlaySurvivalCount();
    }
    public void BattleJoinFreeForAll()
    {
        BattleJoin(1, 6, EGameMode.Survival);
        AnalyticsManager.AddPlaySurvivalCount();
    }
    public void BattleJoinVsOne()
    {
        BattleJoin(1, 2, EGameMode.Solo);
        AnalyticsManager.AddPlayOneOnOneCount();
    }
    public void BattleJoinVsThree()
    {
        BattleJoin(3, 2, EGameMode.Team);
        AnalyticsManager.AddPlayTeamCount();
    }
    public void BattleJoinVsTwo()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        BattleJoin(2, 2, EGameMode.TeamSmall);
    }
    public void BattleJoinVsFive()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        //BattleJoin(1, 5);
    }
    public void BattleJoinVsOneSurvival()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        BattleJoin(1, 3, EGameMode.SurvivalSmall);
        AnalyticsManager.AddPlaySurvivalSmallCount();
    }
    public void CharacterSelectView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.SceneSetNext(new CSceneCharacterList());
    }
    public void MyInfoView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        MyInfo.ShowMyInfoPopup();
    }
    public void RankingView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);

        if (CGlobal.NetRanking == null)
        {
            CGlobal.NetRanking = new rso.balance.CClient(RankingLink, RankingLinkFail, RankingUnLink, RankingRecv);
        }
        if (CGlobal.RankingLogon())
        {
            if (TimePoint.Now.Ticks - CGlobal.RankingTimePoint.Ticks > 0)
            {
                var dateTime = TimePoint.Now.ToDateTime().AddSeconds(10); // 10초 이내이면 호출되지 않도록
                CGlobal.RankingTimePoint = TimePoint.FromDateTime(dateTime);

            CGlobal.SendRequestRanking();
            }
            else
            {
                CGlobal.ProgressLoading.InvisibleProgressLoading();
                Ranking.ShowRankingPopup();
            }
        }
        else if (!CGlobal.NetRanking.IsConnected(0)) // 연결완료된것도 아니고, 연결중도 아니면
        {
#if UNITY_EDITOR
            var DataPath = Application.dataPath + "/../";
#else
            var DataPath = Application.persistentDataPath + "/";
#endif
            CGlobal.NetRanking.Connect(0, DataPath + CGlobal.RankingIPPort.Name + "_" + CGlobal.RankingIPPort.Port.ToString() + "_" + "Data/", CGlobal.RankingIPPort);
        }
    }
    public void ShopView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.SceneSetNext(new CSceneShop(null));
    }
    public void QuestView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        Quest.ShowQuestPopup();
        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Quest);
        BtnQuest.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Quest));
    }
    public void ClipBoardUIDCopy()
    {
        //EditorGUIUtility.systemCopyBuffer = UserID.text;
    }
    public void RankPointInfoDodge()
    {
        RankPointInfoPopup.DodgeView();
    }
    public void RankPointInfoIsland()
    {
        RankPointInfoPopup.IslandView();
    }
    public void RankPointInfoOneOnOne()
    {
        RankPointInfoPopup.OneOnOneView();
    }
    public void RankPointInfoTeam()
    {
        RankPointInfoPopup.TeamView();
    }
    public void RankPointInfoTeamSmall()
    {
        RankPointInfoPopup.TeamSmallView();
    }
    public void RankPointInfoFreeForAll()
    {
        RankPointInfoPopup.FreeForAllView();
    }
    public void RankPointInfoFreeForAllSmall()
    {
        RankPointInfoPopup.FreeForAllSmallView();
    }
    public void RoomListScene()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.ProgressLoading.VisibleProgressLoading();
        CGlobal.NetControl.Send<SRoomListNetCs>(new SRoomListNetCs());
    }
    public void RankRewardView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.SceneSetNext(new CSceneRankingReward());
    }
    public void RankingRewardView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);
        CGlobal.NetControl.Send<SRankingRewardNetCs>(new SRankingRewardNetCs());
        AnalyticsManager.TrackingEvent(ETrackingKey.weekly_ranking_reward);
    }
    public void MapSelectBtn()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.SceneSetNext(new CSceneMapSelector());
    }
    public void CharacterInfoBtn()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CharacterInfoPopup.ShowCharacterInfoPopup(CGlobal.LoginNetSc.User.SelectedCharCode, MainCamera);
    }
    private void Update()
    {
        if (CGlobal.NetRanking != null)
            CGlobal.NetRanking.Proc();
    }
    private void OnDestroy()
    {
        if (CGlobal.NetRanking != null)
        {
            CGlobal.NetRanking.Dispose();
            CGlobal.NetRanking = null;
        }
    }
    public void SingleModePlay()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        var SingleData = CGlobal.GetSinglePlayCountLeftTime();

        if (SingleData.Item1 > 0)
        {
            CGlobal.MusicStop();
            CGlobal.NetControl.Send(new SSingleStartNetCs());
            CGlobal.SceneSetNext(new CSceneBattleSingle());
            if (CGlobal.IsMatchingCancel)
            {
                CGlobal.IsMatchingCancel = false;
                AnalyticsManager.TrackingEvent(ETrackingKey.cancel_multiplay_startsingle);
            }
        }
        else
        {
            CGlobal.SystemPopup.ShowCostResourcePopup(EText.GlobalPopup_Text_SingleCharge, EResource.Gold, CGlobal.MetaData.SingleBalanceMeta.ChargeCostGold, (PopupSystem.PopupBtnType type_) =>
            {
                if (type_ == PopupSystem.PopupBtnType.Ok)
                {
                    if (CGlobal.HaveCost(EResource.Gold, CGlobal.MetaData.SingleBalanceMeta.ChargeCostGold))
                    {
                        CGlobal.MusicStop();
                        CGlobal.NetControl.Send(new SSingleStartNetCs());
                        CGlobal.SceneSetNext(new CSceneBattleSingle());
                        AnalyticsManager.TrackingEvent(ETrackingKey.spend_gold_singlecharge);
                        if (CGlobal.IsMatchingCancel)
                        {
                            CGlobal.IsMatchingCancel = false;
                            AnalyticsManager.TrackingEvent(ETrackingKey.cancel_multiplay_startsingle);
                        }
                    }
                    else
                        CGlobal.ShowResourceNotEnough(EResource.Gold);
                }
            });
        }
    }
    public void SingleIslandModePlay()
    {
        var SingleData = CGlobal.GetSingleIslandPlayCountLeftTime();

        if (SingleData.Item1 > 0)
        {
            CGlobal.MusicStop();
            CGlobal.NetControl.Send(new SIslandStartNetCs());
            CGlobal.SceneSetNext(new CSceneBattleSingleIsland());
            if (CGlobal.IsMatchingCancel)
            {
                CGlobal.IsMatchingCancel = false;
                AnalyticsManager.TrackingEvent(ETrackingKey.cancel_multiplay_startsingle);
            }
        }
        else
        {
            CGlobal.SystemPopup.ShowCostResourcePopup(EText.GlobalPopup_Text_SingleCharge, EResource.Gold, CGlobal.MetaData.SingleIslandBalanceMeta.ChargeCostGold, (PopupSystem.PopupBtnType type_) =>
            {
                if (type_ == PopupSystem.PopupBtnType.Ok)
                {
                    if (CGlobal.HaveCost(EResource.Gold, CGlobal.MetaData.SingleIslandBalanceMeta.ChargeCostGold))
                    {
                        CGlobal.MusicStop();
                        CGlobal.NetControl.Send(new SIslandStartNetCs());
                        CGlobal.SceneSetNext(new CSceneBattleSingleIsland());
                        AnalyticsManager.TrackingEvent(ETrackingKey.spend_gold_singlecharge);
                        if (CGlobal.IsMatchingCancel)
                        {
                            CGlobal.IsMatchingCancel = false;
                            AnalyticsManager.TrackingEvent(ETrackingKey.cancel_multiplay_startsingle);
                        }
                    }
                    else
                        CGlobal.ShowResourceNotEnough(EResource.Gold);
                }
            });
        }
    }
    void RankingLink(CKey Key_)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.RankingLogin(Key_);
        CGlobal.SendRequestRanking();
    }
    void RankingLinkFail(System.UInt32 PeerNum_, ENetRet NetRet_)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        var Msg = string.Format(CGlobal.MetaData.GetText(EText.RankingServer_Error_Connection), (Int32)NetRet_);
        CGlobal.SystemPopup.ShowPopup(Msg, PopupSystem.PopupType.Confirm, (PopupSystem.PopupBtnType type_) => { }, true);
    }
    void RankingUnLink(CKey Key_, ENetRet NetRet_)
    {
        CGlobal.RankingLogout();
        if(CGlobal.ProgressLoading != null)
            CGlobal.ProgressLoading.InvisibleProgressLoading();
    }
    void RankingRecv(CKey Key_, CStream Stream_)
    {
        Int32 ProtoNum = 0;
        Stream_.Pop(ref ProtoNum);

        switch ((EProtoRankingNetSc)ProtoNum)
        {
            case EProtoRankingNetSc.RequestRanking:
                {
                    CGlobal.ProgressLoading.InvisibleProgressLoading();

                    CGlobal.Ranking = new SRanking();
                    CGlobal.Ranking.Push(Stream_);
                    Ranking.SetRanking(CGlobal.Ranking);
                    Ranking.ShowRankingPopup();
                }
                break;
        }
    }
}
