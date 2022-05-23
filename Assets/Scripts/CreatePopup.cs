using bb;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CreatePopup : MonoBehaviour
{
    [SerializeField] Text _PopupTitle = null;
    [SerializeField] InputField _NickName = null;
    [SerializeField] Button _PopupOk = null;

    public delegate void CallbackPopupButton(string NickName_);

    private CallbackPopupButton _fCallback = null;
    public void Show(CallbackPopupButton callback_)
    {
        _NickName.placeholder.GetComponent<Text>().text = CGlobal.MetaData.GetText(EText.Global_Input_PlaceHolder);
        _fCallback = callback_;

        _PopupTitle.text = CGlobal.MetaData.GetText(EText.Global_Popup_Notice);
        _PopupOk.gameObject.SetActive(true);
        _PopupOk.GetComponentInChildren<Text>().text = CGlobal.MetaData.GetText(EText.Global_Button_Ok);
        gameObject.SetActive(true);
    }
    public void OnClickOk()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        if (_NickName.text.Length < rso.game.global.c_NickLengthMin ||
            _NickName.text.Length > rso.game.global.c_NickLengthMax)
        {
            CGlobal.SystemPopup.ShowPopup(EText.GlobalPopup_InvalidNickLength, PopupSystem.PopupType.Confirm);
            return;
        }

        var ForbiddenWord = CGlobal.HaveForbiddenWord(_NickName.text.ToLower());
        if (ForbiddenWord != "")
        {
            CGlobal.ShowHaveForbiddenWord(ForbiddenWord);
            return;
        }

        gameObject.SetActive(false);
        _fCallback?.Invoke(_NickName.text);
    }
}
