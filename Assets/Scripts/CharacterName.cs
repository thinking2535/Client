using rso.core;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterName : MonoBehaviour
{
    [SerializeField] TextMesh NameText = null;
    [SerializeField] Transform StaminaBar = null;
    [SerializeField] Text3DOutLine NameOutline = null;
    [SerializeField] GameObject StaminaObject = null;
    [SerializeField] SpriteRenderer EmotionIcon = null;
    [SerializeField] GameObject StaminaItem = null;
    [SerializeField] GameObject ShieldItem = null;
    [SerializeField] Image StaminaTimer = null;
    [SerializeField] Image ShieldTimer = null;
    [SerializeField] Image StaminaEff = null;

    CBattlePlayer _BattlePlayer = null;
    float _EmotionTime = 0.0f;
    float _StaminaRetention = 0.0f;
    float _StaminaRetentionMax = 0.0f;
    float _ShieldRetention = 0.0f;
    float _ShieldRetentionMax = 0.0f;
    float _StaminaEffTime = 0.0f;
    bool _IsFill = false;
    public void init(CBattlePlayer BattlePlayer_, Camera Camera_)
    {
        EmotionIcon.gameObject.SetActive(false);
        StaminaItem.SetActive(false);
        ShieldItem.SetActive(false);

        _BattlePlayer = BattlePlayer_;
        NameOutline.NowCamera = Camera_;
        NameOutline.outlineColor = CGlobal.NameColorGreen;
        StaminaObject.SetActive(true);
        StaminaBar.transform.localScale = Vector3.one;
        NameText.text = _BattlePlayer.name;
    }
    public void SetEmotion(Int32 Code)
    {
        EmotionIcon.gameObject.SetActive(true);
        EmotionIcon.sprite = Resources.Load<Sprite>("Textures/emo_" + Code.ToString());
        _EmotionTime = 0.0f;
    }
    public void SetStaminaItem(float ItemRetentionMax_)
    {
        _StaminaRetention = 0.0f;
        _StaminaEffTime = 0.0f;
        _StaminaRetentionMax = ItemRetentionMax_;
        StaminaItem.SetActive(true);
    }
    public bool GetStaminaItem()
    {
        return StaminaItem.activeSelf;
    }
    public void SetShieldItem(float ItemRetentionMax_)
    {
        _ShieldRetention = 0.0f;
        _ShieldRetentionMax = ItemRetentionMax_;
        ShieldItem.SetActive(true);
    }
    public bool GetShieldItem()
    {
        return ShieldItem.activeSelf;
    }
    private void Update()
    {
        var scaleX = _BattlePlayer.getStaminaNormalValue();

        StaminaBar.transform.localScale = new Vector3(scaleX, 1.0f, 1.0f);

        if (EmotionIcon.gameObject.activeSelf)
        {
            _EmotionTime += Time.deltaTime;
            if (_EmotionTime >= 2.0f)
            {
                EmotionIcon.gameObject.SetActive(false);
            }
        }

        if (StaminaItem.activeSelf)
        {
            _StaminaRetention += Time.deltaTime;
            _StaminaEffTime += Time.deltaTime;
            if (_StaminaEffTime > 1.0f)
            {
                _StaminaEffTime = 0.0f;
                _IsFill = !_IsFill;
                if (_IsFill)
                    StaminaEff.fillAmount = 1.0f;
                else
                    StaminaEff.fillAmount = 0.0f;
            }
            if (_IsFill)
                StaminaEff.fillAmount = 1.0f - (_StaminaEffTime / 1.0f);
            else
                StaminaEff.fillAmount = (_StaminaEffTime / 1.0f);

            StaminaTimer.fillAmount = 1.0f - (_StaminaRetention / _StaminaRetentionMax);
            if (_StaminaRetention >= _StaminaRetentionMax)
                StaminaItem.SetActive(false);
        }

        if (ShieldItem.activeSelf)
        {
            _ShieldRetention += Time.deltaTime;
            ShieldTimer.fillAmount = 1.0f - (_ShieldRetention / _ShieldRetentionMax);
            if (_ShieldRetention >= _ShieldRetentionMax)
                ShieldItem.SetActive(false);
        }
    }
}
