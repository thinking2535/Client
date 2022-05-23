using rso.unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CSceneRankingReward : CSceneBase
{
    private RankingRewardScene _RankingRewardScene = null;
    private RectTransform _RankTierScrollBarRect = null;
    private GameObject _RankTierScrollContents = null;
    private GameObject _RankTierScrollBar = null;
    private Text _RankTierMyScoreText = null;
    private Text _RankTierMyLevelText = null;
    private RectTransform _RankTierMyScoreNowBarRectTrans = null;
    private RectTransform _RankTierMyScoreOldBarRectTrans = null;
    private MoneyUI _MoneyUI = null;
    private Camera _MainCamera = null;
    private CharacterInfoPopup _CharacterInfoPopup = null;
    RankingRewardPanel _RankingPanel = null;

    List<RankingRewardPanel> _RankingRewardPanels = new List<RankingRewardPanel>();

    private Int32 MaxEndPoint = 0;
    private float _PanelDistance = 300.0f;
    private static readonly float Pedding = 16.0f;
    private static readonly float EndPedding = 550.0f;
    private float MaxBarSize = 0.0f;
    public CSceneRankingReward() :
        base("Prefabs/RankingRewardScene", Vector3.zero, true)
    {
    }
    public override void Dispose()
    {
    }

    public override void Enter()
    {
        _RankingRewardScene = _Object.GetComponent<RankingRewardScene>();
        _RankTierScrollBarRect = _RankingRewardScene.RankTierScrollBarRect;
        _RankTierScrollContents = _RankingRewardScene.RankTierScrollContents;
        _RankTierScrollBar = _RankingRewardScene.RankTierScrollBar;
        _RankTierMyScoreNowBarRectTrans = _RankingRewardScene.RankTierMyScoreNowBar.GetComponent<RectTransform>();
        _RankTierMyScoreOldBarRectTrans = _RankingRewardScene.RankTierMyScoreOldBar.GetComponent<RectTransform>();
        _RankTierMyScoreText = _RankingRewardScene.RankTierMyScoreText;
        _RankTierMyLevelText = _RankingRewardScene.RankTierMyLevelText;
        _PanelDistance = _RankingRewardScene.PanelDistance;
        _RankingPanel = _RankingRewardScene.RankingPanel;
        _MoneyUI = _RankingRewardScene.MoneyUI;
        _MoneyUI.SetResources(CGlobal.LoginNetSc.User.Resources);

        _MainCamera = _RankingRewardScene.MainCamera;
        _CharacterInfoPopup = _RankingRewardScene.CharacterInfoPopup;

        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Rank);

        var newObj = UnityEngine.Object.Instantiate<RankingRewardPanel>(_RankingPanel);
        newObj.transform.SetParent(_RankTierScrollBar.transform);
        newObj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        newObj.transform.localScale = Vector3.one;
        newObj.Init(CGlobal.MetaData.RankMetas[0]);

        Int32 count = 1;
        Int32 RewardStart = -1;
        foreach (var i in CGlobal.MetaData.RankRewardList)
        {
            newObj = UnityEngine.Object.Instantiate<RankingRewardPanel>(_RankingPanel);
            newObj.transform.SetParent(_RankTierScrollBar.transform);
            newObj.transform.localPosition = new Vector3(_PanelDistance * count, 0.0f, 0.0f);
            newObj.transform.localScale = Vector3.one;

            if(CGlobal.MetaData.RankMetas.ContainsKey(i.Point))
                newObj.Init(CGlobal.MetaData.RankMetas[i.Point],i);
            else
                newObj.Init(i);

            if(MaxEndPoint < i.Point)
                MaxEndPoint = i.Point;

            newObj.SetRewardCheckImage(CGlobal.MetaData.RankRewardList.IndexOf(i) <= CGlobal.LoginNetSc.User.LastGotRewardRankIndex);
            if (i.Point <= CGlobal.LoginNetSc.User.PointBest && CGlobal.MetaData.RankRewardList.IndexOf(i) == (CGlobal.LoginNetSc.User.LastGotRewardRankIndex + 1))
            {
                newObj.SetRewardGetCheck(true);
                if (RewardStart == -1)
                    RewardStart = count - 1;
            }
            else
                newObj.SetRewardGetCheck(false);
            _RankingRewardPanels.Add(newObj);
            count++;
        }


        MaxBarSize = CGlobal.MetaData.RankRewardList.Count * _PanelDistance;

        _RankTierMyScoreText.text = CGlobal.LoginNetSc.User.Point.ToString();

        var RankMeta = CGlobal.MetaData.RankMetas.Get(CGlobal.LoginNetSc.User.Point);
        if (RankMeta == null)
            RankMeta = CGlobal.MetaData.RankMetas.First();

        var NowRankMeta = RankMeta.Value.Value;

        _RankTierMyLevelText.text = NowRankMeta.Level.ToString();

        float PosX = 0;
        if (MaxEndPoint < CGlobal.LoginNetSc.User.Point)
            PosX = PointConvert(MaxEndPoint);
        else
            PosX = PointConvert(CGlobal.LoginNetSc.User.Point);

        _RankTierMyScoreNowBarRectTrans.sizeDelta = new Vector2(PosX, _RankTierMyScoreNowBarRectTrans.sizeDelta.y);

        if (RewardStart != -1)
            PosX = (_RankingRewardPanels[RewardStart].transform.localPosition.x * -1.0f + (Screen.width / 2.0f));
        else
            PosX = -PosX;
        _RankTierScrollContents.GetComponent<RectTransform>().anchoredPosition = new Vector3(PosX, 0.0f, 0.0f);

        if (MaxEndPoint < CGlobal.LoginNetSc.User.PointBest)
            PosX = PointConvert(MaxEndPoint);
        else
            PosX = PointConvert(CGlobal.LoginNetSc.User.PointBest);

        _RankTierMyScoreOldBarRectTrans.sizeDelta = new Vector2(PosX, _RankTierMyScoreOldBarRectTrans.sizeDelta.y);

        _RankTierScrollBarRect.sizeDelta = new Vector2(MaxBarSize + Pedding, _RankTierScrollBarRect.sizeDelta.y);
        _RankTierScrollContents.GetComponent<RectTransform>().sizeDelta = new Vector2(MaxBarSize + EndPedding, _RankTierScrollContents.GetComponent<RectTransform>().sizeDelta.y);
    }

    public override bool Update()
    {
        if (_Exit)
            return false;

        if (rso.unity.CBase.BackPushed())
        {
            if (_CharacterInfoPopup.gameObject.activeSelf)
            {
                _CharacterInfoPopup.OnClickBack();
                return true;
            }
            if (_MoneyUI.GetSettingPopup())
            {
                _MoneyUI.SettingPopupClose();
                return true;
            }
            if (CGlobal.RewardPopup.gameObject.activeSelf)
            {
                CGlobal.RewardPopup.OnRecive();
                return true;
            }
            CGlobal.SceneSetNext(new CSceneLobby());
            return false;
        }

        return true;
    }

    private float PointConvert(Int32 Point_)
    {
        Int32 PointCount = 0;
        foreach (var i in CGlobal.MetaData.RankRewardList)
        {
            if (i.Point < Point_)
            {
                PointCount++;
            }
        }
        if (CGlobal.MetaData.RankRewardList.Count > PointCount)
        {
            float NormalPos = _PanelDistance * PointCount;
            float AfterPos = (CGlobal.MetaData.RankRewardList[PointCount].Point - (PointCount > 0 ? CGlobal.MetaData.RankRewardList[PointCount - 1].Point : 0));
            float NowPos = AfterPos - (CGlobal.MetaData.RankRewardList[PointCount].Point - Point_);
            return NormalPos + ((float)(NowPos / AfterPos) * _PanelDistance);
        }    
        else
        {
            return MaxBarSize;
        }
    }
    public void SetRewardList()
    {
        for(var i = 0; i < _RankingRewardPanels.Count; ++i)
        {
            _RankingRewardPanels[i].SetRewardCheckImage(i <= CGlobal.LoginNetSc.User.LastGotRewardRankIndex);
            _RankingRewardPanels[i].SetRewardGetCheck(i <= (CGlobal.LoginNetSc.User.LastGotRewardRankIndex + 1));
            _RankingRewardPanels[i].SetRewardCheckImage(i <= CGlobal.LoginNetSc.User.LastGotRewardRankIndex);
            if (_RankingRewardPanels[i].RankRewardData.Point <= CGlobal.LoginNetSc.User.PointBest && i == (CGlobal.LoginNetSc.User.LastGotRewardRankIndex + 1))
                _RankingRewardPanels[i].SetRewardGetCheck(true);
            else
                _RankingRewardPanels[i].SetRewardGetCheck(false);
        }
    }
    public override void ResourcesUpdate()
    {
        _MoneyUI.SetResources(CGlobal.LoginNetSc.User.Resources);
    }
    public override void ResourcesAction(Int32[] Resources_)
    {
        _MoneyUI.ShowRewardEffect(Resources_);
    }
    public void GetCharacter(Int32 Code_)
    {
        _CharacterInfoPopup.ShowGetPopup(Code_, _MainCamera, false);
    }
}
