using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RankingRewardPopup : ModalDialog
{
    [SerializeField] RankingWeekRewardPanel _rankingWeekRewardPanelPrefab;
    [SerializeField] GameObject _RewardParent = null;
    [SerializeField] Button _receiveButton;

    protected override void Awake()
    {
        base.Awake();

        _receiveButton.onClick.AddListener(_receive);
    }
    protected override void OnDestroy()
    {
        _receiveButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public void init(Int32[] myRankingArray)
    {
        for (Int32 i = 0; i < myRankingArray.Length; ++i)
        {
            var myRanking = myRankingArray[i];
            if (myRanking == -1)
                continue;

            var reward = CGlobal.MetaData.RankingReward[i].Get(myRanking);

            var Panel = UnityEngine.Object.Instantiate<RankingWeekRewardPanel>(_rankingWeekRewardPanelPrefab);
            Panel.transform.SetParent(_RewardParent.transform);
            Panel.transform.localPosition = Vector3.zero;
            Panel.transform.localScale = Vector3.one;
            Panel.Init(CGlobal.RankingTypeInfos[i].GetText(), myRanking, reward.Value.Value);
        }
    }
    public override Task backButtonPressed()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.curScene.popDialog();
        return Task.CompletedTask;
    }
    async void _receive()
    {
        await backButtonPressed();
    }
}
