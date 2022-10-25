using UnityEngine;

namespace rso.unity
{
    public class CBlink
    {
        public delegate void FCallback(bool Show_);
        FCallback _Callback;
        bool _On = false;
        bool _Show = true;
        float _NextCallbackTime = 0.0f;
        float _EndTime = 0.0f;
        float _OnDuration = 0.0f;
        float _OffDuration = 0.0f;
        public CBlink(FCallback Callback_)
        {
            _Callback = Callback_;
        }
        public void On(float Duration_, float OnDuration_, float OffDuration_)
        {
            if (!_On)
                _On = true;

            if (!_Show) // 최초는 Show 인 상태로 하여 코드 단순하게
                _Callback(_Show = true);

            _EndTime = Time.time + Duration_;
            _OnDuration = OnDuration_;
            _OffDuration = OffDuration_;
            _NextCallbackTime = Time.time + _OnDuration;
        }
        public void Off()
        {
            if (!_On)
                return;

            if (!_Show) // Show == true is default
                _Callback(_Show = true);

            _On = false;
        }
        public void Update()
        {
            if (!_On)
                return;

            if (_Show)
            {
                if (Time.time < _EndTime)
                {
                    if (Time.time >= _NextCallbackTime)
                    {
                        _Callback(_Show = false);
                        _NextCallbackTime += _OffDuration;
                    }
                }
                else
                {
                    _On = false;
                }
            }
            else
            {
                if (Time.time >= _NextCallbackTime)
                {
                    _Callback(_Show = true);
                    _NextCallbackTime += _OnDuration;
                }
            }
        }
    }
}
