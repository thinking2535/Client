using Boo.Lang;
using bb;
using rso.core;
using rso.game;
using rso.net;
using rso.unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TPeerCnt = System.UInt32;
using TUID = System.Int64;
using TFriends = System.Collections.Generic.Dictionary<System.Int64, rso.game.SFriend>;

public partial class Main
{
    void Link(CKey Key_, TUID UID_, string Nick_, TFriends Friends_)
    {
        CGlobal.Link(UID_, Nick_);
    }
    void LinkFail(TPeerCnt PeerNum_, EGameRet GameRet_)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        if(GameRet_ == EGameRet.InvalidVersion)
        {
            _SystemPopup.ShowUpdatePopup((PopupSystem.PopupBtnType type_) => {
#if UNITY_IOS
                Application.OpenURL("https://itunes.apple.com/app/apple-store/id1530777694");
#else
                Application.OpenURL("http://play.google.com/store/apps/details?id=com.stairgames.balloonstars");
#endif
                CGlobal.WillClose = true;
                CGlobal.NetControl.Logout();
                rso.unity.CBase.ApplicationQuit();
            });
        }
        else if (GameRet_ == EGameRet.AlreadyExist)
        {
            if (CGlobal.WillClose == false)
            {
                _SystemPopup.ShowPopup(CGlobal.MetaData.GetText(EText.NickNameAlreadyExists), PopupSystem.PopupType.Confirm, (PopupSystem.PopupBtnType type_) =>
                {
                    CGlobal.CreatePopup.Show(CGlobal.Create);
                }, true);
            }
        }
        else
        {
            if (CGlobal.WillClose == false)
            {
                CGlobal.SceneSetNextForce(new CSceneNetDelay());
                _SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(EText.GlobalPopup_Network_LinkFailed), GameRet_), PopupSystem.PopupType.Confirm, (PopupSystem.PopupBtnType type_) =>
                {
                    CGlobal.SceneSetNextForce(new CSceneIntro());
                }, true);
            }
        }
    }
    void UnLink(CKey Key_, EGameRet GameRet_)
    {
        if(CGlobal.WillClose == false)
        {
            CGlobal.ProgressLoading.InvisibleProgressLoading();
            CGlobal.SceneSetNextForce(new CSceneNetDelay());
            _SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(EText.GlobalPopup_Network_Unlink), GameRet_), PopupSystem.PopupType.Confirm, (PopupSystem.PopupBtnType type_) => {
                CGlobal.SceneSetNextForce(new CSceneIntro());
            }, true);
        }
        CGlobal.Logout();
    }
    void Recv(CKey Key_, CStream Stream_)
    {
        Int32 ProtoNum = 0;
        Stream_.Pop(ref ProtoNum);
        CGlobal.NetControl.Recv(Key_, ProtoNum, Stream_);
    }
    void UnLinkSoft(CKey Key_, EGameRet GameRet_)
    {
    }
    void Error(TPeerCnt PeerNum_, EGameRet GameRet_)
    {
    }
    void Check(TUID UID_, CStream Stream_)
    {
        // 처리는 정상으로 계정이 없거나, 있거나 할 수 있음.

        try
        {
            if (UID_ == 0)
                throw new ExceptionRet(ERet.UserDoesNotExist);

            Int32 Ret = 0;
            Stream_.Pop(ref Ret);

            if ((ERet)Ret != ERet.Ok)
                throw new ExceptionRet((ERet)Ret);

            var Proto = new SCheckIDNetSc();
            Proto.Push(Stream_);

            if (Proto.Datas.Count == 0)
                throw new ExceptionRet(ERet.UserDoesNotExist);

            var Data = Proto.Datas.First();

            // Data 정보를 가진 계정이 있음.
        }
        catch (ExceptionRet /*Exception_*/)
        {
            // 계정이 없음. 오류번호 : Exception_.GetRet()

        }
    }

    //절대 오면 안되는 에러. <만약 올 경우 Assert 후 리포트>
    public void RetNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SRetNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        var Msg = string.Format(CGlobal.MetaData.GetText(EText.GlobalPopup_Network_Error), Proto.Ret);
        _SystemPopup.ShowPopup(Msg, PopupSystem.PopupType.Confirm,
            (PopupSystem.PopupBtnType type_) => { throw new Exception(Msg); }, true);
    }

    //Text 데이터에 없고 문제를 바로 알려주는 Message String 타입.
    void MsgNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SMsgNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        string Msg = Proto.Msg;
        EText Text = CGlobal.MetaData.CheckAlarm(Msg);
        if (Text != EText.Null)
        {
            var SplitMsg = Msg.Split(' ');
            _SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(Text), SplitMsg[1]), PopupSystem.PopupType.Confirm, null, true);
        }
        else
            _SystemPopup.ShowPopup(Proto.Msg, PopupSystem.PopupType.Confirm, null, true);
    }
    void LoginNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SLoginNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.Login(Proto);
    }
    void LobbyNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SLobbyNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.SceneSetNext(new CSceneLobby());

    }
    void ChatNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SChatNetSc)Proto_;
    }
    void UserExpNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SUserExpNetSc)Proto_;
    }
    void UserResourcesNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SResourcesNetSc)Proto_;

        CGlobal.LoginNetSc.User.Resources = Proto.Resources;

        var Scene = CGlobal.GetScene<CSceneBase>();
        Scene.ResourcesUpdate();
    }
    void SingleStartNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SSingleStartNetSc)Proto_;

        CGlobal.LoginNetSc.User.Resources.SubResource(EResource.Gold, Proto.GoldCost);
        CGlobal.LoginNetSc.User.SinglePlayCount = Proto.PlayCount;
        CGlobal.LoginNetSc.User.SingleRefreshTime = Proto.RefreshTime;
        foreach(var i in Proto.DoneQuests)
            CGlobal.LoginNetSc.Quests[i.SlotIndex].Count = i.Count;
    }
    void SingleEndNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SSingleEndNetSc)Proto_;

        foreach (var i in Proto.DoneQuests)
            CGlobal.LoginNetSc.Quests[i.SlotIndex].Count = i.Count;

        var Scene = CGlobal.GetScene<CSceneLobby>();
        if (Scene != null)
            Scene.QuestRedDotCheck();
    }
    void IslandStartNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SIslandStartNetSc)Proto_;

        CGlobal.LoginNetSc.User.Resources.SubResource(EResource.Gold, Proto.GoldCost);
        CGlobal.LoginNetSc.User.IslandPlayCount = Proto.PlayCount;
        CGlobal.LoginNetSc.User.IslandRefreshTime = Proto.RefreshTime;
        foreach (var i in Proto.DoneQuests)
            CGlobal.LoginNetSc.Quests[i.SlotIndex].Count = i.Count;
    }
    void IslandEndNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SIslandEndNetSc)Proto_;

        foreach (var i in Proto.DoneQuests)
            CGlobal.LoginNetSc.Quests[i.SlotIndex].Count = i.Count;

        var Scene = CGlobal.GetScene<CSceneLobby>();
        if (Scene != null)
            Scene.QuestRedDotCheck();
    }
    void RankingRewardInfoNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SRankingRewardInfoNetSc)Proto_;
        Dictionary<CGlobal.RankingType, Int32> RewardRankings = new Dictionary<CGlobal.RankingType, int>();
        if (Proto.Ranking != -1)
            RewardRankings.Add(CGlobal.RankingType.Multi, Proto.Ranking);
        if (Proto.RankingSingle != -1)
            RewardRankings.Add(CGlobal.RankingType.Single, Proto.RankingSingle);
        if (Proto.RankingIsland != -1)
            RewardRankings.Add(CGlobal.RankingType.Island, Proto.RankingIsland);

        var IsActive = RewardRankings.Count() > 0;

        if(IsActive)
        {
            if (CGlobal.LoginNetSc.User.RankingRewardedCounter < Proto.Counter)
                IsActive = true;
            else
                IsActive = false;
        }

        var Scene = CGlobal.GetScene<CSceneLobby>();
        if (Scene != null)
            Scene.SetRankingReward(IsActive, RewardRankings);
    }
    void RankingRewardNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SRankingRewardNetSc)Proto_;
        CGlobal.ProgressLoading.InvisibleProgressLoading();

        Dictionary<CGlobal.RankingType, Int32> RewardCodes = new Dictionary<CGlobal.RankingType, int>();
        if (Proto.RewardCode != 0)
            RewardCodes.Add(CGlobal.RankingType.Multi, Proto.RewardCode);
        if (Proto.RewardCodeSingle != 0)
            RewardCodes.Add(CGlobal.RankingType.Single, Proto.RewardCodeSingle);
        if (Proto.RewardCodeIsland != 0)
            RewardCodes.Add(CGlobal.RankingType.Island, Proto.RewardCodeIsland);

        CGlobal.LoginNetSc.User.RankingRewardedCounter = Proto.Counter;

        var Scene = CGlobal.GetScene<CSceneLobby>();
        if (Scene != null)
            Scene.GetRankingReward(RewardCodes);
    }
    void RankingRewardFailNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SRankingRewardFailNetSc)Proto_;
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        _SystemPopup.ShowPopup(EText.RankingReward_Popup_Failed, PopupSystem.PopupType.Confirm, true);
    }
    void PurchaseNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SPurchaseNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.IAPManager.Consum(Proto.ProductID);

        var Scene = CGlobal.GetScene<CSceneBase>();
        Scene.ResourcesAction(NetProtocolExtension.MakeResources(EResource.DiaPaid, Proto.DiaPaidAdded));
    }
    void BuyNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBuyNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.RewardPopup.ViewPopup(EText.Global_Button_Receive, CGlobal.MetaData.RewardItems[CGlobal.MetaData.ShopInGameMetas[Proto.Code].RewardCode].RewardList);

        CGlobal.SubResources(NetProtocolExtension.MakeResources(CGlobal.MetaData.ShopInGameMetas[Proto.Code].CostType, CGlobal.MetaData.ShopInGameMetas[Proto.Code].CostValue));
        var Scene = CGlobal.GetScene<CSceneBase>();
        Scene.ResourcesUpdate();
    }
    void BuyCharNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBuyCharNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.LoginNetSc.Chars.Add(Proto.Code);
        CGlobal.SubResources(NetProtocolExtension.MakeResources(CGlobal.MetaData.Chars[Proto.Code].Cost_Type, CGlobal.MetaData.Chars[Proto.Code].Price));

        var Scene = CGlobal.GetScene<CSceneBase>();
        Scene.ResourcesUpdate();
        CGlobal.RedDotControl.SetReddotChar(Proto.Code);
        AnalyticsManager.TrackingUnlockCharacter(Proto.Code);
    }
    void BattleJoinNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleJoinNetSc)Proto_;

        CGlobal.SceneSetNext(new CSceneMatching());
    }
    void BattleOutNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleOutNetSc)Proto_;

        CGlobal.SceneSetNext(new CSceneLobby());
    }
    void BattleBeginNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleBeginNetSc)Proto_;

        CGlobal.MusicStop();
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameStart);
        CGlobal.SceneSetNext(new CSceneBattleMulti(Proto));
    }
    void BattleMatchingNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleMatchingNetSc)Proto_;

        var Scene = CGlobal.GetScene<CSceneMatching>();
        if (Scene == null)
            return;

        Scene.SetReadyCount(Proto.UserCount);
    }
    void BattleStartNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleStartNetSc)Proto_;

        var Scene = CGlobal.GetScene<CSceneBattleMulti>();
        if (Scene == null)
            return;

        Scene.Start(Proto.EndTime);
    }
    void BattleEndNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleEndNetSc)Proto_;

        CGlobal.MusicStop();
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameEnd);
        var Scene = CGlobal.GetScene<CSceneBattleMulti>();
        var BPlayers = Scene.GetPlayers();
        SBattlePlayer[] Players = new SBattlePlayer[BPlayers.Length];
        SCharacterClientMeta[] PlayerMetas = new SCharacterClientMeta[BPlayers.Length];
        for(var i = 0; i < BPlayers.Length; ++i)
        {
            Players[i] = BPlayers[i].BattlePlayer;
            PlayerMetas[i] = BPlayers[i].Meta;
        }

        CGlobal.SceneSetNext(new CSceneBattleMultiEnd(Proto, Scene.GetSBattleBeginNetSc(), Players, PlayerMetas, Scene.GetWinTeamIndex()));

        foreach (var Quest in Proto.DoneQuests)
            CGlobal.LoginNetSc.Quests[Quest.SlotIndex].Count = Quest.Count;
    }
    void BattleSyncNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleSyncNetSc)Proto_;

        var Scene = CGlobal.GetScene<CSceneBattleMulti>();
        Scene.Sync(Proto);
    }
    void BattleTouchNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleTouchNetSc)Proto_;

        var Scene = CGlobal.GetScene<CSceneBattleMulti>();
        if (Scene == null)
            return;

        Scene.Touch(Proto);
    }
    void BattlePushNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattlePushNetSc)Proto_;

        var Scene = CGlobal.GetScene<CSceneBattleMulti>();
        if (Scene == null)
            return;

        Scene.Push(Proto);
    }
    void BattleIconNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleIconNetSc)Proto_;

        var Scene = CGlobal.GetScene<CSceneBattleMulti>();
        if (Scene == null)
            return;
        Scene.EmotionCallback(Proto.PlayerIndex, Proto.Code);
    }
    void BattleLinkNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleLinkNetSc)Proto_;

        var Scene = CGlobal.GetScene<CSceneBattleMulti>();
        if (Scene == null)
            return;

        Scene.Link(Proto);
    }
    void BattleUnLinkNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleUnLinkNetSc)Proto_;

        var Scene = CGlobal.GetScene<CSceneBattleMulti>();
        if (Scene == null)
            return;

        Scene.UnLink(Proto);
    }
    void SingleBattleStartNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SSingleBattleStartNetSc)Proto_;
        var Scene = CGlobal.GetScene<CSceneBaseMulti>();
        Debug.Log("EndTime = "+ Proto.EndTime.ToString());
        // 싱글 네트웤 컨텐츠 제거 코드 ( 화살피하기, 섬건너기 )
        if (Scene != null)
            Scene.GameStart(Proto.EndTime);
    }
    void SingleBattleScoreNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SSingleBattleScoreNetSc)Proto_;
        var Scene = CGlobal.GetScene<CSceneBaseMulti>();
        // 싱글 네트웤 컨텐츠 제거 코드 ( 화살피하기, 섬건너기 )
        if (Scene != null)
            Scene.UpdateScore(Proto.PlayerIndex,Proto.Score);
    }
    void SingleBattleIconNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SSingleBattleIconNetSc)Proto_;
        var Scene = CGlobal.GetScene<CSceneBaseMulti>();
        // 싱글 네트웤 컨텐츠 제거 코드 ( 화살피하기, 섬건너기 )
        if (Scene != null)
            Scene.ShowEmoticon(Proto.PlayerIndex, Proto.Code);
    }
    void SingleBattleItemNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SSingleBattleItemNetSc)Proto_;
        var Scene = CGlobal.GetScene<CSceneBaseMulti>();
        // 싱글 네트웤 컨텐츠 제거 코드 ( 화살피하기, 섬건너기 )
        if (Scene != null)
            Scene.UseItem(Proto.PlayerIndex, (EMultiItemType)Proto.Code);
    }
    void SingleBattleEndNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SSingleBattleEndNetSc)Proto_;
        var BattleEndNetSc = (SBattleEndNetSc)Proto;

        CGlobal.MusicStop();
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameEnd);
        var Scene = CGlobal.GetScene<CSceneBaseMulti>();
        // 싱글 네트웤 컨텐츠 제거 코드 ( 화살피하기, 섬건너기 )
        if (Scene != null)
        {
            Scene.GameOver();
            CGlobal.SceneSetNext(new CSceneBattleMultiEnd(BattleEndNetSc, Scene.GetSBattleBeginNetSc(), Scene.GetPlayers(), Scene.GetCharacters(), Scene.GetWinTeamIndex()));
        }

        foreach (var Quest in Proto.DoneQuests)
            CGlobal.LoginNetSc.Quests[Quest.SlotIndex].Count = Quest.Count;
    }
    void RoomListNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        SRoomListNetSc Proto = (SRoomListNetSc)Proto_;
        CGlobal.RoomDictionary = Proto.RoomList;
        CGlobal.SceneSetNext(new CSceneRoomList());
    }
    void RoomChangeNetSc(CKey Key_, SProto Proto_)
    {
        SRoomChangeNetSc Proto = (SRoomChangeNetSc)Proto_;
        if (CGlobal.MyRoomInfo != null)
        {
            if (CGlobal.MyRoomInfo.RoomIdx == Proto.RoomIdx)
            {
                CGlobal.MyRoomInfo = Proto.RoomInfo;
            }
        }
        var SceneRoom = CGlobal.GetScene<CSceneRoom>();
        if(SceneRoom != null)
        {
            if(SceneRoom.GetRoomIdx() == Proto.RoomIdx)
            {
                if (Proto.IsEmpty)
                    CGlobal.NetControl.Send<SRoomListNetCs>(new SRoomListNetCs()); //이쪽으로 오면 안됨.
                else
                    SceneRoom.RoomInfoChange(Proto.RoomInfo);
            }
        }
        var SceneRoomList = CGlobal.GetScene<CSceneRoomList>();
        if (SceneRoomList != null)
        {
            if(Proto.IsEmpty)
                CGlobal.RoomDictionary.Remove(Proto.RoomIdx);
            else
            {
                if (CGlobal.RoomDictionary.ContainsKey(Proto.RoomIdx))
                    CGlobal.RoomDictionary[Proto.RoomIdx] = Proto.RoomInfo;
                else
                    CGlobal.RoomDictionary.Add(Proto.RoomIdx, Proto.RoomInfo);
            }
            SceneRoomList.UpdateRoomList();
        }
        var SceneLobby = CGlobal.GetScene<CSceneLobby>();
        if (SceneLobby != null)
        {
            if (Proto.IsEmpty)
                SceneLobby.RemoveADPanel(Proto.RoomIdx);
            else
                SceneLobby.UpdateRoomAD(Proto.RoomInfo);
        }
    }
    void RoomCreateNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        SRoomCreateNetSc Proto = (SRoomCreateNetSc)Proto_;
        CGlobal.MyRoomInfo = Proto.RoomInfo;
        CGlobal.SceneSetNext(new CSceneRoom());
    }
    void RoomJoinNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        SRoomJoinNetSc Proto = (SRoomJoinNetSc)Proto_;
        var SceneRoom = CGlobal.GetScene<CSceneRoom>();
        if (SceneRoom != null)
        {
            SceneRoom.RoomInfoChange(Proto.RoomInfo);
        }
        else
        {
            CGlobal.MyRoomInfo = Proto.RoomInfo;
            CGlobal.SceneSetNext(new CSceneRoom());
        }
    }
    void RoomJoinFailedNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        SRoomJoinFailedNetSc Proto = (SRoomJoinFailedNetSc)Proto_;

        CGlobal.SystemPopup.ShowPopup(EText.MultiScene_Popup_RoomFailed, PopupSystem.PopupType.Confirm);
    }
    void RoomOutNetSc(CKey Key_, SProto Proto_)
    {
        //SRoomOutNetSc Proto = (SRoomOutNetSc)Proto_;
        CGlobal.MyRoomInfo = null;
        CGlobal.NetControl.Send<SRoomListNetCs>(new SRoomListNetCs());
    }
    void RoomOutFailedNetSc(CKey Key_, SProto Proto_)
    {
        //SRoomOutFailedNetSc Proto = (SRoomOutFailedNetSc)Proto_;
        CGlobal.ProgressLoading.InvisibleProgressLoading();
    }
    void RoomReadyNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        SRoomReadyNetSc Proto = (SRoomReadyNetSc)Proto_;
        var SceneRoom = CGlobal.GetScene<CSceneRoom>();
        if (SceneRoom != null)
        {
            SceneRoom.StartReady(Proto.Mode);
        }
        var SceneRoomList = CGlobal.GetScene<CSceneRoomList>();
        if (SceneRoomList != null)
        {
            SceneRoomList.StartReady(Proto.Mode);
        }
    }
    void RoomChatNetSc(CKey Key_, SProto Proto_)
    {
        SRoomChatNetSc Proto = (SRoomChatNetSc)Proto_;
        var SceneRoom = CGlobal.GetScene<CSceneRoom>();
        if (SceneRoom != null)
        {
            SceneRoom.AddChat(Proto.UserNick,Proto.Message);
        }
    }
    void RoomNotiNetSc(CKey Key_, SProto Proto_)
    {
        SRoomNotiNetSc Proto = (SRoomNotiNetSc)Proto_;
        var SceneLobby = CGlobal.GetScene<CSceneLobby>();
        if(SceneLobby != null)
        {
            SceneLobby.UpdateRoomAD(Proto.RoomInfo);
        }
    }
    void GachaNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SGachaNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.SubResources(Proto.Cost);
        CGlobal.LoginNetSc.Chars.Add(Proto.CharCode);

        var Scene = CGlobal.GetScene<CSceneShop>();
        Scene.ResourcesUpdate();
        Scene.GachaAnimationView(GachaPopup.EGachaType.Special, Proto.CharCode);

        CGlobal.RedDotControl.SetReddotChar(Proto.CharCode);
        AnalyticsManager.TrackingUnlockCharacter(Proto.CharCode);
    }
    void GachaX10NetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SGachaX10NetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.SubResources(Proto.Cost);
        CGlobal.LoginNetSc.User.Resources.Add(Proto.Refund);
        var NewCharList = new System.Collections.Generic.List<Int32>();
        foreach(var i in Proto.CharCodeList)
        {
            if(!CGlobal.LoginNetSc.Chars.Contains(i))
            {
                CGlobal.LoginNetSc.Chars.Add(i);
                CGlobal.RedDotControl.SetReddotChar(i);
                AnalyticsManager.TrackingUnlockCharacter(i);
                NewCharList.Add(i);
            }
        }

        var Scene = CGlobal.GetScene<CSceneShop>();
        Scene.ResourcesUpdate();
        Scene.GachaAnimationView(GachaPopup.EGachaType.Special, Proto.CharCodeList, NewCharList);

    }
    void GachaFailedNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SGachaFailedNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.SubResources(Proto.Cost);
        CGlobal.LoginNetSc.User.Resources.Add(Proto.Refund);

        var Resource = Proto.Refund.ToOneResource();
        var Scene = CGlobal.GetScene<CSceneShop>();
        Scene.ResourcesUpdate();
        Scene.GachaAnimationView(GachaPopup.EGachaType.Special, Proto.CharCode, Resource.Item1, Resource.Item2);
    }
    void QuestGotNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SQuestGotNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        foreach (var Quest in Proto.Quests)
            CGlobal.LoginNetSc.Quests.Add(Quest.SlotIndex, new SQuestBase(Quest.Code,0,new TimePoint()));

        var Scene = CGlobal.GetScene<CSceneLobby>();
        if (Scene == null)
            return;

        Scene.UpdateQuestList();
    }
    void QuestDoneNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SQuestDoneNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.LoginNetSc.Quests[Proto.SlotIndex].Count = Proto.Count;

        var Scene = CGlobal.GetScene<CSceneLobby>();
        if (Scene != null)
            Scene.UpdateQuestList();
    }
    void QuestRewardNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SQuestRewardNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.RewardPopup.ViewPopup(EText.Global_Button_Receive, CGlobal.MetaData.RewardItems[CGlobal.MetaData.QuestMetas[CGlobal.GetUserQuestInfo(Proto.SlotIndex).Value.Code].RewardCode].RewardList);

        AnalyticsManager.AddQuestCompleteCount();

        CGlobal.LoginNetSc.User.QuestDailyCompleteCount = Proto.DailyCompleteCount;
        CGlobal.LoginNetSc.User.QuestDailyCompleteRefreshTime = Proto.DailyCompleteRefreshTime;

        var Scene = CGlobal.GetScene<CSceneLobby>();
        if (Scene == null)
            return;

        if (Proto.CoolEndTime.Ticks > 0)
        {
            CGlobal.LoginNetSc.Quests[Proto.SlotIndex].CoolEndTime = Proto.CoolEndTime;
            Scene.ChangeQuest(Proto.SlotIndex);
        }
        else
        {
            CGlobal.LoginNetSc.Quests.Remove(Proto.SlotIndex);
            Scene.RemoveQuest(Proto.SlotIndex);
        }
    }
    void QuestNextNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SQuestNextNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();

        if (Proto.NewCode == 0)
        {
            CGlobal.LoginNetSc.Quests.Remove(Proto.SlotIndex);
        }
        else
        {
            CGlobal.LoginNetSc.Quests[Proto.SlotIndex].Code = Proto.NewCode;
            CGlobal.LoginNetSc.Quests[Proto.SlotIndex].Count = 0;
            CGlobal.LoginNetSc.Quests[Proto.SlotIndex].CoolEndTime = new TimePoint();
        }

        //if (Proto.UseResource)
        //    CGlobal.SubResources(NetProtocolExtension.MakeResources(EResource.Gold, CGlobal.MetaData.ConfigMeta.QuestNextCostGold));

        var Scene = CGlobal.GetScene<CSceneLobby>();
        if (Scene == null)
            return;

        Scene.ResourcesUpdate();
        Scene.ChangeQuest(Proto.SlotIndex, Proto.NewCode);
    }
    void QuestDailyCompleteRewardNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SQuestDailyCompleteRewardNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        var TextID = EText.Global_Button_Receive;
        if (Proto.WatchAd)
            TextID = EText.Popup_DailyMissionAdReward;

        CGlobal.RewardPopup.ViewPopup(TextID, CGlobal.MetaData.RewardItems[CGlobal.MetaData.QuestDailyComplete.Meta.RewardCode].RewardList, Proto.WatchAd);

        CGlobal.LoginNetSc.User.QuestDailyCompleteCount = 0;
        CGlobal.LoginNetSc.User.QuestDailyCompleteRefreshTime = Proto.RefreshTime;
    }
    void ChangeNickNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SChangeNickNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        var Scene = CGlobal.GetScene<CSceneLobby>();
        if (Scene == null)
            return;

        Scene.SetNicknameBox(false);
        if(CGlobal.LoginNetSc.User.ChangeNickFreeCount > 0)
            CGlobal.LoginNetSc.User.ChangeNickFreeCount--;
        else
            CGlobal.LoginNetSc.User.Resources.SubDia(CGlobal.MetaData.ConfigMeta.ChangeNickCostDia);
        Scene.ResourcesUpdate();
        Scene.SetNickname(Proto.Nick);
    }
    void ChangeNickFailNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SChangeNickFailNetSc)Proto_;

        CGlobal.ProgressLoading.InvisibleProgressLoading();

        if (Proto.GameRet == EGameRet.Ok)
            CGlobal.ShowHaveForbiddenWord(Proto.ForbiddenWord);
        else
            CGlobal.SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(EText.SceneMyInfo_NickNameChangeFailed), Proto.GameRet), PopupSystem.PopupType.Confirm, null);
    }
    void SetRankPoint(CKey Key_, SProto Proto_)
    {
        var Proto = (SSetPointNetSc)Proto_;
        CGlobal.LoginNetSc.User.Point = Proto.Point;
        if (CGlobal.LoginNetSc.User.Point > CGlobal.LoginNetSc.User.PointBest)
            CGlobal.LoginNetSc.User.PointBest = CGlobal.LoginNetSc.User.Point;
    }
    void SetCharacter(CKey Key_, SProto Proto_)
    {
        var Proto = (SSetCharNetSc)Proto_;
        foreach(var i in Proto.CharCodes)
        {
            CGlobal.LoginNetSc.Chars.Add(i);
            CGlobal.RedDotControl.SetReddotChar(i);
            AnalyticsManager.TrackingUnlockCharacter(i);
        }
    }
    void DelCharacter(CKey Key_, SProto Proto_)
    {
        var Proto = (SUnsetCharNetSc)Proto_;
        foreach (var i in Proto.CharCodes)
        {
            if(CGlobal.LoginNetSc.Chars.Contains(i))
            {
                CGlobal.LoginNetSc.Chars.Remove(i);
            }
        }
    }
    void CouponUseNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SCouponUseNetSc)Proto_;
        var AddResource = new Int32[(Int32)EResource.Max];
        var RewardMetars = new System.Collections.Generic.List<SRewardMeta>();
        for (var i = 0; i < (Int32)EResource.Max; ++i )
        {
            AddResource[i] = Proto.ResourcesLeft[i];
        }
        AddResource.Sub(CGlobal.LoginNetSc.User.Resources);
        CGlobal.LoginNetSc.User.Resources = Proto.ResourcesLeft;
        if(Proto.CharsAdded.Count() > 0)
        {
            foreach(var i in Proto.CharsAdded)
            {
                RewardMetars.Add(new SRewardMeta(ERewardType.Character, i));
            }
        }
        if(AddResource[(Int32)EResource.Ticket] > 0)
            RewardMetars.Add(new SRewardMeta(ERewardType.Resource_Ticket, AddResource[(Int32)EResource.Ticket]));

        if (AddResource[(Int32)EResource.CP] > 0)
            RewardMetars.Add(new SRewardMeta(ERewardType.Resource_CP, AddResource[(Int32)EResource.CP]));

        if (AddResource[(Int32)EResource.Gold] > 0)
            RewardMetars.Add(new SRewardMeta(ERewardType.Resource_Gold, AddResource[(Int32)EResource.Gold]));

        if (AddResource[(Int32)EResource.Dia] > 0)
            RewardMetars.Add(new SRewardMeta(ERewardType.Resource_Dia, AddResource[(Int32)EResource.Dia]));

        CGlobal.RewardPopup.ViewPopup(EText.SceneSetting_Coupon_Title, RewardMetars, false);
        CGlobal.ProgressLoading.InvisibleProgressLoading();
    }
    void CoupontFailNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SCouponUseFailNetSc)Proto_;
        CGlobal.ProgressLoading.InvisibleProgressLoading();

        if (Proto.Ret == ERet.CouponAlreadyUsed)
            CGlobal.SystemPopup.ShowPopup(EText.SceneSetting_Coupon_Duplicate, PopupSystem.PopupType.Confirm);
        else
            CGlobal.SystemPopup.ShowPopup(EText.SceneSetting_Coupon_Not, PopupSystem.PopupType.Confirm);
    }
    void BuyPackageNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        var Proto = (SBuyPackageNetSc)Proto_;

        CGlobal.LoginNetSc.Packages.Add(Proto.Code);

        var Meta = CGlobal.MetaData.ShopPackageMetas[Proto.Code];
        var RewardList = CGlobal.MetaData.GetRewardList(Meta.RewardCode);
        CGlobal.RewardPopup.ViewPopup(Meta.ETextName, RewardList, false);

        var AddResource = new Int32[(Int32)EResource.Max];
        foreach (var i in RewardList)
        {
            switch (i.Type)
            {
                case ERewardType.Character:
                    if(!CGlobal.LoginNetSc.Chars.Contains(i.Data))
                    {
                        CGlobal.LoginNetSc.Chars.Add(i.Data);
                        CGlobal.RedDotControl.SetReddotChar(i.Data);
                        AnalyticsManager.TrackingUnlockCharacter(i.Data);
                    }
                    break;
                case ERewardType.Resource_Ticket:
                    AddResource[(Int32)EResource.Ticket] = i.Data;
                    break;
                case ERewardType.Resource_Gold:
                    AddResource[(Int32)EResource.Gold] = i.Data;
                    break;
                case ERewardType.Resource_Dia:
                    AddResource[(Int32)EResource.Dia] = i.Data;
                    break;
                case ERewardType.Resource_CP:
                    AddResource[(Int32)EResource.CP] = i.Data;
                    break;
                default:
                    Debug.Log("Reward Type Error !!!");
                    break;
            }
        }
        //CGlobal.LoginNetSc.User.Resources.AddResources(AddResource);

        CGlobal.LoginNetSc.User.Resources.SubResource(Meta.CostType, Meta.CostValue);

        var Scene = CGlobal.GetScene<CSceneShop>();
        if (Scene != null)
            Scene.PackageList();
        Scene.ResourcesUpdate();
    }
    void DailyRewardNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        var Proto = (SDailyRewardNetSc)Proto_;

        //CGlobal.LoginNetSc.User.Resources.AddResource(Proto.Reward.Type, Proto.Reward.Data);
        CGlobal.LoginNetSc.User.DailyRewardExpiredTime = Proto.ExpiredTime;
        CGlobal.LoginNetSc.User.DailyRewardCountLeft = Proto.CountLeft;

        var RewardMetars = new System.Collections.Generic.List<SRewardMeta>();
        RewardMetars.Add(new SRewardMeta(CGlobal.GetResourcesToRewardType(Proto.Reward.Type), Proto.Reward.Data));
        CGlobal.RewardPopup.ViewPopup(EText.Popup_FreeBoxAdReward, RewardMetars, false);

        var UseCount = (CGlobal.MetaData.ShopConfigMeta.DailyRewardAdCount + CGlobal.MetaData.ShopConfigMeta.DailyRewardFreeCount)-CGlobal.LoginNetSc.User.DailyRewardCountLeft;

        AnalyticsManager.SetShopFreeCount(UseCount);

        var Scene = CGlobal.GetScene<CSceneShop>();
        if(Scene != null)
        {
            Scene.DailyRefresh();
            Scene.ResourcesUpdate();
        }
    }
    void DailyRewardFailNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(EText.ShopPopup_Text_Error_Code),CGlobal.MetaData.GetText(EText.ShopScene_Upscale_DailyFree)), PopupSystem.PopupType.Confirm, null);
    }
}
