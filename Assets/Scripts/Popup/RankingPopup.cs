using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TUID = System.Int64;

[Serializable]
public struct RankingTap
{
    public GameObject ScrollView;
    public GameObject ScrollContents;
    public Button Btn;
}
public class RankingPopup : ModalDialog
{
    [SerializeField] RankingRewardInfoPopup _rankingRewardInfoPopupPrefab;

    [SerializeField] Button _closeButton;
    [SerializeField] Button _barrierDismissibleButton;
    [SerializeField] RankingTap[] _RankingTaps = new RankingTap[(Int32)ERankingType.Max];
    [SerializeField] Text _MyRankingMultiNick = null;
    [SerializeField] Text _MyRankingMultiRanking = null;
    [SerializeField] Text _MyRankingMultiRankPointCount = null;
    [SerializeField] RawImage _MyRankkingMultiIcon = null;
    [SerializeField] Image _MyRankkingMultiFlag = null;
    [SerializeField] RankingPanel _RankingPanel = null;
    [SerializeField] Text _NoRankingText = null;

    private ERankingType _SelectedType = ERankingType.Multi;
    protected override void Awake()
    {
        base.Awake();

        _closeButton.onClick.AddListener(_close);
        _barrierDismissibleButton.onClick.AddListener(_close);

        for (Int32 rankingTypeIndex = 0; rankingTypeIndex < CGlobal.Ranking.RankingUsersArray.Length; ++rankingTypeIndex)
        {
            var RankingUsers = CGlobal.Ranking.RankingUsersArray[rankingTypeIndex].RankingUsers;

            var parent = _RankingTaps[rankingTypeIndex].ScrollContents;
            for (Int32 i = 0; i < RankingUsers.Count; ++i)
            {
                var RankingPanel = UnityEngine.Object.Instantiate<RankingPanel>(_RankingPanel);
                RankingPanel.transform.SetParent(parent.transform);
                RankingPanel.transform.localScale = Vector3.one;

                RankingPanel.InitRankingPanel(RankingUsers[i], i + 1);
            }
        }

        OnClickTeam();
    }
    protected override void OnDestroy()
    {
        _barrierDismissibleButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();

        base.OnDestroy();
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
    void _SelectType(ERankingType Type_)
    {
        _SelectedType = Type_;
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        for (Int32 i = 0; i < (Int32)ERankingType.Max; ++i)
        {
            bool Selected = (ERankingType)i == _SelectedType;

            _RankingTaps[i].Btn.interactable = !Selected;
            _RankingTaps[i].ScrollView.SetActive(Selected);
            _RankingTaps[i].ScrollContents.transform.localPosition = Vector3.zero;
        }

        var RankingUsers = CGlobal.Ranking.RankingUsersArray[(Int32)_SelectedType].RankingUsers;

        _NoRankingText.gameObject.SetActive(RankingUsers.Count <= 0);
        _MyRankingMultiNick.text = CGlobal.NickName;

        Int32 MyRankingCount = -1;

        for (Int32 i = 0; i < RankingUsers.Count; ++i)
        {
            if (CGlobal.UID == RankingUsers[i].UID)
            {
                MyRankingCount = i;
                break;
            }
        }

        Int32 Point = 0;
        if (MyRankingCount > -1)
            Point = RankingUsers[MyRankingCount].Point;

        var MyRankingInfo = new SRankingInfo(MyRankingCount, Point);
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

        _MyRankkingMultiIcon.texture = CGlobal.LoginNetSc.GetSelectedCharacterTexture();
        _MyRankkingMultiFlag.sprite = CGlobal.GetFlagSprite(CGlobal.LoginNetSc.User.CountryCode);
        _RankingTaps[(Int32)_SelectedType].ScrollContents.transform.localPosition = Vector3.zero;
    }
    public void OnClickTeam()
    {
        _SelectType(ERankingType.Multi);
    }
    public void OnClickDodge()
    {
        _SelectType(ERankingType.Single);
    }
    public void OnClickIsland()
    {
        _SelectType(ERankingType.Island);
    }
    async void _close()
    {
        await backButtonPressed();
    }
    public async void OnRewardInfo()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        var rankingRewardInfoPopup = UnityEngine.GameObject.Instantiate(_rankingRewardInfoPopupPrefab);
        rankingRewardInfoPopup.init(_SelectedType);
        await CGlobal.curScene.pushPopup(rankingRewardInfoPopup);
    }
}