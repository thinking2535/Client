using bb;
using rso.core;
using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    struct _QuestInfo
    {
    }

    [SerializeField] Text _QuestText = null;
    [SerializeField] Text _QuestProgressText = null;
    [SerializeField] Image _QuestProgressBar = null;
    [SerializeField] Text _QuestRefreshTimeText = null;
    [SerializeField] GameObject _QuestActiveCanvas = null;
    [SerializeField] GameObject _QuestDeactiveCanvas = null;
    [SerializeField] Image _RewardIcon = null;
    [SerializeField] Text _RewardText = null;
    [SerializeField] Button _receiveButton;
    [SerializeField] Button _disabledButton;
    [SerializeField] Image _QuestIcon = null;
    [SerializeField] Image _QuestDeactiveProgressBar = null;

    private Quest _questData;
    Byte _slotIndex;
    SQuestBase _questBase;
    Int32 _secondLeft;

    bool _activated => _secondLeft == 0;
    void Awake()
    {
        _receiveButton.onClick.AddListener(_receive);
    }
    void Update()
    {
        if (!_activated)
            _updateTimeLeft();
    }
    private void OnDestroy()
    {
        _receiveButton.onClick.RemoveAllListeners();
    }
    public void init(Byte slotIndex, SQuestBase newQuest)
    {
        _slotIndex = slotIndex;
        _questBase = newQuest;
        _questData = CGlobal.MetaData.questDatas[newQuest.Code];

        _updateTimeLeft();
        _updateCanvas();
        _updateSecondLeftText();
        updateCount();

        _QuestText.text = _questData.text;
        _QuestIcon.sprite = Resources.Load<Sprite>("Textures/Lobby_Resources/" + _questData.iconName);

        //퀘스트 보상은 무조건 1개로 한다.
        var UnitReward = _questData.reward.GetFirstUnitReward();
        if (UnitReward != null)
        {
            _RewardIcon.sprite = UnitReward.GetSprite();
            _RewardText.text = UnitReward.GetText();
        }
    }
    void _updateTimeLeft()
    {
        var secondLeft = Mathf.CeilToInt((float)(_questBase.CoolEndTime - CGlobal.GetServerTimePoint()).TotalSeconds); ;
        if (secondLeft < 0)
            secondLeft = 0;

        if (secondLeft == _secondLeft)
            return;

        _secondLeft = secondLeft;

        if (_activated)
            _updateCanvas();
        else
            _updateSecondLeftText();
    }
    void _updateCanvas()
    {
        _QuestActiveCanvas.SetActive(_activated);
        _QuestDeactiveCanvas.SetActive(!_activated);
    }
    void _updateSecondLeftText()
    {
        string timeString = "";
        var hour = _secondLeft / 3600;
        var min = _secondLeft % 3600;
        var sec = min % 60;
        min = min / 60;

        if (hour > 0)
            timeString = string.Format("{0}:{1:D2}:{2:D2}", hour, min, sec);
        else
            timeString = string.Format("{0:D2}:{1:D2}", min, sec);

        _QuestRefreshTimeText.text = timeString;
        _QuestDeactiveProgressBar.transform.localScale = new Vector3((float)(_secondLeft) / (float)(CGlobal.MetaData.questConfig.Meta.dailyRefreshMinutes.value * 60), 1.0f, 1.0f);
    }
    public void updateCount()
    {
        _QuestProgressText.text = string.Format("{0}/{1}", _questBase.Count, _questData.completeCount);
        _QuestProgressBar.transform.localScale = new Vector3((float)(_questBase.Count) / (float)(_questData.completeCount), 1.0f, 1.0f);

        var isCompleted = _questBase.Count >= _questData.completeCount;

        _disabledButton.gameObject.SetActive(!isCompleted);
        _receiveButton.gameObject.SetActive(isCompleted);
    }
    public void _receive()
    {
        if (_questData.completeCount >= _questBase.Count)
        {
            CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
            CGlobal.NetControl.Send(new SQuestRewardNetCs(_slotIndex));
            CGlobal.ProgressCircle.Activate();
        }
    }
}