using rso.core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressLoading : MonoBehaviour
{
    [SerializeField] Image _ProgressImage = null;
    [SerializeField] RawImage _ProgressBg = null;

    TimePoint _DelayTime;
    bool IsDelayed = false;

    void Update()
    {
        if (gameObject.activeSelf)
        {
            _ProgressImage.transform.Rotate(new Vector3(0.0f, 0.0f, 360.0f) * Time.deltaTime);
            if(IsDelayed)
            {
                var Time = _DelayTime - CGlobal.GetServerTimePoint();
                if (Time.Ticks <= 0)
                {
                    ViewProgressObject();
                }
            }
        }
    }

    public void VisibleProgressLoading()
    {
        IsDelayed = false;
        ViewProgressObject();
        gameObject.SetActive(true);
    }
    public void VisibleProgressLoading(double Delay_)
    {
        IsDelayed = true;
        _DelayTime = CGlobal.GetServerTimePoint() + TimeSpan.FromSeconds(Delay_);
        _ProgressImage.gameObject.SetActive(false);
        _ProgressBg.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
    public void InvisibleProgressLoading()
    {
        IsDelayed = false;
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
    private void ViewProgressObject()
    {
        IsDelayed = false;
        _ProgressImage.gameObject.SetActive(true);
        _ProgressBg.gameObject.SetActive(true);
    }
}
