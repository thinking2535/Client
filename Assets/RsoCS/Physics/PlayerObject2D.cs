using System;
using System.Collections.Generic;
using System.Linq;

namespace rso.physics
{
    using TContactPoint2Ds = Dictionary<SContactPoint2D, SNormalOtherMovingObject>;
    public struct SNormalOtherMovingObject
    {
        public SPoint Normal;
        public CMovingObject2D OtherMovingObject;

        public SNormalOtherMovingObject(SPoint Normal_, CMovingObject2D OtherMovingObject_)
        {
            Normal = Normal_; ;
            OtherMovingObject = OtherMovingObject_;
        }
    }
    public struct SContactPoint2D
    {
        public CCollider2D Collider;
        public CCollider2D OtherCollider;

        public SContactPoint2D(CCollider2D Collider_, CCollider2D OtherCollider_)
        {
            Collider = Collider_;
            OtherCollider = OtherCollider_;
        }
    }
    public abstract class CPlayerObject2D : CMovingObject2D
    {
        // key를 SContactPoint2D으로 쓰는 이유는 여러개의 Collider를 가지는 객체의 OnCollisionStay 에서
        // 마찰 처리등이 모든 _ContactPoint2Ds 에 대하여 계산되어야 하고,
        // FixedUpdate당 한번 계산되어야 하기 때문에 _ContactPoint2Ds 가 CMovingObject2D 에 존재해야하고
        // 다수의 Collider를 가지는 CMovingObject2D 는 다대다로 Contact가 일어나기 때문에
        TContactPoint2Ds _ContactPoint2Ds = new TContactPoint2Ds();
        public CPlayerObject2D(SPoint LocalPosition_, CCollider2D Collider_, SPoint Velocity_) :
            base(LocalPosition_, Collider_, Velocity_)
        {
        }
        public override CPlayerObject2D GetPlayerObject2D()
        {
            return this;
        }
        public void Overlapped(Int64 Tick_, SPoint Normal_, CCollider2D Collider_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
        {
            var Key = new SContactPoint2D(Collider_, OtherCollider_);
            if (!_ContactPoint2Ds.ContainsKey(Key))
            {
                _ContactPoint2Ds.Add(Key, new SNormalOtherMovingObject(Normal_, OtherMovingObject_));
                OnCollisionEnter(Tick_, Normal_, Collider_, OtherCollider_, OtherMovingObject_);
            }
        }
        public void CollisionEnterCheck(Int64 Tick_, CCollider2D OtherCollider_)
        {
            Collider.OverlappedCheck(Tick_, this, OtherCollider_, null);
        }
        public void CollisionEnterCheck(Int64 Tick_, CMovingObject2D OtherMovingObject_)
        {
            Collider.OverlappedCheck(Tick_, this, OtherMovingObject_.Collider, OtherMovingObject_);
        }
        protected virtual void OnCollisionEnter(Int64 Tick_, SPoint Normal_, CCollider2D Collider_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
        {
        }
        public void CollisionStayCheck(Int64 Tick_)
        {
            OnCollisionStay(Tick_, _ContactPoint2Ds);
        }
        protected virtual void OnCollisionStay(Int64 Tick_, TContactPoint2Ds ContactPoint2Ds_)
        {
        }
        public void CollisionExitCheck(Int64 Tick_)
        {
            foreach (var k in _ContactPoint2Ds.Keys.ToList())
            {
                if (!k.Collider.IsOverlapped(Tick_, k.OtherCollider))
                {
                    var Value = _ContactPoint2Ds[k];
                    _ContactPoint2Ds.Remove(k);
                    OnCollisionExit(Tick_, Value.Normal, k.Collider, k.OtherCollider, Value.OtherMovingObject);
                }
            }
        }
        protected virtual void OnCollisionExit(Int64 Tick_, SPoint Normal_, CCollider2D Collider_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
        {
        }
    }
}