using System;
using System.Collections.Generic;

namespace rso.Base
{
    public class CObserver<_Type>
    {
        _Type _Value = default(_Type);
        Dictionary<object, Action<_Type>> _Observers = new Dictionary<object, Action<_Type>>();
        public CObserver()
        {
        }
        public CObserver(_Type Value_)
        {
            _Value = Value_;
        }
        public void Add(object Observer_, Action<_Type> fObserve_)
        {
            _Observers.Add(Observer_, fObserve_);
            fObserve_(_Value);
        }
        public void Remove(object Observer_)
        {
            _Observers.Remove(Observer_);
        }
        public void SetValue(_Type Value_)
        {
            _Value = Value_;

            foreach (var i in _Observers)
                i.Value(_Value);
        }
    }
}
