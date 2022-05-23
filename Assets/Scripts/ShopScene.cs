using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScene : MonoBehaviour
{
    public MoneyUI MoneyUI = null;
    public GameObject GachaPopup = null;
    public GameObject ShopContent = null;
    public GameObject LimitedParent = null;
    public GameObject BalloonParent = null;
    public GameObject GoldParent = null;
    public GameObject RubyParent = null;
    public GameObject PercentPopup = null;
    public GameObject PercentContents = null;
    public GameObject GachaOkBtn = null;
    public ShopDailyPanel ShopDailyPanel = null;
    public GachaPanel GachaPanel = null;
    public GachaPanel GachaX10Panel = null;
    public Text DailyRewardTimeText = null;

    public GameObject BalloonLayer = null;
    public PopupSystem PopupSystem = null;

    public ShopPanel OriginShopPanel = null;
    public ShopPanel OriginShopPanel_Character = null;
    public ShopLimitedPanel OriginShopLimitedPanel = null;

    [SerializeField] GameObject GachaTab = null;
    [SerializeField] GameObject CommonTab = null;
    [SerializeField] Button GachaTabButton = null;
    [SerializeField] Button CommonTabButton = null;
    public void Back()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.GetScene<CSceneShop>().BackScene();
    }
    public void PercentClose()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        PercentPopup.SetActive(false);
    }
    public void PercentOpen()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.GetScene<CSceneShop>().ShowPercent(0);
    }
    public void DestroyPanel(GameObject ShopPanel_)
    {
        Destroy(ShopPanel_);
    }
    public void OnClickGachaTab()
    {
        GachaTabButton.interactable = false;
        CommonTabButton.interactable = true;
        GachaTab.SetActive(true);
        CommonTab.SetActive(false);
    }
    public void OnClickCommonTab()
    {
        GachaTab.SetActive(false);
        CommonTab.SetActive(true);
        GachaTabButton.interactable = true;
        CommonTabButton.interactable = false;
    }
}
