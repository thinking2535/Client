using bb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRewardItem
{
    public List<SRewardMeta> RewardList = null;

    public CRewardItem(List<SRewardMeta> RewardList_)
    {
        RewardList = RewardList_;
    }

    public void Add(SRewardMeta Data_)
    {
        RewardList.Add(Data_);
    }
    public List<SRewardMeta> GetList()
    {
        return RewardList;
    }
}