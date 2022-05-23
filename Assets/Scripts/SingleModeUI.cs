using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleModeUI : MonoBehaviour
{
    //Game Playing UI
    [SerializeField] Text _GamePlayTime = null;
    [SerializeField] Text _GamePlayGold = null;
    [SerializeField] Image _JoyPadObject = null;
    [SerializeField] Text _BestPlayTime = null;

    //Result UI
    [SerializeField] GameObject _GameResult = null;
    [SerializeField] Text _GameResultTimeText = null;
    [SerializeField] Text _GameResultGoldText = null;
    [SerializeField] Text _GameResultBonusGoldText = null;
    [SerializeField] Text _GameResultScoreText = null;
    [SerializeField] GameObject _GameResultBtn = null;
    [SerializeField] GameObject _GameResultRetryBtn = null;
    [SerializeField] GameObject _GameResultGoldEffect = null;
    [SerializeField] GameObject _GameResultADBtn = null;
    [SerializeField] Text _GameResultADEffect = null;
    [SerializeField] GameObject _GameResultADOffBtn = null;

    [SerializeField] Text _GamePlayScore = null;
    [SerializeField] Text _GameResultReplayText = null;

    [SerializeField] GameObject _JoyPad = null;
    [SerializeField] GameObject _GameStartView = null;
    [SerializeField] GameObject _GameResultSkipBtn = null;

    CSceneBattleSingle _CSceneBattleSingle = null;

    Int32 _AddGold = 0;
    Int32 _Gold = 0;
    Int32 _Wave = 0;
    Int32 _Time = 0;

    bool IsAdview = false;

    float _ResultStepDelta = 0.0f;
    float _ResultStepTerm = 0.5f;
    enum EResultStep
    {
        None,
        ViewBg,
        ViewTime,
        ViewGold,
        ViewScore,
        ViewBonus,
        ViewButton
    }
    EResultStep _ResultStep = EResultStep.None;

    public void Init(Int32 Wave_, Int32 Time_, Int32 Gold_, Int32 BestTime_, CSceneBattleSingle CSceneBattleSingle_)
    {
        SetTimeCount(Time_);
        SetGoldCount(Gold_);
        SetBestTimeCount(BestTime_);
        _GamePlayScore.text = "0";

        _CSceneBattleSingle = CSceneBattleSingle_;
        _GameResultSkipBtn.SetActive(false);

        _GameResult.SetActive(false);
        _AddGold = 0;
        IsAdview = false;
        _JoyPad.SetActive(CGlobal.GameOption.Data.IsPad);
    }
    public void SetTimeCount(Int32 Time_)
    {
        _GamePlayTime.text = TimeString(Time_);
    }
    public void SetBestTimeCount(Int32 Time_)
    {
        _BestPlayTime.text = TimeString(Time_);
    }
    public void SetScoreCount(Int32 Wave_, Int32 Time_, Int32 AddGold_)
    {
        var WaveScore = (Wave_ - 1) * CGlobal.MetaData.SingleBalanceMeta.ScoreFactorWave;
        if (WaveScore < 0) WaveScore = 0;

        _GamePlayScore.text = (WaveScore +
                                     Time_ * CGlobal.MetaData.SingleBalanceMeta.ScoreFactorTime +
                                     AddGold_ * CGlobal.MetaData.SingleBalanceMeta.ScoreFactorGold).ToString();
    }
    public void SetGoldCount(Int32 Gold_)
    {
        _GamePlayGold.text = Gold_.ToString();
    }
    public void ShowResult(Int32 Wave_, Int32 Time_, Int32 Gold_, Int32 AddGold_)
    {
        _GameResult.SetActive(true);
        _GameResultSkipBtn.SetActive(true);
        _GameResultTimeText.gameObject.SetActive(false);
        _GameResultGoldText.gameObject.SetActive(false);
        _GameResultBonusGoldText.gameObject.SetActive(false);
        _GameResultScoreText.gameObject.SetActive(false);
        _GameResultGoldEffect.SetActive(false);
        _GameResultBtn.SetActive(false);
        _GameResultRetryBtn.SetActive(false);
        _GameResultADEffect.gameObject.SetActive(false);
        _GameResultADBtn.SetActive(false);
        _GameResultADOffBtn.SetActive(false);
        _AddGold = AddGold_;
        _Gold = Gold_;
        _Wave = Wave_;
        _Time = Time_;
        _ResultStep = EResultStep.ViewBg;
        _ResultStepDelta = 0.0f;

        var WaveScore = (Wave_ - 1) * CGlobal.MetaData.SingleBalanceMeta.ScoreFactorWave;
        if (WaveScore < 0) WaveScore = 0;
        _GameResultTimeText.text = TimeString(Time_);
        _GameResultGoldText.text = _AddGold.ToString();
        _GameResultBonusGoldText.text = "+" + _Gold.ToString();
        var Score = WaveScore + Time_ * CGlobal.MetaData.SingleBalanceMeta.ScoreFactorTime + AddGold_ * CGlobal.MetaData.SingleBalanceMeta.ScoreFactorGold;
        _GameResultScoreText.text = Score.ToString();

        if(Score >= CGlobal.LoginNetSc.User.SinglePointBest)
        {
            CGlobal.LoginNetSc.User.SinglePointBest = Score;
        }
        if(Time_ >= CGlobal.LoginNetSc.User.SingleSecondBest)
        {
            CGlobal.LoginNetSc.User.SingleSecondBest = Time_;
        }

        _GameResultReplayText.text = string.Format("{2}\n{0}/{1}", CGlobal.GetSinglePlayCountLeftTime().Item1,CGlobal.MetaData.SingleBalanceMeta.PlayCountMax, CGlobal.MetaData.GetText(EText.SceenSingle_Replay));
        AnalyticsManager.AddTutorialTracking();
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameEnd);
    }
    public void SkipResult()
    {
        _ResultStep = EResultStep.None;
        _ResultStepDelta = 0.0f;
        _GameResultSkipBtn.SetActive(false);
        _GameResultTimeText.gameObject.SetActive(true);
        _GameResultGoldText.gameObject.SetActive(true);
        _GameResultScoreText.gameObject.SetActive(true);
        _GameResultBonusGoldText.gameObject.SetActive(false);
        _GameResultGoldText.text = (_Gold + _AddGold).ToString();
        _GameResultBtn.SetActive(true);
        _GameResultRetryBtn.SetActive(true);
        _GameResultADBtn.SetActive(true);
        _GameResultADOffBtn.SetActive(true);
        _CSceneBattleSingle.ResultViewEnd();
    }
    private string TimeString(Int32 Time_)
    {
        if (Time_ < 0) Time_ = 0;
        string timeString = "";
        var min = Time_ / 60;
        var sec = Time_ % 60;
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
        return timeString;
    }
    private void Update()
    {
        if (_ResultStep != EResultStep.None)
        {
            _ResultStepDelta += Time.deltaTime;
            switch (_ResultStep)
            {
                case EResultStep.ViewBg:
                    if (_ResultStepDelta >= _ResultStepTerm)
                    {
                        _ResultStepDelta = 0.0f;
                        _ResultStep = EResultStep.ViewTime;
                        _GameResultTimeText.gameObject.SetActive(true);
                    }
                    break;
                case EResultStep.ViewTime:
                    if (_ResultStepDelta >= _ResultStepTerm)
                    {
                        _ResultStepDelta = 0.0f;
                        _ResultStep = EResultStep.ViewGold;
                        _GameResultGoldText.gameObject.SetActive(true);
                    }
                    break;
                case EResultStep.ViewGold:
                    if (_ResultStepDelta >= _ResultStepTerm)
                    {
                        _ResultStepDelta = 0.0f;
                        if (_Gold > 0)
                            _ResultStep = EResultStep.ViewScore;
                        else
                            _ResultStep = EResultStep.ViewButton;
                        _GameResultScoreText.gameObject.SetActive(true);
                    }
                    break;
                case EResultStep.ViewScore:
                    if (_ResultStepDelta >= _ResultStepTerm)
                    {
                        _ResultStepDelta = 0.0f;
                        _ResultStep = EResultStep.ViewBonus;
                        _GameResultBonusGoldText.gameObject.SetActive(true);
                    }
                    break;
                case EResultStep.ViewBonus:
                    if (_ResultStepDelta >= _ResultStepTerm)
                    {
                        _ResultStepDelta = 0.0f;
                        _ResultStep = EResultStep.ViewButton;
                        _GameResultBonusGoldText.gameObject.SetActive(false);
                        _GameResultGoldText.text = (_Gold + _AddGold).ToString();
                        _GameResultGoldEffect.SetActive(true);
                    }
                    break;
                case EResultStep.ViewButton:
                    if (_ResultStepDelta >= _ResultStepTerm)
                    {
                        SkipResult();
                    }
                    break;
            }
        }
    }
    public void SetJoyPadVisible(bool IsView_)
    {
        _JoyPadObject.gameObject.SetActive(IsView_);
    }
    public void ResultOKClick()
    {
        var Gold = _AddGold + _Gold;
        if (!IsAdview)
            CGlobal.NetControl.Send(new SSingleEndNetCs(_Wave, _Time, _AddGold, Gold, 0));
        else
            Gold *= 2;

        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.MusicStop();
        CGlobal.SceneSetNext(new CSceneLobby(Gold, 0,0));
    }
    public void ResultADClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.ADManager.ShowAdDodgeReward(() =>
        {
            var Gold = (_AddGold + _Gold) * 2;
            var SendPacket = new SSingleEndNetCs(_Wave, _Time, _AddGold, Gold, 0);
            if (!CGlobal.NetControl.IsLinked(0))
            {
                CGlobal.ADManager.SaveDelayPacket(ADManager.EDelayRewardType.DodgeReward, SendPacket);
            }
            else
            {
                IsAdview = true;
                _GameResultADEffect.gameObject.SetActive(true);
                _GameResultADEffect.text = string.Format("+{0}", (_AddGold + _Gold));
                _GameResultADBtn.SetActive(false);
                CGlobal.NetControl.Send(SendPacket);
            }
        });
        AnalyticsManager.TrackingEvent(ETrackingKey.ads_result_dodge);
    }
    public void ResultReplayClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        var SingleData = CGlobal.GetSinglePlayCountLeftTime();

        var Gold = _AddGold + _Gold;
        if (IsAdview)
            Gold *= 2;
        CGlobal.LoginNetSc.User.Resources[(Int32)EResource.Gold] += Gold;

        if (SingleData.Item1 > 0)
        {
            GameRestart();
        }
        else
        {
            CGlobal.SystemPopup.ShowCostResourcePopup(EText.GlobalPopup_Text_SingleCharge, EResource.Gold, CGlobal.MetaData.SingleBalanceMeta.ChargeCostGold, (PopupSystem.PopupBtnType type_) =>
            {
                if (type_ == PopupSystem.PopupBtnType.Ok)
                {
                    if (CGlobal.HaveCost(EResource.Gold, CGlobal.MetaData.SingleBalanceMeta.ChargeCostGold))
                    {
                        GameRestart();
                        AnalyticsManager.TrackingEvent(ETrackingKey.spend_gold_singlecharge);
                    }
                    else
                    {
                        CGlobal.ShowResourceNotEnough(EResource.Gold);
                        CGlobal.LoginNetSc.User.Resources[(Int32)EResource.Gold] -= Gold;
                    }
                }
                else
                {
                    CGlobal.LoginNetSc.User.Resources[(Int32)EResource.Gold] -= Gold;
                }
            });
        }
    }
    public void ExitClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (_GameResult.activeSelf == true)
        {
            ResultOKClick();
        }
        else
            CGlobal.SystemPopup.ShowPopup(EText.SceenSingle_ExitPopup, PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type_) => {
                if (type_ == PopupSystem.PopupBtnType.Ok)
                {
                    CGlobal.MusicStop();
                    CGlobal.SceneSetNext(new CSceneLobby());
                }
            });
    }
    public void ShowGameStart()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameStart);
        _GameStartView.SetActive(true);
    }
    public void HideGameStart()
    {
        _GameStartView.SetActive(false);
    }
    private void GameRestart()
    {
        if (!IsAdview)
            CGlobal.NetControl.Send(new SSingleEndNetCs(_Wave, _Time, _AddGold, _Gold + _AddGold, 0));

        CGlobal.NetControl.Send(new SSingleStartNetCs());
        CGlobal.SceneSetNext(new CSceneBattleSingle());
    }
}
