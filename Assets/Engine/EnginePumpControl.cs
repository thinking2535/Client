using bb;
using rso.physics;
using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class CEnginePumpControl
{
    public delegate void FPump();
    public delegate void FPumpDone();

    FPump _fPump;
    FPumpDone _fPumpDone;
    readonly float _PumpSpeed = 0.0f; // 1회 Pump Speed
    SPumpInfo _PumpInfo = null;
    float _ScaleTo = 0.0f;
    void _SetScaleTo()
    {
        _ScaleTo = (float)(_PumpInfo.Count + 1) / global.c_PumpCountForBalloon;
    }
    public CEnginePumpControl(FPump fPump_, FPumpDone fPumpDone_, float PumpSpeed_, SPumpInfo PumpInfo_)
    {
        _fPump = fPump_;
        _fPumpDone = fPumpDone_;
        _PumpSpeed = PumpSpeed_ / global.c_PumpCountForBalloon / global.c_OnePumpDuration;
        _PumpInfo = PumpInfo_;
        _SetScaleTo();
    }
    void _Pump()
    {
        _SetScaleTo();
        _fPump();
    }
    public bool Pump()
    {
        if (_PumpInfo.CountTo >= global.c_PumpCountForBalloon ||
            _PumpInfo.CountTo - _PumpInfo.Count > 1)
            return false;

        if (!_PumpInfo.IsScaling())
            _Pump();

        ++_PumpInfo.CountTo;

        return true;
    }
    public void FixedUpdate()
    {
        if (!_PumpInfo.IsScaling())
            return;

        _PumpInfo.Scale += (_PumpSpeed * CEngine.DeltaTime);

        if (_PumpInfo.Scale < _ScaleTo)
            return;

        ++_PumpInfo.Count;

        if (_PumpInfo.IsScaling())
        {
            _Pump();
            return;
        }
        else if (_PumpInfo.Count >= global.c_PumpCountForBalloon)
        {
            Clear();
            _fPumpDone();
        }
    }
    public void Clear()
    {
        _PumpInfo.Count = 0;
        _PumpInfo.CountTo = 0;
        _PumpInfo.Scale = 0.0f;
        _ScaleTo = 0.0f;
    }
}
