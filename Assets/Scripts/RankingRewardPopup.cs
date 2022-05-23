using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingRewardPopup : MonoBehaviour
{
    [SerializeField] GameObject _RewardParent = null;
    [SerializeField] List<GameObject> _ControlParent = null;
    List<GameObject> _RewardPanelList = new List<GameObject>();
    Dictionary<CGlobal.RankingType, Int32> _RewardCodes = null;

    public void Init(Dictionary<CGlobal.RankingType, Int32> RewardCodes_, Dictionary<CGlobal.RankingType, Int32> RankingList_)
    {
        _RewardCodes = RewardCodes_;
        foreach (var i in _RewardPanelList)
            Destroy(i.gameObject);
        _RewardPanelList.Clear();

        RankingWeekRewardPanel RankingWeekRewardPanel = Resources.Load<RankingWeekRewardPanel>("Prefabs/UI/RankingWeekRewardPanel");
        foreach(var Reward in RewardCodes_)
        {
            var Panel = UnityEngine.Object.Instantiate<RankingWeekRewardPanel>(RankingWeekRewardPanel);
            Panel.transform.SetParent(_RewardParent.transform);
            Panel.transform.localPosition = Vector3.zero;
            Panel.transform.localScale = Vector3.one;
            Int32 OutData = 0;
            RankingList_.TryGetValue(Reward.Key, out OutData);
            if (Reward.Key == CGlobal.RankingType.Multi)
                Panel.Init(CGlobal.MetaData.GetText(EText.RankingScene_Multi), Reward.Value, OutData + 1);
            else if (Reward.Key == CGlobal.RankingType.Single)
                Panel.Init(CGlobal.MetaData.GetText(EText.LobbyScene_Dodge), Reward.Value, OutData + 1);
            else if (Reward.Key == CGlobal.RankingType.Island)
                Panel.Init(CGlobal.MetaData.GetText(EText.LobbyScene_FlyAway), Reward.Value, OutData + 1);
            _RewardPanelList.Add(Panel.gameObject);
        }
        foreach(var i in _ControlParent)
        {
            i.SetActive(false);
        }
    }

    public void OnClickReceive()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        Debug.Log("Receive Week Ranking Reward !!!");
        foreach (var i in _ControlParent)
        {
            i.SetActive(true);
        }
        gameObject.SetActive(false);

        var AddResource = new Int32[(Int32)EResource.Max];
        foreach (var Reward in _RewardCodes)
        {
            var RewardItems = CGlobal.MetaData.GetRewardList(Reward.Value);
            foreach(var i in RewardItems)
            {
                if (i.Type == ERewardType.Resource_CP)
                    AddResource[(Int32)EResource.CP] += i.Data;
                if (i.Type == ERewardType.Resource_Gold)
                    AddResource[(Int32)EResource.Gold] += i.Data;
                if (i.Type == ERewardType.Resource_Dia)
                    AddResource[(Int32)EResource.Dia] += i.Data;
            }
        }
        var Scene = CGlobal.GetScene<CSceneBase>();
        Scene.ResourcesAction(AddResource);
    }
}
