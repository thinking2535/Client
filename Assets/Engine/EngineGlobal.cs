using rso.physics;
using bb;
using System;

public static class CEngineGlobal
{
    public const Int32 c_ContainerNumber = 0;
    public const Int32 c_StructureNumber = 1;
    public const Int32 c_BodyNumber = 2;
    public const Int32 c_BalloonNumber = 3;
    public const Int32 c_ParachuteNumber = 4;

    public static bool IsAlive(this SCharacter Char_)
    {
        return (Char_.BalloonCount >= 0);
    }
    public static bool IsInvulerable(this SCharacter Char_, Int64 Tick_)
    {
        return (Tick_ < Char_.InvulnerableEndTick);
    }
    public static SByte GetFace(SPoint Pos_)
    {
        return (Pos_.X < global.c_ScreenCenterX ? (SByte)1 : (SByte)(-1));
    }
    public static Int64 GetInvulnerableEndTick(Int64 Tick_)
    {
        return (Tick_ + CGlobal.MetaData.ConfigMeta.InvulnerableDurationSec * 10000000);
    }
    public static SRectCollider2D GetPlayerRect()
    {
        return new SRectCollider2D(new SPoint(global.c_PlayerWidth, global.c_PlayerHeight), new SPoint(0.0f, global.c_PlayerOffsetY), new SPoint(1.0f, 1.0f));
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
        return new SRectCollider2D(GetBalloonSize(BalloonCount_), new SPoint(0.0f, global.c_BalloonOffsetY), new SPoint(global.c_BalloonLocalScale, global.c_BalloonLocalScale));
    }
    public static SRectCollider2D GetParachuteRect(float Scale_)
    {
        return new SRectCollider2D(new SPoint(global.c_ParachuteWidth, global.c_ParachuteHeight), new SPoint(0.0f, global.c_ParachuteOffsetY), new SPoint(Scale_, Scale_));
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
    public static CEngineGameMode GetGameModeMulti(SBattleType BattleType_)
    {
        if (BattleType_.TeamCount == 2 && BattleType_.MemberCount == 1)
            return new CEngineGameModeMultiSolo(BattleType_);
        if (BattleType_.TeamCount == 3 && BattleType_.MemberCount == 1)
            return new CEngineGameModeMultiSurvivalSmall(BattleType_);
        if (BattleType_.TeamCount == 2 && BattleType_.MemberCount == 2)
            return new CEngineGameModeMultiTeamSmall(BattleType_);
        else if (BattleType_.TeamCount >= 2)
        {
            if (BattleType_.MemberCount == 1)
                return new CEngineGameModeMultiSurvival(BattleType_);
            else if (BattleType_.MemberCount > 1)
                return new CEngineGameModeMultiTeam(BattleType_);
            else
                throw new Exception("Invalid MemberCount");
        }
        else if (BattleType_.TeamCount == 1)
            return new CEngineGameModeSingle(BattleType_);
        else
            throw new Exception("Invalid TeamCount");
    }
}
