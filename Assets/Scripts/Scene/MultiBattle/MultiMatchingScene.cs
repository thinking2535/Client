using bb;
using rso.core;
using rso.unity;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MultiMatchingScene : NoMoneyUIScene
{
    TimePoint _BeginTime;

    [SerializeField] Button _btnCancel = null;
    public UIUserCharacter UserCharacter = null;
    public Text ReadyTime = null;
    public Text MatchingTitle = null;
    public GameObject ItemDescriptionBG = null;

    public new void init()
    {
        base.init();

        _btnCancel.onClick.AddListener(_cancel);

        ItemDescriptionBG.SetActive(false);

        ReadyTime.text = "00:00";
        _BeginTime = CGlobal.GetServerTimePoint();
        MatchingTitle.text = "";

        var RankKeyValuePair = CGlobal.MetaData.RankTiers.Get(CGlobal.Point);

        Int32 CharCode = CGlobal.LoginNetSc.User.SelectedCharCode;
        UserCharacter.MakeCharacter(CharCode);
    }
    protected override void Update()
    {
        base.Update();

        SetTimeCount();
    }
    protected override void OnDestroy()
    {
        _btnCancel.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    protected override async Task<bool> _backButtonPressed()
    {
        if (await base._backButtonPressed())
            return true;

        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.NetControl.Send(new SMultiBattleOutNetCs());
        AnalyticsManager.TrackingEvent(ETrackingKey.cancel_multiplay);
        return true;
    }
    public async void _cancel()
    {
        await _backButtonPressed();
    }
    void SetTimeCount()
    {
        Int32 TimeSec = Mathf.CeilToInt((float)(CGlobal.GetServerTimePoint() - _BeginTime).TotalSeconds);
        if (TimeSec < 0) TimeSec = 0;
        string timeString = "";
        var min = TimeSec / 60;
        var sec = TimeSec % 60;
        if (min >= 60)
        {
            var hour = min / 60;
            min = min % 60;
            timeString = hour.ToString() + ":" + string.Format("{0:D2}", min) + ":" + string.Format("{0:D2}", sec);
        }
        else
        {
            timeString = string.Format("{0:D2}", min) + ":" + string.Format("{0:D2}", sec);
        }
        ReadyTime.text = timeString;
    }
}
