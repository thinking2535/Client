using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCoinCharacterPanel : MonoBehaviour
{
    [SerializeField] Image PanelBG = null;
    [SerializeField] Image PanelIcon = null;
    [SerializeField] Image _RefundTypeImage = null;
    [SerializeField] Text PanelCoinText = null;

    public void Init(SCharacterMeta CharacterMeta_)
    {
        PanelBG.sprite = Resources.Load<Sprite>(CGlobal.PanelKeyBGTextrues[(Int32)CharacterMeta_.grade]);
        PanelIcon.sprite = CharacterMeta_.GetSprite();
        _RefundTypeImage.sprite = CGlobal.GetResourceSprite(CharacterMeta_.RefundType);
        PanelCoinText.text = CharacterMeta_.RefundValue.ToString();
    }
}
