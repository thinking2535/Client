using rso.physics;
using System;
using System.Collections.Generic;

namespace rso.physics
{
    public delegate void FCollisionStay(Int64 Tick_, Dictionary<CCollider2D, SPoint> AttachedColliders_);
    public delegate void FCollisionExit(Int64 Tick_, CCollider2D Collider_, CCollider2D OtherCollider_);
    public abstract class CCollider2D : CObject2D
    {
        public Int32 Number { get; protected set; }
        public bool Enabled = true;
        public CCollider2D(SPoint LocalPosition_, Int32 Number_) :
            base(LocalPosition_)
        {
            Number = Number_;
        }

        // �ᱹ CEngineObjectRect �� CEngineObjectRect �� ���� ��길 �����ϰ�, �Ķ���Ͱ� CEngineObject�� ���� ��ü�� �Ǿ�� CEngineObjectRect���� CEngineObjectContainer���� �ľ��� �����ϹǷ�
        // ���� ��ü�� CEngineObjectRect�϶����� �� �� ���� �Ķ���͸� �ٽ� ��ü�� �ٲپ� CEngineObjectRect �϶� ���� CollisionCheck �� ȣ���Ͽ�
        // ��ü�� �Ķ���͸� ��� CEngineObjectRect �� ���� �� ó��
        public abstract void OverlappedCheck(Int64 Tick_, CMovingObject2D MovingObject_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_);
        public abstract void OverlappedCheck(Int64 Tick_, CMovingObject2D MovingObject_, CRectCollider2D OtherRectCollider_, CMovingObject2D OtherMovingObject_);
        public abstract bool IsOverlapped(Int64 Tick_, CCollider2D OtherCollider_);
        public abstract bool IsOverlapped(Int64 Tick_, CRectCollider2D OtherRectCollider_);
    }
}
