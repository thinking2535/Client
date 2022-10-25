using System;
using System.Collections.Generic;
using UnityEngine;

namespace rso.unity
{
    public class CInputKey
    {
        public delegate void FCallback(KeyCode keyCode, bool isPressed);

        FCallback _fCallback;
        List<KeyCode> _keyCodes = new List<KeyCode>();
        HashSet<KeyCode> _pressedKeys = new HashSet<KeyCode>();
        public CInputKey(FCallback callback)
        {
            _fCallback = callback;
        }
        public void add(KeyCode keyCode)
        {
            if (_keyCodes.Contains(keyCode))
                throw new Exception("Already has KeyCode");

            _keyCodes.Add(keyCode);
        }
        public bool getKeyPressed(KeyCode keyCode)
        {
            return _pressedKeys.Contains(keyCode);
        }
        public void update()
        {
            foreach (var i in _keyCodes)
            {
                var isPressed = Input.GetKey(i);
                if (isPressed != getKeyPressed(i))
                {
                    if (isPressed)
                        _pressedKeys.Add(i);
                    else
                        _pressedKeys.Remove(i);

                    _fCallback(i, isPressed);
                }
            }
        }
    }
}