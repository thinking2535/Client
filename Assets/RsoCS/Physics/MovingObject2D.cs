using System;
using System.Collections.Generic;

namespace rso.physics
{
    public abstract class CMovingObject2D : CObject2D
    {
        public CCollider2D Collider;
        public SPoint Velocity;

        public CMovingObject2D(SPoint LocalPosition_, CCollider2D Collider_, SPoint Velocity_) :
            base(LocalPosition_)
        {
            Collider = Collider_;
            Collider.SetParent(this);
            Velocity = Velocity_;
        }
        public abstract void FixedUpdate(Int64 Tick_);
        public virtual CPlayerObject2D GetPlayerObject2D()
        {
            return null;
        }
    }
}
