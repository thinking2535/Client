using bb;
using rso.physics;
using rso.unity;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class NetworkBattleScene : BattleScene
{
    protected CClientEngine _clientEngine;
    List<CBattlePlayer> _BattlePlayers = new List<CBattlePlayer>();
    public void init(Int64 tick, SPoint map)
    {
        _clientEngine = new CClientEngine(
            tick,
            global.c_ContactOffset,
            global.c_FPS,
            global.c_NetworkTickSync,
            global.c_NetworkTickBuffer);

        base.init(_clientEngine, map);
    }
    protected void _AddBattlePlayer(CBattlePlayer BattlePlayer_)
    {
        _BattlePlayers.Add(BattlePlayer_);
        _Engine.AddPlayer(BattlePlayer_.PlayerObject);
    }
    protected override bool _touched(InputTouch.TouchState state, Int32 direction)
    {
        if (!base._touched(state, direction))
            return false;

        if (!_getMyBattlePlayer().IsAlive())
            return false;

        switch (state)
        {
            case InputTouch.TouchState.down:
                break;

            case InputTouch.TouchState.move:
                CGlobal.NetControl.Send(new SBattleTouchNetCs(direction == 0 ? (sbyte)-1 : (sbyte)1));
                break;

            default:
                CGlobal.NetControl.Send(new SBattleTouchNetCs(0));
                break;
        }

        return true;
    }
    protected override bool _pushed(InputTouch.TouchState state)
    {
        if (!base._pushed(state))
            return false;

        var battlePlayer = _getMyBattlePlayer();
        if (!battlePlayer.IsAlive())
            return false;

        if (state == InputTouch.TouchState.down)
        {
            if (battlePlayer.canPush())
                CGlobal.NetControl.Send(new SBattlePushNetCs());
        }

        return true;
    }
    public void Sync(Int64 Tick_)
    {
        _clientEngine.Sync(new CMessage(Tick_));
    }
    public void direct(SBattleDirectNetSc Proto_)
    {
        _clientEngine.Sync(new CMessageDirect(Proto_.Tick, _BattlePlayers[Proto_.PlayerIndex].direct, Proto_.Dir));
    }
    public void flap(SBattleFlapNetSc Proto_)
    {
        _clientEngine.Sync(new CMessageFlap(Proto_.Tick, _BattlePlayers[Proto_.PlayerIndex].flap));
    }
    public void pump(SBattlePumpNetSc Proto_)
    {
        _clientEngine.Sync(new CMessagePump(Proto_.Tick, _BattlePlayers[Proto_.PlayerIndex].pump));
    }
}
public class CMessageDirect : CMessage
{
    public delegate void FCallback(SByte Dir_);

    FCallback _Callback;
    SByte _Dir;
    public CMessageDirect(Int64 Tick_, FCallback Callback_, SByte Dir_) :
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
public class CMessageFlap : CMessage
{
    public delegate void FCallback(Int64 Tick_);

    FCallback _Callback;
    public CMessageFlap(Int64 Tick_, FCallback Callback_) :
        base(Tick_)
    {
        _Callback = Callback_;
    }
    public override void Proc()
    {
        _Callback(Tick);
    }
}
public class CMessagePump : CMessage
{
    public delegate void FCallback();

    FCallback _Callback;
    public CMessagePump(Int64 Tick_, FCallback Callback_) :
        base(Tick_)
    {
        _Callback = Callback_;
    }
    public override void Proc()
    {
        _Callback();
    }
}
