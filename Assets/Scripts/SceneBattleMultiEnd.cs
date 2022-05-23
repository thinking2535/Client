using rso.unity;
using bb;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

class SResultPlayerInfo
{
    public string Nick = "";
    public System.Int64 UID = 0;
    public SCharacterClientMeta Meta = null;
    public Int32 Point = 0;
    public Int32 TeamIndex = 0;
    public Int32 RankPoint = 0;
    public SResultPlayerInfo(string Nick_, long UID_, SCharacterClientMeta Meta_, Int32 Point_, Int32 TeamIndex_, Int32 RankPoint_)
    {
        Nick = Nick_;
        UID = UID_;
        Meta = Meta_;
        Point = Point_;
        TeamIndex = TeamIndex_;
        RankPoint = RankPoint_;
    }
}

public class CSceneBattleMultiEnd : CSceneBase
{
    BattleMultiEndScene _BattleMultiEndScene = null;
    SBattleEndNetSc _Proto;
    SBattleBeginNetSc _BattleProto;
    SBattlePlayer[] _Players = null;
    SCharacterClientMeta[] _PlayerMetas = null;

    Int32 _WinTeamIndex = 0;
    Int32 _TeamCount = 0;
    bool _IsMyTeamWin = false;
    Int32 _MyGold = 0;
    Int32 _MyRankPoint = 0;

    List<SResultPlayerInfo> _WinPlayerInfos = null;
    List<SResultPlayerInfo> _LosePlayerInfos = null;

    Canvas _WinCanvas = null;
    Canvas _LoseCanvas = null;
    Canvas _ResultCanvas = null;
    GameObject[] _CanvasChars = null;
    Text _GoldText = null;
    Text _RankPointText = null;

    Text[] _PlayerNameTexts = null;

    Image _BGImage = null;
    GameObject _ParticleParent = null;
    Text _ResultTitleText = null;
    Text _LoseTitleText = null;
    ResultPlayerInfo[] _ResultPlayerInfos = null; 
    
    GameObject _WinBG = null;
    GameObject _LossBG = null;

    Text _TimerText = null;
    float _ExitDelayTime = 15.0f;
    float _ExitTimeCount = 0.0f;

    private static readonly UnityEngine.Object[] _EffectPrefabs = {
        Resources.Load("FX/00_FXPrefab/FX_FireworkBlue01"), 
        Resources.Load("FX/00_FXPrefab/FX_FireworkGreen01"), 
        Resources.Load("FX/00_FXPrefab/FX_FireworkRed01"),
        Resources.Load("FX/00_FXPrefab/FX_FireworkYellow01") };
    private static readonly float ScaleMax = 300.0f;
    private static readonly float ScaleMin = 100.0f;
    private static readonly float PosXMax = 1900.0f;
    private static readonly float PosXMin = 200.0f;
    private static readonly float PosYMax = 900.0f;
    private static readonly float PosYMin = 600.0f;

    float _EffDeltaTime = 0.0f;
    bool IsDraw = false;
    bool IsClose = false;

    public CSceneBattleMultiEnd(SBattleEndNetSc Proto_, SBattleBeginNetSc BattleProto_, SBattlePlayer[] Players_, SCharacterClientMeta[] PlayerMetas_, Int32 WinTeamIndex_) :
        base("Prefabs/BattleMultiEndScene", Vector3.zero, true)
    {
        _Proto = Proto_;
        _BattleProto = BattleProto_;
        _Players = Players_;
        _PlayerMetas = PlayerMetas_;
        _WinTeamIndex = WinTeamIndex_;
        IsDraw = _WinTeamIndex == -1;
        Int32 loseTeam = 0;
        foreach(var player in _BattleProto.Players)
        {
            if (_TeamCount < player.TeamIndex)
                _TeamCount = player.TeamIndex;
            if(IsDraw && player.UID == CGlobal.UID)
                loseTeam = player.TeamIndex;
        }
        if (IsDraw)
        {
            if (_TeamCount == loseTeam)
                _WinTeamIndex = _TeamCount - 1;
            else
                _WinTeamIndex = loseTeam + 1;
        }
    }
    public override void Dispose()
    {

    }
    public override void Enter()
    {
        _BattleMultiEndScene = _Object.GetComponent<BattleMultiEndScene>();
        _WinCanvas = _BattleMultiEndScene.WinCanvas;
        _LoseCanvas = _BattleMultiEndScene.LoseCanvas;
        _ResultCanvas = _BattleMultiEndScene.ResultCanvas;
        _BGImage = _BattleMultiEndScene.BGImage;
        _ParticleParent = _BattleMultiEndScene.ParticleParent;
        _ResultTitleText = _BattleMultiEndScene.ResultTitleText;
        _LoseTitleText = _BattleMultiEndScene.LoseTitleText;
        _ResultPlayerInfos = _BattleMultiEndScene.ResultPlayerInfos;
        _TimerText = _BattleMultiEndScene.TimerText;
        _ExitDelayTime = _BattleMultiEndScene.ExitDelayTime;
        _ExitTimeCount = 0.0f;
        IsClose = false;
        _TimerText.text = string.Format("{0}", (Int32)_ExitDelayTime);

        _WinBG = _BattleMultiEndScene.WinBG;
        _LossBG = _BattleMultiEndScene.LossBG;

        AnalyticsManager.AddTutorialTracking();

        Int32 WinTeamPoint = 0;
        Int32 LossTeamPoint = 0;
        _WinPlayerInfos = new List<SResultPlayerInfo>();
        _LosePlayerInfos = new List<SResultPlayerInfo>();
        for (Int32 i = 0; i < _BattleProto.Players.Count; ++i)
        {
            var Info = new SResultPlayerInfo(
                _BattleProto.Players[i].Nick,
                _BattleProto.Players[i].UID,
                _PlayerMetas[i],
                _Players[i].Point,
                _BattleProto.Players[i].TeamIndex,
                _Proto.BattleEndPlayers[i].AddedPoint);

            if(CGlobal.UID == _BattleProto.Players[i].UID)
            {
                if (_WinTeamIndex == _BattleProto.Players[i].TeamIndex)
                    _IsMyTeamWin = true;
                else
                    _IsMyTeamWin = false;

                _MyGold = _Proto.BattleEndPlayers[i].AddedGold;
                _MyRankPoint = Info.RankPoint;
                CGlobal.LoginNetSc.User.Resources.AddResource(EResource.Gold, _MyGold);
                CGlobal.AddUserBattleEnd(_MyRankPoint);

                if (CGlobal.LoginNetSc.User.BattlePointBest < Info.Point)
                    CGlobal.LoginNetSc.User.BattlePointBest = Info.Point;
            }

            if (_WinTeamIndex == _BattleProto.Players[i].TeamIndex)
            {
                WinTeamPoint += _Players[i].Point;
                _WinPlayerInfos.Add(Info);
            }
            else
            {
                LossTeamPoint += _Players[i].Point;
                _LosePlayerInfos.Add(Info);
            }

        }
        _WinPlayerInfos.Sort((x1, x2) => x2.Point.CompareTo(x1.Point));
        _LosePlayerInfos.Sort((x1, x2) => x2.Point.CompareTo(x1.Point));

        if (_TeamCount >= 2)
        {
            _CanvasChars = _BattleMultiEndScene.ResultCanvasChars;
            _PlayerNameTexts = _BattleMultiEndScene.ResultPlayerNameTexts;
            _GoldText = _BattleMultiEndScene.ResultGoldText;
            _RankPointText = _BattleMultiEndScene.ResultRankPointText;
            _WinCanvas.gameObject.SetActive(false);
            _LoseCanvas.gameObject.SetActive(false);
            _ResultCanvas.gameObject.SetActive(true);

            var ResultPlayerInfos = new List<SResultPlayerInfo>();
            ResultPlayerInfos.AddRange(_WinPlayerInfos);
            ResultPlayerInfos.AddRange(_LosePlayerInfos);

            foreach (var i in _PlayerNameTexts)
                i.gameObject.SetActive(false);

            foreach (var i in _ResultPlayerInfos)
                i.gameObject.SetActive(false);

            for (Int32 i = 0; i < ResultPlayerInfos.Count; ++i)
                MakeResultPlayerCharacter(i, ResultPlayerInfos[i], true);
        }
        else
        {
            if (_IsMyTeamWin)
            {
                _CanvasChars = _BattleMultiEndScene.WinCanvasChars;
                _PlayerNameTexts = _BattleMultiEndScene.WinPlayerNameTexts;
                _GoldText = _BattleMultiEndScene.WinGoldText;
                _RankPointText = _BattleMultiEndScene.WinRankPointText;
                _WinCanvas.gameObject.SetActive(true);
                _LoseCanvas.gameObject.SetActive(false);
                _ResultCanvas.gameObject.SetActive(false);
                if(_WinPlayerInfos.Count > 1)
                    CGlobal.LoginNetSc.User.WinCountMulti += 1;
                else
                    CGlobal.LoginNetSc.User.WinCountSolo += 1;
            }
            else
            {
                _CanvasChars = _BattleMultiEndScene.LoseCanvasChars;
                _PlayerNameTexts = _BattleMultiEndScene.LosePlayerNameTexts;
                _GoldText = _BattleMultiEndScene.LoseGoldText;
                _RankPointText = _BattleMultiEndScene.LoseRankPointText;
                _WinCanvas.gameObject.SetActive(false);
                _LoseCanvas.gameObject.SetActive(true);
                _ResultCanvas.gameObject.SetActive(false);
                if(IsDraw)
                    _LoseTitleText.text = CGlobal.MetaData.GetText(EText.ResultScene_Text_Draw);
            }
            foreach (var i in _PlayerNameTexts)
                i.gameObject.SetActive(false);

            for (Int32 i = 0; i < _WinPlayerInfos.Count; ++i)
                MakeResultPlayerCharacter(i, _WinPlayerInfos[i], false);

            for (Int32 i = 0; i < _LosePlayerInfos.Count; ++i)
                MakeResultPlayerCharacter(i + 3, _LosePlayerInfos[i], false);
        }
        if (_IsMyTeamWin)
        {
            CGlobal.Sound.PlayOneShot((Int32)ESound.Win);

            _WinBG.SetActive(true);
            _LossBG.SetActive(false);
        }
        else
        {
            CGlobal.Sound.PlayOneShot((Int32)ESound.Lose);

            _WinBG.SetActive(false);
            _LossBG.SetActive(true);
        }

        _GoldText.text = _MyGold.ToString();
    }
    public override bool Update()
    {
        if (_Exit)
            return false;

        if (!_LoseCanvas.gameObject.activeSelf)
        {
            _EffDeltaTime += Time.deltaTime;
            if (_EffDeltaTime > 0.5f)
            {
                _EffDeltaTime -= 0.5f;
                MakeParticleEffect();
            }
        }

        if(!IsClose)
        {
            if (rso.unity.CBase.BackPushed())
            {
                _BattleMultiEndScene.Close();
                IsClose = true;
            }
            _ExitTimeCount += Time.deltaTime;
            _TimerText.text = string.Format("{0}", (Int32)(_ExitDelayTime - _ExitTimeCount));
            if (_ExitTimeCount > _ExitDelayTime)
            {
                _BattleMultiEndScene.Close();
                IsClose = true;
            }
        }

        return true;
    }

    void MakeResultPlayerCharacter(Int32 Idx_, SResultPlayerInfo Player_, bool IsSurvival_)
    {
        Int32 CharCode = Player_.Meta.Code;

        string PrefabPath = string.Format("Prefabs/Char/{0}", Player_.Meta.PrefabName);
        var Prefab = Resources.Load(PrefabPath);
        Debug.Assert(Prefab != null);

        var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "Model_Ch";
        Obj.transform.SetParent(_CanvasChars[Idx_].transform);
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localScale = new Vector3(1, 1, 1);
        Obj.transform.localEulerAngles = new Vector3(0, 0, 0);
        var CharacterModel = Obj.GetComponent<Model_Ch>();

        _PlayerNameTexts[Idx_].text = Player_.Nick;
        _ResultPlayerInfos[Idx_].Init(Player_.Nick, Player_.UID == CGlobal.UID);
        if (Player_.UID == CGlobal.UID)
        {
            _PlayerNameTexts[Idx_].color = Color.green;
            switch(Idx_)
            {
                case 0:
                    _ResultTitleText.text = "1st";
                    if(IsSurvival_)
                        CGlobal.LoginNetSc.User.WinCountSurvival += 1;
                    break;
                case 1:
                    _ResultTitleText.text = "2nd";
                    break;
                case 2:
                    _ResultTitleText.text = "3rd";
                    break;
                default:
                    _ResultTitleText.text = (Idx_ + 1) + "th";
                    break;
            }
            _RankPointText.text = Player_.RankPoint.ToString();
        }
        if(_Players.Length == 3)
        {
            if (Idx_ <= 0)
            {
                CharacterModel.Win();
            }
            else
            {
                CharacterModel.Lose();
            }
        }
        else
        {
            if (Idx_ <= 2)
            {
                CharacterModel.Win();
            }
            else
            {
                CharacterModel.Lose();
            }
        }

        if (IsSurvival_)
        {
            if (Player_.UID == CGlobal.UID)
            {
                _CanvasChars[Idx_].SetActive(true);
                _PlayerNameTexts[Idx_].gameObject.SetActive(true);
            }
            else
            {
                _CanvasChars[Idx_].SetActive(false);
                _PlayerNameTexts[Idx_].gameObject.SetActive(false);
            }
        }
        else
            _PlayerNameTexts[Idx_].gameObject.SetActive(true);
    }

    public void SceneMoveLobby()
    {
        CGlobal.MusicStop();
        CGlobal.SceneSetNext(new CSceneLobby(_MyGold, _MyRankPoint));
    }

    void MakeParticleEffect()
    {
        var PrefabIdx = Random.Range(0, 3);
        var Scale = Random.Range(ScaleMin, ScaleMax);

        var Prefab = _EffectPrefabs[PrefabIdx];
        Debug.Assert(Prefab != null);

        var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "Effect_Friework";
        Obj.transform.SetParent(_ParticleParent.transform);
        Obj.transform.localPosition = new Vector3(Random.Range(PosXMin,PosXMax), Random.Range(PosYMin, PosYMax), 0.0f);
        Obj.transform.localScale = new Vector3(Scale, Scale, Scale);
        Obj.GetComponent<ParticleSystem>().Play();
    }
}
