using System;
using System.Security.Permissions;
using UnityEngine;

namespace rso.unity
{
    public class CFPS
    {
        public Int32 FPS { get; private set; } = 0;
        Int32 _Counter = 0;
        float _LastTime = 0.0f;

        public void Set()
        {
            ++_Counter;

            if (Time.time - _LastTime >= 1.0f)
            {
                _LastTime += 1.0f;
                FPS = _Counter;
                _Counter = 0;
            }
        }
    }
}