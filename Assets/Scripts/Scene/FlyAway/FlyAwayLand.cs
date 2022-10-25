using bb;
using rso.Base;
using rso.physics;
using rso.unity;
using System;
using UnityEngine;

public class FlyAwayLand : MonoBehaviour
{
    System.Random _Random = new System.Random();
    CFlyAwayLand _Land;

    public void Init(CFlyAwayLand Land_)
    {
        _Land = Land_;
    }
    public void Update()
    {
        if (_Land.State == EFlyAwayLandState.Shaking)
        {
            transform.localPosition = new Vector2(
                _Land.LocalPosition.X + (float)((_Random.NextDouble() - 0.5) * 0.01),
                _Land.LocalPosition.Y + (float)((_Random.NextDouble() - 0.5) * 0.01));
        }
        else
        {
            transform.localPosition = _Land.LocalPosition.ToVector2();
        }
    }
}
public class CFlyAwayLand : CFlyAwayObject
{
    public Int32 LandNumber { get; private set; }
    public Int32 Index { get; private set; }
    public CList<SFlyAwayLandObject>.SIterator Iterator;
    public EFlyAwayLandState State = EFlyAwayLandState.Normal;
    public Int64 NextActionTick = 0;
    float _YVelocity = 0.0f;

    public CFlyAwayLand(SPoint LocalPosition_, Int32 LandNumber_, Int32 Index_, EFlyAwayLandState State_, Int64 NextActionTick_) :
        base(CPhysics.GetDefaultTransform(LocalPosition_), CEngineGlobal.c_LandNumber, CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Lands[Index_].Collider)
    {
        LandNumber = LandNumber_;
        Index = Index_;
        State = State_;
        NextActionTick = NextActionTick_;
    }
    public CFlyAwayLand(SPoint LocalPosition_, Int32 LandNumber_, Int32 Index_) :
        this(LocalPosition_, LandNumber_, Index_, EFlyAwayLandState.Normal, 0)
    {
    }
    public override void Proc(CFlyAwayBattlePlayer Player_, FlyAwayBattleScene Battle_)
    {
    }
    public override string GetPrefabName()
    {
        return CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Lands[Index].PrefabName;
    }
    public bool StartShake(Int64 tick)
    {
        if (State != EFlyAwayLandState.Normal)
            return false;

        State = EFlyAwayLandState.Shaking;
        NextActionTick = tick + 15000000;

        return true;
    }
    public void FixedUpdate(Int64 tick)
    {
        if (State == EFlyAwayLandState.Normal)
            return;

        switch (State)
        {
            case EFlyAwayLandState.Shaking:
                {
                    if (tick >= NextActionTick)
                    {
                        Enabled = false;
                        State = EFlyAwayLandState.Falling;
                        NextActionTick = tick + 20000000;
                    }
                }
                break;

            case EFlyAwayLandState.Falling:
                {
                    if (tick < NextActionTick)
                    {
                        _YVelocity += global.c_Gravity * CEngine.DeltaTime;
                        LocalPosition.Y += (CEngine.DeltaTime * (0.5f * global.c_Gravity * CEngine.DeltaTime + _YVelocity));
                    }
                }
                break;

            default:
                break;
        }
    }
}
