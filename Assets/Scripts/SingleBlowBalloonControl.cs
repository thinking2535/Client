using bb;
using System;
using UnityEngine;

// 펌프질 해서 키우는 풍선 크기 등 컨트롤, 1개만 필요하므로 2개가 존재하는 BlowBalloon 에서 처리하지 않음.
public class CSingleBlowBalloonControl
{
    public delegate void TDoneCallback();

    TDoneCallback _DoneCallback; // 1회 마다 호출됨
    bool _Done = true; // true : 콜백이 호출됨
    public sbyte Count { get; protected set; } = 0; // SCharacter에 BlowCount 는 다른 유저 및 서버에서 받는 용도로 처리
    readonly float _PumpDuration = 0.4f; // 현재 Blow 1회 재생하는데 걸리는 시간
    float _ScaleTo = 0.0f;
    public float Scale { get; private set; } = 0.0f;
    float _ScaleVelocity = 0.0f;

    public CSingleBlowBalloonControl(float PumpSpeed_, TDoneCallback DoneCallback_)
    {
        _PumpDuration /= PumpSpeed_;
        _DoneCallback = DoneCallback_;
    }
    protected void _Blow()
    {
        _ScaleTo = (float)(Count + 1) / global.c_PumpCountForBalloon;
        _ScaleVelocity = (_ScaleTo - Scale) / _PumpDuration;
    }
    public bool CheckAndBlow()
    {
        if (Count >= global.c_PumpCountForBalloon)
            return false;

        if (_IsScaling())
            return false;

        _Blow();
        _Done = false;

        return true;
    }
    public virtual void Blow()
    {
        throw new Exception();
    }
    public void Blow(sbyte Count_)
    {
        Count = Count_;
        _Blow();
    }
    public virtual void Done()
    {
        throw new Exception();
    }
    protected bool _IsScaling()
    {
        return Scale < _ScaleTo;
    }
    public virtual bool Update() // true : Changed
    {
        if (!_IsScaling())
            return false;

        Scale += (_ScaleVelocity * Time.deltaTime);
        if (Scale > _ScaleTo)
            Scale = _ScaleTo;

        if (!_Done)
        {
            ++Count;
            _Done = true;
            _DoneCallback?.Invoke();
        }
        return true;
    }
    public virtual void Clear()
    {
        Count = 0;
        _ScaleTo = 0.0f;
        Scale = 0.0f;
        _Done = true;
    }
}
