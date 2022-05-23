using bb;
using rso.core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopLimitedPanel : MonoBehaviour
{
    [SerializeField] Image _ItemPriceIcon = null;
    [SerializeField] Text _ItemPriceText = null;
    [SerializeField] Text _ItemTitleText = null;
    [SerializeField] GameObject _RewardItemParent = null;
    [SerializeField] Image _ItemBG = null;
    [SerializeField] GameObject _SoldOut = null;
    public SShopPackageClientMeta PackageMeta;

    public void Init(SShopPackageClientMeta PackageMeta_)
    {
        PackageMeta = PackageMeta_;
        _ItemPriceIcon.sprite = Resources.Load<Sprite>(CGlobal.GetResourcesIconFile(PackageMeta.CostType));
        _ItemPriceText.text = PackageMeta.CostValue.ToString();
        _ItemTitleText.text = CGlobal.MetaData.GetText(PackageMeta.ETextName);
        _ItemBG.sprite = Resources.Load<Sprite>("GUI/Back/" + PackageMeta.TextureName);
        LimitedItemSet LimitedItemPanel = Resources.Load<LimitedItemSet>("Prefabs/UI/complex/LimitedItemSet");
        var RewardMetas = CGlobal.MetaData.RewardItems[PackageMeta.RewardCode].RewardList;
        bool IsSoldOut = false;
        for (var i = 0; i < RewardMetas.Count; ++i)
        {
            var Panel = UnityEngine.Object.Instantiate<LimitedItemSet>(LimitedItemPanel);
            Panel.transform.SetParent(_RewardItemParent.transform);
            Panel.transform.localScale = Vector3.one;

            Panel.Init(RewardMetas[i]);
            if(RewardMetas[i].Type == ERewardType.Character)
            {
                if(CGlobal.LoginNetSc.Chars.Contains(RewardMetas[i].Data))
                {
                    IsSoldOut = true;
                }
            }
        }
        _SoldOut.SetActive(IsSoldOut);
    }
    public void OnClick()
    {
        if (!_SoldOut.activeSelf)
        {
            CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
            if (CGlobal.HaveCost(PackageMeta.CostType, PackageMeta.CostValue))
                ItemBuy();
            else
                CGlobal.ShowResourceNotEnough(PackageMeta.CostType);
        }
    }
    private void ItemBuy()
    {
        CGlobal.SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(EText.ShopScene_Popup_CheckBuy), CGlobal.MetaData.GetText(PackageMeta.ETextName)), PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type_) =>
        {
            if (type_ == PopupSystem.PopupBtnType.Ok)
            {
                CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);
                CGlobal.NetControl.Send(new SBuyPackageNetCs(PackageMeta.Code));
                AnalyticsManager.TrackingEvent(ETrackingKey.pay_package);
                AnalyticsManager.TrackingEvent(ETrackingKey.spend_ruby_shop);
            }
        });
    }
}
