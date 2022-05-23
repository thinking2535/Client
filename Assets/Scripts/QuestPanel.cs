using bb;
using System;
using UnityEngine;
using UnityEngine.UI;
using TQuestPair = System.Collections.Generic.KeyValuePair<System.Byte, bb.SQuestBase>;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] Text _NextCost = null;
    [SerializeField] Text _QuestText = null;
    [SerializeField] Text _QuestProgressText = null;
    [SerializeField] Image _QuestProgressBar = null;
    [SerializeField] Text _QuestRefreshTimeText = null;
    [SerializeField] GameObject _QuestActiveCanvas = null;
    [SerializeField] GameObject _QuestDeactiveCanvas = null;
    [SerializeField] Image _RewardIcon = null;
    [SerializeField] Text _RewardText = null;
    [SerializeField] Button _BtnNext = null;
    [SerializeField] Button _BtnReceive = null;
    [SerializeField] Button _BtnAD = null;
    [SerializeField] Image _QuestIcon = null;
    [SerializeField] Image _QuestDeactiveProgressBar = null;

    private Int32 _TimeCount = 0;
    private Int32 _MaxTimeCount = 0;
    private SQuestClientMeta _QuestData;
    private bool _IsClear = false;
    private Int32 _SlotIndex = 0;
    private TQuestPair? _SlotInfo = null;
    public void Init(SQuestClientMeta QuestData_, Byte SlotIndex_)
    {
        _QuestData = QuestData_;
        _SlotIndex = SlotIndex_;
        _SlotInfo = CGlobal.GetUserQuestInfo(SlotIndex_);

        _IsClear = _SlotInfo.Value.Value.CoolEndTime.Ticks > 0;

        _QuestActiveCanvas.SetActive(!_IsClear);
        _QuestDeactiveCanvas.SetActive(_IsClear);

        _BtnNext.gameObject.SetActive(false);
        _NextCost.text = CGlobal.MetaData.ConfigMeta.QuestNextCostGold.ToString();
        _QuestIcon.sprite = Resources.Load<Sprite>("Textures/Lobby_Resources/" + QuestData_.IconName);

        //퀘스트 보상은 무조건 1개로 한다.
        var RewardMeta = CGlobal.MetaData.GetRewardList(QuestData_.RewardCode)[0];
        _RewardIcon.sprite = Resources.Load<Sprite>(CGlobal.GetResourcesIconFile(RewardMeta.Type));
        _RewardText.text = RewardMeta.Data.ToString();
        if (!_IsClear)
        {
            _QuestText.text = string.Format(CGlobal.MetaData.GetText(_QuestData.ETextName), _QuestData.RequirmentCount, _QuestData.Param);
            UpdatePanel();
        }
        else
        {
            _MaxTimeCount = CGlobal.MetaData.ConfigMeta.QuestCoolMinutes * 60;
            _TimeCount = Mathf.CeilToInt((float)(_SlotInfo.Value.Value.CoolEndTime - CGlobal.GetServerTimePoint()).TotalSeconds);
            SetTimeCount();
        }
    }
    public void UpdatePanel()
    {
        _QuestProgressText.text = string.Format("{0}/{1}", _SlotInfo.Value.Value.Count, _QuestData.RequirmentCount);
        _QuestProgressBar.transform.localScale = new Vector3((float)(_SlotInfo.Value.Value.Count) / (float)(_QuestData.RequirmentCount), 1.0f, 1.0f);

        if (_QuestData.RequirmentCount == _SlotInfo.Value.Value.Count)
        {
            _BtnAD.gameObject.SetActive(false);
            _BtnReceive.gameObject.SetActive(true);
        }
        else
        {
            _BtnAD.gameObject.SetActive(true);
            _BtnReceive.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(_IsClear)
        {
            _TimeCount = Mathf.CeilToInt((float)(_SlotInfo.Value.Value.CoolEndTime - CGlobal.GetServerTimePoint()).TotalSeconds);
            SetTimeCount();
        }
    }

    private void SetTimeCount()
    {
        if (_TimeCount < 0)
        {
            _TimeCount = 0;
            _IsClear = false;
            CGlobal.NetControl.Send(new SQuestNextNetCs(_SlotInfo.Value.Key));
            CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);
        }
        string timeString = "";
        var hour = _TimeCount / 3600;
        var min = _TimeCount % 3600;
        var sec = min % 60;
        min = min / 60;

        if(hour > 0)
            timeString = string.Format("{0}:{1:D2}:{2:D2}",hour, min, sec);
        else
            timeString = string.Format("{0:D2}:{1:D2}", min, sec);

        _QuestRefreshTimeText.text = timeString;
        _QuestDeactiveProgressBar.transform.localScale = new Vector3((float)(_TimeCount) / (float)(_MaxTimeCount), 1.0f, 1.0f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void GetQuestReward()
    {
        if (_QuestData.RequirmentCount == _SlotInfo.Value.Value.Count)
        {
            CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
            CGlobal.NetControl.Send(new SQuestRewardNetCs(_SlotInfo.Value.Key));
            CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);
        }
    }
    public void ViewAD()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        Debug.Log(string.Format("Click AD"));
        CGlobal.ADManager.ShowAdQuestRefresh(()=>
        {
            var SendPacket = new SQuestNextNetCs(_SlotInfo.Value.Key);
            if (!CGlobal.NetControl.IsLinked(0))
            {
                CGlobal.ADManager.SaveDelayPacket(ADManager.EDelayRewardType.QuestRefresh, SendPacket);
            }
            else
            {
                CGlobal.NetControl.Send(SendPacket);
                AnalyticsManager.TrackingEvent(ETrackingKey.ads_quest);
            }
        });
    }
    public void NextQuest()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (CGlobal.HaveCost(EResource.Gold, CGlobal.MetaData.ConfigMeta.QuestNextCostGold))
        {
            CGlobal.NetControl.Send(new SQuestNextNetCs(_SlotInfo.Value.Key));
            CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);
        }
        else
            CGlobal.ShowResourceNotEnough(EResource.Gold);
    }
}
