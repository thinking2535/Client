using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIUserInfo : MonoBehaviour
{
    [SerializeField] Text _UserName = null;
    [SerializeField] Image _UserPic = null;
    [SerializeField] Text _UserRank = null;
    [SerializeField] Text _UserPoint = null;
    [SerializeField] Image _UserPointGauge = null;

    KeyValuePair<Int32, SRankTierClientMeta>? _LastRankKeyValuePair;

    public void InitUserInfo()
    {
        _UserName.text = CGlobal.NickName;
        _UserPic.sprite = CGlobal.LoginNetSc.GetSelectedCharacterSprite();
        RankUpdate(CGlobal.Point);
    }
    public void RankUpdate(Int32 RankPoint_)
    {
        var NewRankKeyValuePair = CGlobal.MetaData.RankTiers.Get(RankPoint_);
        if (_LastRankKeyValuePair == null || NewRankKeyValuePair.Value.Key != _LastRankKeyValuePair.Value.Key)
        {
            if (_LastRankKeyValuePair != null && NewRankKeyValuePair.Value.Key > _LastRankKeyValuePair.Value.Key)
                CGlobal.Sound.PlayOneShot((Int32)ESound.RankUp);

            _UserRank.text = NewRankKeyValuePair.Value.Value.Level.ToString();
            _LastRankKeyValuePair = NewRankKeyValuePair.Value;
        }

        var StringXScale = CGlobal.GetPointGaugeStringAndXScale(CGlobal.LoginNetSc.User.Point, _LastRankKeyValuePair.Value);
        _UserPoint.text = StringXScale.Item1;
        _UserPointGauge.transform.localScale = new Vector3(StringXScale.Item2, 1.0f, 1.0f);
    }
    public void CharacterChange()
    {
        _UserPic.sprite = CGlobal.LoginNetSc.GetSelectedCharacterSprite();
    }
}
