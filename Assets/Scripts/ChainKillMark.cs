using bb;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChainKillMark : MonoBehaviour
{
    private static readonly string[] _KillMarkImageNames =
    {
        "",
        "",
        "Textures/Kill1",
        "Textures/Kill2",
        "Textures/Kill3",
        "Textures/Kill4"
    };
    private static readonly EText[] _KillMarkTextNames =
    {
        EText.Null,
        EText.Null,
        EText.Game_DoubleKill,
        EText.Game_TripleKill,
        EText.Game_QuadraKill,
        EText.Game_LegendaryKill
    };

    private static readonly Vector3 MaskStartPos = new Vector3(384.0f,0.0f,0.0f);
    private static readonly Vector3 MaskStartScale = new Vector3(1.6f, 1.6f, 1.6f);
    private static readonly Vector3 MaskStartAccelation = new Vector3(0.03f, 0.03f, 0.03f);

    [SerializeField] Image MaskImage = null;
    [SerializeField] Image KillImage = null;
    [SerializeField] Image KillNameBG = null;
    [SerializeField] Text KillName = null;
    [SerializeField] Text KillText = null;

    Int32 _KillCount = 0;
    bool _IsViewKillMark = false;
    bool _IsViewKillText = false;
    float DeltaTime = 0.0f;
    Vector3 Accelation = MaskStartAccelation;
    public void Init()
    {
        gameObject.SetActive(false);
    }
    public void ShowChainKill(Int32 Count_, string UserName_, bool SameTeam_, bool IsMe_)
    {
        _KillCount = Count_;
        if (_KillCount < 2) return;
        else if (_KillCount > 5) _KillCount = 5;

        //if (SameTeam_)
        //{
        //    if (IsMe_)
        //        KillName.color = CGlobal.NameColorGreen;
        //    else
        //        KillName.color = CGlobal.NameColorBlue;
        //}
        //else
        //    KillName.color = CGlobal.NameColorRed;

        gameObject.SetActive(true);
        MaskImage.transform.localPosition = MaskStartPos;
        KillImage.sprite = Resources.Load<Sprite>(_KillMarkImageNames[_KillCount]);
        KillName.text = UserName_;
        KillText.text = CGlobal.MetaData.GetText(_KillMarkTextNames[_KillCount]);
        KillNameBG.transform.localScale = MaskStartScale;
        KillNameBG.gameObject.SetActive(false);
        _IsViewKillMark = true;
        _IsViewKillText = false;
        Accelation = MaskStartAccelation;
        DeltaTime = 0.0f;

        CGlobal.Sound.PlayOneShot((Int32)ESound.DoubleKill);
    }
    private void Update()
    {
        if(_IsViewKillMark)
        {
            MaskImage.transform.localPosition -= ((MaskStartPos - Vector3.zero) * Time.deltaTime) * 6.0f;

            if(MaskImage.transform.localPosition.x <= 0.0f)
            {
                MaskImage.transform.localPosition = Vector3.zero;
                _IsViewKillMark = false;
                _IsViewKillText = true;
                KillNameBG.gameObject.SetActive(true);
            }
        }
        if(_IsViewKillText)
        {
            DeltaTime += Time.deltaTime;
            if (DeltaTime <= 0.3f)
            {
                Accelation += Accelation * (Time.deltaTime / 0.3f);
                KillNameBG.transform.localScale -= ((MaskStartScale - Vector3.one) * Time.deltaTime) + Accelation;
            }
            else if (DeltaTime > 0.7f)
            {
                DeltaTime = 0.0f;
                _IsViewKillText = false;
                gameObject.SetActive(false);
            }
            else
            {
                KillNameBG.transform.localScale = Vector3.one;
            }
        }
    }
}
