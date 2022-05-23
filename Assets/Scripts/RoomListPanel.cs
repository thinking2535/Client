using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : MonoBehaviour
{
    [SerializeField] RawImage[] RoomThumbnailList = null;
    [SerializeField] Text RoomTitle = null;
    [SerializeField] Text RoomMaster = null;
    [SerializeField] ReadyUser[] RoomUsers = null;
    [SerializeField] Image RoomLock = null;

    private SRoomInfo _RoomInfo = null;
    private Int32 _MaxUserCount = 0;
    private bool _IsLock = false;

    public void InitRoomListPanel(SRoomInfo RoomInfo_)
    {
        _RoomInfo = RoomInfo_;
        RoomTitle.text = CGlobal.GetPlayModeTitle(_RoomInfo.Mode);
        RoomMaster.text = _RoomInfo.MasterUser;

        _IsLock = _RoomInfo.Password.Length > 0;
        RoomLock.gameObject.SetActive(_IsLock);

        foreach (var i in RoomUsers)
        {
            i.gameObject.SetActive(false);
        }
        _MaxUserCount = CGlobal.MetaData.GameModeMaxMember[_RoomInfo.Mode];
        for (var i = 0; i < _MaxUserCount; ++i)
        {
            RoomUsers[i].gameObject.SetActive(true);
            RoomUsers[i].OffReady();
        }
        for (var i = 0; i < _RoomInfo.UserCount; ++i)
        {
            RoomUsers[i].OnReady();
        }

        foreach (var Obj in RoomThumbnailList)
            Obj.gameObject.SetActive(false);

        Int32 Count = ((Int32)_RoomInfo.Mode - 1);
        RoomThumbnailList[Count].gameObject.SetActive(true);
    }

    public void OnClickRoomInfo()
    {
        if (_IsLock)
        {
            var Scene = CGlobal.GetScene<CSceneRoomList>();
            Scene.ShowPassword(_RoomInfo);
        }
        else
        {
            CGlobal.ProgressLoading.VisibleProgressLoading();
            CGlobal.NetControl.Send<SRoomJoinNetCs>(new SRoomJoinNetCs(_RoomInfo.RoomIdx));
        }
    }
}
