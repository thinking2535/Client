using System;

namespace rso.physics
{
    public class CRectCollider2D : CCollider2D
    {
        SRectCollider2D _Collider = null;
        public SRect _SizeToRect(SPoint Size_)
        {
            return new SRect(-Size_.X * 0.5f, Size_.X * 0.5f, -Size_.Y * 0.5f, Size_.Y * 0.5f);
        }
        public SRect Rect
        {
            get
            {
                return _SizeToRect(_Collider.Size).Add(_Collider.Offset).Multi(_Collider.Scale).Add(Position);
            }
        }

        public CRectCollider2D(SPoint LocalPosition_, Int32 Number_, SRectCollider2D Collider_) :
            base(LocalPosition_, Number_)
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
        public void SetScale(float Scale_)
        {
            _Collider.Scale.Set(Scale_);
        }
        bool _OverlappedCheck(CPlayerObject2D PlayerObject_, SRect Rect_, SRect RectOther_, SPoint Normal_)
        {
            if (!CPhysics.IsOverlappedRectRect(Rect_, RectOther_))
                return false;

            var rl = RectOther_.Right - Rect_.Left;
            var lr = Rect_.Right - RectOther_.Left;
            var tb = RectOther_.Top - Rect_.Bottom;
            var bt = Rect_.Top - RectOther_.Bottom;

            if (rl < lr) // Normal.X : +
            {
                if (tb < bt) // Normal.Y : +
                {
                    if (rl < tb) // select Normal.X
                    {
                        if (rl > CEngine.ContactOffset)
                            PlayerObject_.LocalPosition.X += (rl - CEngine.ContactOffset);

                        Normal_.X = 1.0f;
                    }
                    else // select Normal.Y
                    {
                        if (tb > CEngine.ContactOffset)
                            PlayerObject_.LocalPosition.Y += (tb - CEngine.ContactOffset);

                        Normal_.Y = 1.0f;
                    }
                }
                else // Normal.Y : -
                {
                    if (rl < bt) // select Normal.X
                    {
                        if (rl > CEngine.ContactOffset)
                            PlayerObject_.LocalPosition.X += (rl - CEngine.ContactOffset);

                        Normal_.X = 1.0f;
                    }
                    else // select Normal.Y
                    {
                        if (bt > CEngine.ContactOffset)
                            PlayerObject_.LocalPosition.Y += (CEngine.ContactOffset - bt);

                        Normal_.Y = -1.0f;
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
                            PlayerObject_.LocalPosition.X += (CEngine.ContactOffset - lr);

                        Normal_.X = -1.0f;
                    }
                    else // select Normal.Y
                    {
                        if (tb > CEngine.ContactOffset)
                            PlayerObject_.LocalPosition.Y += (tb - CEngine.ContactOffset);

                        Normal_.Y = 1.0f;
                    }
                }
                else // Normal.Y : -
                {
                    if (lr < bt) // select Normal.X
                    {
                        if (lr > CEngine.ContactOffset)
                            PlayerObject_.LocalPosition.X += (CEngine.ContactOffset - lr);

                        Normal_.X = -1.0f;
                    }
                    else // select Normal.Y
                    {
                        if (bt > CEngine.ContactOffset)
                            PlayerObject_.LocalPosition.Y += (CEngine.ContactOffset - bt);

                        Normal_.Y = -1.0f;
                    }
                }
            }

            return true;
        }
        public override void OverlappedCheck(Int64 Tick_, CMovingObject2D MovingObject_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
        {
            OtherCollider_.OverlappedCheck(Tick_, OtherMovingObject_, this, MovingObject_);
        }
        public override void OverlappedCheck(Int64 Tick_, CMovingObject2D MovingObject_, CRectCollider2D OtherRectCollider_, CMovingObject2D OtherMovingObject_)
        {
            if (!Enabled || !OtherRectCollider_.Enabled)
                return;

            CPlayerObject2D PlayerObject = MovingObject_?.GetPlayerObject2D();
            CPlayerObject2D OtherPlayerObject = OtherMovingObject_?.GetPlayerObject2D();

            var Normal = new SPoint();

            if (PlayerObject != null)
            {
                if (!_OverlappedCheck(PlayerObject, Rect, OtherRectCollider_.Rect, Normal))
                    return;
            }
            else if (OtherPlayerObject != null)
            {
                if (!_OverlappedCheck(OtherPlayerObject, OtherRectCollider_.Rect, Rect, Normal))
                    return;

                Normal.Multi(-1.0f);
            }
            else
            {
                return;
            }

            PlayerObject?.Overlapped(Tick_, Normal, this, OtherRectCollider_, OtherMovingObject_);
            OtherPlayerObject?.Overlapped(Tick_, Normal.Multi(-1.0f), OtherRectCollider_, this, MovingObject_);
        }
        public override bool IsOverlapped(Int64 Tick_, CCollider2D OtherCollider_)
        {
            return OtherCollider_.IsOverlapped(Tick_, this);
        }
        public override bool IsOverlapped(Int64 Tick_, CRectCollider2D OtherRectCollider_)
        {
            return (Enabled && OtherRectCollider_.Enabled && CPhysics.IsOverlappedRectRect(Rect, OtherRectCollider_.Rect));
        }
    }
}
