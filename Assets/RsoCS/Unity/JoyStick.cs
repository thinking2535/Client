using System;
using UnityEngine;

namespace rso.unity
{
    public class CJoyStick
    {
        class _CObject
        {
            public Int32 ID = 0;

            public _CObject(Int32 ID_)
            {
                ID = ID_;
            }
        }
        class _CStick : _CObject
        {
            public Vector2 Position;
            public Int32 Dir = 0;

            public _CStick(Int32 ID_, Vector2 Position_) :
                base(ID_)
            {
                Position = Position_;
            }
        }

        public delegate void TStickCallback(Int32 Dir_);
        public delegate void TButtonCallback();

        float _InnerRange;
        float _OuterRange;
        TStickCallback _StickCallback;
        TButtonCallback _ButtonCallback;

        _CStick _Stick = null;
        _CObject _Button = null;

        public Vector2? StickPosition
        {
            get
            {
                return _Stick?.Position;
            }
        }
        public CJoyStick(float InnerRange_, float OuterRange_, TStickCallback StickCallback_, TButtonCallback ButtonCallback_)
        {
            if (InnerRange_ > OuterRange_)
                throw new Exception("InnerRange_ > OuterRange_");

            _InnerRange = InnerRange_;
            _OuterRange = OuterRange_;
            _StickCallback = StickCallback_;
            _ButtonCallback = ButtonCallback_;
        }
        public void Update()
        {
#if UNITY_EDITOR

        // Stick ////////////////////////////////////////////////////////////////
        if (_Stick == null)
        {
            if (Input.GetMouseButton(0) && Input.mousePosition.x <= Screen.width * 0.5f)
                _Stick = new _CStick(0, Input.mousePosition);
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                // Fix Stick Position
                _Stick.Position.y = Input.mousePosition.y;

                if (Input.mousePosition.x > _Stick.Position.x + _OuterRange)
                    _Stick.Position.x = Input.mousePosition.x - _OuterRange;
                else if (Input.mousePosition.x < _Stick.Position.x - _OuterRange)
                    _Stick.Position.x = Input.mousePosition.x + _OuterRange;

                // Call StickCallback
                Int32 Dir = 0;
                if (Input.mousePosition.x > _Stick.Position.x + _InnerRange)
                    Dir = 1;
                else if (Input.mousePosition.x < _Stick.Position.x - _InnerRange)
                    Dir = -1;

                if (Dir != _Stick.Dir)
                {
                    _Stick.Dir = Dir;
                    _StickCallback(Dir);
                }
            }
            else
            {
                _Stick = null;
                _StickCallback(0);
            }
        }

        // Button /////////////////////////////////////////////////////////////
        if (_Button == null)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _Button = new _CObject(1);
                _ButtonCallback();
            }
        }
        else
        {
            if (!Input.GetKey(KeyCode.Space))
                _Button = null;
        }
#else

            foreach (var i in Input.touches)
            {
                bool LeftTouch = i.position.x <= Screen.width * 0.5f; // Stick Area

                // Stick ////////////////////////////////////////////////////////////////
                if (_Stick == null)
                {
                    if (LeftTouch)
                        _Stick = new _CStick(i.fingerId, i.position);
                }
                else
                {
                    if (i.fingerId == _Stick.ID)
                    {
                        switch (i.phase)
                        {
                            case TouchPhase.Began:
                            case TouchPhase.Moved:
                            case TouchPhase.Stationary:
                                {
                                    // Fix Stick Position
                                    _Stick.Position.y = i.position.y;

                                    if (i.position.x > _Stick.Position.x + _OuterRange)
                                        _Stick.Position.x = i.position.x - _OuterRange;
                                    else if (i.position.x < _Stick.Position.x - _OuterRange)
                                        _Stick.Position.x = i.position.x + _OuterRange;

                                    // Call StickCallback
                                    Int32 Dir = 0;
                                    if (i.position.x > _Stick.Position.x + _InnerRange)
                                        Dir = 1;
                                    else if (i.position.x < _Stick.Position.x - _InnerRange)
                                        Dir = -1;

                                    if (Dir != _Stick.Dir)
                                    {
                                        _Stick.Dir = Dir;
                                        _StickCallback(Dir);
                                    }
                                }
                                break;

                            case TouchPhase.Ended:
                            case TouchPhase.Canceled:
                                {
                                    _Stick = null;
                                    _StickCallback(0);
                                }
                                break;
                        }

                        continue; // 이미 작용중인 터치번호라면 다음 Object(Stick, Button 등) 체크하지 않음.
                    }
                }

                // Button /////////////////////////////////////////////////////////////
                if (_Button == null)
                {
                    if (!LeftTouch)
                    {
                        _Button = new _CObject(i.fingerId);
                        _ButtonCallback();
                    }
                }
                else
                {
                    if (i.fingerId == _Button.ID)
                    {
                        switch (i.phase)
                        {
                            case TouchPhase.Ended:
                            case TouchPhase.Canceled:
                                _Button = null;
                                break;
                        }

                        continue; // 이미 작용중인 터치번호라면 다음 Object(Stick, Button 등) 체크하지 않음.
                    }
                }
            }
#endif
        }
    }
}
