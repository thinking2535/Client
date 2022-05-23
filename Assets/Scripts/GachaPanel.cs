using Firebase.Analytics;
using bb;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class GachaPanel : MonoBehaviour
{
    public delegate void TShopAction();
    [SerializeField] Image _ItemPriceIcon = null;
    [SerializeField] Text _ItemPriceText = null;
    [SerializeField] bool _IsTen = false;
    private Int32 _Index = 0;
    SGachaClientMeta _GachaItem;
    public void Init(Int32 Index_, SGachaClientMeta GachaItem_)
    {
        _Index = Index_;
        _GachaItem = GachaItem_;

        _ItemPriceIcon.sprite = Resources.Load<Sprite>(CGlobal.GetResourcesIconFile(_GachaItem.CostResource));

        if (_IsTen)
            _ItemPriceText.text = _GachaItem.TenCostValue.ToString();
        else
            _ItemPriceText.text = _GachaItem.CostValue.ToString();
    }
    public void OnGachaClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (!CGlobal.HaveCost(_GachaItem.CostResource, _GachaItem.CostValue))
        {
            CGlobal.ShowResourceNotEnough(_GachaItem.CostResource);
            return;
        }
        //if (CGlobal.MetaData.GetHaveAllChar(_Index, CGlobal.LoginNetSc.Chars))
        //{
        //    CGlobal.SystemPopup.ShowPopup(EText.Global_Popup_AllStarCharacter, PopupSystem.PopupType.Confirm);
        //    return;
        //}

        GachaCheck();
    }
    public void OnGachaX10Click()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (!CGlobal.HaveCost(_GachaItem.TenCostResource, _GachaItem.TenCostValue))
        {
            CGlobal.ShowResourceNotEnough(_GachaItem.TenCostResource);
            return;
        }
        //if (CGlobal.MetaData.GetHaveAllChar(_Index, CGlobal.LoginNetSc.Chars))
        //{
        //    CGlobal.SystemPopup.ShowPopup(EText.Global_Popup_AllStarCharacter, PopupSystem.PopupType.Confirm);
        //    return;
        //}

        GachaCheck();
    }
    private void GachaCheck()
    {
        if (CGlobal.MetaData.GetHaveAllChar(_Index, CGlobal.LoginNetSc.Chars))
        {
            CGlobal.SystemPopup.ShowPopup(EText.Global_Popup_AllStarCharacterBuy, PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type2_) =>
            {
                if (type2_ == PopupSystem.PopupBtnType.Ok)
                {
                    GachaBuySend();
                }
            });
        }
        else
            GachaBuy();
    }
    private void GachaBuy()
    {
        CGlobal.SystemPopup.ShowPopup(EText.ShopScene_Popup_CheckGacha, PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type_) => {
            if (type_ == PopupSystem.PopupBtnType.Ok)
            {
                GachaBuySend();
            }
        });
    }
    private void GachaBuySend()
    {
        if (!_IsTen)
        {
            CGlobal.NetControl.Send(new SGachaNetCs(_Index));
            AnalyticsManager.TrackingEvent(ETrackingKey.buy_gacha);
            AnalyticsManager.TrackingEvent(ETrackingKey.pay_characterbox);
            AnalyticsManager.TrackingEvent(ETrackingKey.spend_gold_gacha);
        }
        else
        {
            CGlobal.NetControl.Send(new SGachaX10NetCs(_Index));
        }
        var Scene = CGlobal.GetScene<CSceneShop>();
        Scene.VisibleGachaAnimation();
        CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);
    }
}
