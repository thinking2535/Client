using rso.core;
using rso.game;
using rso.gameutil;
using rso.physics;
using bb;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TChars = System.Collections.Generic.HashSet<System.Int32>;
using System.IO;

public class CMetaData
{
    public class SQuestDailyComplete
    {
        public SQuestDailyCompleteMeta Meta = null;
        public TimeSpan RefreshDuration;

        public SQuestDailyComplete(SQuestDailyCompleteMeta Meta_)
        {
            Meta = Meta_;
            RefreshDuration = TimeSpan.FromMinutes(Meta_.RefreshMinutes);
        }
    }

    public ulong Checksum = 0;
    public SConfigMeta ConfigMeta = null;
    public string[] ForbiddenWords = null;
    public SSingleBalance SingleBalanceMeta = null;
    public SSingleIslandBalance SingleIslandBalanceMeta = null;
    public List<SingleIslandObject.EItemType> SingleIslandItemPattern = new List<SingleIslandObject.EItemType>();
    public List<SinglePlayObject.EItemType> SingleArrowItemPattern = new List<SinglePlayObject.EItemType>();

    public SMultiIslandBalance MultiIslandBalanceMeta = null;
    public List<SingleIslandObject.EItemType> MultiIslandItemPattern = new List<SingleIslandObject.EItemType>();

    public SMultiBalance MultiBalanceMeta = null;
    public List<SinglePlayObject.EItemType> MultiArrowItemPattern = new List<SinglePlayObject.EItemType>();

    public Dictionary<EMultiItemType, SMultiItemDodge> MultiItemDodgeMetas = new Dictionary<EMultiItemType, SMultiItemDodge>();
    public Dictionary<EMultiItemType, SMultiItemIsland> MultiItemIslandMetas = new Dictionary<EMultiItemType, SMultiItemIsland>();

    public List<SModeEventMeta> ModeEventMetas = new List<SModeEventMeta>();

    public SQuestDailyComplete QuestDailyComplete = null;
    public SShopConfigServerMeta ShopConfigMeta = null;
    public ELanguage Lang { get; private set; } = ELanguage.English;
    public Dictionary<EText, STextSet> TextSets = new Dictionary<EText, STextSet>();
    public Dictionary<EGameRet, STextSet> GameRetSets = new Dictionary<EGameRet, STextSet>();
    public Dictionary<Int32, SCharacterClientMeta> Chars = new Dictionary<Int32, SCharacterClientMeta>();
    public Dictionary<EGrade, SCharacterGradeClientMeta> CharGrades = new Dictionary<EGrade, SCharacterGradeClientMeta>();
    public Dictionary<Int32, SShopInGameMeta> ShopInGameMetas = new Dictionary<Int32, SShopInGameMeta>();
    public Dictionary<string, SShopIAPMeta> ShopIAPMetas = new Dictionary<string, SShopIAPMeta>();
    public Dictionary<Int32, SShopPackageClientMeta> ShopPackageMetas = new Dictionary<Int32, SShopPackageClientMeta>();
    public Dictionary<Int32, SQuestClientMeta> QuestMetas = new Dictionary<Int32, SQuestClientMeta>();
    public Rank<Int32, SRankTierClientMeta> RankMetas = new Rank<Int32, SRankTierClientMeta>();
    public List<SGachaClientMeta> GachaList = new List<SGachaClientMeta>();
    public Dictionary<Int32, SRankRewardViewMeta> RankRewardViewList = new Dictionary<Int32, SRankRewardViewMeta>();
    public List<SRankRewardMeta> RankRewardList = new List<SRankRewardMeta>();
    public Dictionary<Int32, CRewardItem> RewardItems = new Dictionary<Int32, CRewardItem>();
    public Dictionary<string, SCheatMeta> CheatList = new Dictionary<string, SCheatMeta>();
    public Dictionary<Int32, Dictionary<Int32, SGachaRewardClientMeta>> GachaRewardList = new Dictionary<Int32, Dictionary<Int32, SGachaRewardClientMeta>>();
    public Dictionary<EGrade, double> GachaGradeList = new Dictionary<EGrade, double>();
    public Dictionary<EGrade, Dictionary<int, double>> GachaRewardMaxList = new Dictionary<EGrade, Dictionary<int, double>>();
    public Dictionary<ETrackingKey, STrackingMeta> TrackingMetas = new Dictionary<ETrackingKey, STrackingMeta>();
    public List<SRankingRewardMeta> RankingRewardMetas = new List<SRankingRewardMeta>();
    public Dictionary<string, EText> ServerAlarmList = new Dictionary<string, EText>();
    public Dictionary<EGameMode, Int32> GameModeMaxMember = new Dictionary<EGameMode, int>();

    public SMapMeta Maps = new SMapMeta();
    public CMetaData()
    {
        TextAsset t = Resources.Load<TextAsset>("MetaData/Checksum");
        new CStream(t.bytes).Pop(ref Checksum);

        List<SConfigMeta> ConfigMetas = new List<SConfigMeta>();
        t = Resources.Load<TextAsset>("MetaData/Config");
        new CStream(t.bytes).Pop(ref ConfigMetas);
        ConfigMeta = ConfigMetas[0];

        List<SSingleBalance> SingleBalanceMetas = new List<SSingleBalance>();
        t = Resources.Load<TextAsset>("MetaData/Single");
        new CStream(t.bytes).Pop(ref SingleBalanceMetas);
        SingleBalanceMeta = SingleBalanceMetas[0];
        foreach (var i in SingleBalanceMeta.ItemPattern.Split(','))
        {
            switch (i)
            {
                case "coin":
                    SingleArrowItemPattern.Add(SinglePlayObject.EItemType.Coin);
                    break;
                case "goldbar":
                    SingleArrowItemPattern.Add(SinglePlayObject.EItemType.GoldBar);
                    break;
                case "item":
                    SingleArrowItemPattern.Add(SinglePlayObject.EItemType.Item_Stamina);
                    break;
                case "shield":
                    SingleArrowItemPattern.Add(SinglePlayObject.EItemType.Item_Shield);
                    break;
            }
        }

        List<SMultiBalance> MultiBalanceMetas = new List<SMultiBalance>();
        t = Resources.Load<TextAsset>("MetaData/MultiSingle");
        new CStream(t.bytes).Pop(ref MultiBalanceMetas);
        MultiBalanceMeta = MultiBalanceMetas[0];
        foreach (var i in MultiBalanceMeta.ItemPattern.Split(','))
        {
            switch (i)
            {
                case "item":
                    MultiArrowItemPattern.Add(SinglePlayObject.EItemType.Item_Stamina);
                    break;
                case "shield":
                    MultiArrowItemPattern.Add(SinglePlayObject.EItemType.Item_Shield);
                    break;
            }
        }

        List<SSingleIslandBalance> SingleIslandBalanceMetas = new List<SSingleIslandBalance>();
        t = Resources.Load<TextAsset>("MetaData/Island");
        new CStream(t.bytes).Pop(ref SingleIslandBalanceMetas);
        SingleIslandBalanceMeta = SingleIslandBalanceMetas[0];
        foreach (var i in SingleIslandBalanceMeta.ItemPattern.Split(','))
        {
            switch (i)
            {
                case "apple":
                    SingleIslandItemPattern.Add(SingleIslandObject.EItemType.Item_Apple);
                    break;
                case "meat":
                    SingleIslandItemPattern.Add(SingleIslandObject.EItemType.Item_Meat);
                    break;
                case "chicken":
                    SingleIslandItemPattern.Add(SingleIslandObject.EItemType.Item_Chicken);
                    break;
            }
        }

        List<SMultiIslandBalance> MultiIslandBalanceMetas = new List<SMultiIslandBalance>();
        t = Resources.Load<TextAsset>("MetaData/MultiIsland");
        new CStream(t.bytes).Pop(ref MultiIslandBalanceMetas);
        MultiIslandBalanceMeta = MultiIslandBalanceMetas[0];
        foreach (var i in MultiIslandBalanceMeta.ItemPattern.Split(','))
        {
            switch (i)
            {
                case "apple":
                    MultiIslandItemPattern.Add(SingleIslandObject.EItemType.Item_Apple);
                    break;
                case "meat":
                    MultiIslandItemPattern.Add(SingleIslandObject.EItemType.Item_Meat);
                    break;
                case "chicken":
                    MultiIslandItemPattern.Add(SingleIslandObject.EItemType.Item_Chicken);
                    break;
                case "random":
                    MultiIslandItemPattern.Add(SingleIslandObject.EItemType.Item_Random);
                    break;
            }
        }
        var MultiItemMetas = new List<SMultiItemMeta>();
        t = Resources.Load<TextAsset>("MetaData/MultiItem");
        new CStream(t.bytes).Pop(ref MultiItemMetas);

        int RandCountDodge = 0;
        int RandCountIsland = 0;
        foreach (var i in MultiItemMetas)
        {
            RandCountDodge += i.MultiDodgeRand;
            RandCountIsland += i.MultiIslandRand;
            MultiItemDodgeMetas.Add(i.ItemType, new SMultiItemDodge(new SMultiItem(i.ItemType, i.Description), i.MultiDodgeTime, i.MultiDodgeValue, RandCountDodge));
            MultiItemIslandMetas.Add(i.ItemType, new SMultiItemIsland(new SMultiItem(i.ItemType, i.Description), i.MultiIslandTime, i.MultiIslandValue, RandCountIsland));
        }

        var ForbiddenWordMetas = new List<string>();
        t = Resources.Load<TextAsset>("MetaData/ForbiddenWord");
        new CStream(t.bytes).Pop(ref ForbiddenWordMetas);
        ForbiddenWords = ForbiddenWordMetas.Select(s => s.ToLowerInvariant()).ToArray();

        var CheatMetas = new List<SCheatMeta>();
        t = Resources.Load<TextAsset>("MetaData/Cheat");
        new CStream(t.bytes).Pop(ref CheatMetas);
        foreach (var i in CheatMetas)
            CheatList.Add(i.Cheat, i);

        var ServerAlarmMetas = new List<SServerAlarmMeta>();
        t = Resources.Load<TextAsset>("MetaData/ServerAlarm");
        new CStream(t.bytes).Pop(ref ServerAlarmMetas);
        foreach (var i in ServerAlarmMetas)
            ServerAlarmList.Add(i.Key, i.ETextName);

        t = Resources.Load<TextAsset>("MetaData/Text");
        new CStream(t.bytes).Pop(ref TextSets);

        foreach (var i in Enum.GetValues(typeof(EText)))
        {
            if ((EText)i != EText.Null && (EText)i < EText.Max && !TextSets.ContainsKey((EText)i))
                throw new Exception("EText " + ((EText)i).ToString() + " Not Found");
        }

        t = Resources.Load<TextAsset>("MetaData/GameRet");
        new CStream(t.bytes).Pop(ref GameRetSets);

        foreach (var i in Enum.GetValues(typeof(EGameRet)))
        {
            if ((EGameRet)i < EGameRet.Max && !GameRetSets.ContainsKey((EGameRet)i))
                throw new Exception("EGameRet " + ((EGameRet)i).ToString() + " Not Found");
        }

        var CharMetas = new List<SCharacterClientMeta>();
        t = Resources.Load<TextAsset>("MetaData/Character");
        new CStream(t.bytes).Pop(ref CharMetas);
        foreach (var i in CharMetas)
            Chars.Add(i.Code, i);

        var CharGradeMetas = new List<SCharacterGradeClientMeta>();
        t = Resources.Load<TextAsset>("MetaData/CharacterGrade");
        new CStream(t.bytes).Pop(ref CharGradeMetas);
        foreach (var i in CharGradeMetas)
            CharGrades.Add(i.Grade, i);

        t = Resources.Load<TextAsset>("MetaData/Map");
        new CStream(t.bytes).Pop(ref Maps);

        Debug.Log("Loaded MetaData/Map");

        List<SShopConfigServerMeta> ShopConfigMetas = new List<SShopConfigServerMeta>();
        t = Resources.Load<TextAsset>("MetaData/ShopConfig");
        new CStream(t.bytes).Pop(ref ShopConfigMetas);
        ShopConfigMeta = ShopConfigMetas[0];

        var InGameMetas = new List<SShopInGameMeta>();
        t = Resources.Load<TextAsset>("MetaData/Shop");
        new CStream(t.bytes).Pop(ref InGameMetas);
        foreach (var i in InGameMetas)
            ShopInGameMetas.Add(i.Code, i);

        var PackageMetas = new List<SShopPackageClientMeta>();
        t = Resources.Load<TextAsset>("MetaData/ShopPackage");
        new CStream(t.bytes).Pop(ref PackageMetas);
        foreach (var i in PackageMetas)
            ShopPackageMetas.Add(i.Code, i);

        t = Resources.Load<TextAsset>("MetaData/EventTimeModeOpen");
        new CStream(t.bytes).Pop(ref ModeEventMetas);

        var IAPMetas = new List<SShopIAPMeta>();
        t = Resources.Load<TextAsset>("MetaData/ShopCash");
        new CStream(t.bytes).Pop(ref IAPMetas);
        foreach (var i in IAPMetas)
            ShopIAPMetas.Add(i.Pid, i);

        var TempQuestMetas = new List<SQuestClientMeta>();
        t = Resources.Load<TextAsset>("MetaData/Quest");
        new CStream(t.bytes).Pop(ref TempQuestMetas);
        foreach (var meta in TempQuestMetas)
            QuestMetas.Add(meta.Code,meta);
        List<SQuestDailyCompleteMeta> QuestDailyCompleteMetas = new List<SQuestDailyCompleteMeta>();
        t = Resources.Load<TextAsset>("MetaData/QuestDailyComplete");
        new CStream(t.bytes).Pop(ref QuestDailyCompleteMetas);
        QuestDailyComplete = new CMetaData.SQuestDailyComplete(QuestDailyCompleteMetas[0]);

        t = Resources.Load<TextAsset>("MetaData/Gacha");
        new CStream(t.bytes).Pop(ref GachaList);
        var GachaRewardMetas = new List<SGachaRewardClientMeta>();
        t = Resources.Load<TextAsset>("MetaData/GachaReward");
        new CStream(t.bytes).Pop(ref GachaRewardMetas);
        GachaRewardMetas.Sort((x1, x2) => x2.Probability.CompareTo(x1.Probability));
        foreach (var i in GachaRewardMetas)
        {
            var Grade = Chars[i.CharCode].Grade;
            if (GachaRewardMaxList.ContainsKey(Grade))
            {
                if (GachaRewardMaxList[Grade].ContainsKey(i.Code))
                    GachaRewardMaxList[Grade][i.Code] += i.Probability;
                else
                    GachaRewardMaxList[Grade].Add(i.Code, i.Probability);
            }
            else
            {
                GachaRewardMaxList.Add(Grade, new Dictionary<int, double>());
                GachaRewardMaxList[Grade].Add(i.Code, i.Probability);
            }

            if (!GachaRewardList.ContainsKey(i.Code))
                GachaRewardList.Add(i.Code, new Dictionary<int, SGachaRewardClientMeta>());
            GachaRewardList[i.Code].Add(i.CharCode, i);
        }
        var GachaGradeMetas = new List<SGachaGradeMeta>();
        t = Resources.Load<TextAsset>("MetaData/GachaGrade");
        new CStream(t.bytes).Pop(ref GachaGradeMetas);
        foreach (var i in GachaGradeMetas)
            GachaGradeList.Add(i.Grade, i.Probability);

        
        var RankMetaList = new List<SRankTierClientMeta>();
        t = Resources.Load<TextAsset>("MetaData/RankTier");
        new CStream(t.bytes).Pop(ref RankMetaList);
        
        foreach(var i in RankMetaList)
            RankMetas.Add(i.MinPoint, i);

         t = Resources.Load<TextAsset>("MetaData/RankReward");
        new CStream(t.bytes).Pop(ref RankRewardList);

        t = Resources.Load<TextAsset>("MetaData/RankRewardView");
        new CStream(t.bytes).Pop(ref RankRewardViewList);

        var RewardMetas = new List<SKeyRewardMeta>();
        t = Resources.Load<TextAsset>("MetaData/Reward");
        new CStream(t.bytes).Pop(ref RewardMetas);

        foreach(var meta in RewardMetas)
        {
            if (RewardItems.ContainsKey(meta.Code) == false)
                RewardItems.Add(meta.Code, new CRewardItem(new List<SRewardMeta>()));

            RewardItems[meta.Code].Add(meta.Reward);
        }
        var TrackingList = new List<STrackingMeta>();
        t = Resources.Load<TextAsset>("MetaData/Tracking");
        new CStream(t.bytes).Pop(ref TrackingList);
        foreach (var i in TrackingList)
            TrackingMetas.Add(i.ETrackingKey, i);

        t = Resources.Load<TextAsset>("MetaData/RankingReward");
        new CStream(t.bytes).Pop(ref RankingRewardMetas);

        GameModeMaxMember.Add(EGameMode.Solo, 2);
        GameModeMaxMember.Add(EGameMode.DodgeSolo, 2);
        GameModeMaxMember.Add(EGameMode.IslandSolo, 2);
        GameModeMaxMember.Add(EGameMode.SurvivalSmall, 3);
        GameModeMaxMember.Add(EGameMode.TeamSmall, 4);
        GameModeMaxMember.Add(EGameMode.Team, 6);
        GameModeMaxMember.Add(EGameMode.Survival, 6);
    }
    public void ChangeLanguage(ELanguage Language_)
    {
        Lang = Language_;
    }
    public string GetText(EText Text_)
    {
        string TextString = TextSets[Text_].Texts[(Int32)Lang];
        return TextString.Length > 0 ? TextString : TextSets[Text_].Texts[(Int32)ELanguage.English];
    }
    public string GetGameRetText(EGameRet Ret_)
    {
        string TextString = GameRetSets[Ret_].Texts[(Int32)Lang];
        return TextString.Length > 0 ? string.Format(TextString, (Int32)Ret_) : string.Format(GameRetSets[Ret_].Texts[(Int32)ELanguage.English],(Int32)Ret_);
    }
    public string GetCharacterName(Int32 CharCode_)
    {
        return GetText(Chars[CharCode_].ETextName);
    }
    public string GetCharacterIconName(Int32 CharCode_)
    {
        return Chars[CharCode_].IconName;
    }
    public string GetCharacterGrade(Int32 CharCode_)
    {
        var GradeInfo = CharGrades[Chars[CharCode_].Grade];
        return GetText(GradeInfo.ETextGradeName);
    }
    public Color GetCharacterGradeColor(Int32 CharCode_)
    {
        var GradeInfo = CharGrades[Chars[CharCode_].Grade];
        float R, G, B = 0.0f;
        R = (float)GradeInfo.ColorR / 255.0f;
        G = (float)GradeInfo.ColorG / 255.0f;
        B = (float)GradeInfo.ColorB / 255.0f;
        Color GradeColor = new Color(R, G, B);
        return GradeColor;
    }
    public List<SRewardMeta> GetRewardList(Int32 RewardCode)
    {
        return RewardItems[RewardCode].GetList();
    }
    public SCheatMeta CheckCheat(string Text_)
    {
        foreach(var cheat in CheatList)
        {
            if(Text_.Contains(cheat.Key))
            {
                return cheat.Value;
            }
        }
        return null;
    }
    public EText CheckAlarm(string Text_)
    {
        foreach (var Alarm in ServerAlarmList)
        {
            if (Text_.Contains(Alarm.Key))
            {
                return Alarm.Value;
            }
        }
        return EText.Null;
    }
    public double GetGachaPercent(Int32 Index_, Int32 Code_)
    {
        return (((GachaRewardList[GachaList[Index_].RewardCode][Code_].Probability / (GachaRewardMaxList[Chars[Code_].Grade][GachaList[Index_].RewardCode])) * 100.0f) * (double)GachaGradeList[Chars[Code_].Grade]);
    }
    public bool GetHaveAllChar(Int32 Index_, TChars CharsHave_)
    {
        foreach (var i in GachaRewardList[GachaList[Index_].RewardCode])
        {
            if (!CharsHave_.Contains(i.Key))
                return false;
        }

        return true;
    }
    public SMapMulti GetMap(SBattleBeginNetSc Proto_)
    {
        if ((Proto_.BattleType.MemberCount == 1 && (Proto_.BattleType.TeamCount == 2 || Proto_.BattleType.TeamCount == 3)) ||
            (Proto_.BattleType.TeamCount == 2 && Proto_.BattleType.MemberCount == 2))
            return Maps.MapOneOnOnes[Proto_.MapIndex];
        else
            return Maps.MapMulties[Proto_.MapIndex];
    }
    public List<SPoint> GetSPlayerPoses(SBattleBeginNetSc Proto_)
    {
        return GetMap(Proto_).PlayerPoses[Proto_.BattleType.TeamCount].Poses;
    }
    public string GetMapPrefabName(SBattleBeginNetSc Proto_)
    {
        return GetMap(Proto_).PrefabName;
    }
    public STrackingMeta GetTrackingMeta(ETrackingKey TrackingKey_)
    {
        return TrackingMetas[TrackingKey_];
    }
    public string GetPortImagePath()
    {
        return "GUI/Port/";
    }
    public List<SRankingRewardMeta> GetRankingRewardMetas(string Key_)
    {
        List<SRankingRewardMeta> ReturnObjs = new List<SRankingRewardMeta>();
        foreach (var i in RankingRewardMetas)
        {
            if (i.Mode.Equals(Key_))
                ReturnObjs.Add(i);
        }
        return ReturnObjs;
    }
    public SRankingRewardMeta GetRankingRewardMetaBefore(string Key_, Int32 End_)
    {
        SRankingRewardMeta ReturnObj = null;
        foreach (var i in GetRankingRewardMetas(Key_))
        {
            if (i.Mode.Equals(Key_) && End_ > i.End)
                ReturnObj = i;
        }
        return ReturnObj;
    }
}