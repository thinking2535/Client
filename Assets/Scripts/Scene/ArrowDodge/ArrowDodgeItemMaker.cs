using bb;
using rso.gameutil;
using rso.physics;
using rso.unity;
using System;
using UnityEngine;

public class CArrowDodgeItemMaker
{
    CFixedRandom32 _FixedRandom;
    Int64 _NextItemTick;
    public CArrowDodgeItemMaker(CFixedRandom32 FixedRandom_, Int64 NextItemTick_)
    {
        _FixedRandom = FixedRandom_;
        _NextItemTick = NextItemTick_;
    }
    public void FixedUpdate(Int64 tick, ArrowDodgeBattleScene battle, Action<CArrowDodgeItem> fAddItem_)
    {
        if (tick > _NextItemTick)
        {
            _NextItemTick += CGlobal.MetaData.arrowDodgeConfigMeta.ItemRegenPeriodTick;

            // 서버와 맞추기 위해 순서대로 호출되어야 함
            var RandomPosition = battle.getRandomItemPoint();
            var RandomType = CGlobal.MetaData.GetRandomArrowDodgeItemType(_FixedRandom.Get());

            fAddItem_(MakeItem(RandomPosition, RandomType));
        }
    }
    public static CArrowDodgeItem MakeItem(SPoint LocalPosition_, EArrowDodgeItemType ItemType_)
    {
        switch (ItemType_)
        {
            case EArrowDodgeItemType.Coin:
                return new CArrowDodgeCoin(LocalPosition_);

            case EArrowDodgeItemType.GoldBar:
                return new CArrowDodgeGoldBar(LocalPosition_);

            case EArrowDodgeItemType.Shield:
                return new CArrowDodgeShield(LocalPosition_);

            case EArrowDodgeItemType.Stamina:
                return new CArrowDodgeStamina(LocalPosition_);

            default:
                throw new Exception();
        }
    }
}