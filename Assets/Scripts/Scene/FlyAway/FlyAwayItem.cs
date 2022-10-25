using bb;
using rso.Base;
using rso.physics;
using rso.unity;
using System;
using UnityEngine;

public class FlyAwayItem : MonoBehaviour
{
    CFlyAwayItem _Item;

    public void Init(CFlyAwayItem Item_)
    {
        _Item = Item_;
    }
    public void Update()
    {
        transform.localPosition = _Item.LocalPosition.ToVector2();
    }
}
public abstract class CFlyAwayItem : CFlyAwayObject
{
    public CList<SFlyAwayItemObject>.SIterator Iterator;
    public CFlyAwayItem(STransform Transform_, SRectCollider2D Collider_) :
        base(Transform_, CEngineGlobal.c_ItemNumber, Collider_)
    {
        IsTrigger = true;
    }
    public override void Proc(CFlyAwayBattlePlayer Player_, FlyAwayBattleScene Battle_)
    {
        Player_.SetItem(CGlobal.MetaData.FlyAwayItemMetas[(Int32)GetItemType()]);
        Battle_.UpdateScoreGold();
    }
    public abstract EFlyAwayItemType GetItemType();
}
public class CFlyAwayCoin : CFlyAwayItem
{
    public CFlyAwayCoin(SPoint LocalPosition_) :
        this(CPhysics.GetDefaultTransform(LocalPosition_))
    {
    }
    public CFlyAwayCoin(STransform Transform_) :
        base(Transform_, CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Coin.Collider)
    {
    }
    public override EFlyAwayItemType GetItemType()
    {
        return EFlyAwayItemType.Coin;
    }
    public override string GetPrefabName()
    {
        return CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Coin.PrefabName;
    }
}
public class CFlyAwayGoldBar : CFlyAwayItem
{
    public CFlyAwayGoldBar(SPoint LocalPosition_) :
        this(CPhysics.GetDefaultTransform(LocalPosition_))
    {
    }
    public CFlyAwayGoldBar(STransform Transform_) :
        base(Transform_, CGlobal.MetaData.MapMeta.FlyAwayMapInfo.GoldBar.Collider)
    {
    }
    public override EFlyAwayItemType GetItemType()
    {
        return EFlyAwayItemType.GoldBar;
    }
    public override string GetPrefabName()
    {
        return CGlobal.MetaData.MapMeta.FlyAwayMapInfo.GoldBar.PrefabName;
    }
}
public class CFlyAwayApple : CFlyAwayItem
{
    public CFlyAwayApple(SPoint LocalPosition_) :
        this(CPhysics.GetDefaultTransform(LocalPosition_))
    {
    }
    public CFlyAwayApple(STransform Transform_) :
        base(Transform_, CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Apple.Collider)
    {
    }
    public override EFlyAwayItemType GetItemType()
    {
        return EFlyAwayItemType.Apple;
    }
    public override string GetPrefabName()
    {
        return CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Apple.PrefabName;
    }
}
public class CFlyAwayMeat : CFlyAwayItem
{
    public CFlyAwayMeat(SPoint LocalPosition_) :
        this(CPhysics.GetDefaultTransform(LocalPosition_))
    {
    }
    public CFlyAwayMeat(STransform Transform_) :
        base(Transform_, CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Meat.Collider)
    {
    }
    public override EFlyAwayItemType GetItemType()
    {
        return EFlyAwayItemType.Meat;
    }
    public override string GetPrefabName()
    {
        return CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Meat.PrefabName;
    }
}
public class CFlyAwayChicken : CFlyAwayItem
{
    public CFlyAwayChicken(SPoint LocalPosition_) :
        this(CPhysics.GetDefaultTransform(LocalPosition_))
    {
    }
    public CFlyAwayChicken(STransform Transform_) :
        base(Transform_, CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Chicken.Collider)
    {
    }
    public override EFlyAwayItemType GetItemType()
    {
        return EFlyAwayItemType.Chicken;
    }
    public override string GetPrefabName()
    {
        return CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Chicken.PrefabName;
    }
}
