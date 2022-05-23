using rso.core;
using bb;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AccountScene : MonoBehaviour
{
    public GameObject AccountLoginObject = null;
    public GameObject MakeNickObject = null;

    public FirebaseLogin FirebaseLogin = null;
    [SerializeField] Text _VersionText = null;
    public Toggle TermUse = null;
    public Toggle TermPrivate = null;
    [SerializeField] GameObject _TitleImg0 = null;
    [SerializeField] GameObject _TitleImg1 = null;

    private void Start()
    {
        _VersionText.text = "Version : " + CGlobal.VersionText();
        if (CGlobal.IntroTitle == 0)
        {
            _TitleImg0.SetActive(true);
            _TitleImg1.SetActive(false);
        }
        else
        {
            _TitleImg0.SetActive(false);
            _TitleImg1.SetActive(true);
        }
    }
    public void CancelClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.SystemPopup.ShowPopup(EText.Global_Popup_Terms_Cancel, PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type_) => {
            if(type_ == PopupSystem.PopupBtnType.Ok)
            {
                CGlobal.WillClose = true;
                CGlobal.NetControl.Logout();
                rso.unity.CBase.ApplicationQuit();
                return;
            }
        });
    }
    public void LoginComplete()
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CStream Stream = new CStream();
        Stream.Push(new SUserCreateOption(new SUserLoginOption(rso.unity.CBase.GetOS()), ELanguage.English));
#if UNITY_EDITOR
        var DataPath = Application.dataPath + "/../";
#else
        var DataPath = Application.persistentDataPath + "/";
#endif
        CGlobal.NetControl.Create(CGlobal.GameIPPort, FirebaseLogin.FirebaseUID(), CGlobal.NickName, 0, Stream, DataPath + CGlobal.GameIPPort.Name + "_" + CGlobal.GameIPPort.Port.ToString() + "_" + "Data/");

        if (CGlobal.GameOption.Data.IsPush)
        {
            Firebase.Messaging.FirebaseMessaging.SubscribeAsync("Notification");
            CGlobal.PushStatusLanguageOn();
        }
        else
        {
            Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification");
            CGlobal.PushStatusLanguageOff();
        }

        CGlobal.IsGuest = FirebaseLogin.IsGuest();

        AccountLoginObject.SetActive(false);
    }
    public void SendUseAccount()
    {
        AccountLoginObject.SetActive(true);
    }
    public void SendUseAccountClose()
    {
        AccountLoginObject.SetActive(true);
    }
    public void SendGuestCreate()
    {
        if (TermCheck())
            FirebaseLogin.GuestLoginClick();
    }
    public void SendAccountClick()
    {
    }
    public void LoginError()
    {
        AccountLoginObject.SetActive(true);
    }
    public void SendAppleClick()
    {
        if (TermCheck())
            FirebaseLogin.AppleLoginClick();
    }
    public void SendGoogleClick()
    {
        if (TermCheck())
            FirebaseLogin.GoogleLoginClick();
    }
    public void SendFacebookClick()
    {
        if(TermCheck())
            FirebaseLogin.FacebookLoginClick();
    }
    private bool TermCheck()
    {
        if (!TermPrivate.isOn)
        {
            CGlobal.SystemPopup.ShowPopup(EText.Global_Popup_PrivacyPolicy_NeedAgree, PopupSystem.PopupType.Confirm);
            return false;
        }
        if (!TermUse.isOn)
        {
            CGlobal.SystemPopup.ShowPopup(EText.Global_Popup_TermsOfUse_NeedAgree, PopupSystem.PopupType.Confirm);
            return false;
        }
        return true;
    }
    public void TermUseView()
    {
        if (CGlobal.MetaData.Lang == ELanguage.Korean)
            Application.OpenURL("https://sites.google.com/stairgames.com/tos-kr");
        else
            Application.OpenURL("https://sites.google.com/stairgames.com/tos-en");
    }
    public void TermPrivateView()
    {
        if (CGlobal.MetaData.Lang == ELanguage.Korean)
            Application.OpenURL("https://sites.google.com/stairgames.com/privacy-kr");
        else
            Application.OpenURL("https://sites.google.com/stairgames.com/privacy-en");
    }
}
