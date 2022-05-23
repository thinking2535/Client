using bb;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MatchingScene : MonoBehaviour
{
    [SerializeField] Button _btnCancel = null;
    public UIUserCharacter UserCharacter = null;
    public Text UserNick = null;
    public Image UserRank = null;
    public Image UserRankIcon = null;
    public Text UserPoint = null;
    public Image UserPointGauge = null;
    public Text ReadyTime = null;
    public Text MatchingTitle = null;
    public GameObject[] ReadyPlayer = null;
    public GameObject[] ReadyPlayerIcon = null;
    public GameObject ItemDescriptionBG = null;
    public void Cancel()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.NetControl.Send(new SBattleOutNetCs());
        _btnCancel.interactable = false;
        CGlobal.IsMatchingCancel = true;
        AnalyticsManager.TrackingEvent(ETrackingKey.cancel_multiplay);
    }
}
