using bb;
using rso.core;
using rso.unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CSceneRoomList : CSceneBase
{
    RoomListScene _RoomListScene = null;
    RoomCreatePopup _RoomCreatePopup = null;
    MoneyUI _MoneyUILayer = null;
    GameObject _RoomListContents = null;
    RoomListPanel _RoomListPanel = null;
    GameObject _RoomListEmpty = null;
    GameObject _RoomPassword = null;
    InputField _LockPassword = null;
    private GameObject _DelayStart = null;
    private GameObject _DelayStartItem = null;

    List<RoomListPanel> RoomListPanels = new List<RoomListPanel>();

    private SRoomInfo _RoomInfo = null;

    public CSceneRoomList() :
        base("Prefabs/RoomListScene", Vector3.zero, true)
    {
    }
    public override void Dispose()
    {
    }
    public override void Enter()
    {
        _RoomListScene = _Object.GetComponent<RoomListScene>();
        _RoomCreatePopup = _RoomListScene.RoomCreatePopup;
        _MoneyUILayer = _RoomListScene.MoneyUILayer;
        _MoneyUILayer.SetResources(CGlobal.LoginNetSc.User.Resources);
        _RoomListContents = _RoomListScene.RoomListContents;
        _RoomListPanel = _RoomListScene.RoomListPanel;
        _RoomListEmpty = _RoomListScene.RoomListEmpty;
        _RoomPassword = _RoomListScene.RoomPassword;
        _LockPassword = _RoomListScene.LockPassword;
        _DelayStart = _RoomListScene.DelayStart;
        _DelayStartItem = _RoomListScene.DelayStartItem;

        SetRoomList();
    }
    public void SetRoomList()
    {
        foreach (var i in RoomListPanels)
            UnityEngine.Object.Destroy(i.gameObject);
        RoomListPanels.Clear();

        List<SRoomInfo> RoomInfos = new List<SRoomInfo>();
		foreach (var i in CGlobal.RoomDictionary)
		{
			if (i.Value.State == ERoomState.RoomWait)
			{
				if ((i.Value.Mode == EGameMode.Survival && i.Value.UserCount < 6) ||
				(i.Value.Mode == EGameMode.SurvivalSmall && i.Value.UserCount < 3) ||
				(i.Value.Mode == EGameMode.Team && i.Value.UserCount < 6) ||
				(i.Value.Mode == EGameMode.TeamSmall && i.Value.UserCount < 4) ||
				(i.Value.Mode == EGameMode.DodgeSolo && i.Value.UserCount < 2) ||
				(i.Value.Mode == EGameMode.IslandSolo && i.Value.UserCount < 2) ||
				(i.Value.Mode == EGameMode.Solo && i.Value.UserCount < 2))
				{
					RoomInfos.Add(i.Value);
				}
			}
		}

		foreach (var i in RoomInfos)
        {
            var Panel = UnityEngine.Object.Instantiate<RoomListPanel>(_RoomListPanel);
            Panel.InitRoomListPanel(i);
            Panel.transform.SetParent(_RoomListContents.transform);
            Panel.transform.localScale = Vector3.one;
            RoomListPanels.Add(Panel);
        }
        _RoomListEmpty.SetActive(RoomInfos.Count <= 0);
    }
    public void UpdateRoomList()
    {
        SetRoomList();
    }
    public override bool Update()
    {
        if (_Exit)
            return false;

        if (rso.unity.CBase.BackPushed())
        {
            if(_DelayStart.activeSelf)
            {
                return true;
            }
            if (_RoomPassword.activeSelf)
            {
                _RoomPassword.SetActive(false);
                return true;
            }
            if (_MoneyUILayer.GetSettingPopup())
            {
                _MoneyUILayer.SettingPopupClose();
                return true;
            }
            if (CGlobal.RewardPopup.gameObject.activeSelf)
            {
                CGlobal.RewardPopup.OnRecive();
                return true;
            }
            if (CGlobal.SystemPopup.gameObject.activeSelf)
            {
                CGlobal.SystemPopup.OnClickCancel();
                return true;
            }
            if (CGlobal.UpdateInfoPopup.gameObject.activeSelf)
            {
                CGlobal.UpdateInfoPopup.OnClickOk();
                return true;
            }
            if (_RoomCreatePopup.gameObject.activeSelf)
            {
                _RoomCreatePopup.OnClickCancel();
                return true;
            }
            CGlobal.SceneSetNext(new CSceneLobby());
        }

        return true;
    }
    public override void ResourcesUpdate()
    {
        _MoneyUILayer.SetResources(CGlobal.LoginNetSc.User.Resources);
    }
    public void ShowPassword(SRoomInfo RoomInfo_)
    {
        _RoomInfo = RoomInfo_;
        _RoomPassword.SetActive(true);
    }
    public void InputPassword()
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
    private void RoomJoin()
    {
        CGlobal.ProgressLoading.VisibleProgressLoading();
        CGlobal.NetControl.Send<SRoomJoinNetCs>(new SRoomJoinNetCs(_RoomInfo.RoomIdx));
    }
    public void StartReady(EGameMode Mode_)
    {
        _MoneyUILayer.gameObject.SetActive(false);
        _DelayStart.SetActive(true);
        _DelayStartItem.SetActive(Mode_ == EGameMode.DodgeSolo || Mode_ == EGameMode.IslandSolo);
    }
}