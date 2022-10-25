using System;

namespace rso.physics
{
    public class CRectCollider2D : CCollider2D
    {
        SRectCollider2D _Collider = null;
        public SRect Rect
        {
            get
            {
                return _Collider.ToRect().Multi(LocalScale).Add(Position);
            }
        }

        public CRectCollider2D(STransform Transform_, Int32 Number_, SRectCollider2D Collider_) :
            base(Transform_, Number_)
        {
            _Collider = Collider_;
        }
        public CRectCollider2D(STransform Transform_, Int32 Number_, SRectCollider2D Collider_, CObject2D Parent_) :
            base(Transform_, Parent_, Number_)
        {
            _Collider = Collider_;
        }
        public void SetSize(SPoint Size_)
        {
            _Collider.Size.Set(Size_);
        }
        public void SetSizeX(float X_)
        {
            _Collider.Size.X = X_;
        }
        public void SetSizeY(float Y_)
        {
            _Collider.Size.Y = Y_;
        }
        public override bool CheckOverlapped(Int64 tick, CMovingObject2D MovingObject_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
        {
            return OtherCollider_.CheckOverlapped(tick, OtherMovingObject_, this, MovingObject_);
        }
        public override bool CheckOverlapped(Int64 tick, CMovingObject2D MovingObject_, CRectCollider2D OtherRectCollider_, CMovingObject2D OtherMovingObject_)
        {
            CPlayerObject2D PlayerObject = MovingObject_?.GetPlayerObject2D();
            CPlayerObject2D OtherPlayerObject = OtherMovingObject_?.GetPlayerObject2D();

            if (!Enabled || !OtherRectCollider_.Enabled || !CPhysics.IsOverlappedRectRect(Rect, OtherRectCollider_.Rect))
            {
                PlayerObject?.NotOverlapped(tick, this, OtherRectCollider_);
                OtherPlayerObject?.NotOverlapped(tick, OtherRectCollider_, this);
                return false;
            }

            if (IsTrigger || OtherRectCollider_.IsTrigger)
            {
                if (PlayerObject != null)
                {
                    var DoRemove = PlayerObject.Triggered(this, OtherRectCollider_, OtherMovingObject_);

                    if (OtherPlayerObject == null)
                        return DoRemove;

                    OtherPlayerObject.Triggered(OtherRectCollider_, this, MovingObject_);
                    return false;
                }
                else if (OtherPlayerObject != null)
                {
                    return OtherPlayerObject.Triggered(OtherRectCollider_, this, MovingObject_);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (PlayerObject != null)
                {
                    var Normal = _FixPositionAndGetNormal(MovingObject_, Rect, OtherRectCollider_.Rect);

                    var DoRemove = PlayerObject.Collided(
                        tick,
                        new SCollision2D(
                            OtherMovingObject_ != null ? OtherMovingObject_.Velocity.GetSub(MovingObject_.Velocity) : MovingObject_.Velocity.GetMulti(-1.0f),
                            Normal,
                            this,
                            OtherRectCollider_,
                            OtherMovingObject_));

                    if (OtherPlayerObject == null)
                        return DoRemove;

                    OtherPlayerObject.Collided(
                        tick,
                        new SCollision2D(
                            MovingObject_.Velocity.GetSub(OtherMovingObject_.Velocity),
                            Normal.Multi(-1.0f),
                            OtherRectCollider_,
                            this,
                            MovingObject_));
                    return false; // PlayerObject, OtherPlayerObject 둘다 유효하면 return false; Player가 Player가 아닌 대상만 Remove 할 수 있음.
                }
                else if (OtherPlayerObject != null)
                {
                    var Normal = _FixPositionAndGetNormal(OtherMovingObject_, OtherRectCollider_.Rect, Rect);
                    return OtherPlayerObject.Collided(
                        tick,
                        new SCollision2D(
                            OtherMovingObject_.Velocity.GetMulti(-1.0f),
                            Normal,
                            OtherRectCollider_,
                            this,
                            MovingObject_));
                }
                else
                {
                    return false;
                }
            }
        }
        SPoint _FixPositionAndGetNormal(CMovingObject2D MovingObject_, SRect Rect_, SRect RectOther_)
        {
            var rl = RectOther_.Right - Rect_.Left;
            var lr = Rect_.Right - RectOther_.Left;
            var tb = RectOther_.Top - Rect_.Bottom;
            var bt = Rect_.Top - RectOther_.Bottom;

            var Normal = new SPoint();

            if (rl < lr) // Normal.X : +
            {
                if (tb < bt) // Normal.Y : +
                {
                    if (rl < tb) // select Normal.X
                    {
                        if (rl > CEngine.ContactOffset)
                            MovingObject_.LocalPosition.X += (rl - CEngine.ContactOffset);

                        Normal.X = 1.0f;
                    }
                    else // select Normal.Y
                    {
                        if (tb > CEngine.ContactOffset)
                            MovingObject_.LocalPosition.Y += (tb - CEngine.ContactOffset);

                        Normal.Y = 1.0f;
                    }
                }
                else // Normal.Y : -
                {
                    if (rl < bt) // select Normal.X
                    {
                        if (rl > CEngine.ContactOffset)
                            MovingObject_.LocalPosition.X += (rl - CEngine.ContactOffset);

                        Normal.X = 1.0f;
                    }
                    else // select Normal.Y
                    {
                        if (bt > CEngine.ContactOffset)
                            MovingObject_.LocalPosition.Y += (CEngine.ContactOffset - bt);

                        Normal.Y = -1.0f;
                    }
                }
            }
            else // Normal.X : -
            {
                if (tb < bt) // Normal.Y : +
                {
                    if (lr < tb) // select Normal.X
                    {
                        if (lr > CEngine.ContactOffset)
                            MovingObject_.LocalPosition.X += (CEngine.ContactOffset - lr);

                        Normal.X = -1.0f;
                    }
                    else // select Normal.Y
                    {
                        if (tb > CEngine.ContactOffset)
                            MovingObject_.LocalPosition.Y += (tb - CEngine.ContactOffset);

                        Normal.Y = 1.0f;
                    }
                }
                else // Normal.Y : -
                {
                    if (lr < bt) // select Normal.X
                    {
                        if (lr > CEngine.ContactOffset)
                            MovingObject_.LocalPosition.X += (CEngine.ContactOffset - lr);

                        Normal.X = -1.0f;
                    }
                    else // select Normal.Y
                    {
                        if (bt > CEngine.ContactOffset)
                            MovingObject_.LocalPosition.Y += (CEngine.ContactOffset - bt);

                        Normal.Y = -1.0f;
                    }
                }
            }

            return Normal;
        }
    }
}
