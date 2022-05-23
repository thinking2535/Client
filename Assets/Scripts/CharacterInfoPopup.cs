using Firebase.Analytics;
using rso.unity;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoPopup : MonoBehaviour
{
    [SerializeField] UIUserCharacter _UserCharacter = null;
    [SerializeField] Text _CharacterInfoName = null;
    [SerializeField] Text _CharacterInfoGrade = null;
    [SerializeField] Text _CharacterInfoDesc = null;
    [SerializeField] Text _CharacterInfoOption = null;
    [SerializeField] Canvas[] _ParentCanvases = null;
    [SerializeField] GameObject _SelectBtn = null;
    [SerializeField] GameObject _UnlockBtn = null;
    [SerializeField] Image _CostType = null;
    [SerializeField] Text _Cost = null;
    [SerializeField] GameObject _EffectGameObject = null;
    [SerializeField] GameObject[] _SkyStatusStars = null;
    [SerializeField] GameObject[] _LandStatusStars = null;
    [SerializeField] GameObject[] _StaminaStatusStars = null;
    [SerializeField] GameObject[] _PumpStatusStars = null;

    [SerializeField] Image _CharacterLogo = null;
    [SerializeField] GameObject _Disbtn = null;
    [SerializeField] GameObject _Skipbtn = null;

    private Camera _MainCamera = null;

    CInputTouch _Input = null;

    private float BeforeAngle = 0.0f;

    public Int32 CharCode = 0;

    bool _IsCharacterInfo = false;
    float SkinPopupPosXMin = 2.3f;
    float SkinPopupPosXMax = 7.9f;
    float SkinPopupPosYMin = -4.4f;
    float SkinPopupPosYMax = -2.9f;

    void _ScrollCallback(CInputTouch.EState State_, Vector2 DownPos_, Vector2 Diff_, Vector2 Delta_)
    {
        if (State_ == CInputTouch.EState.Move)
        {
            var NowPosX = (DownPos_.x + Diff_.x);
            var NowPosY = (DownPos_.y + Diff_.y);
            var NowPos = new Vector3(NowPosX, NowPosY, 10.0f);
            var DownPos = new Vector3(DownPos_.x, DownPos_.y, 10.0f);
            var WorldPointNow = _MainCamera.ScreenToWorldPoint(NowPos);
            var WorldPointDown = _MainCamera.ScreenToWorldPoint(DownPos);
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
        else if (State_ == CInputTouch.EState.Up)
        {
            BeforeAngle = 0.0f;
        }
    }
    private void Init(Int32 CharCode_, Camera MainCamera_)
    {
        if (!_IsCharacterInfo)
        {
            if (_Input == null)
            {
                _Input = new CInputTouch();
                _Input.Add(new CInputTouch.CObjectScroll((Vector2) => { return true; }, _ScrollCallback));
            }
            _MainCamera = MainCamera_;
            CharCode = CharCode_;

            _CharacterInfoName.text = CGlobal.MetaData.GetCharacterName(CharCode_);
            _CharacterInfoGrade.text = CGlobal.MetaData.GetCharacterGrade(CharCode_);
            _CharacterInfoGrade.color = CGlobal.MetaData.GetCharacterGradeColor(CharCode_);
            _CharacterInfoDesc.text = CGlobal.MetaData.GetText(CGlobal.MetaData.Chars[CharCode_].ETextDescription);
            //_CharacterInfoOption.text = "승리시 획득골드 +3%";
            _CharacterInfoOption.gameObject.SetActive(false);

            MakeCharacter(CGlobal.MetaData.Chars[CharCode_].PrefabName);

            string LogoImageName = CGlobal.MetaData.Chars[CharCode_].DescriptionIcon;
            if(LogoImageName.Length > 0)
            {
                _CharacterLogo.sprite = Resources.Load<Sprite>("Textures/"+ LogoImageName);
                _CharacterLogo.gameObject.SetActive(true);
            }
            else
                _CharacterLogo.gameObject.SetActive(false);

            gameObject.SetActive(true);
            foreach (var Obj in _ParentCanvases)
                Obj.gameObject.SetActive(false);

            for (int i = 0; i < CGlobal.MaxStatusStarsCount; ++i)
            {
                _SkyStatusStars[i].SetActive(false);
                _LandStatusStars[i].SetActive(false);
                _StaminaStatusStars[i].SetActive(false);
                _PumpStatusStars[i].SetActive(false);
            }
            for (int i = 0; i < CGlobal.MetaData.Chars[CharCode_].SkyStatus; ++i)
                _SkyStatusStars[i].SetActive(true);
            for (int i = 0; i < CGlobal.MetaData.Chars[CharCode_].LandStatus; ++i)
                _LandStatusStars[i].SetActive(true);
            for (int i = 0; i < CGlobal.MetaData.Chars[CharCode_].StaminaStatus; ++i)
                _StaminaStatusStars[i].SetActive(true);
            for (int i = 0; i < CGlobal.MetaData.Chars[CharCode_].PumpStatus; ++i)
                _PumpStatusStars[i].SetActive(true);
        }
    }
    public void ShowCharacterInfoPopup(Int32 CharCode_, Camera MainCamera_)
    {
        Init(CharCode_, MainCamera_);
        _UserCharacter.transform.localEulerAngles = new Vector3(0, -140, 0);
        SetInfoButton();
    }
    public void ShowGetPopup(Int32 CharCode_, Camera MainCamera_, bool IsCharacterInfo_)
    {
        _IsCharacterInfo = IsCharacterInfo_;
        Init(CharCode_, MainCamera_);
        _UserCharacter.Win();
        _UserCharacter.transform.localEulerAngles = new Vector3(0, -180, 0);
        _EffectGameObject.SetActive(true);
        _SelectBtn.SetActive(false);
        _UnlockBtn.SetActive(false);
        _Disbtn.SetActive(false);
        _Skipbtn.SetActive(true);
    }
    public void SetInfoButton()
    {
        _Disbtn.SetActive(true);
        if (_IsCharacterInfo)
        {
            _SelectBtn.SetActive(false);
            _UnlockBtn.SetActive(false);
        }
        else
        {
            _EffectGameObject.SetActive(false);
            if (CGlobal.LoginNetSc.Chars.Contains(CharCode) == false)
            {
                var CharMeta = CGlobal.MetaData.Chars[CharCode];
                _UnlockBtn.SetActive(true);
                _CostType.sprite = Resources.Load<Sprite>(CGlobal.GetResourcesIconFile(CharMeta.Cost_Type));
                _Cost.text = CharMeta.Price.ToString();
                _SelectBtn.SetActive(false);
            }
            else
            {
                _SelectBtn.SetActive(true);
                _UnlockBtn.SetActive(false);
            }
        }
    }

    public void SelectedCharacter()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (CGlobal.LoginNetSc.Chars.Contains(CharCode) == false)
        {
            CGlobal.SystemPopup.ShowPopup(EText.CharacterSelectScene_Select_NotHave, PopupSystem.PopupType.Confirm, true);
            return;
        }

        if (CGlobal.LoginNetSc.User.SelectedCharCode != CharCode)
        {
            SelectCharacter();
        }

        var Scene = CGlobal.GetScene<CSceneLobby>();
        if (Scene == null)
        {
            CGlobal.RedDotControl.SetReddotDeleteChar(CharCode);
            CGlobal.SceneSetNext(new CSceneLobby());
        }
        else
        {
            gameObject.SetActive(false);
            Scene.MakeCharacter();
        }
        foreach (var Obj in _ParentCanvases)
            Obj.gameObject.SetActive(true);
    }
    private void SelectCharacter()
    {
        CGlobal.NetControl.Send(new SSelectCharNetCs(CharCode));
        CGlobal.LoginNetSc.User.SelectedCharCode = CharCode;
    }
    public void UnlockCharacter()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        var CharMeta = CGlobal.MetaData.Chars[CharCode];
        if (CGlobal.HaveCost(CharMeta.Cost_Type, CharMeta.Price))
        {
            CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);
            CGlobal.NetControl.Send(new SBuyCharNetCs(CharCode));
            AnalyticsManager.TrackingEvent(ETrackingKey.spend_keycoin);
        }
        else
            CGlobal.ShowResourceNotEnough(CharMeta.Cost_Type);
    }
    public void OnClickBack()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        _Skipbtn.SetActive(false);
        if (_IsCharacterInfo)
        {
            _IsCharacterInfo = false;
            _EffectGameObject.SetActive(false);
            _UserCharacter.Stop();
            _UserCharacter.transform.localEulerAngles = new Vector3(0, -140, 0);
            SetInfoButton();
        }
        else
        {
            CGlobal.RedDotControl.SetReddotDeleteChar(CharCode);

            gameObject.SetActive(false);
            foreach (var Obj in _ParentCanvases)
                Obj.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if(!_EffectGameObject.activeSelf)
            _Input.Update();
    }
    public void ResourcesUpdate()
    {
        ShowGetPopup(CharCode, _MainCamera, true);
    }
    private void MakeCharacter(string PrefabName_)
    {
        _UserCharacter.DeleteCharacter();
        _UserCharacter.MakeCharacter(PrefabName_);
    }
    public void CharAniWin()
    {
        _UserCharacter.Win();
    }
    public void CharAniLose()
    {
        _UserCharacter.Lose();
    }
    public void CharAniStop()
    {
        _UserCharacter.Stop();
    }
}
