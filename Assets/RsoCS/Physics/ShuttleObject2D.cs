using System;
using System.Collections.Generic;

namespace rso.physics
{
    public class CShuttleObject2D : CMovingObject2D
    {
        SPoint _BeginPos;
        SPoint _EndPos;
        Single _ScalarVelocity;
        Single _Delay;
        SStructMove _StructMove;

        public CShuttleObject2D(STransform Transform_, List<CCollider2D> Colliders_, SPoint BeginPos_, SPoint EndPos_, Single ScalarVelocity_, Single Delay_, SStructMove StructMove_) :
            base(Transform_, Colliders_, new SPoint())
        {
            fFixedUpdate = _FixedUpdate;
            _BeginPos = BeginPos_;
            _EndPos = EndPos_;
            _ScalarVelocity = ScalarVelocity_;
            _Delay = Delay_;
            _StructMove = StructMove_;

            if (_StructMove.IsMoving)
                _SetMovingVelocity();
        }
        void _SetMovingVelocity()
        {
            var Vector = _EndPos.GetSub(_BeginPos).GetMulti(_StructMove.Direction);
            Velocity = Vector.Multi(_ScalarVelocity / Vector.GetScalar());
        }
        public void _FixedUpdate(Int64 tick)
        {
            if (_StructMove.IsMoving)
            {
                if (_StructMove.Direction == 1)
                {
                    if (CPhysics.MoveTowards(LocalPosition, _EndPos, _ScalarVelocity * CEngine.DeltaTime))
                    {
                        _StructMove.Direction = -1;
                        _StructMove.IsMoving = false;
                        Velocity.Clear();
                    }
                }
                else
                {
                    if (CPhysics.MoveTowards(LocalPosition, _BeginPos, _ScalarVelocity * CEngine.DeltaTime))
                    {
                        _StructMove.Direction = 1;
                        _StructMove.IsMoving = false;
                        Velocity.Clear();
                    }
                }
            }
            else
            {
                _StructMove.StoppedDuration += CEngine.DeltaTime;

                if (_StructMove.StoppedDuration >= _Delay)
                {
                    _StructMove.StoppedDuration = 0.0f;
                    _StructMove.IsMoving = true;
                    _SetMovingVelocity();
                }
            }
        }
    }
}