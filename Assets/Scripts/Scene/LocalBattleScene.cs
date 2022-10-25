using rso.physics;
using rso.unity;
using System;

public abstract class LocalBattleScene : BattleScene
{
    public void init(SPoint rootPosition)
    {
        base.init(
            new CLocalEngine(
            0,
            bb.global.c_ContactOffset,
            bb.global.c_FPS),
            rootPosition);
    }
    protected override bool _touched(InputTouch.TouchState state, Int32 direction)
    {
        if (!base._touched(state, direction))
            return false;

        switch (state)
        {
            case InputTouch.TouchState.down:
                break;

            case InputTouch.TouchState.move:

                _getMyBattlePlayer().direct(direction == 0 ? (sbyte)-1 : (sbyte)1);
                break;

            default:
                _getMyBattlePlayer().direct(0);
                break;
        }

        return true;
    }
    protected override bool _pushed(InputTouch.TouchState state)
    {
        if (!base._pushed(state))
            return false;

        if (state == InputTouch.TouchState.down)
            _getMyBattlePlayer().push(_Engine.Tick);

        return true;
    }
    protected override void _fixedUpdate()
    {
    }
}