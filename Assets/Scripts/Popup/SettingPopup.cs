using bb;
using rso.unity;
using System;
using System.Collections;
using System.Threading.Tasks;
#if UNITY_IOS
using AppleAuth;
using AppleAuth.Native;
#endif
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SettingPopup : ModalDialog
{
    [SerializeField] Button _BackButton;
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
    [SerializeField] Text _LangugeText = null;
    [SerializeField] FirebaseLogin _FirebaseLogin = null;
    [SerializeField] GameObject _LoginAppleBtn = null;
    [SerializeField] GameObject _LoginGoogleBtn = null;
    [SerializeField] GameObject _LoginFacebookBtn = null;
    [SerializeField] Image _LoginDisGoogle = null;
    [SerializeField] Image _LoginDisApple = null;
    [SerializeField] Image _LoginDisFacebook = null;
    [SerializeField] Text _VersionText = null;
    [SerializeField] GameObject _CouponBtn = null;
    [SerializeField] GameObject _AccountPopup = null;

    bool ProviderLoad = false;
#if UNITY_IOS
    private AppleAuthManager appleAuthManager;
#endif

    protected override void Awake()
    {
        base.Awake();

        _BackButton.onClick.AddListener(Back);
        _LangugeText.text = CGlobal.MetaData.getCurrentLanguageText();

        // rso todo ProgressCircle 설정팝업은 파이어 베이스 활성 여부에 상관없이 즉시 가용하도록 해야 하므로 ProgressCircle 돌리지 말것.
        // 설정 페이지 갔다가 즉시 빠져 나올경우 Circle 없앨 수 없음.
        CGlobal.ProgressCircle.Activate();

        BtnToggle(_MusicBtnOn, _MusicBtnOff, CGlobal.GameOption.Data.IsMusic);
        BtnToggle(_SoundBtnOn, _SoundBtnOff, CGlobal.GameOption.Data.IsSound);
        BtnToggle(_VibeBtnOn, _VibeBtnOff, CGlobal.GameOption.Data.IsVibe);
        BtnToggle(_PushBtnOn, _PushBtnOff, CGlobal.GameOption.Data.IsPush);
        BtnToggle(_PadBtnOn, _PadBtnOff, CGlobal.GameOption.Data.IsPad);
        _VersionText.text = "Version : " + CGlobal.VersionText();
        _VibeToggle.SetActive(false);
        _AccountPopup.SetActive(false);
        _CouponBtn.SetActive(false);
    }
    protected override void Update()
    {
        base.Update();

        if (_FirebaseLogin.InitFirebaseAuthFinish && !ProviderLoad)
        {
            // 임시 주석처리..
            //SetLoginProvider();
            ProviderLoad = true;
            CGlobal.ProgressCircle.Deactivate(); // rso todo ProgressCircle Firebase 모듈 및 위젯을 여기서 활성화 시키는 방식으로 변경하고 이 라인은 제거할것.
        }
        else
        {

        }
    }
    protected override void OnDestroy()
    {
        _BackButton.onClick.RemoveAllListeners();

        base.OnDestroy();
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
        if(CGlobal.GameOption.Data.Language == ELanguage.Korean)
            Application.OpenURL("https://sites.google.com/stairgames.com/tos-kr");
        else
            Application.OpenURL("https://sites.google.com/stairgames.com/tos-en");
        //GlobalVariable.main.ScrollConfirmPopup.ShowPopup(CGlobal.MetaData.GetText(EText.SceneAccount_Term_TermOfUse), CGlobal.MetaData.GetText(EText.GlobalPopup_TermOfUse));
    }
    public void PrivacyClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (CGlobal.GameOption.Data.Language == ELanguage.Korean)
            Application.OpenURL("https://sites.google.com/stairgames.com/privacy-kr");
        else
            Application.OpenURL("https://sites.google.com/stairgames.com/privacy-en");
    }
    public async void LanguageClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        await CGlobal.curScene.pushLanguagePopup();
    }
    public async void UpdateInfo()
    {
        await CGlobal.curScene.pushUpdateInfoPopup(false);
    }
    public void HelpClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
    }
    public void AccountLinkClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _AccountPopup.SetActive(true);
    }
    public void TutorialClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.pushBattleTutorialScene();
    }
    public async void NoticeClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        await CGlobal.curScene.pushScrollConfirmPopup(CGlobal.MetaData.getText(EText.SceneSetting_Notice), CGlobal.MetaData.getText(EText.SceneSetting_NoticeText));
    }
    public async void CouponClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        await CGlobal.curScene.pushCouponPopup();
    }
    public async void Back()
    {
        await backButtonPressed();
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
        CGlobal.ProgressCircle.Deactivate();
        SetLoginProvider();
    }
    public void LoginError()
    {
    }
}
