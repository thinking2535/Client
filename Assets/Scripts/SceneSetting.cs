using bb;
using rso.core;
using rso.unity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CSceneSetting : CSceneBase
{
    private SettingScene _SettingScene = null;
    private Text _LangugeText = null;
    private LanguageButton[] _LangugeBtns = null;
    private Canvas _LangugeCanvas = null;
    private CScene _Scene = null;
    private GameObject _CouponPopup = null;
    private GameObject _AccountPopup = null;
    public CSceneSetting(CScene Scene_) :
        base("Prefabs/SettingScene", Vector3.zero, true)
    {
        _Scene = Scene_;
    }
    public override void Dispose()
    {
    }
    public override void Enter()
    {
        _SettingScene = _Object.GetComponent<SettingScene>();
        _LangugeCanvas = _SettingScene.LangugeCanvas;
        _LangugeText = _SettingScene.LangugeText;
        _LangugeBtns = _SettingScene.LangugeBtns;
        _LangugeText.text = string.Format(CGlobal.MetaData.GetText(EText.OptionScene_Language), CGlobal.MetaData.GetText(_LangugeBtns[(Int32)CGlobal.MetaData.Lang].LanguageText));
        _CouponPopup = _SettingScene.CouponPopup;
        _AccountPopup = _SettingScene.AccountPopup;
    }
    public override bool Update()
    {
        if (_Exit)
            return false;

        if (rso.unity.CBase.BackPushed())
        {
            if (CGlobal.ScrollConfirmPopup.gameObject.activeSelf)
            {
                CGlobal.ScrollConfirmPopup.OnClickConfirm();
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
            if (CGlobal.UpdateInfoPopup.gameObject.activeSelf)
            {
                CGlobal.UpdateInfoPopup.OnClickOk();
                return true;
            }
            if (_LangugeCanvas.gameObject.activeSelf)
            {
                _SettingScene.LanguageBack();
                return true;
            }
            if (_CouponPopup.activeSelf)
            {
                _SettingScene.CouponCancelClick();
                return true;
            }
            if (_AccountPopup.activeSelf)
            {
                _SettingScene.AccountLinkCloseClick();
                return true;
            }
            BackScene();
            return false;
        }

        return true;
    }
    public void BackScene()
    {
        if (_Scene != null)
            CGlobal.SceneSetNext(_Scene);
        else
            CGlobal.SceneSetNext(new CSceneLobby());
    }
}
