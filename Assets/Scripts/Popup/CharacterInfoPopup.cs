using bb;
using rso.unity;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoPopup : ModalDialog
{
    [SerializeField] Button _BackButton;
    [SerializeField] UIUserCharacter _UserCharacter = null;
    [SerializeField] Text _StatusText;
    [SerializeField] Image _CharacterInfoBG = null;
    [SerializeField] Text _CharacterInfoName = null;
    [SerializeField] Text _CharacterInfoGrade = null;
    [SerializeField] Text _CharacterInfoDesc = null;
    [SerializeField] Text _CharacterInfoOption = null;
    [SerializeField] GameObject _havingStateButtonGroup;
    [SerializeField] Button _selectionButton;
    [SerializeField] GameObject _notHavingStateButtonGroup;
    [SerializeField] Button _buyingButton;
    [SerializeField] Button _stopCelebrationButton;
    [SerializeField] Image _CostType = null;
    [SerializeField] Text _Cost = null;
    [SerializeField] Text _notificationText;
    [SerializeField] GameObject _EffectGameObject = null;
    [SerializeField] GameObject[] _SkyStatusStars = null;
    [SerializeField] GameObject[] _LandStatusStars = null;
    [SerializeField] GameObject[] _StaminaStatusStars = null;
    [SerializeField] GameObject[] _PumpStatusStars = null;
    [SerializeField] GameObject _nftInfo;
    [SerializeField] Button _animateStopButton;
    [SerializeField] Button _animateWinButton;
    [SerializeField] Button _animateLoseButton;

    InputTouch _Input = new InputTouch();
    private float BeforeAngle = 0.0f;
    SCharacterMeta _characterMeta;

    float SkinPopupPosXMin = 2.3f;
    float SkinPopupPosXMax = 7.9f;
    float SkinPopupPosYMin = -4.4f;
    float SkinPopupPosYMax = -2.9f;

    protected override void Awake()
    {
        base.Awake();

        _BackButton.onClick.AddListener(_backButtonClicked);
        _Input.add(new InputTouch.Scroller((Vector2) => { return true; }, _ScrollCallback));
        _selectionButton.onClick.AddListener(_selectCharacter);
        _selectionButton.GetComponentInChildren<Text>().text = CGlobal.MetaData.getText(EText.CharacterSelectScene_Select);
        _buyingButton.onClick.AddListener(_buyCharacter);
        _stopCelebrationButton.onClick.AddListener(_stopCelebration);

        _animateStopButton.onClick.AddListener(_animateStop);
        _animateWinButton.onClick.AddListener(_animateWin);
        _animateLoseButton.onClick.AddListener(_animateLose);
    }
    protected override void Update()
    {
        base.Update();

        if (!_EffectGameObject.activeSelf)
            _Input.update();
    }
    protected override void OnDestroy()
    {
        _animateLoseButton.onClick.RemoveAllListeners();
        _animateWinButton.onClick.RemoveAllListeners();
        _animateStopButton.onClick.RemoveAllListeners();

        _BackButton.onClick.RemoveAllListeners();
        _stopCelebrationButton.onClick.RemoveAllListeners();
        _buyingButton.onClick.RemoveAllListeners();
        _selectionButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public void init(SCharacterMeta characterMeta, bool isCelebration, bool isRefunded)
    {
        _characterMeta = characterMeta;
        _CostType.sprite = _characterMeta.getCostTypeSprite();
        _Cost.text = _characterMeta.CostValue.ToString();

        _StatusText.text = CGlobal.MetaData.getText(EText.MultiScne_Popup_Info);
        _CharacterInfoBG.sprite = characterMeta.getGradeInfo().sprite;
        _CharacterInfoName.text = _characterMeta.GetText();
        _CharacterInfoDesc.text = _characterMeta.GetDescriptionText();

        _CharacterInfoGrade.text = _characterMeta.getGradeText();
        _CharacterInfoGrade.color = _characterMeta.getGradeInfo().color;

        //_CharacterInfoOption.text = "승리시 획득골드 +3%";
        _CharacterInfoOption.gameObject.SetActive(false);

        MakeCharacter(_characterMeta.PrefabName);

        for (int i = 0; i < _SkyStatusStars.Length; ++i)
        {
            _SkyStatusStars[i].SetActive(false);
            _LandStatusStars[i].SetActive(false);
            _StaminaStatusStars[i].SetActive(false);
            _PumpStatusStars[i].SetActive(false);
        }
        for (int i = 0; i < _characterMeta.SkyStatus; ++i)
            _SkyStatusStars[i].SetActive(true);
        for (int i = 0; i < _characterMeta.LandStatus; ++i)
            _LandStatusStars[i].SetActive(true);
        for (int i = 0; i < _characterMeta.StaminaStatus; ++i)
            _StaminaStatusStars[i].SetActive(true);
        for (int i = 0; i < _characterMeta.PumpStatus; ++i)
            _PumpStatusStars[i].SetActive(true);

        if (isCelebration)
        {
            _startCelebration();
            CGlobal.RedDotControl.SetReddotDeleteChar(_characterMeta.Code);
        }
        else
        {
            _stopCelebration();
        }

        if (_characterMeta.canBuy())
        {
            _buyingButton.gameObject.SetActive(true);
        }
        else if (_characterMeta.isRewardCharacter())
        {
            if (isRefunded)
                _notificationText.text = CGlobal.MetaData.getTextWithParameters(EText.AlreadyHaveCharacterAndRefund_0Value_1Type, characterMeta.RefundValue, characterMeta.RefundType);
            else if (!_doesHaveCharacter())
                _notificationText.text = CGlobal.MetaData.getTextWithParameters(EText.YouCanGetThisAsReward);
        }

        if (_characterMeta.isNFTCharacter())
            _nftInfo.SetActive(true);

        _setHavingState(_doesHaveCharacter());
        _setSelectionState(CGlobal.LoginNetSc.User.SelectedCharCode == _characterMeta.Code);
    }
    bool _doesHaveCharacter()
    {
        return CGlobal.LoginNetSc.doesHaveCharacter(_characterMeta.Code);
    }
    public void buyCharacterNetSc()
    {
        _startCelebration();
        _setHavingState(true);
    }
    void _startCelebration()
    {
        _UserCharacter.Win();
        _UserCharacter.transform.localEulerAngles = new Vector3(0, -180, 0);
        _EffectGameObject.SetActive(true);
        _stopCelebrationButton.gameObject.SetActive(true);
    }
    void _stopCelebration()
    {
        _stopCelebrationButton.gameObject.SetActive(false);
        _EffectGameObject.SetActive(false);
        _UserCharacter.transform.localEulerAngles = new Vector3(0, -140, 0);
        _UserCharacter.Stop();
    }
    void _setHavingState(bool doesHave)
    {
        _havingStateButtonGroup.SetActive(doesHave);
        _notHavingStateButtonGroup.SetActive(!doesHave);
    }
    void _setSelectionState(bool isSelected)
    {
        _selectionButton.gameObject.SetActive(!isSelected);
    }
    async void _backButtonClicked()
    {
        await backButtonPressed();
    }
    void _ScrollCallback(InputTouch.TouchState State_, Vector2 DownPos_, Vector2 Diff_, Vector2 Delta_)
    {
        if (State_ == InputTouch.TouchState.move)
        {
            var NowPosX = (DownPos_.x + Diff_.x);
            var NowPosY = (DownPos_.y + Diff_.y);
            var NowPos = new Vector3(NowPosX, NowPosY, 10.0f);
            var DownPos = new Vector3(DownPos_.x, DownPos_.y, 10.0f);

            var WorldPointNow = canvas.worldCamera.ScreenToWorldPoint(NowPos);
            var WorldPointDown = canvas.worldCamera.ScreenToWorldPoint(DownPos);
            var WorldDistance = WorldPointNow.x - WorldPointDown.x;

            if(SkinPopupPosXMin > WorldPointDown.x || 
               SkinPopupPosXMax < WorldPointDown.x || 
               SkinPopupPosYMin > WorldPointDown.y || 
               SkinPopupPosYMax < WorldPointDown.y)
            {
                var Angle = (WorldDistance / CGlobal.CircleRound * 360.0f);
                _UserCharacter.transform.localEulerAngles = new Vector3(0.0f, _UserCharacter.transform.localEulerAngles.y - (Angle - BeforeAngle), 0.0f);
                BeforeAngle = Angle;
            }
        }
        else if (State_ == InputTouch.TouchState.up)
        {
            BeforeAngle = 0.0f;
        }
    }
    void _selectCharacter()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        CGlobal.NetControl.Send(new SSelectCharNetCs(_characterMeta.Code));
        CGlobal.LoginNetSc.User.SelectedCharCode = _characterMeta.Code;
        _setSelectionState(true);
    }
    public async void _buyCharacter()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        if (CGlobal.doesHaveCost(_characterMeta.getCost()))
        {
            CGlobal.ProgressCircle.Activate();
            CGlobal.NetControl.Send(new SBuyCharNetCs(_characterMeta.Code));
            AnalyticsManager.TrackingEvent(ETrackingKey.spend_keycoin);
        }
        else
        {
            var costType = _characterMeta.typeMeta.getCostType();
            if (costType != EResource.Null)
                await GlobalFunction.ShowResourceNotEnough(costType);
        }
    }
    private void MakeCharacter(string PrefabName_)
    {
        _UserCharacter.DeleteCharacter();
        _UserCharacter.MakeCharacter(PrefabName_);
    }
    void _animateStop()
    {
        _UserCharacter.Stop();
    }
    void _animateWin()
    {
        _UserCharacter.Win();
    }
    void _animateLose()
    {
        _UserCharacter.Lose();
    }
}
