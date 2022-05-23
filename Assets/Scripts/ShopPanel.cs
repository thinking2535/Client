using bb;
using Firebase.Analytics;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public delegate void TShopAction();
    [SerializeField] Image _ItemImage = null;
    [SerializeField] Image _ItemPriceIcon = null;
    [SerializeField] Text _ItemPriceText = null;
    [SerializeField] Text _ItemTitleText = null;
    private TShopAction _ShopAction = null;
    SShopIAPMeta _IAPItem = null;
    SShopInGameMeta _InGameItem;
    string _Pid = "";
    public delegate void CallbackGuestPopup();

    public void Init(SShopInGameMeta Item_)
    {
        _InGameItem = Item_;

        _ItemImage.sprite = Resources.Load<Sprite>("Textures/" + _InGameItem.TextureName);
        _ItemImage.SetNativeSize();
        _ItemPriceIcon.sprite = Resources.Load<Sprite>(CGlobal.GetResourcesIconFile(Item_.CostType));
        _ItemPriceText.text = Item_.CostValue.ToString();
        _ItemTitleText.text = CGlobal.MetaData.GetText(Item_.ETextName);

        _ShopAction = () => {
            if (CGlobal.HaveCost(_InGameItem.CostType, _InGameItem.CostValue))
            {
                CGlobal.SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(EText.ShopScene_Popup_CheckBuy), CGlobal.MetaData.GetText(Item_.ETextName)), PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type_) =>
                {
                    if (type_ == PopupSystem.PopupBtnType.Ok)
                    {
                        CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);
                        CGlobal.NetControl.Send(new SBuyNetCs(_InGameItem.Code));
                        AnalyticsManager.TrackingEvent(_InGameItem.AnalyticsKey);
                        AnalyticsManager.TrackingEvent(ETrackingKey.spend_ruby_shop);
                    }
                });
            }
            else
                CGlobal.ShowResourceNotEnough(_InGameItem.CostType);
        };
    }

    public void Init(SShopIAPMeta Item_)
    {
        _IAPItem = Item_;
        _Pid = Item_.Pid;

        _ItemImage.sprite = Resources.Load<Sprite>("Textures/" + _IAPItem.TextureName);
        _ItemImage.SetNativeSize();
        _ItemTitleText.text = CGlobal.MetaData.GetText(Item_.ETextName);
        _ItemPriceIcon.gameObject.SetActive(false);
        _ItemPriceText.transform.localPosition = new Vector3(0.0f, _ItemPriceText.transform.localPosition.y, 0.0f);

        var MetaData = CGlobal.IAPManager.GetPidInfo(_Pid);
#if UNITY_EDITOR || UNITY_STANDALONE
        _ItemPriceText.text = MetaData.localizedPriceString;
#else
        _ItemTitleText.text = MetaData.localizedTitle;
        _ItemPriceText.text = MetaData.localizedPriceString;
#endif


        _ShopAction = () => {
            CGlobal.SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(EText.ShopScene_Popup_CheckBuy), CGlobal.MetaData.GetText(Item_.ETextName)), PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type_) =>
            {
                if (type_ == PopupSystem.PopupBtnType.Ok)
                {
                    CGlobal.IAPManager.BuyProductID(_Pid);
                }
            });
        };
    }
    public void OnClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _ShopAction();
    }
}
