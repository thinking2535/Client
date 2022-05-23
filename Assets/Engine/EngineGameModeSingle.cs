using bb;
using rso.physics;
using System;
using System.Collections.Generic;

public class CEngineGameModeSingle : CEngineGameMode
{
    public CEngineGameModeSingle(SBattleType BattleType_) :
        base(BattleType_)
    {
    }
    public override EGameMode GetGameMode()
    {
        return EGameMode.Single;
    }
    public override void FixPos(SPoint Pos_)
    {
    }
    public override float GetCameraY(float CharY_)
    {
        return 0.94f;
    }
}
