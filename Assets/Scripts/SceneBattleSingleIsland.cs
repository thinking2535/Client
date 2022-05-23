using rso.core;
using rso.unity;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneBattleSingleIsland : CSceneBase
{
    enum EGameStep
    {
        Tutorial,
        Ready,
        Start,
        Play,
        End,
        Result,
        Exit
    }


    BattleSingleIslandScene _BattleSingleIslandScene = null;
    SingleIslandModeUI _GameUI = null;
    GameObject _ParticleParent = null;
    GameObject _CharacterParent = null;
    SingleIslandObjectPool _SingleOnjectPool = null;
    RectTransform _BG_0 = null;
    RectTransform _BG_1 = null;
    GameObject _ParticleWaterFall = null;

    CBattlePlayerSingleIsland _Me = null;
    CPadSimulator _Pad = null;

    private EGameStep _GameStep = EGameStep.Tutorial;

    private float OutScreenIslandPosX = -2.1f;
    private float IslandPosYMin = -0.8f;
    private float IslandPosYMax = 0.4f;

    CSeedValue _IslandCount = new CSeedValue(0);
    CSeedValue _GoldCount = new CSeedValue(0);
    CSeedValue _ItemCount = new CSeedValue(0);
    CSeedValue _ViewIslandCount = new CSeedValue(0);
    CSeedValue _BestIslandCount = new CSeedValue(0);
    Int32 _ViewItemCount = 0;
    Int32 _ViewGoldCount = 0;
    Int32 _ViewSpikeCount = 0;

    public bool IsGod = false;
    private Int32 _StartIslandCount = 0;

    private bool ShowTutorial = false;

    TimePoint _DelayTime;
    float _SteminaStart = 0.0f;

    SSingleIslandBalance _Balance = null;

    List<SingleIslandObject.EItemType> _ItemPattern = new List<SingleIslandObject.EItemType>();

    void _Touched(CInputTouch.EState State_, Vector2 Pos_, Int32 Dir_) // Dir_(2 Directions) : 0(Left) , 1(Right)
    {
        //if(_Me.IsGround)
        //{
        //    if (State_ == CInputTouch.EState.Down)
        //    {
        //        _GameUI.SetJoyPadVisible(false);
        //        return;
        //    }

        //    if (State_ == CInputTouch.EState.Move)
        //    {
        //        sbyte Dir = Dir_ == 0 ? (sbyte)-1 : (sbyte)1;

        //        if (_Me.BattlePlayer.Character.Dir != Dir)
        //            _Me.LeftRight(Dir);
        //    }
        //    else
        //    {
        //        if (_Me.BattlePlayer.Character.Dir != 0)
        //            _Me.Center();
        //        _GameUI.SetJoyPadVisible(true);
        //    }
        //}
    }
    void _Pushed(CInputTouch.EState State_)
    {
        if (State_ != CInputTouch.EState.Down)
            return;

        if (_Me.BalloonCount > 0)
            _Me.Flap();
    }
    public CSceneBattleSingleIsland() :
        base("Prefabs/Maps/Single/SingleModeIsland", Vector3.zero, true)
    {
        var Range = Screen.width * 0.02f;
        _Pad = new CPadSimulator(_Touched, _Pushed, Range, Range);
    }
    public override void Enter()
    {
        _BattleSingleIslandScene = _Object.GetComponent<BattleSingleIslandScene>();
        _ParticleParent = _BattleSingleIslandScene.ParticleParent;
        _GameUI = _BattleSingleIslandScene.GameUI;
        _CharacterParent = _BattleSingleIslandScene.CharacterParent;
        _SingleOnjectPool = _BattleSingleIslandScene.SingleOnjectPool;
        IsGod = _BattleSingleIslandScene.IsGod;
        _StartIslandCount = _BattleSingleIslandScene.StartIslandCount;
        _BG_0 = _BattleSingleIslandScene.BG_0;
        _BG_1 = _BattleSingleIslandScene.BG_1;
        _ParticleWaterFall = _BattleSingleIslandScene.ParticleWaterFall;
        _ParticleWaterFall.SetActive(false);

        _Balance = CGlobal.MetaData.SingleIslandBalanceMeta;
        _ItemPattern = CGlobal.MetaData.SingleIslandItemPattern;

        _IslandCount.Set(_StartIslandCount);
        _ViewIslandCount.Set(_StartIslandCount);
        _GoldCount.Set(0);
        _ItemCount.Set(0);
        _BestIslandCount.Set(CGlobal.LoginNetSc.User.IslandPassedCountBest);
        _ViewItemCount = 0;
        _ViewGoldCount = 0;
        _ViewSpikeCount = 0;
        _SteminaStart = 0.0f;

        _GameUI.Init(_IslandCount.Get(), _BestIslandCount.Get(), this);
        _GameUI.SetJoyPadVisible(false);

        var Now = CGlobal.GetServerTimePoint();
        _DelayTime = Now;

        var Obj = new GameObject(CGlobal.NickName);
        Obj.transform.SetParent(_CharacterParent.transform);
        Obj.transform.localPosition = Vector3.zero;

        var Character = new SSingleCharacter(new SSingleCharacterMove(), false, 0, 2, 0, 100, 0, new rso.physics.SPoint(0.0f, global.c_Gravity));
        var Player = new SSinglePlayer(CGlobal.UID, CGlobal.NickName, CGlobal.LoginNetSc.User.CountryCode, 0, CGlobal.LoginNetSc.User.SelectedCharCode, Character, Now);

        _Me = Obj.AddComponent<CBattlePlayerSingleIsland>();
        _Me.Init(CGlobal.MetaData.Chars[CGlobal.LoginNetSc.User.SelectedCharCode], Player, Character, Now, _ParticleParent, _BattleSingleIslandScene.Camera, this);

        _BattleSingleIslandScene.Camera.orthographicSize = CGlobal.OrthographicSize;

        _SingleOnjectPool.Init(10);

        _SingleOnjectPool.UseObject(0, _Me.transform.localPosition, _ViewIslandCount.Get(), _Balance.IslandInitTime, false, 0, 0.0f);
        for (var i = 0; i < 9; ++i)
            IsLandActive();

        AnalyticsManager.AddPlayIslandCount();

        ShowTutorial = false;
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
        if (_GameStep == EGameStep.Tutorial)
        {
            if(!ShowTutorial)
            {
                ShowTutorial = true;
                _GameUI.ViewTutorial();
            }
        }
        else if (_GameStep == EGameStep.Ready)
        {
            Int32 time = Mathf.CeilToInt((float)(CGlobal.GetServerTimePoint() - _DelayTime).TotalSeconds);
            _SteminaStart += Time.deltaTime;
            float SteminaScale = _SteminaStart / 1.0f;
            if (SteminaScale > 1.0f)
                SteminaScale = 1.0f;
            _GameUI.SetStaminaBar(SteminaScale * _Me.GetStamina(), CGlobal.MetaData.SingleIslandBalanceMeta.StaminaMax);
            if (time > 2)
            {
                _GameUI.SetStaminaBar(_Me.GetStamina(), CGlobal.MetaData.SingleIslandBalanceMeta.StaminaMax);
                _DelayTime = CGlobal.GetServerTimePoint();
                _GameStep = EGameStep.Start;
            }
        }
        else if (_GameStep == EGameStep.Start)
        {
            GameStart();
        }
        else if (_GameStep == EGameStep.Play)
        {
            _Pad.Update(); // 전투가 시작되지 않았다면 입력 받지 않도록
            Int32 IslandCount = 0;
            if(!_Me.IsGround)
            {
                foreach (var i in _SingleOnjectPool.GetActiveList())
                {
                    i.transform.position -= new Vector3(CGlobal.MetaData.SingleIslandBalanceMeta.IslandVelocity * Time.deltaTime, 0.0f, 0.0f);
                    if (i.transform.localPosition.x <= OutScreenIslandPosX)
                    {
                        i.DisableObject();
                        IsLandActive();
                    }
                    if(_Me.transform.position.x >= i.transform.position.x)
                    {
                        if(IslandCount <= i.GetIslandCount())
                            IslandCount = i.GetIslandCount();
                    }
                }
                SetIsland(IslandCount);
                var MovePos = new Vector3((CGlobal.MetaData.SingleIslandBalanceMeta.IslandVelocity * Time.deltaTime) * 0.1f, 0.0f, 0.0f);
                _BG_0.position -= MovePos;
                _BG_1.position -= MovePos;
                if(_BG_0.localPosition.x < (_BG_0.sizeDelta.x * -1))
                {
                    _BG_0.localPosition = Vector3.zero;
                    _BG_1.localPosition = new Vector3(_BG_0.sizeDelta.x, 0.0f, 0.0f);
                }
            }
            //else
            //{
            //    if(_Me.transform.localPosition.x != 0.0f)
            //    {
            //        var Pos = new Vector3(_Me.transform.localPosition.x * -1.0f, 0.0f,0.0f);
            //        _Me.transform.localPosition = Vector3.MoveTowards(_Me.transform.localPosition, new Vector3(0.0f, _Me.transform.localPosition.y, _Me.transform.localPosition.z), Balance.IslandVelocity * Time.deltaTime);
            //        foreach (var i in _SingleOnjectPool.GetActiveList())
            //        {
            //            i.transform.position = Vector3.MoveTowards(i.transform.position, i.transform.position+Pos, Balance.IslandVelocity * Time.deltaTime);
            //        }
            //    }
            //}
        }
        else if (_GameStep == EGameStep.End)
        {
            Int32 time = Mathf.CeilToInt((float)(CGlobal.GetServerTimePoint() - _DelayTime).TotalSeconds);
            if (time > 2)
            {
                _DelayTime = CGlobal.GetServerTimePoint();
                _GameStep = EGameStep.Result;
                var Gold = _Balance.InitGold + _Balance.AddGold * ((_IslandCount.Get() - 1) / _Balance.WaveCountGold);
                _GameUI.ShowResult(_IslandCount.Get(), Gold, _GoldCount.Get());
                if (CGlobal.SystemPopup.gameObject.activeSelf)
                    CGlobal.SystemPopup.OnClickCancel();
            }
        }

        // Trace Camera
        //_BattleSingleIslandScene.Camera.transform.position = new Vector3(_Me.GetX(), _BattleSingleIslandScene.Camera.transform.position.y, _BattleSingleIslandScene.Camera.transform.position.z);
        /////////////////////

        if (rso.unity.CBase.BackPushed())
        {
            if (CGlobal.ADManager.isShowing())
                return true;

            if (CGlobal.SystemPopup.gameObject.activeSelf)
            {
                CGlobal.SystemPopup.OnClickCancel();
                return true;
            }
            if (_GameStep == EGameStep.Tutorial)
            {
                _GameUI.TutorialClose();
                return true;
            }
            if (_GameStep == EGameStep.Result)
            {
                _GameUI.SkipResult();
                return true;
            }
            else if (_GameStep == EGameStep.Exit)
            {
                _GameUI.ResultOKClick();
                return false;
            }
            _GameUI.ExitClick();
        }

        return true;
    }
    private void IsLandActive()
    {
        SingleIslandObject BeforeObj = _SingleOnjectPool.GetIsland(_ViewIslandCount.Get());
        Vector3 Pos = new Vector3(0.0f,0.0f,1.0f);
        Int32 BalanceCount = (_ViewIslandCount.Get() / _Balance.BalanceCount);
        var ranX = UnityEngine.Random.Range(_Balance.InitTermMin, (_Balance.InitTermMax)) + (BalanceCount * _Balance.AddTerm);
        Pos.x = BeforeObj.gameObject.transform.localPosition.x + ranX;

        var ExcludePosY = _Balance.ExcludeHeight + (BalanceCount * _Balance.AddExcludeHeight);
        var AddHeight = BalanceCount * _Balance.AddHeight;
        var ranY = UnityEngine.Random.Range(0.0f, (_Balance.InitHeight + AddHeight - ExcludePosY) / 2.0f) + ExcludePosY;

        var ran = UnityEngine.Random.Range(0, 100.0f);
        if (ran < _Balance.IslandDownPercent)
            ranY *= -1;
        Pos.y = BeforeObj.gameObject.transform.localPosition.y + ranY;

        if (Pos.y < IslandPosYMin) Pos.y = IslandPosYMin;
        else if (Pos.y > IslandPosYMax) Pos.y = IslandPosYMax;

        _ViewIslandCount.Add(1);
        var ViewCount = _ViewIslandCount.Get();
        bool IsSpike = false;
        SingleIslandObject.EItemType Type = SingleIslandObject.EItemType.Null;
        if (ViewCount % _Balance.StaminaTerm == 0)
        {
            Type = _ItemPattern[_ViewItemCount % _ItemPattern.Count];
            _ViewItemCount++;
        }
        else if (ViewCount % _Balance.CoinTerm == 0)
        {
            Type = SingleIslandObject.EItemType.Coin;
            if (_ViewGoldCount > 0)
            {
                var CheckItem = _ViewGoldCount % _Balance.GoldBarTerm;
                if(CheckItem == 0)
                {
                    Type = SingleIslandObject.EItemType.GoldBar;
                }
            }
            _ViewGoldCount++;
        }
        
        if (ViewCount % _Balance.SpikeIslandTerm == 0)
        {
            _ViewSpikeCount++;
            IsSpike = true;
        }

        var ranType = UnityEngine.Random.Range(BalanceCount, BalanceCount + _Balance.IslandTypeRange);
        if (ranType >= BeforeObj.GetIslandTypeCount()) ranType = BeforeObj.GetIslandTypeCount() - 1;

        var LandDelay = _Balance.IslandInitTime - (BalanceCount * _Balance.IslandAddTime);
        if (LandDelay <= _Balance.IslandTimeMin) LandDelay = _Balance.IslandTimeMin;

        if (ViewCount % _Balance.CoinTerm == 0 && Type != SingleIslandObject.EItemType.Coin && Type != SingleIslandObject.EItemType.GoldBar)
        {
            float Term = UnityEngine.Random.Range(0.0f, ExcludePosY);
            var newPos = Pos - new Vector3(0.0f, 0.6f + Term, 0.0f);
            if(newPos.y < IslandPosYMin)
                newPos = Pos + new Vector3(0.0f, 0.6f + Term, 0.0f);

            IsSpike = true;
            _SingleOnjectPool.UseObject(ranType, Pos, 0, LandDelay, IsSpike, _ViewSpikeCount, _Balance.IslandStamina, Type);
            var CoinType = SingleIslandObject.EItemType.Coin;
            if (_ViewGoldCount > 0)
            {
                var CheckItem = _ViewGoldCount % _Balance.GoldBarTerm;
                if (CheckItem == 0)
                {
                    CoinType = SingleIslandObject.EItemType.GoldBar;
                }
            }
            _SingleOnjectPool.UseObject(ranType, newPos, _ViewIslandCount.Get(), LandDelay, !IsSpike, _ViewSpikeCount, _Balance.IslandStamina, CoinType);
            _ViewGoldCount++;
        }
        else
        {
            _SingleOnjectPool.UseObject(ranType, Pos, _ViewIslandCount.Get(), LandDelay, IsSpike, _ViewSpikeCount, _Balance.IslandStamina, Type);
        }
    }
    public void GetCoin()
    {
        _GoldCount.Add(1);
        _GameUI.SetGoldCount(_GoldCount.Get());
        _GameUI.SetScore(_IslandCount.Get(), _GoldCount.Get());
    }
    public void GetGoldBar()
    {
        _GoldCount.Add(_Balance.GoldBarCount);
        _GameUI.SetGoldCount(_GoldCount.Get());
        _GameUI.SetScore(_IslandCount.Get(), _GoldCount.Get());
    }
    public void SetIsland(Int32 Count_)
    {
        if(Count_ > _IslandCount.Get())
            _IslandCount.Set(Count_);
        _GameUI.SetIslandCount(_IslandCount.Get());
        _GameUI.SetScore(_IslandCount.Get(), _GoldCount.Get());
        if(_BestIslandCount.Get() < _IslandCount.Get())
        {
            _BestIslandCount.Set(_IslandCount.Get());
            _GameUI.SetBestIslandCount(_BestIslandCount.Get());
        }
    }
    public void GetItem()
    {
        _ItemCount.Add(1);
    }
    public void SetStaminaBar(float Stamina_, float MaxStamina_)
    {
        _GameUI.SetStaminaBar(Stamina_, MaxStamina_);
    }
    public void GameOver()
    {
        _GameStep = EGameStep.End;
        CGlobal.MusicStop();
        //_SingleOnjectPool.AllUnuseObject();
        _DelayTime = CGlobal.GetServerTimePoint();
    }
    public void GameStart()
    {
        _DelayTime = CGlobal.GetServerTimePoint();
        _Me.GameStart();
        CGlobal.MusicStop();
        CGlobal.MusicPlayIsland();
        _GameStep = EGameStep.Play;
        _GameUI.HideGameStart();
    }
    public void ResultViewEnd()
    {
        _GameStep = EGameStep.Exit;
    }
    public void FallEffect(Vector3 Pos_)
    {
        _ParticleWaterFall.transform.position = Pos_ - new Vector3(0.0f,0.05f,0.0f);
        _ParticleWaterFall.SetActive(true);
    }
    public void TutorialClose()
    {
        _GameStep = EGameStep.Ready;
        _DelayTime = CGlobal.GetServerTimePoint();
        _GameUI.ShowGameStart();
    }
}