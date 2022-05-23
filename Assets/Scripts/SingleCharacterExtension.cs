using rso.physics;
using bb;
using System;
using UnityEngine;

public static class CSingleCharacterExtension
{
    public static bool IsAlive(this SSingleCharacter Char_)
    {
        return (Char_.BalloonCount >= 0);
    }
    public static float GetMaxVelDown(this SSingleCharacter Char_, SCharacterMeta Meta_)
    {
        if (Char_.BalloonCount > 0)
            return -Meta_.MaxVelDown;
        else if (Char_.BalloonCount == 0)
            return -global.c_MaxVelParachuteY;
        else
            return -global.c_MaxVelDeadY;
    }
    public static void Center(this SSingleCharacter Char_) // 조이패드 중앙
    {
        Char_.Dir = 0;
        Char_.Acc.X = 0.0f;
    }
    public static void LeftRight(this SSingleCharacter Char_, SCharacterMeta Meta_, sbyte Dir_)
    {
        Char_.Dir = Dir_;

        if (Char_.Face != Dir_)
            Char_.Face = Dir_;

        if (Char_.IsGround)
        {
            Char_.Acc.X = Meta_.RunAcc * Char_.Dir;
            Char_.PumpCount = 0;
        }
        else if (Char_.BalloonCount == 0)
        {
            Char_.Acc.X = global.c_ParachuteAccX * Char_.Dir;
        }
    }
    public static void Fly(this SSingleCharacter Char_)
    {
        Char_.IsGround = false;
        Char_.PumpCount = 0;

        if (Char_.BalloonCount == 0)
            Char_.Acc.X = global.c_ParachuteAccX * Char_.Dir;
    }
    public static void Land(this SSingleCharacter Char_, SCharacterMeta Meta_)
    {
        Char_.IsGround = true;
        Char_.Acc.X = Meta_.RunAcc * Char_.Dir;
    }
    public static bool RecvPumpDone(this SSingleCharacter Char_)
    {
        if (++Char_.PumpCount >= global.c_PumpCountForBalloon)
        {
            Char_.PumpCount = 0;
            Char_.BalloonCount = global.c_BalloonCountForPump;

            if (!Char_.IsGround)
                Char_.Acc.X = 0.0f;

            Char_.Acc.Y = global.c_Gravity;
            return true;
        }
        else
        {
            return false;
        }
    }
    public static SRect GetBalloonRect(this SPoint Pos_, sbyte BalloonCount_)
    {
        if (BalloonCount_ >= global.c_BalloonCountForRegen)
            return new SRect(Pos_.X - CGlobal.c_Balloon2Width_2, Pos_.X + CGlobal.c_Balloon2Width_2, Pos_.Y + global.c_PlayerHeight + CGlobal.c_BalloonGap, Pos_.Y + global.c_PlayerHeight + CGlobal.c_BalloonGap + CGlobal.c_Balloon2Height);
        else if (BalloonCount_ == 1)
            return new SRect(Pos_.X - CGlobal.c_Balloon1Width_2, Pos_.X + CGlobal.c_Balloon1Width_2, Pos_.Y + global.c_PlayerHeight + CGlobal.c_BalloonGap, Pos_.Y + global.c_PlayerHeight + CGlobal.c_BalloonGap + CGlobal.c_Balloon1Height);
        else
            return null;
    }
    public static SRect GetBodyRect(this SPoint Pos_)
    {
        return new SRect(Pos_.X - CGlobal.c_PlayerWidth_2, Pos_.X + CGlobal.c_PlayerWidth_2, Pos_.Y, Pos_.Y + global.c_PlayerHeight);
    }
}