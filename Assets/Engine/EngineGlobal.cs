using rso.physics;
using bb;
using System;
using System.Collections.Generic;
using rso.unity;
using UnityEngine;

public static class CEngineGlobal
{
    public const Int32 c_StructureNumber = 1;
    public const Int32 c_BodyNumber = 2;
    public const Int32 c_BalloonNumber = 3;
    public const Int32 c_ParachuteNumber = 4;
    public const Int32 c_ArrowNumber = 5;
    public const Int32 c_ItemNumber = 6;
    public const Int32 c_LandNumber = 7;
    public const Int32 c_DeadZoneNumber = 8;
    public const Int32 c_OceanNumber = 9;
    public static SByte GetFaceWithX(float x)
    {
        return (x <= 0.0f ? (SByte)1 : (SByte)(-1));
    }
    public static Int64 GetInvulnerableEndTick(Int64 tick)
    {
        return (tick + CGlobal.MetaData.ConfigMeta.InvulnerableDurationSec * 10000000);
    }
    public static SRectCollider2D GetPlayerRect()
    {
        return new SRectCollider2D(new SPoint(global.c_PlayerWidth, global.c_PlayerHeight), new SPoint(0.0f, global.c_PlayerOffsetY));
    }
    public static float BalloonWidth(SByte BalloonCount_)
    {
        if (BalloonCount_ > global.c_BalloonCountForRegen)
            BalloonCount_ = 2;
        else if (BalloonCount_ < 0)
            BalloonCount_ = 0;

        return global.c_BalloonWidth * BalloonCount_;
    }
    public static SPoint GetBalloonSize(SByte BalloonCount_)
    {
        return new SPoint(BalloonWidth(BalloonCount_), global.c_BalloonHeight);
    }
    public static SRectCollider2D GetBalloonRect(SByte BalloonCount_)
    {
        return new SRectCollider2D(GetBalloonSize(BalloonCount_), new SPoint(0.0f, global.c_BalloonOffsetY));
    }
    public static SRectCollider2D GetParachuteRect()
    {
        return new SRectCollider2D(new SPoint(global.c_ParachuteWidth, global.c_ParachuteHeight), new SPoint(0.0f, global.c_ParachuteOffsetY));
    }
    public static bool IsScaling(this SPumpInfo PumpInfo_)
    {
        return (PumpInfo_.Count < PumpInfo_.CountTo);
    }
    public static bool IsScaling(this SParachuteInfo ParachuteInfo_)
    {
        return (ParachuteInfo_.Velocity != 0.0f ||
            (ParachuteInfo_.Scale > 0.0f && ParachuteInfo_.Scale < global.c_ParachuteLocalScale));
    }
}
