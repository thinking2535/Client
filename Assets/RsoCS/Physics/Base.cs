using System;

namespace rso.physics
{
    public static class SPointExtension
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
    }

    public static class CBase
    {
        public static SPoint Vector(float Theta_, float Radius_)
        {
            return new SPoint((float)(Math.Cos(Theta_) * Radius_), (float)(Math.Sin(Theta_) * Radius_));
        }
        public static float DistanceSquare(SPoint Point0_, SPoint Point1_)
        {
            return ((Point0_.X - Point1_.X) * (Point0_.X - Point1_.X) + (Point0_.Y - Point1_.Y) * (Point0_.Y - Point1_.Y));
        }
        public static float Distance(SPoint Point0_, SPoint Point1_)
        {
            return (float)Math.Sqrt(DistanceSquare(Point0_, Point1_));
        }
        public static float Atan2(SPoint Vector_)
        {
            return (float)Math.Atan2(Vector_.Y, Vector_.X);
        }
        public static float ThetaOfTwoThetas(float Theta0_, float Theta1_)
        {
            var Theta = Theta0_ - Theta1_;
            if (Theta > math.CBase.c_PI_F)
                Theta -= math.CBase.c_2_PI_F;
            else if (Theta < -math.CBase.c_PI_F)
                Theta += math.CBase.c_2_PI_F;

            return Theta;
        }
        public static float ThetaOfTwoVectors(SPoint Vector0_, SPoint Vector1_)    // +0 : Vector0_ 를 기준으로 Vector1_ 이 우측
        {
            return ThetaOfTwoThetas((float)Math.Atan2(Vector0_.Y, Vector0_.X), (float)Math.Atan2(Vector1_.Y, Vector1_.X));
        }
        public static bool IsCenterThetaBetweenTwoThetas(float Theta_, float LeftTheta_, float RightTheta_)
        {
            var RangeAngle = LeftTheta_ - RightTheta_;
            if (RangeAngle < 0.0)
                RangeAngle += math.CBase.c_2_PI_F;

            var Angle = LeftTheta_ - Theta_;
            if (Angle < 0.0)
                Angle += math.CBase.c_2_PI_F;

            if (Angle > RangeAngle)
                return false;
            else
                return true;
        }
        public static SPoint SymmetryPoint(SPoint Point_, SLine Line_)
        {
            SPoint PointVector = Point_.Sub(Line_.Point0);
            var NewTheta = (Math.Atan2(PointVector.Y, PointVector.X) - 2.0f * ThetaOfTwoVectors(PointVector, Line_.Point1.Sub(Line_.Point0)));
            var Radius = Math.Sqrt(PointVector.X * PointVector.X + PointVector.Y * PointVector.Y);
            return new SPoint((float)(Math.Cos(NewTheta) * Radius), (float)(Math.Sin(NewTheta) * Radius)).Add(Line_.Point0);
        }
        public static float vRadianToCWvDegree(float vRadian_)
        {
            return -(180.0f * vRadian_ / math.CBase.c_PI_F);
        }
        public static float CWvDegreeTovRadian(float vCWDegree_)
        {
            return -((vCWDegree_ * math.CBase.c_PI_F) / 180.0f);
        }
        public static float RadianToCWDegree(float Radian_)   // X좌표의 +방향이 0 Radian, 90도, Radian은 CCW
        {
            //rad   deg
            //0     90
            //p/2   0
            //p     270
            //p*3/2 180
            var CWDegree = (90.0f + vRadianToCWvDegree(Radian_));
            CWDegree %= 360.0f;
            return CWDegree;
        }
        public static float CWDegreeToRadian(float CWDegree_)   // X좌표의 +방향이 0 Radian, 90도, Radian은 CCW
        {
            //rad   deg
            //0     90
            //p/2   0
            //p     270
            //p*3/2 180
            var Radian = (math.CBase.c_PI_F_2 + CWvDegreeTovRadian(CWDegree_));
            Radian %= math.CBase.c_2_PI_F;
            return Radian;
        }
    }
}