using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleIslandModeUI : MonoBehaviour
{

    [SerializeField] Text _GamePlayGold = null;
    [SerializeField] Text _GamePlayIsland = null;
    [SerializeField] Text _GamePlayScore = null;
    [SerializeField] Image _JoyPadObject = null;
    [SerializeField] Image _StaminaBar = null;

    [SerializeField] GameObject _GameResult = null;
    [SerializeField] Text _GameResultIslandText = null;
    [SerializeField] Text _GameResultGoldText = null;
    [SerializeField] Text _GameResultBonusGoldText = null;
    [SerializeField] Text _GameResultScoreText = null;
    [SerializeField] GameObject _GameResultBtn = null;
    [SerializeField] GameObject _GameResultRetryBtn = null;
    [SerializeField] GameObject _GameResultGoldEffect = null;
    [SerializeField] GameObject _GameResultADBtn = null;
    [SerializeField] Text _GameResultADEffect = null;
    [SerializeField] GameObject _GameResultADOffBtn = null;

    [SerializeField] Text _GameResultReplayText = null;

    [SerializeField] GameObject _JoyPad = null;
    [SerializeField] GameObject _GameStartView = null;

    [SerializeField] Text _BestPlayIsland = null;
    [SerializeField] GameObject _GameResultSkipBtn = null;

    [SerializeField] GameObject Tutorial = null;
    [SerializeField] Text TutorialText = null;
    [SerializeField] Toggle TutorialToggle = null;

    CSceneBattleSingleIsland _CSceneBattleSingleIsland = null;

    Int32 _AddGold = 0;
    Int32 _Gold = 0;
    Int32 _Island = 0;
    Int32 _TutorialCount = 0;

    bool IsAdview = false;

    float _ResultStepDelta = 0.0f;
    float _ResultStepTerm = 0.5f;
    enum EResultStep
    {
        None,
        ViewBg,
        ViewIsland,
        ViewGold,
        ViewScore,
        ViewBonus,
        ViewButton
    }
    EResultStep _ResultStep = EResultStep.None;

    EText[] TutorialTextList =
    {
        EText.GameTip_Island_Step01,
        EText.GameTip_Island_Step02,
        EText.GameTip_Island_Step03,
        EText.GameTip_Island_Step04,
        EText.GameTip_Island_Step05,
        EText.GameTip_Island_Step06,
        EText.GameTip_Island_Step07
    };

    public void Init(Int32 IslandCount_, Int32 BestCount_, CSceneBattleSingleIsland CSceneBattleSingleIsland_)
    {
        SetGoldCount(0);
        SetIslandCount(IslandCount_);
        SetBestIslandCount(BestCount_);
        _GamePlayScore.text = "0";
        SetScore(IslandCount_, 0);

        _CSceneBattleSingleIsland = CSceneBattleSingleIsland_;
        _GameResultSkipBtn.SetActive(false);

        _AddGold = 0;
        IsAdview = false;
        _GameResult.SetActive(false);
        _JoyPad.SetActive(CGlobal.GameOption.Data.IsPad);
        Tutorial.SetActive(false);
    }
    public void SetGoldCount(Int32 Gold_)
    {
        _GamePlayGold.text = Gold_.ToString();
    }
    public void SetIslandCount(Int32 Island_)
    {
        _GamePlayIsland.text = Island_.ToString();
    }
    public void SetBestIslandCount(Int32 Best_)
    {
        _BestPlayIsland.text = Best_.ToString();
    }
    public void SetScore(Int32 Island_, Int32 Gold_)
    {
        _GamePlayScore.text = ScoreText(Island_, Gold_);
    }
    public void ShowResult(Int32 Island_, Int32 Gold_, Int32 AddGold_)
    {
        _GameResult.SetActive(true);
        _GameResultSkipBtn.SetActive(true);
        _GameResultIslandText.gameObject.SetActive(false);
        _GameResultGoldText.gameObject.SetActive(false);
        _GameResultBonusGoldText.gameObject.SetActive(false);
        _GameResultScoreText.gameObject.SetActive(false);
        _GameResultGoldEffect.SetActive(false);
        _GameResultBtn.SetActive(false);
        _GameResultRetryBtn.SetActive(false);
        _GameResultADEffect.gameObject.SetActive(false);
        _GameResultADBtn.SetActive(false);
        _GameResultADOffBtn.SetActive(false);
        _ResultStep = EResultStep.ViewBg;
        _ResultStepDelta = 0.0f;
        _Gold = Gold_;
        _AddGold = AddGold_;
        _Island = Island_;

        _GameResultIslandText.text = Island_.ToString();
        _GameResultGoldText.text = _AddGold.ToString();
        _GameResultBonusGoldText.text = "+"+_Gold.ToString();
        _GameResultScoreText.text = ScoreText(Island_, AddGold_);

        var Score = (Island_ * CGlobal.MetaData.SingleIslandBalanceMeta.ScoreFactorIsland) + (AddGold_ * CGlobal.MetaData.SingleIslandBalanceMeta.ScoreFactorGold);
        if (Score >= CGlobal.LoginNetSc.User.IslandPointBest)
        {
            CGlobal.LoginNetSc.User.IslandPointBest = Score;
        }
        if (Island_ >= CGlobal.LoginNetSc.User.IslandPassedCountBest)
        {
            CGlobal.LoginNetSc.User.IslandPassedCountBest = Island_;
        }

        _GameResultBtn.SetActive(false);
        _GameResultReplayText.text = string.Format("{2}\n{0}/{1}", CGlobal.GetSingleIslandPlayCountLeftTime().Item1, CGlobal.MetaData.SingleIslandBalanceMeta.PlayCountMax, CGlobal.MetaData.GetText(EText.SceenSingle_Replay));
        AnalyticsManager.AddTutorialTracking();
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameEnd);
    }
    public void SkipResult()
    {
        _ResultStep = EResultStep.None;
        _ResultStepDelta = 0.0f;
        _GameResultSkipBtn.SetActive(false);
        _GameResultIslandText.gameObject.SetActive(true);
        _GameResultGoldText.gameObject.SetActive(true);
        _GameResultScoreText.gameObject.SetActive(true);
        _GameResultBonusGoldText.gameObject.SetActive(false);
        _GameResultGoldText.text = (_Gold + _AddGold).ToString();
        _GameResultBtn.SetActive(true);
        _GameResultRetryBtn.SetActive(true);
        _GameResultADBtn.SetActive(true);
        _GameResultADOffBtn.SetActive(true);
        _CSceneBattleSingleIsland.ResultViewEnd();
    }
    public string ScoreText(Int32 Island_, Int32 AddGold_)
    {
          return ((Island_* CGlobal.MetaData.SingleIslandBalanceMeta.ScoreFactorIsland) + (AddGold_ * CGlobal.MetaData.SingleIslandBalanceMeta.ScoreFactorGold)).ToString();
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
        if(_ResultStep != EResultStep.None)
        {
            _ResultStepDelta += Time.deltaTime;
            switch (_ResultStep)
            {
                case EResultStep.ViewBg:
                    if(_ResultStepDelta >= _ResultStepTerm)
                    { 
                        _ResultStepDelta = 0.0f;
                        _ResultStep = EResultStep.ViewIsland;
                        _GameResultIslandText.gameObject.SetActive(true);
                    }
                    break;
                case EResultStep.ViewIsland:
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
    public void SetStaminaBar(float Stamina_, float MaxStamina_)
    {
        //_StaminaBar.transform.localScale = new Vector3(Stamina_/MaxStamina_, 1.0f,1.0f);
        if(Stamina_ > MaxStamina_)
            Stamina_ = MaxStamina_;

        _StaminaBar.transform.localScale = new Vector3(Stamina_ / MaxStamina_, 1.0f, 1.0f);
    }
    public void SetJoyPadVisible(bool IsView_)
    {
        _JoyPadObject.gameObject.SetActive(IsView_);
    }
    public void ResultOKClick()
    {
        var Gold = _AddGold + _Gold;
        if (!IsAdview)
            CGlobal.NetControl.Send(new SIslandEndNetCs(_Island, _AddGold, Gold, 0));
        else
            Gold *= 2;

        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.SceneSetNext(new CSceneLobby(Gold, 0, 0));
    }
    public void ResultADClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.ADManager.ShowAdIslandReward(() =>
        {
            var Gold = (_AddGold + _Gold) * 2;
            var SendPacket = new SIslandEndNetCs(_Island, _AddGold, Gold, 0);
            if (!CGlobal.NetControl.IsLinked(0))
            {
                CGlobal.ADManager.SaveDelayPacket(ADManager.EDelayRewardType.IslandReward, SendPacket);
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
        AnalyticsManager.TrackingEvent(ETrackingKey.ads_result_island);
    }
    public void ResultReplayClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        var SingleData = CGlobal.GetSingleIslandPlayCountLeftTime();

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
            CGlobal.SystemPopup.ShowCostResourcePopup(EText.GlobalPopup_Text_SingleCharge, EResource.Gold, CGlobal.MetaData.SingleIslandBalanceMeta.ChargeCostGold, (PopupSystem.PopupBtnType type_) =>
            {
                if (type_ == PopupSystem.PopupBtnType.Ok)
                {
                    if (CGlobal.HaveCost(EResource.Gold, CGlobal.MetaData.SingleIslandBalanceMeta.ChargeCostGold))
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
            CGlobal.NetControl.Send(new SIslandEndNetCs(_Island, _AddGold, _Gold + _AddGold, 0));

        CGlobal.NetControl.Send(new SIslandStartNetCs());
        CGlobal.SceneSetNext(new CSceneBattleSingleIsland());
    }
    public void ViewTutorial()
    {
        if (PlayerPrefs.GetInt("ViewTutoriealIsland_" + CGlobal.UID.ToString(), 0) == 0)
        {
            _TutorialCount = 0;
            Tutorial.SetActive(true);
            TutorialText.text = CGlobal.MetaData.GetText(TutorialTextList[_TutorialCount]);
        }
        else
        {
            _CSceneBattleSingleIsland.TutorialClose();
        }
    }
    public void ViewTutorialResult()
    {
        _TutorialCount = 0;
        Tutorial.SetActive(true);
        TutorialText.text = CGlobal.MetaData.GetText(TutorialTextList[_TutorialCount]);
        TutorialToggle.gameObject.SetActive(false);
    }
    public void TutorialClose()
    {
        if(TutorialToggle.gameObject.activeSelf)
        {
            if (TutorialToggle.isOn)
            {
                PlayerPrefs.SetInt("ViewTutoriealIsland_" + CGlobal.UID.ToString(), 1);
                PlayerPrefs.Save();
            }
            _CSceneBattleSingleIsland.TutorialClose();
        }
        Tutorial.SetActive(false);
    }
    public void TutorialNext()
    {
        _TutorialCount++;
        if(_TutorialCount >= TutorialTextList.Length)
        {
            TutorialClose();
        }
        else
        {
            TutorialText.text = CGlobal.MetaData.GetText(TutorialTextList[_TutorialCount]);
        }
    }
}
