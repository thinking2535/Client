using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePopup : ModalDialog
{
    [SerializeField] Text _title;
    [SerializeField] public TextButton cancelButton;
    [SerializeField] public TextButton confirmButton;
    [SerializeField] public TextButton okButton;
    [SerializeField] Button _closeButton;
    [SerializeField] Button _barrierDismissibleButton;

    public bool cancelEnabled
    {
        get
        {
            return cancelButton.gameObject.activeSelf;
        }
        set
        {
            cancelButton.gameObject.SetActive(value);
        }
    }
    public bool confirmEnabled
    {
        get
        {
            return confirmButton.gameObject.activeSelf;
        }
        set
        {
            confirmButton.gameObject.SetActive(value);
        }
    }
    public bool okEnabled
    {
        get
        {
            return okButton.gameObject.activeSelf;
        }
        set
        {
            okButton.gameObject.SetActive(value);
        }
    }
    public bool barrierDismissible = true;
    protected override void Awake()
    {
        base.Awake();

        cancelButton.AddListener(_close);
        confirmButton.AddListener(_ok);
        okButton.AddListener(_ok);
        _closeButton.onClick.AddListener(_close);
        _barrierDismissibleButton.onClick.AddListener(_barrierDismiss);

        cancelButton.text = CGlobal.MetaData.getText(EText.Global_Button_Cancel);
        confirmButton.text = CGlobal.MetaData.getText(EText.Global_Button_Confirm);
        okButton.text = CGlobal.MetaData.getText(EText.Global_Button_Ok);
    }
    protected override void OnDestroy()
    {
        _barrierDismissibleButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public void init(EText titleName, bool isError)
    {
        _title.text = CGlobal.MetaData.getText(titleName);

        if (isError)
            CGlobal.Sound.PlayOneShot((Int32)ESound.Error);
        else
            CGlobal.Sound.PlayOneShot((Int32)ESound.Popup);
    }
    protected async void _close()
    {
        await backButtonPressed();
    }
    async void _barrierDismiss()
    {
        if (!barrierDismissible)
            return;

        await backButtonPressed();
    }
    protected virtual void _ok()
    {
        _okWithReturnValue(true);
    }
}
