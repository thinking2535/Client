using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public Image PanelBG = null;
    public Image PanelIcon = null;
    public Image PanelCheck = null;
    public Image PanelDeam = null;
    public Text PanelName = null;
    public Image PanelNew = null;
    public Image PanelKeyCoin = null;
    public Image StautsIcon = null;

    private Int32 _CharCode = 0;
    private bool _IsOpen = true;
    public void InitPanel(Int32 CharCode_, bool IsOpen_ = true)
    {
        _CharCode = CharCode_;
        _IsOpen = IsOpen_;
        var Character = CGlobal.MetaData.Chars[_CharCode];
        PanelBG.sprite = Resources.Load<Sprite>(CGlobal.PanelBGTextrues[(Int32)Character.Grade]);
        PanelIcon.sprite = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + Character.IconName);
        PanelName.text = CGlobal.MetaData.GetText(Character.ETextName);
        PanelCheck.gameObject.SetActive(CGlobal.LoginNetSc.User.SelectedCharCode == _CharCode);
        PanelNew.gameObject.SetActive(CGlobal.RedDotControl.NewChar.Contains(_CharCode));

        if (CGlobal.LoginNetSc.Chars.Contains(_CharCode))
        {
            PanelDeam.gameObject.SetActive(false);
            PanelKeyCoin.gameObject.SetActive(false);
        }
        else
        {
            PanelDeam.gameObject.SetActive(true);
            PanelKeyCoin.gameObject.SetActive(true);
        }
        StautsIcon.sprite = Resources.Load<Sprite>("GUI/Common/" + CGlobal.GetCharStatusIcon(Character.Post_Status));
    }
    public void OnClickCharPanel()
    {
        if (_IsOpen == false) return;

        PanelNew.gameObject.SetActive(false);
        var Scene = CGlobal.GetScene<CSceneCharacterList>();
        Scene.ShowCharacterInfoPopup(_CharCode);
    }
}
