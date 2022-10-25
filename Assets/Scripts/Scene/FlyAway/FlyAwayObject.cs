using rso.physics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CFlyAwayObject : CRectCollider2D
{
    public CFlyAwayObject(STransform Transform_, Int32 objectNumber, SRectCollider2D Collider_) :
        base(Transform_, objectNumber, Collider_)
    {
    }
    public abstract void Proc(CFlyAwayBattlePlayer Player_, FlyAwayBattleScene Battle_);
    public abstract string GetPrefabName();
}