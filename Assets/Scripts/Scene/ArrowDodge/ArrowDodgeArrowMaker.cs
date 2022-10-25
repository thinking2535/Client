using bb;
using rso.gameutil;
using rso.physics;
using rso.unity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CArrowDodgeArrowMaker
{
    CFixedRandom32 _FixedRandom;
    Int64 _NextDownArrowTick;
    Int64 _NextLeftArrowTick;
    Int64 _NextRightArrowTick;

    public CArrowDodgeArrowMaker(CFixedRandom32 FixedRandom_, Int64 NextDownArrowTick_, Int64 NextLeftArrowTick_, Int64 NextRightArrowTick_)
    {
        _FixedRandom = FixedRandom_;
        _NextDownArrowTick = NextDownArrowTick_;
        _NextLeftArrowTick = NextLeftArrowTick_;
        _NextRightArrowTick = NextRightArrowTick_;
    }
    static SRectCollider2D _GetArrowCollider2D(SPoint Velocity_)
    {
        if (Velocity_.X < 0.0f)
            return CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Arrow.Collider;
        else if (Velocity_.X > 0.0f)
            return CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Arrow.Collider.Get180Rotated();
        else if (Velocity_.Y < 0.0f)
            return CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Arrow.Collider.Get90Rotated();
        else
            return CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Arrow.Collider.Get270Rotated();
    }
    internal static Int64 _GetDownArrowDuration(Int64 Tick_)
    {
        return Tick_ > global.arrowDodgeFirstLeftArrowTick ? 5000000 : -Tick_ / 20 + 20000000;
    }
    internal static Int64 _GetLeftArrowDuration(Int64 Tick_)
    {
        return Tick_ > global.arrowDodgeFirstRightArrowTick ? 10000000 : -(Tick_ - global.arrowDodgeFirstLeftArrowTick) / 30 + 20000000;
    }
    internal static Int64 _GetRightArrowDuration(Int64 Tick_)
    {
        return Tick_ > global.arrowDodgeMaxArrowTick ? 10000000 : -(Tick_ - global.arrowDodgeFirstRightArrowTick) / 30 + 20000000;
    }
    public void FixedUpdate(Int64 tick, ArrowDodgeBattleScene battle, Action<CArrow> fAddArrow_)
    {
        float velocityAdded = ((float)(tick / global.arrowDodgeArrowBaseVelocityTick)) * global.arrowDodgeArrowBaseVelocity;

        if (tick > _NextDownArrowTick)
        {
            _NextDownArrowTick += _GetDownArrowDuration(tick);

            var Velocity = new SPoint(0.0f, -(global.arrowDodgeMinDownVelocity + velocityAdded));
            fAddArrow_(MakeArrow(new SPoint(battle.getRandomItemPointX(), global.c_ScreenHeight_2 + global.arrowDodgeArrowCreateAreaHalfHeight), Velocity));
        }

        if (tick > _NextLeftArrowTick)
        {
            _NextLeftArrowTick += _GetLeftArrowDuration(tick);

            var Velocity = new SPoint(-(global.arrowDodgeMinHorizontalVelocity + velocityAdded), 0.0f);
            fAddArrow_(MakeArrow(new SPoint(global.arrowDodgeArrowCreateAreaHalfWidth, battle.getRandomItemPointY()), Velocity));
        }

        if (tick > _NextRightArrowTick)
        {
            _NextRightArrowTick += _GetRightArrowDuration(tick);

            var Velocity = new SPoint((global.arrowDodgeMinHorizontalVelocity + velocityAdded), 0.0f);
            fAddArrow_(MakeArrow(new SPoint(-global.arrowDodgeArrowCreateAreaHalfWidth, battle.getRandomItemPointY()), Velocity));
        }
    }
    public static CArrow MakeArrow(SPoint LocalPosition_, SPoint Velocity_)
    {
        return new CArrow(
                LocalPosition_,
                new List<CCollider2D> { new CRectCollider2D(CPhysics.ZeroTransform, CEngineGlobal.c_ArrowNumber, _GetArrowCollider2D(Velocity_)) },
                Velocity_);
    }
}