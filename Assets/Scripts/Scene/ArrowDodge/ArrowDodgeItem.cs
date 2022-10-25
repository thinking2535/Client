using bb;
using rso.Base;
using rso.physics;
using System;

public abstract class CArrowDodgeItem : CRectCollider2D
{
    public CList<SArrowDodgeItemObject>.SIterator Iterator;
    public CArrowDodgeItem(SPoint LocalPosition_, SRectCollider2D Collider_) :
        base(CPhysics.GetDefaultTransform(LocalPosition_), CEngineGlobal.c_ItemNumber, Collider_)
    {
        IsTrigger = true;
    }
    public virtual void Proc(Int64 tick, CArrowDodgeBattlePlayer Player_, ArrowDodgeBattleScene Battle_)
    {
        Player_.SetItem(CGlobal.MetaData.ArrowDodgeItemMetas[(Int32)GetItemType()]);
        Battle_.UpdateScoreGold();
    }
    public abstract EArrowDodgeItemType GetItemType();
    public abstract string GetPrefabName();
}
public class CArrowDodgeCoin : CArrowDodgeItem
{
    public CArrowDodgeCoin(SPoint LocalPosition_) :
        base(LocalPosition_, CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Coin.Collider)
    {
    }
    public override EArrowDodgeItemType GetItemType()
    {
        return EArrowDodgeItemType.Coin;
    }
    public override string GetPrefabName()
    {
        return CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Coin.PrefabName;
    }
}
public class CArrowDodgeGoldBar : CArrowDodgeItem
{
    public CArrowDodgeGoldBar(SPoint LocalPosition_) :
        base(LocalPosition_, CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.GoldBar.Collider)
    {
    }
    public override EArrowDodgeItemType GetItemType()
    {
        return EArrowDodgeItemType.GoldBar;
    }
    public override string GetPrefabName()
    {
        return CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.GoldBar.PrefabName;
    }
}
public class CArrowDodgeShield : CArrowDodgeItem
{
    public CArrowDodgeShield(SPoint LocalPosition_) :
        base(LocalPosition_, CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Shield.Collider)
    {
    }
    public override void Proc(Int64 tick, CArrowDodgeBattlePlayer Player_, ArrowDodgeBattleScene Battle_)
    {
        base.Proc(tick, Player_, Battle_);
        Player_.SetShieldItem(tick, this);
    }
    public override EArrowDodgeItemType GetItemType()
    {
        return EArrowDodgeItemType.Shield;
    }
    public override string GetPrefabName()
    {
        return CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Shield.PrefabName;
    }
}
public class CArrowDodgeStamina : CArrowDodgeItem
{
    public CArrowDodgeStamina(SPoint LocalPosition_) :
        base(LocalPosition_, CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Stamina.Collider)
    {
    }
    public override void Proc(Int64 tick, CArrowDodgeBattlePlayer Player_, ArrowDodgeBattleScene Battle_)
    {
        base.Proc(tick, Player_, Battle_);
        Player_.SetStaminaItem(tick, this);
    }
    public override EArrowDodgeItemType GetItemType()
    {
        return EArrowDodgeItemType.Stamina;
    }
    public override string GetPrefabName()
    {
        return CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Stamina.PrefabName;
    }
}
