using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitedItemSet : MonoBehaviour
{
    [SerializeField] GameObject LimitedResource = null;
    [SerializeField] Image LimitedResourceImage = null;
    [SerializeField] Text LimitedResourceText = null;

    [SerializeField] GameObject LimitedCharacter = null;
    [SerializeField] Image LimitedCharacterImage = null;
    public void Init(CUnitReward UnitReward_)
    {
        UnitReward_.SetToLimitedItemSet(this);
    }
    public void SetResource(Sprite Sprite_, Int32 Count_)
    {
        LimitedResource.SetActive(true);
        LimitedCharacter.SetActive(false);
        LimitedResourceImage.sprite = Sprite_;
        LimitedResourceText.text = Count_.ToString();
    }
    public void SetCharacter(Sprite Sprite_)
    {
        LimitedResource.SetActive(false);
        LimitedCharacter.SetActive(true);
        LimitedCharacterImage.sprite = Sprite_;
    }
}
