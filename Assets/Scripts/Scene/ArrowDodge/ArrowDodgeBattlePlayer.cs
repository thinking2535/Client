using bb;
using rso.core;
using rso.physics;
using rso.unity;
using System;
using UnityEngine;

using TResource = System.Int32;
public class CArrowDodgeBattlePlayer : CBattlePlayer
{
    public delegate void FHitArrow(CArrow Arrow_, bool IsDefended_);
    public delegate void FGetItem(CArrowDodgeItem Item_);

    FHitArrow _fHitArrow;
    FGetItem _fGetItem;
    public SArrowDodgeBattleInfo BattleInfo;
    public SArrowDodgeBattleBufs Bufs;

    public void Init(
        SPoint InitialPos_,
        SCharacterMeta Meta_,
        SCharacterNet Character_,
        TeamMaterial teamMaterial,
        Int64 Tick_,
        SBattlePlayer BattlePlayer_,
        Transform Prop_,
        Camera Camera_,
        FHitArrow fHitArrow_,
        FGetItem fGetItem_,
        SArrowDodgeBattleInfo BattleInfo_,
        SArrowDodgeBattleBufs Bufs_)
    {
        base.Init(
            0,
            InitialPos_,
            Meta_,
            Character_,
            teamMaterial,
            Tick_,
            BattlePlayer_,
            this,
            true,
            Prop_,
            Camera_);

        PlayerObject.fTriggerEnter = _TriggerEnter;

        _fHitArrow = fHitArrow_;
        _fGetItem = fGetItem_;

        BattleInfo = BattleInfo_;
        Bufs = Bufs_;
    }
    protected override void _FixedUpdate(Int64 tick)
    {
        base._FixedUpdate(tick);

        if (Bufs.Shield.Enabled && Bufs.Shield.EndTick < tick)
            Bufs.Shield.Enabled = false;

        if (Bufs.Stamina.Enabled && Bufs.Stamina.EndTick < tick)
            Bufs.Stamina.Enabled = false;
    }
    protected override bool _CollisionEnter(Int64 tick, SCollision2D Collision_)
    {
        if (_LandEnter(Collision_))
            return false;

        bool isHitArrow = false;

        if (Collision_.OtherCollider.Number == CEngineGlobal.c_ArrowNumber &&
            Collision_.OtherMovingObject != null)
        {
            var Arrow = Collision_.OtherMovingObject as CArrow;

            if (Bufs.Shield.Enabled)
            {
                _fHitArrow(Arrow, true);
                return true;
            }

            if (Arrow.Velocity.X > 0.0f && Collision_.Normal.X > 0.0f ||
                Arrow.Velocity.X < 0.0f && Collision_.Normal.X < 0.0f ||
                Arrow.Velocity.Y > 0.0f && Collision_.Normal.Y > 0.0f ||
                Arrow.Velocity.Y < 0.0f && Collision_.Normal.Y < 0.0f)
            {
                isHitArrow = true;

                if (Collision_.Collider.Number == CEngineGlobal.c_BalloonNumber)
                {
                    if (_beHitBalloon(Collision_.Normal))
                        _Die(tick);
                }
                else if (Collision_.Collider.Number == CEngineGlobal.c_BodyNumber || Collision_.Collider.Number == CEngineGlobal.c_ParachuteNumber)
                {
                    Die(tick);
                }

                _fHitArrow(Arrow, false);
            }
        }

        if (IsAlive())
            bounce(Collision_);

        return isHitArrow;
    }
    protected override bool _TriggerEnter(CCollider2D Collider_)
    {
        if (Collider_.Number == CEngineGlobal.c_ItemNumber)
        {
            _fGetItem(Collider_ as CArrowDodgeItem);
            return true;
        }

        return false;
    }
    protected override bool IsStaminaFree()
    {
	    return Bufs.Stamina.Enabled;
    }
    public void SetItem(SArrowDodgeItemMeta Meta_)
    {
        BattleInfo.Gold += Meta_.AddedGold;
    }
    public void SetShieldItem(Int64 tick, CArrowDodgeShield Shield_)
    {
        Int64 Duration = CGlobal.MetaData.arrowDodgeConfigMeta.ItemDurationTick;
        Bufs.Shield.Enabled = true;
	    Bufs.Shield.EndTick = tick + Duration;
        _Character.SetShieldItem(CPhysics.TickToFloatTime(Duration));
    }
    public void SetStaminaItem(Int64 tick, CArrowDodgeStamina Stamina_)
    {
        Int64 Duration = CGlobal.MetaData.arrowDodgeConfigMeta.ItemDurationTick;
        Character.StaminaInfo.Stamina = Meta.StaminaMax;
        Bufs.Stamina.Enabled = true;
	    Bufs.Stamina.EndTick = tick + Duration;
        _Character.SetStaminaItem(CPhysics.TickToFloatTime(Duration));
    }
}