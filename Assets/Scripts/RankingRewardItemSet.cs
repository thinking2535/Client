using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingRewardItemSet : MonoBehaviour
{
    [SerializeField] Image _Icon = null;
    [SerializeField] Text _Count = null;
    public void Init(Sprite Sprite_, string Text_)
    {
        _Icon.sprite = Sprite_;
        _Count.text = Text_;
    }
}
