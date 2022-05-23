using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayerInfoPanel : MonoBehaviour
{
    [SerializeField] GameObject _MyBG = null;
    [SerializeField] Text _UserName = null;
    [SerializeField] Text _UserScore = null;
    [SerializeField] Image _UserIcon = null;
    [SerializeField] Image _UserEmo = null;
    [SerializeField] GameObject _BuffIconInk = null;
    [SerializeField] GameObject _BuffIconScale = null;
    [SerializeField] GameObject _BuffIconSlow = null;
    private float _EmotionTime = 0.0f;
    public void Init(string IconName_, string UserName_, Int32 UserScore_, bool IsMe_)
    {
        _UserName.text = UserName_;
        _MyBG.SetActive(IsMe_);
        SetScore(UserScore_);
        gameObject.SetActive(true);
        _UserEmo.gameObject.SetActive(false);
        _UserIcon.sprite = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + IconName_);
        _BuffIconInk.SetActive(false);
        _BuffIconScale.SetActive(false);
        _BuffIconSlow.SetActive(false);
    }
    public void SetScore(Int32 UserScore_)
    {
        _UserScore.text = UserScore_.ToString();
    }
    public void SetEmotion(Int32 Code_)
    {
        _UserEmo.gameObject.SetActive(true);
        _UserEmo.sprite = Resources.Load<Sprite>("Textures/emo_" + Code_.ToString());
        _EmotionTime = 0.0f;
    }
    public void SetInk(bool IsOn_)
    {
        _BuffIconInk.SetActive(IsOn_);
    }
    public void SetScale(bool IsOn_)
    {
        _BuffIconScale.SetActive(IsOn_);
    }
    public void SetSlow(bool IsOn_)
    {
        _BuffIconSlow.SetActive(IsOn_);
    }
    private void Update()
    {
        if (_UserEmo != null)
        {
            if (_UserEmo.gameObject.activeSelf)
            {
                _EmotionTime += Time.deltaTime;
                if (_EmotionTime >= 2.0f)
                {
                    _UserEmo.gameObject.SetActive(false);
                }
            }
        }
    }
}
