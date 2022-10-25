using rso.core;
using rso.physics;
using rso.unity;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBattlePlayer : MonoBehaviour
{
    public Int32 PlayerIndex { get; private set; } = 0;
    SPoint _InitialPos;
    public SCharacterMeta Meta;
    public SByte TeamIndex { get => BattlePlayer.TeamIndex; }
    public SCharacter Character;
    HashSet<CCollider2D> _AttachedGrounds = new HashSet<CCollider2D>();
    CEnginePumpControl _PumpControl = null;
    CEngineParachuteControl _ParachuteControl = null;
    CPlayerCollider _PlayerCollider;
    public CPlayerObject2D PlayerObject = null;
    public SBattlePlayer BattlePlayer { get; private set; } = null;
    public bool IsMe { get; private set; }
    protected Character _Character = null;
    protected CBlink _Blink = null;
    public float getStaminaNormalValue()
    {
        return Character.StaminaInfo.Stamina / Meta.StaminaMax;
    }
    void _PumpDoneCallback()
    {
        Character.BalloonCount = global.c_BalloonCountForPump;
        _PlayerCollider.Balloon.SetSizeX(CEngineGlobal.BalloonWidth(Character.BalloonCount));
        _PlayerCollider.Balloon.Enabled = true;
        _ParachuteControl.Clear();

        if (IsMe)
            CGlobal.LoginNetSc.User.BlowBalloonTotal += 1;

        Stop();
        _BlowBalloonOff();
        _Character.SetBalloonCount(BalloonCount);
    }
    void _ParachuteOnCallback(bool On_)
    {
        _PlayerCollider.Parachute.Enabled = On_;

        if (On_)
            CGlobal.Sound.PlayOneShot((Int32)ESound.Parachute);
    }
    protected void _AttachGround(CCollider2D OtherCollider_)
    {
        if (!_AttachedGrounds.Add(OtherCollider_) || _AttachedGrounds.Count != 1) // 최초 지형이 붙었을 때 한번만 코드 실행 위해 1과 비교
            return;

        Character.IsGround = true;

        if (Character.BalloonCount == 0)
            _ParachuteControl.Off();

        _Character.Land(Character.Dir);
    }
    protected void _DetachGround(CCollider2D OtherCollider_)
    {
        if (!_AttachedGrounds.Remove(OtherCollider_) || _AttachedGrounds.Count != 0) // 마지막 지형이 떨어졌을때 한번만 코드 실행 위해 0과 비교
            return;

        Character.IsGround = false;
        _PumpControl.Clear();

        if (Character.BalloonCount == 0)
            _ParachuteControl.On();

        Fly();
    }
    public void bounce(SCollision2D collision)
    {
        if (collision.Normal.X != 0.0f)
        {
            if (PlayerObject.Velocity.X > 0.0f && collision.Normal.X < 0.0f ||
                PlayerObject.Velocity.X < 0.0f && collision.Normal.X > 0.0f)
            {
                PlayerObject.Velocity.X = -PlayerObject.Velocity.X;
                _PlayBounce();
            }
        }
        else if (collision.Normal.Y != 0.0f)
        {
            if (PlayerObject.Velocity.Y > 0.0f && collision.Normal.Y < 0.0f ||
                PlayerObject.Velocity.Y < 0.0f && collision.Normal.Y > 0.0f)
            {
                PlayerObject.Velocity.Y = -PlayerObject.Velocity.Y;
                _PlayBounce();
            }
        }

        return;

        if (collision.OtherMovingObject != null)
        {
            var multiplier = collision.OtherMovingObject.GetPlayerObject2D() != null ? 2.0f * collision.OtherMovingObject.Mass / (PlayerObject.Mass + collision.OtherMovingObject.Mass) : 2.0f;

            if (collision.Normal.X != 0.0f)
                PlayerObject.Velocity.X += collision.RelativeVelocity.X * multiplier;
            else
                PlayerObject.Velocity.Y += collision.RelativeVelocity.Y * multiplier;
        }
        else
        {
            if (collision.Normal.X != 0.0f)
                PlayerObject.Velocity.X = -PlayerObject.Velocity.X;
            else
                PlayerObject.Velocity.Y = -PlayerObject.Velocity.Y;
        }

        _PlayBounce();
    }
    public void Die(Int64 tick)
    {
        Character.BalloonCount = -1;
        _Character.SetBalloonCountAndShowPopEffect(0, 0);
        _Die(tick);
    }
    protected void _Die(Int64 tick)
    {
        PlayerObject.Enabled = false;
        PlayerObject.Velocity.X = 0.0f;
        PlayerObject.Velocity.Y = global.c_DieUpVel;
        Character.IsGround = false;
        Character.Dir = 0;
        Character.RegenTick = tick + global.c_RegenDelayTickCount;

        _PumpControl.Clear();
        _ParachuteControl.Clear();
        _AttachedGrounds.Clear();

        _BlowBalloonOff();
        _Character.Die();
        CGlobal.Sound.PlayOneShot((Int32)ESound.Down);

        if (IsMe)
            CGlobal.VibeOn(0);
    }
    protected bool _beHitBalloon(SPoint Normal_) // return true : dead
    {
        --Character.BalloonCount;

        if (Character.BalloonCount >= 0)
        {
            _Character.SetBalloonCountAndShowPopEffect(Character.BalloonCount, (SByte)(Normal_.X * -1.0f));

            if (Character.BalloonCount > 0)
            {
                _PlayerCollider.Balloon.SetSizeX(CEngineGlobal.BalloonWidth(Character.BalloonCount));
            }
            else
            {
                if (!Character.IsGround)
                    _ParachuteControl.On();

                _PlayerCollider.Balloon.Enabled = false;
            }

            return false;
        }

        return true;
    }
    public bool IsAlive()
    {
        return (Character.BalloonCount >= 0);
    }
    public bool IsInvulerable(Int64 tick)
    {
        return (tick < Character.InvulnerableEndTick);
    }
    public void direct(SByte dir)
    {
        if (dir != 0)
        {
            if (Character.Face != dir)
                Character.Face = dir;

            if (Character.IsGround)
                _PumpControl.Clear();
        }

        Character.Dir = dir;

        if (Character.IsGround)
        {
            if (dir == 0)
            {
                Stop();
            }
            else
            {
                _BlowBalloonOff();

                _Character.Move(dir);

                //        CGlobal.Sound.PlayOneShot((Int32)ESound.Walk);
            }
        }
        else if (dir != 0)
        {
            Face(dir);
        }
    }
    protected virtual bool IsStaminaFree()
    {
        return false;
    }
    public bool canPush()
    {
        return Character.canFlap() || (Character.canPump() && _PumpControl.canPump());
    }
    public void push(Int64 tick)
    {
        if (Character.canFlap())
            flap(tick);
        else if (Character.canPump() && _PumpControl.canPump())
            pump();
    }
    public void flap(Int64 tick)
    {
        if (!IsStaminaFree() && Character.StaminaInfo.Stamina >= 1.0f)
        {
            Character.StaminaInfo.Stamina -= 1.0f;
            Character.StaminaInfo.LastUsedTick = tick;
        }

        var YDiff = Meta.MaxVelAir - PlayerObject.Velocity.Y;
        if (YDiff > 0.0f)
        {
            if (YDiff > global.c_FlapDeltaVelUp)
                PlayerObject.Velocity.Y += global.c_FlapDeltaVelUp;
            else
                PlayerObject.Velocity.Y = Meta.MaxVelAir;
        }

        if (Character.Dir > 0)
        {
            var XDiff = Meta.MaxVelAir - PlayerObject.Velocity.X;
            if (XDiff > 0.0f)
            {
                if (XDiff > global.c_FlapDeltaVelX)
                    PlayerObject.Velocity.X += global.c_FlapDeltaVelX;
                else
                    PlayerObject.Velocity.X = Meta.MaxVelAir;
            }
        }
        else if (Character.Dir < 0)
        {
            var XDiff = Meta.MaxVelAir + PlayerObject.Velocity.X;
            if (XDiff > 0.0f)
            {
                if (XDiff > global.c_FlapDeltaVelX)
                    PlayerObject.Velocity.X -= global.c_FlapDeltaVelX;
                else
                    PlayerObject.Velocity.X = -Meta.MaxVelAir;
            }
        }

        _BlowBalloonOff();

        _Character.Flap();

        CGlobal.Sound.PlayOneShot((Int32)ESound.Flap);
    }
    public void pump()
    {
        _PumpControl.pump();
    }
    public void Link(Int64 Tick_)
    {
        // rso todo 모래시계 사라지고
    }
    public void UnLink(Int64 Tick_)
    {
        // rso todo 모래시계 보여주고
    }

    public void CheckRegen(Int64 tick)
    {
        if (IsAlive() || tick < Character.RegenTick)
            return;

        Character.IsGround = false;
        Character.Dir = 0;
        Character.BalloonCount = global.c_BalloonCountForRegen;
        Character.StaminaInfo.LastUsedTick = tick;
        Character.StaminaInfo.Stamina = Meta.StaminaMax;
        Character.Face = CEngineGlobal.GetFaceWithX(_InitialPos.X);
        PlayerObject.LocalPosition.Set(_InitialPos);
        PlayerObject.Velocity.Clear();
        Character.InvulnerableEndTick = CEngineGlobal.GetInvulnerableEndTick(tick);
        Character.ChainKillCount = 0;
        Character.LastKillTick = 0;
        Character.RegenTick = 0;

        _PlayerCollider.Body.Enabled = true;
        _PlayerCollider.Balloon.SetSizeX(CEngineGlobal.BalloonWidth(Character.BalloonCount));
        _PlayerCollider.Balloon.Enabled = true;
        PlayerObject.Enabled = true;

        _Character.Regen(Character.Face, Character.BalloonCount);
        _BlinkOn(Character.InvulnerableEndTick - tick);
    }
    protected virtual void _FixedUpdate(Int64 tick)
    {
        _UpdateStamina(tick);
        _UpdatePhysics();
        _Character.updateAnimationMoveSpeed();
    }
    protected void _UpdateStamina(Int64 tick)
    {
        if (tick > Character.StaminaInfo.LastUsedTick + global.c_StaminaRegenDelayTick)
        {
            Character.StaminaInfo.Stamina += ((Character.IsGround ? global.c_StaminaRegenSpeedOnGround : global.c_StaminaRegenSpeedInAir) * CEngine.DeltaTime);
            if (Character.StaminaInfo.Stamina > Meta.StaminaMax)
                Character.StaminaInfo.Stamina = Meta.StaminaMax;
        }
    }
    protected void _UpdateGroundPhysics(CMovingObject2D OtherMovingObject_)
    {
        var OtherObjectXVelocity = OtherMovingObject_ == null ? 0.0f : OtherMovingObject_.Velocity.X;
        var DestXVelocity = Character.Dir > 0 ? Meta.MaxVelXGround : Character.Dir < 0 ? -Meta.MaxVelXGround : 0.0f;
        var XVelocityOnGround = PlayerObject.Velocity.X - OtherObjectXVelocity;

        var DeltaX = (DestXVelocity - XVelocityOnGround) * global.c_GroundAccRatio * CEngine.DeltaTime;
        if (Math.Abs(DeltaX) > global.c_IgnoredGroundMaxDeltaVelocity)
            PlayerObject.Velocity.X += DeltaX;
        else
            PlayerObject.Velocity.X = OtherObjectXVelocity + DestXVelocity;
    }
    protected void _UpdatePhysics()
    {
        if (Character.IsGround)
        {
            if (Character.Dir == 0)
                _PumpControl.FixedUpdate();
        }
        else
        {
            _updateXVelocityInAir();
            _updateYVelocityInAir();
        }

        PlayerObject.LocalPosition.X += (CEngine.DeltaTime * PlayerObject.Velocity.X);
        PlayerObject.LocalPosition.Y += (CEngine.DeltaTime * PlayerObject.Velocity.Y);

        if (_ParachuteControl.FixedUpdate())
            _PlayerCollider.Parachute.LocalScale.Set(Character.ParachuteInfo.Scale);
    }
    void _updateXVelocityInAir()
    {
        if (Character.BalloonCount > 0)
        {
            if (PlayerObject.Velocity.X > Meta.MaxVelAir)
                _approximateAirXVelocity(Meta.MaxVelAir);
            else if (PlayerObject.Velocity.X < -Meta.MaxVelAir)
                _approximateAirXVelocity(-Meta.MaxVelAir);
        }
        else if (Character.BalloonCount == 0)
        {
            if (Character.Dir > 0)
                _approximateAirXVelocity(global.c_MaxVelParachuteX);
            else if (Character.Dir < 0)
                _approximateAirXVelocity(-global.c_MaxVelParachuteX);
            else
                if (PlayerObject.Velocity.X > global.c_MaxVelParachuteX)
                    _approximateAirXVelocity(global.c_MaxVelParachuteX);
                else if (PlayerObject.Velocity.X < -global.c_MaxVelParachuteX)
                    _approximateAirXVelocity(-global.c_MaxVelParachuteX);
        }
    }
    void _updateYVelocityInAir()
    {
        var YAcc = Character.BalloonCount > 0 ? global.c_Gravity : Character.BalloonCount == 0 ? (global.c_Gravity * global.c_GravityParachuteRatio) : (global.c_Gravity * global.c_GravityDeadRatio);

        float MaxYVel;
        if (Character.BalloonCount > 0)
            MaxYVel = -global.c_MaxVelDown;
        else if (Character.BalloonCount == 0)
            MaxYVel = -global.c_MaxVelParachuteY;
        else
            MaxYVel = -global.c_MaxVelDeadY;

        var Diff = MaxYVel - PlayerObject.Velocity.Y;
        if (Diff > 0.0f) // 하강속도 초과
        {
            PlayerObject.Velocity.Y += (Diff * global.c_AirAccRatio * CEngine.DeltaTime);
            if (PlayerObject.Velocity.Y > MaxYVel)
                PlayerObject.Velocity.Y = MaxYVel;
        }
        else
        {
            PlayerObject.Velocity.Y += (YAcc * CEngine.DeltaTime);
            if (PlayerObject.Velocity.Y < MaxYVel)
                PlayerObject.Velocity.Y = MaxYVel;
        }
    }
    void _approximateAirXVelocity(float value)
    {
        PlayerObject.Velocity.X += ((value - PlayerObject.Velocity.X) * global.c_AirAccRatio * CEngine.DeltaTime);
    }
    protected void _BlinkOn(Int64 DurationTick_)
    {
        if (DurationTick_ <= 0)
            return;

        _Blink.On((float)TimeSpan.FromTicks(DurationTick_).TotalSeconds, 0.1f, 0.05f);
    }
    public Vector2 velocity
    {
        get { return PlayerObject.Velocity.ToVector2(); }
    }
    public bool IsGround { get { return Character.IsGround; } }
    public sbyte BalloonCount { get { return Character.BalloonCount; } }
    protected bool _LandEnter(SCollision2D Collision_)
    {
        if (Collision_.OtherCollider.Number != CEngineGlobal.c_StructureNumber ||
            Collision_.Collider.Number != CEngineGlobal.c_BodyNumber ||
            Collision_.Normal.Y <= 0.0f)
            return false;

        _UpdateGroundPhysics(Collision_.OtherMovingObject);
        _AttachGround(Collision_.OtherCollider);
        return true;
    }
    protected virtual bool _CollisionEnter(Int64 tick, SCollision2D Collision_)
    {
        if (_LandEnter(Collision_))
            return false;

        bounce(Collision_);

        return false;
    }
    protected void _LandStay(SCollision2D Collision_)
    {
        if (Collision_.Normal.Y > 0.0f)
        {
            _UpdateGroundPhysics(Collision_.OtherMovingObject);
            _AttachGround(Collision_.OtherCollider);
        }
        else
        {
            _DetachGround(Collision_.OtherCollider);
        }
    }
    protected virtual bool _CollisionStay(Int64 tick, SCollision2D Collision_)
    {
        if (Collision_.Collider.Number != CEngineGlobal.c_BodyNumber || Collision_.OtherCollider.Number != CEngineGlobal.c_StructureNumber) // 몸이 지형에 안 닿았으면
            return false;

        _LandStay(Collision_);

        return false;
    }
    protected virtual bool _CollisionExit(Int64 tick, SCollision2D Collision_)
    {
        if (Collision_.Collider.Number != CEngineGlobal.c_BodyNumber || Collision_.OtherCollider.Number != CEngineGlobal.c_StructureNumber)
            return false;

        _DetachGround(Collision_.OtherCollider);

        return false;
    }

    protected virtual bool _TriggerEnter(CCollider2D Collider_)
    {
        return false;
    }
    protected void Init(
        Int32 PlayerIndex_,
        SPoint InitialPos_,
        SCharacterMeta Meta_,
        SCharacterNet Character_,
        TeamMaterial teamMaterial,
        Int64 Tick_,
        SBattlePlayer BattlePlayer_,
        CBattlePlayer VirtualBattlePlayer_,
        bool IsMe_,
        Transform Prop_,
        Camera Camera_)
    {
        PlayerIndex = PlayerIndex_;
        _InitialPos = InitialPos_;
        Meta = Meta_;
        Character = Character_;

        _PumpControl = new CEnginePumpControl(_PumpCallback, _PumpDoneCallback, Meta.PumpSpeed, Character.PumpInfo);
        _ParachuteControl = new CEngineParachuteControl(_ParachuteOnCallback, Character.ParachuteInfo);

        _PlayerCollider = new CPlayerCollider(Character_);
        _PlayerCollider.Body.Enabled = IsAlive();
        _PlayerCollider.Balloon.Enabled = (Character.BalloonCount > 0);
        _PlayerCollider.Parachute.Enabled = Character.ParachuteInfo.IsScaling();
        PlayerObject = new CBattlePlayerObject(CPhysics.GetDefaultTransform(Character_.Pos), _PlayerCollider.Colliders, Character_.Vel, VirtualBattlePlayer_);
        PlayerObject.fCollisionEnter = _CollisionEnter;
        PlayerObject.fCollisionStay = _CollisionStay;
        PlayerObject.fCollisionExit = _CollisionExit;
        PlayerObject.fFixedUpdate = _FixedUpdate;
        BattlePlayer = BattlePlayer_;

        IsMe = IsMe_;
        _Blink = new CBlink(_Show);

        transform.localPosition = Character_.Pos.ToVector2();
        var Obj = new GameObject("Char_0");
        Obj.AddComponent<Character>();
        Obj.transform.SetParent(transform);
        Obj.transform.localPosition = Vector3.zero;

        _Character = GetComponentInChildren<Character>();
        Debug.Assert(_Character != null);

        _Character.Init(this, teamMaterial, IsMe, Prop_, Camera_);

        var Animator = Obj.GetComponentInChildren<Animator>();
        if (IsMe)
            Animator.gameObject.AddComponent<CharacterAnimationEvent>();
        else
            Animator.gameObject.AddComponent<CharacterAnimationEventEmpty>();

        _BlinkOn(Character_.InvulnerableEndTick - Tick_);
        Face(Character_.Face);

        if (Character.PumpInfo.CountTo > 0)
        {
            _BlowBalloonOn();
            _Pump();
        }
    }
    protected void _Pump()
    {
        _Character.Pump();
        CGlobal.Sound.PlayOneShot((Int32)ESound.Pump);
    }
    protected void _BlowBalloonOn()
    {
        _Character.BlowBalloonSetActive(true);
    }
    protected void _FaceUpDown(float VelY_)
    {
        _Character.FaceUpDown(VelY_);
    }
    public void Stop()
    {
        _Character.Stop();
    }
    public void Face(sbyte Dir_)
    {
        _BlowBalloonOff();

        _Character.Face(Dir_);
    }
    public void Fly()
    {
        _BlowBalloonOff();

        if (IsAlive()) // 죽었을 때에도 엔진으로 부터 지형과 떨어짐 Fly 콜백이 오므로
        {
            _Character.Fly();
        }
    }
    void _PumpCallback()
    {
        if (Character.PumpInfo.Count == 0) // 처음 부는 거라면 BlowBalloon 활성화
            _BlowBalloonOn();

        _Pump();
    }
    void _SetParachuteScale(float Scale_)
    {
        _Character.SetParachuteScale(Scale_);
    }
    void _PlayBounce()
    {
        if (BattlePlayer.UID == CGlobal.UID)
            CGlobal.Sound.PlayOneShot((Int32)ESound.Bounce);
    }
    public void ShowPoint(Int32 Point_)
    {
        _Character.ShowPoint(BalloonCount, Point_);
    }
    public void SetEmotion(Int32 Code_)
    {
        _SetEmotion(Code_);
    }
    protected void _Win()
    {
        _Character.Win();
    }
    protected void _Lose()
    {
        _Character.Lose();
    }
    protected void _SetEmotion(Int32 Code_)
    {
        _Character.SetEmotion(Code_);
    }
    protected void _BlowBalloonOff()
    {
        _Character.BlowBalloonSetActive(false);
    }
    protected void _BlowBalloonSetScale(float Scale_)
    {
        _Character.BlowBalloonSetScale(Scale_);
    }
    public void Update()
    {
        transform.localPosition = PlayerObject.LocalPosition.ToVector2();
        _Blink.Update();
        _FaceUpDown(velocity.y);
        _BlowBalloonSetScale(Character.PumpInfo.Scale);
        _SetParachuteScale(Character.ParachuteInfo.Scale);
    }
    protected void _Show(bool Show_)
    {
        _Character.Show(Show_);
    }
    public float GetY()
    {
        return transform.position.y;
    }
}