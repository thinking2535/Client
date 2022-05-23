using rso.physics;
using bb;
using System;
using System.Collections.Generic;

public class CEngineGameModeMultiTeamSmall : CEngineGameModeMulti
{
    public CEngineGameModeMultiTeamSmall(SBattleType BattleType_) :
        base(BattleType_)
    {
    }
    public override EGameMode GetGameMode()
    {
        return EGameMode.TeamSmall;
    }
    public override void FixPos(SPoint Pos_)
    {
    }
    public override float GetCameraY(float CharY_)
    {
        return 0.94f;
    }
}
