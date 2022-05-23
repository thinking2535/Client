using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollConfirmPopup : MonoBehaviour
{
    [SerializeField] Text _PopupTitle = null;
    [SerializeField] Text _PopupText = null;
    [SerializeField] GameObject _PopupTextContent = null;
    [SerializeField] Text _PopupBtnText = null;

    public delegate void CallbackPopupButton();

    private CallbackPopupButton _fCallback = null;
    public void ShowPopup(string title_, string msg_, CallbackPopupButton callback_ = null)
    {
        _fCallback = callback_;
        _PopupTitle.text = title_;
        _PopupText.text = msg_;
        gameObject.SetActive(true);
        CGlobal.Sound.PlayOneShot((Int32)ESound.Popup);
        _PopupTextContent.transform.localPosition = new Vector3(0.0f, _PopupTextContent.GetComponent<RectTransform>().rect.height * -1, 0.0f);
        _PopupBtnText.text = CGlobal.MetaData.GetText(EText.Global_Button_Confirm);
    }
    public void OnClickConfirm()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        gameObject.SetActive(false);
        _fCallback?.Invoke();
    }
}
