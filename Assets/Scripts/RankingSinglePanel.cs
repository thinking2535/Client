using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingSinglePanel : MonoBehaviour
{
    public static readonly float PanelHeight = 80.0f;
    public static readonly float PanelWidth = 1020.0f;
    public static readonly float PanelHeightTerm = 20.0f;

    [SerializeField] Text _RankText = null;
    [SerializeField] Text _RankNick = null;
    [SerializeField] Text _RankScore = null;
    [SerializeField] Image _RankIcon = null;
    [SerializeField] Image _RankFlag = null;
    [SerializeField] GameObject _MyRankBG = null;

    public void InitRankingPanel(SRankingUserSingle Rank_, Int32 Ranking_)
    {
        if(Ranking_ > 0)
            _RankText.text = Ranking_.ToString();
        else
            _RankText.text = "-";
        _RankNick.text = Rank_.Nick;
        _RankScore.text = Rank_.Point.ToString();
        _RankIcon.sprite = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + CGlobal.MetaData.GetCharacterIconName(Rank_.CharCode));

        if (Rank_.CountryCode.Length > 0)
            _RankFlag.sprite = Resources.Load<Sprite>("Flag/" + Rank_.CountryCode);
        else
            _RankFlag.sprite = Resources.Load<Sprite>("Flag/unknown");

        _MyRankBG.SetActive(CGlobal.UID == Rank_.UID);
    }
    public void InitRankingPanel(SRankingUserIsland Rank_, Int32 Ranking_)
    {
        if (Ranking_ > 0)
            _RankText.text = Ranking_.ToString();
        else
            _RankText.text = "-";
        _RankNick.text = Rank_.Nick;
        _RankScore.text = Rank_.Point.ToString();
        _RankIcon.sprite = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + CGlobal.MetaData.GetCharacterIconName(Rank_.CharCode));

        if (Rank_.CountryCode.Length > 0)
            _RankFlag.sprite = Resources.Load<Sprite>("Flag/" + Rank_.CountryCode);
        else
            _RankFlag.sprite = Resources.Load<Sprite>("Flag/unknown");

        _MyRankBG.SetActive(CGlobal.UID == Rank_.UID);
    }
}
