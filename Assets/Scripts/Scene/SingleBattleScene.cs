using bb;
using rso.physics;
using rso.unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SingleBattleScene : NetworkBattleScene
{
    [SerializeField] protected SingleModeUI _gameUI;

    protected CObjectPool _ObjectPool = new CObjectPool();
    bool _isBattleEnded = false;
    protected override void OnDestroy()
    {
        _ObjectPool.Dispose();

        base.OnDestroy();
    }
    public void init(Int64 tick, SPoint map, Func<string> fGetRetryString, Int32 scorePerGold, SingleBattleInfo battleInfo)
    {
        base.init(tick, map);

        _gameUI.Init(
            _showExitPopup,
            _retry,
            _goToLobbyScene,
            fGetRetryString,
            scorePerGold,
            battleInfo.Point,
            battleInfo.Gold);
    }
    public void startBattle(SingleBattleStartNetSc Proto_)
    {
        _Engine.Start();
        _gameUI.HideGameStart();
        CGlobal.MusicPlayDodge();
    }
    void _goToLobbyScene()
    {
        CGlobal.setLobbyScene();
    }
    async void _showExitPopup()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        if (await CGlobal.curScene.pushAskingPopup(EText.SceenSingle_ExitPopup) is true)
        {
            _sendBattleEnd();
            CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        }
    }
    protected override async Task<bool> _backButtonPressed()
    {
        if (await base._backButtonPressed())
            return true;

        if (!_isBattleEnded)
        {
            _showExitPopup();
        }
        else
        {
            if (_gameUI.isPlayingResult())
                _gameUI.skipResult();
            else
                _goToLobbyScene();
        }

        return true;
    }
    protected void _battleEnd(SingleBattleEndNetSc proto)
    {
        Sync(proto.Tick);
        CGlobal.MusicStop();

        CGlobal.UpdateQuest(proto.DoneQuests);

        CGlobal.CheckQuestRedDot = true;
        CGlobal.LoginNetSc.User.Resources = proto.ResourcesLeft;

        _isBattleEnded = true;
        _gameUI.ShowResultPopup();
    }
    protected void _setScore(Int32 score)
    {
        _gameUI.setScore(score);
    }
    protected abstract void _retry();
    protected abstract void _sendBattleEnd();
}
