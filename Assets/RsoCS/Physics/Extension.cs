using System;

namespace rso.physics
{
    public static class Extension
    {
        public static SPoint Add(this SPoint Lhs_, SPoint Rhs_)
        {
            Lhs_.X += Rhs_.X;
            Lhs_.Y += Rhs_.Y;
            return Lhs_;
        }
        public static SPoint GetAdd(this SPoint Lhs_, SPoint Rhs_)
        {
            return new SPoint(Lhs_).Add(Rhs_);
        }
        public static SPoint Sub(this SPoint Lhs_, SPoint Rhs_)
        {
            Lhs_.X -= Rhs_.X;
            Lhs_.Y -= Rhs_.Y;
            return Lhs_;
        }
        public static SPoint GetSub(this SPoint Lhs_, SPoint Rhs_)
        {
            return new SPoint(Lhs_).Sub(Rhs_);
        }
        public static SPoint Add(this SPoint Lhs_, float Value_)
        {
            Lhs_.X += Value_;
            Lhs_.Y += Value_;
            return Lhs_;
        }
        public static SPoint GetAdd(this SPoint Lhs_, float Value_)
        {
            return new SPoint(Lhs_).Add(Value_);
        }
        public static SPoint Sub(this SPoint Lhs_, float Value_)
        {
            Lhs_.X -= Value_;
            Lhs_.Y -= Value_;
            return Lhs_;
        }
        public static SPoint GetSub(this SPoint Lhs_, float Value_)
        {
            return new SPoint(Lhs_).Sub(Value_);
        }
        public static SPoint Multi(this SPoint Lhs_, float Value_)
        {
            Lhs_.X *= Value_;
            Lhs_.Y *= Value_;
            return Lhs_;
        }
        public static SPoint GetMulti(this SPoint Lhs_, float Value_)
        {
            return new SPoint(Lhs_).Multi(Value_);
        }
        public static SPoint Div(this SPoint Lhs_, float Value_)
        {
            Lhs_.X /= Value_;
            Lhs_.Y /= Value_;
            return Lhs_;
        }
        public static SPoint GetDiv(this SPoint Lhs_, float Value_)
        {
            return new SPoint(Lhs_).Div(Value_);
        }
        public static SPoint Negative(this SPoint Lhs_)
        {
            Lhs_.X = -Lhs_.X;
            Lhs_.Y = -Lhs_.Y;
            return Lhs_;
        }
        public static SPoint GetNegative(this SPoint Lhs_)
        {
            return new SPoint(Lhs_).Negative();
        }

        public static SPoint Center(this SRect Rect_)
        {
            return new SPoint((Rect_.Right + Rect_.Left) * 0.5f, (Rect_.Top + Rect_.Bottom) * 0.5f);
        }
        public static float Width(this SRect Rect_)
        {
            return Rect_.Right - Rect_.Left;
        }
        public static float Height(this SRect Rect_)
        {
            return Rect_.Top - Rect_.Bottom;
        }
        public static SRect Add(this SRect Rect_, SPoint Dir_)
        {
            Rect_.Left += Dir_.X;
            Rect_.Right += Dir_.X;
            Rect_.Bottom += Dir_.Y;
            Rect_.Top += Dir_.Y;
            return Rect_;
        }
        public static SRect GetAdd(this SRect Rect_, SPoint Dir_)
        {
            return new SRect(Rect_).Add(Dir_);
        }
        public static SRect Multi(this SRect Rect_, SPoint Dir_)
        {
            Rect_.Left *= Dir_.X;
            Rect_.Right *= Dir_.X;
            Rect_.Bottom *= Dir_.Y;
            Rect_.Top *= Dir_.Y;
            return Rect_;
        }
        public static SRect GetMulti(this SRect Rect_, SPoint Dir_)
        {
            return new SRect(Rect_).Multi(Dir_);
        }
        public static float GetScalar(this SPoint Vector_)
        {
            return (float)Math.Sqrt(Vector_.X * Vector_.X + Vector_.Y * Vector_.Y);
        }
        public static SPoint GetCopy(this SPoint Point_)
        {
            return new SPoint(Point_);
        }
        public static void Set(this SPoint Point_, SPoint Value_)
        {
            Point_.X = Value_.X;
            Point_.Y = Value_.Y;
        }
        public static void Set(this SPoint Point_, float Value_)
        {
            Point_.X = Value_;
            Point_.Y = Value_;
        }
        public static void Clear(this SPoint Point_)
        {
            Point_.X = Point_.Y = 0.0f;
        }
        public static bool IsZero(this SPoint Point_)
        {
            return (Point_.X == 0.0f && Point_.Y == 0.0f);
        }
        public static SPoint GetLeftTop(this SRect Rect_)
        {
            return new SPoint(Rect_.Left, Rect_.Top);
        }
        public static SPoint GetRightTop(this SRect Rect_)
        {
            return new SPoint(Rect_.Right, Rect_.Top);
        }
        public static SPoint GetLeftBottom(this SRect Rect_)
        {
            return new SPoint(Rect_.Left, Rect_.Bottom);
        }
        public static SPoint GetRightBottom(this SRect Rect_)
        {
            return new SPoint(Rect_.Right, Rect_.Bottom);
        }

        public static SPoint GetCurPos(this SObjectStraight ObjectStraight_, float Time_)
        {
            if (ObjectStraight_.Dist == 0.0f)
                return ObjectStraight_.PosTheta.Pos;

            var CurDist = ObjectStraight_.Speed * (Time_ - ObjectStraight_.Time);
            if (CurDist > ObjectStraight_.Dist)
            {
                CurDist = ObjectStraight_.Dist;
                ObjectStraight_.Dist = 0.0f;
                ObjectStraight_.Time = Time_;
                return ObjectStraight_.PosTheta.Pos.Add(CPhysics.Vector(ObjectStraight_.PosTheta.Theta, CurDist));
            }
            else
            {
                return ObjectStraight_.PosTheta.Pos.GetAdd(CPhysics.Vector(ObjectStraight_.PosTheta.Theta, CurDist));
            }
        }
        public static SPosTheta GetCurPosTheta(this SObjectStraight ObjectStraight_, float Time_)
        {
            return new SPosTheta(GetCurPos(ObjectStraight_, Time_), ObjectStraight_.PosTheta.Theta);
        }
        public static SRect ToRect(this SRectCollider2D Self_)
        {
            return new SRect(-Self_.Size.X * 0.5f, Self_.Size.X * 0.5f, -Self_.Size.Y * 0.5f, Self_.Size.Y * 0.5f).Add(Self_.Offset);
        }
        public static SRectCollider2D Get90Rotated(this SRectCollider2D Self_) // CCW
        {
            return new SRectCollider2D(new SPoint(Self_.Size.Y, Self_.Size.X), new SPoint(-Self_.Offset.Y, Self_.Offset.X));
        }
        public static SRectCollider2D Get180Rotated(this SRectCollider2D Self_)
        {
            return new SRectCollider2D(Self_.Size, new SPoint(-Self_.Offset.X, -Self_.Offset.Y));
        }
        public static SRectCollider2D Get270Rotated(this SRectCollider2D Self_) // CCW
        {
            return new SRectCollider2D(new SPoint(Self_.Size.Y, Self_.Size.X), new SPoint(Self_.Offset.Y, -Self_.Offset.X));
        }
    }
}
