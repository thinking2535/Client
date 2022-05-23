using bb;
using rso.core;
using rso.net;
using rso.unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TQuestDailyCompleteInfo = System.Tuple<System.Int32, rso.core.TimePoint>;

public class PopupQuest : MonoBehaviour
{
    [SerializeField] GameObject _QuestContent = null;
    Dictionary<Byte,QuestPanel> _QuestPanels = new Dictionary<Byte, QuestPanel>();
    [SerializeField] Text _TimeText = null;
    [SerializeField] GameObject _QuestDailyParent = null;
    [SerializeField] Text _QuestText = null;
    [SerializeField] Text _QuestProgressText = null;
    [SerializeField] Image _QuestProgressBar = null;
    [SerializeField] Text _RewardText = null;
    [SerializeField] Button _RewardBtn = null;
    [SerializeField] GameObject _WaitObject = null;
    [SerializeField] GameObject _DailyRewardPopup = null;
    [SerializeField] QuestPanel _QuestPanel = null;

    [SerializeField] GameObject[] _ParentCanvases = null;

    TimeSpan _DailyTime;
    public void ShowQuestPopup()
    {
        gameObject.SetActive(true);

        _WaitObject.SetActive(false);

        foreach (var i in _QuestPanels)
            Destroy(i.Value.gameObject);
        _QuestPanels.Clear();

        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Quest);

        foreach (var i in CGlobal.LoginNetSc.Quests)
        {
            var Panel = UnityEngine.Object.Instantiate<QuestPanel>(_QuestPanel);
            Panel.transform.SetParent(_QuestContent.transform);
            Panel.transform.localScale = Vector3.one;
            Panel.transform.localPosition = new Vector3(Panel.transform.localPosition.x, Panel.transform.localPosition.y,0.0f);

            Panel.Init(CGlobal.MetaData.QuestMetas[i.Value.Code],i.Key);
            _QuestPanels.Add(i.Key, Panel);
        }
        foreach (var Obj in _ParentCanvases)
            Obj.SetActive(false);
    }

    private void Update()
    {
        UpdateDailyQuest();
    }
    public void UpdateQuestList()
    {
        foreach (var i in _QuestPanels)
            i.Value.UpdatePanel();
    }
    public static TQuestDailyCompleteInfo GetDailyCompleteInfo(TimePoint Now_)
    {
        Int32 NewCount = CGlobal.LoginNetSc.User.QuestDailyCompleteCount;
        TimePoint NewRefreshTime = CGlobal.LoginNetSc.User.QuestDailyCompleteRefreshTime;

        if (NewCount < CGlobal.MetaData.QuestDailyComplete.Meta.RequirmentCount && Now_ >= NewRefreshTime) // 미달성이고, 쿨타임이 아니면
        {
            if (Now_ >= (NewRefreshTime + CGlobal.MetaData.QuestDailyComplete.RefreshDuration)) // 다음 주기에 도래했으면
                NewCount = 0;
        }

        if (Now_ >= NewRefreshTime) // Item1 : 달성여부에 상관없이 가장 가까운 미래의 일일미션 시작 시간
        {
            var ElapsedDurationCount = (Now_ - NewRefreshTime).TotalMinutesLong() / CGlobal.MetaData.QuestDailyComplete.Meta.RefreshMinutes;
            NewRefreshTime += TimeSpan.FromMinutes(CGlobal.MetaData.QuestDailyComplete.Meta.RefreshMinutes * (ElapsedDurationCount + 1));
        }

        return new TQuestDailyCompleteInfo(NewCount, NewRefreshTime);
    }
    public static TQuestDailyCompleteInfo GetDailyCompleteInfo()
    {
        return GetDailyCompleteInfo(CGlobal.GetServerTimePoint());
    }
    public void UpdateDailyQuest()
    {
        var Now = CGlobal.GetServerTimePoint();
        var Info = GetDailyCompleteInfo(Now);

        var LeftDuration = (Now < Info.Item2 ? Info.Item2 - Now : (Info.Item2 + CGlobal.MetaData.QuestDailyComplete.RefreshDuration) - Now);
        _TimeText.text = string.Format(CGlobal.MetaData.GetText(EText.QuestScene_Text_DailyMissionTimer), LeftDuration.Hours, LeftDuration.Minutes, LeftDuration.Seconds);

        if (Info.Item1 < CGlobal.MetaData.QuestDailyComplete.Meta.RequirmentCount && Now < CGlobal.LoginNetSc.User.QuestDailyCompleteRefreshTime) // 완료한 상태가 아니고, 쿨타임 이면
            DailyQuestWait();
        else
            DailyQuestShow();
    }
    public void ChangeQuest(Byte SlotIndex_, Int32 NewQuestCode_)
    {
        if (NewQuestCode_ == 0)
            RemoveQuest(SlotIndex_);
        else
            _QuestPanels[SlotIndex_].Init(CGlobal.MetaData.QuestMetas[NewQuestCode_], SlotIndex_);
    }
    public void ChangeQuest(Byte SlotIndex_)
    {
        _QuestPanels[SlotIndex_].Init(CGlobal.MetaData.QuestMetas[CGlobal.GetUserQuestInfo(SlotIndex_).Value.Code], SlotIndex_);
    }
    public void RemoveQuest(Byte SlotIndex_)
    {
        var Panel = _QuestPanels[SlotIndex_];
        _QuestPanels.Remove(SlotIndex_);
        Panel.Destroy();
    }
    public void DailyQuestShow()
    {
        _QuestDailyParent.SetActive(true);
        _WaitObject.SetActive(false);
        _QuestText.text = CGlobal.MetaData.GetText(EText.QuestScene_Text_DailyMission);
        var Item = CGlobal.ToOneResource(CGlobal.GetReward(CGlobal.MetaData.GetRewardList(CGlobal.MetaData.QuestDailyComplete.Meta.RewardCode)));
        _RewardText.text = Item.Item2.ToString();

        var Now = CGlobal.GetServerTimePoint();
        var Info = GetDailyCompleteInfo(Now);

        _QuestProgressText.text = string.Format("{0}/{1}", Info.Item1, CGlobal.MetaData.QuestDailyComplete.Meta.RequirmentCount);
        _QuestProgressBar.transform.localScale = new Vector3((float)(Info.Item1) / (float)(CGlobal.MetaData.QuestDailyComplete.Meta.RequirmentCount), 1.0f, 1.0f);
    }
    public void DailyQuestWait()
    {
        _QuestDailyParent.SetActive(false);
        _WaitObject.SetActive(true);
    }
    public void Back()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        if (_DailyRewardPopup.activeSelf)
            _DailyRewardPopup.SetActive(false);
        else
        {
            gameObject.SetActive(false);
            foreach (var Obj in _ParentCanvases)
                Obj.SetActive(true);
        }
    }
    public void GetDailyReward()
    {
        Debug.Log("Get Daily Reward !!");
        var Info = GetDailyCompleteInfo();
        if (Info.Item1 >= CGlobal.MetaData.QuestDailyComplete.Meta.RequirmentCount)
            _DailyRewardPopup.SetActive(true);
        else
            CGlobal.SystemPopup.ShowPopup(EText.QuestScene_DailyQuest_NotComplete, PopupSystem.PopupType.Confirm, true);
    }
    public void GetDailyADReward()
    {
        Debug.Log("Get Daily Reward !!");
        CGlobal.ADManager.ShowAdQuestDailyReward(() =>
        {
            var SendPacket = new SQuestDailyCompleteRewardNetCs(true);
            if (!CGlobal.NetControl.IsLinked(0))
            {
                CGlobal.ADManager.SaveDelayPacket(ADManager.EDelayRewardType.QuestDailyReward, SendPacket);
            }
            else
            {
                _DailyRewardPopup.SetActive(false);
                CGlobal.NetControl.Send(SendPacket);
                AnalyticsManager.TrackingEvent(ETrackingKey.dailyquest_ad_view);
            }
        });
    }
    public void GetDailyRewardRecive()
    {
        Debug.Log("Get Daily Reward !!");
        _DailyRewardPopup.SetActive(false);
        CGlobal.NetControl.Send(new SQuestDailyCompleteRewardNetCs(false));
    }
}
