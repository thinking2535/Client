using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomScene : MonoBehaviour
{
    public MoneyUI MoneyUILayer = null;
    public InputField InputChat = null;
    public GameObject ChatList = null;
    public Text RoomTitle = null;
    public Text RoomMaster = null;
    public ReadyUser[] RoomUsers = null;
    public UIUserCharacter UserCharacter = null;
    public UIUserInfo UserInfo = null;
    public GameObject DelayStart = null;
    public Button PromotionBtn = null;
    public GameObject PromotionDelay = null;
    public Text PromotionDelayText = null;
    public GameObject DelayStartItem = null;
    float Delay = 0.0f;
    public bool IsDelay = false;
    public void OnClickSend()
    {
        if(InputChat.text.Length > 0)
            CGlobal.NetControl.Send<SRoomChatNetCs>(new SRoomChatNetCs(CGlobal.MyRoomInfo.RoomIdx,InputChat.text));
        InputChat.text = "";
    }
    public void OnClickExit()
    {
        CGlobal.ProgressLoading.VisibleProgressLoading();
        CGlobal.NetControl.Send<SRoomOutNetCs>(new SRoomOutNetCs(CGlobal.MyRoomInfo.RoomIdx));
    }
    public void OnClickPromotion()
    {
        if (IsDelay == true) return;
        Delay = 0.0f;
        IsDelay = true;
        CGlobal.NetControl.Send<SRoomNotiNetCs>(new SRoomNotiNetCs(CGlobal.MyRoomInfo.RoomIdx));
        PromotionBtn.gameObject.SetActive(false);
        PromotionDelay.SetActive(true);
        PromotionDelayText.text = "5";
    }
    private void Update()
    {
        if (IsDelay == true)
        {
            Delay += Time.deltaTime;
            PromotionDelayText.text = ((Int32)(5.0f - Delay)).ToString();
            if (Delay > 5.0f)
            {
                IsDelay = false;
                PromotionBtn.gameObject.SetActive(true);
                PromotionDelay.SetActive(false);
            }
        }
    }
}
