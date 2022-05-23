using System;
using System.Collections.Generic;
using UnityEngine;

namespace rso.unity
{
    public class CInputKey
    {
        public delegate void TCallback(KeyCode KeyCode_, bool Down_);

        TCallback _Callback;
        List<KeyCode> _States = new List<KeyCode>();
        bool[] _Downs = new bool[(Int32)KeyCode.Joystick8Button19];
        public CInputKey(TCallback Callback_)
        {
            _Callback = Callback_;
        }
        public void Add(KeyCode KeyCode_)
        {
            if (_States.Contains(KeyCode_))
                throw new Exception("Already has KeyCode");

            _States.Add(KeyCode_);
        }
        public bool Get(KeyCode KeyCode_)
        {
            return _Downs[(Int32)KeyCode_];
        }
        public void Update()
        {
            foreach (var i in _States)
            {
                if (Input.GetKey(i) != _Downs[(Int32)i])
                {
                    _Downs[(Int32)i] = !_Downs[(Int32)i];
                    _Callback(i, _Downs[(Int32)i]);
                }
            }
        }
    }
}