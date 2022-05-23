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

    public void Init(string Title_, Int32 RewardCode_, Int32 Ranking_)
    {
        _Title.text = Title_;
        if(Ranking_ <= 3)
        {
            var r = _RankingTrophyImage.GetComponentInChildren<MeshRenderer>();
            r.material = Resources.Load<Material>("Material/" + CGlobal.GetRankingRewardImagePath(Ranking_));
        }

        _RankingTrophyImage.SetActive(Ranking_ <= 3);
        _RankingText.gameObject.SetActive(Ranking_ > 3);
        _RankingText.text = Ranking_.ToString();

        RankingRewardItemSet RankingRewardItemSet = Resources.Load<RankingRewardItemSet>("Prefabs/UI/RankingRewardItemSet");
        var RewardItems = CGlobal.MetaData.GetRewardList(RewardCode_);
        foreach (var i in RewardItems)
        {
            var Panel = UnityEngine.Object.Instantiate<RankingRewardItemSet>(RankingRewardItemSet);
            Panel.transform.SetParent(_RewardItemParent.transform);
            Panel.transform.localScale = Vector3.one;
            var Type = EResource.Gold;
            switch(i.Type)
            {
                case ERewardType.Resource_Gold:
                    Type = EResource.Gold;
                    break;
                case ERewardType.Resource_Dia:
                    Type = EResource.Dia;
                    break;
                case ERewardType.Resource_CP:
                    Type = EResource.CP;
                    break;
            }

            Panel.Init(Type, i.Data);
        }
    }
}
