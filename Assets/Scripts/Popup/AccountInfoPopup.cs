using bb;
using rso.gameutil;
using rso.unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AccountInfoPopup : ModalDialog
{
    [SerializeField] Text _UserName = null;
    [SerializeField] Text _UserID = null;
    [SerializeField] Image _UserPic = null;
    [SerializeField] Text _UserRank = null;
    [SerializeField] Text _UserPoint = null;
    [SerializeField] Image _UserPointGauge = null;

    [SerializeField] Text _UserMaxWinPointName;
    [SerializeField] Text _UserMaxWinPoint;
    [SerializeField] Text _UserBestPlayName;
    [SerializeField] Text _UserBestPlay;
    [SerializeField] Text _UserSoloWinName;
    [SerializeField] Text _UserSoloWin;
    [SerializeField] Text _UserDodgeScoreName;
    [SerializeField] Text _UserDodgeScore;
    [SerializeField] Text _UserFlyawayScoreName;
    [SerializeField] Text _UserFlyawayScore;
    [SerializeField] Text _UserFlyawayBestComboName;
    [SerializeField] Text _UserFlyawayBestCombo;
    [SerializeField] Text _UserIngameKillName;
    [SerializeField] Text _UserIngameKill;
    [SerializeField] Text _UserBlowBalloonName;
    [SerializeField] Text _UserBlowBalloon;
    [SerializeField] Text _UserCharacterText = null;

    protected override void Awake()
    {
        base.Awake();

        _UserName.text = CGlobal.NickName;
        _UserID.text = CGlobal.UID.ToString();

        var SelectedCharMeta = CGlobal.LoginNetSc.GetSelectedCharacterMeta();

        _UserPic.sprite = SelectedCharMeta.GetSprite();
        _UserCharacterText.text = SelectedCharMeta.GetDescriptionText();

        var RankKeyValuePair = CGlobal.MetaData.RankTiers.Get(CGlobal.Point);
        _UserRank.text = RankKeyValuePair.Value.Value.Level.ToString();

        var StringXScale = CGlobal.GetPointGaugeStringAndXScale(CGlobal.Point, RankKeyValuePair.Value);
        _UserPoint.text = StringXScale.Item1;
        _UserPointGauge.transform.localScale = new Vector3(StringXScale.Item2, 1.0f, 1.0f);

        _UserMaxWinPointName.text = CGlobal.MetaData.getText(EText.MyInfoScene_MaxWinPoint);
        _UserMaxWinPoint.text = CGlobal.LoginNetSc.User.PointBest.ToString();

        _UserBestPlayName.text = CGlobal.MetaData.getText(EText.MyInfoScene_BestPlay);
        _UserBestPlay.text = CGlobal.LoginNetSc.User.BattlePointBest.ToString();

        _UserSoloWinName.text = CGlobal.MetaData.getText(EText.MyInfoScene_SoloWin);
        _UserSoloWin.text = CGlobal.LoginNetSc.User.WinCountSolo.ToString();

        _UserDodgeScoreName.text = CGlobal.MetaData.getText(EText.MyInfoScene_DodgeScore);
        _UserDodgeScore.text = CGlobal.LoginNetSc.User.SinglePointBest.ToString();

        _UserFlyawayScoreName.text = CGlobal.MetaData.getText(EText.MyInfoScene_FlyawayScore);
        _UserFlyawayScore.text = CGlobal.LoginNetSc.User.IslandPointBest.ToString();

        _UserFlyawayBestComboName.text = CGlobal.MetaData.getText(EText.MyInfoScene_FlyawayBestCombo);
        _UserFlyawayBestCombo.text = CGlobal.LoginNetSc.User.IslandComboBest.ToString();

        _UserIngameKillName.text = CGlobal.MetaData.getText(EText.MyInfoScene_IngameKill);
        _UserIngameKill.text = CGlobal.LoginNetSc.User.KillTotal.ToString();

        _UserBlowBalloonName.text = CGlobal.MetaData.getText(EText.MyInfoScene_BlowBalloon);
        _UserBlowBalloon.text = CGlobal.LoginNetSc.User.BlowBalloonTotal.ToString();
    }
    public void SetNickname(string Nickname_)
    {
        CGlobal.NickName = Nickname_;
        _UserName.text = Nickname_;
    }
    public async void EditNickName()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        await CGlobal.curScene.pushChangingNicknamePopup();
    }
    public async void Back()
    {
        await backButtonPressed();
    }
    private void CopyText(string TextToCopy_)
    {
        TextEditor editor = new TextEditor
        {
            text = TextToCopy_
        };
        editor.SelectAll();
        editor.Copy();
    }
    public void CopyUID()
    {
        CopyText(_UserID.text);
    }
}
