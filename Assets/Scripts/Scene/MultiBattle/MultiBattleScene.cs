using bb;
using rso.Base;
using rso.core;
using rso.physics;
using rso.unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MultiBattleScene : NetworkBattleScene
{
    SMultiMap _MultiMap;
    protected SBattleType _BattleType;
    public GameUICanvas GameUI = null;
    internal GameObject PlayerDisconnectPopup = null;
    PlayerDisconnectPopup _PlayerDisconnectPopup;
    SMultiBattleBeginNetSc _Proto;
    Int64 _EndTick;
    Int32[] _TeamScores;
    List<CMultiBattlePlayer> _multiBattlePlayers = new List<CMultiBattlePlayer>();
    CMultiBattlePlayer _MyMultiBattlePlayer;
    DateTime? _EndTime;

    Func<Scene> _MultiBattleEndSceneCreator;
    public void init(SMultiBattleBeginNetSc Proto_)
    {
        base.init(Proto_.Tick, CGlobal.MetaData.GetMultiMap(Proto_));
        _MultiMap = (SMultiMap)_Map;
        _BattleType = Proto_.BattleType;

        PlayerDisconnectPopup = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefabs/UI/popup/PlayerDisconnectPopup"), new Vector3(0, 0, 0), Quaternion.identity);
        var playerDisconnectPopupCanvas = PlayerDisconnectPopup.GetComponentInChildren<Canvas>();
        playerDisconnectPopupCanvas.worldCamera = camera;
        playerDisconnectPopupCanvas.planeDistance = BaseScene.childPlaneDistance;

        PlayerDisconnectPopup.SetActive(false);

        // Structure /////////////////////////////////
        foreach (var s in _MultiMap.Structures)
            _clientEngine.AddObject(new CRectCollider2D(s, CEngineGlobal.c_StructureNumber, s.RectCollider2D, _RootObject));

        // Moving Structures //////////////////////////
        for (Int32 i = 0; i < _MultiMap.StructureMoves.Count; ++i)
        {
            var s = _MultiMap.StructureMoves[i];
            var StructMovePosition = Proto_.StructMovePositions[i];

            var Colliders = new List<CCollider2D>();
            foreach (var c in s.Colliders)
                Colliders.Add(new CRectCollider2D(CPhysics.ZeroTransform, CEngineGlobal.c_StructureNumber, c));

            var ShuttleObject = new CShuttleObject2D(
                CPhysics.GetDefaultTransform(StructMovePosition.LocalPosition),
                Colliders,
                s.BeginPos,
                s.EndPos,
                s.Velocity,
                s.Delay,
                StructMovePosition);

            ShuttleObject.SetParent(_RootObject);
            _clientEngine.AddMovingObject(ShuttleObject);
        }

        _Proto = Proto_;
        _EndTick = Proto_.EndTick;
        _PlayerDisconnectPopup = PlayerDisconnectPopup.GetComponent<PlayerDisconnectPopup>();


        SBattlePlayer myBattlePlayer = null;
        for (Int32 i = 0; i < _Proto.Players.Count; ++i)
        {
            var Player = _Proto.Players[i];

            if (Player.UID != CGlobal.UID)
                continue;

            myBattlePlayer = Player;
        }

        // Players ////////////////////////////
        var PlayerPoses = CGlobal.MetaData.GetSPlayerPoses(_Proto);
        for (Int32 i = 0; i < _Proto.Players.Count; ++i)
        {
            var Player = _Proto.Players[i];
            bool IsMe = Player.UID == CGlobal.UID;
            var Character = _Proto.Characters[i];

            var Obj = new GameObject(Player.Nick);
            Obj.transform.SetParent(_Prop);

            var MultiBattlePlayer = Obj.AddComponent<CMultiBattlePlayer>();

            MultiBattlePlayer.Init(
                _HitCallback,
                i,
                PlayerPoses[i],
                CGlobal.MetaData.Characters[Player.CharCode],
                Character,
                getTeamMaterial(Player.TeamIndex, myBattlePlayer.TeamIndex),
                _Engine.Tick,
                Player,
                IsMe,
                _Prop,
                camera,
                _Proto.BattleInfos[i]);

            Obj.transform.position = MultiBattlePlayer.PlayerObject.Position.ToVector2();
            MultiBattlePlayer.PlayerObject.SetParent(_RootObject);

            if (IsMe)
                _MyMultiBattlePlayer = MultiBattlePlayer;

            _multiBattlePlayers.Add(MultiBattlePlayer);
            _AddBattlePlayer(MultiBattlePlayer);

            // rso todo Player.IsLinked 가 false 이면 와이파이 표시
        }

        // For Setting EngineUnityStructure's CurPos
        {
            var e = _clientEngine.MovingObjects.GetEnumerator();

            for (Int32 i = 0; i < _Prop.childCount; ++i)
            {
                var tf = _Prop.GetChild(i);

                var us = tf.gameObject.GetComponentInChildren<UnityMovingStructure>();
                if (us == null)
                    continue;

                if (!e.MoveNext())
                    break;

                us.EngineObject = e.Current;
            }
        }
        /////////////////////////////////////////////////////


        _TeamScores = new Int32[_Proto.BattleType.TeamCount];

        for (Int32 i = 0; i < _Proto.Players.Count; ++i)
            _TeamScores[_Proto.Players[i].TeamIndex] += _Proto.BattleInfos[i].Point;

        GameUI.Init(_Proto.BattleType, myBattlePlayer.TeamIndex, _TeamScores.Length);
        if (_TeamScores.Length > 2)
        {
            for (Int32 i = 0; i < _TeamScores.Length; ++i)
                GameUI.SetPoint(_TeamScores[i], i);
        }
        else
        {
            for (Int32 i = 0; i < _TeamScores.Length; ++i)
                GameUI.SetPoint(_MyMultiBattlePlayer.BattlePlayer.TeamIndex == i, _TeamScores[i]);
        }
        GameUI.ShowGameStart();
        //////////////////////////////////////////////////////

        if (_EndTick > 0)
            _StartBattle();

        _UpdatePlayerDisconnectPopup();
    }
    protected override void Update()
    {
        base.Update();

        if (_MultiBattleEndSceneCreator != null)
        {
            if (_EndTime == null)
            {
                _EndTime = DateTime.Now + TimeSpan.FromSeconds(2);
                GameUI.ShowGameEnd();
            }
            else if (DateTime.Now >= _EndTime.Value)
            {
                CGlobal.sceneController.set(_MultiBattleEndSceneCreator);
                return;
            }
        }

        _RefreshTime();
    }
    protected override void OnDestroy()
    {
        GameObject.DestroyImmediate(PlayerDisconnectPopup);

        base.OnDestroy();
    }
    protected override CBattlePlayer _getMyBattlePlayer()
    {
        return _MyMultiBattlePlayer;
    }
    public void SetMultiBattleEndScene(SMultiBattleEndNetSc Proto_)
    {
        CGlobal.LoginNetSc.User.eloPoint = Proto_.eloPoint;
        var oldPoint = CGlobal.LoginNetSc.User.Point;
        CGlobal.LoginNetSc.User.setPoint(Proto_.point);
        var addedPoint = CGlobal.LoginNetSc.User.Point - oldPoint;

        if (CGlobal.LoginNetSc.User.BattlePointBest < Proto_.battlePoint)
            CGlobal.LoginNetSc.User.BattlePointBest = Proto_.battlePoint;

        bool DoesMyTeamWin = Proto_.myTeamRanking == 0;

        if (_BattleType.IsOnoOnOneBattle())
        {
            if (DoesMyTeamWin)
                ++CGlobal.LoginNetSc.User.WinCountSolo;
            else
                ++CGlobal.LoginNetSc.User.LoseCountSolo;
        }
        if (_BattleType.IsMultiBattle())
        {
            if (DoesMyTeamWin)
                ++CGlobal.LoginNetSc.User.WinCountMulti;
            else
                ++CGlobal.LoginNetSc.User.LoseCountMulti;
        }

        CGlobal.LoginNetSc.User.InvalidDisconnectInfo = Proto_.InvalidDisconnectInfo;

        var unitRewards = CGlobal.LoginNetSc.getUnitRewardsAndSetResources(Proto_.ResourcesLeft);
        CGlobal.LoginNetSc.User.Resources = Proto_.ResourcesLeft;

        _MultiBattleEndSceneCreator = () =>
        {
            var scene = SceneController.create(GlobalVariable.main.multiBattleEndScenePrefab);
            scene.init(Proto_, _Proto, _BattleType, _multiBattlePlayers, _MyMultiBattlePlayer, addedPoint, unitRewards);
            return scene;
        };
    }
    public void SetMultiBattleEndDrawScene(SMultiBattleEndDrawNetSc Proto_)
    {
        CGlobal.LoginNetSc.User.Resources = Proto_.ResourcesLeft;
        CGlobal.LoginNetSc.User.InvalidDisconnectInfo = Proto_.InvalidDisconnectInfo;
        _MultiBattleEndSceneCreator = () =>
        {
            var scene = SceneController.create(GlobalVariable.main.multiBattleEndDrawScenePrefab);
            scene.init(Proto_, _Proto, _BattleType, _multiBattlePlayers, _MyMultiBattlePlayer);
            return scene;
        };
    }
    public void SetMultiBattleEndInvalidScene(SMultiBattleEndInvalidNetSc Proto_)
    {
        CGlobal.LoginNetSc.User.InvalidDisconnectInfo = Proto_.InvalidDisconnectInfo;
        _MultiBattleEndSceneCreator = () =>
        {
            var scene = SceneController.create(GlobalVariable.main.multiBattleEndInvalidScenePrefab);
            scene.init(Proto_, _Proto, _BattleType, _multiBattlePlayers, _MyMultiBattlePlayer);
            return scene;
        };
    }
    void _HitCallback(Int32 AttackerIndex_, Int32 TargetIndex_)
    {
        var Attacker = _multiBattlePlayers[AttackerIndex_];
        var Target = _multiBattlePlayers[TargetIndex_];

        Int32 AddedPoint = 0;

        if (Target.IsAlive()) // Hit Balloon
        {
            ++_Proto.Record.TotalBalloonHitCount;

            if (_Proto.Record.TotalBalloonHitCount == 1)
                AddedPoint = CGlobal.MetaData.ConfigMeta.FirstBalloonHitPoint;
            else
                AddedPoint = CGlobal.MetaData.ConfigMeta.BalloonHitPoint;
        }
        else // Kill
        {
            ++_Proto.Record.TotalKillCount;
            AddedPoint = CGlobal.MetaData.ConfigMeta.ParachuteHitPoint;
        }

        if (Target.IsAlive())
            Attacker.AttackBalloon(AddedPoint);
        else
            Attacker.Kill(AddedPoint);

        Target.ShowPoint(AddedPoint);

        _TeamScores[Attacker.BattlePlayer.TeamIndex] += AddedPoint;
        if (_TeamScores.Length > 2)
            GameUI.SetPoint(_TeamScores[Attacker.BattlePlayer.TeamIndex], Attacker.PlayerIndex);
        else
            GameUI.SetPoint(Attacker.BattlePlayer.TeamIndex == _MyMultiBattlePlayer.BattlePlayer.TeamIndex, _TeamScores[Attacker.BattlePlayer.TeamIndex]);

        if (Target.IsAlive())
        {
            if (_Proto.Record.TotalBalloonHitCount == 1)
                GameUI.ShowFirstHit(Attacker.BattlePlayer.Nick, Attacker.BattlePlayer.TeamIndex == _MyMultiBattlePlayer.BattlePlayer.TeamIndex, Attacker.IsMe);
        }
        else
        {

            if (Attacker.IsMe)
            {
                CGlobal.LoginNetSc.User.KillTotal += 1;
                if (Attacker.Character.ChainKillCount > 1)
                {
                    CGlobal.LoginNetSc.User.ChainKillTotal += 1;
                }
            }
            _ShowChainKill(Attacker.Character.ChainKillCount, Attacker.BattlePlayer.Nick, Attacker.BattlePlayer.TeamIndex == _MyMultiBattlePlayer.BattlePlayer.TeamIndex, Attacker.IsMe);
        }
    }
    protected override void _fixedUpdate()
    {
        foreach (var i in _multiBattlePlayers)
            i.CheckRegen(_Engine.Tick);
    }
    void _ShowChainKill(Int32 Count_, string UserName_, bool SameTeam_, bool IsMe_)
    {
        GameUI.ShowChainKill(Count_, UserName_, SameTeam_, IsMe_);
    }
    public void EmotionCallback(Int32 PlayerIndex_, Int32 Code_)
    {
        _multiBattlePlayers[PlayerIndex_].SetEmotion(Code_);
    }
    void _RefreshTime()
    {
        var SecondsLeft = (_EndTick - _Engine.Tick) / 10000000;
        GameUI.SetTime(SecondsLeft);
    }
    void _StartBattle()
    {
        _clientEngine.Start();
        CGlobal.MusicPlayBattle();
        GameUI.HideGameStart();
    }
    public void StartBattle(Int64 EndTick_)
    {
        _EndTick = EndTick_;
        _StartBattle();
    }
    void _UpdatePlayerDisconnectPopup()
    {
        if (_Proto.DisconnectEndTimes.Count > 0)
        {
            var MinEndTimePoint = TimePoint.FromTicks(Int64.MaxValue);
            foreach (var p in _Proto.DisconnectEndTimes)
            {
                if (p.Value < MinEndTimePoint)
                    MinEndTimePoint = p.Value;
            }

            _PlayerDisconnectPopup.EndTimePoint = MinEndTimePoint;
            PlayerDisconnectPopup.SetActive(true);
        }
        else
        {
            PlayerDisconnectPopup.SetActive(false);
        }
    }
    public void Link(SMultiBattleLinkNetSc Proto_)
    {
        _Proto.DisconnectEndTimes.Remove(Proto_.PlayerIndex);
        _clientEngine.Sync(new CMessageLink(Proto_.Tick, _multiBattlePlayers[Proto_.PlayerIndex].Link));
        _UpdatePlayerDisconnectPopup();
    }
    public void UnLink(SMultiBattleUnLinkNetSc Proto_)
    {
        _Proto.DisconnectEndTimes.Add(Proto_.PlayerIndex, Proto_.DisconnectEndTime);
        _clientEngine.Sync(new CMessageUnLink(Proto_.Tick, _multiBattlePlayers[Proto_.PlayerIndex].UnLink));
        _UpdatePlayerDisconnectPopup();
    }
    protected TeamMaterial getTeamMaterial(SByte teamIndex, SByte myTeamIndex)
    {
        if (_BattleType.TeamCount == 2)
        {
            if (teamIndex == myTeamIndex)
                return _teamMaterials.Last();
            else
                return _teamMaterials.First();
        }
        else
        {
            return _teamMaterials[teamIndex];
        }
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
