namespace rso.physics
{
    public static class CPhysics
    {
        public static bool IsOverlappedPointRect(SPoint Point_, SRect Rect_)
        {
            if (
                Point_.X >= Rect_.Left &&
                Point_.X <= Rect_.Right &&
                Point_.Y >= Rect_.Bottom &&
                Point_.Y <= Rect_.Top)
                return true;

            return false;
        }
        public static bool IsOverlappedRectRect(SRect Rect0_, SRect Rect1_)
        {
            return (
                Rect0_.Left <= Rect1_.Right &&
                Rect0_.Right >= Rect1_.Left &&
                Rect0_.Bottom <= Rect1_.Top &&
                Rect0_.Top >= Rect1_.Bottom);
        }
        public static bool IsOverlappedPointCircle(SPoint Point_, SCircle Circle_)
        {
            if ((Point_.X - Circle_.X) * (Point_.X - Circle_.X) + (Point_.Y - Circle_.Y) * (Point_.Y - Circle_.Y) <= Circle_.Radius * Circle_.Radius)
                return true;

            return false;
        }
        public static bool IsOverlappedCircleRect(SCircle Circle_, SRect Rect_)
        {
            if (
                // 세로로 긴 직사각형에 포함
                IsOverlappedPointRect(Circle_, new SRect(Rect_.Left, Rect_.Right, Rect_.Bottom - Circle_.Radius, Rect_.Top + Circle_.Radius)) ||
                // 가로로 긴 직사각형에 포함
                IsOverlappedPointRect(Circle_, new SRect(Rect_.Left - Circle_.Radius, Rect_.Right + Circle_.Radius, Rect_.Bottom, Rect_.Top)) ||
                // 네 모서리 1/4 원에 포함
                IsOverlappedPointCircle(new SPoint(Rect_.Left, Rect_.Bottom), Circle_) ||
                IsOverlappedPointCircle(new SPoint(Rect_.Right, Rect_.Bottom), Circle_) ||
                IsOverlappedPointCircle(new SPoint(Rect_.Left, Rect_.Top), Circle_) ||
                IsOverlappedPointCircle(new SPoint(Rect_.Right, Rect_.Top), Circle_)
                )
                return true;

            return false;
        }
        public static bool IsOverlappedCircleCircle(SCircle Circle0_, SCircle Circle1_)
        {
            if ((Circle0_.X - Circle1_.X) * (Circle0_.X - Circle1_.X) + (Circle0_.Y - Circle1_.Y) * (Circle0_.Y - Circle1_.Y) <=
                (Circle0_.Radius + Circle1_.Radius) * (Circle0_.Radius + Circle1_.Radius))
                return true;

            return false;
        }
        public static bool IsOverlappedPointPointRect(SPoint Point_, SPointRect PointRect_)
        {
            return (
                CBase.ThetaOfTwoVectors(PointRect_.TopLeft.Sub(PointRect_.TopRight), Point_.Sub(PointRect_.TopRight)) < 0.0 &&
                CBase.ThetaOfTwoVectors(PointRect_.BottomLeft.Sub(PointRect_.TopLeft), Point_.Sub(PointRect_.TopLeft)) < 0.0 &&
                CBase.ThetaOfTwoVectors(PointRect_.BottomRight.Sub(PointRect_.BottomLeft), Point_.Sub(PointRect_.BottomLeft)) < 0.0 &&
                CBase.ThetaOfTwoVectors(PointRect_.TopRight.Sub(PointRect_.BottomRight), Point_.Sub(PointRect_.BottomRight)) < 0.0);
        }
        public static bool IsOverlappedCirclePointRect(SCircle Circle_, SPointRect PointRect_)
        {
            return (
                IsOverlappedPointPointRect(Circle_, new SPointRect(new SPoint(PointRect_.TopRight.X, PointRect_.TopRight.Y + Circle_.Radius), new SPoint(PointRect_.TopLeft.X, PointRect_.TopLeft.Y + Circle_.Radius), new SPoint(PointRect_.BottomLeft.X, PointRect_.BottomLeft.Y - Circle_.Radius), new SPoint(PointRect_.BottomRight.X, PointRect_.BottomRight.Y - Circle_.Radius))) ||
                IsOverlappedPointPointRect(Circle_, new SPointRect(new SPoint(PointRect_.TopRight.X + Circle_.Radius, PointRect_.TopRight.Y), new SPoint(PointRect_.TopLeft.X - Circle_.Radius, PointRect_.TopLeft.Y), new SPoint(PointRect_.BottomLeft.X - Circle_.Radius, PointRect_.BottomLeft.Y), new SPoint(PointRect_.BottomRight.X + Circle_.Radius, PointRect_.BottomRight.Y))) ||
                IsOverlappedPointCircle(Circle_, new SCircle(PointRect_.TopRight, Circle_.Radius)) ||
                IsOverlappedPointCircle(Circle_, new SCircle(PointRect_.TopLeft, Circle_.Radius)) ||
                IsOverlappedPointCircle(Circle_, new SCircle(PointRect_.BottomLeft, Circle_.Radius)) ||
                IsOverlappedPointCircle(Circle_, new SCircle(PointRect_.BottomRight, Circle_.Radius))
                );
        }
        public static bool IsCollidedPointVLine(SPoint Point_, SVLine VLine_, SPoint Dir_, SCollisionInfo CollisionInfo_)
        {
            if (Dir_.X == 0.0)
                return false;

            if (Dir_.X < 0.0 &&

                Point_.X < VLine_.X)
                return false;

            if (Dir_.X > 0.0 &&
                Point_.X > VLine_.X)
                return false;

            var ContactY = (Dir_.Y / Dir_.X) * (VLine_.X - Point_.X) + Point_.Y;
            CollisionInfo_.Time = (VLine_.X - Point_.X) / Dir_.X;

            if (CollisionInfo_.Time < 0.0)
                return false;

            CollisionInfo_.Point.X = VLine_.X;
            CollisionInfo_.Point.Y = ContactY;
            CollisionInfo_.Normal.X = -Dir_.X;
            CollisionInfo_.Normal.Y = 0.0f;
            return true;
        }
        public static bool IsCollidedPointHLine(SPoint Point_, SHLine HLine_, SPoint Dir_, SCollisionInfo CollisionInfo_)
        {
            if (Dir_.Y == 0.0)
                return false;

            if (Dir_.Y < 0.0 &&

                Point_.Y < HLine_.Y)
                return false;

            if (Dir_.Y > 0.0 &&
                Point_.Y > HLine_.Y)
                return false;

            var ContactX = (Dir_.X / Dir_.Y) * (HLine_.Y - Point_.Y) + Point_.X;
            CollisionInfo_.Time = (HLine_.Y - Point_.Y) / Dir_.Y;

            if (CollisionInfo_.Time < 0.0)
                return false;

            CollisionInfo_.Point.X = ContactX;
            CollisionInfo_.Point.Y = HLine_.Y;
            CollisionInfo_.Normal.X = 0.0f;
            CollisionInfo_.Normal.Y = -Dir_.Y;
            return true;
        }
        public static bool IsCollidedVSegmentVSegment(SVSegment VSegment0_, SVSegment VSegment1_, SPoint Dir_, SCollisionInfo CollisionInfo_)
        {
            if (!IsCollidedPointVLine(new SPoint(VSegment0_.X, VSegment0_.Bottom), VSegment1_, Dir_, CollisionInfo_))
                return false;

            // 라인의 중첩영역을 임시로 CollisionInfo의 Point에 저장
            CollisionInfo_.Point.X = CollisionInfo_.Point.Y + (VSegment0_.Top - VSegment0_.Bottom);

            if (CollisionInfo_.Point.Y < VSegment1_.Bottom) // Point.Y : ContactBottom
                CollisionInfo_.Point.Y = VSegment1_.Bottom;
            if (CollisionInfo_.Point.X > VSegment1_.Top)    // Point.X : ContactTop
                CollisionInfo_.Point.X = VSegment1_.Top;

            if (CollisionInfo_.Point.X < CollisionInfo_.Point.Y)    // 중첩영역이 없으면
                return false;

            CollisionInfo_.Point.Y = ((CollisionInfo_.Point.X + CollisionInfo_.Point.Y) / 2.0f);
            CollisionInfo_.Point.X = VSegment1_.X;
            CollisionInfo_.Normal.X = -Dir_.X;
            CollisionInfo_.Normal.Y = 0.0f;
            return true;
        }
        public static bool IsCollidedHSegmentHSegment(SHSegment HSegment0_, SHSegment HSegment1_, SPoint Dir_, SCollisionInfo CollisionInfo_)
        {
            if (!IsCollidedPointHLine(new SPoint(HSegment0_.Left, HSegment0_.Y), HSegment1_, Dir_, CollisionInfo_))
                return false;

            // 라인의 중첩영역을 임시로 CollisionInfo의 Point에 저장
            CollisionInfo_.Point.Y = CollisionInfo_.Point.X + (HSegment0_.Right - HSegment0_.Left);

            if (CollisionInfo_.Point.X < HSegment1_.Left)   // Point.Y : ContactLeft
                CollisionInfo_.Point.X = HSegment1_.Left;
            if (CollisionInfo_.Point.Y > HSegment1_.Right)  // Point.X : ContactRight
                CollisionInfo_.Point.Y = HSegment1_.Right;

            if (CollisionInfo_.Point.Y < CollisionInfo_.Point.X)    // 중첩영역이 없으면
                return false;

            CollisionInfo_.Point.X = ((CollisionInfo_.Point.Y + CollisionInfo_.Point.X) / 2.0f);
            CollisionInfo_.Point.Y = HSegment1_.Y;
            CollisionInfo_.Normal.X = 0.0f;
            CollisionInfo_.Normal.Y = -Dir_.Y;
            return true;
        }
        public static bool IsCollidedRectRect(SRect Rect0_, SRect Rect1_, SPoint Dir_, SCollisionInfo CollisionInfo_)
        {
            if (Dir_.X > 0.0)
            {
                if (IsCollidedVSegmentVSegment(
                    new SVSegment(new SVLine(Rect0_.Right), Rect0_.Bottom, Rect0_.Top),
                    new SVSegment(new SVLine(Rect1_.Left), Rect1_.Bottom, Rect1_.Top),
                    Dir_, CollisionInfo_))
                    return true;
            }
            else if (Dir_.X < 0.0)
            {
                if (IsCollidedVSegmentVSegment(
                    new SVSegment(new SVLine(Rect0_.Left), Rect0_.Bottom, Rect0_.Top),
                    new SVSegment(new SVLine(Rect1_.Right), Rect1_.Bottom, Rect1_.Top),
                    Dir_, CollisionInfo_))
                    return true;
            }

            if (Dir_.Y > 0.0)
            {
                if (IsCollidedHSegmentHSegment(
                    new SHSegment(new SHLine(Rect0_.Top), Rect0_.Left, Rect0_.Right),
                    new SHSegment(new SHLine(Rect1_.Bottom), Rect1_.Left, Rect1_.Right),
                    Dir_, CollisionInfo_))
                    return true;
            }
            else if (Dir_.Y < 0.0)
            {
                if (IsCollidedHSegmentHSegment(
                    new SHSegment(new SHLine(Rect0_.Bottom), Rect0_.Left, Rect0_.Right),
                    new SHSegment(new SHLine(Rect1_.Top), Rect1_.Left, Rect1_.Right),
                    Dir_, CollisionInfo_))
                    return true;
            }

            return false;
        }
        public static bool IsCollidedRectRect2(SRect Rect0_, SRect Rect1_, SPoint Dir0_, SPoint Dir1_, SCollisionInfo CollisionInfo_)
        {
            if (!IsCollidedRectRect(Rect0_, Rect1_, Dir0_.GetSub(Dir1_), CollisionInfo_))
                return false;

            CollisionInfo_.Point.Add(Dir1_.GetMulti(CollisionInfo_.Time));
            return true;
        }
        public static bool MoveTowards(SPoint Current_, SPoint Target_, float DistanceDelta_) // return true : reached
        {
            var Vector = Target_.GetSub(Current_);
            var Scalar = Vector.GetScalar();
            if (Scalar > DistanceDelta_)
            {
                Current_.Add(new SPoint(Vector.X * DistanceDelta_ / Scalar, Vector.Y * DistanceDelta_ / Scalar));
                return false;
            }
            else
            {
                Current_.Set(Target_);
                return true;
            }
        }
    }
}