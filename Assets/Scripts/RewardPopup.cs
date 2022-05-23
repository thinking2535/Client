using bb;
using Firebase.Analytics;
using rso.unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopup : MonoBehaviour
{
    [SerializeField] GameObject RewardItemParent = null;
    [SerializeField] Text RewardPopupTitle2 = null;
    [SerializeField] Text RewardPopupTitle4 = null;
    [SerializeField] Text RewardPopupBtn = null;
    [SerializeField] RewardItemSet RewardItemPanel = null;
    [SerializeField] GameObject RewardBG2 = null;
    [SerializeField] GameObject RewardBG4 = null;

    List<SRewardMeta> _RewardMetas = null;
    List<RewardItemSet> _RewardPanels = new List<RewardItemSet>();
    bool _IsAD = false;
    Int32 _CharCode = 0;
    public void ViewPopup(EText PopupTitle_, List<SRewardMeta> RewardMetas_, bool IsAD_ = false)
    {
        _IsAD = IsAD_;
        foreach (var i in _RewardPanels)
        {
            i.gameObject.SetActive(false);
            Destroy(i.gameObject);
        }
        _RewardPanels.Clear();

        _RewardMetas = RewardMetas_;

        _CharCode = -1;

        if(_RewardMetas.Count <= 2)
        {
            RewardBG2.SetActive(true);
            RewardBG4.SetActive(false);
        }
        else
        {
            RewardBG2.SetActive(false);
            RewardBG4.SetActive(true);
        }

        RewardPopupTitle2.text = CGlobal.MetaData.GetText(PopupTitle_);
        RewardPopupTitle4.text = CGlobal.MetaData.GetText(PopupTitle_);

        for (var i = 0; i < _RewardMetas.Count; ++i)
        {
            var Panel = UnityEngine.Object.Instantiate<RewardItemSet>(RewardItemPanel);
            Panel.transform.SetParent(RewardItemParent.transform);
            Panel.transform.localScale = Vector3.one;

            Panel.Init(_RewardMetas[i],IsAD_);
            _RewardPanels.Add(Panel);
            if (_RewardMetas[i].Type == ERewardType.Character)
            {
                if(!CGlobal.LoginNetSc.Chars.Contains(_RewardMetas[i].Data))
                    _CharCode = _RewardMetas[i].Data;
            }
        }
        gameObject.SetActive(true);
        CGlobal.Sound.PlayOneShot((Int32)ESound.Popup);
        RewardPopupBtn.text = CGlobal.MetaData.GetText(EText.Global_Button_Receive);
    }
    public void OnRecive()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        foreach (var i in _RewardPanels)
        {
            i.gameObject.SetActive(false);
            Destroy(i.gameObject);
        }
        _RewardPanels.Clear();
        gameObject.SetActive(false);
        var Resources = CGlobal.GetReward(_RewardMetas);

        if(_IsAD)
            Resources.Add(Resources);

        if(Resources[(Int32)EResource.Dia] > 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.earn_ruby_reward);
        }
        else if (Resources[(Int32)EResource.CP] > 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.earn_keycoin);
        }
        var Scene = CGlobal.GetScene<CSceneBase>();
        Scene.ResourcesAction(Resources);
        var SceneRank = Scene as CSceneRankingReward;
        if (SceneRank == null)
            return;
        SceneRank.SetRewardList();
        if(_CharCode != -1)
            SceneRank.GetCharacter(_CharCode);
    }
}
