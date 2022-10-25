using rso.core;
using rso.game;
using rso.gameutil;
using bb;
using System;
using TResource = System.Int32;
using rso.Base;
using System.Collections.Generic;
using UnityEngine;

public static class NetProtocolExtension
{
    public static STimeBoost ZeroTimeBoost = new STimeBoost();
    public static TimePoint ZeroTimePoint = new TimePoint(0);
    public static Int32[] ZeroResources = new Int32[(Int32)EResource.Max];

    public static Int32[] Add(this Int32[] lhs_, Int32[] rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
            lhs_[i] += rhs_[i];

        return lhs_;
    }
    public static Int32[] GetAdd(this Int32[] lhs_, Int32[] rhs_)
    {
        return lhs_.GetCopy().Add(rhs_);
    }
    public static Int32[] Sub(this Int32[] lhs_, Int32[] rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
            lhs_[i] -= rhs_[i];

        return lhs_;
    }
    public static Int32[] GetSub(this Int32[] lhs_, Int32[] rhs_)
    {
        return lhs_.GetCopy().Sub(rhs_);
    }
    public static Int32[] Multi(this Int32[] lhs_, Int32[] rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
            lhs_[i] *= rhs_[i];

        return lhs_;
    }
    public static Int32[] GetMulti(this Int32[] lhs_, Int32[] rhs_)
    {
        return lhs_.GetCopy().Multi(rhs_);
    }
    public static Int32[] Multi(this Int32[] lhs_, Int32 rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
            lhs_[i] *= rhs_;

        return lhs_;
    }
    public static Int32[] GetMulti(this Int32[] lhs_, Int32 rhs_)
    {
        return lhs_.GetCopy().Multi(rhs_);
    }
    public static Int32[] Div(this Int32[] lhs_, Int32[] rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
        {
            if (rhs_[i] == 0)
                continue;

            lhs_[i] /= rhs_[i];
        }

        return lhs_;
    }
    public static Int32[] GetDiv(this Int32[] lhs_, Int32[] rhs_)
    {
        return lhs_.GetCopy().Div(rhs_);
    }
    public static Int32[] Div(this Int32[] lhs_, Int32 rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
        {
            if (rhs_ == 0)
                continue;

            lhs_[i] /= rhs_;
        }

        return lhs_;
    }
    public static Int32[] GetDiv(this Int32[] lhs_, Int32 rhs_)
    {
        return lhs_.GetCopy().Div(rhs_);
    }
    public static List<CUnitReward> GetUnitRewards(this Int32[] Self_)
    {
        var UnitRewards = new List<CUnitReward>();

        for (Int32 i = 0; i < (Int32)EResource.Max; ++i)
        {
            if (Self_[i] > 0)
                UnitRewards.Add(new CUnitRewardResource(new SResourceTypeData((EResource)i, Self_[i])));
        }

        return UnitRewards;
    }
    public static List<CUnitRewardResource> GetUnitRewardResources(this Int32[] Self_)
    {
        var UnitRewards = new List<CUnitRewardResource>();

        for (Int32 i = 0; i < (Int32)EResource.Max; ++i)
        {
            if (Self_[i] > 0)
                UnitRewards.Add(new CUnitRewardResource(new SResourceTypeData((EResource)i, Self_[i])));
        }

        return UnitRewards;
    }
    public static CUnitReward GetFirstUnitReward(this Int32[] Self_) // rso todo 제거할것 (여러 단위 보상이 모여있는데 하나만 보여주지 말것)
    {
        for (Int32 i = 0; i < (Int32)EResource.Max; ++i)
        {
            if (Self_[i] > 0)
                return new CUnitRewardResource(new SResourceTypeData((EResource)i, Self_[i]));
        }

        return null;
    }

    public static Int64 c_TicksPerHour = 36000000000;

    public static bool IsEqual(this SVersion Lhs_, SVersion Rhs_)
    {
        return (Lhs_.Main == Rhs_.Main && Lhs_.Data == Rhs_.Data);
    }
    public static bool doesHaveCost(this TResource[] resources, SResourceTypeData cost)
    {
        return resources.doesHaveCost(cost.Type, cost.Data);
    }
    public static bool doesHaveCost(this TResource[] resources, EResource costType, TResource costValue)
    {
        return (resources[(Int32)costType] >= costValue);
    }
    public static bool doesHaveCost(this TResource[] resources, TResource[] cost)
    {
        for (Int32 i = 0; i < resources.Length; ++i)
        {
            if (resources[i] < cost[i])
                return false;
        }

        return true;
    }

    public static void AddResource(this TResource[] self, Int32 index, TResource data)
    {
        if (data > 0)
        {
            if (self[index] + data > CGlobal.MetaData.MaxResources[index] || self[index] + data < 0)
                self[index] = CGlobal.MetaData.MaxResources[index];
            else
                self[index] += data;
        }
        else if (data < 0)
        {
            if (self[index] + data < 0)
                self[index] = 0;
            else
                self[index] += data;
        }
    }
    public static void AddResource(this TResource[] self, EResource resourceType, TResource data)
    {
        AddResource(self, (Int32)resourceType, data);
    }
    public static void AddResource(this TResource[] self, SResourceTypeData value)
    {
        self.AddResource(value.Type, value.Data);
    }
    public static void AddResources(this TResource[] self, TResource[] value)
    {
        for (Int32 i = 0; i < self.Length; ++i)
            AddResource(self, i, value[i]);
    }
    public static Int32 GetAllMemberCount(this SBattleType Self_)
    {
        return Self_.TeamCount * Self_.TeamMemberCount;
    }
    public static bool IsOnoOnOneBattle(this SBattleType Self_)
    {
        return Self_.TeamCount == 2 && Self_.TeamMemberCount == 1;
    }
    public static bool IsMultiBattle(this SBattleType BattleType_)
    {
        return BattleType_.TeamCount >= 2;
    }
    public static void setPoint(this SUserBase Self_, Int32 point)
    {
        Self_.Point = point;

        if (Self_.Point > Self_.PointBest)
            Self_.PointBest = Self_.Point;
    }
    public static bool CanMatchable(this SInvalidDisconnectInfo Self_, TimePoint ServerNow_)
    {
        return (Self_.MatchBlockEndTime <= ServerNow_);
    }
    static List<CUnitReward> _getUnitRewards(this SRewardInfo self)
    {
        var UnitRewards = self.ResourcesLeft.GetSub(CGlobal.LoginNetSc.User.Resources).GetUnitRewards();
        UnitRewards.AddRange(CGlobal.CharCodesToUnitRewards(self.Chars));
        return UnitRewards;
    }
    static void _setRewardDB(this SLoginNetSc self, SRewardInfo rewardInfo)
    {
        CGlobal.LoginNetSc.User.Resources = rewardInfo.ResourcesLeft;
        CGlobal.LoginNetSc.Chars.UnionWith(rewardInfo.Chars);
    }
    public static List<CUnitReward> getUnitRewardsAndSetReward(this SLoginNetSc self, SRewardInfo rewardInfo)
    {
        var UnitRewards = rewardInfo._getUnitRewards();
        self._setRewardDB(rewardInfo);
        return UnitRewards;
    }
    public static List<CUnitReward> getUnitRewardsAndSetResources(this SLoginNetSc self, Int32[] resourcesLeft)
    {
        var unitRewards = resourcesLeft.GetSub(CGlobal.LoginNetSc.User.Resources).GetUnitRewards();
        CGlobal.LoginNetSc.User.Resources = resourcesLeft;
        return unitRewards;
    }
    public static SCharacterMeta GetSelectedCharacterMeta(this SLoginNetSc Self_)
    {
        return CGlobal.MetaData.Characters[Self_.User.SelectedCharCode];
    }
    public static Sprite GetSelectedCharacterSprite(this SLoginNetSc Self_)
    {
        return Self_.GetSelectedCharacterMeta().GetSprite();
    }
    public static Texture GetSelectedCharacterTexture(this SLoginNetSc Self_)
    {
        return Self_.GetSelectedCharacterMeta().GetTexture();
    }
    public static bool doesHaveCharacter(this SLoginNetSc self_, Int32 characterCode)
    {
        return self_.Chars.Contains(characterCode);
    }

    public static bool canFlap(this SCharacter self)
    {
        return self.BalloonCount > 0 && self.StaminaInfo.Stamina >= 1.0f;
    }
    public static bool canPump(this SCharacter self)
    {
        return self.BalloonCount == 0 && self.IsGround && self.Dir == 0;
    }
}
