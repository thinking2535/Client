using bb;
using rso.core;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisconnectPopup : MonoBehaviour
{
    [SerializeField] Text _Title = null;
    [SerializeField] Text _Text = null;
    [SerializeField] Text _SecondsLeft = null;
    public TimePoint EndTimePoint;
    private void Awake()
    {
        _Title.text = CGlobal.MetaData.getText(EText.Global_Popup_Notice);
        _Text.text = CGlobal.MetaData.getText(EText.OtherPlayerDisconnected);
    }
    private void Update()
    {
        var LeftDuration = EndTimePoint - TimePoint.Now;
        if (LeftDuration > TimeSpan.Zero)
            _SecondsLeft.text = ((Int32)Math.Ceiling(LeftDuration.TotalSeconds)).ToString();
        else
            _SecondsLeft.text = "0";
    }
}
