using rso.core;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiModeUI : MonoBehaviour
{
    [SerializeField] EGameMode _GameMode = EGameMode.Null;
    [SerializeField] Text _GamePlayTime = null;
    [SerializeField] Text _GamePlayIsland = null;
    [SerializeField] Text _GamePlayScore = null;
    [SerializeField] Image _JoyPadObject = null;
    [SerializeField] Image _StaminaBar = null;
    [SerializeField] Text _TimeCount = null;
    [SerializeField] Int32 _StartCountDown = 10;

    [SerializeField] GameObject _JoyPad = null;
    [SerializeField] GameObject _GameStartView = null;
    [SerializeField] GameObject _GameEndView = null;

    [SerializeField] GameObject _GameIsland = null;
    [SerializeField] GameObject _GameStamina = null;

    [SerializeField] GameObject _EmotionLayer = null;
    [SerializeField] MultiPlayerInfoPanel _MyInfoPanel = null;
    [SerializeField] MultiPlayerInfoPanel _EnemyInfoPanel = null;

    CSceneBaseMulti _CSceneBattleMulti = null;

    TimePoint _EndTime;
    CSeedValue _Score;
    bool IsStart = false;

    Int32 _CountNum = 0;

    float _MinTimeCountScale = 0.7f;
    float _NowTimeCountScale = 1.0f;
    float _TimeCountScaleAcel = 0.6f;

    public void Init(Int32 BalanceCount_, CSceneBaseMulti CSceneBaseMulti_, EGameMode Type_)
    {
        _Score = new CSeedValue(0);
        _Score.Set(0);

        SetIslandCount(BalanceCount_);
        _GamePlayScore.text = "0";
        _GamePlayTime.text = "2:00";

        _CSceneBattleMulti = CSceneBaseMulti_;
        if (_GameMode == EGameMode.DodgeSolo)
        {
            _JoyPad.SetActive(CGlobal.GameOption.Data.IsPad);
            _GameIsland.SetActive(false);
            _GameStamina.SetActive(false);
            SetDodgeScore(BalanceCount_, 0);
        }
        else if (_GameMode == EGameMode.IslandSolo)
        {
            _JoyPad.SetActive(false);
            _GameIsland.SetActive(true);
            _GameStamina.SetActive(true);
            SetIslandScore(BalanceCount_, 0, 0);
        }
        var Players = _CSceneBattleMulti.GetPlayers();
        for (var i = 0; i < Players.Length; ++i)
        {
            var IconName = CGlobal.MetaData.Chars[Players[i].CharCode].IconName;
            if (Players[i].UID == CGlobal.UID)
                _MyInfoPanel.Init(IconName, Players[i].Nick, 0, Players[i].UID == CGlobal.UID);
            else
                _EnemyInfoPanel.Init(IconName, Players[i].Nick, 0, Players[i].UID == CGlobal.UID);
        }
        _EmotionLayer.SetActive(false);
        IsStart = false;
    }
    public void SetJoyPadVisible(bool IsView_)
    {
        _JoyPadObject.gameObject.SetActive(IsView_);
    }
    public void SetIslandCount(Int32 Island_)
    {
        _GamePlayIsland.text = Island_.ToString();
    }
    public void SetIslandScore(Int32 Island_, Int32 Point_, Int32 Landing_)
    {
        _Score.Set((Island_ * CGlobal.MetaData.MultiIslandBalanceMeta.ScoreFactorIsland) +
                   (Point_ * CGlobal.MetaData.MultiIslandBalanceMeta.ScoreFactorPoint) +
                   (Landing_ * CGlobal.MetaData.MultiIslandBalanceMeta.ScoreFactorLanding));

        CGlobal.NetControl.Send<SSingleBattleScoreNetCs>(new SSingleBattleScoreNetCs(_Score.Get()));
        _GamePlayScore.text = _Score.Get().ToString();
    }
    public void SetDodgeScore(Int32 Wave_, Int32 Point_)
    {
        _Score.Set((Wave_ * CGlobal.MetaData.MultiBalanceMeta.ScoreFactorWave) +
                   (Point_ * CGlobal.MetaData.MultiBalanceMeta.ScoreFactorPoint));

        CGlobal.NetControl.Send<SSingleBattleScoreNetCs>(new SSingleBattleScoreNetCs(_Score.Get()));
        _GamePlayScore.text = _Score.Get().ToString();
    }
    public void SetPlayerScoreText(bool IsMe_, Int32 PlayerIndex_, Int32 Score_)
    {
        if (IsMe_)
            _MyInfoPanel.SetScore(Score_);
        else
            _EnemyInfoPanel.SetScore(Score_);
        _CSceneBattleMulti.GetPlayers()[PlayerIndex_].Point = Score_;
    }
    public void SendEmotion(Int32 Code_)
    {
        CGlobal.NetControl.Send<SSingleBattleIconNetCs>(new SSingleBattleIconNetCs(Code_));
        ViewEmotionLayer();
    }
    public void ViewEmotionLayer()
    {
        _EmotionLayer.SetActive(!_EmotionLayer.activeSelf);
    }
    public void SetPlayerEmotion(bool IsMe_, Int32 Code_)
    {
        if (IsMe_)
            _MyInfoPanel.SetEmotion(Code_);
        else
            _EnemyInfoPanel.SetEmotion(Code_);
    }
    public void SetInk(bool IsOn_, bool IsMe_)
    {
        if (IsMe_)
            _MyInfoPanel.SetInk(IsOn_);
        else
            _EnemyInfoPanel.SetInk(IsOn_);
    }
    public void SetScale(bool IsOn_, bool IsMe_)
    {
        if (IsMe_)
            _MyInfoPanel.SetScale(IsOn_);
        else
            _EnemyInfoPanel.SetScale(IsOn_);
    }
    public void SetSlow(bool IsOn_, bool IsMe_)
    {
        if (IsMe_)
            _MyInfoPanel.SetSlow(IsOn_);
        else
            _EnemyInfoPanel.SetSlow(IsOn_);
    }

    private void SetTimeString()
    {
        var Now = CGlobal.GetServerTimePoint();
        Int32 Time_ = Mathf.CeilToInt((float)(_EndTime - Now).TotalSeconds);
        SetTime(Time_);
    }
    private void Update()
    {
        if (IsStart)
        {
            SetTimeString();
        }
    }
    public void SetStaminaBar(float Stamina_, float MaxStamina_)
    {
        //_StaminaBar.transform.localScale = new Vector3(Stamina_/MaxStamina_, 1.0f,1.0f);
        if(Stamina_ > MaxStamina_)
            Stamina_ = MaxStamina_;

        _StaminaBar.transform.localScale = new Vector3(Stamina_ / MaxStamina_, 1.0f, 1.0f);
    }
    public void ShowGameStart()
    {
        _GameStartView.SetActive(true);
    }
    public void HideGameStart(TimePoint EndTime_)
    {
        _EndTime = EndTime_;
        _GameStartView.SetActive(false);
        IsStart = true;
    }
    public void ShowGameEnd()
    {
        _GamePlayTime.text = "2:00";
        IsStart = false;
        _TimeCount.transform.gameObject.SetActive(false);
        _GameEndView.SetActive(true);
    }

    public void SetTime(Int32 time_)
    {
        if (time_ < 0) time_ = 0;
        string timeString = "";
        var min = time_ / 60;
        var sec = time_ % 60;

        timeString = min.ToString() + ":" + string.Format("{0:D2}", sec);

        _GamePlayTime.text = timeString;

        if (time_ > _StartCountDown) return;
        else if (time_ == _StartCountDown)
        {
            _TimeCount.transform.gameObject.SetActive(true);
            _TimeCount.text = System.Convert.ToString(time_);
        }

        SetTimeCount(time_);
        TimeCountEffect();
    }
    void SetTimeCount(Int32 count_)
    {
        if (_CountNum > count_)
        {
            _TimeCount.text = System.Convert.ToString(count_);
            CGlobal.Sound.PlayOneShot((Int32)ESound.Countdown);
        }

        _CountNum = count_;
    }
    void TimeCountEffect()
    {
        _NowTimeCountScale -= _TimeCountScaleAcel * Time.deltaTime;
        if (_NowTimeCountScale < _MinTimeCountScale || _NowTimeCountScale > 1.0f)
        {
            _TimeCountScaleAcel *= -1;
        }
        _TimeCount.rectTransform.localScale = new Vector3(_NowTimeCountScale, _NowTimeCountScale, 1.0f);
    }
}
