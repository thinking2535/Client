using bb;
using rso.Base;
using rso.core;
using rso.physics;
using rso.unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class TutorialScene : LocalBattleScene
{
    enum _ETutorialStep
    {
        Ready,
        Play,
        Exit
    }

    [SerializeField] GameObject _Hand_L = null;
    [SerializeField] GameObject _Hand_R = null;
    [SerializeField] GameObject _TouchAreaR = null;
    [SerializeField] GameObject _TouchAreaL = null;
    [SerializeField] GameObject _startPosition;
    [SerializeField] CheckPoint _checkPoint;
    [SerializeField] GameObject[] _checkPointPositions;

    TutorialPlayer _Me = null;

    float _startedTime;
    float _TouchAreaCount;

    _ETutorialStep _TutorialStep = _ETutorialStep.Ready;
    Int32 _TutorialGrade = 0;
    CList<CCollider2D>.SIterator _itCheckPoint;
    Vector3 _checkPointColliderOffset;

    public new void init()
    {
        var Prop = gameObject.transform.Find(CGlobal.c_PropName);
        Debug.Assert(Prop != null);

        base.init(Prop.position.ToSPoint());

        // Structure /////////////////////////////////
        {
            var structures = _Prop.transform.Find("structures");
            var boxCollider2Ds = structures.GetComponentsInChildren<BoxCollider2D>();

            foreach (var c in boxCollider2Ds)
                _Engine.AddObject(new CRectCollider2D(c.transform.ToSTransform(), CEngineGlobal.c_StructureNumber, c.ToSRectCollider2D(), _RootObject));
        }

        // Checkpoint
        {
            var checkPointBoxCollider2D = _checkPoint.GetComponent<BoxCollider2D>();
            _checkPointColliderOffset = new Vector3(checkPointBoxCollider2D.offset.x, checkPointBoxCollider2D.offset.y, 0.0f);
            var checkPointRectCollider2D = checkPointBoxCollider2D.ToSRectCollider2D();

            var checkPointTransform = checkPointBoxCollider2D.transform.ToSTransform();
            var checkPointCollider = new CRectCollider2D(checkPointTransform, CEngineGlobal.c_ItemNumber, checkPointRectCollider2D);
            checkPointCollider.IsTrigger = true;
            _itCheckPoint = _Engine.AddObject(checkPointCollider);
            _checkPoint.init(checkPointTransform.LocalPosition);
            _checkPoint.setLocalPosition(_checkPointPositions[_TutorialGrade].transform.localPosition);
        }

        var Obj = new GameObject(CGlobal.NickName);
        Obj.transform.SetParent(_Prop);

        _Me = Obj.AddComponent<TutorialPlayer>();

        var characterMeta = CGlobal.MetaData.Characters[CGlobal.LoginNetSc.User.SelectedCharCode];
        _Me.init(
            new SPoint(),
            characterMeta,
            new SCharacterNet(
                new SCharacter(
                    false,
                    0,
                    0,
                    new SPumpInfo(0, 0, 0.0f),
                    new SParachuteInfo(0.0f, 0.0f),
                    new SStaminaInfo(0, characterMeta.StaminaMax),
                    1,
                    0,
                    0,
                    0,
                    0),
                _startPosition.transform.localPosition.ToSPoint(),
                new SPoint()),
            _teamMaterials.Last(),
            new SBattlePlayer(CGlobal.UID, CGlobal.NickName, CGlobal.LoginNetSc.User.CountryCode, 0, CGlobal.LoginNetSc.User.SelectedCharCode),
            _Prop,
            camera,
            NextTutorial);

        _Me.PlayerObject.SetParent(_RootObject);
        _Engine.AddPlayer(_Me.PlayerObject);

        _Engine.Start();

        _Hand_R.SetActive(false);
        _Hand_L.SetActive(false);
        _TouchAreaR.SetActive(false);
        _TouchAreaL.SetActive(false);
        _TouchAreaCount = 0.0f;

        _startedTime = Time.time;

        CGlobal.SetIsTutorial(true);
    }
    protected override void Update()
    {
        base.Update();

        if (_TutorialStep == _ETutorialStep.Ready)
        {
            if (Time.time - _startedTime > 1.0f)
            {
                CGlobal.MusicPlayBattle();
                _TutorialStep = _ETutorialStep.Play;
                _TutorialGrade = 0;
                _Engine.Start();
                TutorialSetting();
            }
        }
        else if (_TutorialStep == _ETutorialStep.Play)
        {
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
    }
    protected override async Task<bool> _backButtonPressed()
    {
        if (await base._backButtonPressed())
            return true;

        ExitClick();

        return true;
    }
    public void NextTutorial()
    {
        _showItemGetEffect(_checkPoint.transform.localPosition + _checkPointColliderOffset);
        ++_TutorialGrade;
        _TouchAreaR.SetActive(false);
        _TouchAreaL.SetActive(false);
        _TouchAreaCount = 0.0f;
        TutorialSetting();
    }
    public async void TutorialSetting()
    {
        _Hand_R.SetActive(false);
        _Hand_L.SetActive(false);

        switch (_TutorialGrade)
        {
            case 0:
                _Hand_L.SetActive(true);
                break;
            case 1:
                _Hand_R.SetActive(true);
                _checkPoint.setLocalPosition(_checkPointPositions[_TutorialGrade].transform.localPosition);
                break;
            case 2:
                _Hand_L.SetActive(true);
                _Hand_R.SetActive(true);
                _checkPoint.setLocalPosition(_checkPointPositions[_TutorialGrade].transform.localPosition);
                break;
            case 3:
            default:
                _Engine.RemoveObject(_itCheckPoint);
                _checkPoint.gameObject.SetActive(false);

                if (CGlobal.LoginNetSc.User.TutorialReward == false)
                {
                    CGlobal.LoginNetSc.User.TutorialReward = true;
                    CGlobal.LoginNetSc.User.Resources.AddResource(CGlobal.MetaData.ConfigMeta.TutorialRewardType, CGlobal.MetaData.ConfigMeta.TutorialRewardValue);
                    CGlobal.NetControl.Send(new STutorialRewardNetCs());
                    AnalyticsManager.TrackingEvent(ETrackingKey.tutorial_1);
                }

                _TutorialStep = _ETutorialStep.Exit;
                await pushNoticePopup(false, EText.Tutorial_Text_Complete);
                _exit();
                break;
        }
    }
    public async void ExitClick()
    {
        var clickedButtonType = await pushAskingPopup(EText.Tutorial_Popup_Skip);
        if (clickedButtonType is true)
            _exit();
    }
    void _exit()
    {
        CGlobal.MusicStop();
        CGlobal.sceneController.pop();
    }
    protected override CBattlePlayer _getMyBattlePlayer()
    {
        return _Me;
    }
}
