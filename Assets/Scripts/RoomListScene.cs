using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListScene : MonoBehaviour
{
    public RoomCreatePopup RoomCreatePopup = null;
    public MoneyUI MoneyUILayer = null;
    public GameObject RoomListContents = null;
    public RoomListPanel RoomListPanel = null;
    public GameObject RoomListEmpty = null;
    public GameObject RoomPassword = null;
    public InputField LockPassword = null;
    public GameObject DelayStart = null;
    public GameObject DelayStartItem = null;

    public void PasswordSend()
    {
        var Scene = CGlobal.GetScene<CSceneRoomList>();
        Scene.InputPassword();
    }
    public void OnClickCancel()
    {
        LockPassword.text = "";
        RoomPassword.SetActive(false);
    }
    public void OnClickCreateRoom()
    {
        RoomCreatePopup.ShowRoomCreatePopup();
    }
    public void OnClickQuickJoin()
    {
        bool IsJoin = false;
        foreach(var i in CGlobal.RoomDictionary)
        {
            Int32 MaxUserCount = CGlobal.MetaData.GameModeMaxMember[i.Value.Mode];
            if (i.Value.UserCount < MaxUserCount)
            {
                //Join Room
                CGlobal.ProgressLoading.VisibleProgressLoading();
                CGlobal.NetControl.Send<SRoomJoinNetCs>(new SRoomJoinNetCs(i.Key));
                IsJoin = true;
                break;
            }
        }
        if(!IsJoin)
        {
            CGlobal.SystemPopup.ShowPopup(EText.MultiScene_Popup_ErrorQuickEnter, PopupSystem.PopupType.Confirm);
        }
    }
    public void OnClickExit()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.SceneSetNext(new CSceneLobby());
    }
}
