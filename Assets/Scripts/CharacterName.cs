using rso.core;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterName : MonoBehaviour
{
    SCharacterMeta _Meta = null;
    [SerializeField] TextMesh NameText = null;
    [SerializeField] Transform StaminaBar = null;
    [SerializeField] Text3DOutLine NameOutline = null;
    [SerializeField] GameObject StaminaObject = null;
    [SerializeField] SpriteRenderer EmotionIcon = null;
    [SerializeField] GameObject StaminaItem = null;
    [SerializeField] GameObject ShieldItem = null;
    [SerializeField] Image StaminaTimer = null;
    [SerializeField] Image ShieldTimer = null;
    [SerializeField] GameObject StaminaEffObject = null;
    [SerializeField] Image StaminaEff = null;

    private CBattlePlayer _BattlePlayer = null;
    private CSinglePlayer _SinglePlayer = null;
    private bool _IsStamina = true;
    private bool _IsSingle = false;
    private float _EmotionTime = 0.0f;
    private float _StaminaRetention = 0.0f;
    private float _StaminaRetentionMax = 0.0f;
    private float _ShieldRetention = 0.0f;
    private float _ShieldRetentionMax = 0.0f;
    private float _StaminaEffTime = 0.0f;
    private bool _IsFill = false;
    public void Init(SCharacterMeta Meta_)
    {
        _Meta = Meta_;
        EmotionIcon.gameObject.SetActive(false);
        StaminaItem.SetActive(false);
        ShieldItem.SetActive(false);
        StaminaEffObject.SetActive(false);
    }
    public void SetName(CBattlePlayer BattlePlayer_, Camera Camera_, bool IsStamina_)
    {
        _BattlePlayer = BattlePlayer_;
        SetName(Camera_, IsStamina_);
        NameText.text = _BattlePlayer.name;
        _IsSingle = false;
    }
    public void SetName(CSinglePlayer SinglePlayer_, Camera Camera_, bool IsStamina_)
    {
        _SinglePlayer = SinglePlayer_;
        SetName(Camera_, IsStamina_);
        NameText.text = _SinglePlayer.name;
        NameText.gameObject.SetActive(false);
        _IsSingle = true;
    }
    public void SetEmotion(Int32 Code)
    {
        EmotionIcon.gameObject.SetActive(true);
        EmotionIcon.sprite = Resources.Load<Sprite>("Textures/emo_" + Code.ToString());
        _EmotionTime = 0.0f;
    }
    public void SetName(Camera Camera_, bool IsStamina_)
    {
        NameOutline.NowCamera = Camera_;

        NameOutline.outlineColor = CGlobal.NameColorGreen;

        _IsStamina = IsStamina_;
        StaminaObject.SetActive(_IsStamina);
        StaminaBar.transform.localScale = Vector3.one;
    }
    public void SetStaminaItem(float ItemRetentionMax_)
    {
        _StaminaRetention = 0.0f;
        _StaminaEffTime = 0.0f;
        _StaminaRetentionMax = ItemRetentionMax_;
        StaminaItem.SetActive(true);
        StaminaEffObject.SetActive(true);
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
        if(_IsStamina)
        {
            var scaleX = 0.0f;
            if (!_IsSingle)
                scaleX = _BattlePlayer.Character.StaminaInfo.Stamina / _Meta.StaminaMax;
            else
                scaleX = (_SinglePlayer.GetStamina(CGlobal.GetServerTimePoint()) / _Meta.StaminaMax);

            if (scaleX > 1.0f)
                scaleX = 1.0f;
            StaminaBar.transform.localScale = new Vector3(scaleX, 1.0f, 1.0f);
        }
        if(EmotionIcon.gameObject.activeSelf)
        {
            _EmotionTime += Time.deltaTime;
            if(_EmotionTime >= 2.0f)
            {
                EmotionIcon.gameObject.SetActive(false);
            }
        }
        if(StaminaItem.activeSelf)
        {
            _StaminaRetention += Time.deltaTime;
            _StaminaEffTime += Time.deltaTime;
            if(_StaminaEffTime > 1.0f)
            {
                _StaminaEffTime = 0.0f;
                _IsFill = !_IsFill;
                if(_IsFill)
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
            {
                StaminaItem.SetActive(false);
                StaminaEffObject.SetActive(false);
            }
        }
        if (ShieldItem.activeSelf)
        {
            _ShieldRetention += Time.deltaTime;
            ShieldTimer.fillAmount = 1.0f - (_ShieldRetention / _ShieldRetentionMax);
            if (_ShieldRetention >= _ShieldRetentionMax)
            {
                ShieldItem.SetActive(false);
            }
        }
    }
}
