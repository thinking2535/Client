using rso.core;
using rso.unity;
using bb;
using System;
using UnityEngine;

public abstract class CSceneBaseMulti : CSceneBase
{
    protected MultiModeUI _GameUI = null;
    protected SBattleBeginNetSc _Proto;
    protected SBattlePlayer[] _Players;
    protected SCharacterClientMeta[] _Characters;
    protected bool IsGameOver = false;
    public CSceneBaseMulti(string PrefabName_, Vector3 Pos_, bool Active_, SBattleBeginNetSc Proto_) :
        base(PrefabName_, Pos_, Active_)
    {
        _Proto = Proto_;
        _Players = new SBattlePlayer[_Proto.Players.Count];
        _Characters = new SCharacterClientMeta[_Proto.Players.Count];
        for(var i = 0; i < _Proto.Players.Count; ++i)
        {
            _Players[i] = _Proto.Players[i];
            _Characters[i] = CGlobal.MetaData.Chars[_Proto.Players[i].CharCode];
        }
        IsGameOver = false;
    }
    public SBattleBeginNetSc GetSBattleBeginNetSc()
    {
        return (SBattleBeginNetSc)_Proto;
    }
    public SBattlePlayer[] GetPlayers()
    {
        return _Players;
    }
    public SCharacterClientMeta[] GetCharacters()
    {
        return _Characters;
    }
    public virtual Int32 GetWinTeamIndex()
    {
        return -1;
    }
    public virtual void GameStart(TimePoint EndTime_)
    {
    }
    public virtual void ShowEmoticon(Int32 PlayerIndex_, Int32 Code_)
    {
    }
    public virtual void UpdateScore(Int32 PlayerIndex_, Int32 Score_)
    {
    }
    public virtual void UseItem(Int32 PlayerIndex_, EMultiItemType Code_)
    {
    }
    public virtual void GameOver()
    {
        IsGameOver = true;
    }
    public bool GetGameOver()
    {
        return IsGameOver;
    }
}
