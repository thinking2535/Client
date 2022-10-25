// rso todo account scene
//using rso.core;
//using bb;
//using System;
//using UnityEngine;
//using UnityEngine.UI;
//using rso.unity;

//public class AccountScene : NoMoneyUIScene
//{
//    CAccountScene _AccountScene;

//    bool _IsFirebaseFirst = true;
//    [SerializeField] GameObject _AccountLoginObject = null;
//    [SerializeField] GameObject _MakeNickObject = null;
//    [SerializeField] FirebaseLogin _FirebaseLogin = null;
//    [SerializeField] Text _VersionText = null;
//    [SerializeField] Toggle _TermUse = null;
//    [SerializeField] Toggle _TermPrivate = null;
//    [SerializeField] GameObject _TitleImg0 = null;
//    [SerializeField] GameObject _TitleImg1 = null;

//    void Start()
//    {
//        _VersionText.text = "Version : " + CGlobal.VersionText();
//        if (CGlobal.IntroTitle == 0)
//        {
//            _TitleImg0.SetActive(true);
//            _TitleImg1.SetActive(false);
//        }
//        else
//        {
//            _TitleImg0.SetActive(false);
//            _TitleImg1.SetActive(true);
//        }
//    }
//    void Update()
//    {
//        if (_IsFirebaseFirst)
//        {
//            if (_FirebaseLogin.InitFirebaseAuthFinish)
//            {
//                if (_FirebaseLogin.AutoLoginCheck())
//                {
//                    var Stream = new CStream();
//                    Stream.Push(new SUserLoginOption(CUnity.GetOS()));
//#if UNITY_EDITOR || UNITY_STANDALONE
//                    var DataPath = Application.dataPath + "/../";
//#else
//                    var DataPath = Application.persistentDataPath + "/";
//#endif
//                    CGlobal.ProgressCircle.Activate();
//                    if (!CGlobal.NetControl.Login(CGlobal.GameServerNamePort, _FirebaseLogin.FirebaseUID(), 0, Stream,
//                                                  DataPath + CGlobal.GameServerNamePort.Name + "_" + CGlobal.GameServerNamePort.Port.ToString() + "_" + "Data/"))
//                    {
//                        _FirebaseLogin.FirebaseLogout();
//                        _AccountLoginObject.SetActive(true);
//                    }
//                    else
//                    {
//                        if (CGlobal.GameOption.Data.IsPush)
//                        {
//                            Firebase.Messaging.FirebaseMessaging.SubscribeAsync("Notification");
//                            CGlobal.PushStatusLanguageOn();
//                        }
//                        else
//                        {
//                            Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification");
//                            CGlobal.PushStatusLanguageOff();
//                        }
//                        CGlobal.IsGuest = _FirebaseLogin.IsGuest();
//                    }
//                }
//                else
//                {
//                    _AccountLoginObject.SetActive(true);
//                }
//                _IsFirebaseFirst = false;
//            }
//        }

//        if (GlobalFunction.isBackButtonPressed())
//        {
//            if (GlobalVariable.main.SystemPopup.gameObject.activeSelf)
//            {
//                GlobalVariable.main.SystemPopup.Hide();
//                return;
//            }
//            GlobalVariable.main.SystemPopup.ShowGameOut();
//        }
//    }
//    public void Init(CAccountScene AccountScene_)
//    {
//        _AccountScene = AccountScene_;

//        _AccountLoginObject.SetActive(false);
//    }
//    public void CancelClick()
//    {
//        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
//        GlobalVariable.main.SystemPopup.ShowPopup(EText.Global_Popup_Terms_Cancel, PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type_) =>
//        {
//            if (type_ == PopupSystem.PopupBtnType.Ok)
//            {
//                CGlobal.WillClose = true;
//                CGlobal.NetControl.Logout();
//                CUnity.ApplicationQuit();
//                return;
//            }
//        });
//    }
//    public void LoginComplete()
//    {
//        CGlobal.ProgressCircle.Deactivate();
//        CStream Stream = new CStream();
//        Stream.Push(new SUserCreateOption(new SUserLoginOption(CUnity.GetOS()), ELanguage.English));
//#if UNITY_EDITOR
//        var DataPath = Application.dataPath + "/../";
//#else
//        var DataPath = Application.persistentDataPath + "/";
//#endif
//        CGlobal.ProgressCircle.Activate();
//        CGlobal.NetControl.Create(CGlobal.GameServerNamePort, _FirebaseLogin.FirebaseUID(), CGlobal.NickName, 0, Stream, DataPath + CGlobal.GameServerNamePort.Name + "_" + CGlobal.GameServerNamePort.Port.ToString() + "_" + "Data/");

//        if (CGlobal.GameOption.Data.IsPush)
//        {
//            Firebase.Messaging.FirebaseMessaging.SubscribeAsync("Notification");
//            CGlobal.PushStatusLanguageOn();
//        }
//        else
//        {
//            Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("Notification");
//            CGlobal.PushStatusLanguageOff();
//        }

//        CGlobal.IsGuest = _FirebaseLogin.IsGuest();

//        _AccountLoginObject.SetActive(false);
//    }
//    public void SendUseAccount()
//    {
//        _AccountLoginObject.SetActive(true);
//    }
//    public void SendUseAccountClose()
//    {
//        _AccountLoginObject.SetActive(true);
//    }
//    public void SendGuestCreate()
//    {
//        if (TermCheck())
//            _FirebaseLogin.GuestLoginClick();
//    }
//    public void SendAccountClick()
//    {
//    }
//    public void LoginError()
//    {
//        _AccountLoginObject.SetActive(true);
//    }
//    public void SendAppleClick()
//    {
//        if (TermCheck())
//            _FirebaseLogin.AppleLoginClick();
//    }
//    public void SendGoogleClick()
//    {
//        if (TermCheck())
//            _FirebaseLogin.GoogleLoginClick();
//    }
//    public void SendFacebookClick()
//    {
//        if (TermCheck())
//            _FirebaseLogin.FacebookLoginClick();
//    }
//    private bool TermCheck()
//    {
//        if (!_TermPrivate.isOn)
//        {
//            GlobalVariable.main.SystemPopup.ShowPopup(EText.Global_Popup_PrivacyPolicy_NeedAgree, PopupSystem.PopupType.Confirm);
//            return false;
//        }
//        if (!_TermUse.isOn)
//        {
//            GlobalVariable.main.SystemPopup.ShowPopup(EText.Global_Popup_TermsOfUse_NeedAgree, PopupSystem.PopupType.Confirm);
//            return false;
//        }
//        return true;
//    }
//    public void TermUseView()
//    {
//        if (CGlobal.MetaData.Lang == ELanguage.Korean)
//            Application.OpenURL("https://sites.google.com/stairgames.com/tos-kr");
//        else
//            Application.OpenURL("https://sites.google.com/stairgames.com/tos-en");
//    }
//    public void TermPrivateView()
//    {
//        if (CGlobal.MetaData.Lang == ELanguage.Korean)
//            Application.OpenURL("https://sites.google.com/stairgames.com/privacy-kr");
//        else
//            Application.OpenURL("https://sites.google.com/stairgames.com/privacy-en");
//    }
//}

//public class CAccountScene : CNoMoneyUIScene
//{
//    private AccountScene _AccountScene = null;

//    public CAccountScene() :
//        base("Prefabs/SceneAccount", Vector3.zero)
//    {
//        _AccountScene = gameObject.GetComponent<AccountScene>();
//        _AccountScene.Init(this);
//    }
//}
