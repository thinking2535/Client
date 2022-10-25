using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingRewardInfoPopup : ModalDialog
{
    [SerializeField] RewardInfoPanel _rewardInfoPanelPrefab;
    [SerializeField] Text _title;
    [SerializeField] Button _closeButton;
    [SerializeField] GameObject _infoParent;

    protected override void Awake()
    {
        base.Awake();

        _closeButton.onClick.AddListener(_close);
    }
    protected override void OnDestroy()
    {
        _closeButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public void init(ERankingType rankingType)
    {
        _title.text = CGlobal.RankingTypeInfos[(Int32)rankingType].GetText();

        Int32 MinRank = 0;
        foreach (var i in CGlobal.MetaData.RankingReward[(Int32)rankingType])
        {
            var Panel = UnityEngine.Object.Instantiate(_rewardInfoPanelPrefab);
            Panel.transform.SetParent(_infoParent.transform);
            Panel.transform.localScale = Vector3.one;

            Panel.Init(MinRank, i.Key, i.Value);
            MinRank = i.Key;
        }
    }
    async void _close()
    {
        await backButtonPressed();
    }
}
