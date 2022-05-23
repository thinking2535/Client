using System;
using rso.physics;

namespace rso
{
    namespace gameutil
    {
        public class CControlImmovable
        {
            SPoint _Center = new SPoint();
            float _EffectiveRadius = 0.0f;
            bool _Enabled = false;
            SPoint _Vector = new SPoint();

            public CControlImmovable(float EffectiveRadius_)
            {
                _EffectiveRadius = EffectiveRadius_;
            }
            public void Down(SPoint Point_) // return : Vector
            {
                _Center.X = Point_.X;
                _Center.Y = Point_.Y;

                _Enabled = true;
            }
            public SPoint Move(SPoint Point_) // return : Vector
            {
                if (!_Enabled)
                    return null;

                _Vector.X = Point_.X - _Center.X;
                _Vector.Y = Point_.Y - _Center.Y;

                var Distance = CBase.Distance(_Center, Point_);
                if (Distance > _EffectiveRadius)
                {
                    var Ratio = _EffectiveRadius / Distance;
                    _Vector.X *= Ratio;
                    _Vector.Y *= Ratio;
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
