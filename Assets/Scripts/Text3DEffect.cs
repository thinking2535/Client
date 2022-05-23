using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text3DEffect : MonoBehaviour
{
    TextMesh EffectText;

    float time;
    public float _fadeTime = 0.5f;
    public Vector3 dir;
    public bool IsFadeOut = false;
    Color OriginColor;
    Vector3 OriginDir;
    bool _IsFadeOut = false;

    public bool IsEnd { get; private set; } = false;

    public void Init()
    {
        EffectText = GetComponent<TextMesh>();
        OriginColor = EffectText.color;
        OriginDir = EffectText.transform.localPosition;
    }

    public void SetStart(Int32 ViewText_)
    {
        EffectText.gameObject.SetActive(true);
        EffectText.transform.localPosition = OriginDir;
        EffectText.color = OriginColor;
        EffectText.text = ViewText_.ToString();
        _IsFadeOut = IsFadeOut;
        IsEnd = false;
    }
    
    void Update()
    {
        if (IsEnd) return;

        EffectText.transform.Translate(dir * Time.deltaTime);
        if (time < _fadeTime && _IsFadeOut)
        {
            EffectText.color = new Color(OriginColor.r, OriginColor.g, OriginColor.b, 1f - time / _fadeTime);
        }
        else
        {
            time = 0;
            _IsFadeOut = false;
            EffectText.gameObject.SetActive(false);
            IsEnd = true;
        }
        time += Time.deltaTime;
    }
}
