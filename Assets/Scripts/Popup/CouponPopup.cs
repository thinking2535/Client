using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CouponPopup : ModalDialog
{
    [SerializeField] Text _title;
    [SerializeField] Text _placeholder;
    [SerializeField] InputField _input;
    [SerializeField] Button _cancelButton;
    [SerializeField] Text _cancelText;
    [SerializeField] Button _sendButton;
    [SerializeField] Text _sendText;
    protected override void Awake()
    {
        base.Awake();

        _title.text = CGlobal.MetaData.getText(EText.SceneSetting_Coupon_Title);
        _placeholder.text = CGlobal.MetaData.getText(EText.SceneSetting_Coupon_Placeholder);
        _cancelText.text = CGlobal.MetaData.getText(EText.Global_Button_Cancel);
        _sendText.text = CGlobal.MetaData.getText(EText.SceneSetting_Coupon_Button_Send);

        _cancelButton.onClick.AddListener(_cancelButtonClick);
        _sendButton.onClick.AddListener(_sendButtonClick);
    }
    protected override void OnDestroy()
    {
        _sendButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    async void _cancelButtonClick()
    {
        await backButtonPressed();
    }
    public async void _sendButtonClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (_input.text.Length > 0)
        {
            CGlobal.curScene.popDialog();
            CGlobal.ProgressCircle.Activate();
            var SendObj = new SCouponUseNetCs(_input.text);
            CGlobal.NetControl.Send(SendObj);
        }
        else
        {
            await CGlobal.curScene.pushNoticePopup(true, EText.SceneSetting_Coupon_Not);
        }
    }
}
