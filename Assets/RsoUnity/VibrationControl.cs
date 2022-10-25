using System;

namespace rso.unity
{
    public class CVibrationControl
    {
        public struct SVibration
        {
            Int64[] Pattern;
            Int32 Repeat;

            public SVibration(Int64[] Pattern_, Int32 Repeat_)
            {
                Pattern = Pattern_;
                Repeat = Repeat_;
            }
            public void On()
            {
                CVibration.On(Pattern, Repeat);
            }
        }
        SVibration[] _Vibrations;
        public CVibrationControl(SVibration[] Vibrations_)
        {
            _Vibrations = Vibrations_;
        }
        public void On(Int32 VibrationIndex_)
        {
            _Vibrations[VibrationIndex_].On();
        }
        public void Off()
        {
            CVibration.Off();
        }
    }
}
