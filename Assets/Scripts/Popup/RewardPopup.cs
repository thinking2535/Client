using bb;
using Firebase.Analytics;
using rso.unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopup : ModalDialog
{
    [SerializeField] GameObject ContentParent;
    [SerializeField] Text Title;
    [SerializeField] Button _ReceiveButton;
    [SerializeField] Text ReceiveButtonText;

    List<CUnitReward> _UnitRewards;
    RewardItemSet _RewardPanel = null;

    protected override void Awake()
    {
        base.Awake();

        _ReceiveButton.onClick.AddListener(_receiveButtonClicked);
        ReceiveButtonText.text = CGlobal.MetaData.getText(EText.Global_Button_Ok);
    }
    protected override void OnDestroy()
    {
        _ReceiveButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public void init(EText PopupTitle_, List<CUnitReward> UnitRewards_)
    {
        _UnitRewards = UnitRewards_;
        Title.text = CGlobal.MetaData.getText(PopupTitle_);

        RetrieveFirstUnitReward();
    }
    public override async Task backButtonPressed()
    {
        var UnitReward = _RewardPanel.UnitReward;

        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        GameObject.Destroy(_RewardPanel.gameObject);
        _RewardPanel = null;
        await UnitReward.NotifyToCurrentScene();
        RetrieveFirstUnitReward();
    }
    async void _receiveButtonClicked()
    {
        await backButtonPressed();
    }
    public void RetrieveFirstUnitReward()
    {
        if (_UnitRewards.Count == 0)
        {
            CGlobal.curScene.popDialog();
            return;
        }

        var FirstUnitReward = _UnitRewards.First();
        _RewardPanel = FirstUnitReward.GetRewardItemSet();
        _RewardPanel.transform.SetParent(ContentParent.transform, false);
        _RewardPanel.transform.localScale = Vector3.one;
        _RewardPanel.Init(FirstUnitReward);
        _UnitRewards.RemoveAt(0);
        CGlobal.Sound.PlayOneShot((Int32)ESound.Popup);
    }
}
