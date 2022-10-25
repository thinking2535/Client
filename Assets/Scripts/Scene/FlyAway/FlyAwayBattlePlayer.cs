using bb;
using rso.core;
using rso.physics;
using rso.unity;
using System;
using UnityEngine;

using TResource = System.Int32;
public class CFlyAwayBattlePlayer : CBattlePlayer
{
    public delegate void FGetItem(CFlyAwayItem Item_);
    public delegate void FLand(CFlyAwayLand Land_);
    public delegate void FDead(bool isIntoWater);

    FGetItem _fGetItem;
    FLand _fLand;
    FDead _fDead;
    public SFlyAwayBattleInfo BattleInfo;

    public void Init(
        SPoint InitialPos_,
        SCharacterMeta Meta_,
        SCharacterNet Character_,
        TeamMaterial teamMaterial,
        Int64 Tick_,
        SBattlePlayer BattlePlayer_,
        Transform Prop_,
        Camera Camera_,
        FGetItem fGetItem_,
        FLand fLand_,
        FDead fDead,
        SFlyAwayBattleInfo BattleInfo_)
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

        _fGetItem = fGetItem_;
        _fLand = fLand_;
        _fDead = fDead;

        BattleInfo = BattleInfo_;
    }
    protected override void _FixedUpdate(Int64 tick)
    {
        _UpdatePhysics();
    }
    protected override bool _CollisionEnter(Int64 tick, SCollision2D Collision_)
    {
        if (_LandEnter(Collision_))
            return false;

        if (Collision_.OtherCollider.Number == CEngineGlobal.c_LandNumber &&
            Collision_.Collider.Number == CEngineGlobal.c_BodyNumber && Collision_.Normal.Y > 0.0f)
        {
            _AttachGround(Collision_.OtherCollider);

            var Land = Collision_.OtherCollider as CFlyAwayLand;
            if (Land.StartShake(tick))
                _fLand(Land);

            return false;
        }
        else if (
            Collision_.OtherCollider.Number == CEngineGlobal.c_DeadZoneNumber ||
            Collision_.OtherCollider.Number == CEngineGlobal.c_OceanNumber)
        {
            Die(tick);

            _fDead(Collision_.OtherCollider.Number == CEngineGlobal.c_OceanNumber);
            return true;
        }
        else
        {
            bounce(Collision_);
            return false;
        }
    }
    protected override bool _CollisionStay(Int64 tick, SCollision2D Collision_)
    {
        if (Collision_.Collider.Number != CEngineGlobal.c_BodyNumber || (Collision_.OtherCollider.Number != CEngineGlobal.c_StructureNumber && Collision_.OtherCollider.Number != CEngineGlobal.c_LandNumber))
            return false;

        _LandStay(Collision_);

        return false;
    }
    protected override bool _CollisionExit(Int64 tick, SCollision2D Collision_)
    {
        if (Collision_.Collider.Number != CEngineGlobal.c_BodyNumber || (Collision_.OtherCollider.Number != CEngineGlobal.c_StructureNumber && Collision_.OtherCollider.Number != CEngineGlobal.c_LandNumber))
            return false;

        _DetachGround(Collision_.OtherCollider);

        return false;
    }
    protected override bool _TriggerEnter(CCollider2D Collider_)
    {
        if (Collider_.Number == CEngineGlobal.c_ItemNumber)
        {
            _fGetItem(Collider_ as CFlyAwayItem);
            return true;
        }

        return false;
    }

    public void SetItem(SFlyAwayItemMeta Meta_)
    {
        BattleInfo.Gold += Meta_.AddedGold;
        addStamina(Meta_.AddedStamina);
    }
    public void addStamina(Single stamina)
    {
        Character.StaminaInfo.Stamina += stamina;

        if (Character.StaminaInfo.Stamina > Meta.StaminaMax)
            Character.StaminaInfo.Stamina = Meta.StaminaMax;
    }
    public float GetStaminaValue()
    {
        return Character.StaminaInfo.Stamina / Meta.StaminaMax;
    }
}