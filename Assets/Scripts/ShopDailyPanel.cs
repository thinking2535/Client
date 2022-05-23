using bb;
using Firebase.Analytics;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ShopDailyPanel : MonoBehaviour
{
    [SerializeField] Text _DailyStatusText = null;
    [SerializeField] Text _DailyCountText = null;
    [SerializeField] GameObject _DailyADLayer = null;
    [SerializeField] GameObject _DailyStatusLayer = null;
    [SerializeField] Text _DailyADText = null;
    public void Init()
    {
        bool IsAd = CGlobal.LoginNetSc.User.DailyRewardCountLeft <= CGlobal.MetaData.ShopConfigMeta.DailyRewardAdCount;
        _DailyCountText.text = string.Format("{0}/{1}", CGlobal.LoginNetSc.User.DailyRewardCountLeft, CGlobal.MetaData.ShopConfigMeta.DailyRewardAdCount + CGlobal.MetaData.ShopConfigMeta.DailyRewardFreeCount);

        _DailyADLayer.SetActive(IsAd);
        _DailyStatusLayer.SetActive(!IsAd);
        if(CGlobal.LoginNetSc.User.DailyRewardCountLeft > 0)
        {
            _DailyStatusText.text = CGlobal.MetaData.GetText(EText.SceneShop_DailyOpen);
            _DailyADText.text = CGlobal.MetaData.GetText(EText.SceneShop_DailyOpen);
        }
        else
        {
            _DailyStatusText.text = CGlobal.MetaData.GetText(EText.SceneShop_DailyWait);
            _DailyADText.text = CGlobal.MetaData.GetText(EText.SceneShop_DailyWait);
        }
    }
    public void OnClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if(CGlobal.LoginNetSc.User.DailyRewardCountLeft > 0)
        {
            bool IsAd = CGlobal.LoginNetSc.User.DailyRewardCountLeft <= CGlobal.MetaData.ShopConfigMeta.DailyRewardAdCount;
            if (IsAd)
            {
                CGlobal.SystemPopup.ShowPopup(CGlobal.MetaData.GetText(EText.ShopScene_Popup_CheckAdGacha), PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type_) =>
                {
                    if (type_ == PopupSystem.PopupBtnType.Ok)
                    {
                        CGlobal.ADManager.ShowAdShopDailyReward(() =>
                        {
                            var SendPacket = new SDailyRewardNetCs(true);
                            if (!CGlobal.NetControl.IsLinked(0))
                            {
                                CGlobal.ADManager.SaveDelayPacket(ADManager.EDelayRewardType.ShopDailyReward, SendPacket);
                            }
                            else
                            {
                                CGlobal.NetControl.Send(SendPacket);
                            }
                        });
                    }
                });
            }
            else
            {
                CGlobal.SystemPopup.ShowPopup(CGlobal.MetaData.GetText(EText.ShopScene_Popup_CheckFreeGacha), PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type_) =>
                {
                    if (type_ == PopupSystem.PopupBtnType.Ok)
                    {
                        CGlobal.NetControl.Send(new SDailyRewardNetCs(false));
                    }
                });
            }
        }
        else
        {
            CGlobal.SystemPopup.ShowPopup(CGlobal.MetaData.GetText(EText.SceneShop_Popup_DailyCountNot), PopupSystem.PopupType.Confirm, null);
        }
    }
}
