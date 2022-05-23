using bb;
using rso.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneAccount : CSceneBase
{
    private AccountScene _AccountScene = null;
    //private GameObject _TermCheckObject = null;
    private GameObject _AccountLoginObject = null;
    private FirebaseLogin _FirebaseLogin = null;

    bool IsFirebaseFirst = true;
    public CSceneAccount() :
        base("Prefabs/SceneAccount", Vector3.zero, true)
    {
    }
    public override void Dispose()
    {
    }

    public override void Enter()
    {
        _AccountScene = _Object.GetComponent<AccountScene>();
        _AccountLoginObject = _AccountScene.AccountLoginObject;
        _FirebaseLogin = _AccountScene.FirebaseLogin;
        _AccountLoginObject.SetActive(false);
    }

    public override bool Update()
    {
        if (_Exit)
            return false;

        if(IsFirebaseFirst)
        {
            if(_FirebaseLogin.InitFirebaseAuthFinish)
            {
                if (_FirebaseLogin.AutoLoginCheck())
                {
                    var Stream = new CStream();
                    Stream.Push(new SUserLoginOption(rso.unity.CBase.GetOS()));
#if UNITY_EDITOR || UNITY_STANDALONE
                    var DataPath = Application.dataPath + "/../";
#else
                    var DataPath = Application.persistentDataPath + "/";
#endif
                    if (!CGlobal.NetControl.Login(CGlobal.GameIPPort, _FirebaseLogin.FirebaseUID(), 0, Stream, 
                                                  DataPath + CGlobal.GameIPPort.Name + "_" + CGlobal.GameIPPort.Port.ToString() + "_" + "Data/"))
                    {
                        _FirebaseLogin.FirebaseLogout();
                        _AccountLoginObject.SetActive(true);
                    }
                    else
                    {
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
                        CGlobal.IsGuest = _FirebaseLogin.IsGuest();
                    }
                }
                else
                {
                    _AccountLoginObject.SetActive(true);
                }
                IsFirebaseFirst = false;
            }
        }

        if (rso.unity.CBase.BackPushed())
        {
            if (CGlobal.SystemPopup.gameObject.activeSelf)
            {
                CGlobal.SystemPopup.OnClickCancel();
                return true;
            }
            CGlobal.SystemPopup.ShowGameOut();
        }

        return true;
    }
}
