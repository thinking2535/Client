using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaAnimationEvent : MonoBehaviour
{
    [SerializeField] GachaPopup _GachaPopup = null;
    public void GachaAnimationEnd()
    {
        _GachaPopup.GachaAnimationEnd();
    }
    public void GachaX10AnimationEnd()
    {
        _GachaPopup.GachaX10AnimationEnd();
    }
    public void GachaCharWin()
    {
        _GachaPopup.GachaCharWin();
    }
    public void GachaBounce()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Gacha_bounce);
    }
    public void GachaShoot()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Gacha_shoot);
    }
    public void GachaOpenAni()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Gacha_openani);
    }
    public void GachaStart()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Gacha_Start);
    }
}
