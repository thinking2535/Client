using bb;
using UnityEngine;
using rso.physics;
using System;

public class Balloon : MonoBehaviour
{
    static readonly float BalloonUnitSizeWidth = 0.2f;
    [SerializeField] BalloonSub[] _BalloonSubs = new BalloonSub[global.c_BalloonCountForRegen];
    public void Init(Material material, sbyte BalloonCount_)
    {
        foreach (var i in _BalloonSubs)
            i.Init(material);

        SetCount(BalloonCount_);
    }
    public void SetCount(sbyte Count_)
    {
        for (Int32 i = 0; i < _BalloonSubs.Length; ++i)
        {
            if (i == Count_ - 1)
                _BalloonSubs[i].gameObject.SetActive(true);
            else
                _BalloonSubs[i].gameObject.SetActive(false);
        }
    }
    public float GetHalfWidth()
    {
        return transform.localScale.x * BalloonUnitSizeWidth * 0.5f;
    }
}
