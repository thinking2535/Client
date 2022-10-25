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

    public void ShowPopEffectAndPoint(sbyte AttackerRelativeDir_, Int32 Point_)
    {
        ShowPopEffect(AttackerRelativeDir_);
        ShowPoint(Point_);
    }
    public void ShowPoint(Int32 Point_)
    {
        EffectText.SetStart(Point_);
    }
    public void ShowPopEffect(sbyte AttackerRelativeDir_)
    {
        BalloonPaticle.gameObject.SetActive(true);
        float effectPosX = EffectXPosition * AttackerRelativeDir_;
        transform.localPosition = new Vector3(transform.localPosition.x + effectPosX, transform.localPosition.y + 0.2f, 0.12f);
        EffectText.gameObject.SetActive(false);

        BalloonPaticle.Play();
        CGlobal.Sound.PlayOneShot((Int32)ESound.Pop);
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
