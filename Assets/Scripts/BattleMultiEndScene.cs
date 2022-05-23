using bb;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleMultiEndScene : MonoBehaviour
{
    public float ExitDelayTime = 0.0f;

    public Canvas WinCanvas = null;
    public GameObject[] WinCanvasChars = null;
    public Text[] WinPlayerNameTexts = null;
    public Text WinGoldText = null;
    public Text WinRankPointText = null;

    public Canvas LoseCanvas = null;
    public GameObject[] LoseCanvasChars = null;
    public Text[] LosePlayerNameTexts = null;
    public Text LoseGoldText = null;
    public Text LoseTitleText = null;
    public Text LoseRankPointText = null;

    public Canvas ResultCanvas = null;
    public GameObject[] ResultCanvasChars = null;
    public Text[] ResultPlayerNameTexts = null;
    public Text ResultGoldText = null;
    public Text ResultTitleText = null;
    public Text ResultRankPointText = null;

    public Image BGImage = null;
    public GameObject ParticleParent = null;

    public ResultPlayerInfo[] ResultPlayerInfos = null;
    public Text TimerText = null;

    public GameObject WinBG = null;
    public GameObject LossBG = null;
    public void Close()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.ProgressLoading.VisibleProgressLoading();
        if(CGlobal.MyRoomInfo != null)
            CGlobal.NetControl.Send<SRoomOutNetCs>(new SRoomOutNetCs(CGlobal.MyRoomInfo.RoomIdx));
        else
            CGlobal.NetControl.Send<SRoomListNetCs>(new SRoomListNetCs());
    }
    public void ReJoin()
    {
        CGlobal.ProgressLoading.VisibleProgressLoading();
        if (CGlobal.MyRoomInfo != null)
            CGlobal.NetControl.Send<SRoomJoinNetCs>(new SRoomJoinNetCs(CGlobal.MyRoomInfo.RoomIdx));
        else
            CGlobal.NetControl.Send<SRoomListNetCs>(new SRoomListNetCs());
    }
}
