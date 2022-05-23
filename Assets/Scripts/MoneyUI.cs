using rso.core;
using rso.unity;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] Text _Ticket = null;
    [SerializeField] Text _Gold = null;
    [SerializeField] Text _Dia = null;
    [SerializeField] Text _CP = null;
    [SerializeField] GameObject[] _Icons = null;
    [SerializeField] GameObject[] _EffectIcon = null;
    [SerializeField] GameObject[] _EffectBGIcon = null;
    [SerializeField] GameObject _EffectParent = null;
    [SerializeField] Transform _EffectMinPos = null;
    [SerializeField] Transform _EffectMaxPos = null;
    [SerializeField] string _SceneString = ""; //이전 Scene 클래스 명
    [SerializeField] bool _IsPopupSetting = false;
    [SerializeField] GameObject _SettingPopup = null;
    [SerializeField] Image _MusicBtnOn = null;
    [SerializeField] Image _MusicBtnOff = null;
    [SerializeField] Image _SoundBtnOn = null;
    [SerializeField] Image _SoundBtnOff = null;
    [SerializeField] Image _PadBtnOn = null;
    [SerializeField] Image _PadBtnOff = null;

    private List<ResourceIcon> ResourceIcons = new List<ResourceIcon>();
    Int32[] _Resources = new Int32[(Int32)EResource.Max];
    bool IsResourceEffect = false;
    float SoundDeltaTime = 0.0f;
    private void Awake()
    {
        BtnToggle(_MusicBtnOn, _MusicBtnOff, CGlobal.GameOption.Data.IsMusic);
        BtnToggle(_SoundBtnOn, _SoundBtnOff, CGlobal.GameOption.Data.IsSound);
        BtnToggle(_PadBtnOn, _PadBtnOff, CGlobal.GameOption.Data.IsPad);
        _SettingPopup.SetActive(false);
    }
    public void MusicClick()
    {
        CGlobal.SetIsMusic(!CGlobal.GameOption.Data.IsMusic);
        BtnToggle(_MusicBtnOn, _MusicBtnOff, CGlobal.GameOption.Data.IsMusic);
    }
    public void SoundClick()
    {
        CGlobal.SetIsSound(!CGlobal.GameOption.Data.IsSound);
        BtnToggle(_SoundBtnOn, _SoundBtnOff, CGlobal.GameOption.Data.IsSound);
    }
    public void PadClick()
    {
        CGlobal.SetIsPad(!CGlobal.GameOption.Data.IsPad);
        BtnToggle(_PadBtnOn, _PadBtnOff, CGlobal.GameOption.Data.IsPad);
    }
    private void BtnToggle(Image On_, Image Off_, bool Value_)
    {
        if (Value_)
        {
            CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
            On_.gameObject.SetActive(true);
            Off_.gameObject.SetActive(false);
        }
        else
        {
            CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
            On_.gameObject.SetActive(false);
            Off_.gameObject.SetActive(true);
        }
    }

    public void SetResources(Int32[] Resources_)
    {
        SetTicket(Resources_[(Int32)EResource.Ticket]);
        SetGold(Resources_[(Int32)EResource.Gold]);
        SetDia(Resources_[(Int32)EResource.Dia] + Resources_[(Int32)EResource.DiaPaid]);
        SetCP(Resources_[(Int32)EResource.CP]);
    }
    public void SetTicket(Int32 ticket_)
    {
        //TO-DO 나중에 아래 코드 참고해서 추가 개발 필요. <추가 개발 시 gold_ 변수 제거 가능.>
        //CGlobal.LoginNetSc.User.Point
        _Ticket.text = System.Convert.ToString(ticket_);
    }
    public void SetGold(Int32 gold_)
    {
        //TO-DO 나중에 아래 코드 참고해서 추가 개발 필요. <추가 개발 시 gold_ 변수 제거 가능.>
        //CGlobal.LoginNetSc.User.Point
        _Gold.text = System.Convert.ToString(gold_);
    }
    public void SetDia(Int32 dia_)
    {
        //TO-DO 나중에 아래 코드 참고해서 추가 개발 필요. <추가 개발 시 gold_ 변수 제거 가능.>
        //CGlobal.LoginNetSc.User.Point
        _Dia.text = System.Convert.ToString(dia_);
    }
    public void SetCP(Int32 CP_)
    {
        //TO-DO 나중에 아래 코드 참고해서 추가 개발 필요. <추가 개발 시 gold_ 변수 제거 가능.>
        //CGlobal.LoginNetSc.User.Point
        _CP.text = System.Convert.ToString(CP_);
    }
    public void OptionView()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if(_IsPopupSetting)
        {
            _SettingPopup.SetActive(true);
        }
        else
        {
            //이전 Scene 클래스 명으로 이동할 Scene 생성.
            CScene Scene = null;
            if (_SceneString.Equals("CSceneShop"))
                Scene = new CSceneShop(null);
            else if (!_SceneString.Equals("CSceneLobby"))
                Scene = Activator.CreateInstance(Type.GetType(_SceneString)) as CScene;

            CGlobal.SceneSetNext(new CSceneSetting(Scene));
        }
    }
    public void SettingPopupClose()
    {
        _SettingPopup.SetActive(false);
    }
    public bool GetSettingPopup()
    {
        return _SettingPopup.activeSelf;
    }
    public void GotoShopGold()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        Debug.Log("골드 상점 화면!!!");
        Type Scene = null;
        if (!_SceneString.Equals("CSceneLobby"))
            Scene = Type.GetType(_SceneString);
        CGlobal.SceneSetNext(new CSceneShop(Scene == null ? null : Activator.CreateInstance(Scene) as CScene));
    }
    public void GotoShopDia()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        Debug.Log("다이아 상점 화면!!!");
        Type Scene = null;
        if (!_SceneString.Equals("CSceneLobby"))
            Scene = Type.GetType(_SceneString);
        CGlobal.SceneSetNext(new CSceneShop(Scene == null ? null : Activator.CreateInstance(Scene) as CScene));
    }
    public void ShowRewardEffect(Int32[] Resources_)
    {
        for (Int32 i = 0; i < CGlobal.LoginNetSc.User.Resources.Length; ++i)
            _Resources[i] = CGlobal.LoginNetSc.User.Resources[i];
        for (Int32 i = 0; i < (Int32)EResource.Max; ++i)
        {
            if(Resources_[i] <= 10)
            {
                for (Int32 a = 0; a < Resources_[i]; ++a)
                {
                    MakeResourceIcon((EResource)i, 1);
                }
            }
            else if(Resources_[i] <= 1000)
            {
                Int32 ResourceTenCount = Resources_[i] / 10;
                Int32 ResourceOneCount = Resources_[i] % 10;
                for (Int32 a = 0; a < ResourceTenCount; ++a)
                {
                    MakeResourceIcon((EResource)i, 10);
                }
                MakeResourceIcon((EResource)i, ResourceOneCount);
            }
            else if (Resources_[i] <= 10000)
            {
                Int32 ResourceTenCount = Resources_[i] / 100;
                Int32 ResourceOneCount = Resources_[i] % 100;
                for (Int32 a = 0; a < ResourceTenCount; ++a)
                {
                    MakeResourceIcon((EResource)i, 100);
                }
                MakeResourceIcon((EResource)i, ResourceOneCount);
            }
            else
            {
                Int32 ResourceTenCount = Resources_[i] / 1000;
                Int32 ResourceOneCount = Resources_[i] % 1000;
                for (Int32 a = 0; a < ResourceTenCount; ++a)
                {
                    MakeResourceIcon((EResource)i, 1000);
                }
                MakeResourceIcon((EResource)i, ResourceOneCount);
            }
            CGlobal.LoginNetSc.User.Resources[i] += Resources_[i];
        }
        IsResourceEffect = true;
        SoundDeltaTime = 0.0f;
    }
    void MakeResourceIcon(EResource Resource_, Int32 Count)
    {
        var icon = ResourceIconPool();
        icon.SetResourceIcon(Resource_, Count);
        icon.transform.position = Vector3.zero;
        if (Resource_ == EResource.DiaPaid)
            icon.EndPosition = new Vector2(_Icons[(Int32)EResource.Dia].transform.position.x, _Icons[(Int32)EResource.Dia].transform.position.y);
        else
            icon.EndPosition = new Vector2(_Icons[(Int32)Resource_].transform.position.x, _Icons[(Int32)Resource_].transform.position.y);
        icon.StartPosition = new Vector2(Random.Range(_EffectMinPos.position.x, _EffectMaxPos.position.x), Random.Range(_EffectMinPos.position.y, _EffectMaxPos.position.y));
        icon.IsAdd = true;
        icon.IsMove = false;
    }
    private void Update()
    {
        if(IsResourceEffect)
        {
            SoundDeltaTime += Time.deltaTime;
            if(SoundDeltaTime > 0.3f)
            {
                SoundDeltaTime -= 0.3f;
                CGlobal.Sound.PlayOneShot((Int32)ESound.GetGold);
            }
            foreach (var i in ResourceIconActiveList())
            {
                if (i.IsAdd)
                {
                    i.transform.position = Vector3.MoveTowards(i.transform.position, i.StartPosition, (i.Velocity * 0.7f) * Time.deltaTime);
                    if (i.transform.position == i.StartPosition)
                    {
                        i.IsAdd = false;
                        i.IsMove = true;
                    }
                }
                if (i.IsMove)
                {
                    i.transform.position = Vector3.MoveTowards(i.transform.position, i.EndPosition, i.Velocity * Time.deltaTime);
                    if (i.transform.position == i.EndPosition)
                    {
                        i.IsAdd = false;
                        i.IsMove = false;
                        i.IsActive = false;
                        switch (i.ResourceEnum)
                        {
                            case EResource.Ticket:
                                _Resources[(Int32)EResource.Ticket] += i.ResourceCount;
                                SetTicket(_Resources[(Int32)EResource.Ticket]);
                                _EffectIcon[(Int32)EResource.Ticket].SetActive(true);
                                _EffectBGIcon[(Int32)EResource.Ticket].SetActive(false);
                                break;
                            case EResource.Gold:
                                _Resources[(Int32)EResource.Gold] += i.ResourceCount;
                                SetGold(_Resources[(Int32)EResource.Gold]);
                                _EffectIcon[(Int32)EResource.Gold].SetActive(true);
                                _EffectBGIcon[(Int32)EResource.Gold].SetActive(false);
                                break;
                            case EResource.Dia:
                            case EResource.DiaPaid:
                                _Resources[(Int32)i.ResourceEnum] += i.ResourceCount;
                                SetDia(_Resources[(Int32)EResource.Dia]+ _Resources[(Int32)EResource.DiaPaid]);
                                _EffectIcon[(Int32)EResource.Dia].SetActive(true);
                                _EffectBGIcon[(Int32)EResource.Dia].SetActive(false);
                                break;
                            case EResource.CP:
                                _Resources[(Int32)EResource.CP] += i.ResourceCount;
                                SetCP(_Resources[(Int32)EResource.CP]);
                                _EffectIcon[(Int32)EResource.CP].SetActive(true);
                                _EffectBGIcon[(Int32)EResource.CP].SetActive(false);
                                break;
                        }
                    }
                }
            }
            if (!IsActiveResourceIcon())
            {
                IsResourceEffect = false;
                SetResources(CGlobal.LoginNetSc.User.Resources);
                foreach (var i in _EffectIcon)
                    i.SetActive(false);
                foreach (var i in _EffectBGIcon)
                    i.SetActive(true);
            }
        }
    }
    private ResourceIcon ResourceIconPool()
    {
        foreach(var icon in ResourceIcons)
        {
            if(icon.IsActive == false)
            {
                icon.IsActive = true;
                return icon;
            }
        }
        var Prefab = Resources.Load<GameObject>("Prefabs/UI/ResourceIcon");
        Debug.Assert(Prefab != null);

        var Icon = UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Icon.transform.SetParent(_EffectParent.transform);
        Icon.transform.localScale = Vector3.one;
        var IconScript = Icon.GetComponent<ResourceIcon>();
        IconScript.IsActive = true;
        ResourceIcons.Add(IconScript);
        return IconScript;
    }
    private List<ResourceIcon> ResourceIconActiveList()
    {
        var icons = new List<ResourceIcon>();
        foreach (var icon in ResourceIcons)
        {
            if (icon.IsActive == true)
            {
                icons.Add(icon);
            }
        }
        return icons;
    }
    private bool IsActiveResourceIcon()
    {
        bool IsActive = false;
        foreach (var icon in ResourceIcons)
        {
            if (icon.IsActive == true)
            {
                IsActive = true;
                break;
            }
        }
        return IsActive;
    }
}
