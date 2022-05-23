using UnityEngine;

namespace rso.unity
{
    public class CLinear
    {
        float _EndTime = 0.0f;
        float _A = 0.0f;
        float _B = 0.0f;
        public void SetDuration(float Duration_, float StartValue_, float EndValue_)
        {
            _A = (EndValue_ - StartValue_) / Duration_;
            _B = -(_A * Time.time) + StartValue_;
            _EndTime = Time.time + Duration_;
        }
        public void SetVelocity(float Velocity_, float StartValue_, float EndValue_)
        {
            _A = Velocity_;
            _B = -(_A * Time.time) + StartValue_;
            _EndTime = Time.time + (EndValue_ - StartValue_) / Velocity_;
        }
        public bool Get(ref float Value_)
        {
            if (Time.time >= _EndTime)
            {
                Value_ = _A * _EndTime + _B;
                return false;
            }

            Value_ = _A * Time.time + _B;
            return true;
        }
    }
}
