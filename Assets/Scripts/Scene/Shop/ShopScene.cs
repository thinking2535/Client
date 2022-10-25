using bb;
using rso.Base;
using rso.core;
using rso.unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ShopScene : MoneyUIScene
{
    [SerializeField] Button _backButton;
    [SerializeField] Text _title;
    [SerializeField] GameObject _contentParent;
    [SerializeField] ShopCharacterPanel _shopCharacterPanelPrefab;

    public async new void init()
    {
        base.init();

        _backButton.onClick.AddListener(_back);
        _title.text = CGlobal.MetaData.getText(EText.SceneLobby_BtnText_Shop);

        foreach (var i in CGlobal.MetaData.shopCharacters)
        {
            if (CGlobal.LoginNetSc.doesHaveCharacter(i.Code))
                continue;

            UnityEngine.Object.Instantiate(_shopCharacterPanelPrefab, _contentParent.transform).init(i);
        }

        foreach (var i in CGlobal.MetaData.shopCharacters)
        {
            if (!CGlobal.LoginNetSc.doesHaveCharacter(i.Code))
                continue;

            UnityEngine.Object.Instantiate(_shopCharacterPanelPrefab, _contentParent.transform).init(i);
        }

        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Shop);

        if (CGlobal.IsGuest && !CGlobal.IsWarnedNeedToLinkAccount)
        {
            var clickedButtonType = await pushAccountLinkMessagePopup();
            if (clickedButtonType is true)
                await CGlobal.curScene.pushSettingPopup();

            CGlobal.IsWarnedNeedToLinkAccount = true;
        }
    }
    protected override void OnDestroy()
    {
        _backButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    protected override async Task<bool> _backButtonPressed()
    {
        if (await base._backButtonPressed())
            return true;

        _back();
        return true;
    }
    void _back()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.sceneController.pop();
    }
    public bool CanBuy(TimePoint BeginTime_, TimePoint EndTime_, TimePoint Now_, Int32 Unit_, Int32 PeriodMinutesOn_)
    {
        if (BeginTime_ > Now_ || EndTime_ < Now_)
            return false;

        long Min = ((Now_ - BeginTime_).TotalMinutesLong() / Unit_) * Unit_;
        var CheckTime = BeginTime_ + TimeSpan.FromMinutes(Min) + TimeSpan.FromMinutes(PeriodMinutesOn_);

        return Now_ < CheckTime;
    }
    public override void UpdateResource(EResource Resource_)
    {
        CGlobal.MoneyUI.UpdateResource(Resource_);
    }
    public override void UpdateResources()
    {
        CGlobal.MoneyUI.UpdateResources();
    }
    public override async void ShowCharacterInfoPopup(SCharacterMeta CharacterMeta_)
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        await pushCharacterInfoPopup(CharacterMeta_, false, false);
    }
    public override void buyCharacterNetSc(Int32 characterCode)
    {
        var dialog = _dialogController.peek() as CharacterInfoPopup;
        if (dialog != null)
            dialog.buyCharacterNetSc();

        for (Int32 i = 0; i < _contentParent.transform.childCount; ++i)
        {
            var child = _contentParent.transform.GetChild(i);
            var shopCharacterPanel = child.GetComponent<ShopCharacterPanel>();

            if (shopCharacterPanel.meta.Code == characterCode)
            {
                shopCharacterPanel.isPurchased = true;
                break;
            }
        }
    }
}

