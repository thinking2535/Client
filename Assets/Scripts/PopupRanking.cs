using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TUID = System.Int64;

public class PopupRanking : MonoBehaviour
{
    [SerializeField] GameObject _MultiScrollView = null;
    [SerializeField] GameObject _MultiScrollContents = null;

    [SerializeField] GameObject _DodgeScrollView = null;
    [SerializeField] GameObject _DodgeScrollContents = null;

    [SerializeField] GameObject _IslandScrollView = null;
    [SerializeField] GameObject _IslandScrollContents = null;

    [SerializeField] Button _MultiBtn = null;
    [SerializeField] Button _DodgeBtn = null;
    [SerializeField] Button _IslandBtn = null;

    [SerializeField] Text _MyRankingMultiNick = null;
    [SerializeField] Text _MyRankingMultiRanking = null;
    [SerializeField] Text _MyRankingMultiRankPointCount = null;
    [SerializeField] RawImage _MyRankkingMultiIcon = null;
    [SerializeField] Image _MyRankkingMultiFlag = null;

    [SerializeField] GameObject _RewardInfo = null;
    [SerializeField] Text _RewardInfoTitle = null;
    [SerializeField] GameObject _RewardInfoMulti = null;
    [SerializeField] GameObject _RewardInfoDodge = null;
    [SerializeField] GameObject _RewardInfoIsland = null;

    [SerializeField] RankingPanel _RankingPanel = null;
    [SerializeField] RewardInfoPanel _RewardInfoPanel = null;

    [SerializeField] Text _NoRankingText = null;
    [SerializeField] GameObject[] _ParentCanvases = null;

    List<RankingPanel> _TeamRankingPanels = new List<RankingPanel>();
    List<RankingPanel> _DodgeRankingPanels = new List<RankingPanel>();
    List<RankingPanel> _IslandRankingPanels = new List<RankingPanel>();

    private Int32 _SelectType = 0;
    private void Awake()
    {
        foreach (var i in CGlobal.MetaData.GetRankingRewardMetas("MULTI"))
        {
            var Panel = UnityEngine.Object.Instantiate<RewardInfoPanel>(_RewardInfoPanel);
            Panel.transform.SetParent(_RewardInfoMulti.transform);
            Panel.transform.localScale = Vector3.one;

            Panel.Init(i);
        }
        foreach (var i in CGlobal.MetaData.GetRankingRewardMetas("ARROW"))
        {
            var Panel = UnityEngine.Object.Instantiate<RewardInfoPanel>(_RewardInfoPanel);
            Panel.transform.SetParent(_RewardInfoDodge.transform);
            Panel.transform.localScale = Vector3.one;

            Panel.Init(i);
        }
        foreach (var i in CGlobal.MetaData.GetRankingRewardMetas("ISLAND"))
        {
            var Panel = UnityEngine.Object.Instantiate<RewardInfoPanel>(_RewardInfoPanel);
            Panel.transform.SetParent(_RewardInfoIsland.transform);
            Panel.transform.localScale = Vector3.one;

            Panel.Init(i);
        }
    }
    void _FixRankingCount(Int32 DataCount_, List<RankingPanel> RannkingPanels_)
    {
        var ChangedSize = DataCount_ - RannkingPanels_.Count;
        for (Int32 i = 0; i < ChangedSize; ++i)
        {
            var RankingPanel = UnityEngine.Object.Instantiate<RankingPanel>(_RankingPanel);
            RankingPanel.transform.SetParent(_DodgeScrollContents.transform);
            RankingPanel.transform.localScale = Vector3.one;

            RannkingPanels_.Add(RankingPanel);
        }

        if (ChangedSize < 0)
            RannkingPanels_.RemoveRange(DataCount_, -ChangedSize);
    }
    public void SetRanking(SRanking Ranking_)
    {
        {
            // Multi ///////////////////////////
            _FixRankingCount(CGlobal.Ranking.RankingUsers.Count, _TeamRankingPanels);

            for (Int32 i = 0; i < _TeamRankingPanels.Count; ++i)
                _TeamRankingPanels[i].InitRankingPanel(CGlobal.Ranking.RankingUsers[i], i + 1);
        }

        {
            // Dodge ///////////////////////////
            _FixRankingCount(CGlobal.Ranking.RankingUserSingles.Count, _DodgeRankingPanels);

            for (Int32 i = 0; i < _DodgeRankingPanels.Count; ++i)
                _DodgeRankingPanels[i].InitRankingPanel(CGlobal.Ranking.RankingUserSingles[i], i + 1);
        }

        {
            // Dodge ///////////////////////////
            _FixRankingCount(CGlobal.Ranking.RankingUserIslands.Count, _IslandRankingPanels);

            for (Int32 i = 0; i < _IslandRankingPanels.Count; ++i)
                _IslandRankingPanels[i].InitRankingPanel(CGlobal.Ranking.RankingUserIslands[i], i + 1);
        }
    }
    public void ShowRankingPopup()
    {
        gameObject.SetActive(true);
        _RewardInfo.SetActive(false);
        OnClickTeam();

        foreach (var Obj in _ParentCanvases)
            Obj.SetActive(false);
    }
    struct SRankingInfo
    {
        public Int32 Ranking;
        public Int32 Point;

        public SRankingInfo(Int32 Ranking_, Int32 Point_)
        {
            Ranking = Ranking_;
            Point = Point_;
        }
    }
    void _SetMyRanking(Int32 DataCount_, Func<SRankingInfo> fGetMyRankingInfo_)
    {
        _NoRankingText.gameObject.SetActive(DataCount_ <= 0);
        _MyRankingMultiNick.text = CGlobal.NickName;

        var MyRankingInfo = fGetMyRankingInfo_();
        if (MyRankingInfo.Ranking != -1)
        {
            _MyRankingMultiRanking.text = (MyRankingInfo.Ranking + 1).ToString();
            _MyRankingMultiRankPointCount.text = MyRankingInfo.Point.ToString();
        }
        else
        {
            _MyRankingMultiRanking.text = "-";
            _MyRankingMultiRankPointCount.text = "-";
        }
        _MyRankkingMultiIcon.texture = Resources.Load<Texture>(CGlobal.MetaData.GetPortImagePath() + CGlobal.MetaData.Chars[CGlobal.LoginNetSc.User.SelectedCharCode].IconName);

        if (CGlobal.LoginNetSc.User.CountryCode.Length > 0)
            _MyRankkingMultiFlag.sprite = Resources.Load<Sprite>("Flag/" + CGlobal.LoginNetSc.User.CountryCode);
        else
            _MyRankkingMultiFlag.sprite = Resources.Load<Sprite>("Flag/unknown");
        _MultiScrollContents.transform.localPosition = Vector3.zero;
    }
    public void SetRankingMulti()
    {
        _SetMyRanking(
            CGlobal.Ranking.RankingUsers.Count,
            () => {
                Int32 MyRankingCount = -1;

                for (Int32 i = 0; i < CGlobal.Ranking.RankingUsers.Count; ++i)
                {
                    if (CGlobal.UID == CGlobal.Ranking.RankingUsers[i].UID)
                    {
                        MyRankingCount = i;
                        break;
                    }
                }

                Int32 Point = 0;
                if (MyRankingCount > -1)
                    Point = CGlobal.Ranking.RankingUsers[MyRankingCount].Point;

                return new SRankingInfo(MyRankingCount, Point);
            });
    }
    public void SetRankingDodge()
    {
        _SetMyRanking(
            CGlobal.Ranking.RankingUserSingles.Count,
            () => {
                Int32 MyRankingCount = -1;

                for (Int32 i = 0; i < CGlobal.Ranking.RankingUserSingles.Count; ++i)
                {
                    if (CGlobal.UID == CGlobal.Ranking.RankingUserSingles[i].UID)
                    {
                        MyRankingCount = i;
                        break;
                    }
                }

                Int32 Point = 0;
                if (MyRankingCount > -1)
                    Point = CGlobal.Ranking.RankingUserSingles[MyRankingCount].Point;

                return new SRankingInfo(MyRankingCount, Point);
            });
    }
    public void SetRankingIsland()
    {
        _SetMyRanking(
            CGlobal.Ranking.RankingUserIslands.Count,
            () => {
                Int32 MyRankingCount = -1;

                for (Int32 i = 0; i < CGlobal.Ranking.RankingUserIslands.Count; ++i)
                {
                    if (CGlobal.UID == CGlobal.Ranking.RankingUserIslands[i].UID)
                    {
                        MyRankingCount = i;
                        break;
                    }
                }

                Int32 Point = 0;
                if (MyRankingCount > -1)
                    Point = CGlobal.Ranking.RankingUserIslands[MyRankingCount].Point;

                return new SRankingInfo(MyRankingCount, Point);
            });
    }
    public void OnClickTeam()
    {
        _SelectType = 1;
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _MultiBtn.interactable = false;
        _DodgeBtn.interactable = true;
        _IslandBtn.interactable = true;
        _MultiScrollView.SetActive(true);
        _DodgeScrollView.SetActive(false);
        _IslandScrollView.SetActive(false);
        _MultiScrollContents.transform.localPosition = Vector3.zero;
        SetRankingMulti();
    }
    public void OnClickDodge()
    {
        _SelectType = 2;
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _MultiBtn.interactable = true;
        _DodgeBtn.interactable = false;
        _IslandBtn.interactable = true;
        _MultiScrollView.SetActive(false);
        _DodgeScrollView.SetActive(true);
        _IslandScrollView.SetActive(false);
        _DodgeScrollContents.transform.localPosition = Vector3.zero;
        SetRankingDodge();
    }
    public void OnClickIsland()
    {
        _SelectType = 3;
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _MultiBtn.interactable = true;
        _DodgeBtn.interactable = true;
        _IslandBtn.interactable = false;
        _MultiScrollView.SetActive(false);
        _DodgeScrollView.SetActive(false);
        _IslandScrollView.SetActive(true);
        _IslandScrollContents.transform.localPosition = Vector3.zero;
        SetRankingIsland();
    }
    public void OnClose()
    {
        if (_RewardInfo.activeSelf)
            _RewardInfo.SetActive(false);
        else
        {
            gameObject.SetActive(false);
            foreach (var Obj in _ParentCanvases)
                Obj.SetActive(true);
        }
    }
    public void OnRewardInfo()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _RewardInfo.SetActive(true);
        switch(_SelectType)
        {
            case 1:
                _RewardInfoMulti.SetActive(true);
                _RewardInfoDodge.SetActive(false);
                _RewardInfoIsland.SetActive(false);
                _RewardInfoTitle.text = CGlobal.MetaData.GetText(EText.RankingScene_Popup_RewardMulti);
                break;
            case 2:
                _RewardInfoMulti.SetActive(false);
                _RewardInfoDodge.SetActive(true);
                _RewardInfoIsland.SetActive(false);
                _RewardInfoTitle.text = CGlobal.MetaData.GetText(EText.RankingScene_Popup_RewardDodge);
                break;
            case 3:
                _RewardInfoMulti.SetActive(false);
                _RewardInfoDodge.SetActive(false);
                _RewardInfoIsland.SetActive(true);
                _RewardInfoTitle.text = CGlobal.MetaData.GetText(EText.RankingScene_Popup_RewardIsland);
                break;
        }
    }
}