using bb;
using rso.core;
using rso.unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneBattleTutorial : CSceneBase
{
    enum ETutorialStep
    {
        Ready,
        Start,
        Play,
        Exit
    }

    BattleTutorialScene _BattleTutorialScene = null;
    GameObject _CharacterParent = null;
    GameObject _ParticleParent = null;
    GameObject _JoyPad = null;
    GameObject _Hand_L = null;
    GameObject _Hand_R = null;
    GameObject _Point = null;
    GameObject _TouchAreaR = null;
    GameObject _TouchAreaL = null;
    CBattlePlayerTutorial _Me = null;
    CPadSimulator _Pad = null;
    Animator _Hand_L_Animator = null;

    TimePoint _DelayTime;
    float _TouchAreaCount;

    private ETutorialStep _TutorialStep = ETutorialStep.Ready;
    private Int32 _TutorialGrade = 0;
    private Vector3[] _TutorialPointVectorArray = { new Vector3(1.14f, -0.9f, 0.0f), new Vector3(1.14f, 0.0f, 0.0f), new Vector3(-0.99f, -0.23f, 0.0f) };

    void _Touched(CInputTouch.EState State_, Vector2 Pos_, Int32 Dir_) // Dir_(2 Directions) : 0(Left) , 1(Right)
    {
        if (State_ == CInputTouch.EState.Down)
        {
            _JoyPad.SetActive(false);
            return;
        }

        if (State_ == CInputTouch.EState.Move)
        {
            sbyte Dir = Dir_ == 0 ? (sbyte)-1 : (sbyte)1;

            if (_Me.SinglePlayer.Character.Dir != Dir)
                _Me.LeftRight(Dir);
        }
        else
        {
            if (_Me.SinglePlayer.Character.Dir != 0)
                _Me.Center();
            _JoyPad.SetActive(true);
        }
    }
    void _Pushed(CInputTouch.EState State_)
    {
        if (State_ != CInputTouch.EState.Down)
            return;

        if (_Me.BalloonCount > 0)
            _Me.Flap();
        else if (_Me.BalloonCount == 0 && _Me.IsGround)
            _Me.Pump();
    }
    public CSceneBattleTutorial() :
        base("Prefabs/Maps/Tutorial/TutorialScene_01", Vector3.zero, true)
    {
        var Range = Screen.width * 0.02f;
        _Pad = new CPadSimulator(_Touched, _Pushed, Range, Range);
    }
    public override void Enter()
    {
        _BattleTutorialScene = _Object.GetComponent<BattleTutorialScene>();
        _CharacterParent = _BattleTutorialScene.CharacterParent;
        _ParticleParent = _BattleTutorialScene.ParticleParent;
        _JoyPad = _BattleTutorialScene.JoyPad;
        _Hand_L = _BattleTutorialScene.Hand_L;
        _Hand_R = _BattleTutorialScene.Hand_R;
        _Point = _BattleTutorialScene.Point;
        _TouchAreaR = _BattleTutorialScene.TouchAreaR;
        _TouchAreaL = _BattleTutorialScene.TouchAreaL;
        _Hand_L_Animator = _Hand_L.GetComponentInChildren<Animator>();
        _Hand_R.SetActive(false);
        _Hand_L.SetActive(false);
        _Point.SetActive(false);
        _TouchAreaR.SetActive(false);
        _TouchAreaL.SetActive(false);
        _TouchAreaCount = 0.0f;

        var Now = CGlobal.GetServerTimePoint();
        _DelayTime = Now;

        var Obj = new GameObject(CGlobal.NickName);
        Obj.transform.SetParent(_CharacterParent.transform);
        Obj.transform.localPosition = new Vector3(-1.0f,-0.911f, 0.0f);

        var Character = new SSingleCharacter(new SSingleCharacterMove(), false, 0, 2, 0, 100, 0, new rso.physics.SPoint(0.0f, global.c_Gravity));
        var Player = new SSinglePlayer(CGlobal.UID, CGlobal.NickName, CGlobal.LoginNetSc.User.CountryCode, 0, CGlobal.LoginNetSc.User.SelectedCharCode, Character, Now);

        _Me = Obj.AddComponent<CBattlePlayerTutorial>();
        _Me.Init(CGlobal.MetaData.Chars[CGlobal.LoginNetSc.User.SelectedCharCode], Player, Character, Now, _ParticleParent, _BattleTutorialScene.Camera, this);

        _BattleTutorialScene.Camera.orthographicSize = CGlobal.OrthographicSize;
        CGlobal.SetIsTutorial(true);
    }

    public override void Dispose()
    {
    }
    public override bool Update()
    {
        if (_Exit)
        {
            return false;
        }
        if (_TutorialStep == ETutorialStep.Ready)
        {
            Int32 time = Mathf.CeilToInt((float)(CGlobal.GetServerTimePoint() - _DelayTime).TotalSeconds);
            if (time > 1)
            {
                _DelayTime = CGlobal.GetServerTimePoint();
                _TutorialStep = ETutorialStep.Start;
            }
        }
        else if (_TutorialStep == ETutorialStep.Start)
        {
            GameStart();
        }
        else if (_TutorialStep == ETutorialStep.Play)
        {
            _Pad.Update();
            if (_TutorialGrade == 0)
            {
                _TouchAreaCount += Time.deltaTime;
                if (_TouchAreaCount > 0.3f)
                {
                    _TouchAreaCount = 0.0f;
                    _TouchAreaL.SetActive(!_TouchAreaL.activeSelf);
                }
            }
            else if (_TutorialGrade == 1)
            {
                _TouchAreaCount += Time.deltaTime;
                if (_TouchAreaCount > 0.3f)
                {
                    _TouchAreaCount = 0.0f;
                    _TouchAreaR.SetActive(!_TouchAreaR.activeSelf);
                }
            }
        }

        if (rso.unity.CBase.BackPushed())
        {
            if (CGlobal.SystemPopup.gameObject.activeSelf)
            {
                CGlobal.SystemPopup.OnClickCancel();
                return true;
            }
            ExitClick();
        }

        return true;
    }
    public void GameStart()
    {
        _Me.GameStart();
        CGlobal.MusicPlayBattle();
        _TutorialStep = ETutorialStep.Play;
        _TutorialGrade = 0;
        TutorialSetting();
    }
    public void ExitClick()
    {
        CGlobal.SystemPopup.ShowPopup(EText.Tutorial_Popup_Skip, PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type_) => {
            if (type_ == PopupSystem.PopupBtnType.Ok)
            {
                CGlobal.MusicStop();
                CGlobal.SceneSetNext(new CSceneLobby());
            }
        });
    }
    public void NextTutorial()
    {
        _TutorialGrade++;
        _TouchAreaR.SetActive(false);
        _TouchAreaL.SetActive(false);
        _TouchAreaCount = 0.0f;
        TutorialSetting();
    }
    public void TutorialSetting()
    {
        _Hand_L_Animator.enabled = false;
        _Hand_R.SetActive(false);
        _Hand_L.SetActive(false);
        _Point.SetActive(false);
        switch (_TutorialGrade)
        {
            case 0:
                _Hand_L_Animator.enabled = true;
                _Hand_L.SetActive(true);
                _Point.transform.localPosition = _TutorialPointVectorArray[_TutorialGrade];
                _Point.SetActive(true);
                break;
            case 1:
                _Hand_R.SetActive(true);
                _Point.transform.localPosition = _TutorialPointVectorArray[_TutorialGrade];
                _Point.SetActive(true);
                break;
            case 2:
                _Hand_L_Animator.enabled = true;
                _Hand_L.SetActive(true);
                _Hand_R.SetActive(true);
                _Point.transform.localPosition = _TutorialPointVectorArray[_TutorialGrade];
                _Point.SetActive(true);
                break;
            case 3:
            default:
                {
                    CSceneLobby Scene;

                    if (CGlobal.LoginNetSc.User.TutorialReward == false)
                    {
                        Scene = new CSceneLobby(0, 0, CGlobal.MetaData.ConfigMeta.TutorialRewardDia);
                        CGlobal.LoginNetSc.User.TutorialReward = true;
                        CGlobal.NetControl.Send(new STutorialRewardNetCs());
                        AnalyticsManager.TrackingEvent(ETrackingKey.tutorial_1);
                    }
                    else
                    {
                        Scene = new CSceneLobby();
                    }
                    CGlobal.SystemPopup.ShowPopup(EText.Tutorial_Text_Complete, PopupSystem.PopupType.Confirm, (PopupSystem.PopupBtnType type_) => {
                        CGlobal.MusicStop();
                        CGlobal.SceneSetNext(Scene);
                    });

                    _TutorialStep = ETutorialStep.Exit;
                    _Me.Center();
                    _JoyPad.SetActive(true);
                }
                break;
        }
    }
}