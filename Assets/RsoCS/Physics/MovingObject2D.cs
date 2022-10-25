using System;
using System.Collections.Generic;

namespace rso.physics
{
    public class CMovingObject2D : CObject2D
    {
        public List<CCollider2D> Colliders { get; private set; } = new List<CCollider2D>();
        public SPoint Velocity;
        public float Mass = 1.0f;

        public CMovingObject2D(STransform Transform_, List<CCollider2D> Colliders_, SPoint Velocity_) :
            base(Transform_)
        {
            Colliders = Colliders_;
            Velocity = Velocity_;

            foreach (var c in Colliders)
                c.SetParent(this);
        }
        public virtual CPlayerObject2D GetPlayerObject2D()
        {
            return null;
        }
        public Action<Int64> fFixedUpdate;
    }
}
