using bb;
using rso.core;
using rso.gameutil;
using rso.physics;
using System;
using System.Collections.Generic;
using System.Linq;

public delegate void FBattleMove(Int32 PlayerIndex_, SByte Dir_);
public delegate void FBattleFace(Int32 PlayerIndex_, SByte Dir_);
public delegate void FBattleFly(Int32 PlayerIndex_);
public delegate void FBattleLand(Int32 PlayerIndex_);
public delegate void FBattleFlap(Int32 PlayerIndex_);
public delegate void FBattlePump(Int32 PlayerIndex_);
public delegate void FBattlePumpDone(Int32 PlayerIndex_);
public delegate void FBattleParachuteOn(Int32 PlayerIndex_, bool On_);
public delegate void FBattleBounce(Int32 PlayerIndex_);
public delegate void FBattleHit(Int32 AttackerIndex_, Int32 TargetIndex_, Int32 AddedPoint_);
public delegate void FBattleRegen(Int32 PlayerIndex_, SCharacter Character_);
public class CMessageTouch : CMessage
{
    public delegate bool FCallback(SByte Dir_);

    FCallback _Callback;
    SByte _Dir;
    public CMessageTouch(Int64 Tick_, FCallback Callback_, SByte Dir_) :
        base(Tick_)
    {
        _Callback = Callback_;
        _Dir = Dir_;
    }
    public override void Proc()
    {
        _Callback(_Dir);
    }
}
public class CMessagePush : CMessage
{
    public delegate bool FCallback(Int64 Tick_);

    FCallback _Callback;
    public CMessagePush(Int64 Tick_, FCallback Callback_) :
        base(Tick_)
    {
        _Callback = Callback_;
    }
    public override void Proc()
    {
        _Callback(Tick);
    }
}
public class CMessageLink : CMessage
{
    public delegate void FCallback(Int64 Tick_);

    FCallback _Callback;
    public CMessageLink(Int64 Tick_, FCallback Callback_) :
        base(Tick_)
    {
        _Callback = Callback_;
    }
    public override void Proc()
    {
        _Callback(Tick);
    }
}
public class CMessageUnLink : CMessage
{
    public delegate void FCallback(Int64 Tick_);

    FCallback _Callback;
    public CMessageUnLink(Int64 Tick_, FCallback Callback_) :
        base(Tick_)
    {
        _Callback = Callback_;
    }
    public override void Proc()
    {
        _Callback(Tick);
    }
}

public class CPhysicsEngine
{
    CEngineGameMode _GameMode;
    FBattleMove _fBattleMove;
    FBattleFace _fBattleFace;
    FBattleFly _fBattleFly;
    FBattleLand _fBattleLand;
    FBattleFlap _fBattleFlap;
    FBattlePump _fBattlePump;
    FBattlePumpDone _fBattlePumpDone;
    FBattleParachuteOn _fBattleParachuteOn;
    FBattleBounce _fBattleBounce;
    FBattleHit _fBattleHit;
    FBattleRegen _fBattleRegen;
    public SBattleRecord BattleRecord { get; private set; } = null;
    List<SPoint> _PlayerPoses = null;
    CClientEngine _Engine;
    List<CMovingObject2D> _MovingStructures = new List<CMovingObject2D>();
    CEnginePlayer[] _EnginePlayers = null;
    CObject2D _RootObject;

    void _MoveCallback(CEnginePlayer Player_, SByte Dir_)
    {
        _fBattleMove(Player_.PlayerIndex, Dir_);
    }
    void _FaceCallback(CEnginePlayer Player_, SByte Dir_)
    {
        _fBattleFace(Player_.PlayerIndex, Dir_);
    }
    void _FlyCallback(CEnginePlayer Player_)
    {
        _fBattleFly(Player_.PlayerIndex);
    }
    void _LandCallback(CEnginePlayer Player_)
    {
        _fBattleLand(Player_.PlayerIndex);
    }
    void _FlapCallback(CEnginePlayer Player_)
    {
        _fBattleFlap(Player_.PlayerIndex);
    }
    void _PumpCallback(CEnginePlayer Player_)
    {
        _fBattlePump(Player_.PlayerIndex);
    }
    void _PumpDoneCallback(CEnginePlayer Player_)
    {
        _fBattlePumpDone(Player_.PlayerIndex);
    }
    void _ParachuteOnCallback(CEnginePlayer Player_, bool On_)
    {
        _fBattleParachuteOn(Player_.PlayerIndex, On_);
    }
    void _BounceCallback(CEnginePlayer Player_)
    {
        _fBattleBounce(Player_.PlayerIndex);
    }
    void _HitCallback(CEnginePlayer Attacker_, CEnginePlayer Target_)
    {
        Int32 AddedPoint = 0;

        if (Target_.IsAlive()) // Hit Balloon
        {
            ++BattleRecord.TotalBalloonHitCount;

            if (BattleRecord.TotalBalloonHitCount == 1)
                AddedPoint = CGlobal.MetaData.ConfigMeta.FirstBalloonHitPoint;
            else
                AddedPoint = CGlobal.MetaData.ConfigMeta.BalloonHitPoint;
        }
        else // Kill
        {
            ++BattleRecord.TotalKillCount;
            AddedPoint = CGlobal.MetaData.ConfigMeta.ParachuteHitPoint;
        }

        _fBattleHit(Attacker_.PlayerIndex, Target_.PlayerIndex, AddedPoint);
    }
    void _RegenCallback(CEnginePlayer Player_)
    {
        _fBattleRegen(Player_.PlayerIndex, Player_.Character);
    }
    public CPhysicsEngine(CEngineGameMode GameMode_, FBattleMove fBattleMove_, FBattleFace fBattleFace_, FBattleFly fBattleFly_, FBattleLand fBattleLand_, FBattleFlap fBattleFlap_, FBattlePump fBattlePump_, FBattlePumpDone fBattlePumpDone_, FBattleParachuteOn fBattleParachuteOn_, FBattleBounce fBattleBounce_, FBattleHit fBattleHit_, FBattleRegen fBattleRegen_,
        Int64 CurTick_, SBattleRecord BattleRecord_, SPoint PropPosition_, List<SStructure> Structures_, List<SStructureMove> StructureMoves_, List<SPoint> PlayerPoses_,
        List<SCharacterClientMeta> Metas_, List<SByte> TeamIndices_, List<SCharacterNet> Characters_,
        List<SStructMovePosition> StructMovePositions_)
    {
        _GameMode = GameMode_;
        _fBattleMove = fBattleMove_;
        _fBattleFace = fBattleFace_;
        _fBattleFly = fBattleFly_;
        _fBattleLand = fBattleLand_;
        _fBattleFlap = fBattleFlap_;
        _fBattlePump = fBattlePump_;
        _fBattlePumpDone = fBattlePumpDone_;
        _fBattleParachuteOn = fBattleParachuteOn_;
        _fBattleBounce = fBattleBounce_;
        _fBattleHit = fBattleHit_;
        _fBattleRegen = fBattleRegen_;

        BattleRecord = BattleRecord_;

        // Structure /////////////////////////////////
        var StructureColliders = new List<CCollider2D>();
        foreach (var s in Structures_)
            StructureColliders.Add(new CRectCollider2D(s.LocalPosition, CEngineGlobal.c_StructureNumber, s));

        _RootObject = new CObject2D(PropPosition_);

        // Moving Structures //////////////////////////
        for (Int32 i = 0; i < StructureMoves_.Count; ++i)
        {
            var s = StructureMoves_[i];
            var StructMovePosition = StructMovePositions_[i];

            var Colliders = new List<CCollider2D>();
            foreach (var c in s.Colliders)
                Colliders.Add(new CRectCollider2D(new SPoint(), CEngineGlobal.c_StructureNumber, c));

            var ShuttleObject = new CShuttleObject2D(
                StructMovePosition.LocalPosition,
                new CCollectionCollider2D(new SPoint(), CEngineGlobal.c_ContainerNumber, Colliders),
                s.BeginPos,
                s.EndPos,
                s.Velocity,
                s.Delay,
                StructMovePosition);
            ShuttleObject.SetParent(_RootObject);
            _MovingStructures.Add(ShuttleObject);
        }

        // Players ////////////////////////////
        _PlayerPoses = PlayerPoses_;

        var Players = new List<CPlayerObject2D>();
        _EnginePlayers = new CEnginePlayer[Metas_.Count];
        for (Int32 i = 0; i < Metas_.Count; ++i)
        {
            var Meta = Metas_[i];
            var TeamIndex = TeamIndices_[i];
            var Character = Characters_[i];

            var PlayerColliders = new List<CCollider2D>();

            var Body = new CRectCollider2D(new SPoint(), CEngineGlobal.c_BodyNumber, CEngineGlobal.GetPlayerRect());
            PlayerColliders.Add(Body);

            var Balloon = new CRectCollider2D(new SPoint(), CEngineGlobal.c_BalloonNumber, CEngineGlobal.GetBalloonRect(Character.BalloonCount));
            PlayerColliders.Add(Balloon);

            var Parachute = new CRectCollider2D(new SPoint(), CEngineGlobal.c_ParachuteNumber, CEngineGlobal.GetParachuteRect(Character.ParachuteInfo.Scale));
            PlayerColliders.Add(Parachute);

            var InitialPos = _PlayerPoses[i];

            _EnginePlayers[i] = new CEnginePlayer(
                _MoveCallback, _FaceCallback, _FlyCallback, _LandCallback, _FlapCallback, _PumpCallback, _PumpDoneCallback, _ParachuteOnCallback, _BounceCallback, _HitCallback, _RegenCallback,
                i, InitialPos, Meta, TeamIndex, Character, new CCollectionCollider2D(new SPoint(), CEngineGlobal.c_ContainerNumber, PlayerColliders),
                Body, Balloon, Parachute);

            Players.Add(_EnginePlayers[i]);
        }

        // Engine ////////////////////////////
        _Engine = new CClientEngine(
            global.c_NetworkTickSync,
            global.c_NetworkTickBuffer,
            CurTick_,
            global.c_ContactOffset,
            global.c_FPS,
            new CCollectionCollider2D(PropPosition_, CEngineGlobal.c_ContainerNumber, StructureColliders),
            _MovingStructures,
            Players);

        _Engine.OnFixedUpdate = _FixedUpdate;
    }
    public Int64 Tick
    {
        get
        {
            return _Engine.Tick;
        }
    }
    public void Sync(Int64 Tick_)
    {
        _Engine.Sync(new CMessage(Tick_));
    }
    public void Touch(Int64 Tick_, Int32 PlayerIndex_, SByte Dir_)
    {
        _Engine.Sync(new CMessageTouch(Tick_, _EnginePlayers[PlayerIndex_].Touch, Dir_));
    }
    public void Push(Int64 Tick_, Int32 PlayerIndex_)
    {
        _Engine.Sync(new CMessagePush(Tick_, _EnginePlayers[PlayerIndex_].Push));
    }
    public void Link(Int64 Tick_, Int32 PlayerIndex_)
    {
        _Engine.Sync(new CMessageLink(Tick_, _EnginePlayers[PlayerIndex_].Link));
    }
    public void UnLink(Int64 Tick_, Int32 PlayerIndex_)
    {
        _Engine.Sync(new CMessageUnLink(Tick_, _EnginePlayers[PlayerIndex_].UnLink));
    }
    public void Update()
    {
        _Engine.Update();
    }
    void _FixedUpdate(Int64 Tick_)
    {
        foreach (var i in _EnginePlayers)
        {
            i.CheckRegen(Tick_);
            _GameMode.FixPos(i.LocalPosition);
        }
    }
    public CEnginePlayer GetEnginePlayer(Int32 Index_)
    {
        return _EnginePlayers[Index_];
    }
    public CMovingObject2D GetMovingStructures(Int32 Index_)
    {
        return _Engine.MovingStructures[Index_];
    }
}
