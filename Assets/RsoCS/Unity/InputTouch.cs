using System;
using System.Collections.Generic;
using UnityEngine;

namespace rso.unity
{
    public class CInputTouch
    {
        public enum EState
        {
            Down,
            Move,
            Up,
            Cancel
        }

#if UNITY_EDITOR
        struct TouchForMouse
        {
            public Int32 fingerId;
            public Vector2 position;
            public TouchPhase phase;
        }
        const Int32 c_FingerCountMax = 3; // Mouse Button Count
#else
        const Int32 c_FingerCountMax = 10;
#endif
        public delegate bool FCheck(Vector2 Pos_);
        public delegate void FAim(EState State_, Vector2 Pos_);
        public delegate void FStick(EState State_, Vector2 Pos_, Vector2 Dir_);
        public delegate void FPad(EState State_, Vector2 Pos_, Int32 Dir_);
        public delegate void FButton(EState State_);
        public delegate void FScroll(EState State_, Vector2 DownPos_, Vector2 Diff_, Vector2 Delta_);
        public abstract class CObject
        {
            protected readonly FCheck _fCheck;
            protected bool _On = false; // 객체가 작동중인가? (동시에 1개만 작동될 수 있음)
            public abstract void Down(Vector2 Pos_);
            public abstract void Move(Vector2 Pos_);
            public abstract void Up(Vector2 Pos_);
            public abstract void Up(); // Update() 호출 지연으로 Touch phase 의 Ended 가 누락된 경우 호출
            public CObject(FCheck fCheck_)
            {
                _fCheck = fCheck_;
            }
            public bool IsOn()
            {
                return _On;
            }
            public bool Check(Vector2 Pos_)
            {
                return _fCheck(Pos_);
            }
        }
        public class CObjectAim : CObject
        {
            readonly FAim _fCallback;
            protected Vector2 _LastPos;
            public CObjectAim(FCheck fCheck_, FAim fCallback_) :
                base(fCheck_)
            {
                _fCallback = fCallback_;
            }
            public override void Down(Vector2 Pos_)
            {
                _On = true;
                _LastPos = Pos_;
                _fCallback(EState.Down, Pos_);
            }
            public override void Move(Vector2 Pos_)
            {
                _LastPos = Pos_;
                _fCallback(EState.Move, Pos_);
            }
            public override void Up(Vector2 Pos_)
            {
                Up();
            }
            public override void Up()
            {
                _On = false;
                _fCallback(EState.Up, _LastPos);
            }
        }
        public class CObjectStick : CObject
        {
            readonly FStick _fCallback;
            readonly float _StandbyRange;
            readonly float _ActiveRange;
            readonly bool _Tracking;
            Vector2 _Pos;
            bool _Activated = false;
            public CObjectStick(FCheck fCheck_, FStick fCallback_, float StandbyRange_, float ActiveRange_, bool Tracking_) :
                base(fCheck_)
            {
                if (StandbyRange_ < 0.0f)
                    throw new Exception("Invalid StandbyRange");

                if (ActiveRange_ < 0.0f)
                    throw new Exception("Invalid ActiveRange");

                _fCallback = fCallback_;
                _StandbyRange = StandbyRange_;
                _ActiveRange = ActiveRange_;
                _Tracking = Tracking_;
            }
            public override void Down(Vector2 Pos_)
            {
                _On = true;
                _Pos = Pos_;
                _fCallback(EState.Down, _Pos, Vector2.zero);
            }
            public override void Move(Vector2 Pos_)
            {
                var Vec = Pos_ - _Pos;
                var Magnitude = Vec.magnitude;
                if (!_Activated)
                {
                    if (Magnitude < _StandbyRange)
                        return;

                    _Activated = true;
                }

                if (Magnitude > _ActiveRange)
                {
                    Vec.x = Vec.x / Magnitude * _ActiveRange;
                    Vec.y = Vec.y / Magnitude * _ActiveRange;

                    if (_Tracking)
                        _Pos = Pos_ - Vec;
                }

                _fCallback(EState.Move, _Pos, Vec);
            }
            public override void Up(Vector2 Pos_)
            {
                Up();
            }
            public override void Up()
            {
                _On = false;
                _fCallback(EState.Up, _Pos, Vector2.zero);
                _Activated = false;
            }
        }
        public class CObjectPad : CObject
        {
            readonly FPad _fCallback;
            readonly float _StandbyRange;
            readonly float _ActiveRange;
            readonly Int32 _DirCount = 1;
            readonly float _UnitTheta;
            readonly float _UnitTheta_2;

            Vector2 _Pos;
            Int32 _LastDir = -1; // 9시 방향부터 0 ~ 반시계방향으로
            public CObjectPad(FCheck fCheck_, FPad fCallback_, float StandbyRange_, float ActiveRange_, Int32 ExpDirCount_) :
                base(fCheck_)
            {
                if (StandbyRange_ < 0.0f)
                    throw new Exception("Invalid StandbyRange");

                if (ActiveRange_ < 0.0f)
                    throw new Exception("Invalid ActiveRange");

                if (ExpDirCount_ < 0)
                    throw new Exception("Invalid ExpDirCount Count");

                _fCallback = fCallback_;
                _StandbyRange = StandbyRange_;
                _ActiveRange = ActiveRange_;

                for (Int32 i = 0; i < ExpDirCount_; ++i)
                    _DirCount *= 2;

                _UnitTheta = Mathf.PI * 2.0f / _DirCount;
                _UnitTheta_2 = _UnitTheta * 0.5f;
            }
            public override void Down(Vector2 Pos_)
            {
                _On = true;
                _Pos = Pos_;
                _fCallback(EState.Down, _Pos, -1);
            }
            public override void Move(Vector2 Pos_)
            {
                var Vec = Pos_ - _Pos;
                var Magnitude = Vec.magnitude;
                if (_LastDir == -1)
                {
                    if (Magnitude < _StandbyRange)
                        return;
                }

                var Theta = Mathf.PI - Mathf.Atan2(Vec.y, Vec.x);
                Theta += _UnitTheta_2;
                Theta %= math.CBase.c_2_PI_F;
                var Dir = (Int32)(Theta / _UnitTheta);

                if (Magnitude > _ActiveRange)
                {
                    Vec.x = Vec.x / Magnitude * _ActiveRange;
                    Vec.y = Vec.y / Magnitude * _ActiveRange;
                    _Pos = Pos_ - Vec;
                }

                if (Dir != _LastDir)
                    _fCallback(EState.Move, _Pos, Dir);

                _LastDir = Dir;
            }
            public override void Up(Vector2 Pos_)
            {
                Up();
            }
            public override void Up()
            {
                _On = false;
                _fCallback(EState.Up, _Pos, -1);
                _LastDir = -1;
            }
        }
        public class CObjectButton : CObject
        {
            readonly FButton _fCallback;
            public CObjectButton(FCheck fCheck_, FButton fCallback_) :
                base(fCheck_)
            {
                _fCallback = fCallback_;
            }
            public override void Down(Vector2 Pos_)
            {
                _On = true;
                _fCallback(EState.Down);
            }
            public override void Move(Vector2 Pos_)
            {
            }
            public override void Up(Vector2 Pos_)
            {
                _On = false;

                if (_fCheck(Pos_))
                    _fCallback(EState.Up);
                else
                    _fCallback(EState.Cancel);
            }
            public override void Up()
            {
                _On = false;
                _fCallback(EState.Up);
            }
        }
        public class CObjectScroll : CObject
        {
            readonly FScroll _fCallback;
            Vector2 _DownPos;
            Vector2 _LastPos;
            public CObjectScroll(FCheck fCheck_, FScroll fCallback_) :
                base(fCheck_)
            {
                _fCallback = fCallback_;
            }
            public override void Down(Vector2 Pos_)
            {
                _On = true;
                _DownPos = _LastPos = Pos_;
                _fCallback(EState.Down, _DownPos, Vector2.zero, Vector2.zero);
            }
            public override void Move(Vector2 Pos_)
            {
                _fCallback(EState.Move, _DownPos, Pos_ - _DownPos, Pos_ - _LastPos);
                _LastPos = Pos_;
            }
            public override void Up(Vector2 Pos_)
            {
                _On = false;
                _fCallback(EState.Up, _DownPos, Pos_ - _DownPos, Pos_ - _LastPos);
            }
            public override void Up()
            {
                _On = false;
                _fCallback(EState.Up, _DownPos, _LastPos - _DownPos, Vector2.zero);
            }
        }
        struct _STouch
        {
            public CObject Obj;
            public bool Flag;

            public _STouch(CObject Obj_)
            {
                Obj = Obj_;
                Flag = false;
            }
        }
        _STouch[] _Touches = new _STouch[c_FingerCountMax];
        List<CObject> _Objects = new List<CObject>();
        bool _CurFlag = false; // 매 Update 마다 반전시켜  Input.touches 에 없는것을 _Touches 에서 Release 처리하기 위함

        public void Add(CObject Object_)
        {
            _Objects.Add(Object_);
        }
        public void Update()
        {
            _CurFlag = !_CurFlag;

#if UNITY_EDITOR
            for (Int32 Index = 0; Index < c_FingerCountMax; ++Index)
            {
                if (!Input.GetMouseButton(Index) && !Input.GetMouseButtonUp(Index))
                    continue;

                TouchForMouse i = new TouchForMouse {
                    fingerId = Index,
                    phase = (Input.GetMouseButtonUp(Index) ? TouchPhase.Ended : TouchPhase.Moved),
                    position = Input.mousePosition };
#else
            foreach (var i in Input.touches)
            {
#endif
                if (_Touches[i.fingerId].Obj == null)
                {
                    switch (i.phase)
                    {
                        case TouchPhase.Began:
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            {
                                foreach (var o in _Objects)
                                {
                                    if (o.IsOn())
                                        continue;

                                    if (o.Check(i.position))
                                    {
                                        _Touches[i.fingerId].Obj = o;
                                        o.Down(i.position);
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
                            _Touches[i.fingerId].Obj.Move(i.position);
                            break;

                        case TouchPhase.Ended:
                        case TouchPhase.Canceled:
                            {
                                _Touches[i.fingerId].Obj.Up(i.position);
                                _Touches[i.fingerId].Obj = null;
                            }
                            break;
                    }
                }

                _Touches[i.fingerId].Flag = _CurFlag;
            }

            for (Int32 i = 0; i < _Touches.Length; ++i)
            {
                if (_Touches[i].Obj != null && _Touches[i].Flag != _CurFlag) // i.Obj 가 유효하고 Input.touches 에 없으면 Release 처리
                {
                    _Touches[i].Obj.Up();
                    _Touches[i].Obj = null;
                }
            }
        }
    }
}