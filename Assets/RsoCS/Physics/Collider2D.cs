using System;

namespace rso.physics
{
    public class SCollision2D
    {
        public SPoint RelativeVelocity { get; }
        public SPoint Normal { get; }
        public CCollider2D Collider { get; }
        public CCollider2D OtherCollider { get; }
        public CMovingObject2D OtherMovingObject { get; }
        public SCollision2D(SPoint RelativeVelocity_, SPoint Normal_, CCollider2D Collider_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
        {
            RelativeVelocity = RelativeVelocity_;
            Normal = Normal_;
            Collider = Collider_;
            OtherCollider = OtherCollider_;
            OtherMovingObject = OtherMovingObject_;
        }
    }

    public abstract class CCollider2D : CObject2D
    {
        public Int32 Number { get; protected set; }
        public bool IsTrigger = false;
        public void SetParentCollider(CCollider2D ParentCollider_)
        {
            SetParent(ParentCollider_);
        }
        public CCollider2D(STransform Transform_, Int32 Number_) :
            base(Transform_)
        {
            Number = Number_;
        }
        public CCollider2D(STransform Transform_, CObject2D Parent_, Int32 Number_) :
            base(Transform_, Parent_)
        {
            Number = Number_;
        }

        // 결국 CEngineObjectRect 과 CEngineObjectRect 에 대한 계산만 가능하고, 파라미터가 CEngineObject일 때는 주체가 되어야 CEngineObjectRect인지 CEngineObjectContainer인지 파악이 가능하므로
        // 먼저 주체가 CEngineObjectRect일때까지 들어간 후 이후 파라미터를 다시 주체로 바꾸어 CEngineObjectRect 일때 까지 CollisionCheck 를 호출하여
        // 주체와 파라미터를 모두 CEngineObjectRect 로 만든 후 처리
        public abstract bool CheckOverlapped(Int64 tick, CMovingObject2D MovingObject_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_);
        public abstract bool CheckOverlapped(Int64 tick, CMovingObject2D MovingObject_, CRectCollider2D OtherRectCollider_, CMovingObject2D OtherMovingObject_);
    }
}
