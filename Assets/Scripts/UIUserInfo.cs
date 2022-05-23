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

    Int32 _MyRankPoint = 0;
    SRankTierClientMeta _NextRankMeta = null;
    SRankTierClientMeta _NowRankMeta = null;

    public void InitUserInfo()
    {
        _UserName.text = CGlobal.NickName;
        _UserPic.sprite = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + CGlobal.MetaData.Chars[CGlobal.LoginNetSc.User.SelectedCharCode].IconName);

        _MyRankPoint = CGlobal.LoginNetSc.User.Point;
        SetRankMetas(_MyRankPoint);

        _UserRank.text = _NowRankMeta.Level.ToString();
        //_UserRankIcon.sprite = Resources.Load<Sprite>("Textures/" + _NowRankMeta.TextureName);

        RankUpdate(_MyRankPoint);

    }
    void SetRankMetas(Int32 Point_)
    {
        var RankMeta = CGlobal.MetaData.RankMetas.Get(Point_);
        if (RankMeta == null)
            RankMeta = CGlobal.MetaData.RankMetas.First();

        _NowRankMeta = RankMeta.Value.Value;

        AnalyticsManager.AddRank(_NowRankMeta.Tier, _NowRankMeta.Rank);

        _NextRankMeta = CGlobal.MetaData.RankMetas.LastOrDefault(
            x => (_NowRankMeta.Tier == 1 ?
            (x.Value.Tier == 5 && x.Value.Rank == (ERank)(_NowRankMeta.Rank + 1)) :
            (x.Value.Tier == _NowRankMeta.Tier - 1 && x.Value.Rank == _NowRankMeta.Rank))).Value;
    }
    public void RankUpdate(Int32 RankPoint_)
    {
        if (_NextRankMeta != null)
        {
            if (_MyRankPoint >= _NextRankMeta.MinPoint || _MyRankPoint < _NowRankMeta.MinPoint)
            {
                SetRankMetas(_MyRankPoint);

                if (_MyRankPoint >= _NextRankMeta.MinPoint)
                    CGlobal.Sound.PlayOneShot((Int32)ESound.RankUp);
                _UserRank.text = _NowRankMeta.Level.ToString();
                //_UserRankIcon.sprite = Resources.Load<Sprite>("Textures/" + _NowRankMeta.TextureName);
            }
            _UserPoint.text = string.Format("{0}/{1}", _MyRankPoint, _NextRankMeta.MinPoint);
            _UserPointGauge.transform.localScale = new Vector3((float)(_MyRankPoint - _NowRankMeta.MinPoint) / (float)(_NextRankMeta.MinPoint - _NowRankMeta.MinPoint), 1.0f, 1.0f);
        }
        else
        {
            _UserRank.text = _NowRankMeta.Level.ToString();
            //_UserRankIcon.sprite = Resources.Load<Sprite>("Textures/" + _NowRankMeta.TextureName);
            _UserPoint.text = string.Format("{0}", _MyRankPoint);
            _UserPointGauge.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
    public void CharacterChange()
    {
        _UserPic.sprite = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + CGlobal.MetaData.Chars[CGlobal.LoginNetSc.User.SelectedCharCode].IconName);
    }
}
