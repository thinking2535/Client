using bb;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupSystem : MonoBehaviour
{
    public enum PopupType
    {
        None,
        Confirm,
        CancelOk,
        GameOut,
        CostResource,
        ToolTip,
        GuestBuy,
        Max,
        Null,
    };
    public enum PopupBtnType
    {
        Ok,
        Confirm,
        Cancel,
        Max,
	    Null,
    };

    [SerializeField] Text _PopupTitle = null;
    [SerializeField] Text _PopupText = null;
    [SerializeField] Button _PopupCancel = null;
    [SerializeField] Button _PopupConfirm = null;
    [SerializeField] Button _PopupOk = null;
    [SerializeField] Text _ResourcePopupText = null;
    [SerializeField] Text _Cost = null;
    [SerializeField] Image _Icon = null;
    [SerializeField] GameObject _Normal = null;
    [SerializeField] GameObject _Resource = null;
    PopupType _Type = PopupType.None;

    public delegate void CallbackPopupButton(PopupBtnType type_);

    private CallbackPopupButton _fCallback = null;
    public void ShowGameOut()
    {
        ShowPopup(EText.Global_Popup_GameExit, PopupType.GameOut);
    }

    public void ShowPopup(string msg_, PopupType type_, CallbackPopupButton callback_, bool IsError_ = false)
    {
        _fCallback = callback_;
        ShowPopup(msg_, type_, IsError_);
    }
    public void ShowPopup(EText eText_, PopupType type_, CallbackPopupButton callback_, bool IsError_ = false)
    {
        _fCallback = callback_;
        ShowPopup(CGlobal.MetaData.GetText(eText_), type_, IsError_);
    }
    private void ShowPopup(string msg_, PopupType type_, bool IsError_ = false)
    {
        _PopupText.text = msg_;
        _ResourcePopupText.text = msg_;
        _Type = type_;

        _PopupCancel.gameObject.SetActive(false);
        _PopupOk.gameObject.SetActive(false);
        _PopupConfirm.gameObject.SetActive(false);
        _PopupCancel.GetComponentInChildren<Text>().text = CGlobal.MetaData.GetText(EText.Global_Button_Cancel);
        _PopupOk.GetComponentInChildren<Text>().text = CGlobal.MetaData.GetText(EText.Global_Button_Ok);
        _PopupConfirm.GetComponentInChildren<Text>().text = CGlobal.MetaData.GetText(EText.Global_Button_Confirm);
        _Normal.SetActive(false);
        _Resource.SetActive(false);
        gameObject.SetActive(true);

        switch (_Type)
        {
            case PopupType.Confirm:
                _PopupTitle.text = CGlobal.MetaData.GetText(EText.Global_Popup_Notice);
                _PopupConfirm.gameObject.SetActive(true);
                _Normal.SetActive(true);
                break;
            case PopupType.CancelOk:
            case PopupType.GameOut:
                _PopupTitle.text = CGlobal.MetaData.GetText(EText.Global_Popup_Confirm);
                _PopupCancel.gameObject.SetActive(true);
                _PopupOk.gameObject.SetActive(true);
                _Normal.SetActive(true);
                break;
            case PopupType.CostResource:
                _PopupTitle.text = CGlobal.MetaData.GetText(EText.Global_Popup_Confirm);
                _PopupCancel.gameObject.SetActive(true);
                _PopupOk.gameObject.SetActive(true);
                _Resource.SetActive(true);
                break;
            case PopupType.ToolTip:
                _PopupTitle.text = CGlobal.MetaData.GetText(EText.PopupTitle_Tip);
                _PopupConfirm.gameObject.SetActive(true);
                _Normal.SetActive(true);
                break;
            case PopupType.GuestBuy:
                _PopupTitle.text = CGlobal.MetaData.GetText(EText.OptionScene_Invite_title);
                _PopupCancel.gameObject.SetActive(true);
                _PopupOk.gameObject.SetActive(true);
                _PopupOk.GetComponentInChildren<Text>().text = CGlobal.MetaData.GetText(EText.OptionScene_Invite_title);
                _Normal.SetActive(true);
                break;
            default:
                _PopupConfirm.gameObject.SetActive(true);
                _Normal.SetActive(true);
                Debug.Log("Popup Type Error !!! PopupType = " + System.Convert.ToString(type_));
                break;
        }

        if(IsError_)
            CGlobal.Sound.PlayOneShot((Int32)ESound.Error);
        else
            CGlobal.Sound.PlayOneShot((Int32)ESound.Popup);
    }
    public void ShowUpdatePopup(CallbackPopupButton callback_)
    {
        ShowPopup(EText.GlobalPopup_Text_InvalidVersion, PopupType.GameOut, callback_);
        _PopupCancel.GetComponentInChildren<BalloonStarsText>().text = CGlobal.MetaData.GetText(EText.GlobalPopup_Button_Update);
        _PopupTitle.text = CGlobal.MetaData.GetText(EText.Global_Popup_Notice);
    }
    public void ShowPopup(EText eText_, PopupType type_, bool IsError_ = false)
    {
        _fCallback = null;
        ShowPopup(CGlobal.MetaData.GetText(eText_), type_, IsError_);
    }
    public void ShowPopup(EText eText_, PopupType type_, string String_, bool IsError_ = false)
    {
        _fCallback = null;
        ShowPopup(CGlobal.MetaData.GetText(eText_), type_, IsError_);
    }
    public void ShowCostResourcePopup(EText eText_, EResource Resource_, Int32 Cost_, CallbackPopupButton callback_)
    {
        ShowPopup(CGlobal.MetaData.GetText(eText_), PopupType.CostResource, callback_);
        _Icon.sprite = Resources.Load<Sprite>(CGlobal.GetResourcesIconFile(Resource_));
        _Cost.text = Cost_.ToString();
    }

    public void OnClickCancel()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        gameObject.SetActive(false);
        _fCallback?.Invoke(PopupBtnType.Cancel);
    }
    public void OnClickConfirm()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        gameObject.SetActive(false);
        _fCallback?.Invoke(PopupBtnType.Confirm);
    }
    public void OnClickOk()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (_Type == PopupType.GameOut)
        {
            CGlobal.WillClose = true;
            CGlobal.NetControl.Logout();
            rso.unity.CBase.ApplicationQuit();
            return;
        }
        gameObject.SetActive(false);
        _fCallback?.Invoke(PopupBtnType.Ok);
    }
}
