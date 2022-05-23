using rso.core;
using rso.unity;
using bb;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CSceneLobby : CSceneBase
{
    LobbyScene _LobbyScene = null;
    MoneyUI _MoneyUI = null;
    Camera _MainCamera = null;
    UIUserCharacter _UserCharacter = null;
    UIUserInfo _UserInfo = null;
    CInputTouch _Input = new CInputTouch();
    Int32 _AddRankPoint = 0;
    Int32 _ConstAddRankPoint = 0;
    Int32 _AddGold = 0;
    Int32 _AddDia = 0;
    Int32 _MyRankPoint = 0;
    CharacterInfoPopup _CharacterInfoPopup = null;
    Button _BtnRankingRewardEffect = null;
    RankingRewardPopup _RankingRewardPopup = null;
    RankPointInfoPopup _RankPointInfoPopup = null;
    Image _EventBG = null;
    Text _EventText = null;
    Text _DodgePlayCountText = null;
    Text _IslandPlayCountText = null;
    Text _DodgePlayTimeText = null;
    Text _IslandPlayTimeText = null;
    GameObject _RoomADParant = null;
    RoomInfoPopup _RoomInfoPopup = null;
    PopupMyInfo _MyInfo = null;
    PopupRanking _Ranking = null;
    PopupQuest _Quest = null;

    Vector3 _StartPos;
    Vector3 _EndPos;
    bool _IsSizeSet = false;
    float _MoveSpeed = 120.0f;
    bool _IsEvent = false;
    //Int32 _NowViewEventCount = 0;
    //Int32 _DiffEventCount = 0;

    //GameObject _SelectModePopup = null;

    private float BeforeAngle = 0.0f;
    Dictionary<CGlobal.RankingType, Int32> _RankingList = null;

    Dictionary<Int32, RoomADInfo> RoomInfoList = new Dictionary<Int32, RoomADInfo>();
    class RoomADInfo
    {
        public Int32 ADIdx = 0;
        public SRoomInfo RoomInfo = null;
        public RoomADPanel RoomADPanel = null;
        public RoomADInfo(SRoomInfo RoomInfo_, RoomADPanel RoomADPanel_, Int32 Idx_)
        {
            ADIdx = Idx_;
            RoomInfo = RoomInfo_;
            RoomADPanel = RoomADPanel_;
        }
    }

    void _ScrollCallback(CInputTouch.EState State_, Vector2 DownPos_, Vector2 Diff_, Vector2 Delta_)
    {
        if (State_ == CInputTouch.EState.Move)
        {
            var NowPosX = (DownPos_.x + Diff_.x);
            var NowPos = new Vector3(NowPosX, 0.0f, 10.0f);
            var DownPos = new Vector3(DownPos_.x, 0.0f, 10.0f);
            var WorldPointNow = _MainCamera.ScreenToWorldPoint(NowPos);
            var WorldPointDown = _MainCamera.ScreenToWorldPoint(DownPos);
            var WorldDistance = WorldPointNow.x - WorldPointDown.x;

            var Angle = (WorldDistance / CGlobal.CircleRound * 360.0f);
            _UserCharacter.transform.localEulerAngles = new Vector3(0.0f, _UserCharacter.transform.localEulerAngles.y - (Angle - BeforeAngle), 0.0f);
            BeforeAngle = Angle;
        }
        else if (State_ == CInputTouch.EState.Up)
        {
            BeforeAngle = 0.0f;
        }
    }
    public CSceneLobby(Int32 AddGold_ = 0, Int32 AddRankPoint_ = 0, Int32 AddDia_ = 0) :
        base("Prefabs/LobbyScene", Vector3.zero, true)
    {
        _Input.Add(new CInputTouch.CObjectScroll((Vector2) => { return true; }, _ScrollCallback));
        _AddGold = AddGold_;
        _AddDia = AddDia_;
        _AddRankPoint = AddRankPoint_;
        _ConstAddRankPoint = AddRankPoint_;
    }
    public override void Dispose()
    {
    }
    public override void Enter()
    {
        _LobbyScene = _Object.GetComponent<LobbyScene>();
        _MoneyUI = _LobbyScene.MoneyUILayer;
        _MoneyUI.SetResources(CGlobal.LoginNetSc.User.Resources);
        CGlobal.MusicStop();
        CGlobal.MusicPlayNormal();
        _MainCamera = _LobbyScene.MainCamera;

        foreach (var i in CGlobal.LoginNetSc.Quests)
        {
            if(CGlobal.MetaData.QuestMetas[i.Value.Code].RequirmentCount <= i.Value.Count && i.Value.CoolEndTime.Ticks <= 0)
            {
                CGlobal.RedDotControl.SetReddotOn(RedDotControl.EReddotType.Quest);
                break;
            }
        }

        //래드 닷 관련 작업.
        var LobbyBtn = _LobbyScene.BtnCharacterSelect;
        LobbyBtn.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Character));
        LobbyBtn = _LobbyScene.BtnRanking;
        LobbyBtn.SetRedDot(false);
        LobbyBtn = _LobbyScene.BtnShop;
        LobbyBtn.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Shop));
        LobbyBtn = _LobbyScene.BtnQuest;
        LobbyBtn.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Quest));
        CGlobal.RedDotControl.SetReddotOff(RedDotControl.EReddotType.Shop);

        //User 정보 Character, Name, ProfileImage 초기화.
        _UserCharacter = _LobbyScene.UserCharacter;
        _UserInfo = _LobbyScene.UserInfo;
        _CharacterInfoPopup = _LobbyScene.CharacterInfoPopup;
        _BtnRankingRewardEffect = _LobbyScene.BtnRankingRewardEffect;

        _EventBG = _LobbyScene.EventBG;
        _EventText = _LobbyScene.EventText;
        _EventBG.gameObject.SetActive(false);

        //CheckEvent();
        _RoomADParant = _LobbyScene.RoomADParant;
        _RoomInfoPopup = _LobbyScene.RoomInfoPopup;

        _DodgePlayCountText = _LobbyScene.DodgePlayCountText;
        _IslandPlayCountText = _LobbyScene.IslandPlayCountText;
        _DodgePlayTimeText = _LobbyScene.DodgePlayTimeText;
        _IslandPlayTimeText = _LobbyScene.IslandPlayTimeText;

        _RankingRewardPopup = _LobbyScene.RankingRewardPopup;
        _RankingRewardPopup.gameObject.SetActive(false);

        _RankPointInfoPopup = _LobbyScene.RankPointInfoPopup;
        _RankPointInfoPopup.gameObject.SetActive(false);

        _MyInfo = _LobbyScene.MyInfo;
        _Ranking = _LobbyScene.Ranking;
        _Quest = _LobbyScene.Quest;

        SetDodgeChargeTimeCount();
        SetIslandChargeTimeCount();

        _MyRankPoint = CGlobal.LoginNetSc.User.Point;
        _UserInfo.InitUserInfo();

        MakeCharacter();

        CGlobal.AddUserBattleEnd(_AddRankPoint);

        for (Int32 i = 0; i < CGlobal.MetaData.RankRewardList.Count; ++i )
        {
            if (CGlobal.MetaData.RankRewardList[i].Point <= CGlobal.LoginNetSc.User.PointBest && i == (CGlobal.LoginNetSc.User.LastGotRewardRankIndex + 1))
            {
                CGlobal.RedDotControl.SetReddotOn(RedDotControl.EReddotType.Rank);
                break;
            }
        }
        LobbyBtn = _LobbyScene.BtnRank;
        LobbyBtn.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Rank));

        if (PlayerPrefs.GetInt("ViewTutorieal_" + CGlobal.UID.ToString(), 0) == 0)
        {
            PlayerPrefs.SetInt("ViewTutorieal_" + CGlobal.UID.ToString(), 1);
            PlayerPrefs.Save();
            CGlobal.SystemPopup.ShowPopup(EText.Tutorial_Text_Start, PopupSystem.PopupType.Confirm, (PopupSystem.PopupBtnType type_) => {
                CGlobal.SceneSetNext(new CSceneBattleTutorial());
            });
        }
        else
        {
            if(CGlobal.IsUpdateInfoPopup == false)
            {
                var IsUpdatePopup = PlayerPrefs.GetInt(CGlobal.UpdateInfoKey(), 0);
                if (IsUpdatePopup == 0)
                    CGlobal.UpdateInfoPopup.ShowUpdateInfoPopup();
            }
        }
        CGlobal.ADManager.SendDelayPacket();
        _BtnRankingRewardEffect.gameObject.SetActive(false);
        CGlobal.NetControl.Send<SRankingRewardInfoNetCs>(new SRankingRewardInfoNetCs());
    }
    public override bool Update()
    {
        if (_Exit)
            return false;

        if(_AddRankPoint > 0)
        {
            Int32 Add = (Int32)((float)_ConstAddRankPoint * Time.deltaTime);
            if (Add == 0) Add = 1;
            _MyRankPoint += Add;
            _AddRankPoint -= Add;
            if (_AddRankPoint <= 0)
                _MyRankPoint = CGlobal.LoginNetSc.User.Point;
            if(_MyRankPoint < 0)
            {
                _AddRankPoint = 0;
                _MyRankPoint = CGlobal.LoginNetSc.User.Point;
            }
            _UserInfo.RankUpdate(_MyRankPoint);
        }
        else if(_AddRankPoint < 0)
        {
            Int32 Add = (Int32)((float)_ConstAddRankPoint * Time.deltaTime);
            if (Add == 0) Add = 1;
            _MyRankPoint -= Add;
            _AddRankPoint += Add;
            if (_AddRankPoint >= 0)
                _MyRankPoint = CGlobal.LoginNetSc.User.Point;
            if (_MyRankPoint < 0)
            {
                _AddRankPoint = 0;
                _MyRankPoint = CGlobal.LoginNetSc.User.Point;
            }
            _UserInfo.RankUpdate(_MyRankPoint);
        }
        if (_AddGold > 0 || _AddDia > 0)
        {
            Int32[] Resources = new Int32[(Int32)EResource.Max];
            Resources[(Int32)EResource.Gold] = _AddGold;
            Resources[(Int32)EResource.Dia] = _AddDia;
            _MoneyUI.ShowRewardEffect(Resources);
            _AddGold = 0;
            _AddDia = 0;
        }
        if (!_CharacterInfoPopup.gameObject.activeSelf && !_RankingRewardPopup.gameObject.activeSelf && !_RankPointInfoPopup.gameObject.activeSelf)
            _Input.Update();

        if(CGlobal.LoginNetSc != null)
        {
            SetDodgeChargeTimeCount();
            SetIslandChargeTimeCount();
        }
        if (_IsEvent)
        {
            if (_IsSizeSet)
            {
                float Widht = (_EventText.rectTransform.sizeDelta.x) * -1;
                _StartPos = _EventText.transform.localPosition;
                _EndPos = new Vector3(Widht, 0.0f, 0.0f);
                _IsSizeSet = false;
            }
            _EventText.transform.localPosition = Vector3.MoveTowards(_EventText.transform.localPosition, _EndPos, _MoveSpeed * Time.deltaTime);
            if (_EventText.transform.localPosition == _EndPos)
            {
                _EventText.transform.localPosition = _StartPos;
            }
        }
        //CheckEvent();

        if (rso.unity.CBase.BackPushed())
        {
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
            if (_CharacterInfoPopup.gameObject.activeSelf)
            {
                _CharacterInfoPopup.OnClickBack();
                return true;
            }
            if (_RankingRewardPopup.gameObject.activeSelf)
            {
                _RankingRewardPopup.OnClickReceive();
                return true;
            }
            if (_RankPointInfoPopup.gameObject.activeSelf)
            {
                _RankPointInfoPopup.Close();
                return true;
            }
            if (_MyInfo.gameObject.activeSelf)
            {
                _MyInfo.Back();
                return true;
            }
            if (_Quest.gameObject.activeSelf)
            {
                _Quest.Back();
                return true;
            }
            if (_Ranking.gameObject.activeSelf)
            {
                _Ranking.OnClose();
                return true;
            }
            CGlobal.SystemPopup.ShowGameOut();
        }

        return true;
    }
    private void SetDodgeChargeTimeCount()
    {
        var SingleData = CGlobal.GetSinglePlayCountLeftTime();
        string timeString = string.Format("{0:D2}:{1:D2}", SingleData.Item2.Minutes, SingleData.Item2.Seconds);
        bool IsView = SingleData.Item1 < CGlobal.MetaData.SingleBalanceMeta.PlayCountMax;
        if (IsView)
            _DodgePlayTimeText.text =  timeString;
        else
            _DodgePlayTimeText.text = "";

        _DodgePlayTimeText.gameObject.SetActive(IsView);

        _DodgePlayCountText.text = SingleData.Item1.ToString() + "/" + CGlobal.MetaData.SingleBalanceMeta.PlayCountMax.ToString();
    }
    private void SetIslandChargeTimeCount()
    {
        var SingleData = CGlobal.GetSingleIslandPlayCountLeftTime();
        string timeString = string.Format("{0:D2}:{1:D2}", SingleData.Item2.Minutes, SingleData.Item2.Seconds);
        bool IsView = SingleData.Item1 < CGlobal.MetaData.SingleIslandBalanceMeta.PlayCountMax;
        if (IsView)
            _IslandPlayTimeText.text = timeString;
        else
            _IslandPlayTimeText.text = "";

        _IslandPlayTimeText.gameObject.SetActive(IsView);

        _IslandPlayCountText.text = SingleData.Item1.ToString() + "/" + CGlobal.MetaData.SingleIslandBalanceMeta.PlayCountMax.ToString();
    }
    public override void ResourcesUpdate()
    {
        _MoneyUI.SetResources(CGlobal.LoginNetSc.User.Resources);
        if(_CharacterInfoPopup.gameObject.activeSelf)
            _CharacterInfoPopup.ResourcesUpdate();
    }
    public override void ResourcesAction(Int32[] Resources_)
    {
        _MoneyUI.ShowRewardEffect(Resources_);
    }
    public void MakeCharacter()
    {
        _UserCharacter.DeleteCharacter();

        Int32 CharCode = CGlobal.LoginNetSc.User.SelectedCharCode;
        _UserCharacter.MakeCharacter(CharCode);

        _UserInfo.CharacterChange();
    }
    public void QuestRedDotCheck()
    {
        foreach (var i in CGlobal.LoginNetSc.Quests)
        {
            if (CGlobal.MetaData.QuestMetas[i.Value.Code].RequirmentCount <= i.Value.Count && i.Value.CoolEndTime.Ticks <= 0)
            {
                CGlobal.RedDotControl.SetReddotOn(RedDotControl.EReddotType.Quest);
                break;
            }
        }

        var LobbyBtn = _LobbyScene.BtnQuest;
        LobbyBtn.SetRedDot(CGlobal.RedDotControl.IsReddotCheck(RedDotControl.EReddotType.Quest));
    }
    public void SetRankingReward(bool IsActive_, Dictionary<CGlobal.RankingType, Int32> RankingList_)
    {
        _RankingList = RankingList_;
        _BtnRankingRewardEffect.gameObject.SetActive(IsActive_);
    }
    public void GetRankingReward(Dictionary<CGlobal.RankingType,Int32> RewardCodes_)
    {
        _RankingRewardPopup.Init(RewardCodes_, _RankingList);
        _RankingRewardPopup.gameObject.SetActive(true);
        _BtnRankingRewardEffect.gameObject.SetActive(false);
    }

    public void CheckEvent()
    {
        //var EventList = CGlobal.MetaData.ModeEventMetas;
        //string EventText = "";
        //var Now = CGlobal.GetServerTimePoint().ToDateTime();
        //_DiffEventCount = 0;

        //foreach (var i in EventList)
        //{
        //    var BeginEventTime = new DateTime(Now.Year, Now.Month, Now.Day, i.BeginHour, i.BeginMin, i.BeginSec);
        //    var EndEventTime = new DateTime(Now.Year, Now.Month, Now.Day, i.EndHour, i.EndMin, i.EndSec);
        //    if (Now > BeginEventTime && Now < EndEventTime)
        //    {
        //        if (EventText.Length > 0) EventText += "     ";
        //        EventText += CGlobal.MetaData.GetText(i.ETextName);
        //        _DiffEventCount++;
        //        ModeBtnSetting(i.Mode, true);
        //    }
        //    else
        //    {
        //        if(CGlobal.PlayMode == i.Mode)
        //        {
        //            _LobbyScene.SelectMode((Int32)EPlayMode.Null);
        //        }
        //        ModeBtnSetting(i.Mode, false);
        //    }
        //}
        //if(_DiffEventCount == 0)
        //{
        //    _EventBG.gameObject.SetActive(false);
        //    _IsEvent = false;
        //}
        //else if(_DiffEventCount != _NowViewEventCount)
        //{
        //    _EventBG.gameObject.SetActive(true);
        //    _EventText.text = EventText;
        //    _IsSizeSet = true;
        //    _IsEvent = true;
        //}
        //_NowViewEventCount = _DiffEventCount;
    }
    public void UpdateRoomAD(SRoomInfo RoomInfo_)
    {
        if (RoomInfo_.State == ERoomState.RoomWait)
        {
            if (RoomInfoList.ContainsKey(RoomInfo_.RoomIdx))
            {
                RoomInfoList[RoomInfo_.RoomIdx].RoomADPanel.transform.SetAsLastSibling();
                RoomInfoList[RoomInfo_.RoomIdx].RoomADPanel.initRoomADPanel(RoomInfo_);
                Int32 Idx = RoomInfoList[RoomInfo_.RoomIdx].ADIdx;
                RoomInfoList[RoomInfo_.RoomIdx].ADIdx = RoomInfoList.Count;
                foreach (var i in RoomInfoList)
                {
                    if (i.Value.ADIdx > Idx && i.Key != RoomInfo_.RoomIdx) i.Value.ADIdx--;
                }
            }
            else
            {
                RoomADPanel RoomADPanel = Resources.Load<RoomADPanel>("Prefabs/UI/complex/slot_roomList");
                var Panel = UnityEngine.Object.Instantiate<RoomADPanel>(RoomADPanel);
                Panel.initRoomADPanel(RoomInfo_);
                Panel.transform.SetParent(_RoomADParant.transform);
                Panel.transform.localScale = Vector3.one;
                RoomInfoList.Add(RoomInfo_.RoomIdx, new RoomADInfo(RoomInfo_, Panel, RoomInfoList.Count));
                if (RoomInfoList.Count > 20)
                {
                    foreach (var i in RoomInfoList)
                    {
                        if (i.Value.ADIdx == 0)
                        {
                            UnityEngine.Object.Destroy(i.Value.RoomADPanel.gameObject);
                            RoomInfoList.Remove(i.Key);
                        }
                        else
                        {
                            i.Value.ADIdx--;
                        }
                    }
                }
            }
        }
        else
        {
            if(CGlobal.MyRoomInfo != null && CGlobal.MyRoomInfo.RoomIdx != RoomInfo_.RoomIdx)
            {
                if (_RoomInfoPopup.GetRoomIdx() == RoomInfo_.RoomIdx)
                {
                    CGlobal.SystemPopup.ShowPopup(EText.LobbyScene_Text_StartRooom, PopupSystem.PopupType.Confirm);
                    _RoomInfoPopup.OnClickCancel();
                }
            }
            RemoveADPanel(RoomInfo_.RoomIdx);
        }
    }
    public void RemoveADPanel(Int32 RoomIdx_)
    {
        if (RoomInfoList.ContainsKey(RoomIdx_))
        {
            UnityEngine.Object.Destroy(RoomInfoList[RoomIdx_].RoomADPanel.gameObject);
            RoomInfoList.Remove(RoomIdx_);
        }
    }
    public void ShopRoomInfoPopup(SRoomInfo RoomInfo_)
    {
        _RoomInfoPopup.ShowRoomInfoPopup(RoomInfo_);
    }

    public void SetNicknameBox(bool Active_)
    {
        _MyInfo.SetNicknameBox(Active_);
    }

    public void SetNickname(string Nick_)
    {
        _MyInfo.SetNickname(Nick_);
        _UserInfo.InitUserInfo();
    }
    public void UpdateQuestList()
    {
        _Quest.UpdateQuestList();
    }
    public void ChangeQuest(byte slotIndex, int newCode)
    {
        _Quest.ChangeQuest(slotIndex, newCode);
    }
    public void ChangeQuest(byte slotIndex)
    {
        _Quest.ChangeQuest(slotIndex);
    }
    public void RemoveQuest(byte slotIndex)
    {
        _Quest.RemoveQuest(slotIndex);
    }
}
