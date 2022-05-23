using bb;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_IOS
using AppleAuth;
using AppleAuth.Native;
#endif
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SettingScene : MonoBehaviour
{
    [SerializeField] Image _MusicBtnOn = null;
    [SerializeField] Image _MusicBtnOff = null;
    [SerializeField] Image _SoundBtnOn = null;
    [SerializeField] Image _SoundBtnOff = null;
    [SerializeField] Image _VibeBtnOn = null;
    [SerializeField] Image _VibeBtnOff = null;
    [SerializeField] Image _PushBtnOn = null;
    [SerializeField] Image _PushBtnOff = null;
    [SerializeField] Image _PadBtnOn = null;
    [SerializeField] Image _PadBtnOff = null;
    [SerializeField] GameObject _VibeToggle = null;
    public Text LangugeText = null;
    [SerializeField] Canvas _SettingCanvas = null;
    public Canvas LangugeCanvas = null;
    public LanguageButton[] LangugeBtns = null;
    [SerializeField] FirebaseLogin _FirebaseLogin = null;
    [SerializeField] GameObject _LoginAppleBtn = null;
    [SerializeField] GameObject _LoginGoogleBtn = null;
    [SerializeField] GameObject _LoginFacebookBtn = null;
    [SerializeField] Image _LoginDisGoogle = null;
    [SerializeField] Image _LoginDisApple = null;
    [SerializeField] Image _LoginDisFacebook = null;
    [SerializeField] Text _VersionText = null;
    [SerializeField] GameObject _CouponBtn = null;
    public GameObject CouponPopup = null;
    [SerializeField] InputField _CouponInput = null;
    public GameObject AccountPopup = null;

    private ELanguage _SelectLanguage = 0;
    bool ProviderLoad = false;
#if UNITY_IOS
    private AppleAuthManager appleAuthManager;
#endif

    private void Awake()
    {
        CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);
        BtnToggle(_MusicBtnOn, _MusicBtnOff, CGlobal.GameOption.Data.IsMusic);
        BtnToggle(_SoundBtnOn, _SoundBtnOff, CGlobal.GameOption.Data.IsSound);
        BtnToggle(_VibeBtnOn, _VibeBtnOff, CGlobal.GameOption.Data.IsVibe);
        BtnToggle(_PushBtnOn, _PushBtnOff, CGlobal.GameOption.Data.IsPush);
        BtnToggle(_PadBtnOn, _PadBtnOff, CGlobal.GameOption.Data.IsPad);
        _VersionText.text = "Version : " + CGlobal.VersionText();
        _VibeToggle.SetActive(false);
        CouponPopup.SetActive(false);
        _CouponBtn.SetActive(true);
        AccountPopup.SetActive(false);
#if UNITY_IOS
        //_VibeToggle.SetActive(false);
        if(CGlobal.IsTestServer)
            _CouponBtn.SetActive(false);
#endif
    }
    public void SetLoginProvider()
    {
        if (_FirebaseLogin.IsGuest())
        {
            _LoginDisFacebook.gameObject.SetActive(true);
            _LoginFacebookBtn.SetActive(true);
#if UNITY_ANDROID
            _LoginDisGoogle.gameObject.SetActive(true);
            _LoginDisApple.gameObject.SetActive(false);
            _LoginAppleBtn.SetActive(false);
            _LoginGoogleBtn.SetActive(true);
#elif UNITY_IOS
            if (AppleAuthManager.IsCurrentPlatformSupported)
            {
                _LoginDisApple.gameObject.SetActive(true);
                _LoginAppleBtn.SetActive(true);
                var deserializer = new PayloadDeserializer();
                appleAuthManager = new AppleAuthManager(deserializer);
            }
            else
            {
                _LoginDisApple.gameObject.SetActive(false);
                _LoginAppleBtn.SetActive(false);
            }
            _LoginDisGoogle.gameObject.SetActive(true);
            _LoginGoogleBtn.SetActive(true);
#endif
        }
        else
        {
            _LoginAppleBtn.SetActive(false);
            _LoginGoogleBtn.SetActive(false);
            _LoginFacebookBtn.SetActive(false);

            _LoginDisGoogle.gameObject.SetActive(false);
            _LoginDisApple.gameObject.SetActive(false);
            _LoginDisFacebook.gameObject.SetActive(false);
            var Provider = _FirebaseLogin.GetProvider();
            if (Provider == FirebaseLogin.ELoginProvider.Google)
            {
                _LoginDisGoogle.gameObject.SetActive(true);
            }
            else if (Provider == FirebaseLogin.ELoginProvider.Facebook)
            {
                _LoginDisFacebook.gameObject.SetActive(true);
            }
            else if (Provider == FirebaseLogin.ELoginProvider.Apple)
            {
                _LoginDisApple.gameObject.SetActive(true);
            }
        }
    }
    private void Update()
    {
        if(_FirebaseLogin.InitFirebaseAuthFinish && !ProviderLoad)
        {
            // 임시 주석처리..
            //SetLoginProvider();
            ProviderLoad = true;
            CGlobal.ProgressLoading.InvisibleProgressLoading();
        }
    }
    public void MusicClick()
    {
        CGlobal.SetIsMusic(!CGlobal.GameOption.Data.IsMusic);
        BtnToggle(_MusicBtnOn, _MusicBtnOff, CGlobal.GameOption.Data.IsMusic);
    }
    public void SoundClick()
    {
        CGlobal.SetIsSound(!CGlobal.GameOption.Data.IsSound);
        BtnToggle(_SoundBtnOn, _SoundBtnOff, CGlobal.GameOption.Data.IsSound);
    }
    public void VibeClick()
    {
        CGlobal.SetIsVibe(!CGlobal.GameOption.Data.IsVibe);
        BtnToggle(_VibeBtnOn, _VibeBtnOff, CGlobal.GameOption.Data.IsVibe);
    }
    public void PushClick()
    {
        CGlobal.SetIsPush(!CGlobal.GameOption.Data.IsPush);
        BtnToggle(_PushBtnOn, _PushBtnOff, CGlobal.GameOption.Data.IsPush);
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
    }
    public void PadClick()
    {
        CGlobal.SetIsPad(!CGlobal.GameOption.Data.IsPad);
        BtnToggle(_PadBtnOn, _PadBtnOff, CGlobal.GameOption.Data.IsPad);
    }
    public void TermClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if(CGlobal.MetaData.Lang == ELanguage.Korean)
            Application.OpenURL("https://sites.google.com/stairgames.com/tos-kr");
        else
            Application.OpenURL("https://sites.google.com/stairgames.com/tos-en");
        //CGlobal.ScrollConfirmPopup.ShowPopup(CGlobal.MetaData.GetText(EText.SceneAccount_Term_TermOfUse), CGlobal.MetaData.GetText(EText.GlobalPopup_TermOfUse));
    }
    public void PrivacyClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (CGlobal.MetaData.Lang == ELanguage.Korean)
            Application.OpenURL("https://sites.google.com/stairgames.com/privacy-kr");
        else
            Application.OpenURL("https://sites.google.com/stairgames.com/privacy-en");
    }
    public void LanguageClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _SettingCanvas.gameObject.SetActive(false);
        LangugeCanvas.gameObject.SetActive(true);
    }
    public void UpdateInfo()
    {
        CGlobal.UpdateInfoPopup.ShowSettingUpdateInfoPopup();
    }
    public void HelpClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
    }
    public void AccountLinkClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        AccountPopup.SetActive(true);
    }
    public void AccountLinkCloseClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        AccountPopup.SetActive(false);
    }
    public void TutorialClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.SceneSetNext(new CSceneBattleTutorial());
    }
    public void NoticeClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        //CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);
        //StartCoroutine(NoticeView());
        CGlobal.ScrollConfirmPopup.ShowPopup(CGlobal.MetaData.GetText(EText.SceneSetting_Notice), CGlobal.MetaData.GetText(EText.SceneSetting_NoticeText));
    }
    public void CouponClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CouponPopup.SetActive(true);
        _CouponInput.text = "";
    }
    public void CouponSendClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if(_CouponInput.text.Length > 0)
        {
            CouponPopup.SetActive(false);
            CGlobal.ProgressLoading.VisibleProgressLoading();
            var SendObj = new SCouponUseNetCs(_CouponInput.text);
            CGlobal.NetControl.Send(SendObj);
        }
        else
            CGlobal.SystemPopup.ShowPopup(EText.SceneSetting_Coupon_Not, PopupSystem.PopupType.Confirm);
    }
    public void CouponCancelClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CouponPopup.SetActive(false);
    }
    IEnumerator NoticeView()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("https://any-balloon.s3.ap-northeast-2.amazonaws.com/Notice.txt");
        yield return webRequest.SendWebRequest();

        CGlobal.ProgressLoading.InvisibleProgressLoading();
        CGlobal.ScrollConfirmPopup.ShowPopup(CGlobal.MetaData.GetText(EText.SceneSetting_Notice), webRequest.downloadHandler.text);
    }
    public void Back()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel); 
        CGlobal.GetScene<CSceneSetting>().BackScene();
    }
    public void LanguageBack()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        _SettingCanvas.gameObject.SetActive(true);
        LangugeCanvas.gameObject.SetActive(false);
    }
    public void LanguageSelect(LanguageButton LangBtn_)
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _SelectLanguage = LangBtn_.Language;
        CGlobal.SystemPopup.ShowPopup(EText.Global_Popup_LanguageChanage, PopupSystem.PopupType.CancelOk, LanguageSelectCallbackButton);
    }
    void LanguageSelectCallbackButton(PopupSystem.PopupBtnType type_)
    {
        if(type_ == PopupSystem.PopupBtnType.Ok)
        {
            CGlobal.SetLanguage(_SelectLanguage);
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
            CGlobal.SceneSetNext(new CSceneLobby());
        }
    }
    private void BtnToggle(Image On_, Image Off_, bool Value_)
    {
        if (Value_)
        {
            CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
            On_.gameObject.SetActive(true);
            Off_.gameObject.SetActive(false);
        }
        else
        {
            CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
            On_.gameObject.SetActive(false);
            Off_.gameObject.SetActive(true);
        }
    }
    public void LoginComplete()
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        SetLoginProvider();
    }
    public void LoginError()
    {
    }
}
