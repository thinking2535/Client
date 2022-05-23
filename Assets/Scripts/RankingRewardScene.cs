using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingRewardScene : MonoBehaviour
{
    public GameObject RankTierScrollContents = null;
    public RectTransform RankTierScrollBarRect = null;
    public GameObject RankTierScrollBar = null;
    public GameObject RankTierMyScoreNowBar = null;
    public GameObject RankTierMyScoreOldBar = null;
    public Text RankTierMyScoreText = null;
    public Text RankTierMyLevelText = null;
    public MoneyUI MoneyUI = null;
    public float PanelDistance = 300.0f;
    public Camera MainCamera = null;
    public CharacterInfoPopup CharacterInfoPopup = null;
    public RankingRewardPanel RankingPanel = null;
    public void Back()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.SceneSetNext(new CSceneLobby());
    }
}
