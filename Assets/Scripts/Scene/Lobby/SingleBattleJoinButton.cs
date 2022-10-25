using rso.core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SingleBattleJoinButton : MonoBehaviour
{
    [SerializeField] Button _Button;
    [SerializeField] Text _BattleName;
    [SerializeField] Text _CountLeft;
    [SerializeField] Text _TimeLeft;
    [SerializeField] Image _Clock;

    void OnDestroy()
    {
        _Button.onClick.RemoveAllListeners();
    }
    public void Init(UnityAction fClicked_, string BattleName_)
    {
        _Button.onClick.AddListener(fClicked_);
        _BattleName.text = BattleName_;
        _CountLeft.text = "";
        _TimeLeft.text = "";
    }
    public void SetCountLeftAndTimeLeft(Int32 CountLeft_, Int32 MaxCount_, TimeSpan TimeLeft_)
    {
        _CountLeft.text = CountLeft_.ToString() + "/" + MaxCount_.ToString();

        var LeftTimeVisible = CountLeft_ < MaxCount_;

        _TimeLeft.gameObject.SetActive(LeftTimeVisible);
        _Clock.gameObject.SetActive(LeftTimeVisible);

        if (LeftTimeVisible)
            _TimeLeft.text = string.Format("{0:D2}:{1:D2}", TimeLeft_.Minutes, TimeLeft_.Seconds);
    }
}
