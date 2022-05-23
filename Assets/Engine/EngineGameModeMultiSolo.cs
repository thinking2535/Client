using rso.physics;
using bb;
using System;
using System.Collections.Generic;

public class CEngineGameModeMultiSolo : CEngineGameModeMulti
{
    public CEngineGameModeMultiSolo(SBattleType BattleType_) :
        base(BattleType_)
    {
    }
    public override EGameMode GetGameMode()
    {
        return EGameMode.Solo;
    }
    public override void FixPos(SPoint Pos_)
    {
    }
    public override float GetCameraY(float CharY_)
    {
        return 0.94f;
    }
}
