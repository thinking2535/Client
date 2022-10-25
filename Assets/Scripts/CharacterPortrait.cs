using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPortrait : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] Image _backgroundImage;
    [SerializeField] Image _characterImage;
    [SerializeField] Image _classIcon;
    [SerializeField] Image _nftIcon;

    public SCharacterMeta meta { get; private set; }
    protected virtual void Awake()
    {
        _button.onClick.AddListener(_click);
    }
    protected virtual void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    public void init(SCharacterMeta meta, Sprite backgroundImage)
    {
        this.meta = meta;
        _backgroundImage.sprite = backgroundImage;
        _characterImage.sprite = this.meta.GetSprite();
        _classIcon.sprite = Resources.Load<Sprite>("GUI/Common/" + CGlobal.GetCharStatusIcon(this.meta.Post_Status));

        if (this.meta.isNFTCharacter())
            _nftIcon.gameObject.SetActive(true);
    }
    protected virtual void _click()
    {
        var Scene = CGlobal.GetScene<MoneyUIScene>();
        Scene.ShowCharacterInfoPopup(meta);
    }
}
