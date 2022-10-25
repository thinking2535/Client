using bb;
using rso.Base;
using rso.unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RankingRewardScene : MoneyUIScene
{
    private RectTransform _RankTierMyScoreNowBarRectTrans = null;
    private RectTransform _RankTierMyScoreOldBarRectTrans = null;

    List<RankingRewardPanel> _RankingRewardPanels = new List<RankingRewardPanel>();

    private static readonly float Pedding = 16.0f;
    private static readonly float EndPedding = 550.0f;
    private float MaxBarSize = 0.0f;

    [SerializeField] Text _title;
    [SerializeField] CanvasScaler _rankingRewardCanvasScaler;
    [SerializeField] Button _BackButton;
    public GameObject RankTierScrollContents = null;
    public RectTransform RankTierScrollBarRect = null;
    public GameObject RankTierScrollBar = null;
    public GameObject RankTierMyScoreNowBar = null;
    public GameObject RankTierMyScoreOldBar = null;
    public Text RankTierMyScoreText = null;
    public Text RankTierMyLevelText = null;
    public float PanelDistance = 300.0f;
    public RankingRewardPanel RankingPanel = null;

    protected override void Awake()
    {
        base.Awake();

        _title.text = CGlobal.MetaData.getText(EText.Global_Text_Rank);
    }

    public new void init()
    {
        base.init();

        _BackButton.onClick.AddListener(Back);

        _RankTierMyScoreNowBarRectTrans = RankTierMyScoreNowBar.GetComponent<RectTransform>();
        _RankTierMyScoreOldBarRectTrans = RankTierMyScoreOldBar.GetComponent<RectTransform>();

        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Rank);

        var newObj = UnityEngine.Object.Instantiate<RankingRewardPanel>(RankingPanel);
        newObj.transform.SetParent(RankTierScrollBar.transform);
        newObj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        newObj.transform.localScale = Vector3.one;

        newObj.InitForFirst();

        Int32 count = 1;
        for (Int32 i = 0; i < CGlobal.MetaData.RankRewards.Count; ++i)
        {
            newObj = UnityEngine.Object.Instantiate<RankingRewardPanel>(RankingPanel);
            newObj.transform.SetParent(RankTierScrollBar.transform);
            newObj.transform.localPosition = new Vector3(PanelDistance * count, 0.0f, 0.0f);
            newObj.transform.localScale = Vector3.one;
            newObj.Init(i);
            _RankingRewardPanels.Add(newObj);
            count++;
        }

        MaxBarSize = CGlobal.MetaData.RankRewards.Count * PanelDistance;

        RankTierMyScoreText.text = CGlobal.LoginNetSc.User.Point.ToString();

        var RankMeta = CGlobal.MetaData.RankTiers.Get(CGlobal.LoginNetSc.User.Point);
        var NowRankMeta = RankMeta.Value.Value;

        RankTierMyLevelText.text = NowRankMeta.Level.ToString();

        var maxPoint = CGlobal.MetaData.RankRewards.Last().Meta.point;

        {
            float bestPointXPosition;
            if (maxPoint < CGlobal.LoginNetSc.User.PointBest)
                bestPointXPosition = PointConvert(maxPoint);
            else
                bestPointXPosition = PointConvert(CGlobal.LoginNetSc.User.PointBest);

            _RankTierMyScoreOldBarRectTrans.sizeDelta = new Vector2(bestPointXPosition, _RankTierMyScoreOldBarRectTrans.sizeDelta.y);
        }

        {
            float currentPointXPosition;
            if (maxPoint < CGlobal.LoginNetSc.User.Point)
                currentPointXPosition = PointConvert(maxPoint);
            else
                currentPointXPosition = PointConvert(CGlobal.LoginNetSc.User.Point);

            _RankTierMyScoreNowBarRectTrans.sizeDelta = new Vector2(currentPointXPosition, _RankTierMyScoreNowBarRectTrans.sizeDelta.y);

            var nextRewardItem = CGlobal.LoginNetSc.User.NextRewardRankIndex < _RankingRewardPanels.Count ? _RankingRewardPanels[CGlobal.LoginNetSc.User.NextRewardRankIndex] : null;
            bool canReward = nextRewardItem != null && CGlobal.LoginNetSc.User.Point >= nextRewardItem.rankReward.Meta.point;
            var scrollXPosition = canReward ? nextRewardItem.transform.localPosition.x : currentPointXPosition;

            // fix scrollXPosition to center
            {
                scrollXPosition = -scrollXPosition;
                scrollXPosition -= RankTierScrollBarRect.anchoredPosition.x; // remove left margin
                scrollXPosition += (_rankingRewardCanvasScaler.referenceResolution.x * 0.5f); // add half width
            }
            RankTierScrollContents.GetComponent<RectTransform>().anchoredPosition = new Vector3(scrollXPosition, 0.0f, 0.0f);
        }

        RankTierScrollBarRect.sizeDelta = new Vector2(MaxBarSize + Pedding, RankTierScrollBarRect.sizeDelta.y);
        RankTierScrollContents.GetComponent<RectTransform>().sizeDelta = new Vector2(MaxBarSize + EndPedding, RankTierScrollContents.GetComponent<RectTransform>().sizeDelta.y);
    }
    protected override void OnDestroy()
    {
        _BackButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    protected override async Task<bool> _backButtonPressed()
    {
        if (await base._backButtonPressed())
            return true;

        CGlobal.sceneController.pop();
        return true;
    }
    public void Back()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.sceneController.pop();
    }
    private float PointConvert(Int32 Point_)
    {
        Int32 PointCount = 0;
        foreach (var i in CGlobal.MetaData.RankRewards)
        {
            if (i.Meta.point < Point_)
            {
                PointCount++;
            }
        }
        if (CGlobal.MetaData.RankRewards.Count > PointCount)
        {
            float NormalPos = PanelDistance * PointCount;
            float AfterPos = (CGlobal.MetaData.RankRewards[PointCount].Meta.point - (PointCount > 0 ? CGlobal.MetaData.RankRewards[PointCount - 1].Meta.point : 0));
            float NowPos = AfterPos - (CGlobal.MetaData.RankRewards[PointCount].Meta.point - Point_);
            return NormalPos + ((float)(NowPos / AfterPos) * PanelDistance);
        }
        else
        {
            return MaxBarSize;
        }
    }
    public void RankRewardNetSc()
    {
        _RankingRewardPanels[CGlobal.LoginNetSc.User.NextRewardRankIndex - 1].update();

        if (CGlobal.LoginNetSc.User.NextRewardRankIndex < _RankingRewardPanels.Count)
            _RankingRewardPanels[CGlobal.LoginNetSc.User.NextRewardRankIndex].update();
    }
    public override void UpdateResources()
    {
        CGlobal.MoneyUI.UpdateResources();
    }
}
