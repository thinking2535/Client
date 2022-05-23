using rso.unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class CSceneCharacterList : CSceneBase
{
    private CharacterListScene _CharacterListScene = null;
    private MoneyUI _MoneyUI = null;
    private GameObject _ActiveCharListParent = null;
    private GameObject _DeactiveCharListParent = null;
    private CharacterInfoPopup _CharacterInfoPopup = null;
    private CharacterPanel _CharacterPanelOrigin = null;
    private Camera _MainCamera = null;
    private GameObject _CharacterListContent = null;
    private Dictionary<Int32, GameObject> _CharPanels = new Dictionary<int, GameObject>();
    private Text _TipText = null;

    //float _DelayTip = 0.0f;
    //bool _TipType = false;
    public CSceneCharacterList() :
        base("Prefabs/CharacterListScene", Vector3.zero, true)
    {
    }
    public override void Dispose()
    {
    }
    public override void Enter()
    {
        _CharacterListScene = _Object.GetComponent<CharacterListScene>();
        _MoneyUI = _CharacterListScene.MoneyUI.GetComponent<MoneyUI>();
        _MoneyUI.SetResources(CGlobal.LoginNetSc.User.Resources);
        _ActiveCharListParent = _CharacterListScene.ActiveCharListParent;
        _DeactiveCharListParent = _CharacterListScene.DeactiveCharListParent;
        _CharacterInfoPopup = _CharacterListScene.CharacterInfoPopup;
        _CharacterPanelOrigin = _CharacterListScene.CharacterPanelOrigin;
        _MainCamera = _CharacterListScene.MainCamera;
        _CharacterListContent = _CharacterListScene.CharacterListContent;
        _TipText = _CharacterListScene.TipText;
        //_DelayTip = 0.0f;
        //_TipType = false;

        var SortList = CGlobal.MetaData.Chars.OrderBy(x => x.Value.Grade);
        CharacterPanel Panel = null;

        foreach (var Char in SortList)
        {
            if (!CGlobal.LoginNetSc.Chars.Contains(Char.Key)) continue;

            Panel = Object.Instantiate(_CharacterPanelOrigin, _ActiveCharListParent.transform);
            Panel.transform.localScale = Vector3.one;
            Panel.transform.localPosition = new Vector3(Panel.transform.localPosition.x, Panel.transform.localPosition.y, 0.0f);

            Panel.InitPanel(Char.Key);
            _CharPanels.Add(Char.Key, Panel.gameObject);
        }
        foreach (var Char in SortList)
        {
            if (_CharPanels.ContainsKey(Char.Key)) continue;

            Panel = Object.Instantiate(_CharacterPanelOrigin, _DeactiveCharListParent.transform);
            Panel.transform.localScale = Vector3.one;
            Panel.transform.localPosition = new Vector3(Panel.transform.localPosition.x, Panel.transform.localPosition.y, 0.0f);

            Panel.InitPanel(Char.Key);
            _CharPanels.Add(Char.Key, Panel.gameObject);
        }

        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Character);
    }
    public override bool Update()
    {
        if (_Exit)
            return false;

        if (rso.unity.CBase.BackPushed())
        {
            if(_CharacterInfoPopup.gameObject.activeSelf)
            {
                _CharacterInfoPopup.OnClickBack();
                return true;
            }
            if (_MoneyUI.GetSettingPopup())
            {
                _MoneyUI.SettingPopupClose();
                return true;
            }
            if (CGlobal.SystemPopup.gameObject.activeSelf)
            {
                CGlobal.SystemPopup.OnClickCancel();
                return true;
            }
            CGlobal.SceneSetNext(new CSceneLobby());
            return false;
        }
        _TipText.text = string.Format("{0}/{1}",CGlobal.LoginNetSc.Chars.Count(),CGlobal.MetaData.Chars.Count());
        //_DelayTip += Time.deltaTime;
        //if(_DelayTip >= 10.0f)
        //{
        //    _TipType = !_TipType;
        //    if (_TipType)
        //        _TipText.text = CGlobal.MetaData.GetText(EText.CharacterSelectScene_Text_Tip2);
        //    else
        //        _TipText.text = CGlobal.MetaData.GetText(EText.CharacterSelectScene_Text_Tip);
        //    _DelayTip = 0.0f;
        //}

        return true;
    }
    public void ShowCharacterInfoPopup(Int32 CharCode_)
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _CharacterInfoPopup.ShowCharacterInfoPopup(CharCode_, _MainCamera);
    }
    public override void ResourcesUpdate()
    {
        _CharacterInfoPopup.ResourcesUpdate();
        var SortList = CGlobal.MetaData.Chars.OrderBy(x => x.Value.Grade);
        foreach (var Char in SortList)
        {
            _CharPanels[Char.Key].transform.SetParent(_DeactiveCharListParent.transform);
            if (CGlobal.LoginNetSc.Chars.Contains(Char.Key))
                _CharPanels[Char.Key].transform.SetParent(_ActiveCharListParent.transform);
            _CharPanels[Char.Key].GetComponent<CharacterPanel>().InitPanel(Char.Key);
        }
        _CharacterListContent.transform.localPosition = new Vector3(_CharacterListContent.transform.localPosition.x, 0.0f, _CharacterListContent.transform.localPosition.z);
        _MoneyUI.SetResources(CGlobal.LoginNetSc.User.Resources);
    }
}
