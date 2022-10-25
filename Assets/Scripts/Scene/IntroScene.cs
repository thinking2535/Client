using bb;
using rso.core;
using rso.net;
using rso.unity;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class IntroScene : NoMoneyUIScene
{
    struct _SVersion
    {
        public bool IsBusy;
        public string EndDateTimeString;
        public string GameServer;
        public string RankServer;
        public UInt32 Main;
        public UInt64 Data;

        public _SVersion(
            bool IsBusy_,
            string EndDateTimeString_,
            string GameServer_,
            string RankServer_,
            UInt32 Main_,
            UInt64 Data_)
        {
            IsBusy = IsBusy_;
            EndDateTimeString = EndDateTimeString_;
            GameServer = GameServer_;
            RankServer = RankServer_;
            Main = Main_;
            Data = Data_;
        }
    }

    DateTime _IntroEndDateTime;
    bool _IsRequestingServerInfo = false;

    [SerializeField] Text _VersionText = null;
    [SerializeField] GameObject[] _BackgroundImages;

    public new void init()
    {
        base.init();

        _VersionText.text = "Version : " + CGlobal.VersionText();
        _SelectBackgroundImage();

        if (AnalyticsManager.AppCloseCount() == 0)
            AnalyticsManager.TrackingEvent(ETrackingKey.title_end);

        _IntroEndDateTime = DateTime.Now + TimeSpan.FromSeconds(2.0);
    }
    protected override async void Update()
    {
        base.Update();

        if (!_IsRequestingServerInfo && DateTime.Now >= _IntroEndDateTime)
        {
            _IsRequestingServerInfo = true;
            await RequestServerInfo();
        }
    }
    protected override Task<bool> _backButtonPressed()
    {
        return Task.FromResult(true);
    }
    void _SelectBackgroundImage()
    {
        var SelectedBackgroundImageIndex = UnityEngine.Random.Range(0, _BackgroundImages.Length);

        for (Int32 i = 0; i < _BackgroundImages.Length; ++i)
            _BackgroundImages[i].SetActive(i == SelectedBackgroundImageIndex);
    }
    async Task RequestServerInfo()
    {
        if (CGlobal.SelectedServerURLPort.ServerUrl.Length > 0)
        {
            CGlobal.ProgressCircle.Activate();

            var WebRequest = UnityWebRequest.Get(CGlobal.SelectedServerURLPort.ServerUrl);
            var Operation = WebRequest.SendWebRequest();

            while (!Operation.isDone)
                await Task.Yield();

            CGlobal.ProgressCircle.Deactivate();

            if (WebRequest.error != null)
            {
                await pushNoticePopup(true, EText.GlobalPopup_Network_LinkFailed, WebRequest.error);
                CGlobal.Quit();

                return;
            }

            var JsonData = (JsonDataArray)JsonParser.Parse(WebRequest.downloadHandler.text);
            var Versions = new _SVersion[JsonData.Count];
            for (Int32 i = 0; i < JsonData.Count; ++i)
            {
                var VersionObject = (JsonDataObject)JsonData[i];
                Versions[i] = new _SVersion(
                    VersionObject["IsBusy"].GetBool(),
                    VersionObject["EndDateTimeString"].GetString(),
                    VersionObject["GameServer"].GetString(),
                    VersionObject["RankingServer"].GetString(),
                    UInt32.Parse(VersionObject["Main"].GetString()),
                    UInt64.Parse(VersionObject["Data"].GetString()));
            }

            _SVersion? SelectedVersion = null;
            foreach (var Version in Versions)
            {
                if (Version.Main == global.c_Ver_Main && Version.Data == CGlobal.MetaData.Checksum)
                {
                    SelectedVersion = Version;
                    break;
                }
            }

            if (SelectedVersion == null)
            {
                // 내 Main 버전이 목록의 Main 버전보다 크면 바로접속 (개발 편의)
                foreach (var Version in Versions)
                {
                    if (Version.Main < global.c_Ver_Main)
                    {
                        SelectedVersion = Version;
                        break;
                    }
                }
            }

            if (SelectedVersion == null)
            {
                if (await pushUpdateMessagePopup() is true)
                    Application.OpenURL(CGlobal.Device.GetUpdateUrl());

                CGlobal.Quit();
                return;
            }


            CGlobal.GameServerNamePort = new CNamePort(SelectedVersion.Value.GameServer, CGlobal.SelectedServerURLPort.GameServerPort);
            CGlobal.RankingServerNamePort = new CNamePort(SelectedVersion.Value.RankServer, CGlobal.SelectedServerURLPort.RankingServerPort);

            if (SelectedVersion.Value.IsBusy)
            {
                EText textName = CGlobal.MetaData.CheckAlarm(SelectedVersion.Value.EndDateTimeString);
                if (textName != EText.Null)
                {
                    var SplitMsg = SelectedVersion.Value.EndDateTimeString.Split(' ');
                    await pushNoticePopup(true, textName, SplitMsg[1]);
                    CGlobal.Quit();
                }
                else
                {
                    await pushNoticePopup(true, EText.GlobalPopup_Text_ServerCheck, SelectedVersion.Value.EndDateTimeString);
                    CGlobal.Quit();
                }
            }
            else
            {
                login();
            }
        }
        else
        {
            CGlobal.GameServerNamePort = new CNamePort("115.90.183.62", CGlobal.SelectedServerURLPort.GameServerPort);
            CGlobal.RankingServerNamePort = new CNamePort("115.90.183.62", CGlobal.SelectedServerURLPort.RankingServerPort);

            login();
        }
    }
    async void login()
    {
        var Stream = new CStream();

        Stream.Push(new SUserLoginOption(CUnity.GetOS()));

        if (!CGlobal.NetControl.Login(
            CGlobal.GameServerNamePort,
            "",
            0,
            Stream,
            CGlobal.Device.GetDataPath() + CGlobal.GameServerNamePort.Name + "_" + CGlobal.GameServerNamePort.Port.ToString() + "_" + "Data/"))
        {
            var nickname = await pushCreatePopup();
            CGlobal.Create((string)nickname);
            return;
        }

        CGlobal.ProgressCircle.Activate();
    }
}
