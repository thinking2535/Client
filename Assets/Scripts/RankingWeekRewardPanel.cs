using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingWeekRewardPanel : MonoBehaviour
{
    [SerializeField] Text _Title = null;
    [SerializeField] GameObject _RankingTrophyImage = null;
    [SerializeField] Text _RankingText = null;
    [SerializeField] GameObject _RewardItemParent = null;

    public void Init(string Title_, Int32 myRanking, SReward reward)
    {
        Int32 oneBaseRanking = myRanking + 1;

        _Title.text = Title_;
        if(myRanking < 3)
        {
            var r = _RankingTrophyImage.GetComponentInChildren<MeshRenderer>();
            r.material = Resources.Load<Material>("Material/" + CGlobal.GetRankingRewardImagePath(oneBaseRanking));
        }

        _RankingTrophyImage.SetActive(myRanking < 3);
        _RankingText.gameObject.SetActive(myRanking >= 3);
        _RankingText.text = oneBaseRanking.ToString();

        var UnitRewards = reward.GetUnitRewards();

        foreach (var i in UnitRewards)
        {
            var Panel = UnityEngine.Object.Instantiate<RankingRewardItemSet>(Resources.Load<RankingRewardItemSet>("Prefabs/UI/RankingRewardItemSet"));
            Panel.transform.SetParent(_RewardItemParent.transform);
            Panel.transform.localScale = Vector3.one;
            Panel.Init(i.GetSprite(), i.GetText());
        }
    }
}
