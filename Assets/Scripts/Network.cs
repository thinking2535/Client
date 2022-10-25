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
using rso.Base;

public partial class Main
{
    void Link(CKey Key_, TUID UID_, string Nick_, TFriends Friends_)
    {
        CGlobal.ProgressCircle.Deactivate();
        CGlobal.Link(UID_, Nick_);
    }
    async void LinkFail(TPeerCnt PeerNum_, EGameRet GameRet_)
    {
        CGlobal.ProgressCircle.Deactivate();
        if(GameRet_ == EGameRet.InvalidVersion)
        {
            if (await CGlobal.curScene.pushUpdateMessagePopup() is true)
                Application.OpenURL(CGlobal.Device.GetUpdateUrl());

            CGlobal.Quit();
        }
        else if (GameRet_ == EGameRet.AlreadyExist)
        {
            if (CGlobal.WillClose == false)
            {
                await CGlobal.curScene.pushNoticePopup(true, EText.NickNameAlreadyExists);

                var nickname = await CGlobal.curScene.pushCreatePopup();
                CGlobal.Create((string)nickname);
            }
        }
        else
        {
            if (CGlobal.WillClose == false)
            {
                await CGlobal.curScene.pushNoticePopup(true, EText.GlobalPopup_Network_LinkFailed, GameRet_);

                CGlobal.clearAndPushIntroScene();
            }
        }
    }
    async void UnLink(CKey Key_, EGameRet GameRet_)
    {
        if (CGlobal.WillClose == false)
        {
            CGlobal.ProgressCircle.Deactivate();

            await CGlobal.curScene.pushNoticePopup(true, EText.GlobalPopup_Network_Unlink, GameRet_);

            CGlobal.clearAndPushIntroScene();
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
    public async void RetNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SRetNetSc)Proto_;

        await CGlobal.curScene.pushNoticePopup(true, EText.GlobalPopup_Network_Error, Proto.Ret);

        throw new Exception("RetNetSc : " + Proto.Ret.ToString());
    }

    //Text 데이터에 없고 문제를 바로 알려주는 Message String 타입.
    async void MsgNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SMsgNetSc)Proto_;
        await CGlobal.curScene.pushNoticePopup(true, Proto.Msg);
    }
    void LoginNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SLoginNetSc)Proto_;
        CGlobal.Login(Proto);
    }
    void LobbyNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SLobbyNetSc)Proto_;
        CGlobal.setLobbyScene();
    }
    void ChatNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SChatNetSc)Proto_;
        CGlobal.ConsoleWindow.AddContent(Proto.Msg);
    }
    void ExpNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SUserExpNetSc)Proto_;
    }
    void ResourcesNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SResourcesNetSc)Proto_;

        CGlobal.LoginNetSc.User.Resources = Proto.Resources;
        CGlobal.curScene.UpdateResources();
    }
    void RankingRewardInfoNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SRankingRewardInfoNetSc)Proto_;

        var Scene = CGlobal.GetScene<LobbyScene>();
        if (Scene != null)
            Scene.SetRankingReward(Proto);
    }
    void RankingRewardNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SRankingRewardNetSc)Proto_;
        CGlobal.LoginNetSc.getUnitRewardsAndSetReward(Proto);
        CGlobal.LoginNetSc.User.RankingRewardedCounter = Proto.Counter;

        var Scene = CGlobal.UpdateResourcesAndGetScene<LobbyScene>();
        if (Scene != null)
            Scene.GetRankingReward(Proto.myRankingArray);
    }
    async void RankingRewardFailNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SRankingRewardFailNetSc)Proto_;

        await CGlobal.curScene.pushNoticePopup(true, EText.RankingReward_Popup_Failed);
    }
    async void BuyNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SBuyNetSc)Proto_;
        var UnitRewards = CGlobal.LoginNetSc.getUnitRewardsAndSetResources(Proto.ResourcesLeft);
        if (UnitRewards.Count > 0)
            await CGlobal.curScene.pushRewardPopup(EText.Global_Button_Receive, UnitRewards);

        CGlobal.UpdateResourcesCurrentScene();
    }
    void BuyCharNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SBuyCharNetSc)Proto_;
        CGlobal.LoginNetSc.Chars.Add(Proto.Code);
        CGlobal.AddResource(-CGlobal.MetaData.Characters[Proto.Code].getCost());

        var Scene = CGlobal.UpdateResourcesAndGetScene<MoneyUIScene>();
        Scene.buyCharacterNetSc(Proto.Code);
        CGlobal.RedDotControl.SetReddotChar(Proto.Code);
    }
    void BattleSyncNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleSyncNetSc)Proto_;

        var Scene = CGlobal.GetScene<NetworkBattleScene>();
        if (Scene == null)
            return;

        Scene.Sync(Proto.Tick);
    }
    void BattleDirectNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleDirectNetSc)Proto_;

        var Scene = CGlobal.GetScene<NetworkBattleScene>();
        if (Scene == null)
            return;

        Scene.direct(Proto);
    }
    void BattleFlapNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattleFlapNetSc)Proto_;

        var Scene = CGlobal.GetScene<NetworkBattleScene>();
        if (Scene == null)
            return;

        Scene.flap(Proto);
    }
    void BattlePumpNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SBattlePumpNetSc)Proto_;

        var Scene = CGlobal.GetScene<NetworkBattleScene>();
        if (Scene == null)
            return;

        Scene.pump(Proto);
    }
    void MultiBattleJoinNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SMultiBattleJoinNetSc)Proto_;
        CGlobal.setMultiMatchingScene();
    }
    void MultiBattleOutNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SMultiBattleOutNetSc)Proto_;
        CGlobal.setLobbyScene();
    }
    void MultiBattleBeginNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SMultiBattleBeginNetSc)Proto_;

        CGlobal.MusicStop();
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameStart);

        CGlobal.sceneController.set(() =>
        {
            var scene = SceneController.create<MultiBattleScene>(CGlobal.MetaData.GetMapPrefabName(Proto));
            scene.init(Proto);
            return scene;
        });
    }
    void MultiBattleStartNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SMultiBattleStartNetSc)Proto_;

        var Scene = CGlobal.GetScene<MultiBattleScene>();
        if (Scene == null)
            return;

        Scene.StartBattle(Proto.EndTick);
    }
    void MultiBattleEndNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SMultiBattleEndNetSc)Proto_;

        CGlobal.UpdateQuest(Proto.DoneQuests);
        CGlobal.MusicStop();
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameEnd);

        var Scene = CGlobal.GetScene<MultiBattleScene>();
        Scene.SetMultiBattleEndScene(Proto);
    }
    void MultiBattleEndDrawNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SMultiBattleEndDrawNetSc)Proto_;

        CGlobal.UpdateQuest(Proto.DoneQuests);
        CGlobal.MusicStop();
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameEnd);

        var Scene = CGlobal.GetScene<MultiBattleScene>();
        Scene.SetMultiBattleEndDrawScene(Proto);
    }
    void MultiBattleEndInvalidNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SMultiBattleEndInvalidNetSc)Proto_;

        CGlobal.MusicStop();
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameEnd);

        var Scene = CGlobal.GetScene<MultiBattleScene>();
        Scene.SetMultiBattleEndInvalidScene(Proto);
    }
    void MultiBattleIconNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SMultiBattleIconNetSc)Proto_;

        var Scene = CGlobal.GetScene<MultiBattleScene>();
        if (Scene == null)
            return;
        Scene.EmotionCallback(Proto.PlayerIndex, Proto.Code);
    }
    void MultiBattleLinkNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SMultiBattleLinkNetSc)Proto_;

        var Scene = CGlobal.GetScene<MultiBattleScene>();
        if (Scene == null)
            return;

        Scene.Link(Proto);
    }
    void MultiBattleUnLinkNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SMultiBattleUnLinkNetSc)Proto_;

        var Scene = CGlobal.GetScene<MultiBattleScene>();
        if (Scene == null)
            return;

        Scene.UnLink(Proto);
    }
    void InvalidDisconnectInfoNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SInvalidDisconnectInfoNetSc)Proto_;
        CGlobal.LoginNetSc.User.InvalidDisconnectInfo = Proto;
    }
    void ArrowDodgeBattleJoinNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();
        var Proto = (SArrowDodgeBattleJoinNetSc)Proto_;

        CGlobal.LoginNetSc.User.Resources.AddResource(EResource.Gold, -Proto.GoldCost);
        CGlobal.LoginNetSc.User.SinglePlayCount = Proto.PlayCount;
        CGlobal.LoginNetSc.User.SingleRefreshTime = Proto.RefreshTime;

        CGlobal.UpdateQuest(Proto.DoneQuests);
    }
    void ArrowDodgeBattleBeginNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SArrowDodgeBattleBeginNetSc)Proto_;
        CGlobal.sceneController.set(() =>
        {
            var scene = SceneController.create<ArrowDodgeBattleScene>(CGlobal.MetaData.GetArrowDodgeMap().PrefabName);
            scene.init(Proto);
            return scene;
        });
    }
    void ArrowDodgeBattleStartNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SArrowDodgeBattleStartNetSc)Proto_;

        var Scene = CGlobal.GetScene<ArrowDodgeBattleScene>();
        if (Scene == null)
            return;

        Scene.startBattle(Proto);
    }
    void ArrowDodgeBattleEndNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SArrowDodgeBattleEndNetSc)Proto_;

        var Scene = CGlobal.GetScene<ArrowDodgeBattleScene>();
        if (Scene == null)
            return;

        Scene.battleEnd(Proto);
    }
    void FlyAwayBattleJoinNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();
        var Proto = (SFlyAwayBattleJoinNetSc)Proto_;

        CGlobal.LoginNetSc.User.Resources.AddResource(EResource.Gold, -Proto.GoldCost);
        CGlobal.LoginNetSc.User.IslandPlayCount = Proto.PlayCount;
        CGlobal.LoginNetSc.User.IslandRefreshTime = Proto.RefreshTime;
        CGlobal.UpdateQuest(Proto.DoneQuests);
    }
    void FlyAwayBattleBeginNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SFlyAwayBattleBeginNetSc)Proto_;
        CGlobal.sceneController.set(() =>
        {
            var scene = SceneController.create<FlyAwayBattleScene>(CGlobal.MetaData.GetFlyAwayMap().PrefabName);
            scene.init(Proto);
            return scene;
        });
    }
    void FlyAwayBattleStartNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SFlyAwayBattleStartNetSc)Proto_;

        var Scene = CGlobal.GetScene<FlyAwayBattleScene>();
        if (Scene == null)
            return;

        Scene.startBattle(Proto);
    }
    void FlyAwayBattleEndNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SFlyAwayBattleEndNetSc)Proto_;

        var Scene = CGlobal.GetScene<FlyAwayBattleScene>();
        if (Scene == null)
            return;

        Scene.battleEnd(Proto);
    }
    async void RankRewardNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SRankRewardNetSc)Proto_;
        CGlobal.LoginNetSc.User.NextRewardRankIndex = Proto.NextRewardRankIndex;

        var UnitRewards = CGlobal.LoginNetSc.getUnitRewardsAndSetReward(Proto);
        if (UnitRewards.Count > 0)
            await CGlobal.curScene.pushRewardPopup(EText.Ranking_Popup_RewardInfo, UnitRewards);

        var Scene = CGlobal.UpdateResourcesAndGetScene<RankingRewardScene>();
        if (Scene == null)
            return;

        Scene.RankRewardNetSc();
    }
    void QuestGotNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SQuestGotNetSc)Proto_;
        var questsGot = new Dictionary<Byte, SQuestBase>();

        foreach (var quest in Proto.Quests)
        {
            var newQuestBase = new SQuestBase(quest.Code, 0, new TimePoint());
            CGlobal.LoginNetSc.Quests.Add(quest.SlotIndex, newQuestBase);
            questsGot.Add(quest.SlotIndex, newQuestBase);
        }

        var Scene = CGlobal.GetScene<LobbyScene>();
        if (Scene == null)
            return;

        Scene.gotQuests(questsGot);
    }
    void QuestSetNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SQuestSetNetSc)Proto_;
        var newQuest = new SQuestBase(Proto.NewQuestCode, 0, new TimePoint());
        CGlobal.LoginNetSc.Quests[Proto.SlotIndex] = newQuest;

        var Scene = CGlobal.GetScene<LobbyScene>();
        if (Scene == null)
            return;

        Scene.updateQuest(Proto.SlotIndex, newQuest);
    }
    void QuestDoneNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SQuestDoneNetSc)Proto_;
        CGlobal.LoginNetSc.Quests[Proto.SlotIndex].Count = Proto.Count;

        var Scene = CGlobal.GetScene<LobbyScene>();
        if (Scene != null)
            Scene.doneQuest(Proto.SlotIndex);
    }
    async void QuestRewardNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SQuestRewardNetSc)Proto_;

        CGlobal.LoginNetSc.User.QuestDailyCompleteCount = Proto.DailyCompleteCount;
        CGlobal.LoginNetSc.User.QuestDailyCompleteRefreshTime = Proto.DailyCompleteRefreshTime;

        SQuestBase questBase = null;
        if (Proto.newCode == 0)
        {
            CGlobal.LoginNetSc.Quests.Remove(Proto.SlotIndex);
        }
        else
        {
            questBase = new SQuestBase(Proto.newCode, 0, Proto.CoolEndTime);
            CGlobal.LoginNetSc.Quests[Proto.SlotIndex] = questBase;
        }

        var UnitRewards = CGlobal.LoginNetSc.getUnitRewardsAndSetReward(Proto);
        if (UnitRewards.Count > 0)
            await CGlobal.curScene.pushRewardPopup(EText.Ranking_Popup_RewardInfo, UnitRewards);

        AnalyticsManager.AddQuestCompleteCount();

        var lobbyScene = CGlobal.UpdateResourcesAndGetScene<LobbyScene>();
        if (lobbyScene == null)
            return;

        lobbyScene.rewardQuest(questBase, Proto);
    }
    async void QuestDailyCompleteRewardNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SQuestDailyCompleteRewardNetSc)Proto_;
        CGlobal.LoginNetSc.User.QuestDailyCompleteCount = 0;
        CGlobal.LoginNetSc.User.QuestDailyCompleteRefreshTime = Proto.RefreshTime;

        var UnitRewards = CGlobal.LoginNetSc.getUnitRewardsAndSetReward(Proto);
        if (UnitRewards.Count > 0)
            await CGlobal.curScene.pushRewardPopup(EText.Ranking_Popup_RewardInfo, UnitRewards);

        CGlobal.UpdateResourcesCurrentScene();
    }
    void ChangeNickNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SChangeNickNetSc)Proto_;

        var Scene = CGlobal.GetScene<LobbyScene>();
        if (Scene == null)
            return;

        if(CGlobal.LoginNetSc.User.ChangeNickFreeCount > 0)
        {
            CGlobal.LoginNetSc.User.ChangeNickFreeCount--;
        }
        else
        {
            CGlobal.LoginNetSc.User.Resources.AddResource(CGlobal.MetaData.ConfigMeta.ChangeNickCostType, -CGlobal.MetaData.ConfigMeta.ChangeNickCostValue);
            Scene.UpdateResources();
        }

        Scene.SetNickname(Proto.Nick);
    }
    async void ChangeNickFailNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SChangeNickFailNetSc)Proto_;

        CGlobal.ProgressCircle.Deactivate();
        await CGlobal.curScene.pushNoticePopup(true, EText.SceneMyInfo_NickNameChangeFailed, Proto.GameRet);
    }
    void SetPoint(CKey Key_, SProto Proto_)
    {
        var Proto = (SSetPointNetSc)Proto_;
        CGlobal.LoginNetSc.User.setPoint(Proto.Point);
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
    async void CouponUseNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();

        var Proto = (SCouponUseNetSc)Proto_;

        var UnitRewards = CGlobal.LoginNetSc.getUnitRewardsAndSetReward(Proto);
        if (UnitRewards.Count > 0)
            await CGlobal.curScene.pushRewardPopup(EText.SceneSetting_Coupon_Title, UnitRewards);

        CGlobal.UpdateResourcesCurrentScene();
    }
    async void CoupontFailNetSc(CKey Key_, SProto Proto_)
    {
        var Proto = (SCouponUseFailNetSc)Proto_;
        CGlobal.ProgressCircle.Deactivate();

        var textName = Proto.Ret == ERet.CouponAlreadyUsed ? EText.SceneSetting_Coupon_Duplicate : EText.SceneSetting_Coupon_Not;

        await CGlobal.curScene.pushNoticePopup(true, textName);
    }
    void BuyResourceNetSc(CKey Key_, SProto Proto_)
    {
        CGlobal.ProgressCircle.Deactivate();
        var Proto = (SBuyResourceNetSc)Proto_;

        CGlobal.LoginNetSc.User.Resources = Proto.ResourcesLeft;
        CGlobal.curScene.UpdateResources();
    }
    static void _StartArrowDodgeBattleJoin()
    {
        CGlobal.MusicStop();
        CGlobal.NetControl.Send(new SArrowDodgeBattleJoinNetCs());
        CGlobal.ProgressCircle.Activate();
    }
    public static async void ArrowDodgeBattleJoin()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        var Data = CGlobal.GetSinglePlayCountLeftTime();
        if (Data.Item1 > 0)
            _StartArrowDodgeBattleJoin();
        else
            await CGlobal.curScene.pushNoticePopup(true, EText.Global_Popup_PlayCountNotEnough);
    }
    static void _StartFlyAwayBattleJoin()
    {
        CGlobal.MusicStop();
        CGlobal.NetControl.Send(new SFlyAwayBattleJoinNetCs());
        CGlobal.ProgressCircle.Activate();
    }
    public static async void FlyAwayBattleJoin()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        var Data = CGlobal.GetSingleIslandPlayCountLeftTime();
        if (Data.Item1 > 0)
            _StartFlyAwayBattleJoin();
        else
            await CGlobal.curScene.pushNoticePopup(true, EText.Global_Popup_PlayCountNotEnough);
    }
}
