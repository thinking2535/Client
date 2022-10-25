using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCharacterPanel : CharacterPortrait
{
    [SerializeField] Image _costIcon;
    [SerializeField] Text _costValue;
    [SerializeField] Image _isPurchasedImage;

    public bool isPurchased
    {
        set
        {
            _isPurchasedImage.gameObject.SetActive(value);
        }
    }
    public void init(SCharacterMeta meta)
    {
        base.init(meta, meta.getGradeInfo().shopCharacterBackgroundSprite);

        _costIcon.sprite = meta.getCostTypeSprite();
        _costValue.text = meta.CostValue.ToString();
        isPurchased = CGlobal.LoginNetSc.doesHaveCharacter(meta.Code);
    }
}
