using bb;
using rso.core;
using rso.unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CSceneRoom : CSceneBase
{
    private RoomScene _RoomScene = null;
    private MoneyUI _MoneyUILayer = null;
    private GameObject _ChatList = null;
    private Text _RoomTitle = null;
    private Text _RoomMaster = null;
    private ReadyUser[] _RoomUsers = null;
    private UIUserCharacter _UserCharacter = null;
    private UIUserInfo _UserInfo = null;
    private GameObject _DelayStart = null;
    private GameObject _PromotionBtn = null;
    private GameObject _PromotionDelay = null;
    private GameObject _DelayStartItem = null;

    private SRoomInfo _RoomInfo = null;
    private List<ChatPanel> Chats = new List<ChatPanel>();

    private Int32 _MaxUserCount = 0;

    public Int32 GetRoomIdx()
    {
        return _RoomInfo.RoomIdx;
    }
    public CSceneRoom() :
        base("Prefabs/RoomScene", Vector3.zero, true)
    {
    }
    public override void Dispose()
    {
    }
    public override void Enter()
    {
        _RoomScene = _Object.GetComponent<RoomScene>();
        _MoneyUILayer = _RoomScene.MoneyUILayer;
        _ChatList = _RoomScene.ChatList;
        _RoomTitle = _RoomScene.RoomTitle;
        _RoomMaster = _RoomScene.RoomMaster;
        _RoomUsers = _RoomScene.RoomUsers;
        _UserCharacter = _RoomScene.UserCharacter;
        _UserInfo = _RoomScene.UserInfo;
        _DelayStart = _RoomScene.DelayStart;
        _PromotionBtn = _RoomScene.PromotionBtn.gameObject;
        _PromotionDelay = _RoomScene.PromotionDelay;
        _DelayStartItem = _RoomScene.DelayStartItem;

        _RoomInfo = CGlobal.MyRoomInfo;
        _RoomTitle.text = CGlobal.GetPlayModeTitle(_RoomInfo.Mode);
        _RoomMaster.text = _RoomInfo.MasterUser;
        foreach (var i in _RoomUsers)
        {
            i.gameObject.SetActive(false);
        }
        _MaxUserCount = CGlobal.MetaData.GameModeMaxMember[_RoomInfo.Mode];
        for (var i = 0; i < _MaxUserCount; ++i)
        {
            _RoomUsers[i].gameObject.SetActive(true);
            _RoomUsers[i].OffReady();
        }

        _UserInfo.InitUserInfo();

        MakeCharacter();

        _DelayStart.SetActive(false);
        RoomInfoChange(_RoomInfo);
    }
    public override bool Update()
    {
        if (_Exit)
            return false;

        if (rso.unity.CBase.BackPushed())
        {
            if (_DelayStart.activeSelf)
            {
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
            _RoomScene.OnClickExit();
        }

        return true;
    }
    public void MakeCharacter()
    {
        _UserCharacter.DeleteCharacter();

        Int32 CharCode = CGlobal.LoginNetSc.User.SelectedCharCode;
        _UserCharacter.MakeCharacter(CharCode);

        _UserInfo.CharacterChange();
    }
    public void RoomInfoChange(SRoomInfo RoomInfo_)
    {
        CGlobal.MyRoomInfo = RoomInfo_;
        _RoomInfo = RoomInfo_;

        _RoomMaster.text = _RoomInfo.MasterUser;
        for (var i = 0; i < _MaxUserCount; ++i)
        {
            _RoomUsers[i].OffReady();
        }
        for (var i = 0; i < _MaxUserCount; ++i)
        {
            if(i < _RoomInfo.UserCount) 
                _RoomUsers[i].OnReady();
        }
        if(_RoomScene.IsDelay == false)
        {
            _PromotionBtn.SetActive(CGlobal.UID == _RoomInfo.MasterUID);
            _PromotionDelay.SetActive(false);
        }
    }
    public void StartReady(EGameMode Mode_)
    {
        _UserCharacter.gameObject.SetActive(false);
        _MoneyUILayer.gameObject.SetActive(false);
        _DelayStart.SetActive(true);
        _DelayStartItem.SetActive(Mode_ == EGameMode.DodgeSolo || Mode_ == EGameMode.IslandSolo);
    }
    public void AddChat(string Nick_, string Msg_)
    {
        ChatPanel ChatPanel = Resources.Load<ChatPanel>("Prefabs/UI/ChatText");
        var Panel = UnityEngine.Object.Instantiate<ChatPanel>(ChatPanel);
        Panel.InitChatPanel(Nick_, Msg_);
        Panel.transform.SetParent(_ChatList.transform);
        Panel.transform.localScale = Vector3.one;
        Chats.Add(Panel);
        if(Chats.Count > 20)
        {
            UnityEngine.Object.Destroy(Chats[0].gameObject);
            Chats.RemoveAt(0);
        }
    }
}
