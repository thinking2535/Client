using rso.unity;
using UnityEngine;

namespace rso.unity
{
    public class CPadSimulator
    {
        readonly InputTouch.FJoypad _fTouched;
        readonly InputTouch.FButton _fPushed;
        readonly InputTouch.Joypad _joypad;
        public Vector2 centerPosition { get => _joypad.centerPosition; }
        public Vector2 touchPosition { get => _joypad.touchPosition; }
#if UNITY_EDITOR
        readonly CInputKey _inputKey;
        void _callback(KeyCode keyCode, bool isPressed)
        {
            switch (keyCode)
            {
                case KeyCode.A:
                    {
                        if (isPressed)
                        {
                            if (_inputKey.getKeyPressed(KeyCode.D))
                            {
                                _fTouched(InputTouch.TouchState.up, -1);
                            }
                            else
                            {
                                _fTouched(InputTouch.TouchState.down, -1);
                                _fTouched(InputTouch.TouchState.move, 0);
                            }
                        }
                        else
                        {
                            if (_inputKey.getKeyPressed(KeyCode.D))
                            {
                                _fTouched(InputTouch.TouchState.down, -1);
                                _fTouched(InputTouch.TouchState.move, 1);
                            }
                            else
                            {
                                _fTouched(InputTouch.TouchState.up, -1);
                            }
                        }
                    }
                    break;
                case KeyCode.D:
                    {
                        if (isPressed)
                        {
                            if (_inputKey.getKeyPressed(KeyCode.A))
                            {
                                _fTouched(InputTouch.TouchState.up, -1);
                            }
                            else
                            {
                                _fTouched(InputTouch.TouchState.down, -1);
                                _fTouched(InputTouch.TouchState.move, 1);
                            }
                        }
                        else
                        {
                            if (_inputKey.getKeyPressed(KeyCode.A))
                            {
                                _fTouched(InputTouch.TouchState.down, -1);
                                _fTouched(InputTouch.TouchState.move, 0);
                            }
                            else
                            {
                                _fTouched(InputTouch.TouchState.up, -1);
                            }
                        }
                    }
                    break;
                case KeyCode.Space:
                case KeyCode.Return:
                    {
                        if (isPressed)
                            _fPushed(InputTouch.TouchState.down);
                    }
                    break;
            }
        }
#endif
        readonly InputTouch _InputTouch = new InputTouch();
        public CPadSimulator(InputTouch.FJoypad fTouched, InputTouch.FButton fPushed, float activeRange, Vector2 defaultPosition)
        {
            _fTouched = fTouched;
            _fPushed = fPushed;
            _joypad = new InputTouch.Joypad((Vector2 position) => { return position.x < Screen.width * 0.5f; }, fTouched, activeRange, 1, defaultPosition);
            _InputTouch.add(_joypad);
            _InputTouch.add(new InputTouch.Button((Vector2 position) => { return position.x >= Screen.width * 0.5f; }, fPushed));

#if UNITY_EDITOR
            _inputKey = new CInputKey(_callback);
            _inputKey.add(KeyCode.A);
            _inputKey.add(KeyCode.D);
            _inputKey.add(KeyCode.Space);
            _inputKey.add(KeyCode.Return);
#endif
        }
        public void update()
        {
#if UNITY_EDITOR
            _inputKey.update();
#endif
            _InputTouch.update();
        }
    }
}
