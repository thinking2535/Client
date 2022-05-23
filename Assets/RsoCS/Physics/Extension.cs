using System;

namespace rso.physics
{
    public static class Extension
    {
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
                return ObjectStraight_.PosTheta.Pos.Add(CBase.Vector(ObjectStraight_.PosTheta.Theta, CurDist));
            }
            else
            {
                return ObjectStraight_.PosTheta.Pos.GetAdd(CBase.Vector(ObjectStraight_.PosTheta.Theta, CurDist));
            }
        }
        public static SPosTheta GetCurPosTheta(this SObjectStraight ObjectStraight_, float Time_)
        {
            return new SPosTheta(GetCurPos(ObjectStraight_, Time_), ObjectStraight_.PosTheta.Theta);
        }
    }
}
