using rso.core;
using rso.game;
using rso.gameutil;
using bb;
using System;
using TResource = System.Int32;

public static class NetProtocolExtension
{
    public static STimeBoost ZeroTimeBoost = new STimeBoost();
    public static TimePoint ZeroTimePoint = new TimePoint(0);
    public static Int32[] ZeroResources = new Int32[(Int32)EResource.Max];

    public static bool IsSame(this Int32[] lhs_, Int32[] rhs_)
    {
        if (lhs_.Length != rhs_.Length)
            throw new Exception("Invalid Resources Size");

        for (Int32 i = 0; i < lhs_.Length; ++i)
            if (lhs_[i] != rhs_[i])
                return false;

        return true;
    }
    public static Int32[] Copy(this Int32[] lhs_)
    {
        Int32[] ret = new Int32[lhs_.Length];

        for (Int32 i = 0; i < lhs_.Length; ++i)
            ret[i] = lhs_[i];

        return ret;
    }
    public static Int32[] Clear(this Int32[] lhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
            lhs_[i] = 0;

        return lhs_;
    }
    public static Int32[] Set(this Int32[] lhs_, Int32[] rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
            lhs_[i] = rhs_[i];

        return lhs_;
    }
    public static Int32[] Limit(this Int32[] lhs_, Int32[] rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
        {
            if (lhs_[i] < 0)
                lhs_[i] = 0;
            else if (lhs_[i] > rhs_[i])
                lhs_[i] = rhs_[i];
        }

        return lhs_;
    }
    public static Int32[] Add(this Int32[] lhs_, Int32[] rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
            lhs_[i] += rhs_[i];

        return lhs_;
    }
    public static Int32[] GetAdd(this Int32[] lhs_, Int32[] rhs_)
    {
        return lhs_.Copy().Add(rhs_);
    }
    public static Int32[] Sub(this Int32[] lhs_, Int32[] rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
            lhs_[i] -= rhs_[i];

        return lhs_;
    }
    public static Int32[] GetSub(this Int32[] lhs_, Int32[] rhs_)
    {
        return lhs_.Copy().Sub(rhs_);
    }
    public static Int32[] Multi(this Int32[] lhs_, Int32[] rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
            lhs_[i] *= rhs_[i];

        return lhs_;
    }
    public static Int32[] GetMulti(this Int32[] lhs_, Int32[] rhs_)
    {
        return lhs_.Copy().Multi(rhs_);
    }
    public static Int32[] Multi(this Int32[] lhs_, Int32 rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
            lhs_[i] *= rhs_;

        return lhs_;
    }
    public static Int32[] GetMulti(this Int32[] lhs_, Int32 rhs_)
    {
        return lhs_.Copy().Multi(rhs_);
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
        return lhs_.Copy().Div(rhs_);
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
        return lhs_.Copy().Div(rhs_);
    }
    public static bool GreaterThan(this Int32[] lhs_, Int32[] rhs_)
    {
        bool HaveGreater = false;
        for (Int32 i = 0; i < lhs_.Length; ++i)
        {
            if (lhs_[i] < rhs_[i])
                return false;

            if (lhs_[i] > rhs_[i])
                HaveGreater = true;
        }

        return HaveGreater;
    }
    public static bool GreaterThanEqual(this Int32[] lhs_, Int32[] rhs_)
    {
        for (Int32 i = 0; i < lhs_.Length; ++i)
            if (lhs_[i] < rhs_[i])
                return false;

        return true;
    }
    public static string ToArrayString(this Int32[] lhs_)
    {
        string ret = "[";

        if (lhs_.Length > 0)
            ret += lhs_[0].ToString();

        for (Int32 i = 1; i < lhs_.Length; ++i)
        {
            ret += ", ";
            ret += lhs_[i].ToString();
        }

        ret += ']';

        return ret;
    }
    public static Int32[] MakeResources()
    {
        return new Int32[(Int32)EResource.Max];
    }
    public static Int32[] MakeResources(EResource Type_, Int32 Value_)
    {
        var Resources = MakeResources();
        Resources[(Int32)Type_] = Value_;
        return Resources;
    }
    public static Int32[] MakeResources(this SResourceTypeData ResourceTypeData_)
    {
        return MakeResources(ResourceTypeData_.Type, ResourceTypeData_.Data);
    }
    public static Int32 GetResources(this Int32[] resources, EResource eResource)
    {
        return resources[(Int32)eResource];
    }

    public static Int64 c_TicksPerHour = 36000000000;

    public static bool IsEqual(this SVersion Lhs_, SVersion Rhs_)
    {
        return (Lhs_.Main == Rhs_.Main && Lhs_.Data == Rhs_.Data);
    }
    public static bool IsDia(EResource Resource_)
    {
        return (Resource_ == EResource.Dia || Resource_ == EResource.DiaPaid);
    }
    public static bool IsAlive(this SSinglePlayer Player_)
    {
        return Player_.Character.IsAlive();
    }
    public static bool HaveCost(this TResource[] Resources_, EResource CostType_, TResource Cost_)
    {
        if (IsDia(CostType_))
            return (GetDia(Resources_) >= Cost_);
        else
            return (Resources_[(int)CostType_] >= Cost_);
    }
    public static bool HaveCost(this TResource[] Resources_, TResource[] Cost_)
    {
        TResource DiaResource = 0;
        TResource DiaCost = 0;

        for (Int32 i = 0; i < Resources_.Length; ++i)
        {
            if (IsDia((EResource)i))
            {
                DiaResource += Resources_[i];
                DiaCost += Cost_[i];
            }
            else if (Resources_[i] < Cost_[i])
            {
                return false;
            }
        }

        return (DiaResource >= DiaCost);
    }
    public static TResource GetDia(this TResource[] Resources_)
    {
        TResource Dia = 0;

        for (Int32 i = 0; i < Resources_.Length; ++i)
        {
            if (IsDia((EResource)i))
                Dia += Resources_[i];
        }

        return Dia;
    }
    public static void SubDia(this TResource[] Resources_, TResource Dia_)
    {
        for (Int32 i = 0; i < Resources_.Length; ++i)
        {
            if ((EResource)i == EResource.Dia) // DiaPaid 나올때 까지 DiaAdded 에서 차감
            {
                if (Resources_[i] - Dia_ < 0)
                {
                    Dia_ -= Resources_[i];
                    Resources_[i] = 0;
                }
                else
                {
                    Resources_[i] -= Dia_;
                    Dia_ = 0;
                }
            }
            else if ((EResource)i == EResource.DiaPaid)
            {
                if (Resources_[i] - Dia_ < 0)
                    Resources_[i] = 0;
                else
                    Resources_[i] -= Dia_;

                Dia_ = 0;
            }
        }
    }
    public static void AddResource(this TResource[] Resources_, Int32 Index_, TResource Data_)
    {
        if (Resources_[Index_] + Data_ < 0)
            Resources_[Index_] = TResource.MaxValue;
        else
            Resources_[Index_] += Data_;
    }
    public static void AddResource(this TResource[] Resources_, EResource Resource_, TResource Data_)
    {
        AddResource(Resources_, (Int32)Resource_, Data_);
    }
    public static void SubResource(this TResource[] Resources_, Int32 Index_, TResource Data_)
    {
        if (Resources_[Index_] - Data_ < 0)
            Resources_[Index_] = 0;
        else
            Resources_[Index_] -= Data_;
    }
    public static void SubResource(this TResource[] Resources_, EResource Resource_, TResource Data_)
    {
        if (IsDia(Resource_))
            SubDia(Resources_, Data_);
        else
            SubResource(Resources_, (Int32)Resource_, Data_);
    }
    public static void AddResources(this TResource[] Resources_, TResource[] Added_)
    {
        for (Int32 i = 0; i < Resources_.Length; ++i)
            AddResource(Resources_, i, Added_[i]);
    }
    public static void SubResources(this TResource[] Resources_, TResource[] Added_)
    {
        for (Int32 i = 0; i < Resources_.Length; ++i)
        {
            if (IsDia((EResource)i))
                continue;

            SubResource(Resources_, i, Added_[i]);
        }

        SubDia(Resources_, GetDia(Added_));
    }
    public static Int32 GetPlayerCount(this SBattleType BattleType_)
    {
        return BattleType_.TeamCount * BattleType_.MemberCount;
    }
    public static bool IsSolo(this SBattleType BattleType_)
    {
        return (BattleType_.TeamCount == 2 && BattleType_.MemberCount == 1);
    }
}
