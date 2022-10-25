using rso.math;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace rso.unity
{
    public class InputTouch
    {
        public enum TouchState
        {
            down,
            move,
            up,
            cancel
        }

#if UNITY_EDITOR
        struct TouchForMouse
        {
            public Int32 fingerId;
            public Vector2 position;
            public TouchPhase phase;
        }
        const Int32 _fingerCount = 3; // Mouse Button Count
#else
        const Int32 _fingerCount = 10;
#endif
        public delegate bool FIsActivated(Vector2 position);
        public delegate void FAim(TouchState state, Vector2 position);
        public delegate void FStick(TouchState state, Vector2 position, Vector2 direction);
        public delegate bool FJoypad(TouchState state, Int32 direction);
        public delegate bool FButton(TouchState state);
        public delegate void FScroll(TouchState state, Vector2 downPosition, Vector2 diff, Vector2 delta);
        public abstract class InputObject
        {
            public FIsActivated fIsActivated { get; }
            public bool isOn { get; protected set; } = false; // 객체가 작동중인가? (동시에 1개만 작동될 수 있음)
            public abstract void down(Vector2 position);
            public abstract void move(Vector2 position);
            public abstract void up(Vector2 position);
            public abstract void up(); // Update() 호출 지연으로 Touch phase 의 Ended 가 누락된 경우 호출
            public InputObject(FIsActivated fIsActivated)
            {
                this.fIsActivated = fIsActivated;
            }
        }
        public class Aim : InputObject
        {
            readonly FAim _fCallback;
            protected Vector2 _lastPosition;
            public Aim(FIsActivated fIsActivated, FAim fCallback) :
                base(fIsActivated)
            {
                _fCallback = fCallback;
            }
            public override void down(Vector2 position)
            {
                isOn = true;
                _lastPosition = position;
                _fCallback(TouchState.down, position);
            }
            public override void move(Vector2 position)
            {
                _lastPosition = position;
                _fCallback(TouchState.move, position);
            }
            public override void up(Vector2 position)
            {
                up();
            }
            public override void up()
            {
                isOn = false;
                _fCallback(TouchState.up, _lastPosition);
            }
        }
        public class Stick : InputObject
        {
            readonly FStick _fCallback;
            readonly float _standbyRange;
            readonly float _activeRange;
            readonly bool _isTrackable;
            Vector2 _position;
            bool _activated = false;
            public Stick(FIsActivated fIsActivated, FStick fCallback, float standbyRange, float activeRange, bool isTrackable) :
                base(fIsActivated)
            {
                if (standbyRange < 0.0f)
                    throw new Exception("Invalid StandbyRange");

                if (activeRange < 0.0f)
                    throw new Exception("Invalid ActiveRange");

                _fCallback = fCallback;
                _standbyRange = standbyRange;
                _activeRange = activeRange;
                _isTrackable = isTrackable;
            }
            public override void down(Vector2 position)
            {
                isOn = true;
                _position = position;
                _fCallback(TouchState.down, _position, Vector2.zero);
            }
            public override void move(Vector2 position)
            {
                var Vec = position - _position;
                var Magnitude = Vec.magnitude;
                if (!_activated)
                {
                    if (Magnitude < _standbyRange)
                        return;

                    _activated = true;
                }

                if (Magnitude > _activeRange)
                {
                    Vec.x = Vec.x / Magnitude * _activeRange;
                    Vec.y = Vec.y / Magnitude * _activeRange;

                    if (_isTrackable)
                        _position = position - Vec;
                }

                _fCallback(TouchState.move, _position, Vec);
            }
            public override void up(Vector2 position)
            {
                up();
            }
            public override void up()
            {
                isOn = false;
                _fCallback(TouchState.up, _position, Vector2.zero);
                _activated = false;
            }
        }
        public class Joypad : InputObject
        {
            readonly FJoypad _fCallback;
            readonly float _activeRange;
            readonly Int32 _directionCount = 1;
            readonly float _unitTheta;
            readonly float _unitTheta_2;
            readonly Vector2 _defaultPosition;
            public Vector2 centerPosition { get; private set; }
            public Vector2 touchPosition { get; private set; }
            public Vector2 relativeTouchPosition { get => touchPosition - centerPosition; }
            Int32 _lastDirection = -1; // 9시 방향부터 0 ~ 반시계방향으로
            public Joypad(FIsActivated fIsActivated, FJoypad fCallback, float activeRange, Int32 expDirectionCount, Vector2 defaultPosition) :
                base(fIsActivated)
            {
                if (activeRange < 0.0f)
                    throw new Exception("Invalid ActiveRange");

                if (expDirectionCount < 0)
                    throw new Exception("Invalid ExpDirCount Count");

                _fCallback = fCallback;
                this._activeRange = activeRange;

                for (Int32 i = 0; i < expDirectionCount; ++i)
                    _directionCount *= 2;

                _unitTheta = Mathf.PI * 2.0f / _directionCount;
                _unitTheta_2 = _unitTheta * 0.5f;
                touchPosition = centerPosition = _defaultPosition = defaultPosition;
            }
            public override void down(Vector2 position)
            {
                isOn = true;
                touchPosition = centerPosition = position;
                _fCallback(TouchState.down, -1);
            }
            public override void move(Vector2 position)
            {
                touchPosition = position;
                var vector = touchPosition - centerPosition;
                var magnitude = vector.magnitude;
                if (_lastDirection == -1)
                {
                    if (magnitude < _activeRange)
                        return;
                }

                var theta = Mathf.PI - Mathf.Atan2(vector.y, vector.x);
                theta += _unitTheta_2;
                theta %= CMath.c_2_PI_F;
                var direction = (Int32)(theta / _unitTheta);

                if (magnitude > _activeRange)
                {
                    vector.x = vector.x / magnitude * _activeRange;
                    vector.y = vector.y / magnitude * _activeRange;
                    centerPosition = touchPosition - vector;
                }

                if (direction != _lastDirection)
                    _fCallback(TouchState.move, direction);

                _lastDirection = direction;
            }
            public override void up(Vector2 position)
            {
                up();
            }
            public override void up()
            {
                isOn = false;
                _fCallback(TouchState.up, -1);
                _lastDirection = -1;
                touchPosition = centerPosition = _defaultPosition;
            }
        }
        public class Button : InputObject
        {
            readonly FButton _fCallback;
            public Button(FIsActivated fIsActivated, FButton fCallback) :
                base(fIsActivated)
            {
                _fCallback = fCallback;
            }
            public override void down(Vector2 position)
            {
                isOn = true;
                _fCallback(TouchState.down);
            }
            public override void move(Vector2 position)
            {
            }
            public override void up(Vector2 position)
            {
                isOn = false;

                if (fIsActivated(position))
                    _fCallback(TouchState.up);
                else
                    _fCallback(TouchState.cancel);
            }
            public override void up()
            {
                isOn = false;
                _fCallback(TouchState.up);
            }
        }
        public class Scroller : InputObject
        {
            readonly FScroll _fCallback;
            Vector2 _downPosition;
            Vector2 _lastPosition;
            public Scroller(FIsActivated fIsActivated, FScroll fCallback) :
                base(fIsActivated)
            {
                _fCallback = fCallback;
            }
            public override void down(Vector2 position)
            {
                isOn = true;
                _downPosition = _lastPosition = position;
                _fCallback(TouchState.down, _downPosition, Vector2.zero, Vector2.zero);
            }
            public override void move(Vector2 position)
            {
                _fCallback(TouchState.move, _downPosition, position - _downPosition, position - _lastPosition);
                _lastPosition = position;
            }
            public override void up(Vector2 position)
            {
                isOn = false;
                _fCallback(TouchState.up, _downPosition, position - _downPosition, position - _lastPosition);
            }
            public override void up()
            {
                isOn = false;
                _fCallback(TouchState.up, _downPosition, _lastPosition - _downPosition, Vector2.zero);
            }
        }
        struct _Touch
        {
            public InputObject inputObject;
            public bool flag;

            public _Touch(InputObject inputObject)
            {
                this.inputObject = inputObject;
                flag = false;
            }
        }
        _Touch[] _touches = new _Touch[_fingerCount];
        List<InputObject> _objects = new List<InputObject>();
        bool _curFlag = false; // 매 Update 마다 반전시켜  Input.touches 에 없는것을 _Touches 에서 Release 처리하기 위함

        public void add(InputObject inputObject)
        {
            _objects.Add(inputObject);
        }
        public void update()
        {
            _curFlag = !_curFlag;

#if UNITY_EDITOR
            for (Int32 index = 0; index < _fingerCount; ++index)
            {
                if (!Input.GetMouseButton(index) && !Input.GetMouseButtonUp(index))
                    continue;

                TouchForMouse i = new TouchForMouse
                {
                    fingerId = index,
                    phase = (Input.GetMouseButtonUp(index) ? TouchPhase.Ended : TouchPhase.Moved),
                    position = Input.mousePosition
                };
#else
            foreach (var i in Input.touches)
            {
#endif
                if (_touches[i.fingerId].inputObject == null)
                {
                    switch (i.phase)
                    {
                        case TouchPhase.Began:
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            {
                                foreach (var o in _objects)
                                {
                                    if (o.isOn)
                                        continue;

                                    if (o.fIsActivated(i.position))
                                    {
                                        _touches[i.fingerId].inputObject = o;
                                        o.down(i.position);
                                        break;
                                    }
                                }
                            }
                            break;

                            // 이미 release 된 것이므로 무시
                            //case TouchPhase.Ended:
                            //case TouchPhase.Canceled:
                            //    {
                            //    }
                            //    break;
                    }
                }
                else
                {
                    switch (i.phase)
                    {
                        case TouchPhase.Began:
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            _touches[i.fingerId].inputObject.move(i.position);
                            break;

                        case TouchPhase.Ended:
                        case TouchPhase.Canceled:
                            {
                                _touches[i.fingerId].inputObject.up(i.position);
                                _touches[i.fingerId].inputObject = null;
                            }
                            break;
                    }
                }

                _touches[i.fingerId].flag = _curFlag;
            }

            for (Int32 i = 0; i < _touches.Length; ++i)
            {
                if (_touches[i].inputObject != null && _touches[i].flag != _curFlag) // i.Obj 가 유효하고 Input.touches 에 없으면 Release 처리
                {
                    _touches[i].inputObject.up();
                    _touches[i].inputObject = null;
                }
            }
        }
    }
}