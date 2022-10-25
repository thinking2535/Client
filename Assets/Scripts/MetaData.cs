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


public class SReward
{
    public static SReward create(string type, Int32 value)
    {
        var reward = new SReward();

        switch (type)
        {
            case "Gold":
                reward.Resources[(Int32)EResource.Gold] += value;
                break;

            default:
                throw new Exception();
        }

        return reward;
    }

    public Int32[] Resources = CGlobal.getEmptyResources();
    public HashSet<SCharacterMeta> Chars = new HashSet<SCharacterMeta>();

    public Sprite GetSprite()
    {
        return GetFirstUnitReward()?.GetSprite();
    }
    public Sprite getBigSprite()
    {
        return GetFirstUnitReward()?.getBigSprite();
    }
    public string GetText()
    {
        var FirstUnitReward = GetFirstUnitReward();
        return FirstUnitReward == null ? "" : FirstUnitReward.GetText();
    }
    public List<CUnitRewardResource> GetUnitRewardResources()
    {
        // rso todo moneyui  Resource 보상만 있는 것도 아닌데 Resource 타입의 보상만 보여주면 안된댜~.
        // GetUnitRewards() 이 함수를 쓰고 자원, 캐릭 등 모든 타입의 보상을 보여주도록 하고 스크롤 가능하도록(큰 버전, 작은 버전의 프리팹 만들것)
        return Resources.GetUnitRewardResources();
    }
    public List<CUnitReward> GetUnitRewards()
    {
        var UnitRewards = new List<CUnitReward>();

        foreach (var i in Chars)
            UnitRewards.Add(new CUnitRewardCharacter(CGlobal.MetaData.Characters[i.Code], CGlobal.LoginNetSc.doesHaveCharacter(i.Code)));

        UnitRewards.AddRange(Resources.GetUnitRewardResources().Cast<CUnitReward>());

        return UnitRewards;
    }
    public CUnitReward GetFirstUnitReward() // rso todo 제거할것 (여러 단위 보상이 모여있는데 하나만 보여주지 말것)
    {
        if (Chars.Count > 0)
            return new CUnitRewardCharacter(CGlobal.MetaData.Characters[Chars.First().Code], CGlobal.LoginNetSc.doesHaveCharacter(Chars.First().Code));

        return Resources.GetFirstUnitReward();
    }
};
public class SRankReward
{
    public SRankRewardMeta Meta;
    public SReward Reward;

    public SRankReward(SRankRewardMeta meta)
    {
        Meta = meta;
        Reward = SReward.create(meta.rewardType, meta.rewardValue);
    }
};
public class Quest
{
    public QuestTypeValueMeta questTypeValueMeta;
    public SQuestMeta questMeta;
    public SReward reward;

    public Quest(QuestTypeValueMeta questTypeValueMeta, SQuestMeta questMeta)
    {
        this.questTypeValueMeta = questTypeValueMeta;
        this.questMeta = questMeta;
        reward = SReward.create(questMeta.rewardType, questMeta.rewardValue);
    }
    public string iconName => questTypeValueMeta.iconName;
    public string text => CGlobal.MetaData.getTextWithParameters(questTypeValueMeta.textName, questMeta.unitCompleteCount);
    public Int32 completeCount => questMeta.completeCount;
}
public class CMetaData
{
    public class QuestConfig
    {
        public QuestConfigMeta Meta = null;
        public SReward Reward;

        public QuestConfig(QuestConfigMeta Meta_)
        {
            Meta = Meta_;
            Reward = SReward.create(Meta_.dailyRewardType, Meta_.dailyRewardValue);
        }
    }

    public ulong Checksum = 0;
    public SConfigMeta ConfigMeta = null;
    public MultiBattleConfigMeta multiBattleConfigMeta;
    public Int32[] MaxResources { get; private set; }
    public string[] ForbiddenWords = null;

    public QuestConfig questConfig = null;
    public Dictionary<EText, STextSet> TextSets = new Dictionary<EText, STextSet>();
    string[] _languageTexts = new string[(Int32)ELanguage.Max];
    public Dictionary<EGameRet, STextSet> GameRetSets = new Dictionary<EGameRet, STextSet>();
    public Dictionary<Int32, SCharacterMeta> Characters { get; private set; } = new Dictionary<Int32, SCharacterMeta>();
    public Sprite GetCharacterSprite(Int32 Code_)
    {
        return Characters[Code_].GetSprite();
    }
    public Texture GetCharacterTexture(Int32 Code_)
    {
        return Characters[Code_].GetTexture();
    }

    public float MinVelAir { get; private set; } = float.MaxValue;
    public List<SCharacterMeta> shopCharacters{ get; private set; } = new List<SCharacterMeta>();
    Dictionary<EResource, ExchangeValue> _exchangeValues = new Dictionary<EResource, ExchangeValue>();
    public Dictionary<Int32, Quest> questDatas = new Dictionary<Int32, Quest>();
    public CClosedRank<Int32, SRankTierClientMeta> RankTiers = new CClosedRank<Int32, SRankTierClientMeta>();
    public List<SRankReward> RankRewards = new List<SRankReward>();
    public SReward GetRankReward(Int32 PointBest_, Int32 RewardIndex_)
    {
        if (RewardIndex_ > RankRewards.Count - 1)
            return null;

        if (PointBest_ < RankRewards[RewardIndex_].Meta.point)
            return null;

        return RankRewards[RewardIndex_].Reward;
    }
    public Dictionary<ETrackingKey, STrackingMeta> TrackingMetas = new Dictionary<ETrackingKey, STrackingMeta>();
    public CRank<Int32, SReward>[] RankingReward = new CRank<Int32, SReward>[(Int32)ERankingType.Max];
    public Dictionary<string, EText> ServerAlarmList = new Dictionary<string, EText>();
    public ArrowDodgeConfigMeta arrowDodgeConfigMeta;
    public FlyAwayConfigMeta flyAwayConfigMeta;
    public List<SArrowDodgeItemMeta> ArrowDodgeItemMetas = new List<SArrowDodgeItemMeta>();

    CClosedRank<UInt32, EArrowDodgeItemType> _ArrowDodgeItemSelector = new CClosedRank<UInt32, EArrowDodgeItemType>();
    public EArrowDodgeItemType GetRandomArrowDodgeItemType(UInt32 RandomNumber_)
    {
        return _ArrowDodgeItemSelector.Get(RandomNumber_ % _ArrowDodgeItemSelector.Last().Key).Value.Value;
    }
    public List<SFlyAwayItemMeta> FlyAwayItemMetas = new List<SFlyAwayItemMeta>();

    CClosedRank<UInt32, EFlyAwayItemType> _FlyAwayStaminaItemSelector = new CClosedRank<UInt32, EFlyAwayItemType>();
    public EFlyAwayItemType GetRandomFlyAwayStaminaItemType(UInt32 RandomNumber_)
    {
        return _FlyAwayStaminaItemSelector.Get(RandomNumber_ % _FlyAwayStaminaItemSelector.Last().Key).Value.Value;
    }
    public SMapMeta MapMeta = new SMapMeta();
    public CMetaData()
    {
        TextAsset t = Resources.Load<TextAsset>("MetaData/Checksum");
        new CStream(t.bytes).Pop(ref Checksum);

        List<SConfigMeta> ConfigMetas = new List<SConfigMeta>();
        t = Resources.Load<TextAsset>("MetaData/Config");
        new CStream(t.bytes).Pop(ref ConfigMetas);
        ConfigMeta = ConfigMetas[0];
        MaxResources = CGlobal.getFullResources();
        MaxResources[(Int32)EResource.Ticket] = ConfigMeta.MaxTicket;

        var ForbiddenWordMetas = new List<string>();
        t = Resources.Load<TextAsset>("MetaData/ForbiddenWord");
        new CStream(t.bytes).Pop(ref ForbiddenWordMetas);
        ForbiddenWords = ForbiddenWordMetas.Select(s => s.ToLowerInvariant()).ToArray();

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

        {
            var LanguageTextMetas = new List<SLanguageTextMeta>();
            t = Resources.Load<TextAsset>("MetaData/LanguageText");
            new CStream(t.bytes).Pop(ref LanguageTextMetas);

            foreach (var i in LanguageTextMetas)
                _languageTexts[(Int32)i.Language] = i.Text;

            foreach (var i in _languageTexts)
                Debug.Assert(i != null);
        }

        t = Resources.Load<TextAsset>("MetaData/GameRet");
        new CStream(t.bytes).Pop(ref GameRetSets);

        foreach (var i in Enum.GetValues(typeof(EGameRet)))
        {
            if ((EGameRet)i < EGameRet.Max && !GameRetSets.ContainsKey((EGameRet)i))
                throw new Exception("EGameRet " + ((EGameRet)i).ToString() + " Not Found");
        }


        {
            var characterTypeMetas = new Dictionary<string, CharacterTypeClientMeta>();
            t = Resources.Load<TextAsset>("MetaData/CharacterType");
            new CStream(t.bytes).Pop(ref characterTypeMetas);

            foreach (var i in characterTypeMetas)
            {
                if (i.Value.MaxVelAir < MinVelAir)
                    MinVelAir = i.Value.MaxVelAir;

                var gradeInfo = CGlobal.getGradeInfo(i.Value.grade);
                if (gradeInfo.subGradeCount <= i.Value.subGrade)
                    gradeInfo.subGradeCount = i.Value.subGrade + 1;
            }


            var characterMetas = new List<CharacterClientMeta>();
            t = Resources.Load<TextAsset>("MetaData/Character");
            new CStream(t.bytes).Pop(ref characterMetas);

            foreach (var i in characterMetas)
            {
                var characterTypeMeta = characterTypeMetas[i.type];

                Characters.Add(i.Code, new SCharacterMeta(i, characterTypeMeta));

                var characterMeta = Characters[i.Code];

                if (characterMeta.isShopCharacter())
                    shopCharacters.Add(characterMeta);
            }
        }


        {
            // MultiBattleConfig ////////////////////////
            var multiBattleConfigMetas = new List<MultiBattleConfigMeta>();
            t = Resources.Load<TextAsset>("MetaData/MultiBattleConfig");
            new CStream(t.bytes).Pop(ref multiBattleConfigMetas);
            multiBattleConfigMeta = multiBattleConfigMetas[0];


            // ArrowDodge ///////////////////////
            var arrowDodgeConfigMetas = new List<ArrowDodgeConfigMeta>();
            t = Resources.Load<TextAsset>("MetaData/ArrowDodgeConfig");
            new CStream(t.bytes).Pop(ref arrowDodgeConfigMetas);
            Debug.Assert(arrowDodgeConfigMetas.Count > 0, "ArrowDodgeMetas.Length");
            arrowDodgeConfigMeta = arrowDodgeConfigMetas.First();

            // ArrowDodgeItem ////////////////////
            t = Resources.Load<TextAsset>("MetaData/ArrowDodgeItem");
            new CStream(t.bytes).Pop(ref ArrowDodgeItemMetas);
            Debug.Assert(ArrowDodgeItemMetas.Count > 0, "ArrowDodgeItemMetas.Length");

            ArrowDodgeItemMetas.Sort((a, b) => a.ItemType.CompareTo(b.ItemType));

            UInt32 WeightSum = 0;
            foreach (var i in ArrowDodgeItemMetas)
            {
                WeightSum += i.CreateWeight;
                _ArrowDodgeItemSelector.Add(WeightSum, i.ItemType);
            }
        }
        
        
        {
            // FlyAway ///////////////////////
            var flyAwayConfigMetas = new List<FlyAwayConfigMeta>();
            t = Resources.Load<TextAsset>("MetaData/FlyAwayConfig");
            new CStream(t.bytes).Pop(ref flyAwayConfigMetas);
            Debug.Assert(flyAwayConfigMetas.Count > 0, "FlyAwayMetas.Length");
            flyAwayConfigMeta = flyAwayConfigMetas.First();


            // FlyAwayItem ////////////////////
            t = Resources.Load<TextAsset>("MetaData/FlyAwayItem");
            new CStream(t.bytes).Pop(ref FlyAwayItemMetas);
            Debug.Assert(FlyAwayItemMetas.Count > 0, "UnOrderedFlyAwayItemMetas.Length");

            FlyAwayItemMetas.Sort((a, b) => a.ItemType.CompareTo(b.ItemType));

            UInt32 WeightSum = 0;
            foreach (var i in FlyAwayItemMetas)
            {
                if (i.StaminaCreateWeight <= 0)
                    continue;

                WeightSum += i.StaminaCreateWeight;
                _FlyAwayStaminaItemSelector.Add(WeightSum, i.ItemType);
            }
        }

        {
            t = Resources.Load<TextAsset>("MetaData/Map");
            new CStream(t.bytes).Pop(ref MapMeta);
        }

        {
            var shopExchangeMetas = new List<ShopExchangeMeta>();
            t = Resources.Load<TextAsset>("MetaData/ShopExchange");
            new CStream(t.bytes).Pop(ref shopExchangeMetas);
            foreach (var i in shopExchangeMetas)
                _exchangeValues.Add(i.targetResourceType, i.exchangeValue);
        }

        {
            var questTypeValueMetas = new Dictionary<EQuestType, QuestTypeValueMeta>();
            t = Resources.Load<TextAsset>("MetaData/QuestType");
            new CStream(t.bytes).Pop(ref questTypeValueMetas);

            var questMetas = new List<SQuestMeta>();
            t = Resources.Load<TextAsset>("MetaData/Quest");
            new CStream(t.bytes).Pop(ref questMetas);

            foreach (var questMeta in questMetas)
                questDatas.Add(questMeta.Code, new Quest(questTypeValueMetas[questMeta.QuestType], questMeta));


            var QuestConfigMetas = new List<QuestConfigMeta>();
            t = Resources.Load<TextAsset>("MetaData/QuestConfig");
            new CStream(t.bytes).Pop(ref QuestConfigMetas);
            {
                questConfig = new CMetaData.QuestConfig(QuestConfigMetas[0]);
            }
        }

        var RankTierClientMetas = new List<SRankTierClientMeta>();
        t = Resources.Load<TextAsset>("MetaData/RankTier");
        new CStream(t.bytes).Pop(ref RankTierClientMetas);
        
        foreach(var i in RankTierClientMetas)
            RankTiers.Add(i.MaxPoint, i);

        Debug.Assert(RankTiers.Count > 0);

        var RankRewardMetas = new List<SRankRewardMeta>();
        t = Resources.Load<TextAsset>("MetaData/RankReward");
        new CStream(t.bytes).Pop(ref RankRewardMetas);
        Debug.Assert(RankRewardMetas.Count > 0);

        // Check that RewardCode Exist
        foreach (var i in RankRewardMetas)
        {
            RankRewards.Add(new SRankReward(i));
        }

        var TrackingList = new List<STrackingMeta>();
        t = Resources.Load<TextAsset>("MetaData/Tracking");
        new CStream(t.bytes).Pop(ref TrackingList);
        foreach (var i in TrackingList)
            TrackingMetas.Add(i.ETrackingKey, i);

        var RankingRewardMetas = new List<SRankingRewardMeta>();
        t = Resources.Load<TextAsset>("MetaData/RankingReward");
        new CStream(t.bytes).Pop(ref RankingRewardMetas);

        for (Int32 i = 0; i < RankingReward.Length; ++i)
            RankingReward[i] = new CRank<Int32, SReward>();

        foreach (var i in RankingRewardMetas)
        {
            ERankingType RankingType = ERankingType.Null;

            if (i.Mode == "MULTI")
                RankingType = ERankingType.Multi;

            else if (i.Mode == "ARROW")
                RankingType = ERankingType.Single;

            else if (i.Mode == "ISLAND")
                RankingType = ERankingType.Island;
            else
                throw new Exception(string.Format("Invalid Reward Mode[{0}]", i.Mode));

            if (RankingReward[(Int32)RankingType].ContainsKey(i.End))
                throw new Exception();

            RankingReward[(Int32)RankingType].Add(i.End, SReward.create(i.rewardType, i.rewardValue));
        }
    }
    public string getText(EText textName)
    {
        return TextSets[textName].Texts[(Int32)CGlobal.GameOption.Data.Language];
    }
    public string getText(EText textName, ELanguage language)
    {
        return TextSets[textName].Texts[(Int32)language];
    }
    public string getTextWithParameters(EText textName, params object[] parameters)
    {
        object[] filteredParameters = new object[parameters.Length];
        for (Int32 i = 0; i < parameters.Length; ++i)
        {
            if (parameters[i] is EText)
                filteredParameters[i] = getText((EText)parameters[i]);
            else
                filteredParameters[i] = parameters[i];
        }

        return string.Format(getText(textName), parameters);
    }
    public string getCurrentLanguageText()
    {
        return _languageTexts[(Int32)CGlobal.GameOption.Data.Language];
    }
    public string GetGameRetText(EGameRet Ret_)
    {
        string TextString = GameRetSets[Ret_].Texts[(Int32)CGlobal.GameOption.Data.Language];
        return TextString.Length > 0 ? string.Format(TextString, (Int32)Ret_) : string.Format(GameRetSets[Ret_].Texts[(Int32)ELanguage.English],(Int32)Ret_);
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
    public SMultiMap GetMultiMap(SMultiBattleBeginNetSc Proto_)
    {
        return MapMeta.OneOnOneMaps[Proto_.MapIndex];
    }
    public SArrowDodgeMap GetArrowDodgeMap()
    {
        return MapMeta.ArrowDodgeMapInfo.Maps[0];
    }
    public SFlyAwayMap GetFlyAwayMap()
    {
        return MapMeta.FlyAwayMapInfo.Maps[0];
    }
    public List<SPoint> GetSPlayerPoses(SMultiBattleBeginNetSc Proto_)
    {
        return GetMultiMap(Proto_).Poses;
    }
    public string GetMapPrefabName(SMultiBattleBeginNetSc Proto_)
    {
        return GetMultiMap(Proto_).PrefabName;
    }
    public STrackingMeta GetTrackingMeta(ETrackingKey TrackingKey_)
    {
        return TrackingMetas[TrackingKey_];
    }
    public ExchangeValue getExchangeValue(EResource targetResource)
    {
        return _exchangeValues[targetResource];
    }
}