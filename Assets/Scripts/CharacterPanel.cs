using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : CharacterPortrait
{
    [SerializeField] Image _isSelectedImage;
    [SerializeField] Image _isNewImage;
    [SerializeField] Image _isDisabledImage;

    public void init(SCharacterMeta meta)
    {
        base.init(meta, meta.getGradeInfo().characterBackgroundSprite);

        isSelected = CGlobal.LoginNetSc.User.SelectedCharCode == meta.Code;
        isNew = CGlobal.RedDotControl.NewChar.Contains(meta.Code);
        isDisabled = !CGlobal.LoginNetSc.doesHaveCharacter(meta.Code);
    }
    public bool isSelected
    {
        get
        {
            return _isSelectedImage.gameObject.activeSelf;
        }
        set
        {
            _isSelectedImage.gameObject.SetActive(value);
        }
    }
    public bool isNew
    {
        get
        {
            return _isNewImage.gameObject.activeSelf;
        }
        set
        {
            _isNewImage.gameObject.SetActive(value);
        }
    }
    public bool isDisabled
    {
        get
        {
            return _isDisabledImage.gameObject.activeSelf;
        }
        set
        {
            _isDisabledImage.gameObject.SetActive(value);
        }
    }
    protected override void _click()
    {
        isNew = false;
        base._click();
    }
}
