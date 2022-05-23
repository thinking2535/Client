using bb;
using rso.physics;
using System;
using System.Collections.Generic;

public class CEngineGameModeMultiTeam : CEngineGameModeMulti
{
    public CEngineGameModeMultiTeam(SBattleType BattleType_) :
        base(BattleType_)
    {
    }
    public override EGameMode GetGameMode()
    {
        return EGameMode.Team;
    }
}

