using bb;
using rso.unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PopupMyInfo : MonoBehaviour
{
    [SerializeField] Text _UserName = null;
    [SerializeField] Text _UserID = null;
    [SerializeField] Image _UserPic = null;
    [SerializeField] Text _UserRank = null;
    [SerializeField] Text _UserPoint = null;
    [SerializeField] Image _UserPointGauge = null;
    [SerializeField] Text _UserMaxWinPoint = null;
    [SerializeField] Text _UserBestPlay = null;
    [SerializeField] Text _UserSoloWin = null;
    [SerializeField] Text _UserDodgeScore = null;
    [SerializeField] Text _UserDodgeTime = null;
    [SerializeField] Text _UserFlyawayScore = null;
    [SerializeField] Text _UserFlyawayCount = null;
    [SerializeField] Text _UserIngameKill = null;
    [SerializeField] Text _UserBlowBalloon = null;
    [SerializeField] GameObject _CreateBox = null;
    [SerializeField] InputField _NickName = null;
    [SerializeField] GameObject _PriceBG = null;
    [SerializeField] Text _PriceText = null;
    [SerializeField] Text _ChangeDescription = null;
    [SerializeField] GameObject[] _ParentCanvases = null;
    [SerializeField] Text _UserCharacterText = null;

    public void ShowMyInfoPopup()
    {
        gameObject.SetActive(true);
        _UserName.text = CGlobal.NickName;
        _UserID.text = CGlobal.UID.ToString();
        _UserPic.sprite = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + CGlobal.MetaData.Chars[CGlobal.LoginNetSc.User.SelectedCharCode].IconName);
        _UserCharacterText.text = CGlobal.MetaData.GetText(CGlobal.MetaData.Chars[CGlobal.LoginNetSc.User.SelectedCharCode].ETextDescription);

        _PriceText.text = CGlobal.MetaData.ConfigMeta.ChangeNickCostDia.ToString();
        if (CGlobal.LoginNetSc.User.ChangeNickFreeCount > 0)
        {
            _ChangeDescription.gameObject.SetActive(true);
            _PriceBG.SetActive(false);
        }
        else
        {
            _ChangeDescription.gameObject.SetActive(false);
            _PriceBG.SetActive(true);
        }

        var MyPoint = CGlobal.LoginNetSc.User.Point;
        var RankMeta = CGlobal.MetaData.RankMetas.Get(CGlobal.LoginNetSc.User.Point);
        if (RankMeta == null)
            RankMeta = CGlobal.MetaData.RankMetas.First();
        var RankMetaValue = RankMeta.Value.Value;
        var RankMetaNextValue = CGlobal.MetaData.RankMetas.LastOrDefault(
            x => (RankMetaValue.Tier == 1 ?
            (x.Value.Tier == 5 && x.Value.Rank == (ERank)(RankMetaValue.Rank + 1)) :
            (x.Value.Tier == RankMetaValue.Tier - 1 && x.Value.Rank == RankMetaValue.Rank))).Value;

        _UserRank.text = RankMetaValue.Level.ToString();

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

        _UserMaxWinPoint.text = CGlobal.LoginNetSc.User.PointBest.ToString();
        _UserBestPlay.text = CGlobal.LoginNetSc.User.BattlePointBest.ToString();
        _UserSoloWin.text = CGlobal.LoginNetSc.User.WinCountSolo.ToString();
        _UserDodgeScore.text = CGlobal.LoginNetSc.User.SinglePointBest.ToString();
        _UserDodgeTime.text = TimeString(CGlobal.LoginNetSc.User.SingleSecondBest);
        _UserFlyawayScore.text = CGlobal.LoginNetSc.User.IslandPointBest.ToString();
        _UserFlyawayCount.text = CGlobal.LoginNetSc.User.IslandPassedCountBest.ToString();
        _UserIngameKill.text = CGlobal.LoginNetSc.User.KillTotal.ToString();
        _UserBlowBalloon.text = CGlobal.LoginNetSc.User.BlowBalloonTotal.ToString();

        foreach (var Obj in _ParentCanvases)
            Obj.SetActive(false);
    }
    public void SetNickname(string Nickname_)
    {
        CGlobal.NickName = Nickname_;
        _UserName.text = Nickname_;
        if (CGlobal.LoginNetSc.User.ChangeNickFreeCount > 0)
        {
            _ChangeDescription.gameObject.SetActive(true);
            _PriceBG.SetActive(false);
        }
        else
        {
            _ChangeDescription.gameObject.SetActive(false);
            _PriceBG.SetActive(true);
        }
    }
    private string TimeString(Int32 Time_)
    {
        if (Time_ < 0) Time_ = 0;
        string timeString = "";
        var min = Time_ / 60;
        var sec = Time_ % 60;
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
        return timeString;
    }
    public void EditNickName()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        SetNicknameBox(true);
        _PriceText.text = CGlobal.MetaData.ConfigMeta.ChangeNickCostDia.ToString();
        if (CGlobal.LoginNetSc.User.ChangeNickFreeCount > 0)
        {
            _ChangeDescription.gameObject.SetActive(true);
            _PriceBG.SetActive(false);
        }
        else
        {
            _ChangeDescription.gameObject.SetActive(false);
            _PriceBG.SetActive(true);
        }
    }
    public void NickNameChangeSend()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        if (_NickName.text.Equals(CGlobal.NickName))
        {
            CGlobal.SystemPopup.ShowPopup(EText.GlobalPopup_SameNick, PopupSystem.PopupType.Confirm);
            return;
        }

        if (_NickName.text.Length < rso.game.global.c_NickLengthMin ||
            _NickName.text.Length > rso.game.global.c_NickLengthMax)
        {
            CGlobal.SystemPopup.ShowPopup(EText.GlobalPopup_InvalidNickLength, PopupSystem.PopupType.Confirm);
            return;
        }

        var ForbiddenWord = CGlobal.HaveForbiddenWord(_NickName.text.ToLower());
        if (ForbiddenWord != "")
        {
            CGlobal.ShowHaveForbiddenWord(ForbiddenWord);
            return;
        }
        if (CGlobal.LoginNetSc.User.ChangeNickFreeCount <= 0)
        {
            if (!CGlobal.HaveCost(EResource.Dia, CGlobal.MetaData.ConfigMeta.ChangeNickCostDia))
            {
                CGlobal.ShowResourceNotEnough(EResource.Dia);
                return;
            }
        }
        CGlobal.ProgressLoading.VisibleProgressLoading(1.0f);
        CGlobal.NetControl.Send(new SChangeNickNetCs(_NickName.text));
    }
    public void NickNameChangeCancel()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        SetNicknameBox(false);
        if (Debug.isDebugBuild)
        {
            var Cheat = CGlobal.MetaData.CheckCheat(_NickName.text);
            if (Cheat != null)
            {
                if (_NickName.text.Equals("/stair") || _NickName.text.Equals("/스테어"))
                    CGlobal.IsDebugMode = !CGlobal.IsDebugMode;
                else
                    CGlobal.NetControl.Send(new SChatNetCs(_NickName.text.Replace(Cheat.Cheat, Cheat.CheatCommand)));
            }
        }
    }
    public void Back()
    {
        if (_CreateBox.activeSelf)
            SetNicknameBox(false);
        else
        {
            gameObject.SetActive(false);
            foreach (var Obj in _ParentCanvases)
                Obj.SetActive(true);
        }
    }
    public void SetNicknameBox(bool IsActive_)
    {
        _CreateBox.SetActive(IsActive_);
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
