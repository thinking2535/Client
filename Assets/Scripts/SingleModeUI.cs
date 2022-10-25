using bb;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SingleModeUI : MonoBehaviour
{
    enum EResultStep
    {
        none,
        startAnimatingScore,
        animatingScore,
        startAnimatingGold,
        animatingGold,
        startAnimatingScoreToGold,
        animatingScoreToGold,
    }

    [SerializeField] Button _exitButton;
    [SerializeField] Text _title;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _scoreValue;
    [SerializeField] Image _goldIcon;
    [SerializeField] Text _goldText;
    [SerializeField] GameObject _resultPopup;
    [SerializeField] Text _resultScoreText;
    [SerializeField] Image _resultGoldIcon;
    [SerializeField] Text _resultGoldText;
    [SerializeField] Button _retryButton;
    [SerializeField] Button _okButton;
    [SerializeField] Text _retryButtonText;
    [SerializeField] Text _okButtonText;
    [SerializeField] GameObject _gameStartObject;
    [SerializeField] Button _skipButton;

    float _nextStepTime = 0.0f;
    const float _stepDuration = 1.0f;

    EResultStep _resultStep = EResultStep.none;
    Action _fShowExitPopup;
    Action _fReplayButtonClicked;
    Action _fOkButtonClicked;
    Func<string> _fGetRetryString;
    Int32 _scorePerGold;
    Int32 _score = 0;
    Int32 _gold = 0;
    float _scoreToGoldAnimationStartedTime = 0.0f;

    void Awake()
    {
        _exitButton.onClick.AddListener(_showExitPopup);
        _skipButton.onClick.AddListener(skipResult);
        _retryButton.onClick.AddListener(_retry);
        _okButton.onClick.AddListener(_oK);
        _title.text = CGlobal.MetaData.getText(EText.ResultScene_Text_Result);
        _scoreText.text = CGlobal.MetaData.getText(EText.SceenSingle_Score);
        _goldIcon.sprite = CGlobal.GetResourceSprite(EResource.Gold);
        _resultGoldIcon.sprite = CGlobal.GetResourceSprite(EResource.Gold);
    }
    void Update()
    {
        _updateResult(false);
    }
    void OnDestroy()
    {
        _okButton.onClick.RemoveAllListeners();
        _retryButton.onClick.RemoveAllListeners();
        _skipButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }
    public void Init(
        Action fShowExitPopup,
        Action fReplayButtonClicked_,
        Action fOkButtonClicked_,
        Func<string> fGetRetryString,
        Int32 scorePerGold,
        Int32 score,
        Int32 gold)
    {
        _fShowExitPopup = fShowExitPopup;
        _fReplayButtonClicked = fReplayButtonClicked_;
        _fOkButtonClicked = fOkButtonClicked_;
        _fGetRetryString = fGetRetryString;
        _scorePerGold = scorePerGold;

        setScore(score);
        setGold(gold);
    }
    public void setScore(Int32 score)
    {
        _score = score;
        _scoreValue.text = score.ToString();
    }
    public void setGold(Int32 gold)
    {
        _gold = gold;
        _goldText.text = gold.ToString();
    }
    public void ShowResultPopup()
    {
        HideGameStart();
        _resultPopup.SetActive(true);
        _resultGoldText.gameObject.SetActive(false);
        _resultScoreText.gameObject.SetActive(false);
        _okButton.gameObject.SetActive(false);
        _retryButton.gameObject.SetActive(false);
        _resultGoldText.text = _gold.ToString();
        _resultScoreText.text = _score.ToString();

        _retryButtonText.text = _fGetRetryString();
        _okButtonText.text = CGlobal.MetaData.getText(EText.Global_Button_Ok);
        AnalyticsManager.AddTutorialTracking();
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameEnd);

        _resultStep = EResultStep.startAnimatingScore;
    }
    public bool isPlayingResult()
    {
        return _resultStep != EResultStep.none;
    }
    public void skipResult()
    {
        _updateResult(true);
    }
    void _updateResult(bool skip)
    {
        if (!isPlayingResult())
            return;

        switch (_resultStep)
        {
            case EResultStep.startAnimatingScore:
                _resultScoreText.gameObject.SetActive(true);
                _nextStepTime = Time.time + _stepDuration;
                _resultStep = EResultStep.animatingScore;
                break;

            case EResultStep.animatingScore:
                if (skip || _nextStepTime <= Time.time)
                    _resultStep = EResultStep.startAnimatingGold;

                break;

            case EResultStep.startAnimatingGold:
                _resultGoldText.gameObject.SetActive(true);
                _nextStepTime += _stepDuration;
                _resultStep = EResultStep.animatingGold;
                break;

            case EResultStep.animatingGold:
                if (skip || _nextStepTime <= Time.time)
                    _resultStep = EResultStep.startAnimatingScoreToGold;

                break;

            case EResultStep.startAnimatingScoreToGold:
                _scoreToGoldAnimationStartedTime = Time.time;
                _resultStep = EResultStep.animatingScoreToGold;
                break;

            case EResultStep.animatingScoreToGold:
                {
                    Int32 scoreAnimated;

                    if (skip)
                    {
                        scoreAnimated = _score;
                    }
                    else
                    {
                        var elapsedTime = Time.time - _scoreToGoldAnimationStartedTime;
                        scoreAnimated = (Int32)(elapsedTime * 1000);

                        if (scoreAnimated > _score)
                            scoreAnimated = _score;
                    }

                    _resultScoreText.text = (_score - scoreAnimated).ToString();
                    _resultGoldText.text = (_gold + scoreAnimated / _scorePerGold).ToString();

                    if (skip || scoreAnimated == _score)
                    {
                        _okButton.gameObject.SetActive(true);
                        _retryButton.gameObject.SetActive(true);
                        _skipButton.gameObject.SetActive(false);
                        _resultStep = EResultStep.none;
                    }
                }
                break;
        }
    }
    public void _retry()
    {
        _fReplayButtonClicked();
    }
    public void _oK()
    {
        _fOkButtonClicked();
    }
    public void ShowGameStart()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.GameStart);
        _gameStartObject.SetActive(true);
    }
    public void HideGameStart()
    {
        _gameStartObject.SetActive(false);
    }
    public void _showExitPopup()
    {
        _fShowExitPopup();
    }
}
