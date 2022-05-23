using rso.core;
using rso.physics;
using rso.unity;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CSinglePlayer : MonoBehaviour
{
    public SCharacterClientMeta Meta { get; private set; } = null;
    public SSingleCharacter Character = null;
    public SSinglePlayer SinglePlayer { get; private set; } = null;
    protected Rigidbody2D _Rigidbody = null;
    protected TimePoint _LastStaminaUpdateTime;
    protected SingleCharacter _Character = null;
    protected CSingleBlowBalloonControl _BlowBalloonControl = null;
    protected GameObject _ParticleParent;
    protected bool _IsScaleUp = false;
    protected bool _IsSlow = false;
    protected float _SlowValue = 0.0f;
    protected Vector3 _ScaleValue;
    public Vector2 position {  get { return _Rigidbody.position; } set { _Rigidbody.position = value; } }
    public Vector2 velocity { get { return _Rigidbody.velocity; } set { _Rigidbody.velocity = value; } }
    public SSingleCharacterMove GetCharacterMove()
    {
        SinglePlayer.Character.Move.Pos.X = _Rigidbody.position.x;
        SinglePlayer.Character.Move.Pos.Y = _Rigidbody.position.y;
        SinglePlayer.Character.Move.Vel.X = _Rigidbody.velocity.x;
        SinglePlayer.Character.Move.Vel.Y = _Rigidbody.velocity.y;

        return SinglePlayer.Character.Move;
    }
    public void SetCharacterMove(SSingleCharacterMove Move_)
    {
        _Rigidbody.position = Move_.Pos.ToVector2();
        _Rigidbody.velocity = Move_.Vel.ToVector2();
    }
    public bool IsGround { get { return SinglePlayer.Character.IsGround; } }
    public sbyte BalloonCount { get { return SinglePlayer.Character.BalloonCount; } }
    public sbyte PumpCount { get { return SinglePlayer.Character.PumpCount; } }
    public bool IsAlive() { return SinglePlayer.IsAlive(); }
    protected void Init(SCharacterClientMeta Meta_, SSinglePlayer SinglePlayer_, SSingleCharacter Character_, TimePoint Now_, GameObject ParticleParent_, Camera Camera_, bool IsStamina_)
    {
        Meta = Meta_;
        Character = Character_;
        SinglePlayer = SinglePlayer_;
        _LastStaminaUpdateTime = Now_;
        _ParticleParent = ParticleParent_;

         var Obj = new GameObject("Char_0");
        _Character = Obj.AddComponent<SingleCharacter>();
        Obj.transform.SetParent(transform);
        Obj.transform.localPosition = Vector3.zero;

        _Character.Init(this, Meta, ParticleParent_, Camera_, IsStamina_);

        var Animator = Obj.GetComponentInChildren<Animator>();
        Animator.gameObject.AddComponent<CharacterAnimationEvent>();

        _Rigidbody = gameObject.AddComponent<Rigidbody2D>();
        _Rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _Rigidbody.angularDrag = 0.0f;
        _Rigidbody.isKinematic = true;
    }
    protected void InitBlowBalloonControl()
    {
        _Face(SinglePlayer.Character.Face);

        if (SinglePlayer.Character.PumpCount > 0)
        {
            _BlowBalloonControl.Blow(SinglePlayer.Character.PumpCount);
            _BlowBalloonOn();
            _Pump();
        }
    }
    protected void _Pump()
    {
        _Character.Pump();

        CGlobal.Sound.PlayOneShot((Int32)ESound.Pump);
    }
    protected void _PumpCore() // Me : ������ ���� ȣ��, Other : PumpOtherSc �������ݷ� ȣ��
    {
        if (_BlowBalloonControl.Count == 0) // ó�� �δ� �Ŷ�� BlowBalloon Ȱ��ȭ
            _BlowBalloonOn();

        _Pump();
    }
    protected void _BlowBalloonOn()
    {
        _Character.BlowBalloonSetActive(true);
    }
    protected void _Face(sbyte Dir_)
    {
        _BlowBalloonOff();

        _Character.Face(Dir_);
    }
    protected void _FaceUpDown(float VelY_)
    {
        _Character.FaceUpDown(VelY_);
    }
    protected void _Stop()
    {
        _Character.Stop();
    }
    protected void _Move(sbyte Dir_)
    {
        _BlowBalloonOff();

        _Character.Move(Dir_);
    }
    protected void _Flap()
    {
        _BlowBalloonOff();

        _Character.Flap();
    }
    protected void _Fly()
    {
        _BlowBalloonOff();

        _Character.Fly(BalloonCount);
    }
    protected void _Die()
    {
        _BlowBalloonOff();

        _Character.Die();

        CGlobal.Sound.PlayOneShot((Int32)ESound.Down);
    }
    protected void _Regen(sbyte BalloonCount_)
    {
        _Character.Regen(1, BalloonCount_);
    }
    protected void _Win()
    {
        _Character.Win();
    }
    protected void _Lose()
    {
        _Character.Lose();
    }
    //protected void _ParachuteOn()
    //{
    //    _Character.ParachuteOn();

    //    CGlobal.Sound.PlayOneShot((Int32)ESound.Parachute);
    //}
    //protected void _ParachuteOff()
    //{
    //    _Character.ParachuteOff();
    //}
    protected void _Land()
    {
        _Character.Land(SinglePlayer.Character.Dir);
    }
    protected void _SetHitBalloon(sbyte Count_, sbyte AttackerRelativeDir_, Int32 AddPoint_)
    {
        _Character.SetHitBalloon(Count_, AttackerRelativeDir_, AddPoint_);
    }
    protected void _SetHitBalloon(sbyte Count_, sbyte AttackerRelativeDir_)
    {
        _Character.SetHitBalloon(Count_, AttackerRelativeDir_);
    }
    protected void _SetBalloonCount(sbyte Count_)
    {
        _Character.SetBalloonCount(Count_);
    }
    protected void _BlowBalloonOff()
    {
        _Character.BlowBalloonSetActive(false);

        _BlowBalloonControl.Clear();
    }
    protected void _BlowBalloonSetScale(float Scale_)
    {
        _Character.BlowBalloonSetScale(Scale_);
    }
    public float GetStamina(TimePoint Now_)
    {
        if (Now_.Ticks <= _LastStaminaUpdateTime.Ticks + Meta.StaminaRegenDelay)
            return SinglePlayer.Character.Stamina;

        var Stamina = SinglePlayer.Character.Stamina + ((Now_.Ticks - (_LastStaminaUpdateTime.Ticks + Meta.StaminaRegenDelay)) * Meta.StaminaPerTick);
        if (Stamina < Meta.StaminaMax)
            return Stamina;
        else
            return Meta.StaminaMax;
    }
    protected bool Flap()
    {
        // Char_.IsGround �� ����Ƽ �������� �����ϹǷ� ���⼭ �������� ����.

        // �ִ� ��� �ӵ��� ��� ���ʿ��� Flap �� �Ǿ� �������� ������ �ʵ��� �ϱ� �����̸�, ����ȭ ���� ���ÿ����� ó������ ����.
        if ((SinglePlayer.Character.Dir == 0 || velocity.x * SinglePlayer.Character.Dir >= Meta.MaxVelXAir) &&
            velocity.y >= global.c_DefaultVel * 0.8f)
            return false;

        var NewVel = velocity;
        NewVel.y += Meta.FlapDeltaVelUp * (_IsSlow == false ? 1.0f : _SlowValue);
        if (NewVel.y > Meta.MaxVelUp)
            NewVel.y = Meta.MaxVelUp;

        if (SinglePlayer.Character.Dir > 0)
        {
            if (velocity.x + Meta.FlapDeltaVelX > Meta.MaxVelXAir)
            {
                if (Meta.MaxVelXAir > velocity.x)
                    NewVel.x = Meta.MaxVelXAir;
            }
            else
            {
                NewVel.x += Meta.FlapDeltaVelX;
            }
        }
        else if (SinglePlayer.Character.Dir < 0)
        {
            if (velocity.x - Meta.FlapDeltaVelX < -Meta.MaxVelXAir)
            {
                if (-Meta.MaxVelXAir < velocity.x)
                    NewVel.x = -Meta.MaxVelXAir;
            }
            else
            {
                NewVel.x -= Meta.FlapDeltaVelX;
            }
        }

        velocity = NewVel;

        return true;
    }
    protected void FixedUpdate()
    {
        // Fix X
        var FixedX = position.x;
        if (FixedX < 0.0f)
            FixedX += ((Int32)(-FixedX / global.c_2_ScreenWidth) + 1) * global.c_2_ScreenWidth;
        FixedX %= (global.c_2_ScreenWidth);

        position = new Vector2(FixedX, position.y);

        var NewVel = velocity;
        // �ӵ� ó��
        if (SinglePlayer.Character.IsGround)
        {
            // X �� �ӵ� ó��
            if (SinglePlayer.Character.Dir > 0)
            {
                if (NewVel.x < Meta.MaxVelXGround) // �ְ� �ӵ��� �ƴϸ�
                {
                    NewVel.x += SinglePlayer.Character.Acc.X * Time.deltaTime;
                    if (NewVel.x > Meta.MaxVelXGround)
                        NewVel.x = Meta.MaxVelXGround;
                }
                else if (NewVel.x > Meta.MaxVelXGround) // ������ ����
                {
                    var ReducedVel = (global.c_GroundResistance * Time.deltaTime);
                    if (NewVel.x - ReducedVel > Meta.MaxVelXGround)
                        NewVel.x -= ReducedVel;
                    else
                        NewVel.x = Meta.MaxVelXGround;
                }
            }
            else if (SinglePlayer.Character.Dir < 0)
            {
                if (NewVel.x > -Meta.MaxVelXGround) // �ְ� �ӵ��� �ƴϸ�
                {
                    NewVel.x += SinglePlayer.Character.Acc.X * Time.deltaTime;
                    if (NewVel.x < -Meta.MaxVelXGround)
                        NewVel.x = -Meta.MaxVelXGround;
                }
                else if (NewVel.x < -Meta.MaxVelXGround) // ������ ����
                {
                    var ReducedVel = (global.c_GroundResistance * Time.deltaTime);
                    if (NewVel.x + ReducedVel < -Meta.MaxVelXGround)
                        NewVel.x += ReducedVel;
                    else
                        NewVel.x = -Meta.MaxVelXGround;
                }
            }
            else // ������ �� ���� ����
            {
                if (NewVel.x != 0.0f) // �����ϰ� �ִ� ��캸�� ������ �ִ� ��찡 �� ���� ���̹Ƿ� ���ǹ��� �̷���.
                {
                    if (NewVel.x > 0.0f)
                    {
                        NewVel.x -= (Meta.RunAcc * Time.deltaTime);
                        if (NewVel.x < 0.0f)
                            NewVel.x = 0.0f;
                    }
                    else
                    {
                        NewVel.x += (Meta.RunAcc * Time.deltaTime);
                        if (NewVel.x > 0.0f)
                            NewVel.x = 0.0f;
                    }
                }
            }
        }
        else
        {
            // X �� �ӵ� ó��
            if (SinglePlayer.Character.BalloonCount == 0 && SinglePlayer.Character.Dir != 0) // �ִ� �ӵ����� ����
            {
                if (!(SinglePlayer.Character.Dir == 1 && NewVel.x >= global.c_MaxVelParachuteX ||
                    SinglePlayer.Character.Dir == -1 && NewVel.x <= -global.c_MaxVelParachuteX)) // ���� �ְ� �ƴϸ�
                {
                    NewVel.x += SinglePlayer.Character.Acc.X * Time.deltaTime;
                    if (NewVel.x > global.c_MaxVelParachuteX)
                        NewVel.x = global.c_MaxVelParachuteX;
                    else if (NewVel.x < -global.c_MaxVelParachuteX)
                        NewVel.x = -global.c_MaxVelParachuteX;
                }
            }

            // Y �� �ӵ� ó��
            var MaxVelY = 0.0f;
            if (SinglePlayer.Character.BalloonCount > 0)
                MaxVelY = -Meta.MaxVelDown;
            else if (SinglePlayer.Character.BalloonCount == 0)
                MaxVelY = -global.c_MaxVelParachuteY;
            else
                MaxVelY = -global.c_MaxVelDeadY;

            if (NewVel.y < MaxVelY)
                NewVel.y = MaxVelY;
        }

        velocity = NewVel;
    }
    public void Update()
    {
        _FaceUpDown(velocity.y);

        if (_BlowBalloonControl.Update())
            _BlowBalloonSetScale(_BlowBalloonControl.Scale);
    }
    protected void _Show(bool Show_)
    {
        _Character.Show(Show_);
    }
    public float GetY()
    {
        return transform.position.y;
    }
    public float GetX()
    {
        return transform.position.x;
    }
    public void SetScaleUp(bool IsScaleUp_)
    {
        _IsScaleUp = IsScaleUp_;
        if (IsScaleUp_)
            transform.localScale = _ScaleValue;
        else
            transform.localScale = Vector3.one;
    }
    public bool GetScaleUp()
    {
        return _IsScaleUp;
    }
    public void SetSlow(bool IsSlow_)
    {
        _IsSlow = IsSlow_;
    }
    public bool GetSlow()
    {
        return _IsSlow;
    }
}