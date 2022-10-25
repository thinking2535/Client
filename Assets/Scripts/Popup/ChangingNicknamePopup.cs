using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingNicknamePopup : ModalDialog
{
    [SerializeField] Text _ChangeDescription = null;
    [SerializeField] GameObject _PriceBG = null;
    [SerializeField] Image _PriceType = null;
    [SerializeField] Text _PriceText = null;
    [SerializeField] InputField _inputField = null;
    [SerializeField] Button _cancelButton;
    [SerializeField] Button _changeButton;

    protected override void Awake()
    {
        base.Awake();

        _cancelButton.onClick.AddListener(_cancel);
        _changeButton.onClick.AddListener(_change);

        _PriceType.sprite = CGlobal.GetResourceSprite(CGlobal.MetaData.ConfigMeta.ChangeNickCostType);
        _PriceText.text = CGlobal.MetaData.ConfigMeta.ChangeNickCostValue.ToString();

        bool isFree = CGlobal.LoginNetSc.User.ChangeNickFreeCount > 0;
        _ChangeDescription.gameObject.SetActive(isFree);
        _PriceBG.SetActive(!isFree);
    }
    protected override void OnDestroy()
    {
        _changeButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public async void _cancel()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        await backButtonPressed();
    }
    public async void _change()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (_inputField.text.Equals(CGlobal.NickName))
        {
            await CGlobal.curScene.pushNoticePopup(true, EText.GlobalPopup_SameNick);
            return;
        }

        if (!await CGlobal.curScene.checkNicknameAndPushNoticePopup(_inputField.text))
            return;

        if (CGlobal.LoginNetSc.User.ChangeNickFreeCount <= 0)
        {
            if (!CGlobal.doesHaveCost(CGlobal.MetaData.ConfigMeta.ChangeNickCostType, CGlobal.MetaData.ConfigMeta.ChangeNickCostValue))
            {
                await GlobalFunction.ShowResourceNotEnough(CGlobal.MetaData.ConfigMeta.ChangeNickCostType);
                return;
            }
        }

        CGlobal.ProgressCircle.Activate();
        CGlobal.NetControl.Send(new SChangeNickNetCs(_inputField.text));
    }
}
