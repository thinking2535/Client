using rso.Base;
using rso.physics;
using rso.unity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    CArrow _Arrow;

    public void Init(CArrow Arrow_)
    {
        _Arrow = Arrow_;

        var Radian = CPhysics.Atan2(_Arrow.Velocity);
        var Degree = Radian * 180.0f / rso.math.CMath.c_PI_F;
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, Degree + 180.0f);
    }
    public void Update()
    {
        transform.localPosition = _Arrow.LocalPosition.ToVector2();
    }
}

public class CArrow : CMovingObject2D
{
    public CList<SArrowObject>.SIterator Iterator;
    public CArrow(SPoint LocalPosition_, List<CCollider2D> Colliders_, SPoint Velocity_) :
        base(CPhysics.GetDefaultTransform(LocalPosition_), Colliders_, Velocity_)
    {
    }
    public bool FixedUpdate() // true : 화면 내에서 보여져야 하는 화살
    {
        LocalPosition.Add(Velocity.GetMulti(CEngine.DeltaTime));

        return (LocalPosition.X > -bb.global.arrowDodgeArrowActiveAreaHalfWidth &&
            LocalPosition.X < bb.global.arrowDodgeArrowActiveAreaHalfWidth &&
            LocalPosition.Y > bb.global.arrowDodgeArrowActiveBottom &&
            LocalPosition.Y < bb.global.arrowDodgeArrowActiveTop);
    }
}