using bb;
using rso.core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaPopup : MonoBehaviour
{
    public enum EGachaType
    {
        Normal,
        Special
    }

    [SerializeField] GameObject NormalGachaBox = null;
    [SerializeField] GameObject SpecialGachaBox = null;
    [SerializeField] GameObject SpecialGachaBoxX10 = null;

    [SerializeField] GameObject NormalGachaCharParent = null;
    [SerializeField] GameObject SpecialGachaCharParent = null;
    [SerializeField] GameObject SpecialGachaCharParentX10 = null;

    [SerializeField] GameObject NormalGachaCapsule = null;
    [SerializeField] GameObject SpecialGachaCapsule = null;
    [SerializeField] GameObject SpecialGachaCapsuleX10 = null;

    [SerializeField] GameObject _CharacterInfo = null;
    [SerializeField] Text _CharacterInfoName = null;
    [SerializeField] Text _CharacterInfoGrade = null;
    [SerializeField] Text _CharacterInfoDesc = null;
    [SerializeField] Text _CharacterInfoOption = null;
    [SerializeField] Image _CharacterInfoBg = null;

    [SerializeField] Animator _NormalGachaAnimation = null;
    [SerializeField] Animator _SpecialGachaAnimation = null;
    [SerializeField] Animator _SpecialGachaX10Animation = null;
    [SerializeField] GameObject _SkipLayer = null;

    [SerializeField] GameObject[] _SkyStatusStars = null;
    [SerializeField] GameObject[] _LandStatusStars = null;
    [SerializeField] GameObject[] _StaminaStatusStars = null;
    [SerializeField] GameObject[] _PumpStatusStars = null;

    [SerializeField] GameObject _GachaOkBtn = null;
    [SerializeField] Image _CharacterLogo = null;

    [SerializeField] BackgroundUI _GachaBG = null;

    [SerializeField] CharacterPanel _OriginCharPanel = null;
    [SerializeField] KeyCoinCharacterPanel _OriginKeyCharPanel = null;
    [SerializeField] GameObject _GachaX10Parent = null;
    [SerializeField] GameObject _GachaX10BG = null;
    [SerializeField] GameObject _GachaX10OkBtn = null;

    List<GameObject> _GachaX10List = new List<GameObject>();

    GameObject _GachaBox = null;
    GameObject _GachaCharParent = null;
    GameObject _GachaCapsule = null;
    GameObject _GachaCharacter = null;
    EGachaType _EGachaType;
    Int32 _CharCode;
    List<Int32> _CharCodeList;
    List<Int32> _NewCharCodeList;

    Int32 RefundChar = 0;
    Int32 RefundKeycoin = 0;

    bool IsTen = false;

    bool DelayAnimation = false;
    TimePoint _DelayTime;
    public void ShowGachaAnimation(EGachaType EGachaType_, Int32 CharCode_)
    {
        IsTen = false;
        _EGachaType = EGachaType_;
        _CharCode = CharCode_;
        if (EGachaType_ == EGachaType.Normal)
        {
            _GachaBox = NormalGachaBox;
            _GachaCharParent = NormalGachaCharParent;
            _GachaCapsule = NormalGachaCapsule;
        }
        else if (EGachaType_ == EGachaType.Special)
        {
            _GachaBox = SpecialGachaBox;
            _GachaCharParent = SpecialGachaCharParent;
            _GachaCapsule = SpecialGachaCapsule;
        }

        _GachaX10BG.SetActive(false);
        _GachaX10OkBtn.SetActive(false);
        _GachaBox.SetActive(true);
        _CharacterInfo.SetActive(false);
        _GachaBG.DeactiveChangeBG();

        _CharacterInfoName.text = CGlobal.MetaData.GetCharacterName(CharCode_);
        _CharacterInfoGrade.text = CGlobal.MetaData.GetCharacterGrade(CharCode_);
        _CharacterInfoGrade.color = CGlobal.MetaData.GetCharacterGradeColor(CharCode_);
        _CharacterInfoDesc.text = CGlobal.MetaData.GetText(CGlobal.MetaData.Chars[CharCode_].ETextDescription);
        //_CharacterInfoOption.text = "승리시 획득골드 +3%";
        _CharacterInfoBg.sprite = Resources.Load<Sprite>(CGlobal.CharInfoBGTextrues[1]);
        _CharacterInfoOption.gameObject.SetActive(false);

        string LogoImageName = CGlobal.MetaData.Chars[CharCode_].DescriptionIcon;
        if (LogoImageName.Length > 0)
        {
            _CharacterLogo.sprite = Resources.Load<Sprite>("Textures/" + LogoImageName);
            _CharacterLogo.gameObject.SetActive(true);
        }
        else
            _CharacterLogo.gameObject.SetActive(false);

        // Model_Ch
        var Prefab = Resources.Load("Prefabs/Char/" + CGlobal.MetaData.Chars[CharCode_].PrefabName);
        Debug.Assert(Prefab != null);

        _GachaCharacter = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        _GachaCharacter.name = "Model_Ch";
        _GachaCharacter.transform.SetParent(_GachaCharParent.transform);
        _GachaCharacter.transform.localPosition = Vector3.zero;
        _GachaCharacter.transform.localScale = Vector3.one;
        _GachaCharacter.transform.localEulerAngles = new Vector3(0, 0, 0);

        if(CGlobal.MetaData.Chars[CharCode_].Grade == EGrade.Normal)
            _GachaCapsule.GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("Gacha/TM_GachaBox_D01");
        else if(CGlobal.MetaData.Chars[CharCode_].Grade == EGrade.Rare)
            _GachaCapsule.GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("Gacha/TM_GachaBox_D03");
        else if (CGlobal.MetaData.Chars[CharCode_].Grade == EGrade.Epic)
            _GachaCapsule.GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("Gacha/TM_GachaBox_D02");

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

        _SkipLayer.SetActive(true);
        DelayAnimation = false;
    }
    public void ShowGachaAnimation(EGachaType EGachaType_, List<Int32> CharCodeList_, List<Int32> NewCharCodeList_)
    {
        IsTen = true;
        _EGachaType = EGachaType_;
        _CharCodeList = CharCodeList_;
        _NewCharCodeList = NewCharCodeList_;

        _GachaBox = SpecialGachaBoxX10;
        _GachaCharParent = _GachaX10Parent;
        _GachaCapsule = SpecialGachaCapsuleX10;

        _GachaBox.SetActive(true);
        _CharacterInfo.SetActive(false);
        _GachaX10BG.SetActive(false);
        _GachaBG.DeactiveChangeBG();
        _CharacterInfoBg.sprite = Resources.Load<Sprite>(CGlobal.CharInfoBGTextrues[1]);

        _SkipLayer.SetActive(true);
        DelayAnimation = false;
    }
    public void GachaAnimationEnd()
    {
        _SkipLayer.SetActive(false);
        _CharacterInfo.SetActive(true);
        _GachaOkBtn.SetActive(false);
        _GachaX10BG.SetActive(false);
        _GachaX10OkBtn.SetActive(false);
        _DelayTime = CGlobal.GetServerTimePoint();
        DelayAnimation = true;
    }
    public void GachaX10AnimationEnd()
    {
        _SkipLayer.SetActive(false);
        _CharacterInfo.SetActive(false);
        _GachaOkBtn.SetActive(false);
        _GachaX10BG.SetActive(true);
        _GachaX10OkBtn.SetActive(true);
        CGlobal.Sound.PlayOneShot((Int32)ESound.GetChar);

        RefundChar = 0;
        RefundKeycoin = 0;
        List<Int32> NewRefundCharCodeList = new List<int>();
        foreach (var i in _CharCodeList)
        {
            var CharMeta = CGlobal.MetaData.Chars[i];
            if (_NewCharCodeList.Contains(i) && !NewRefundCharCodeList.Contains(i))
            {
                NewRefundCharCodeList.Add(i);
                var Panel = UnityEngine.Object.Instantiate<CharacterPanel>(_OriginCharPanel);
                Panel.InitPanel(i, false);
                Panel.transform.SetParent(_GachaCharParent.transform);
                Panel.transform.localScale = Vector3.one;
                _GachaX10List.Add(Panel.gameObject);
            }
            else
            {
                var Panel = UnityEngine.Object.Instantiate<KeyCoinCharacterPanel>(_OriginKeyCharPanel);
                Panel.Init(i, CharMeta.CPRefund);
                Panel.transform.SetParent(_GachaCharParent.transform);
                Panel.transform.localScale = Vector3.one;
                _GachaX10List.Add(Panel.gameObject);
                RefundChar++;
                RefundKeycoin += CharMeta.CPRefund;
            }
        }
        _GachaBG.ActiveChangeBG();
    }
    public void OnClickOk()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (_GachaCharacter != null)
            Destroy(_GachaCharacter);

        _GachaCharacter = null;
        gameObject.SetActive(false);
        _GachaBox.SetActive(false);
        var Scene = CGlobal.GetScene<CSceneShop>();
        Scene.InvisibleGachaAnimation();
    }
    public void OnClickX10Ok()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        foreach (var i in _GachaX10List)
        {
            Destroy(i);
        }
        _GachaX10List.Clear();

        _GachaBG.DeactiveChangeBG();
        gameObject.SetActive(false);
        _GachaBox.SetActive(false);
        var Scene = CGlobal.GetScene<CSceneShop>();
        Scene.InvisibleGachaAnimation();
        if(RefundChar > 0)
            CGlobal.SystemPopup.ShowPopup(string.Format(CGlobal.MetaData.GetText(EText.ShopScene_GachaOverlapText), RefundChar, RefundKeycoin), PopupSystem.PopupType.Confirm, null);
    }

    public void GachaCharWin()
    {
        _SkipLayer.SetActive(false);
        CGlobal.Sound.PlayOneShot((Int32)ESound.GetChar);
        var GachaModel = _GachaCharacter.GetComponent<Model_Ch>();
        GachaModel.Win();
        _CharacterInfoBg.sprite = Resources.Load<Sprite>(CGlobal.CharInfoBGTextrues[(Int32)CGlobal.MetaData.Chars[_CharCode].Grade]);
    }
    public void Skip()
    {
        _SkipLayer.SetActive(false);
        if (_EGachaType == EGachaType.Normal)
            _NormalGachaAnimation.Play("Anim_GachaBox_Skip01");
        else if (_EGachaType == EGachaType.Special)
        {
            if(IsTen)
                _SpecialGachaX10Animation.Play("Anim_GachaBox_SP_Skip01_X10");
            else
                _SpecialGachaAnimation.Play("Anim_GachaBox_SP_Skip01");
        }
    }
    private void Update()
    {
        if(DelayAnimation)
        {
            Int32 time = Mathf.CeilToInt((float)(CGlobal.GetServerTimePoint() - _DelayTime).TotalSeconds);
            if (time > 1)
            {
                DelayAnimation = false;
                var Scene = CGlobal.GetScene<CSceneShop>();
                Scene.ViewRefundPopup();
            }
        }
        if (rso.unity.CBase.BackPushed())
        {
            if (_CharacterInfo.activeSelf)
                OnClickOk();
            else if (_GachaX10BG.activeSelf)
                OnClickX10Ok();
            else if (_SkipLayer.activeSelf)
                Skip();
        }
    }
}
