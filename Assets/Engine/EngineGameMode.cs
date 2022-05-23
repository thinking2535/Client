using rso.physics;
using bb;
using System;
using System.Collections.Generic;

public abstract class CEngineGameMode : SBattleType
{
    public static readonly string[] BalloonMaterialNames = {
        "Material/Balloon_01_Red",
        "Material/Balloon_01_Yellow",
        "Material/Balloon_01_Orange",
        "Material/Balloon_01_Bluegreen",
        "Material/Balloon_01_Purple",
        "Material/Balloon_01_Green"
    };

    private static readonly string[] ParachuteMaterialNames = {
        "Material/Parachute_Red",
        "Material/Parachute_Yellow",
        "Material/Parachute_Orange",
        "Material/Parachute_Bluegreen",
        "Material/Parachute_Purple",
        "Material/Parachute_Green"
    };

    public CEngineGameMode(SBattleType BattleType_) :
        base(BattleType_)
    {
    }
    public abstract EGameMode GetGameMode();
    public abstract void FixPos(SPoint Pos_);
    public abstract float GetCameraY(float CharY_);


    // Client Only /////////////////////////////////////////////////
    public string GetBalloonMaterialName(SByte TeamIndex_, SByte MyTeamIndex_)
    {
        if (TeamCount == 2)
        {
            if (TeamIndex_ == MyTeamIndex_)
                return "Material/Balloon_01_Blue";
            else
                return "Material/Balloon_01_Red";
        }
        else
        {
            return BalloonMaterialNames[TeamIndex_];
        }
    }
    public string GetParachuteMaterialName(SByte TeamIndex_, SByte MyTeamIndex_)
    {
        if (TeamCount == 2)
        {
            if (TeamIndex_ == MyTeamIndex_)
                return "Material/Parachute_Blue";
            else
                return "Material/Parachute_Red";
        }
        else
        {
            return ParachuteMaterialNames[TeamIndex_];
        }
    }
}
