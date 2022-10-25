namespace rso.physics
{
    public class CObjectStraight : SObjectStraight
    {
        public CObjectStraight(SObjectStraight ObjectStraight_) :
            base(ObjectStraight_)
        {
        }
        public void SetPosTheta(float Time_, SPosTheta PosTheta_)
        {
            PosTheta = PosTheta_;
            Time = Time_;
        }
        public void SetSpeed(float Time_, float Speed_)
        {
            if (Dist > 0.0f)
            {
                var CurDist = Speed * (Time_ - Time);
                if (CurDist > Dist)
                {
                    CurDist = Dist;
                    Dist = 0.0f;
                }

                Time = Time_;
                PosTheta.Pos.Add(CPhysics.Vector(PosTheta.Theta, CurDist));
            }
            Speed = Speed_;
        }
    }
}