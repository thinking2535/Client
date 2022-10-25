using bb;
using rso.core;
using rso.gameutil;
using rso.unity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InvalidDisconnectNoticePopup : MonoBehaviour
{
    [SerializeField] Text _Title = null;
    [SerializeField] Text _Text = null;
    [SerializeField] Text _TimeLeftText = null;
    [SerializeField] Text _LeftDuration = null;
    private void Awake()
    {
        _Title.text = CGlobal.MetaData.getText(EText.Global_Popup_Notice);
        _Text.text = CGlobal.MetaData.getText(EText.InvalidDisconnectNotice);
        _TimeLeftText.text = CGlobal.MetaData.getText(EText.TimeLeft);
    }
    private void Update()
    {
        var BlockedTicks = (CGlobal.LoginNetSc.User.InvalidDisconnectInfo.MatchBlockEndTime - CGlobal.GetServerTimePoint()).Ticks;
        if (BlockedTicks < 0)
            BlockedTicks = 0;

        _LeftDuration.text = CGameUtil.TickToHourMinuteSecondString(BlockedTicks);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
