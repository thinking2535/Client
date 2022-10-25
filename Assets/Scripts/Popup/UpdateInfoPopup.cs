using bb;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UpdateInfoPopup : ModalDialog
{
    [SerializeField] Text TitleText;
    [SerializeField] Text VersionText;
    [SerializeField] Text UpdateText;
    [SerializeField] Button _ConfirmButton;
    [SerializeField] Text BtnText;
    [SerializeField] Toggle UpdateToggle;
    [SerializeField] Text _toggleText;
    protected override void Awake()
    {
        base.Awake();

        _ConfirmButton.onClick.AddListener(_confirmButtonClicked);
        TitleText.text = CGlobal.MetaData.getText(EText.Global_Popup_News);
        VersionText.text = string.Format("{0} {1}", CGlobal.VersionText(), CGlobal.MetaData.getText(EText.GlobalPopup_Button_Update));
        UpdateText.text = CGlobal.MetaData.getText(EText.Global_Popup_NewsText);
        BtnText.text = CGlobal.MetaData.getText(EText.Global_Button_Confirm);
        _toggleText.text = CGlobal.MetaData.getText(EText.GameTip_Island_TipOff);
    }
    protected override void OnDestroy()
    {
        _ConfirmButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public void init(bool showToggleButton)
    {
        UpdateToggle.gameObject.SetActive(showToggleButton);
    }
    public override Task backButtonPressed()
    {
        if (UpdateToggle.gameObject.activeSelf)
        {
            if (UpdateToggle.isOn)
                PlayerPrefs.SetInt(CGlobal.UpdateInfoKey(), 1);
            else
                PlayerPrefs.SetInt(CGlobal.UpdateInfoKey(), 0);
        }

        return base.backButtonPressed();
    }
    async void _confirmButtonClicked()
    {
        await backButtonPressed();
    }
}
