using System;

namespace rso
{
    namespace Base
    {
        public delegate void TCallback();
        public interface IFSM
        {
            void Set(TCallback Callback_);
            void Clear();
            void Proc();
        }
        public class CFSM : IFSM
        {
            delegate void TSetNextOutTickFunc();

            readonly Random _Random;
            readonly Int32 _Delay;
            TCallback _Callback = null;
            Int32 _NextOutTick = 0;
            TSetNextOutTickFunc _SetNextOutTickFunc;

            public CFSM(Int32 Delay_)
            {
                _Random = new Random(Environment.TickCount);
                _Delay = Delay_;

                if (_Delay > 0)
                    _SetNextOutTickFunc = () => { _NextOutTick = Environment.TickCount + _Random.Next() % _Delay; };
                else
                    _SetNextOutTickFunc = () => { _NextOutTick = Environment.TickCount; };
            }
            public void Set(TCallback Callback_)
            {
                _Callback = Callback_;
                _SetNextOutTickFunc();
            }
            public void Clear()
            {
                _Callback = null;
            }
            public void Proc()
            {
                if (_Callback == null)
                    return;

                if (Environment.TickCount < _NextOutTick)
                    return;

                var Callback = _Callback;
                _Callback = null;
                Callback();
            }
        }
    }
}