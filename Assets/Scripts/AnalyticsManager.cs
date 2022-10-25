using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rso.core;
using bb;

public static class AnalyticsManager
{
    public static string PrefKey_DailyTrackingDate = "DailyTrackingDate";
    public static string PrefKey_singleplay_select_island = "singleplay_select_island";
    public static string PrefKey_singleplay_select_arrow = "singleplay_select_arrow";
    public static string PrefKey_teamplay_select_teamB = "teamplay_select_teamB";
    public static string PrefKey_survivalplay_select = "survivalplay_select";
    public static string PrefKey_oneone_select = "oneone_select";
    public static string PrefKey_survival3pplay_select = "survival3pplay_select";
    public static string PrefKey_twotwo_select = "twotwo_select";
    public static string PrefKey_multiplay_select_island = "multiplay_select_island";
    public static string PrefKey_multiplay_select_arrow = "multiplay_select_arrow";
    public static string PrefKey_quest_reward_complete = "quest_reward_complete";
    public static string PrefKey_app_close = "app_close";
    public static string PrefKey_RankLevel = "RankLevel";
    public static string PrefKey_RankGrade = "RankGrade";
    public static string PrefKey_TutorialTracking = "TutorialTracking";
    public static void DailyInit()
    {
        var Date = CGlobal.GetServerTimePoint().ToDateTime();
        var TimePoint = new TimePoint(PlayerPrefs.GetString(PrefKey_DailyTrackingDate, Date.ToString())).ToDateTime();
        var year = Date.Year - TimePoint.Year;
        var month = Date.Month - TimePoint.Month;
        var day = Date.Day - TimePoint.Day;
        if (year > 0 || month > 0 || day > 0)
        {
            PlayerPrefs.SetString(PrefKey_DailyTrackingDate, Date.ToString());
            PlayerPrefs.SetInt(PrefKey_singleplay_select_island, 0);
            PlayerPrefs.SetInt(PrefKey_singleplay_select_arrow, 0);
            PlayerPrefs.SetInt(PrefKey_teamplay_select_teamB, 0);
            PlayerPrefs.SetInt(PrefKey_survivalplay_select, 0);
            PlayerPrefs.SetInt(PrefKey_oneone_select, 0);
            PlayerPrefs.SetInt(PrefKey_survival3pplay_select, 0);
            PlayerPrefs.SetInt(PrefKey_twotwo_select, 0);
            PlayerPrefs.SetInt(PrefKey_multiplay_select_island, 0);
            PlayerPrefs.SetInt(PrefKey_multiplay_select_arrow, 0);
            PlayerPrefs.SetInt(PrefKey_quest_reward_complete, 0);
            PlayerPrefs.SetInt(PrefKey_app_close, 0);
        }
        else
        {
            AddAppCloseCount();
        }
    }
    public static Int32 AppCloseCount()
    {
        return PlayerPrefs.GetInt(PrefKey_app_close, 0);
    }
    public static Int32 PlayIslandCount()
    {
        return PlayerPrefs.GetInt(PrefKey_singleplay_select_island, 0);
    }
    public static Int32 PlayDodgeCount()
    {
        return PlayerPrefs.GetInt(PrefKey_singleplay_select_arrow, 0);
    }
    public static Int32 PlayTeamCount()
    {
        return PlayerPrefs.GetInt(PrefKey_teamplay_select_teamB, 0);
    }
    public static Int32 PlaySurvivalCount()
    {
        return PlayerPrefs.GetInt(PrefKey_survivalplay_select, 0);
    }
    public static Int32 PlayOneOnOneCount()
    {
        return PlayerPrefs.GetInt(PrefKey_oneone_select, 0);
    }
    public static Int32 PlaySurvivalSmallCount()
    {
        return PlayerPrefs.GetInt(PrefKey_survival3pplay_select, 0);
    }
    public static Int32 PlayTwoOnTwoCount()
    {
        return PlayerPrefs.GetInt(PrefKey_twotwo_select, 0);
    }
    public static Int32 PlayMultiIslandCount()
    {
        return PlayerPrefs.GetInt(PrefKey_multiplay_select_island, 0);
    }
    public static Int32 PlayMultiArrowCount()
    {
        return PlayerPrefs.GetInt(PrefKey_multiplay_select_arrow, 0);
    }
    public static Int32 QuestCompleteCount()
    {
        return PlayerPrefs.GetInt(PrefKey_quest_reward_complete, 0);
    }
    public static Tuple<Int32, ERank> Rank()
    {
        var Level = PlayerPrefs.GetInt(CGlobal.UID.ToString() + PrefKey_RankLevel, 6);
        var Grade = (ERank)PlayerPrefs.GetInt(CGlobal.UID.ToString() + PrefKey_RankGrade, 0);
        return new Tuple<Int32, ERank>(Level, Grade);
    }
    public static Int32 TutorialTracking()
    {
        return PlayerPrefs.GetInt(CGlobal.UID + PrefKey_TutorialTracking, 0);
    }
    public static void AddAppCloseCount()
    {
        PlayerPrefs.SetInt(PrefKey_app_close, AppCloseCount() + 1);
    }
    public static void AddPlayIslandCount()
    {
        if (AnalyticsManager.PlayIslandCount() == 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_island);
        }
        if (AnalyticsManager.AppCloseCount() > 0 && AnalyticsManager.PlayIslandCount() > 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_island_r2);
        }
        AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_island_total);
        PlayerPrefs.SetInt(PrefKey_singleplay_select_island, PlayIslandCount() + 1);
    }
    public static void AddPlayDodgeCount()
    {
        if (AnalyticsManager.PlayDodgeCount() == 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_arrow);
        }
        if (AnalyticsManager.AppCloseCount() > 0 && AnalyticsManager.PlayDodgeCount() > 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_arrow_r2);
        }
        AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_arrow_total);
        PlayerPrefs.SetInt(PrefKey_singleplay_select_arrow, PlayDodgeCount() + 1);
    }
    public static void AddPlayTeamCount()
    {
        if (AnalyticsManager.PlayTeamCount() == 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.teamplay_select_teamB);
        }
        if (AnalyticsManager.AppCloseCount() > 0 && AnalyticsManager.PlayTeamCount() > 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.teamplay_select_teamB_r2);
        }
        AnalyticsManager.TrackingEvent(ETrackingKey.teamplay_select_teamB_total);
        PlayerPrefs.SetInt(PrefKey_teamplay_select_teamB, PlayTeamCount() + 1);
    }
    public static void AddPlaySurvivalCount()
    {
        if (AnalyticsManager.PlaySurvivalCount() == 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.survivalplay_select);
        }
        if (AnalyticsManager.AppCloseCount() > 0 && AnalyticsManager.PlaySurvivalCount() > 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.survivalplay_select_r2);
        }
        AnalyticsManager.TrackingEvent(ETrackingKey.survivalplay_select_total);
        PlayerPrefs.SetInt(PrefKey_survivalplay_select, PlaySurvivalCount() + 1);
    }
    public static void AddPlayOneOnOneCount()
    {
        if (AnalyticsManager.PlayOneOnOneCount() == 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.oneone_select);
        }
        if (AnalyticsManager.AppCloseCount() > 0 && AnalyticsManager.PlayOneOnOneCount() > 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.oneone_select_r2);
        }
        AnalyticsManager.TrackingEvent(ETrackingKey.oneone_select_total);
        PlayerPrefs.SetInt(PrefKey_oneone_select, PlayOneOnOneCount() + 1);
    }
    public static void AddPlaySurvivalSmallCount()
    {
        if (AnalyticsManager.PlaySurvivalSmallCount() == 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.survival3pplay_select);
        }
        if (AnalyticsManager.AppCloseCount() > 0 && AnalyticsManager.PlaySurvivalSmallCount() > 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.survival3pplay_select_r2);
        }
        AnalyticsManager.TrackingEvent(ETrackingKey.survival3pplay_select_total);
        PlayerPrefs.SetInt(PrefKey_survival3pplay_select, PlaySurvivalSmallCount() + 1);
    }
    public static void AddPlayTwoOnTwoCount()
    {
        if (AnalyticsManager.PlayTwoOnTwoCount() == 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.teamplay_select_team2);
        }
        if (AnalyticsManager.AppCloseCount() > 0 && AnalyticsManager.PlayTwoOnTwoCount() > 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.teamplay_select_team2_r2);
        }
        AnalyticsManager.TrackingEvent(ETrackingKey.teamplay_select_team2_total);
        PlayerPrefs.SetInt(PrefKey_twotwo_select, PlayTwoOnTwoCount() + 1);
    }
    public static void AddPlayMultiArrowCount()
    {
        if (AnalyticsManager.PlayMultiArrowCount() == 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_arrowM);
        }
        if (AnalyticsManager.AppCloseCount() > 0 && AnalyticsManager.PlayMultiArrowCount() > 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_arrowM_r2);
        }
        AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_arrowM_total);
        PlayerPrefs.SetInt(PrefKey_multiplay_select_arrow, PlayMultiArrowCount() + 1);
    }
    public static void AddPlayMultiIslandCount()
    {
        if (AnalyticsManager.PlayMultiIslandCount() == 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_islandM);
        }
        if (AnalyticsManager.AppCloseCount() > 0 && AnalyticsManager.PlayMultiIslandCount() > 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_islandM_r2);
        }
        AnalyticsManager.TrackingEvent(ETrackingKey.singleplay_select_islandM_total);
        PlayerPrefs.SetInt(PrefKey_multiplay_select_island, PlayMultiIslandCount() + 1);
    }
    public static void AddQuestCompleteCount()
    {
        if (AnalyticsManager.QuestCompleteCount() == 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.quest_reward_complete_1);
        }
        else if (AnalyticsManager.QuestCompleteCount() == 2)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.quest_reward_complete_3);
        }
        else if (AnalyticsManager.QuestCompleteCount() == 4)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.quest_reward_complete_5);
        }
        else if (AnalyticsManager.QuestCompleteCount() == 6)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.quest_reward_complete_7);
        }
        PlayerPrefs.SetInt(PrefKey_quest_reward_complete, QuestCompleteCount() + 1);
    }
    public static void AddRank(Int32 Level_, ERank Grade_)
    {
        var Rank = AnalyticsManager.Rank();

        if(Rank.Item2 < Grade_)
        {
            PlayerPrefs.SetInt(CGlobal.UID.ToString() + PrefKey_RankLevel, Level_);
            PlayerPrefs.SetInt(CGlobal.UID.ToString() + PrefKey_RankGrade, (Int32)Grade_);
            SentTrackingRank();
        }
        else if(Rank.Item2 == Grade_ && Rank.Item1 > Level_)
        {
            PlayerPrefs.SetInt(CGlobal.UID.ToString() + PrefKey_RankLevel, Level_);
            SentTrackingRank();
        }
    }
    public static void AddTutorialTracking()
    {
        if (TutorialTracking() > 10) return;

        if(TutorialTracking() == 0)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.tutorial_2);
        }
        else if(TutorialTracking() == 4)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.tutorial_3);
        }
        else if(TutorialTracking() == 9)
        {
            AnalyticsManager.TrackingEvent(ETrackingKey.tutorial_4);
        }
        PlayerPrefs.SetInt(CGlobal.UID + PrefKey_TutorialTracking, TutorialTracking() + 1);
    }
    public static void SentTrackingRank()
    {
        var Rank = AnalyticsManager.Rank();
        string TrackingKeyName = "";
        switch (Rank.Item2)
        {
            case ERank.Unranked:
                TrackingKeyName = "unrank_" + Rank.Item1.ToString();
                break;
            case ERank.Bronze:
                TrackingKeyName = "bronze_rank_" + Rank.Item1.ToString();
                break;
            case ERank.Silver:
                TrackingKeyName = "silver_rank_" + Rank.Item1.ToString();
                break;
            case ERank.Gold:
                TrackingKeyName = "gold_rank_" + Rank.Item1.ToString();
                break;
            case ERank.Diamond:
                TrackingKeyName = "dia_rank_" + Rank.Item1.ToString();
                break;
            case ERank.Champion:
                TrackingKeyName = "champion_rank_" + Rank.Item1.ToString();
                break;
            default:
                return;
        }
        ETrackingKey trackingKey = ETrackingKey.Null;
        System.Enum.TryParse<ETrackingKey>(TrackingKeyName, out trackingKey);
        TrackingEvent(trackingKey);
    }
    public static void TrackingEvent(ETrackingKey TrackingKey_)
    {
        if(CGlobal.MetaData.TrackingMetas.ContainsKey(TrackingKey_))
        {
            var Meta = CGlobal.MetaData.GetTrackingMeta(TrackingKey_);
            Firebase.Analytics.FirebaseAnalytics.LogEvent(Meta.EventKey, Meta.Param.ParamKey, Meta.Param.ParamValue);
        }
        else
        {
            Debug.Log("ETrackingKey = null {"+ TrackingKey_.ToString() + "}");
        }
    }
    public static void TrackingUnlockCharacter(Int32 Code_)
    {
        var Meta = CGlobal.MetaData.GetTrackingMeta(ETrackingKey.unblock_character);
        Firebase.Analytics.FirebaseAnalytics.LogEvent(Meta.EventKey, string.Format("{0}_{1}", Meta.Param.ParamKey,Code_), Meta.Param.ParamValue);
    }
}
