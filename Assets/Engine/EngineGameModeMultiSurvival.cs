using bb;
using rso.physics;
using System;
using System.Collections.Generic;

public class CEngineGameModeMultiSurvival : CEngineGameModeMulti
{
    public CEngineGameModeMultiSurvival(SBattleType BattleType_) :
        base(BattleType_)
    {
    }
    public override EGameMode GetGameMode()
    {
        return EGameMode.Survival;
    }
}
