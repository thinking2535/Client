using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bb;

public class SinglePlayObject : MonoBehaviour
{
    public enum EItemType
    {
        Null,
        Coin,
        GoldBar,
        Item_Stamina,
        Item_Shield,
        Item_Point,
        Item_Random,
        Trap,
        Dia
    }
    private bool _IsActive = false;
    private EItemType _ItemType = EItemType.Null;
    private Vector3 _StartPos;
    private Vector3 _EndPos;
    private float _Velocity = 0.20f;
    private bool _IsMove = false;
    [SerializeField] GameObject _Trap = null;
    [SerializeField] GameObject _Coin = null;
    [SerializeField] GameObject _GoldBar = null;
    [SerializeField] GameObject _Dia = null;
    [SerializeField] GameObject _Shield = null;
    [SerializeField] GameObject _StaminaItem = null;
    [SerializeField] GameObject _Point = null;
    [SerializeField] GameObject _Ink = null;
    [SerializeField] GameObject _Scale = null;
    [SerializeField] GameObject _Slow = null;

    private void SetActiveObject(bool IsActive_)
    {
        _IsActive = IsActive_;
        gameObject.SetActive(IsActive_);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;
        _Trap.SetActive(false);
        _Coin.SetActive(false);
        _GoldBar.SetActive(false);
        _Dia.SetActive(false);
        _Shield.SetActive(false);
        _StaminaItem.SetActive(false);
        _Point.SetActive(false);
        _Ink.SetActive(false);
        _Scale.SetActive(false);
        _Slow.SetActive(false);
    }
    public void EnableObject(EItemType ItemType_, Vector3 StartPos_, Vector3 EndPos_, float Velocity_, float Angle_)
    {
        SetActiveObject(true);
        _ItemType = ItemType_;
        _StartPos = StartPos_;
        _EndPos = EndPos_;
        _Velocity = Velocity_ * 60.0f;
        gameObject.transform.localPosition = _StartPos;
        gameObject.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Angle_);
        SetActive(_ItemType);
        _IsMove = true;
    }
    public void EnableObject(EItemType ItemType_, Vector3 Pos_)
    {
        SetActiveObject(true);
        _ItemType = ItemType_;
        gameObject.transform.localPosition = Pos_;
        gameObject.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        SetActive(_ItemType);
        _IsMove = false;
    }
    public void DisableObject()
    {
        SetActiveObject(false);
    }
    public string GetTag()
    {
        return _ItemType.ToString();
    }
    public EItemType GetItemType()
    {
        return _ItemType;
    }
    public bool GetActive()
    {
        return _IsActive;
    }
    public bool GetMove()
    {
        return _IsMove;
    }
    private void SetActive(EItemType ItemType_)
    {
        switch(ItemType_)
        {
            case EItemType.Coin:
                _Coin.SetActive(true);
                break;
            case EItemType.GoldBar:
                _GoldBar.SetActive(true);
                break;
            case EItemType.Item_Stamina:
                _StaminaItem.SetActive(true);
                break;
            case EItemType.Item_Shield:
                _Shield.SetActive(true);
                break;
            case EItemType.Item_Point:
                _Point.SetActive(true);
                break;
            case EItemType.Item_Random:
                Int32 Ran = UnityEngine.Random.Range(0, 100);
                EMultiItemType Item = EMultiItemType.Ink;
                foreach(var i in CGlobal.MetaData.MultiItemDodgeMetas)
                {
                    if(Ran < i.Value.MultiDodgeRand)
                    {
                        Item = i.Key;
                        break;
                    }
                }
                switch (Item)
                {
                    case EMultiItemType.Ink:
                        _Ink.SetActive(true);
                        break;
                    case EMultiItemType.Scale:
                        _Scale.SetActive(true);
                        break;
                    case EMultiItemType.Slow:
                        _Slow.SetActive(true);
                        break;
                }
                break;
            case EItemType.Trap:
                _Trap.SetActive(true);
                break;
            case EItemType.Dia:
                _Dia.SetActive(true);
                break;
        }
    }
    private void Update()
    {
        if(_IsActive && _IsMove)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _EndPos, _Velocity * Time.deltaTime);
            if (transform.localPosition == _EndPos)
            {
                DisableObject();
            }
        }
    }
}
