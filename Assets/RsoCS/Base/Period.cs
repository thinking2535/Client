using System;

namespace rso
{
    namespace Base
    {
        public class CPeriod
        {
            TimeSpan _Period;
            DateTime _Next;

            public CPeriod(TimeSpan Period_)
            {
                _Period = Period_;
                _Next = (DateTime.Now + Period_);
            }
            public CPeriod(TimeSpan Period_, DateTime Next_)
            {
                _Period = Period_;
                _Next = Next_;
            }
            public void Reset(TimeSpan Period_, DateTime Next_)
            {
                _Period = Period_;
                _Next = Next_;
            }
            public void NextFixed()
            {
                _Next += _Period;
            }
            public void NextLoose()
            {
                _Next = (DateTime.Now + _Period);
            }
            public bool CheckAndNextFixed()
            {
                if (DateTime.Now < _Next)
                    return false;

                NextFixed();

                return true;
            }
            public bool CheckAndNextLoose()
            {
                if (DateTime.Now < _Next)
                    return false;

                NextLoose();

                return true;
            }
        }
    }
}