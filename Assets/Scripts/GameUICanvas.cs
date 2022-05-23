using bb;
using rso.net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class FreeForAllScore
{
    public static readonly string[] SpriteNames = {
        "Textures/Lobby_Resources/ico_teamRed",
        "Textures/Lobby_Resources/ico_teamYellow",
        "Textures/Lobby_Resources/ico_teamOrange",
        "Textures/Lobby_Resources/ico_teamBlueGreen",
        "Textures/Lobby_Resources/ico_teamPurple",
        "Textures/Lobby_Resources/ico_teamGreen"
    };

    public Text PointText = null;
    public Text RankText = null;
    public Image RankBG = null;
    public Int32 Score = 0;
    public Int32 Rank = 0;
    public FreeForAllScore(Text Point_, Text Rank_, Image RankBG_)
    {
        PointText = Point_;
        RankText = Rank_;
        RankBG = RankBG_;
        Score = 0;
        Rank = 0;
    }
    public void SetData()
    {
        PointText.text = Score.ToString();
        RankText.text = Rank.ToString();
    }
}

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] GameObject _FreeForAllCanvas = null;
    [SerializeField] GameObject _TeamCanvas = null;
    [SerializeField] GameObject _FreeForAllSmallCanvas = null;
    [SerializeField] Text _BluePoint = null;
    [SerializeField] Text _RedPoint = null;
    [SerializeField] Text _Time = null;
    [SerializeField] Text _TimeCount = null;
    [SerializeField] Int32 _StartCountDown = 10;

    [SerializeField] Text[] _FreeForAllPoint = null;
    [SerializeField] Text[] _FreeForAllRank = null;
    [SerializeField] Image[] _FreeForAllBG = null;

    [SerializeField] Text[] _FreeForAllSmallPoint = null;
    [SerializeField] Text[] _FreeForAllSmallRank = null;
    [SerializeField] Image[] _FreeForAllSmallBG = null;

    [SerializeField] ChainKillMark _ChainKillMark = null;

    [SerializeField] GameObject _GameStartView = null;
    [SerializeField] GameObject _GameEndView = null;

    [SerializeField] Image _JoyPadObject = null;

    [SerializeField] GameObject _FirstHitObject = null;
    [SerializeField] Text _FirstHitUserText = null;

    [SerializeField] GameObject _JoyPad = null;

    [SerializeField] GameObject _EmotionLayer = null;

    Dictionary<Int32, FreeForAllScore> _FreeForAllScoreList = new Dictionary<Int32, FreeForAllScore>();

    //bool _IsSlidingViewMove = false;
    //float _SlidingViewMoveVelocity = 20.0f;
    //float _SlidingViewMoveAcceleration = 20.0f;

    //private float _SlidingMax = 0.0f;
    //private float _SlidingMin = 0.0f;

    //bool _IsOn = false;

    Int32 _CountNum = 0;

    float _MinTimeCountScale = 0.7f;
    float _NowTimeCountScale = 1.0f;
    float _TimeCountScaleAcel = 0.6f;

    bool _IsFirstHitView = false;
    float _FirstViewDelta = 0.0f;

    private void Awake()
    {
        _BluePoint.text = "0";
        _RedPoint.text = "0";

        foreach(var point in _FreeForAllPoint)
            point.text = "0";
        foreach (var rank in _FreeForAllRank)
            rank.text = "0";

        _CountNum = 0;
        _FreeForAllCanvas.SetActive(false);
        _TeamCanvas.SetActive(true);
        _EmotionLayer.SetActive(false);
    }
    public void SetPoint(bool MyTeam_, Int32 Point_)
    {
        if(MyTeam_)
            _BluePoint.text = System.Convert.ToString(Point_);
        else
            _RedPoint.text = System.Convert.ToString(Point_);
    }
    public void SetPoint(Int32 Point_, Int32 TeamIdx_)
    {
        _FreeForAllScoreList[TeamIdx_].Score = Point_;
        var SortList = _FreeForAllScoreList.OrderByDescending(i => i.Value.Score);
        Int32 rank = 1;
        foreach(var i in SortList)
        {
            i.Value.Rank = rank;
            rank++;
            i.Value.SetData();
        }
    }

    public void Init(SBattleType BattleType_, Int32 MyTeam_, Int32 TeamCount_)
    {
        _ChainKillMark.Init();
        _GameStartView.SetActive(false);
        _GameEndView.SetActive(false);
        if (TeamCount_ == 3)
        {
            _FreeForAllCanvas.SetActive(false);
            _TeamCanvas.SetActive(false);
            _FreeForAllSmallCanvas.SetActive(true);

            _FreeForAllScoreList.Add(MyTeam_, new FreeForAllScore(_FreeForAllSmallPoint[0], _FreeForAllSmallRank[0], _FreeForAllSmallBG[0]));
            Int32 count = 1;
            for (Int32 i = 0; i < BattleType_.GetPlayerCount(); ++i)
            {
                if (i == MyTeam_) continue;
                _FreeForAllScoreList.Add(i, new FreeForAllScore(_FreeForAllSmallPoint[count], _FreeForAllSmallRank[count], _FreeForAllSmallBG[count]));
                count++;
            }

            foreach (var scoreData in _FreeForAllScoreList)
            {
                scoreData.Value.RankBG.sprite = Resources.Load<Sprite>(FreeForAllScore.SpriteNames[scoreData.Key]);
            }
        }
        else if (TeamCount_ > 2)
        {
            _FreeForAllCanvas.SetActive(true);
            _TeamCanvas.SetActive(false);
            _FreeForAllSmallCanvas.SetActive(false);

            _FreeForAllScoreList.Add(MyTeam_, new FreeForAllScore(_FreeForAllPoint[0], _FreeForAllRank[0], _FreeForAllBG[0]));
            Int32 count = 1;
            for (Int32 i = 0; i < BattleType_.GetPlayerCount(); ++i)
            {
                if (i == MyTeam_) continue;
                _FreeForAllScoreList.Add(i, new FreeForAllScore(_FreeForAllPoint[count], _FreeForAllRank[count], _FreeForAllBG[count]));
                count++;
            }

            foreach (var scoreData in _FreeForAllScoreList)
            {
                scoreData.Value.RankBG.sprite = Resources.Load<Sprite>(FreeForAllScore.SpriteNames[scoreData.Key]);
            }
        }
        else
        {
            _FreeForAllCanvas.SetActive(false);
            _TeamCanvas.SetActive(true);
            _FreeForAllSmallCanvas.SetActive(false);
        }
        _JoyPad.SetActive(CGlobal.GameOption.Data.IsPad);
    }
    public void ShowGameStart()
    {
        _GameStartView.SetActive(true);
    }
    public void ShowGameEnd()
    {
        _TimeCount.transform.gameObject.SetActive(false);
        _GameEndView.SetActive(true);
    }
    public void HideGameStart()
    {
        _GameStartView.SetActive(false);
    }
    public void ShowChainKill(Int32 Count_, string UserName_, bool SameTeam_, bool IsMe_)
    {
        _ChainKillMark.ShowChainKill(Count_, UserName_, SameTeam_, IsMe_);
    }
    public void ShowFirstHit(string UserName_, bool SameTeam_, bool IsMe_)
    {
        _IsFirstHitView = true;
        _FirstHitObject.SetActive(true);
        _FirstHitUserText.text = UserName_;

        //if (SameTeam_)
        //{
        //    if (IsMe_)
        //        _FirstHitUserText.color = CGlobal.NameColorGreen;
        //    else
        //        _FirstHitUserText.color = CGlobal.NameColorBlue;
        //}
        //else
        //    _FirstHitUserText.color = CGlobal.NameColorRed;
    }

    public void SetTime(Int32 time_)
    {
        if (time_ < 0) time_ = 0;
        string timeString = "";
        var min = time_ / 60;
        var sec = time_ % 60;

        timeString = min.ToString() + ":" + string.Format("{0:D2}", sec);

        //if (min > 0)
        //    timeString = min + ":" + string.Format("{0:D2}", sec);
        //else
        //    timeString = string.Format("{0:D2}", sec);

        _Time.text = timeString;

        if(time_ > _StartCountDown) return;
        else if (time_ == _StartCountDown)
        {
            _TimeCount.transform.gameObject.SetActive(true);
            _TimeCount.text = System.Convert.ToString(time_);
        }

        SetTimeCount(time_);
        TimeCountEffect();
    }
    void SetTimeCount(Int32 count_)
    {
        if(_CountNum > count_)
        {
            _TimeCount.text = System.Convert.ToString(count_);
            CGlobal.Sound.PlayOneShot((Int32)ESound.Countdown);
        }

        _CountNum = count_;
    }
    void TimeCountEffect()
    {
        _NowTimeCountScale -= _TimeCountScaleAcel * Time.deltaTime;
        if (_NowTimeCountScale < _MinTimeCountScale || _NowTimeCountScale > 1.0f)
        {
            _TimeCountScaleAcel *= -1;
        }
        _TimeCount.rectTransform.localScale = new Vector3(_NowTimeCountScale, _NowTimeCountScale, 1.0f);
    }

    public void SetJoyPadVisible(bool IsView_)
    {
        _JoyPadObject.gameObject.SetActive(IsView_);
    }
    private void Update()
    {
        if(_IsFirstHitView)
        {
            _FirstViewDelta += Time.deltaTime;
            if(_FirstViewDelta >= 2.5f)
            {
                _IsFirstHitView = false;
                _FirstHitObject.SetActive(false);
            }
        }
    }
    public void SendEmotion(Int32 Code_)
    {
        CGlobal.NetControl.Send<SBattleIconNetCs>(new SBattleIconNetCs(Code_));
        ViewEmotionLayer();
    }
    public void ViewEmotionLayer()
    {
        _EmotionLayer.SetActive(!_EmotionLayer.activeSelf);
    }
}
