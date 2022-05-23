using bb;
using rso.core;
using rso.unity;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CSceneMatching : CSceneBase
{
    MatchingScene _MatchingScene = null;
    UIUserCharacter _UserCharacter = null;
    Text _UserNick = null;
    Image _UserRank = null;
    Image _UserRankIcon = null;
    Text _UserPoint = null;
    Image _UserPointGauge = null;
    Text _MatchingTitle = null;
    GameObject[] _ReadyPlayer = null;
    GameObject[] _ReadyPlayerIcon = null;
    GameObject _ItemDescriptionBG = null;
    Text _ReadyTime = null;
    TimePoint _BeginTime;
    public CSceneMatching() :
        base("Prefabs/MatchingScene", Vector3.zero, true)
    {
    }
    public override void Dispose()
    {
    }
    public override void Enter()
    {
        _MatchingScene = _Object.GetComponent<MatchingScene>();
        _UserCharacter = _MatchingScene.UserCharacter;
        _UserNick = _MatchingScene.UserNick;
        _UserRank = _MatchingScene.UserRank;
        _UserRankIcon = _MatchingScene.UserRankIcon;
        _UserPoint = _MatchingScene.UserPoint;
        _UserPointGauge = _MatchingScene.UserPointGauge;
        _ReadyPlayer = _MatchingScene.ReadyPlayer;
        _ReadyPlayerIcon = _MatchingScene.ReadyPlayerIcon;
        _ReadyTime = _MatchingScene.ReadyTime;
        _ItemDescriptionBG = _MatchingScene.ItemDescriptionBG;
        _ItemDescriptionBG.SetActive(false);
        _MatchingTitle = _MatchingScene.MatchingTitle;

        _ReadyTime.text = "00:00";
        _BeginTime = CGlobal.GetServerTimePoint();
        _MatchingTitle.text = CGlobal.GetPlayModeString(CGlobal.PlayMode);

        foreach (var i in _ReadyPlayer)
            i.SetActive(false);
        int MaxCount = 0;
        switch(CGlobal.PlayMode)
        {
            case EPlayMode.DodgeSolo:
            case EPlayMode.IslandSolo:
                _ItemDescriptionBG.SetActive(true);
                MaxCount = 1;
                break;
            case EPlayMode.Solo:
                MaxCount = 1;
                break;
            case EPlayMode.SurvivalSmall:
                MaxCount = 2;
                break;
            case EPlayMode.TeamSmall:
                MaxCount = 3;
                break;
            case EPlayMode.Team:
            case EPlayMode.Survival:
                MaxCount = 5;
                break;
        }
        for (int i = 0; i < MaxCount; ++i)
            _ReadyPlayer[i].SetActive(true);
        foreach (var i in _ReadyPlayerIcon)
            i.SetActive(false);

        var MyPoint = CGlobal.LoginNetSc.User.Point;
        var RankMeta = CGlobal.MetaData.RankMetas.Get(CGlobal.LoginNetSc.User.Point);
        if (RankMeta == null)
            RankMeta = CGlobal.MetaData.RankMetas.First();
        var RankMetaValue = RankMeta.Value.Value;
        var RankMetaNextValue = CGlobal.MetaData.RankMetas.LastOrDefault(
            x => (RankMetaValue.Tier == 1 ?
            (x.Value.Tier == 5 && x.Value.Rank == (ERank)(RankMetaValue.Rank + 1)) :
            (x.Value.Tier == RankMetaValue.Tier - 1 && x.Value.Rank == RankMetaValue.Rank))).Value;

        _UserRank.sprite = Resources.Load<Sprite>("Textures/Num_" + RankMetaValue.Tier.ToString());
        _UserRankIcon.sprite = Resources.Load<Sprite>("Textures/" + RankMetaValue.TextureName);

        if (RankMetaNextValue != null)
        {
            _UserPoint.text = MyPoint.ToString() + "/" + RankMetaNextValue.MinPoint.ToString();
            _UserPointGauge.transform.localScale = new Vector3((float)(MyPoint - RankMetaValue.MinPoint) / (float)(RankMetaNextValue.MinPoint - RankMetaValue.MinPoint), 1.0f, 1.0f);
        }
        else
        {
            _UserPoint.text = MyPoint.ToString();
            _UserPointGauge.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        Int32 CharCode = CGlobal.LoginNetSc.User.SelectedCharCode;
        _UserCharacter.MakeCharacter(CharCode);

        _UserNick.text = CGlobal.NickName;
    }
    public override bool Update()
    {
        if (_Exit)
            return false;

        if (rso.unity.CBase.BackPushed())
        {
            CGlobal.NetControl.Send(new SBattleOutNetCs());
            return true;
        }
        SetTimeCount();

        //_MatchingScene.ImgLoading.transform.Rotate(new Vector3(0.0f, 0.0f, -360.0f) * Time.deltaTime);
        return true;
    }
    private void SetTimeCount()
    {
        Int32 TimeSec = Mathf.CeilToInt((float)(CGlobal.GetServerTimePoint() - _BeginTime).TotalSeconds);
        if (TimeSec < 0) TimeSec = 0;
        string timeString = "";
        var min = TimeSec / 60;
        var sec = TimeSec % 60;
        if (min >= 60)
        {
            var hour = min / 60;
            min = min % 60;
            timeString = hour.ToString() + ":" + string.Format("{0:D2}", min) + ":" + string.Format("{0:D2}", sec);
        }
        else
        {
            timeString = string.Format("{0:D2}", min) + ":" + string.Format("{0:D2}", sec);
        }
        _ReadyTime.text = timeString;
    }
    public void SetReadyCount(Int32 Count_)
    {
        foreach(var i in _ReadyPlayerIcon)
            i.SetActive(false);

        for (int i = 0; i < Count_; ++i)
            _ReadyPlayerIcon[i].SetActive(true);
    }
}
