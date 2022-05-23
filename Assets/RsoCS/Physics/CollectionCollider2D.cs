using System;
using System.Collections.Generic;

namespace rso.physics
{
    public class CCollectionCollider2D : CCollider2D
    {
        List<CCollider2D> _Colliders = new List<CCollider2D>();

        public CCollectionCollider2D(SPoint LocalPosition_, Int32 Number_, List<CCollider2D> Colliders_) :
            base(LocalPosition_, Number_)
        {
            _Colliders = Colliders_;

            foreach (var c in _Colliders)
                c.SetParent(this);
        }
        public override void OverlappedCheck(Int64 Tick_, CMovingObject2D MovingObject_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
        {
            foreach (var o in _Colliders)
                o.OverlappedCheck(Tick_, MovingObject_, OtherCollider_, OtherMovingObject_);
        }
        public override void OverlappedCheck(Int64 Tick_, CMovingObject2D MovingObject_, CRectCollider2D OtherRectCollider_, CMovingObject2D OtherMovingObject_)
        {
            foreach (var o in _Colliders)
                o.OverlappedCheck(Tick_, MovingObject_, OtherRectCollider_, OtherMovingObject_);
        }
        public override bool IsOverlapped(Int64 Tick_, CCollider2D OtherCollider_)
        {
            throw new Exception();
        }
        public override bool IsOverlapped(Int64 Tick_, CRectCollider2D OtherRectCollider_)
        {
            throw new Exception();
        }
    }
}