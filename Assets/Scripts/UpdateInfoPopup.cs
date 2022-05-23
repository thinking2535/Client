using bb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateInfoPopup : MonoBehaviour
{
    [SerializeField] Text TitleText = null;
    [SerializeField] Text VersionText = null;
    [SerializeField] Text UpdateText = null;
    [SerializeField] Text BtnText = null;
    [SerializeField] Image UpdateImage = null;
    [SerializeField] Toggle UpdateToggle = null;
    public void ShowUpdateInfoPopup()
    {
        CGlobal.IsUpdateInfoPopup = true;
        SettingPopup();
    }
    public void ShowSettingUpdateInfoPopup()
    {
        SettingPopup();
        UpdateToggle.gameObject.SetActive(false);
    }
    private void SettingPopup()
    {
        gameObject.SetActive(true);
        TitleText.text = CGlobal.MetaData.GetText(EText.Global_Popup_News);
        VersionText.text = string.Format("{0} {1}", CGlobal.VersionText(), CGlobal.MetaData.GetText(EText.GlobalPopup_Button_Update));
        UpdateText.text = CGlobal.MetaData.GetText(EText.Global_Popup_NewsText);
        BtnText.text = CGlobal.MetaData.GetText(EText.Global_Button_Confirm);
    }
    public void OnClickOk()
    {
        if(UpdateToggle.gameObject.activeSelf)
        {
            if (UpdateToggle.isOn)
                PlayerPrefs.SetInt(CGlobal.UpdateInfoKey(), 1);
            else
                PlayerPrefs.SetInt(CGlobal.UpdateInfoKey(), 0);
        }
        gameObject.SetActive(false);
    }
}
