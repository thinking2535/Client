using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScore : MonoBehaviour
{
    [SerializeField] ParticleSystem KillPaticle = null;
    Text3DEffect[] EffectText;

    public void init(Int32 AddPoint_)
    {
        foreach (var Effect in EffectText)
            Effect.SetStart(AddPoint_);

        KillPaticle.Play();
    }
    public void Awake()
    {
        EffectText = GetComponentsInChildren<Text3DEffect>();
        foreach (var Effect in EffectText)
            Effect.Init();
    }
    private void Update()
    {
        foreach (var Effect in EffectText)
        {
            if (Effect.IsEnd) gameObject.SetActive(false);
        }
    }
}
