using bb;
using rso.unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class BaseScene : Scene
{
    public static float planeDistance = 15.0f;
    public static float thickness = 6.0f;
    public static float childPlaneDistance = planeDistance - thickness;
    protected DialogController _dialogController = new DialogController(CGlobal.sortingLayerPopup, childPlaneDistance);

    public void init()
    {
    }
    protected override void Awake()
    {
        base.Awake();

        var cameraClipper = camera.gameObject.AddComponent<CameraClipper>();
        cameraClipper.camera = camera;
        cameraClipper.clip(16.0f / 9.0f);
    }
    protected override async void Update()
    {
        base.Update();

        if (GlobalFunction.isBackButtonPressed())
        {
            if (!await _backButtonPressed())
            {
                var clickedButtonType = await pushAskingPopup(EText.Global_Popup_GameExit);
                if (clickedButtonType is true)
                    CGlobal.Quit();
            }
        }
    }
    protected override void OnDestroy()
    {
        // GameObject.Destroy 에 의해서 Scene 의 gameObject를 지우기 시도해도 Scene의 멤버인 _dialogController 의 소멸자가 호출 안되므로 여기서 강제로 호출하여 팝업을 제거
        _dialogController.clear();

        base.OnDestroy();
    }
    private void OnPostRender()
    {
        GL.Clear(true, true, new Color(0f, 0f, 0f), 10.0f);
    }












    public virtual void UpdateResource(EResource Resource_)
    {
    }
    public virtual void UpdateResources()
    {
    }
    // rso todo popup 자식클래스는 의 가상함수는 _dialogController.sendBackButtonPressedEvent() 가 false를 반환하면 호출하도록 할것.
    // 자식이  _dialogController.sendBackButtonPressedEvent 호출하지 않도록
    protected virtual Task<bool> _backButtonPressed()
    {
        return _dialogController.sendBackButtonPressedEvent();
    }
    public void popDialog(object returnValue = null)
    {
        _dialogController.pop(returnValue);
    }
    public Task<object> pushPopup(ModalDialog popup)
    {
        return _dialogController.push(popup, camera);
    }
    public ModalDialog peekPopup()
    {
        return _dialogController.peek() as ModalDialog;
    }
    public Task<object> pushCreatePopup()
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.createPopupPrefab);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushUpdateInfoPopup(bool showToggleButton)
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.updateInfoPopupPrefab);
        popup.init(showToggleButton);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushRewardPopup(EText popupTitle, List<CUnitReward> unitRewards)
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.rewardPopupPrefab);
        popup.init(popupTitle, unitRewards);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushScrollConfirmPopup(string title, string msg)
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.scrollConfirmPopupPrefab);
        popup.init(title, msg);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushCharacterInfoPopup(SCharacterMeta CharacterMeta_, bool isCelebration, bool isRefunded)
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.characterInfoPopupPrefab);
        popup.init(CharacterMeta_, isCelebration, isRefunded);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushSettingPopup()
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.settingPopupPrefab);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushLanguagePopup()
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.languagePopupPrefab);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushCouponPopup()
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.couponPopupPrefab);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushToolTipPopup(EText textName)
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.toolTipPopupPrefab);
        popup.init(textName);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushUpdateMessagePopup()
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.messagePopupPrefab);
        popup.init(EText.Global_Popup_Notice, true, CGlobal.MetaData.getText(EText.GlobalPopup_Text_InvalidVersion));
        popup.confirmEnabled = false;
        popup.okButton.text = CGlobal.MetaData.getText(EText.GlobalPopup_Button_Update);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushAccountLinkMessagePopup()
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.messagePopupPrefab);
        popup.init(EText.OptionScene_Invite_title, false, CGlobal.MetaData.getText(EText.Global_Popup_GuestBuy));
        popup.confirmEnabled = false;
        popup.okButton.text = CGlobal.MetaData.getText(EText.OptionScene_Invite_title);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushNoticePopup(bool isError, EText messageName, params object[] messageparameters)
    {
        return pushNoticePopup(isError, CGlobal.MetaData.getTextWithParameters(messageName, messageparameters));
    }
    public Task<object> pushNoticePopup(bool isError, string message)
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.messagePopupPrefab);
        popup.init(EText.Global_Popup_Notice, isError, message);
        popup.cancelEnabled = false;
        popup.okEnabled = false;
        popup.barrierDismissible = false;
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushAskingPopup(EText messageName, params object[] messageparameters)
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.messagePopupPrefab);
        popup.init(EText.Global_Popup_Confirm, false, CGlobal.MetaData.getTextWithParameters(messageName, messageparameters));
        popup.confirmEnabled = false;
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushBuyingResourcePopup(EResource targetResource, Int32 targetResourceMinValue, Int32 targetResourceMaxValue, ExchangeValue exchangeValue, EText messageName, params object[] messageparameters)
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.buyingResourcePopupPrefab);
        popup.init(targetResource, targetResourceMinValue, targetResourceMaxValue, exchangeValue, CGlobal.MetaData.getTextWithParameters(messageName, messageparameters));
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushAccountInfoPopup()
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.accountInfoPopupPrefab);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushChangingNicknamePopup()
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.changingNicknamePopupPrefab);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushQuestPopup()
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.questPopupPrefab);
        return _dialogController.push(popup, camera);
    }
    public Task<object> pushRankingPopup()
    {
        var popup = UnityEngine.GameObject.Instantiate(GlobalVariable.main.rankingPopupPrefab);
        return _dialogController.push(popup, camera);
    }
    public async Task<bool> checkNicknameAndPushNoticePopup(string nickName)
    {
        if (nickName.Length < global.minNicknameLength ||
            nickName.Length > global.maxNicknameLength)
        {
            await pushNoticePopup(true, EText.GlobalPopup_InvalidNickLength, global.minNicknameLength, global.maxNicknameLength);
            return false;
        }

        var ForbiddenWord = CGlobal.HaveForbiddenWord(nickName.ToLower());
        if (ForbiddenWord != "")
        {
            await pushNoticePopup(true, EText.Have_ForbiddenWord, ForbiddenWord);
            return false;
        }

        return true;
    }
}
