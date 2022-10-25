using bb;
using rso.unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Google;
using Firebase.Auth;
using System.Security.Cryptography;
using System.Text;
#if UNITY_IOS
using AppleAuth;
using AppleAuth.Native;
using AppleAuth.Enums;
using AppleAuth.Interfaces;
#endif
using Facebook.Unity;
using rso.game;
using rso.core;
using System.Threading.Tasks;
using UnityEngine.Events;
using System;

public class FirebaseLogin : MonoBehaviour
{
    public enum ELoginProvider
    {
        Google,
        Facebook,
        Apple,
        Guest
    }
    public UnityEvent CompleteCallback = null;
    public UnityEvent ErrorCallback = null;
    [SerializeField] GameObject _AppleLogin = null;
    [SerializeField] GameObject _GoogleLogin = null;

    FirebaseAuth _Auth = null;
    bool _IsCancel = false;
    bool _IsError = false;
    bool _IsErrorDuplicate = false;
    string _ErrorText = "";
    bool _IsComplelet = false;
    public bool InitFirebaseAuthFinish = false;
#if UNITY_IOS
    public static string IdToken { get; private set; }
    public static string AuthCode { get; private set; }
    public static string RawNonce { get; private set; }
    public static string Nonce { get; private set; }
    private IAppleAuthManager appleAuthManager = null;
#endif

    private bool _IsFirst = true;
    public string UID = "";
    public bool IsGuestAsync = false;

    private void Awake()
    {
        if (!_IsFirst) return;
        _IsFirst = false;
#if UNITY_EDITOR
        _AppleLogin.SetActive(false);
        _GoogleLogin.SetActive(false);
        InitFirebaseAuthFinish = true;
#endif
#if UNITY_STANDALONE

#elif !UNITY_EDITOR
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(async task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                _Auth = FirebaseAuth.DefaultInstance;
                InitFirebaseAuthFinish = true;
                Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
            }
            else
            {
                if (await CGlobal.curScene.pushAskingPopup(EText.SceneAccount_ModuelInitFailed, dependencyStatus) is true)
                {
                    CGlobal.WillClose = true;
                    CGlobal.NetControl.Logout();
                    CUnity.ApplicationQuit();
                }
            }
        });
        if(GoogleSignIn.Configuration == null)
        {
            GoogleSignIn.Configuration = new GoogleSignInConfiguration
            {
                RequestIdToken = true,
#if UNITY_ANDROID
                WebClientId = "525200764975-lr447hdfn6cqn4iuajk1fvjq993hnmov.apps.googleusercontent.com"
#else
                WebClientId = "525200764975-n81mkh4p1tuuk17b4m3vs0crlcahqibh.apps.googleusercontent.com"
#endif
            };
        }
        if (!FB.IsInitialized)
            FB.Init(OnFBInitComplete);
#if UNITY_ANDROID
        _AppleLogin.SetActive(false);
        _GoogleLogin.SetActive(true);
#else
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            _AppleLogin.SetActive(true);
            var deserializer = new PayloadDeserializer();
            appleAuthManager = new AppleAuthManager(deserializer);
        }
        else
        {
            _AppleLogin.SetActive(false);
        }
        _GoogleLogin.SetActive(true);
#endif
#else
        _Auth = FirebaseAuth.DefaultInstance;
#endif
    }

    internal void FirebaseLogout()
    {
        _Auth.SignOut();
    }
#if !UNITY_EDITOR
    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }
#endif
    private void OnFBInitComplete()
    {
    }
#if UNITY_IOS

    // Nonce는 SHA256으로 만들어서 전달해야함
    private static string GenerateNonce(string RawNonce_)
    {
        SHA256 sha = new SHA256Managed();
        var sh = new StringBuilder();
        // Encoding은 반드시 ASCII여야 함
        byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(RawNonce_));
        // ToString에서 "x2"로 소문자 변환해야 함. 대문자면 실패함. ㅠㅠ
        foreach (var b in hash) sh.Append(b.ToString("x2"));
        return sh.ToString();
    }
#endif
    public void AppleLoginClick()
    {
#if UNITY_IOS
        PlayerPrefs.SetString("ProviderID", "Apple");
        CGlobal.ProgressCircle.Show();
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        RawNonce = System.Guid.NewGuid().ToString();
        Nonce = GenerateNonce(RawNonce);

        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail, Nonce);
        appleAuthManager.LoginWithAppleId(loginArgs,
        async Credential_ => {
            try
            {
                var appleIdCredential = Credential_ as IAppleIDCredential;
                AuthCode = Encoding.UTF8.GetString(appleIdCredential.AuthorizationCode);
                IdToken = Encoding.UTF8.GetString(appleIdCredential.IdentityToken);

                Credential credential = OAuthProvider.GetCredential("apple.com", IdToken, RawNonce, AuthCode);
                if(IsGuestAsync)
                {
                    AnalyticsManager.TrackingEvent(ETrackingKey.guest_to_apple);
                    GetFirebaseGuestAsyncId(credential);
                }
                else
                    GetFirebaseId(credential);
                    
                AnalyticsManager.TrackingEvent(ETrackingKey.apple_login);

            }
            catch (System.Exception e)
            {
                CGlobal.ProgressCircle.Hide();
                await CGlobal.curScene.CGlobal.curScene.pushNoticePopup(EText.SceneAccount_LoginFailed, 1);
            }
        },
        error => {
            CGlobal.ProgressCircle.Hide();
            await CGlobal.curScene.CGlobal.curScene.pushNoticePopup(EText.SceneAccount_LoginFailed, error.Code);
        });
#endif
    }

    async void Update()
    {
#if UNITY_IOS
        appleAuthManager?.Update();
#endif
        if(_IsError)
        {
            PlayerPrefs.SetString("ProviderID", "Guest");
            _IsError = false;
            CGlobal.ProgressCircle.Deactivate();

            await CGlobal.curScene.pushNoticePopup(true, EText.SceneAccount_LoginFailed, 1);

            ErrorCallback?.Invoke();
        }
        if(_IsCancel)
        {
            PlayerPrefs.SetString("ProviderID", "Guest");
            _IsCancel = false;
            CGlobal.ProgressCircle.Deactivate();

            await CGlobal.curScene.pushNoticePopup(false, EText.SceneAccount_LoginCancel);

            ErrorCallback?.Invoke();
        }
        if(_IsErrorDuplicate)
        {
            PlayerPrefs.SetString("ProviderID", "Guest");
            _IsErrorDuplicate = false;
            CGlobal.ProgressCircle.Deactivate();

            await CGlobal.curScene.pushNoticePopup(true, EText.Firebase_Login_Duplicate);

            ErrorCallback?.Invoke();
        }
        if(_IsComplelet)
        {
            _IsComplelet = false;
            CompleteCallback.Invoke();
        }
    }
    public void GoogleLoginClick()
    {
        PlayerPrefs.SetString("ProviderID", "Google");
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.ProgressCircle.Activate();
        Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn();

        signIn.ContinueWith(Task_ => {
            if (Task_.IsCanceled)
            {
                _IsCancel = true;
            }
            else if (Task_.IsFaulted)
            {
                _IsError = true;
                _ErrorText = Task_.Exception.ToString();
            }
            else
            {
                Credential credential = GoogleAuthProvider.GetCredential(Task_.Result.IdToken, null);
                if (IsGuestAsync)
                {
                    AnalyticsManager.TrackingEvent(ETrackingKey.guest_to_google);
                    GetFirebaseGuestAsyncId(credential);
                }
                else
                    GetFirebaseId(credential);

                AnalyticsManager.TrackingEvent(ETrackingKey.google_login);
            }
        });
    }
    public void FacebookLoginClick()
    {
        PlayerPrefs.SetString("ProviderID", "Facebook");
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.ProgressCircle.Activate();
        FB.LogInWithReadPermissions(new List<string>() { "email", "public_profile" }, async LoginResult_ =>
        {
            if (LoginResult_ == null)
            {
                //이쪽으로 오면 심각한 문제가 있음.
                _IsError = true;
                _ErrorText = "Result Null";
                return;
            }
            // Some platforms return the empty string instead of null.
            if (!string.IsNullOrEmpty(LoginResult_.Error))
            {
                _IsError = true;
                _ErrorText = LoginResult_.Error;
            }
            else if (LoginResult_.Cancelled)
            {
                _IsCancel = true;
            }
            else if (!string.IsNullOrEmpty(LoginResult_.RawResult))
            {
                //Facebook 플러그인에서 함수처리를 Task를 사용하지 않아 AOS에서
                //Main Thread 침범으로 튕기는 이슈가 발생!
                //강제로 비동기로 Firebase 연동하도록 로직 수정.<나중에 우아한 방법이 있을 경우 수정 예정>
                await Task.Run(() =>
                {
                    Credential credential = FacebookAuthProvider.GetCredential(LoginResult_.AccessToken.TokenString);
                    if (IsGuestAsync)
                    {
                        AnalyticsManager.TrackingEvent(ETrackingKey.guest_to_facebook);
                        GetFirebaseGuestAsyncId(credential);
                    }
                    else
                        GetFirebaseId(credential);

                    AnalyticsManager.TrackingEvent(ETrackingKey.facebook_login);
                });
            }
            else
            {
                //이쪽으로 오면 심각한 문제가 있음.
                _IsError = true;
                _ErrorText = "Unknown Error";
            }
        });
    }
    public void GuestLoginClick()
    {
        PlayerPrefs.SetString("ProviderID", "Guest");
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.ProgressCircle.Activate();
        _Auth.SignInAnonymouslyAsync().ContinueWith(Task_ => {
            if (Task_.IsCanceled)
            {
                _IsCancel = true;
                return;
            }
            if (Task_.IsFaulted)
            {
                _IsError = true;
                _ErrorText = Task_.Exception.ToString();
                return;
            }

            FirebaseUser newUser = Task_.Result;
            UID = newUser.UserId;
            _IsComplelet = true;
            AnalyticsManager.TrackingEvent(ETrackingKey.guest_login);
        });
    }
    public void GetFirebaseId(Credential credential)
    {
        _Auth.SignInWithCredentialAsync(credential).ContinueWith(Task_ => {
            if (Task_.IsCanceled)
            {
                _IsCancel = true;
                return;
            }
            if (Task_.IsFaulted)
            {
                _IsError = true;
                _ErrorText = Task_.Exception.ToString();
                return;
            }

            FirebaseUser newUser = Task_.Result;
            UID = newUser.UserId;
            _IsComplelet = true;
        });
    }
    public void GetFirebaseGuestAsyncId(Credential credential)
    {
        _Auth.CurrentUser.LinkWithCredentialAsync(credential).ContinueWith(Task_ => {
            if (Task_.IsCanceled)
            {
                _IsCancel = true;
                return;
            }
            if (Task_.IsFaulted)
            {
                _IsErrorDuplicate = true;
                _ErrorText = Task_.Exception.ToString();
                return;
            }

            FirebaseUser newUser = Task_.Result;
            UID = newUser.UserId;
            _IsComplelet = true;
        });
    }
    public string FirebaseUID()
    {
        if (_Auth.CurrentUser != null)
        {
            return _Auth.CurrentUser.UserId;
        }
        return "";
    }
    public bool AutoLoginCheck()
    {
        bool IsLogin = false;
        if (_Auth.CurrentUser != null)
        {
            IsLogin = true;
        }
        return IsLogin;
    }
    public bool IsGuest()
    {
#if UNITY_EDITOR
        return false;
#else
        return _Auth.CurrentUser.IsAnonymous;
#endif
    }
    public ELoginProvider GetProvider()
    {
#if UNITY_EDITOR
        return ELoginProvider.Facebook;
#else
        ELoginProvider Provider = ELoginProvider.Guest;
        switch (PlayerPrefs.GetString("ProviderID", "Guest"))
        {
            case "Google":
                Provider = ELoginProvider.Google;
                break;
            case "Facebook":
                Provider = ELoginProvider.Facebook;
                break;
            case "Apple":
                Provider = ELoginProvider.Apple;
                break;
            default:
                Provider = ELoginProvider.Guest;
                break;
        }
        return Provider;
#endif
    }
}
