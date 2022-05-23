using bb;
using rso.core;
using rso.net;
using rso.unity;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CSceneIntro : CSceneBase
{
    IntroScene _IntroScene = null;
    bool StartVersionCheck = false;
    bool IsUpdate = false;
    bool IsServerBusy = false;
    bool IsNetworkError = false;
    public CSceneIntro() :
        base("Prefabs/IntroScene", Vector3.zero, true)
    {
    }
    public override void Dispose()
    {
    }
    public override void Enter()
    {
        _IntroScene = _Object.GetComponent<IntroScene>();
        StartVersionCheck = false;
        _IntroScene.Enter();
        if (AnalyticsManager.AppCloseCount() == 0)
            AnalyticsManager.TrackingEvent(ETrackingKey.title_end);
    }
    public override bool Update()
    {
        if (_Exit)
            return false;

        if (_IntroScene.Ended() && !StartVersionCheck)
        {

            StartVersionCheck = true;
            //CGlobal.ScrollConfirmPopup.ShowPopup(CGlobal.MetaData.GetText(EText.Global_Popup_Notice),CGlobal.MetaData.GetText(EText.Global_Popup_CloseService),
            //        () => {
            //#if !UNITY_EDITOR && !UNITY_STANDALONE
            //                        if (CGlobal.ServerAnchor)
            //                        {
            //                            CGlobal.SceneSetNext(new CSceneAccount());
            //                        }
            //                        else
            //                        {
            //                            _IntroScene.StartCoroutine(VersionCheck());
            //                        }
            //#else
            CGlobal.SceneSetNext(new CSceneLogin());
            //#endif
            //});
        }

        if (rso.unity.CBase.BackPushed())
        {
            if (IsUpdate || IsServerBusy || IsNetworkError)
            {
                CGlobal.WillClose = true;
                CGlobal.NetControl.Logout();
                rso.unity.CBase.ApplicationQuit();
            }
        }

        return true;
    }
    IEnumerator VersionCheck()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("https://any-balloon.s3.ap-northeast-2.amazonaws.com/ServerInfo.txt");
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            ShowErrorPopup("Network");
            yield break;
        }
        else if (webRequest.isHttpError)
        {
            ShowErrorPopup("Http");
            yield break;
        }
        else if (webRequest.isModifiable)
        {
            ShowErrorPopup("Modifiable");
            yield break;
        }
#if !UNITY_EDITOR
        var JsonData = (JsonDataObject)JsonParser.Parse(webRequest.downloadHandler.text);
        var TestVersion = (JsonDataObject)JsonData["TestVersion"];
        var TestVersionIsBusy = TestVersion["IsBusy"].GetBool();
        var TestVersionEndDateTimeString = TestVersion["EndDateTimeString"].GetString();
        var TestVersionGameServer = TestVersion["GameServer"].GetString();
        var TestVersionRankServer = TestVersion["RankingServer"].GetString();
        var TestVersionMain = UInt32.Parse(TestVersion["Main"].GetString());
        var TestVersionData = UInt64.Parse(TestVersion["Data"].GetString());
        var LiveVersion = (JsonDataObject)JsonData["LiveVersion"];
        var LiveVersionIsBusy = LiveVersion["IsBusy"].GetBool();
        var LiveVersionEndDateTimeString = LiveVersion["EndDateTimeString"].GetString();
        var LiveVersionGameServer = LiveVersion["GameServer"].GetString();
        var LiveVersionRankServer = LiveVersion["RankingServer"].GetString();
        var LiveVersionMain = UInt32.Parse(LiveVersion["Main"].GetString());
        var LiveVersionData = UInt64.Parse(LiveVersion["Data"].GetString());
        if (TestVersionMain == global.c_Ver_Main && TestVersionData == CGlobal.MetaData.Checksum)
        {
            CGlobal.IsTestServer = true;
            NextScene(CGlobal.EServer.AWSTest, TestVersionGameServer, TestVersionRankServer, TestVersionIsBusy, TestVersionEndDateTimeString);
        }
        else if (LiveVersionMain == global.c_Ver_Main && LiveVersionData == CGlobal.MetaData.Checksum)
        {
            CGlobal.IsTestServer = false;
            NextScene(CGlobal.EServer.AWSTest, LiveVersionGameServer, LiveVersionRankServer, LiveVersionIsBusy, LiveVersionEndDateTimeString);
        }
        else
        {
            IsUpdate = true;
            CGlobal.SystemPopup.ShowUpdatePopup((PopupSystem.PopupBtnType type_) =>
            {
#if UNITY_IOS
                    Application.OpenURL("https://itunes.apple.com/kr/app/apple-store/id1530777694");
#else
                    Application.OpenURL("http://play.google.com/store/apps/details?id=com.stairgames.balloonstars");
#endif
                    CGlobal.WillClose = true;
                CGlobal.NetControl.Logout();
                rso.unity.CBase.ApplicationQuit();
            });
        }
#endif
    }
    public void NextScene(CGlobal.EServer Server_, string GameServer_, string RankServer_, bool IsBusy_, string EndDateTimeString_)
    {
        if (IsBusy_)
        {
            IsServerBusy = true;
            EText Text = CGlobal.MetaData.CheckAlarm(EndDateTimeString_);
            if (Text != EText.Null)
            {
                var SplitMsg = EndDateTimeString_.Split(' ');
                CGlobal.SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(Text), SplitMsg[1]),
                    PopupSystem.PopupType.Confirm, (PopupSystem.PopupBtnType type_) =>
                    {
                        CGlobal.WillClose = true;
                        CGlobal.NetControl.Logout();
                        rso.unity.CBase.ApplicationQuit();
                    });
            }
            else
            {
                CGlobal.SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(EText.GlobalPopup_Text_ServerCheck), EndDateTimeString_),
                    PopupSystem.PopupType.Confirm, (PopupSystem.PopupBtnType type_) =>
                    {
                        CGlobal.WillClose = true;
                        CGlobal.NetControl.Logout();
                        rso.unity.CBase.ApplicationQuit();
                    });
            }
        }
        else
        {
            CGlobal.Server = Server_;
            CGlobal.Servers[(Int32)CGlobal.Server] = new CGlobal._SServer(new CNamePort(GameServer_, CGlobal.Servers[(Int32)CGlobal.Server].Game.Port),
                new CNamePort(RankServer_, CGlobal.Servers[(Int32)CGlobal.Server].Ranking.Port));
            CGlobal.SceneSetNext(new CSceneAccount());
        }
    }
    private void ShowErrorPopup(string ErrorCode_)
    {
        IsNetworkError = true;
        CGlobal.SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(EText.GlobalPopup_Network_LinkFailed), ErrorCode_), PopupSystem.PopupType.Confirm,
        (PopupSystem.PopupBtnType type_) =>
        {
            CGlobal.WillClose = true;
            CGlobal.NetControl.Logout();
            rso.unity.CBase.ApplicationQuit();
        });
    }
}
