using rso.core;
using rso.gameutil;
using rso.physics;
using rso.unity;
using bb;
using System;
using System.Collections.Generic;
using UnityEngine;
public class CSceneBattleMulti : CSceneBase
{
    CPhysicsEngine _Engine = null;
    BattleMultiScene _BattleMultiScene = null;
    CPadSimulator _Pad;
    SBattleBeginNetSc _Proto;
    CEngineGameMode _GameMode;
    TimePoint _EndTime;
    TimeSpan _EndDelay = TimeSpan.FromSeconds(2);
    Int32[] _TeamScores = null;
    CBattlePlayer[] _Players;
    CBattlePlayer _Me;
    GameUICanvas _GameUI;
    GameObject _ParticleParent;
    public SBattleBeginNetSc GetSBattleBeginNetSc()
    {
        return _Proto;
    }
    public CBattlePlayer[] GetPlayers()
    {
        return _Players;
    }
    public Int32 GetWinTeamIndex()
    {
        Int32 BigPoint = 0;
        Int32 MinPoint = Int32.MaxValue;
        Int32 BigPointIndex = -1;
        for (Int32 i = 0; i < _TeamScores.Length; ++i)
        {
            if (BigPoint < _TeamScores[i])
            {
                BigPoint = _TeamScores[i];
                BigPointIndex = i;
            }
            if (MinPoint > _TeamScores[i])
            {
                MinPoint = _TeamScores[i];
            }
        }
        if (MinPoint == BigPoint)
            BigPointIndex = -1;
        return BigPointIndex;
    }

    void _Touched(CInputTouch.EState State_, Vector2 Pos_, Int32 Dir_) // Dir_(2 Directions) : 0(Left) , 1(Right)
    {
        if (State_ == CInputTouch.EState.Down)
        {
            _GameUI.SetJoyPadVisible(false);
            return;
        }

        if (State_ == CInputTouch.EState.Move)
        {
            if (!_Me.IsAlive())
                return;

            CGlobal.NetControl.Send(new SBattleTouchNetCs(Dir_ == 0 ? (sbyte)-1 : (sbyte)1));
        }
        else
        {
            if (!_Me.IsAlive())
                return;

            CGlobal.NetControl.Send(new SBattleTouchNetCs(0));
            _GameUI.SetJoyPadVisible(true);
        }
    }
    void _Pushed(CInputTouch.EState State_)
    {
        if (State_ != CInputTouch.EState.Down)
            return;

        if (!_Me.IsAlive())
            return;

        CGlobal.NetControl.Send(new SBattlePushNetCs());
    }
    void _AddPoint(CBattlePlayer Player_, Int32 AddedPoint_)
    {
        _Players[Player_.PlayerIndex].BattlePlayer.Point += AddedPoint_;
        _TeamScores[Player_.BattlePlayer.TeamIndex] += AddedPoint_;
        if (_TeamScores.Length > 2)
            _GameUI.SetPoint(_TeamScores[Player_.BattlePlayer.TeamIndex], Player_.PlayerIndex);
        else
            _GameUI.SetPoint(Player_.BattlePlayer.TeamIndex == _Me.BattlePlayer.TeamIndex, _TeamScores[Player_.BattlePlayer.TeamIndex]);
    }
    void _ShowChainKill(Int32 Count_, string UserName_, bool SameTeam_, bool IsMe_)
    {
        _GameUI.ShowChainKill(Count_, UserName_, SameTeam_, IsMe_);
    }
    void _MoveCallback(Int32 PlayerIndex_, SByte Dir_)
    {
        if (Dir_ == 0)
            _Players[PlayerIndex_].Stop();
        else
            _Players[PlayerIndex_].Move(Dir_);
    }
    void _FaceCallback(Int32 PlayerIndex_, SByte Dir_)
    {
        _Players[PlayerIndex_].Face(Dir_);
    }
    void _FlyCallback(Int32 PlayerIndex_)
    {
        _Players[PlayerIndex_].Fly();
    }
    void _LandCallback(Int32 PlayerIndex_)
    {
        _Players[PlayerIndex_].Land();
    }
    void _FlapCallback(Int32 PlayerIndex_)
    {
        _Players[PlayerIndex_].Flap();
    }
    void _PumpCallback(Int32 PlayerIndex_)
    {
        _Players[PlayerIndex_].Pump();
    }
    void _PumpDoneCallback(Int32 PlayerIndex_)
    {
        _Players[PlayerIndex_].PumpDone();
        if (_Players[PlayerIndex_].IsMe())
            CGlobal.LoginNetSc.User.BlowBalloonTotal += 1;
    }
    void _ParachuteOnCallback(Int32 PlayerIndex_, bool On_)
    {
        _Players[PlayerIndex_].ParachuteOn(On_);
    }
    void _BounceCallback(Int32 PlayerIndex_)
    {
        _Players[PlayerIndex_].Bounce();
    }
    void _HitCallback(Int32 AttackerIndex_, Int32 TargetIndex_, Int32 AddedPoint_)
    {
        var Attacker = _Players[AttackerIndex_];
        var Target = _Players[TargetIndex_];

        Target.Hit(AddedPoint_);
        _AddPoint(Attacker, AddedPoint_);

        if (Target.IsAlive())
        {
            if (_Engine.BattleRecord.TotalBalloonHitCount == 1)
                _GameUI.ShowFirstHit(Attacker.BattlePlayer.Nick, Attacker.BattlePlayer.TeamIndex == _Me.BattlePlayer.TeamIndex, Attacker.IsMe());
        }
        else
        {

            if (Attacker.IsMe())
            {
                CGlobal.LoginNetSc.User.KillTotal += 1;
                if (Attacker.Character.ChainKillCount > 1)
                {
                    CGlobal.LoginNetSc.User.ChainKillTotal += 1;
                }
            }
            _ShowChainKill(Attacker.Character.ChainKillCount, Attacker.BattlePlayer.Nick, Attacker.BattlePlayer.TeamIndex == _Me.BattlePlayer.TeamIndex, Attacker.IsMe());
        }
    }
    void _RegenCallback(Int32 PlayerIndex_, SCharacter Character_)
    {
        _Players[PlayerIndex_].Regen(_Engine.Tick, Character_);
    }
    public void EmotionCallback(Int32 PlayerIndex_, Int32 Code_)
    {
        _Players[PlayerIndex_].SetEmotion(Code_);
    }
    public CSceneBattleMulti(SBattleBeginNetSc Proto_) :
        base(CGlobal.MetaData.GetMapPrefabName(Proto_), Vector3.zero, true)
    {
        var Range = Screen.width * 0.02f;
        _Pad = new CPadSimulator(_Touched, _Pushed, Range, Range);
        _Proto = Proto_;
        _GameMode = CEngineGlobal.GetGameModeMulti(Proto_.BattleType);
        _EndTime = Proto_.EndTime;

        var PlayerPoses = CGlobal.MetaData.GetSPlayerPoses(Proto_);
        var Metas = new List<SCharacterClientMeta>();
        var TeamIndices = new List<SByte>();
        var Characters = new List<SCharacterNet>();
        Int64 Tick = 0;

        for (Int32 i = 0; i < Proto_.Players.Count; ++i)
        {
            var Player = Proto_.Players[i];
            var Pos = PlayerPoses[i];
            var Meta = CGlobal.MetaData.Chars[Player.CharCode];
            var Character = Proto_.Characters[i];

            Metas.Add(CGlobal.MetaData.Chars[Player.CharCode]);
            TeamIndices.Add(Player.TeamIndex);
            Characters.Add(Character);
        }

        SBattleRecord BattleRecord;
        Int64 CurTick = 0;

        BattleRecord = Proto_.Record;
        CurTick = Proto_.Tick;

        var Map = CGlobal.MetaData.GetMap(Proto_);

        _Engine = new CPhysicsEngine(
            _GameMode, _MoveCallback, _FaceCallback, _FlyCallback, _LandCallback, _FlapCallback, _PumpCallback, _PumpDoneCallback, _ParachuteOnCallback, _BounceCallback, _HitCallback, _RegenCallback,
            CurTick, BattleRecord, Map.PropPosition, Map.Structures, Map.StructureMoves, CGlobal.MetaData.GetSPlayerPoses(Proto_), Metas, TeamIndices, Characters, Proto_.StructMovePositions);
    }
    public override void Enter()
    {
        _BattleMultiScene = _Object.GetComponent<BattleMultiScene>();

        // GameUI 세팅 ///////////////////////////////////////
        _GameUI = _BattleMultiScene.GameUI.GetComponent<GameUICanvas>();

        _ParticleParent = _BattleMultiScene.ParticleParent;
        // Player Pos 안보이도록 수정.
        var playerPos = _BattleMultiScene.GetComponentInChildren<PlayerPosSave>();
        playerPos.gameObject.SetActive(false);

        _Players = new CBattlePlayer[_Proto.Players.Count];

        var Now = CGlobal.GetServerTimePoint();
        sbyte MyTeamIndex = 0;

        Int32 MaxTeamIndex = 0;
        for (Int32 i = 0; i < _Players.Length; ++i)
        {
            var Player = _Proto.Players[i];
            if (Player.TeamIndex > MaxTeamIndex)
                MaxTeamIndex = Player.TeamIndex;
        }
        var BattleType = new SBattleTypeMeta(_Proto.BattleType, MaxTeamIndex > 1, false);



        // For Me
        for (Int32 i = 0; i < _Proto.Players.Count; ++i)
        {
            var Player = _Proto.Players[i];

            if (Player.UID != CGlobal.UID)
                continue;

            MyTeamIndex = Player.TeamIndex;

            var Meta = CGlobal.MetaData.Chars[Player.CharCode];
            var Obj = new GameObject(Player.Nick);
            Obj.transform.SetParent(_Object.transform);
            Obj.transform.position = _Engine.GetEnginePlayer(i).LocalPosition.ToVector2();

            _Me = _Players[i] = Obj.AddComponent<CBattlePlayer>();
            _Players[i].Init(_GameMode, _Engine.Tick, i, MyTeamIndex, Player, _Engine.GetEnginePlayer(i), _Me, Now, _ParticleParent, _BattleMultiScene.Camera);

            break;
        }

        // For Others
        for (Int32 i = 0; i < _Proto.Players.Count; ++i)
        {
            var Player = _Proto.Players[i];

            if (Player.UID == CGlobal.UID)
                continue;

            var Meta = CGlobal.MetaData.Chars[Player.CharCode];
            var Obj = new GameObject(Player.Nick);
            Obj.transform.SetParent(_Object.transform);
            Obj.transform.position = _Engine.GetEnginePlayer(i).LocalPosition.ToVector2();

            _Players[i] = Obj.AddComponent<CBattlePlayer>();
            _Players[i].Init(_GameMode, _Engine.Tick, i, MyTeamIndex, Player, _Engine.GetEnginePlayer(i), _Me, Now, _ParticleParent, _BattleMultiScene.Camera);
        }


        // For Setting EngineUnityStructure's CurPos
        {
            Int32 Index = 0;

            var Prop = _Object.transform.Find(CGlobal.c_PropName);
            Debug.Assert(Prop != null);

            for (Int32 i = 0; i < Prop.childCount; ++i)
            {
                var tf = Prop.GetChild(i);

                var us = tf.gameObject.GetComponentInChildren<EngineUnityStructure>();
                if (us == null)
                    continue;

                us.EngineObject = _Engine.GetMovingStructures(Index++);
            }
        }
        /////////////////////////////////////////////////////


        _TeamScores = new Int32[MaxTeamIndex + 1];

        for (Int32 i = 0; i < _Proto.Players.Count; ++i)
            _TeamScores[_Proto.Players[i].TeamIndex] += _Proto.Players[i].Point;

        _GameUI.Init(_Proto.BattleType, MyTeamIndex, _TeamScores.Length);
        if (_TeamScores.Length > 2)
        {
            for (Int32 i = 0; i < _TeamScores.Length; ++i)
                _GameUI.SetPoint(_TeamScores[i], i);
        }
        else
        {
            for (Int32 i = 0; i < _TeamScores.Length; ++i)
                _GameUI.SetPoint(_Me.BattlePlayer.TeamIndex == i, _TeamScores[i]);
        }
        _GameUI.ShowGameStart();
        //////////////////////////////////////////////////////

        _BattleMultiScene.Camera.orthographicSize = CGlobal.OrthographicSize;

        if (_EndTime.Ticks > 0)
            _Start();
    }
    public override void Dispose()
    {
    }
    void _RefreshTime(TimePoint ServerNow_)
    {
        Int32 time = Mathf.CeilToInt((float)(_EndTime - ServerNow_).TotalSeconds);
        _GameUI.SetTime(time);
    }
    void _RefreshTime()
    {
        _RefreshTime(CGlobal.GetServerTimePoint());
    }
    public override bool Update()
    {
        if (_Exit)
        {
            var ServerNow = CGlobal.GetServerTimePoint();
            if (ServerNow > _EndTime + _EndDelay)
            {
                return false;
            }

            _RefreshTime(ServerNow);
            _GameUI.ShowGameEnd();
            return true;
        }

        _Engine.Update();

        if (_EndTime.Ticks > 0)
            _Pad.Update(); // 전투가 시작되지 않았다면 입력 받지 않도록

        _BattleMultiScene.Camera.transform.position = new Vector3(_BattleMultiScene.Camera.transform.position.x, _GameMode.GetCameraY(_Me.GetY()), _BattleMultiScene.Camera.transform.position.z);
        _RefreshTime();

        if (rso.unity.CBase.BackPushed())
        {
            if (CGlobal.SystemPopup.gameObject.activeSelf)
            {
                CGlobal.SystemPopup.OnClickCancel();
                return true;
            }
            CGlobal.SystemPopup.ShowGameOut();
        }

        return true;
    }
    void _Start()
    {
        CGlobal.MusicPlayBattle();
        _GameUI.HideGameStart();
    }
    public void Start(TimePoint EndTime_)
    {
        _EndTime = EndTime_;
        _Start();
    }
    public void Sync(SBattleSyncNetSc Proto_)
    {
        _Engine.Sync(Proto_.Tick);
    }
    public void Touch(SBattleTouchNetSc Proto_)
    {
        _Engine.Touch(Proto_.Tick, Proto_.PlayerIndex, Proto_.Dir);
    }
    public void Push(SBattlePushNetSc Proto_)
    {
        _Engine.Push(Proto_.Tick, Proto_.PlayerIndex);
    }
    public void Link(SBattleLinkNetSc Proto_)
    {
        _Engine.Link(Proto_.Tick, Proto_.PlayerIndex);
    }
    public void UnLink(SBattleUnLinkNetSc Proto_)
    {
        _Engine.UnLink(Proto_.Tick, Proto_.PlayerIndex);
    }
}
