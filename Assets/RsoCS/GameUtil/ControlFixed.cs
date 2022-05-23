using rso.physics;

namespace rso
{
    namespace gameutil
    {
        public class CControlFixed
        {
            SPoint _Center = new SPoint();
            float _EffectiveRadius = 0.0f;
            float _InputRadius = 0.0f;
            bool _Enabled = false;
            SPoint _Vector = new SPoint();

            public CControlFixed(SPoint Center_, float EffectiveRadius_, float InputRadius_)
            {
                _Center.X = Center_.X;
                _Center.Y = Center_.Y;
                _EffectiveRadius = EffectiveRadius_;
                _InputRadius = InputRadius_;
            }
            public SPoint Down(SPoint Point_) // return : Vector
            {
                if (CBase.Distance(_Center, Point_) > _InputRadius)
                    return null;

                _Enabled = true;

                return Move(Point_);
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
