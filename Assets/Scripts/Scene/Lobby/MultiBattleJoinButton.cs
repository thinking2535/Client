using bb;
using rso.gameutil;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MultiBattleJoinButton : MonoBehaviour
{
    [SerializeField] Button _Button;
    [SerializeField] Image _CostIcon;
    [SerializeField] Text _CostValue;
    [SerializeField] GameObject _Clock;
    [SerializeField] Text _MainText;
    [SerializeField] Text _SubText;
    TimeSpan _Update()
    {
        return CGlobal.LoginNetSc.User.InvalidDisconnectInfo.MatchBlockEndTime - CGlobal.GetServerTimePoint();
    }
    Int64 _BlockedTicks;
    void Awake()
    {
        _CostIcon.sprite = CGlobal.GetResourceSprite(CGlobal.MetaData.ConfigMeta.BattleCostType);
        _CostValue.text = CGlobal.MetaData.ConfigMeta.BattleCostValue.ToString();
        _MainText.text = CGlobal.MetaData.getText(EText.LobbyScene_MultiPlay);

        _BlockedTicks = _GetBlockedTicks();
        if (_IsBlocked())
            _Block();
        else
            _UnBlock();
    }
    void Update()
    {
        if (_IsBlocked())
        {
            _SubText.text = CGameUtil.TickToHourMinuteSecondString(_BlockedTicks);

            _BlockedTicks = _GetBlockedTicks();
            if (!_IsBlocked())
                _UnBlock();
        }
        else
        {
            _BlockedTicks = _GetBlockedTicks();
            if (_IsBlocked())
                _Block();
        }
    }
    void OnDestroy()
    {
        _Button.onClick.RemoveAllListeners();
    }
    public void Init(UnityAction fClicked_)
    {
        _Button.onClick.AddListener(fClicked_);
    }
    Int64 _GetBlockedTicks()
    {
        return (CGlobal.LoginNetSc.User.InvalidDisconnectInfo.MatchBlockEndTime - CGlobal.GetServerTimePoint()).Ticks;
    }
    bool _IsBlocked()
    {
        return _BlockedTicks > 0;
    }
    void _Block()
    {
        _Clock.SetActive(true);
        _CostIcon.gameObject.SetActive(false);
        _SubText.text = CGlobal.MetaData.getText(EText.MultiScene_Enter);
    }
    void _UnBlock()
    {
        _Clock.SetActive(false);
        _CostIcon.gameObject.SetActive(true);
        _SubText.text = CGlobal.MetaData.getText(EText.MultiScene_Enter);
    }
}