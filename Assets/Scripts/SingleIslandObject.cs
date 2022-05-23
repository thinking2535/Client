using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleIslandObject : MonoBehaviour
{
    public enum EItemType
    {
        Null,
        Coin,
        GoldBar,
        Item_Apple,
        Item_Meat,
        Item_Chicken,
        Item_Point,
        Item_Random
    }

    private bool _IsActive = false;
    private Int32 _IslandType = 0;
    [SerializeField] GameObject[] _IsLands = null;
    [SerializeField] GameObject[] _TrapIslandWidths = null;
    [SerializeField] GameObject[] _TrapIslandHeights = null;
    [SerializeField] GameObject _Coin = null;
    [SerializeField] GameObject _GoldBar = null;
    [SerializeField] GameObject _ItemApple = null;
    [SerializeField] GameObject _ItemMeat = null;
    [SerializeField] GameObject _ItemChicken = null;
    [SerializeField] GameObject _ItemPoint = null;
    [SerializeField] GameObject _ItemInk = null;
    [SerializeField] GameObject _ItemScale = null;
    [SerializeField] GameObject _ItemSlow = null;
    [SerializeField] GameObject _IslandPoint = null;
    private EItemType _ItemType = EItemType.Null;
    private Int32 _IslandCount = 0;
    private bool _IsLanding = false;
    private float _LandDelayTime = 0.0f;
    private float _LandDelayTimeMax = 0.0f;
    private bool _IsSpike = false;
    private float _StaminaRecovery = 0.0f;
    private bool _IsFall = false;
    private float _FallVelocity = 1.0f;
    private bool _IsShack = false;
    private Vector3 OriginPosition;
    private Quaternion OriginRotation;
    private void SetActiveObject(bool IsActive_)
    {
        _IsActive = IsActive_;
        gameObject.SetActive(IsActive_);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;
        foreach(var i in _IsLands)
            i.SetActive(false);
        foreach(var i in _TrapIslandWidths)
        {
            i.SetActive(false);
            i.GetComponent<MoveObject>().IsMove = false;
        }
        foreach (var i in _TrapIslandHeights)
        {
            i.SetActive(false);
            i.GetComponent<MoveObject>().IsMove = false;
        }

        _Coin.SetActive(false);
        _GoldBar.SetActive(false);
        _ItemApple.SetActive(false);
        _ItemMeat.SetActive(false);
        _ItemChicken.SetActive(false);
        _ItemPoint.SetActive(false);
        _ItemInk.SetActive(false);
        _ItemScale.SetActive(false);
        _ItemSlow.SetActive(false);
        _IslandPoint.SetActive(false);
        _IsLanding = false;
        _LandDelayTime = 0.0f;
        _IsShack = false;
    }
    public void EnableObject(Int32 IslandType_, Vector3 Pos_, Int32 IslandCount_, float LandDelayTimeMax_, bool IsSpike_, Int32 SpikeCount_, float StaminaRecovery_, EItemType Type_, bool IsMulti_)
    {
        EnableObject(IslandType_, Pos_, IslandCount_, LandDelayTimeMax_, IsSpike_, SpikeCount_, StaminaRecovery_, IsMulti_);
        _ItemType = Type_;
        switch(Type_)
        {
            case EItemType.Coin:
                _Coin.SetActive(true);
                break;
            case EItemType.GoldBar:
                _GoldBar.SetActive(true);
                break;
            case EItemType.Item_Apple:
                _ItemApple.SetActive(true);
                break;
            case EItemType.Item_Meat:
                _ItemMeat.SetActive(true);
                break;
            case EItemType.Item_Chicken:
                _ItemChicken.SetActive(true);
                break;
            case EItemType.Item_Point:
                _ItemPoint.SetActive(true);
                break;
            case EItemType.Item_Random:
                Int32 Ran = UnityEngine.Random.Range(0, 100);
                EMultiItemType Item = EMultiItemType.Ink;
                foreach (var i in CGlobal.MetaData.MultiItemIslandMetas)
                {
                    if (Ran < i.Value.MultiIslandRand)
                    {
                        Item = i.Key;
                        break;
                    }
                }
                switch (Item)
                {
                    case EMultiItemType.Ink:
                        _ItemInk.SetActive(true);
                        break;
                    case EMultiItemType.Scale:
                        _ItemScale.SetActive(true);
                        break;
                    case EMultiItemType.Slow:
                        _ItemSlow.SetActive(true);
                        break;
                }
                break;
        }
    }
    public void EnableObject(Int32 IslandType_, Vector3 Pos_, Int32 IslandCount_, float LandDelayTimeMax_, bool IsSpike_, Int32 SpikeCount_, float StaminaRecovery_, bool IsMulti_)
    {
        SetActiveObject(true);
        _IslandType = IslandType_;
        gameObject.transform.localPosition = Pos_;
        _ItemType = EItemType.Null;
        _IslandCount = IslandCount_;
        _IsLanding = false;
        _LandDelayTime = 0.0f;
        _LandDelayTimeMax = LandDelayTimeMax_;
        _IsSpike = IsSpike_;
        Int32 BalanceCount = SpikeCount_ / CGlobal.MetaData.SingleIslandBalanceMeta.SpikeIslandBalanceCount;
        if(_IsSpike)
        {
            if(IslandCount_ == 0)
            {
                if (BalanceCount < 1)
                {
                    _TrapIslandWidths[_IslandType].SetActive(true);
                    _TrapIslandWidths[_IslandType].GetComponent<MoveObject>().IsMove = false;
                    _TrapIslandWidths[_IslandType].transform.localPosition = Vector3.zero;
                }
                else
                {
                    _TrapIslandWidths[_IslandType].SetActive(true);
                    _TrapIslandWidths[_IslandType].GetComponent<MoveObject>().StartMove();
                }
            }
            else
            {
                if (BalanceCount < 1)
                {
                    var Ran = UnityEngine.Random.Range(0, 100);
                    if (Ran < 50)
                    {
                        _TrapIslandWidths[_IslandType].SetActive(true);
                        _TrapIslandWidths[_IslandType].GetComponent<MoveObject>().IsMove = false;
                        _TrapIslandWidths[_IslandType].transform.localPosition = Vector3.zero;
                    }
                    else
                    {
                        _TrapIslandHeights[_IslandType].SetActive(true);
                        _TrapIslandHeights[_IslandType].GetComponent<MoveObject>().IsMove = false;
                    }
                }
                else
                {
                    var Ran = UnityEngine.Random.Range(0, 100);
                    if (Ran < 50)
                    {
                        _TrapIslandHeights[_IslandType].SetActive(true);
                        _TrapIslandHeights[_IslandType].GetComponent<MoveObject>().StartMove();
                    }
                    else
                    {
                        _TrapIslandWidths[_IslandType].SetActive(true);
                        _TrapIslandWidths[_IslandType].GetComponent<MoveObject>().StartMove();
                    }
                }
            }
        }
        else
        {
            _IsLands[_IslandType].SetActive(true);
            _IsLands[_IslandType].transform.localPosition = Vector3.zero;
            _IslandPoint.SetActive(IsMulti_);
        }
        _StaminaRecovery = StaminaRecovery_;
        _IsFall = false;
        _IsShack = false;
    }
    public void DisableObject()
    {
        SetActiveObject(false);
    }
    public Int32 GetIslandType()
    {
        return _IslandType;
    }
    public Int32 GetIslandCount()
    {
        return _IslandCount;
    }
    public Int32 GetIslandTypeCount()
    {
        return _IsLands.Length;
    }
    public bool GetActive()
    {
        return _IsActive;
    }
    public bool GetIsSpike()
    {
        return _IsSpike;
    }
    public EItemType GetItemType()
    {
        return _ItemType;
    }
    public float GetStaminaRecovery()
    {
        return _StaminaRecovery;
    }
    public void SetIsLanding(bool IsLanding_)
    {
        if (IsLanding_)
        {
            OriginPosition = transform.position;
            OriginRotation = transform.rotation;
            _StaminaRecovery = 0.0f;
            _IslandPoint.SetActive(false);
        }

        _IsLanding = IsLanding_;
    }
    private void Update()
    {
        if(_IsActive)
        {
            if (_IsLanding)
            {
                _LandDelayTime += Time.deltaTime;
                if (!_IsShack && (_LandDelayTimeMax-_LandDelayTime) <= CGlobal.MetaData.SingleIslandBalanceMeta.IslandTimeMin)
                {
                     _IsShack = true;
                }
                if (_LandDelayTime >= _LandDelayTimeMax)
                    _IsFall = true;
                if (_IsShack && !_IsFall)
                {
                    _IsLands[_IslandType].transform.position = new Vector3(OriginPosition.x + UnityEngine.Random.insideUnitSphere.x * Time.deltaTime, OriginPosition.y, OriginPosition.z);
                    _IsLands[_IslandType].transform.rotation = new Quaternion(
                        OriginRotation.x - UnityEngine.Random.Range(-0.01f, 0.01f) * Time.deltaTime,
                        OriginRotation.y - UnityEngine.Random.Range(-0.01f, 0.01f) * Time.deltaTime,
                        OriginRotation.z - UnityEngine.Random.Range(-0.01f, 0.01f) * Time.deltaTime,
                        OriginRotation.w - UnityEngine.Random.Range(-0.01f, 0.01f) * Time.deltaTime
                        );
                }
            }
            if (_IsFall)
            {
                _IsLands[_IslandType].transform.position -= new Vector3(0.0f, _FallVelocity * Time.deltaTime, 0.0f);
                if (_IsLands[_IslandType].transform.localPosition.y <= -1.1f)
                    _IsFall = false;
            }
        }
    }
}
