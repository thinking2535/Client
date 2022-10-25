using bb;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LanguagePopup : ModalDialog
{
    [SerializeField] Button _backButton;

    protected override void Awake()
    {
        base.Awake();

        _backButton.onClick.AddListener(_backButtonClick);
    }
    protected override void OnDestroy()
    {
        _backButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public async void _backButtonClick()
    {
        await backButtonPressed();
    }
    private ELanguage _SelectLanguage = ELanguage.English;
    public async void setLanguage(LanguageButton languageButton)
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _SelectLanguage = languageButton.Language;

        if (await CGlobal.curScene.pushAskingPopup(EText.Global_Popup_LanguageChanage) is true)
        {
            CGlobal.SetLanguage(_SelectLanguage);
            if (CGlobal.GameOption.Data.IsPush)
            {
                await Firebase.Messaging.FirebaseMessaging.SubscribeAsync("Notification");
                CGlobal.PushStatusLanguageOn();
            }
            else
            {
                await Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification");
                CGlobal.PushStatusLanguageOff();
            }
            CGlobal.setLobbyScene();
        }

        // rso todo systemPopup 수정할때 _SelectLanguage 도 제거할것.
    }
}
