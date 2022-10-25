using bb;
using rso.core;
using rso.physics;
using rso.unity;
using System;
using UnityEngine;

public class CMultiBattlePlayer : CBattlePlayer
{
    public delegate void FHit(Int32 AttackerIndex_, Int32 TargetIndex_);

    FHit _fHit;
    public SMultiBattleInfo BattleInfo;

    public void Init(
        FHit fHit_,
        Int32 PlayerIndex_,
        SPoint InitialPos_,
        SCharacterMeta Meta_,
        SCharacterNet Character_,
        TeamMaterial teamMaterial,
        Int64 Tick_,
        SBattlePlayer BattlePlayer_,
        bool IsMe_,
        Transform Prop_,
        Camera Camera_,
        SMultiBattleInfo BattleInfo_)
    {
        base.Init(
            PlayerIndex_,
            InitialPos_,
            Meta_,
            Character_,
            teamMaterial,
            Tick_,
            BattlePlayer_,
            this,
            IsMe_,
            Prop_,
            Camera_);

        _fHit = fHit_;
        BattleInfo = BattleInfo_;
    }
    public void Kill(Int32 AddedPoint_)
    {
        BattleInfo.Point += AddedPoint_;
    }
    public void AttackBalloon(Int32 AddedPoint_)
    {
        BattleInfo.Point += AddedPoint_;
    }
    protected override bool _CollisionEnter(Int64 tick, SCollision2D Collision_)
    {
        if (_LandEnter(Collision_))
            return false;

        var OtherBattlePlayerObject = Collision_.OtherMovingObject as CBattlePlayerObject;
        if (OtherBattlePlayerObject != null)
        {
            var OtherBattlePlayer = OtherBattlePlayerObject.BattlePlayer;

            if (TeamIndex != OtherBattlePlayer.TeamIndex && // 다른편이고
                Collision_.OtherCollider.Number == CEngineGlobal.c_BodyNumber && // 적의 몸과 부딪혔고
                !IsInvulerable(tick) && // 내가 무적이 아니고
                !OtherBattlePlayer.IsInvulerable(tick) && // 적이 무적이 아니고
                OtherBattlePlayer.Character.BalloonCount > 0 && // 적이 풍선이 있고
                (Collision_.Collider.Number == CEngineGlobal.c_BalloonNumber || Character.BalloonCount == 0)) // 내 풍선과 부딪혔거나, 내 풍선이 없으면
            {
                if (_beHitBalloon(Collision_.Normal)) // Dead
                {
                    if ((tick - OtherBattlePlayer.Character.LastKillTick) < global.c_ChainKillDelayTickCount)
                        ++OtherBattlePlayer.Character.ChainKillCount;
                    else
                        OtherBattlePlayer.Character.ChainKillCount = 1;

                    OtherBattlePlayer.Character.LastKillTick = tick;
                    _Die(tick);
                }

                _fHit(OtherBattlePlayer.PlayerIndex, PlayerIndex);
            }
        }

        if (IsAlive())
            bounce(Collision_);

        return false;
    }
}
