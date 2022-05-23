using bb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardInfoPanel : MonoBehaviour
{
    [SerializeField] Text _RankingRange = null;
    [SerializeField] GameObject _RewardItemParent = null;

    public void Init(SRankingRewardMeta Meta_)
    {
        var BeforeRanking = CGlobal.MetaData.GetRankingRewardMetaBefore(Meta_.Mode, Meta_.End);
        if(BeforeRanking != null)
        {
            var Term = Meta_.End - BeforeRanking.End;
            if (Term <= 1)
                _RankingRange.text = string.Format("{0}", Meta_.End + 1);
            else
                _RankingRange.text = string.Format("{0}~{1}", BeforeRanking.End + 2, Meta_.End + 1);
        }
        else
        {
            _RankingRange.text = string.Format("{0}", Meta_.End + 1);
        }

        RankingRewardItemSet RankingRewardItemSet = Resources.Load<RankingRewardItemSet>("Prefabs/UI/RewardInfoItemSet");
        foreach(var i in CGlobal.MetaData.GetRewardList(Meta_.RewardCode))
        {
            var Panel = UnityEngine.Object.Instantiate<RankingRewardItemSet>(RankingRewardItemSet);
            Panel.transform.SetParent(_RewardItemParent.transform);
            Panel.transform.localScale = Vector3.one;
            var Type = EResource.Gold;
            switch (i.Type)
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
