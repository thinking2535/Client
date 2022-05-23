using rso.core;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Advertisements;
//using GoogleMobileAds.Api;

public class ADManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_ANDROID
    string gameId = "3854359";
#elif UNITY_IOS
    string gameId = "3854358";
#else
    string gameId = "3854359";
#endif
    string PlacementID_QuestRefresh = "QUEST_refresh";
    string PlacementID_QuestDailyReward = "QUEST_dailyreward";
    string PlacementID_ShopDailyReward = "Shop_daily_reward";
    string PlacementID_DodgeReward = "Dodge_Reward";
    string PlacementID_IslandReward = "FlyAway_Reward";

    Dictionary<string, string> GoogleAdsKeyList = new Dictionary<string, string>();

    private SProto _SendPacket = null;
    private EDelayRewardType _SendPacketType = EDelayRewardType.None;
    public enum EDelayRewardType
    {
        None,
        QuestRefresh,
        QuestDailyReward,
        ShopDailyReward, 
        DodgeReward,
        IslandReward
    };

    public delegate void CallbackADComplete();

    private CallbackADComplete _fCallback = null;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, Debug.isDebugBuild);
#if UNITY_IOS
        if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
        }  
#endif
    }
    public void LoadAdQuestRefresh()
    {
        Advertisement.Load(PlacementID_QuestRefresh);
    }
    public void LoadAdQuestDailyReward()
    {
        Advertisement.Load(PlacementID_QuestDailyReward);
    }
    public void LoadAdShopDailyReward()
    {
        Advertisement.Load(PlacementID_ShopDailyReward);
    }
    public void LoadAdDodgeReward()
    {
        Advertisement.Load(PlacementID_DodgeReward);
    }
    public void LoadAdIslandReward()
    {
        Advertisement.Load(PlacementID_IslandReward);
    }
    public void ShowAdQuestRefresh(CallbackADComplete Callback_)
    {
        _fCallback = Callback_;
            Advertisement.Show(PlacementID_QuestRefresh);
    }
    public void ShowAdQuestDailyReward(CallbackADComplete Callback_)
    {
        _fCallback = Callback_;
            Advertisement.Show(PlacementID_QuestDailyReward);
    }
    public void ShowAdShopDailyReward(CallbackADComplete Callback_)
    {
        _fCallback = Callback_;
            Advertisement.Show(PlacementID_ShopDailyReward);
    }
    public void ShowAdDodgeReward(CallbackADComplete Callback_)
    {
        _fCallback = Callback_;
            Advertisement.Show(PlacementID_DodgeReward);
    }
    public void ShowAdIslandReward(CallbackADComplete Callback_)
    {
        _fCallback = Callback_;
            Advertisement.Show(PlacementID_IslandReward);
    }
    public bool IsReadyQuestRefresh()
    {
        return Advertisement.IsReady(PlacementID_QuestRefresh);
    }
    public bool IsReadyQuestDailyReward()
    {
        return Advertisement.IsReady(PlacementID_QuestDailyReward);
    }
    public bool IsReadyShopDailyReward()
    {
        return Advertisement.IsReady(PlacementID_ShopDailyReward);
    }
    public bool IsReadyDodgeReward()
    {
        return Advertisement.IsReady(PlacementID_DodgeReward);
    }
    public bool IsReadyIslandReward()
    {
        return Advertisement.IsReady(PlacementID_IslandReward);
    }
    public bool isShowing()
    {
        return Advertisement.isShowing;
    }
    public void SaveDelayPacket(EDelayRewardType Type_, SProto Packet_)
    {
        _SendPacket = Packet_;
        _SendPacketType = Type_;
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ready AD ID = "+placementId);
    }

    public void OnUnityAdsDidError()
    {
        CGlobal.SystemPopup.ShowPopup(CGlobal.MetaData.GetText(EText.GlobalPopup_Text_AdFailed), PopupSystem.PopupType.Confirm, null, true);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Start AD ID = " + placementId);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log("Finish AD ID = " + placementId);
        Debug.Log("Finish AD Result = " + showResult.ToString());
        switch(showResult)
        {
            case ShowResult.Finished:
                _fCallback?.Invoke();
                break;
            case ShowResult.Failed:
                LoadAdQuestRefresh();
                LoadAdQuestDailyReward();
                LoadAdShopDailyReward();
                LoadAdDodgeReward();
                LoadAdIslandReward();

                OnUnityAdsDidError();
                break;
            case ShowResult.Skipped:
            default:
                break;
        }
    }
    public void OnUnityAdsDidError(string message)
    {
        OnUnityAdsDidError();
    }

    internal void SendDelayPacket()
    {
        var AddResource = new Int32[(Int32)EResource.Max];
        var RewardMetars = new System.Collections.Generic.List<SRewardMeta>();
        switch (_SendPacketType)
        {
            case EDelayRewardType.QuestRefresh:
                var SQuestNextNetCs = (SQuestNextNetCs)_SendPacket;
                CGlobal.NetControl.Send(SQuestNextNetCs);
                CGlobal.SystemPopup.ShowPopup(EText.Popup_QuestAdChange, PopupSystem.PopupType.Confirm);
                break;
            case EDelayRewardType.QuestDailyReward:
                var SQuestDailyCompleteRewardNetCs = (SQuestDailyCompleteRewardNetCs)_SendPacket;
                CGlobal.NetControl.Send(SQuestDailyCompleteRewardNetCs);
                break;
            case EDelayRewardType.ShopDailyReward:
                var SDailyRewardNetCs = (SDailyRewardNetCs)_SendPacket;
                CGlobal.NetControl.Send(SDailyRewardNetCs);
                break;
            case EDelayRewardType.DodgeReward:
                var SSingleEndNetCs = (SSingleEndNetCs)_SendPacket;
                CGlobal.NetControl.Send(SSingleEndNetCs);
                AddResource.AddResource((Int32)EResource.Gold, SSingleEndNetCs.GoldAdded);
                RewardMetars.Add(new SRewardMeta(ERewardType.Resource_Gold, AddResource[(Int32)EResource.Gold]));
                CGlobal.RewardPopup.ViewPopup(EText.Popup_DodgeAdReward, RewardMetars, false);
                break;
            case EDelayRewardType.IslandReward:
                var SIslandEndNetCs = (SIslandEndNetCs)_SendPacket;
                CGlobal.NetControl.Send(SIslandEndNetCs);
                AddResource.AddResource((Int32)EResource.Gold, SIslandEndNetCs.GoldAdded);
                RewardMetars.Add(new SRewardMeta(ERewardType.Resource_Gold, AddResource[(Int32)EResource.Gold]));
                CGlobal.RewardPopup.ViewPopup(EText.Popup_IslandAdReward, RewardMetars, false);
                break;
        }
        _SendPacket = null;
        _SendPacketType = EDelayRewardType.None;
    }
}
