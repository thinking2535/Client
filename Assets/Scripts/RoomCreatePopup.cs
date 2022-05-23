using bb;
using UnityEngine;
using UnityEngine.UI;

public class RoomCreatePopup : MonoBehaviour
{
    [SerializeField] Button Btn1VS1Tab = null;
    [SerializeField] Button Btn1VS1Detail = null;

    [SerializeField] Button BtnOpen = null;
    [SerializeField] Button BtnLock = null;

    [SerializeField] GameObject PasswordParent = null;
    [SerializeField] InputField RoomPassword = null;
    [SerializeField] GameObject PasswordCheck = null;

    private EGameMode _RoomMode = EGameMode.Null;
    private bool _IsOpen = true;
    public void LockInput()
    {
        if (RoomPassword.text.Length >= 4 && RoomPassword.text.Length <= 12)
        {
            PasswordCheck.SetActive(true);
        }
        else 
        {
            PasswordCheck.SetActive(false);
        }
    }
    public void ShowRoomCreatePopup()
    {
        gameObject.SetActive(true);
        OnClickMode();
    }

    public void OnClickMode()
    {
		Btn1VS1Tab.interactable = false;
		OnClickModeDetail();
	}
    public void OnClickModeDetail()
    {
		Btn1VS1Detail.gameObject.SetActive(true);
		Btn1VS1Detail.interactable = false;

		_RoomMode = EGameMode.Solo;
	}
    public void OnClickOpen(bool IsOpen_)
    {
        _IsOpen = IsOpen_;
        BtnOpen.interactable = !IsOpen_;
        BtnLock.interactable = IsOpen_;
        PasswordParent.SetActive(!IsOpen_);
        PasswordCheck.SetActive(false);
        RoomPassword.text = "";
        RoomPassword.contentType = InputField.ContentType.Password;
    }
    public void OnClickCheck()
    {
        if (RoomPassword.contentType == InputField.ContentType.Password) 
            RoomPassword.contentType = InputField.ContentType.Standard;
        else 
            RoomPassword.contentType = InputField.ContentType.Password;

        RoomPassword.ForceLabelUpdate();
    }
    public void OnClickCreate()
    {
        string Passwaord = "";
        if(!_IsOpen)
        {
            if (RoomPassword.text.Length >= 4 && RoomPassword.text.Length <= 12)
            {
                Passwaord = RoomPassword.text;
            }
            else
            {
                CGlobal.SystemPopup.ShowPopup(EText.MultiScne_Popup_Text_Password, PopupSystem.PopupType.Confirm);
                return;
            }
        }
        CGlobal.ProgressLoading.VisibleProgressLoading();
        CGlobal.NetControl.Send<SRoomCreateNetCs>(new SRoomCreateNetCs(_RoomMode, Passwaord));
    }
    public void OnClickCancel()
    {
        gameObject.SetActive(false);
    }
}
