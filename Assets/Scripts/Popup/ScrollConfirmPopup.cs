using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ScrollConfirmPopup : ModalDialog
{
    [SerializeField] Text _PopupTitle;
    [SerializeField] Text _PopupText;
    [SerializeField] GameObject _PopupTextContent;
    [SerializeField] Text _PopupBtnText;
    [SerializeField] Button _confirmButton;
    protected override void Awake()
    {
        base.Awake();

        CGlobal.Sound.PlayOneShot((Int32)ESound.Popup);
        _PopupTextContent.transform.localPosition = new Vector3(0.0f, _PopupTextContent.GetComponent<RectTransform>().rect.height * -1, 0.0f);
        _PopupBtnText.text = CGlobal.MetaData.getText(EText.Global_Button_Confirm);
        _confirmButton.onClick.AddListener(_confirmButtonClicked);
    }
    protected override void OnDestroy()
    {
        _confirmButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public void init(string title, string msg)
    {
        _PopupTitle.text = title;
        _PopupText.text = msg;
    }
    async void _confirmButtonClicked()
    {
        await backButtonPressed();
    }
}
