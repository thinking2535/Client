using rso.core;
using rso.physics;
using rso.unity;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CSceneBattleSingle : CSceneBase
{
    enum EGameStep
    {
        Ready,
        Start,
        Play,
        End,
        Result,
        Exit
    }
    enum EDirection
    {
        Right,
        Left,
        Bottom,
        Diagonal
    }
    BattleSingleScene _BattleSingleScene = null;
    SingleModeUI _GameUI = null;
    GameObject _ParticleParent = null;
    GameObject _CharacterParent = null;
    SinglePlayObjectPool _SingleOnjectPool = null;
    CBattlePlayerSingle _Me = null;
    CPadSimulator _Pad = null;

    private EGameStep _GameStep = EGameStep.Ready;

    private float _PosMinY = -0.9f;
    private float _PosMaxY = 0.9f;
    private float _PosOutMinY = -1.5f;
    private float _PosOutMaxY = 1.5f;
    private float _PosMinX = -1.624f;
    private float _PosMaxX = 1.624f;
    private float _PosOutMinX = -2.224f;
    private float _PosOutMaxX = 2.224f;
    private float _PosWaveGoldMinX = -1.3f;
    private float _PosWaveGoldMaxX = 1.3f;
    private float _PosWaveGoldMinY = -0.5f;
    private float _PosWaveGoldMaxY = 0.6f;

    private float _DeltaTimeAdd = 0.0f;

    TimePoint _BeginTime;
    TimePoint _DelayTime;

    CSeedValue _WaveCount = new CSeedValue(0);
    CSeedValue _GoldCount = new CSeedValue(0);
    CSeedValue _TimeCount = new CSeedValue(0);
    CSeedValue _BestTimeCount = new CSeedValue(0);

    public bool IsGod = false;
    private Int32 _StartStage = 0;

    Int32 _MaxStageGoldCount = 0;

    //벨런스 관련 변수
    Int32 _WaveTrapCount = 0;
    Int32 _WaveMaxTrapCount = 0;
    Int32 _OnceMaxTrapCount = 0;
    Int32 _WaveItemCount = 0;
    Int32 _TotalItemCount = 0;

    //벨런스 관련 상수
    SSingleBalance _BalanceMeta = null;
    Int32[] _DirectionPercentMaxList = { 35, 70, 90, 100 };
    void _Touched(CInputTouch.EState State_, Vector2 Pos_, Int32 Dir_) // Dir_(2 Directions) : 0(Left) , 1(Right)
    {
        if (State_ == CInputTouch.EState.Down)
        {
            _GameUI.SetJoyPadVisible(false);
            return;
        }

        if (State_ == CInputTouch.EState.Move)
        {
            sbyte Dir = Dir_ == 0 ? (sbyte)-1 : (sbyte)1;

            if (_Me.SinglePlayer.Character.Dir != Dir)
                _Me.LeftRight(Dir);
        }
        else
        {
            if (_Me.SinglePlayer.Character.Dir != 0)
                _Me.Center();
            _GameUI.SetJoyPadVisible(true);
        }
    }
    void _Pushed(CInputTouch.EState State_)
    {
        if (State_ != CInputTouch.EState.Down)
            return;

        if (_Me.BalloonCount > 0)
            _Me.Flap();
    }
    public CSceneBattleSingle() :
        //base(string.Format("Prefabs/Maps/Single/{0}", CGlobal.MetaData.MapNames.SingleMaps[0].PrefabName), Vector3.zero, true)
        base("Prefabs/Maps/Single/SingleModeDodge", Vector3.zero, true)
    {
        var Range = Screen.width * 0.02f;
        _Pad = new CPadSimulator(_Touched, _Pushed, Range, Range);
    }
    public override void Enter()
    {
        _BattleSingleScene = _Object.GetComponent<BattleSingleScene>();
        _ParticleParent = _BattleSingleScene.ParticleParent;
        _GameUI = _BattleSingleScene.GameUI;
        _CharacterParent = _BattleSingleScene.CharacterParent;
        _SingleOnjectPool = _BattleSingleScene.SingleOnjectPool;
        IsGod = _BattleSingleScene.IsGod;
        _StartStage = _BattleSingleScene.StartStage;

        _BalanceMeta = CGlobal.MetaData.SingleBalanceMeta;

        Int32[] DirPercent = { _BalanceMeta.Right,
                             _BalanceMeta.Left,
                             _BalanceMeta.Bottom,
                             _BalanceMeta.Diagonal };
        Int32 MaxPercent = 0;
        for(Int32 i = 0; i < DirPercent.Length; ++i)
        {
            MaxPercent += DirPercent[i];
            _DirectionPercentMaxList[i] = MaxPercent;
        }

        _WaveCount.Set(_StartStage);
        _TimeCount.Set(0);
        _GoldCount.Set(0);
        _BestTimeCount.Set(CGlobal.LoginNetSc.User.SingleSecondBest);

        _GameUI.Init(_WaveCount.Get(), _TimeCount.Get(), _GoldCount.Get(), _BestTimeCount.Get(), this);
        _GameUI.ShowGameStart();

        var Now = CGlobal.GetServerTimePoint();

        var Obj = new GameObject(CGlobal.NickName);
        Obj.transform.SetParent(_CharacterParent.transform);
        Obj.transform.localPosition = Vector3.zero;

        var Character = new SSingleCharacter(new SSingleCharacterMove(), false, 0, 2, 0, 100, 0, new rso.physics.SPoint(0.0f, global.c_Gravity));
        var Player = new SSinglePlayer(CGlobal.UID, CGlobal.NickName, CGlobal.LoginNetSc.User.CountryCode, 0, CGlobal.LoginNetSc.User.SelectedCharCode, Character, Now);

        _Me = Obj.AddComponent<CBattlePlayerSingle>();
        _Me.Init(CGlobal.MetaData.Chars[CGlobal.LoginNetSc.User.SelectedCharCode], Player, Character, Now, _ParticleParent, _BattleSingleScene.Camera, this);

        _BattleSingleScene.Camera.orthographicSize = CGlobal.OrthographicSize;

        _BeginTime = CGlobal.GetServerTimePoint();
        _SingleOnjectPool.Init(10);

        AnalyticsManager.AddPlayDodgeCount();
    }
    public override void Dispose()
    {
    }
    public override bool Update()
    {
        if (_Exit)
        {
            return false;
        }
        if(_GameStep == EGameStep.Ready)
        {
            Int32 time = Mathf.CeilToInt((float)(CGlobal.GetServerTimePoint() - _BeginTime).TotalSeconds);
            if (time > 2)
            {
                _BeginTime = CGlobal.GetServerTimePoint();
                _GameStep = EGameStep.Start;
            }
        }
        else if(_GameStep == EGameStep.Start)
        {
            GameStart();
        }
        else if (_GameStep == EGameStep.Play)
        {
            _Pad.Update(); // 전투가 시작되지 않았다면 입력 받지 않도록
            _TimeCount.Set(Mathf.CeilToInt((float)(CGlobal.GetServerTimePoint() - _BeginTime).TotalSeconds));
            _GameUI.SetTimeCount(_TimeCount.Get());
            if(_BestTimeCount.Get() < _TimeCount.Get())
            {
                _BestTimeCount.Set(_TimeCount.Get());
                _GameUI.SetBestTimeCount(_BestTimeCount.Get());
            }
            _GameUI.SetScoreCount(_WaveCount.Get(),_TimeCount.Get(),_GoldCount.Get());
            if(_WaveTrapCount < _WaveMaxTrapCount)
            {
                _DeltaTimeAdd += Time.deltaTime;
                if (_DeltaTimeAdd > _BalanceMeta.ShotDelay)
                {
                    _DeltaTimeAdd -= _BalanceMeta.ShotDelay;
                    TrapActive(_OnceMaxTrapCount);

                    if (_WaveTrapCount % 2 == 0)
                    {
                        if (_WaveItemCount < _MaxStageGoldCount)
                            WaveItemActive();
                    }
                }
            }
            else
            {
                _WaveCount.Add(1);
                NextWave();
            }
        }
        else if(_GameStep == EGameStep.End)
        {
            Int32 time = Mathf.CeilToInt((float)(CGlobal.GetServerTimePoint() - _BeginTime).TotalSeconds);
            if (time > 2)
            {
                _BeginTime = CGlobal.GetServerTimePoint();
                _GameStep = EGameStep.Result;
                var Gold = _BalanceMeta.InitGold + _BalanceMeta.AddGold * ((_WaveCount.Get() - 1) / _BalanceMeta.WaveCountGold);
                _GameUI.ShowResult(_WaveCount.Get(), _TimeCount.Get(), Gold, _GoldCount.Get());
                if (CGlobal.SystemPopup.gameObject.activeSelf)
                    CGlobal.SystemPopup.OnClickCancel();
            }
        }

        if (rso.unity.CBase.BackPushed())
        {
            if (CGlobal.ADManager.isShowing())
                return true;

            if (CGlobal.SystemPopup.gameObject.activeSelf)
            {
                CGlobal.SystemPopup.OnClickCancel();
                return true;
            }
            if (_GameStep == EGameStep.Result)
            {
                _GameUI.SkipResult();
                return true;
            }
            if (_GameStep == EGameStep.Exit)
            {
                _GameUI.ResultOKClick();
                return false;
            }
            _GameUI.ExitClick();
        }

        return true;
    }
    private void TrapActive(Int32 Count_)
    {
        for(Int32 i = 0; i < Count_; ++i)
        {
            var Direction = SingleObjectDirection();
            _SingleOnjectPool.UseObject(SinglePlayObject.EItemType.Trap, Direction.Item1, Direction.Item2, SpeedRandom(), Direction.Item3);
        }
        _WaveTrapCount++;
    }
    private void CoinActive(Int32 Count_)
    {
        for (Int32 i = 0; i < Count_; ++i)
        {
            var Direction = SingleObjectDirection();
            _SingleOnjectPool.UseObject(SinglePlayObject.EItemType.Coin, Direction.Item1, Direction.Item2, _BalanceMeta.BonusCoinSpeed, 0.0f);
        }
        _WaveTrapCount++;
    }
    private void WaveItemActive()
    {
        var Pos = new Vector3(Random.Range(_PosWaveGoldMinX, _PosWaveGoldMaxX), Random.Range(_PosWaveGoldMinY, _PosWaveGoldMaxY), 0.0f);
        SinglePlayObject.EItemType ItemTag = CGlobal.MetaData.SingleArrowItemPattern[_TotalItemCount % CGlobal.MetaData.SingleArrowItemPattern.Count];

        if(ItemTag == SinglePlayObject.EItemType.Item_Shield ||
            ItemTag == SinglePlayObject.EItemType.Item_Stamina)
        {
            bool IsItem = _SingleOnjectPool.GetUseObjectCheck(ItemTag);
            if (IsItem)
                ItemTag = SinglePlayObject.EItemType.Coin;
        }
        _SingleOnjectPool.UseObject(ItemTag, Pos);

        _WaveItemCount++;
        _TotalItemCount++;
    }
    private void DiaActive(Int32 Count_)
    {
        for (Int32 i = 0; i < Count_; ++i)
        {
            var Direction = SingleObjectDirection();
            _SingleOnjectPool.UseObject(SinglePlayObject.EItemType.Dia, Direction.Item1, Direction.Item2, _BalanceMeta.BonusCoinSpeed, 0.0f);
        }
    }
    Tuple<Vector3,Vector3,float> SingleObjectDirection()
    {
        Vector3 StartPos = Vector3.zero;
        Vector3 EndPos = Vector3.zero;
        float Angle = 0.0f;

        EDirection Dir = RendomDir();
        switch (Dir)
        {
            case EDirection.Right:
                StartPos = new Vector3(_PosOutMinX, Random.Range(_PosMinY, _PosMaxY), 0.0f);
                EndPos = new Vector3(_PosOutMaxX, StartPos.y, 0.0f);
                Angle = 180.0f;
                break;
            case EDirection.Left:
                StartPos = new Vector3(_PosOutMaxX, Random.Range(_PosMinY, _PosMaxY), 0.0f);
                EndPos = new Vector3(_PosOutMinX, StartPos.y, 0.0f);
                Angle = 0.0f;
                break;
            case EDirection.Bottom:
                StartPos = new Vector3(Random.Range(_PosMinX, _PosMaxX), _PosOutMaxY, 0.0f);
                EndPos = new Vector3(StartPos.x, _PosOutMinY, 0.0f);
                Angle = 90.0f;
                break;
            case EDirection.Diagonal:
                float PosX = Random.Range(_PosMinX, _PosMaxX);
                StartPos = new Vector3(PosX, _PosOutMaxY, 0.0f);
                EndPos = new Vector3(PosX == 0.0f ? PosX : PosX * -1, _PosOutMinY, 0.0f);
                Vector3 v3 = EndPos - StartPos;
                Angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg + 180;
                break;
        }
        return new Tuple<Vector3, Vector3, float>(StartPos,EndPos,Angle);
    }
    public void GetCoin()
    {
        _GoldCount.Add(1);
        _GameUI.SetGoldCount(_GoldCount.Get());
    }
    public void GetGoldBar()
    {
        _GoldCount.Add(5);
        _GameUI.SetGoldCount(_GoldCount.Get());
    }
    public void GameOver()
    {
        _GameStep = EGameStep.End;
        CGlobal.MusicStop();
        _SingleOnjectPool.AllUnuseObject();
        _BeginTime = CGlobal.GetServerTimePoint();
    }
    public void ResultViewEnd()
    {
        _GameStep = EGameStep.Exit;
    }
    public void GameStart()
    {
        _DelayTime = CGlobal.GetServerTimePoint();
        _DeltaTimeAdd = 0.0f;
        _WaveTrapCount = 0;
        BalanceTrap();
        _GameStep = EGameStep.Play;
        _Me.GameStart();
        CGlobal.MusicStop();
        CGlobal.MusicPlayDodge();
        _GameUI.HideGameStart();
    }
    public void NextWave()
    {
        _DeltaTimeAdd = 0.0f;
        _WaveTrapCount = 0;
        _WaveItemCount = 0;
        BalanceTrap();
        TrapActive(_OnceMaxTrapCount);
    }
    float SpeedRandom()
    {
        float Min = ((float)_WaveCount.Get()) / _BalanceMeta.SpeedFix + _BalanceMeta.MinSpeed;
        float Max = ((float)_WaveCount.Get()) / _BalanceMeta.SpeedFix + _BalanceMeta.MaxSpeed;
        return Random.Range(Min, Max);
    }
    private void BalanceTrap()
    {
        Int32 MaxBalance = _WaveCount.Get();
        if (MaxBalance > 39) MaxBalance = 39;
        _WaveMaxTrapCount = _BalanceMeta.FixCount + (MaxBalance % _BalanceMeta.CountFix) + (MaxBalance / _BalanceMeta.CountFix) - (MaxBalance / _BalanceMeta.TypeFix);
        _OnceMaxTrapCount = _BalanceMeta.FixOnceCount + (MaxBalance / _BalanceMeta.CountFix) - (MaxBalance / _BalanceMeta.TypeFix);

        _MaxStageGoldCount = _BalanceMeta.StageGoldCount + ((MaxBalance - 1) / _BalanceMeta.StageGoldWave);
        if(_MaxStageGoldCount > _BalanceMeta.StageGoldCountMax) _MaxStageGoldCount = _BalanceMeta.StageGoldCountMax;
    }
    private EDirection RendomDir()
    {
        EDirection Dir = EDirection.Right;
        Int32 Max = (_WaveCount.Get() / _BalanceMeta.TypeFix) + 2;
        if (Max >= 4) Max = 4;

        Int32 MaxPercent = 0;
        for(Int32 i = 0; i < Max; ++i)
        {
            MaxPercent += _DirectionPercentMaxList[i] - MaxPercent;
        }
        Int32 Percent = Random.Range(0, MaxPercent);

        for(Int32 i = 0; i < Max; ++i)
        {
            if (Percent < _DirectionPercentMaxList[i])
            {
                Dir = (EDirection)i;
                break;
            }
        }

        return Dir;
    }
}
