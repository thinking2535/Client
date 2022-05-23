using System;
using UnityEngine;

public class BalloonEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem BalloonPaticle = null;
    [SerializeField] Text3DEffect EffectText = null;

    private float EffectXPosition = 0.0f;

    public void init(float HalfWidth_)
    {
        EffectText.Init();

        EffectXPosition = HalfWidth_;
    }

    public void ViewEffectBallon(sbyte AttackerRelativeDir_, Int32 AddPoint_)
    {
        ViewEffectBallon(AttackerRelativeDir_);

        EffectText.SetStart(AddPoint_);

        CGlobal.Sound.PlayOneShot((Int32)ESound.Pop);
    }
    public void ViewEffectBallon(sbyte AttackerRelativeDir_)
    {
        BalloonPaticle.gameObject.SetActive(true);
        float effectPosX = EffectXPosition * AttackerRelativeDir_;
        transform.localPosition = new Vector3(transform.localPosition.x + effectPosX, transform.localPosition.y + 0.2f, 0.12f);
        EffectText.gameObject.SetActive(false);

        BalloonPaticle.Play();
    }

    public void EndEffect()
    {
        gameObject.SetActive(false);
        BalloonPaticle.Stop();
    }
    private void Update()
    {
        if (EffectText.IsEnd) gameObject.SetActive(false);
    }
}
