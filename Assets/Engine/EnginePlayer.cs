using rso.physics;
using bb;
using System;
using System.Collections.Generic;
using System.Threading;

using TContactPoint2Ds = System.Collections.Generic.Dictionary<rso.physics.SContactPoint2D, rso.physics.SNormalOtherMovingObject>;

public delegate void FMove(CEnginePlayer Player_, SByte Dir_);
public delegate void FFace(CEnginePlayer Player_, SByte Dir_);
public delegate void FFly(CEnginePlayer Player_);
public delegate void FLand(CEnginePlayer Player_);
public delegate void FFlap(CEnginePlayer Player_);
public delegate void FPump(CEnginePlayer Player_);
public delegate void FPumpDone(CEnginePlayer Player_);
public delegate void FParachuteOn(CEnginePlayer Player_, bool On_);
public delegate void FBounce(CEnginePlayer Player_);
public delegate void FHit(CEnginePlayer Attacker_, CEnginePlayer Target_);
public delegate void FRegen(CEnginePlayer Player_);

public class CEnginePlayer : CPlayerObject2D
{
    FMove _fMove;
    FFace _fFace;
    FFly _fFly;
    FLand _fLand;
    FFlap _fFlap;
    FPump _fPump;
    FPumpDone _fPumpDone;
    FParachuteOn _fParachuteOn;
    FBounce _fBounce;
    FHit _fHit;
    FRegen _fRegen;
    public Int32 PlayerIndex { get; private set; } = 0;
    SPoint _InitialPos;
    public SCharacterClientMeta Meta;
    public SByte TeamIndex;
    public SCharacter Character;
    CRectCollider2D _Body = null;
    CRectCollider2D _Balloon = null;
    CRectCollider2D _Parachute = null;
    HashSet<CCollider2D> _AttachedGrounds = new HashSet<CCollider2D>();
    CEnginePumpControl _PumpControl = null;
    CEngineParachuteControl _ParachuteControl = null;

    void _PumpCallback()
    {
        _fPump(this);
    }
    void _PumpDoneCallback()
    {
        Character.BalloonCount = global.c_BalloonCountForPump;
        _Balloon.SetSizeX(CEngineGlobal.BalloonWidth(Character.BalloonCount));
        _Balloon.Enabled = true;
        Character.Acc.Y = global.c_Gravity;
        _ParachuteControl.Clear();
        _fPumpDone(this);
    }
    void _ParachuteOnCallback(bool On_)
    {
        _Parachute.Enabled = On_;
        _fParachuteOn(this, On_);
    }
    void _AttachGround(CCollider2D OtherCollider_)
    {
        if (!_AttachedGrounds.Add(OtherCollider_) || _AttachedGrounds.Count != 1) // 최초 지형이 붙었을 때 한번만 코드 실행 위해 1과 비교
            return;

        Character.IsGround = true;
        Character.Acc.X = Meta.RunAcc * Character.Dir;

        if (Character.BalloonCount == 0)
            _ParachuteControl.Off();

        _fLand(this);
    }
    void _DetachGround(CCollider2D OtherCollider_)
    {
        if (!_AttachedGrounds.Remove(OtherCollider_) || _AttachedGrounds.Count != 0) // 마지막 지형이 떨어졌을때 한번만 코드 실행 위해 0과 비교
            return;

        Character.IsGround = false;

        if (Character.BalloonCount == 0)
            Character.Acc.X = global.c_ParachuteAccX * Character.Dir;

        _PumpControl.Clear();

        if (Character.BalloonCount == 0)
            _ParachuteControl.On();
        
        _fFly(this);
    }
    void _Bounce(SPoint Normal_)
    {
        if (Normal_.X != 0.0f)
        {
            if (Velocity.X > 0.0f && Normal_.X < 0.0f ||
                Velocity.X < 0.0f && Normal_.X > 0.0f)
            {
                Velocity.X = -Velocity.X;
                _fBounce(this);
            }
        }
        else if (Normal_.Y != 0.0f)
        {
            if (Velocity.Y > 0.0f && Normal_.Y < 0.0f ||
                Velocity.Y < 0.0f && Normal_.Y > 0.0f)
            {
                Velocity.Y = -Velocity.Y;
                _fBounce(this);
            }
        }
    }
    public CEnginePlayer(FMove fMove_, FFace fFace_, FFly fFly_, FLand fLand_, FFlap fFlap_, FPump fPump_, FPumpDone fPumpDone_, FParachuteOn fParachuteOn_, FBounce fBounce_, FHit fHit_, FRegen fRegen_,
        Int32 PlayerIndex_, SPoint InitialPos_, SCharacterClientMeta Meta_, SByte TeamIndex_, SCharacterNet Character_,
        CCollider2D PlayerCollider_, CRectCollider2D Body_, CRectCollider2D Balloon_, CRectCollider2D Parachute_) : base(Character_.Pos, PlayerCollider_, Character_.Vel)
    {
        _fMove = fMove_;
        _fFace = fFace_;
        _fFly = fFly_;
        _fLand = fLand_;
        _fFlap = fFlap_;
        _fPump = fPump_;
        _fPumpDone = fPumpDone_;
        _fParachuteOn = fParachuteOn_;
        _fBounce = fBounce_;
        _fHit = fHit_;
        _fRegen = fRegen_;

        PlayerIndex = PlayerIndex_;
        _InitialPos = InitialPos_;
        Meta = Meta_;
        TeamIndex = TeamIndex_;
        Character = Character_;

        _Body = Body_;
        _Body.Enabled = Character.IsAlive();

        _Balloon = Balloon_;
        _Balloon.Enabled = (Character.BalloonCount > 0);

        _Parachute = Parachute_;
        _Parachute.Enabled = Character.ParachuteInfo.IsScaling();

        _PumpControl = new CEnginePumpControl(_PumpCallback, _PumpDoneCallback, Meta.PumpSpeed, Character.PumpInfo);
        _ParachuteControl = new CEngineParachuteControl(_ParachuteOnCallback, Character.ParachuteInfo);
    }
    void _Hit(Int64 Tick_, SPoint Normal_, CEnginePlayer Attacker_)
    {
        --Character.BalloonCount;

        if (Character.BalloonCount >= 0)
        {
            if (Character.BalloonCount > 0)
            {
                _Balloon.SetSizeX(CEngineGlobal.BalloonWidth(Character.BalloonCount));
            }
            else
            {
                if (!Character.IsGround)
                {
                    Character.Acc.X = global.c_ParachuteAccX * Character.Dir;
                    _ParachuteControl.On();
                }

                Character.Acc.Y = (global.c_Gravity * global.c_GravityParachuteRatio);
                _Balloon.Enabled = false;
            }

            _Bounce(Normal_);
        }
        else // Die
        {
            if ((Tick_ - Attacker_.Character.LastKillTick) < global.c_ChainKillDelayTickCount)
                ++Attacker_.Character.ChainKillCount;
            else
                Attacker_.Character.ChainKillCount = 1;

            Attacker_.Character.LastKillTick = Tick_;

            Velocity.X = 0.0f;
            Velocity.Y = global.c_DieUpVel;
            Character.Dir = 0;
            Character.Acc.X = 0.0f;
            Character.Acc.Y = (global.c_Gravity * global.c_GravityDeadRatio);
            Character.RegenTick = Tick_ + global.c_RegenDelayTickCount;

            _PumpControl.Clear();
            _ParachuteControl.Clear();
            _Body.Enabled = false;
            _Parachute.Enabled = false;
        }

        _fHit(Attacker_, this);
    }
    void _SetLandingVelocity(CMovingObject2D OtherMovingObject_)
    {
        var ObjectVelocity = OtherMovingObject_ == null ? new SPoint() : OtherMovingObject_.Velocity;
        if (ObjectVelocity.Y > Velocity.Y)
            Velocity.Y = ObjectVelocity.Y;

        if (Character.Dir == 0)
        {
            var DiffX = ObjectVelocity.X - Velocity.X;
            if (DiffX > 0.0f)
            {
                if (DiffX > global.c_LandXDragPerFrame)
                    Velocity.X += global.c_LandXDragPerFrame;
                else
                    Velocity.X = ObjectVelocity.X;
            }
            else
            {
                if (DiffX < -global.c_LandXDragPerFrame)
                    Velocity.X -= global.c_LandXDragPerFrame;
                else
                    Velocity.X = ObjectVelocity.X;

            }
        }
    }
    protected override void OnCollisionEnter(Int64 Tick_, SPoint Normal_, CCollider2D Collider_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
    {
        if (OtherCollider_.Number == CEngineGlobal.c_StructureNumber)
        {
            if (Collider_.Number == CEngineGlobal.c_BodyNumber && Normal_.Y > 0.0f)
            {
                _SetLandingVelocity(OtherMovingObject_);
                _AttachGround(OtherCollider_);
            }
            else
            {
                _Bounce(Normal_);
            }
        }
        else
        {
            var OtherPlayer = OtherMovingObject_ as CEnginePlayer;
            if (OtherPlayer == null)
                return;

            if (TeamIndex != OtherPlayer.TeamIndex && // 다른편이고
                OtherCollider_.Number == CEngineGlobal.c_BodyNumber && // 적의 몸과 부딪혔고
                !Character.IsInvulerable(Tick_) && // 내가 무적이 아니고
                !OtherPlayer.Character.IsInvulerable(Tick_) && // 적이 무적이 아니고
                OtherPlayer.Character.BalloonCount > 0 && // 적이 풍선이 있고
                (Collider_.Number == CEngineGlobal.c_BalloonNumber || Character.BalloonCount == 0)) // 내 풍선과 부딪혔거나, 내 풍선이 없으면
            {
                _Hit(Tick_, Normal_, OtherPlayer);
                OtherPlayer._Bounce(Normal_.GetMulti(-1.0f));
            }
            else
            {
                _Bounce(Normal_);
            }
        }
    }
    protected override void OnCollisionStay(Int64 Tick_, TContactPoint2Ds ContactPoint2Ds_)
    {
        foreach (var i in ContactPoint2Ds_)
        {
            if (i.Key.Collider.Number == CEngineGlobal.c_BodyNumber && i.Key.OtherCollider.Number == CEngineGlobal.c_StructureNumber) // 몸이 지형에 닿았으면
            {
                if (i.Value.Normal.Y > 0.0f)
                {
                    _SetLandingVelocity(i.Value.OtherMovingObject);
                    _AttachGround(i.Key.OtherCollider);
                }
                else
                {
                    _DetachGround(i.Key.OtherCollider);
                }
            }
        }
    }
    protected override void OnCollisionExit(Int64 Tick_, SPoint Normal_, CCollider2D Collider_, CCollider2D OtherCollider_, CMovingObject2D OtherMovingObject_)
    {
        if (Collider_.Number != CEngineGlobal.c_BodyNumber || OtherCollider_.Number != CEngineGlobal.c_StructureNumber)
            return;

        _DetachGround(OtherCollider_);
    }
    public bool IsAlive()
    {
        return Character.IsAlive();
    }
    public bool Touch(SByte Dir_)
    {
        if (!Character.IsAlive())
            return false;

        if (Dir_ == Character.Dir || Dir_ < -1 || Dir_ > 1)
            return false;

        if (Dir_ == 0)
        {
            Character.Acc.X = 0.0f;
        }
        else
        {
            if (Character.Face != Dir_)
                Character.Face = Dir_;

            if (Character.IsGround)
            {
                Character.Acc.X = Meta.RunAcc * Dir_;
                _PumpControl.Clear();
            }
            else if (Character.BalloonCount == 0)
            {
                Character.Acc.X = global.c_ParachuteAccX * Dir_;
            }
        }

        Character.Dir = Dir_;

        if (Character.IsGround)
            _fMove(this, Dir_);
        else if (Dir_ != 0)
            _fFace(this, Dir_);

        return true;
    }
    public bool Push(Int64 Tick_)
    {
        if (!Character.IsAlive())
            return false;

        if (Character.BalloonCount > 0) // Flap
        {
            // Stamina /////////////////////////
            if (Character.StaminaInfo.Stamina < 1.0f)
                return false;

            Character.StaminaInfo.Stamina -= 1.0f;
            Character.StaminaInfo.LastUsedTick = Tick_;
            /////////////////////////////////////////

            Velocity.Y += Meta.FlapDeltaVelUp;
            if (Velocity.Y > Meta.MaxVelUp)
                Velocity.Y = Meta.MaxVelUp;

            if (Character.Dir > 0)
            {
                if (Velocity.X + Meta.FlapDeltaVelX > Meta.MaxVelXAir)
                {
                    if (Meta.MaxVelXAir > Velocity.X)
                        Velocity.X = Meta.MaxVelXAir;
                }
                else
                {
                    Velocity.X += Meta.FlapDeltaVelX;
                }
            }
            else if (Character.Dir < 0)
            {
                if (Velocity.X - Meta.FlapDeltaVelX < -Meta.MaxVelXAir)
                {
                    if (-Meta.MaxVelXAir < Velocity.X)
                        Velocity.X = -Meta.MaxVelXAir;
                }
                else
                {
                    Velocity.X -= Meta.FlapDeltaVelX;
                }
            }

            _fFlap(this);

            return true;
        }
        else if (Character.BalloonCount == 0 && Character.IsGround && Character.Dir == 0)
        {
            return _PumpControl.Pump();
        }

        return false;
    }
    public void Link(Int64 Tick_)
    {
    }
    public void UnLink(Int64 Tick_)
    {
        Touch(0);
    }

    public void CheckRegen(Int64 Tick_)
    {
        if (IsAlive() || Tick_ < Character.RegenTick)
            return;

        Character.IsGround = false;
        Character.Dir = 0;
        Character.BalloonCount = global.c_BalloonCountForRegen;
        Character.StaminaInfo.LastUsedTick = Tick_;
        Character.StaminaInfo.Stamina = Meta.StaminaMax;
        Character.Face = CEngineGlobal.GetFace(_InitialPos);
        LocalPosition.Set(_InitialPos);
        Velocity.Clear();
        Character.Acc.Set(new SPoint(0.0f, global.c_Gravity));
        Character.InvulnerableEndTick = CEngineGlobal.GetInvulnerableEndTick(Tick_);
        Character.ChainKillCount = 0;
        Character.LastKillTick = 0;
        Character.RegenTick = 0;

        //new SCharacter(false, 0, global.c_BalloonCountForRegen, new SPumpInfo(), new SStaminaInfo(Tick_, Meta_.StaminaMax), GetFace(Pos_),
        //    new SPoint(Pos_), new SPoint(), new SPoint(0.0f, global.c_Gravity), GetInvulnerableEndTick(Tick_), 0, 0, 0);

        _Body.Enabled = true;
        _Balloon.SetSizeX(CEngineGlobal.BalloonWidth(Character.BalloonCount));
        _Balloon.Enabled = true;
        _fRegen(this);
    }
    public override void FixedUpdate(Int64 Tick_)
    {
        if (Tick_ > Character.StaminaInfo.LastUsedTick + Meta.StaminaRegenDelay)
        {
            Character.StaminaInfo.Stamina += ((Tick_ - (Character.StaminaInfo.LastUsedTick + Meta.StaminaRegenDelay)) * Meta.StaminaPerTick);
            if (Character.StaminaInfo.Stamina > Meta.StaminaMax)
                Character.StaminaInfo.Stamina = Meta.StaminaMax;
        }

        // 변위 = 0.5 * g * t*t + 현재v * t
        //      = t * (0.5 * g * t + 현재v)

        // Pos X
        LocalPosition.X += (CEngine.DeltaTime * (0.5f * Character.Acc.X * CEngine.DeltaTime + Velocity.X));

        // Pos Y
        LocalPosition.Y += (CEngine.DeltaTime * (0.5f * Character.Acc.Y * CEngine.DeltaTime + Velocity.Y));

        // X 속도 처리
        if (Character.IsGround)
        {
            // X 축 속도 처리
            if (Character.Dir > 0)
            {
                if (Velocity.X < Meta.MaxVelXGround) // 최고 속도가 아니면
                {
                    Velocity.X += Character.Acc.X * CEngine.DeltaTime;
                    if (Velocity.X > Meta.MaxVelXGround)
                        Velocity.X = Meta.MaxVelXGround;
                }
                else if (Velocity.X > Meta.MaxVelXGround) // 점진적 감속
                {
                    var ReducedVel = (global.c_GroundResistance * CEngine.DeltaTime);
                    if (Velocity.X - ReducedVel > Meta.MaxVelXGround)
                        Velocity.X -= ReducedVel;
                    else
                        Velocity.X = Meta.MaxVelXGround;
                }
            }
            else if (Character.Dir < 0)
            {
                if (Velocity.X > -Meta.MaxVelXGround) // 최고 속도가 아니면
                {
                    Velocity.X += Character.Acc.X * CEngine.DeltaTime;
                    if (Velocity.X < -Meta.MaxVelXGround)
                        Velocity.X = -Meta.MaxVelXGround;
                }
                else if (Velocity.X < -Meta.MaxVelXGround) // 점진적 감속
                {
                    var ReducedVel = (global.c_GroundResistance * CEngine.DeltaTime);
                    if (Velocity.X + ReducedVel < -Meta.MaxVelXGround)
                        Velocity.X += ReducedVel;
                    else
                        Velocity.X = -Meta.MaxVelXGround;
                }
            }
            else // 정지할 때 까지 감속
            {
                _PumpControl.FixedUpdate();
            }
        }
        else
        {
            // X 축 속도 처리
            if (Character.BalloonCount == 0 && Character.Dir != 0) // 최대 속도까지 가속
            {
                if (!(Character.Dir == 1 && Velocity.X >= global.c_MaxVelParachuteX ||
                    Character.Dir == -1 && Velocity.X <= -global.c_MaxVelParachuteX)) // 현재 최고가 아니면
                {
                    Velocity.X += Character.Acc.X * CEngine.DeltaTime;
                    if (Velocity.X > global.c_MaxVelParachuteX)
                        Velocity.X = global.c_MaxVelParachuteX;
                    else if (Velocity.X < -global.c_MaxVelParachuteX)
                        Velocity.X = -global.c_MaxVelParachuteX;
                }
            }
        }

        if (_ParachuteControl.FixedUpdate())
            _Parachute.SetScale(Character.ParachuteInfo.Scale);

        // Y 축 속도 처리
        Velocity.Y += Character.Acc.Y * CEngine.DeltaTime;

        var MaxVelY = 0.0f;
        if (Character.BalloonCount > 0)
            MaxVelY = -Meta.MaxVelDown;
        else if (Character.BalloonCount == 0)
            MaxVelY = -global.c_MaxVelParachuteY;
        else
            MaxVelY = -global.c_MaxVelDeadY;

        if (Velocity.Y < MaxVelY)
            Velocity.Y = MaxVelY;
    }
}
