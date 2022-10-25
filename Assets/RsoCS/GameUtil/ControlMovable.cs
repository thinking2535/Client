using System;
using rso.physics;

namespace rso
{
    namespace gameutil
    {
        public class CControlMovable
        {
            float _EffectiveRadius = 0.0f;
            SPoint _Center = new SPoint();
            bool _Enabled = false;
            SPoint _Vector = new SPoint();

            public CControlMovable(float EffectiveRadius_)
            {
                _EffectiveRadius = EffectiveRadius_;
            }
            public void Down(SPoint Point_)
            {
                _Center.X = Point_.X;
                _Center.Y = Point_.Y;
                _Enabled = true;
            }
            public SPoint Move(SPoint Point_)
            {
                if (!_Enabled)
                    return null;

                _Vector.X = Point_.X - _Center.X;
                _Vector.Y = Point_.Y - _Center.Y;

                var Distance = CPhysics.Distance(_Center, Point_);
                if (Distance > _EffectiveRadius)
                {
                    var Ratio = _EffectiveRadius / Distance;
                    _Vector.X *= Ratio;
                    _Vector.Y *= Ratio;

                    _Center.X = Point_.X - _Vector.X;
                    _Center.Y = Point_.Y - _Vector.Y;
                }

                return _Vector;
            }
            public void Up()
            {
                _Enabled = false;
            }
        }
    }
}
