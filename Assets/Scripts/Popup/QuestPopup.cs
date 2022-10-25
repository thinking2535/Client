using bb;
using rso.core;
using rso.net;
using rso.unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TQuestDailyCompleteInfo = System.Tuple<System.Int32, rso.core.TimePoint>;

public class QuestPopup : ModalDialog
{
    [SerializeField] Text _title;
    [SerializeField] GameObject _QuestContent = null;
    Dictionary<Byte,QuestPanel> _QuestPanels = new Dictionary<Byte, QuestPanel>();
    [SerializeField] Text _TimeText = null;
    [SerializeField] GameObject _QuestDailyParent = null;
    [SerializeField] Text _QuestText = null;
    [SerializeField] Text _QuestProgressText = null;
    [SerializeField] Image _QuestProgressBar = null;
    [SerializeField] Text _RewardText = null;
    [SerializeField] GameObject _WaitObject = null;
    [SerializeField] QuestPanel _QuestPanel = null;
    [SerializeField] TextButton _gettingDailyRewardButton;


    TimeSpan _DailyTime;
    protected override void Awake()
    {
        base.Awake();

        _gettingDailyRewardButton.AddListener(_getDailyReward);
        _gettingDailyRewardButton.text = CGlobal.MetaData.getText(EText.QuestScene_Text_GetReward);

        _WaitObject.SetActive(false);

        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Quest);

        foreach (var i in CGlobal.LoginNetSc.Quests)
            _addQuest(i.Key, i.Value);
    }
    protected override void Update()
    {
        base.Update();

        UpdateDailyQuest();
    }
    public static TQuestDailyCompleteInfo GetDailyCompleteInfo(TimePoint Now_)
    {
        Int32 NewCount = CGlobal.LoginNetSc.User.QuestDailyCompleteCount;
        TimePoint NewRefreshTime = CGlobal.LoginNetSc.User.QuestDailyCompleteRefreshTime;

        if (NewCount < CGlobal.MetaData.questConfig.Meta.dailyRequirementCount && Now_ >= NewRefreshTime) // 미달성이고, 쿨타임이 아니면
        {
            if (Now_ >= (NewRefreshTime + CGlobal.MetaData.questConfig.Meta.dailyRefreshMinutes)) // 다음 주기에 도래했으면
                NewCount = 0;
        }

        if (Now_ >= NewRefreshTime) // Item1 : 달성여부에 상관없이 가장 가까운 미래의 일일미션 시작 시간
        {
            var ElapsedDurationCount = (Now_ - NewRefreshTime).TotalMinutesLong() / CGlobal.MetaData.questConfig.Meta.dailyRefreshMinutes.value;
            NewRefreshTime += TimeSpan.FromMinutes(CGlobal.MetaData.questConfig.Meta.dailyRefreshMinutes.value * (ElapsedDurationCount + 1));
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

        var LeftDuration = (Now < Info.Item2 ? Info.Item2 - Now : (Info.Item2 + CGlobal.MetaData.questConfig.Meta.dailyRefreshMinutes) - Now);
        _TimeText.text = string.Format(CGlobal.MetaData.getText(EText.QuestScene_Text_DailyMissionTimer), LeftDuration.Hours, LeftDuration.Minutes, LeftDuration.Seconds);

        if (Info.Item1 < CGlobal.MetaData.questConfig.Meta.dailyRequirementCount && Now < CGlobal.LoginNetSc.User.QuestDailyCompleteRefreshTime) // 완료한 상태가 아니고, 쿨타임 이면
            DailyQuestWait();
        else
            DailyQuestShow();
    }
    public void updateQuest(Byte slotIndex, SQuestBase newQuest)
    {
        _QuestPanels[slotIndex].init(slotIndex, newQuest);
    }
    public void updateCount(Byte slotIndex)
    {
        _QuestPanels[slotIndex].updateCount();
    }
    public void rewardQuest(SQuestBase questBase, SQuestRewardNetSc proto)
    {
        if (questBase == null)
        {
            var Panel = _QuestPanels[proto.SlotIndex];
            _QuestPanels.Remove(proto.SlotIndex);
            GameObject.Destroy(Panel.gameObject);
        }
        else
        {
            _QuestPanels[proto.SlotIndex].init(proto.SlotIndex, questBase);
        }
    }
    public void gotQuests(Dictionary<Byte, SQuestBase> questsGot)
    {
        foreach (var i in questsGot)
            _addQuest(i.Key, i.Value);
    }
    void _addQuest(Byte slotIndex, SQuestBase questBase)
    {
        var Panel = UnityEngine.Object.Instantiate<QuestPanel>(_QuestPanel);
        Panel.transform.SetParent(_QuestContent.transform);
        Panel.transform.SetSiblingIndex(slotIndex);
        Panel.transform.localScale = Vector3.one;
        Panel.transform.localPosition = new Vector3(Panel.transform.localPosition.x, Panel.transform.localPosition.y, 0.0f);
        Panel.init(slotIndex, questBase);

        _QuestPanels.Add(slotIndex, Panel);
    }
    public void DailyQuestShow()
    {
        _QuestDailyParent.SetActive(true);
        _WaitObject.SetActive(false);
        _QuestText.text = CGlobal.MetaData.getText(EText.QuestScene_Text_DailyMission);

        var FirstUnitReward = CGlobal.MetaData.questConfig.Reward.GetFirstUnitReward();
        if (FirstUnitReward != null)
            _RewardText.text = FirstUnitReward.GetText();

        var Now = CGlobal.GetServerTimePoint();
        var Info = GetDailyCompleteInfo(Now);

        _QuestProgressText.text = string.Format("{0}/{1}", Info.Item1, CGlobal.MetaData.questConfig.Meta.dailyRequirementCount);
        _QuestProgressBar.transform.localScale = new Vector3((float)(Info.Item1) / (float)(CGlobal.MetaData.questConfig.Meta.dailyRequirementCount), 1.0f, 1.0f);
    }
    public void DailyQuestWait()
    {
        _QuestDailyParent.SetActive(false);
        _WaitObject.SetActive(true);
    }
    public async void Back()
    {
        await backButtonPressed();
    }
    async void _getDailyReward()
    {
        var Info = GetDailyCompleteInfo();
        if (Info.Item1 >= CGlobal.MetaData.questConfig.Meta.dailyRequirementCount)
            CGlobal.NetControl.Send(new SQuestDailyCompleteRewardNetCs());
        else
            await CGlobal.curScene.pushNoticePopup(true, EText.QuestScene_DailyQuest_NotComplete);
    }
}
