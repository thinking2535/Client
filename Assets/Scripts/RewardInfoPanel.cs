using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardInfoPanel : MonoBehaviour
{
    [SerializeField] Text _RankingRange = null;
    [SerializeField] GameObject _RewardItemParent = null;

    public void Init(Int32 MinRank_, Int32 Rank_, SReward Reward_)
    {
        var Diff = Rank_ - MinRank_;
        if (Diff <= 1)
            _RankingRange.text = string.Format("{0}", Rank_ + 1);
        else
            _RankingRange.text = string.Format("{0}~{1}", MinRank_ + 2, Rank_ + 1);

        foreach(var i in Reward_.GetUnitRewardResources())
        {
            var Panel = UnityEngine.Object.Instantiate<RankingRewardItemSet>(Resources.Load<RankingRewardItemSet>("Prefabs/UI/RewardInfoItemSet"));
            Panel.transform.SetParent(_RewardItemParent.transform);
            Panel.transform.localScale = Vector3.one;
            Panel.Init(i.GetSprite(), i.GetText());
        }
    }
}
