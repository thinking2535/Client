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
    public void Init(EResource Type_, Int32 Count_)
    {
        _Icon.sprite = Resources.Load<Sprite>(CGlobal.GetResourcesIconFile(Type_));
        _Count.text = Count_.ToString();
    }
}
