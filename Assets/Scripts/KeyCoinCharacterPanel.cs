using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCoinCharacterPanel : MonoBehaviour
{
    [SerializeField] Image PanelBG = null;
    [SerializeField] Image PanelIcon = null;
    [SerializeField] Text PanelCoinText = null;

    public void Init(Int32 CharCode_, Int32 KeyCoin_)
    {
        var CharMeta = CGlobal.MetaData.Chars[CharCode_];
        PanelBG.sprite = Resources.Load<Sprite>(CGlobal.PanelKeyBGTextrues[(Int32)CharMeta.Grade]);
        PanelIcon.sprite = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + CharMeta.IconName);
        PanelCoinText.text = KeyCoin_.ToString();
    }
}
