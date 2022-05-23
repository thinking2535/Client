using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfoPopup : MonoBehaviour
{
    [SerializeField] RawImage[] RoomThumbnailList = null;
    [SerializeField] Text RoomTitle = null;
    [SerializeField] Text RoomMaster = null;
    [SerializeField] ReadyUser[] RoomUsers = null;
    [SerializeField] Text RoomReadyCount = null;
    [SerializeField] GameObject OpenParent = null;
    [SerializeField] GameObject LockParent = null;
    [SerializeField] InputField _LockPassword = null;
    [SerializeField] Text RoomMode = null;
    [SerializeField] Text RoomModeDetail = null;
    [SerializeField] GameObject[] _ParentCanvases = null;

    private SRoomInfo _RoomInfo = null;
    private bool _IsLock = false;
    private Int32 _MaxUserCount = 0;
    public Int32 GetRoomIdx()
    {
        return gameObject.activeSelf ? _RoomInfo.RoomIdx : -1;
    }
    public void ShowRoomInfoPopup(SRoomInfo RoomInfo_)
    {
        gameObject.SetActive(true);

        _RoomInfo = RoomInfo_;
        _IsLock = _RoomInfo.Password.Length > 0;

        RoomTitle.text = CGlobal.GetPlayModeTitle(_RoomInfo.Mode);
        RoomMaster.text = _RoomInfo.MasterUser;
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
        RoomMode.text = CGlobal.GetPlayModeType(_RoomInfo.Mode);
        RoomModeDetail.text = CGlobal.GetPlayModeTitle(_RoomInfo.Mode);
        RoomReadyCount.text = string.Format("{0}/{1}", _RoomInfo.UserCount, _MaxUserCount);

        OpenParent.SetActive(!_IsLock);
        LockParent.SetActive(_IsLock);

        foreach (var Obj in _ParentCanvases)
            Obj.SetActive(false);

        foreach (var Obj in RoomThumbnailList)
            Obj.gameObject.SetActive(false);

        Int32 Count = ((Int32)_RoomInfo.Mode - 1);
        RoomThumbnailList[Count].gameObject.SetActive(true);
    }
    public void OnClickCancel()
    {
        gameObject.SetActive(false);
        foreach (var Obj in _ParentCanvases)
            Obj.SetActive(true);
    }
    public void OnClickRoonJoin()
    {
        if (_IsLock)
        {
            if (_RoomInfo.Password.Equals(_LockPassword.text))
            {
                RoomJoin();
            }
            else
            {
                CGlobal.SystemPopup.ShowPopup(EText.MultiScene_Popup_PwFailed, PopupSystem.PopupType.Confirm);
            }
        }
        else
        {
            RoomJoin();
        }
    }
    private void RoomJoin()
    {
        CGlobal.ProgressLoading.VisibleProgressLoading();
        CGlobal.NetControl.Send<SRoomJoinNetCs>(new SRoomJoinNetCs(_RoomInfo.RoomIdx));
    }
}
