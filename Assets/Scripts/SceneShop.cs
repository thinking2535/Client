using bb;
using rso.core;
using rso.unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSceneShop : CSceneBase
{
    ShopScene _ShopScene = null;
    MoneyUI _MoneyUI = null;
    GachaPopup _GachaPopup = null;
    EResource _RefundType;
    Int32 _RefundValue;
    GameObject _ShopContent = null;
    GameObject _LimitedParent = null;
    GameObject _BalloonParent = null;
    GameObject _GoldParent = null;
    GameObject _RubyParent = null;
    GameObject _PercentPopup = null;
    GameObject _PercentContents = null;
    GameObject _GachaOkBtn = null;
    ShopDailyPanel _ShopDailyPanel = null;
    GachaPanel _GachaPanel = null;
    GachaPanel _GachaX10Panel = null;
    Text _DailyRewardTimeText = null;
    GameObject _BalloonLayer = null;
    List<CharacterPercentPanel> CharacterPercentPanels = new List<CharacterPercentPanel>();
    List<ShopLimitedPanel> LimitedPanels = new List<ShopLimitedPanel>();
    ShopPanel _OriginShopPanel = null;
    ShopPanel _OriginShopPanel_Character = null;
    ShopLimitedPanel _OriginShopLimitedPanel = null;
    private CScene _Scene = null;
    private PopupSystem _PopupSystem = null;

    bool IsShopRefeash = false;
    float RefeashTime = 0.0f;

    public CSceneShop(CScene Scene_) :
        base("Prefabs/ShopScene", Vector3.zero, true)
    {
        _Scene = Scene_;
    }
    public override void Dispose()
    {
    }

    public override void Enter()
    {
        _ShopScene = _Object.GetComponent<ShopScene>();
        _MoneyUI = _ShopScene.MoneyUI;
        _MoneyUI.SetResources(CGlobal.LoginNetSc.User.Resources);
        _GachaPopup = _ShopScene.GachaPopup.GetComponent<GachaPopup>();
        _ShopContent = _ShopScene.ShopContent;
        _LimitedParent = _ShopScene.LimitedParent;
        _BalloonParent = _ShopScene.BalloonParent;
        _GoldParent = _ShopScene.GoldParent;
        _RubyParent = _ShopScene.RubyParent;
        _PercentPopup = _ShopScene.PercentPopup;
        _PercentContents = _ShopScene.PercentContents;
        _GachaOkBtn = _ShopScene.GachaOkBtn;
        _ShopDailyPanel = _ShopScene.ShopDailyPanel;
        _GachaPanel = _ShopScene.GachaPanel;
        _GachaX10Panel = _ShopScene.GachaX10Panel;
        _DailyRewardTimeText = _ShopScene.DailyRewardTimeText;
        _BalloonLayer = _ShopScene.BalloonLayer;
        _PopupSystem = _ShopScene.PopupSystem;
        _OriginShopPanel = _ShopScene.OriginShopPanel;
        _OriginShopPanel_Character = _ShopScene.OriginShopPanel_Character;
        _OriginShopLimitedPanel = _ShopScene.OriginShopLimitedPanel;

        
        _GachaPanel.Init(0, CGlobal.MetaData.GachaList[0]);
        _GachaX10Panel.Init(0, CGlobal.MetaData.GachaList[0]);

        foreach (var i in CGlobal.MetaData.ShopInGameMetas)
        {
            var Panel = UnityEngine.Object.Instantiate<ShopPanel>(_OriginShopPanel);
            Panel.transform.SetParent(_GoldParent.transform);
            Panel.transform.localScale = Vector3.one;
            Panel.transform.localPosition = new Vector3(Panel.transform.localPosition.x, Panel.transform.localPosition.y, 0.0f);

            Panel.Init(i.Value);
        }

        //foreach (var i in CGlobal.MetaData.ShopIAPMetas)
        //{
        //    var Panel = UnityEngine.Object.Instantiate<ShopPanel>(_OriginShopPanel);
        //    Panel.transform.SetParent(_RubyParent.transform);
        //    Panel.transform.localScale = Vector3.one;
        //    Panel.transform.localPosition = new Vector3(Panel.transform.localPosition.x, Panel.transform.localPosition.y, 0.0f);

        //    Panel.Init(i.Value);
        //}
        PackageList();

        _GachaPopup.gameObject.SetActive(false);
        _PercentPopup.SetActive(false);

        _ShopDailyPanel.Init();

        CGlobal.ProgressLoading.VisibleProgressLoading();
        IsShopRefeash = true;
        RefeashTime = 0.0f;
        _ShopContent.transform.localScale = Vector3.zero;
        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Shop);
        if (CGlobal.IsGuest)
        {
            _PopupSystem.ShowPopup(CGlobal.MetaData.GetText(EText.Global_Popup_GuestBuy), PopupSystem.PopupType.GuestBuy, (PopupSystem.PopupBtnType type_) =>
            {
                if (type_ == PopupSystem.PopupBtnType.Ok)
                {
                    CGlobal.SceneSetNext(new CSceneSetting(new CSceneShop(null)));
                }
            });
        }
        else
            _PopupSystem.OnClickCancel();
    }
    public void DailyTimeText()
    {
        var DelayTime = CGlobal.LoginNetSc.User.DailyRewardExpiredTime - CGlobal.GetServerTimePoint();
        _DailyRewardTimeText.text = string.Format(CGlobal.MetaData.GetText(EText.SceneShop_TimeText), DelayTime.Hours, DelayTime.Minutes, DelayTime.Seconds);
        if (DelayTime.Ticks <= 0)
        {
            CGlobal.LoginNetSc.User.DailyRewardCountLeft = CGlobal.MetaData.ShopConfigMeta.DailyRewardAdCount + CGlobal.MetaData.ShopConfigMeta.DailyRewardFreeCount;
            CGlobal.LoginNetSc.User.DailyRewardExpiredTime = CGlobal.LoginNetSc.User.DailyRewardExpiredTime + TimeSpan.FromMinutes(CGlobal.MetaData.ShopConfigMeta.DailyRewardDurationMinute);
            DailyRefresh();
        }
    }
    public void DailyRefresh()
    {
        if (CGlobal.SystemPopup.gameObject.activeSelf)
        {
            CGlobal.SystemPopup.OnClickCancel();
        }
        _ShopDailyPanel.Init();
    }
    public void PackageList()
    {
        foreach (var i in LimitedPanels)
        {
            _ShopScene.DestroyPanel(i.gameObject);
        }
        LimitedPanels.Clear();
        foreach (var i in CGlobal.MetaData.ShopPackageMetas)
        {
            var Panel = UnityEngine.Object.Instantiate<ShopLimitedPanel>(_OriginShopLimitedPanel);
            Panel.transform.SetParent(_LimitedParent.transform);
            Panel.transform.localScale = Vector3.one;
            Panel.transform.localPosition = new Vector3(Panel.transform.localPosition.x, Panel.transform.localPosition.y, 0.0f);

            Panel.Init(i.Value);
            LimitedPanels.Add(Panel);
            Panel.transform.SetAsFirstSibling();
        }
        CGlobal.ProgressLoading.VisibleProgressLoading();
        IsShopRefeash = true;
        RefeashTime = 0.0f;
        _ShopContent.transform.localScale = Vector3.zero;
    }
    public bool CanBuy(TimePoint BeginTime_, TimePoint EndTime_, TimePoint Now_, Int32 Unit_, Int32 PeriodMinutesOn_)
    {
        if (BeginTime_ > Now_ || EndTime_ < Now_)
            return false;

        TimeSpan span = Now_ - BeginTime_;
        
        long Min = (span.TotalMinutesLong() / Unit_) * Unit_;

        var CheckTime = BeginTime_ + TimeSpan.FromMinutes(Min) + TimeSpan.FromMinutes(PeriodMinutesOn_);

        if (Now_ < CheckTime)
            return true;
        else
            return false;
    }
    public void BalloonTimeText()
    {
    }
    public override bool Update()
    {
        if (_Exit)
            return false;

        DailyTimeText();

        if (_BalloonLayer.activeSelf)
            BalloonTimeText();

        if(IsShopRefeash)
        {
            RefeashTime += Time.deltaTime;
            if (RefeashTime > 0.3f)
            {
                CGlobal.ProgressLoading.InvisibleProgressLoading();
                _ShopContent.SetActive(true);
                IsShopRefeash = false;
                RefeashTime = 0.0f;
                _ShopContent.transform.localScale = Vector3.one;
            }
            else if (RefeashTime > 0.2f)
            {
                _ShopContent.SetActive(false);
            }
            else if (RefeashTime > 0.1f)
            {
                _ShopContent.SetActive(true);
            }
        }

        if (rso.unity.CBase.BackPushed())
        {
            if (CGlobal.ADManager.isShowing())
                return true;

            if (_GachaPopup.gameObject.activeSelf)
                return true;
            if (_PercentPopup.activeSelf)
            {
                _PercentPopup.SetActive(false);
                return true;
            }
            if (_MoneyUI.GetSettingPopup())
            {
                _MoneyUI.SettingPopupClose();
                return true;
            }
            if (CGlobal.RewardPopup.gameObject.activeSelf)
            {
                CGlobal.RewardPopup.OnRecive();
                return true;
            }

            if (CGlobal.SystemPopup.gameObject.activeSelf)
            {
                CGlobal.SystemPopup.OnClickCancel();
                return true;
            }
            if (_PopupSystem.gameObject.activeSelf)
            {
                _PopupSystem.OnClickCancel();
                return true;
            }
            BackScene();
            return false;
        }
        //var Now = CGlobal.GetServerTimePoint();
        //foreach (var i in CGlobal.MetaData.GetShopPackageDate())
        //{
        //    if (!CGlobal.LoginNetSc.Packages.Contains(i.PackageCode))
        //    {
        //        if (LimitedPanels.Count > 0)
        //        {
        //            bool Refresh = false;
        //            foreach (var j in LimitedPanels)
        //            {
        //                if (j.PackageMeta.Code != i.PackageCode)
        //                {
        //                    Refresh = true;
        //                    break;
        //                }
        //            }
        //            if(Refresh)
        //            {
        //                PackageList();
        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            PackageList();
        //            return true;
        //        }
        //    }
        //}
        return true;
    }

    public override void ResourcesUpdate()
    {
        _MoneyUI.SetResources(CGlobal.LoginNetSc.User.Resources);
    }
    public void GachaAnimationView(GachaPopup.EGachaType Type_, Int32 CharCode_)
    {
        _RefundType = EResource.Null;
        _RefundValue = 0;
        _GachaPopup.gameObject.SetActive(true);
        _GachaPopup.ShowGachaAnimation(Type_, CharCode_);
    }
    public void GachaAnimationView(GachaPopup.EGachaType Type_, List<Int32> CharCodeList_, List<Int32> NewCharCodeList_)
    {
        _RefundType = EResource.Null;
        _RefundValue = 0;
        _GachaPopup.gameObject.SetActive(true);
        _GachaPopup.ShowGachaAnimation(Type_, CharCodeList_, NewCharCodeList_);
    }
    public void GachaAnimationView(GachaPopup.EGachaType Type_, Int32 CharCode_, EResource RefundType_, Int32 RefundValue_)
    {
        _RefundType = RefundType_;
        _RefundValue = RefundValue_;
        _GachaPopup.gameObject.SetActive(true);
        _GachaPopup.ShowGachaAnimation(Type_, CharCode_);
    }
    public void ViewRefundPopup()
    {
        if(_RefundType != EResource.Null)
        {
            CGlobal.SystemPopup.ShowPopup(
                string.Format(
                    CGlobal.MetaData.GetText(EText.ShopScene_GachaFailedText),
                    CGlobal.GetResourcesText(_RefundType),
                    _RefundValue),
                PopupSystem.PopupType.Confirm,
                (PopupSystem.PopupBtnType type_) => {
                    _GachaPopup.OnClickOk();
                });
            _GachaOkBtn.SetActive(false);
        }
        else
        {
            _GachaOkBtn.SetActive(true);
        }
    }
    public void VisibleGachaAnimation()
    {
        _ShopContent.SetActive(false);
    }
    public void InvisibleGachaAnimation()
    {
        _ShopContent.SetActive(true);
    }
    public override void ResourcesAction(Int32[] Resources_)
    {
        _MoneyUI.ShowRewardEffect(Resources_);
    }
    public void ShowPercent(Int32 Index_)
    {
        var GachaPercentList = CGlobal.MetaData.GachaRewardList[CGlobal.MetaData.GachaList[Index_].RewardCode];
        if (CharacterPercentPanels.Count < GachaPercentList.Count)
        {
            CharacterPercentPanel CharacterPercentPanel = Resources.Load<CharacterPercentPanel>("Prefabs/UI/CharacterPercentPanel");
            Int32 MakeCount = GachaPercentList.Count - CharacterPercentPanels.Count;
            for (int i = 0; i < MakeCount; ++i)
            {
                var Panel = UnityEngine.Object.Instantiate<CharacterPercentPanel>(CharacterPercentPanel);
                Panel.transform.SetParent(_PercentContents.transform);
                Panel.transform.localScale = Vector3.one;
                Panel.transform.localPosition = new Vector3(Panel.transform.localPosition.x, Panel.transform.localPosition.y, 0.0f);
                CharacterPercentPanels.Add(Panel);
            }
        }
        foreach (var i in CharacterPercentPanels)
            i.gameObject.SetActive(false);

        Int32 Count = 0;
        foreach (var i in GachaPercentList)
        {
            CharacterPercentPanels[Count].Init(i.Key, CGlobal.MetaData.GetGachaPercent(Index_, i.Key), i.Value.Event);
            CharacterPercentPanels[Count].gameObject.SetActive(true);
            Count++;
        }
        _PercentPopup.SetActive(true);
    }
    public void BackScene()
    {
        if (_Scene != null)
            CGlobal.SceneSetNext(_Scene);
        else
            CGlobal.SceneSetNext(new CSceneLobby());
    }
}
