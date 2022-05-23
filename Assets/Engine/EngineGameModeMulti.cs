using rso.physics;
using bb;
using System;
using System.Collections.Generic;

public abstract class CEngineGameModeMulti : CEngineGameMode
{
    public CEngineGameModeMulti(SBattleType BattleType_) :
        base(BattleType_)
    {
    }
    public override void FixPos(SPoint Pos_)
    {
        // Fix X
        Pos_.X -= global.c_ScreenWidth_2;
        if (Pos_.X < 0.0f)
            Pos_.X += ((Int32)(-Pos_.X / global.c_ScreenWidth) + 1) * global.c_ScreenWidth;
        Pos_.X %= global.c_ScreenWidth;
        Pos_.X += global.c_ScreenWidth_2;

        // Fix Y
        if (Pos_.Y < 0.0f)
            Pos_.Y += ((Int32)(-Pos_.Y / global.c_ScreenHeight) + 1) * global.c_ScreenHeight;
        Pos_.Y %= global.c_ScreenHeight;
    }
    public override float GetCameraY(float CharY_)
    {
        return CharY_ + global.c_PlayerHeight;
    }
}
