using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemSet : MonoBehaviour
{
    [SerializeField] Image _Image = null;
    [SerializeField] Text _Text = null;

    public CUnitReward UnitReward { get; private set; }

    public void Init(CUnitReward UnitReward_)
    {
        UnitReward = UnitReward_;
        _Image.sprite = UnitReward.GetSprite();
        _Text.text = UnitReward.GetText();
    }
}
