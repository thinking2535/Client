using bb;
using rso.physics;
using rso.unity;
using System;
using System.Linq;
using UnityEngine;

public class TeamMaterial
{
    public Material balloon { get; }
    public Material parachute { get; }

    public TeamMaterial(string balloonName, string parachuteName)
    {
        balloon = Resources.Load<Material>(balloonName);
        parachute = Resources.Load<Material>(parachuteName);
    }
}
public abstract class BattleScene : NoMoneyUIScene
{
    protected TeamMaterial[] _teamMaterials = new TeamMaterial[bb.global.c_MaxPlayer + 1];
    protected CEngine _Engine;
    protected CPadSimulator _joypadSimulator;
    protected Joypad _joypad;
    protected GameObject _joypadController;
    protected Transform _Prop;
    protected SPoint _Map;
    protected CObject2D _RootObject;

    protected override void Update()
    {
        base.Update();

        if (!_Engine.IsStarted())
            return;

        _Engine.Update();
        _joypadSimulator.update();

        if (_joypad.gameObject.activeSelf)
            _updateJoypadPosition();
    }
    public void init(CEngine engine, SPoint map)
    {
        base.init();

        _teamMaterials[0] = new TeamMaterial("Material/Balloon_01_Red", "Material/Parachute_Red");
        _teamMaterials[1] = new TeamMaterial("Material/Balloon_01_Yellow", "Material/Parachute_Yellow");
        _teamMaterials[2] = new TeamMaterial("Material/Balloon_01_Orange", "Material/Parachute_Orange");
        _teamMaterials[3] = new TeamMaterial("Material/Balloon_01_Bluegreen", "Material/Parachute_Bluegreen");
        _teamMaterials[4] = new TeamMaterial("Material/Balloon_01_Purple", "Material/Parachute_Purple");
        _teamMaterials[5] = new TeamMaterial("Material/Balloon_01_Green", "Material/Parachute_Green");
        _teamMaterials[6] = new TeamMaterial("Material/Balloon_01_Blue", "Material/Parachute_Blue");

        for (Int32 i = 0; i < bb.global.c_MaxPlayer + 1; ++i)
            Debug.Assert(_teamMaterials[i] != null);

        _Engine = engine;
        _Engine.fFixedUpdate = _fixedUpdate;

        // 뷰포트의 0.02% 영역을 activeRange 로 잡고 이것에 대한 스크린 좌표계로의 길이를 구함
        var activeRangeStartPointX = camera.ViewportToScreenPoint(new Vector3(0.0f, 0.0f, 0.0f));
        var activeRangeEndPointX = camera.ViewportToScreenPoint(new Vector3(0.017f, 0.0f, 0.0f));
        var activeRange = activeRangeEndPointX.x - activeRangeStartPointX.x;

        // 뷰포트 0, 0에 해당하는 ScreenPoint : screenPointOfBottomLeftViewPort
        var screenPointOfBottomLeftViewPort = camera.ViewportToScreenPoint(Vector3.zero);
        // bottomLeftScreenPoint.x : a
        // 뷰포트의 가로 0.125%에 해당하는 ScreenPoint.X : screenPointOfDefaultJoypadX 
        var screenPointOfDefaultJoypadX = camera.ViewportToScreenPoint(new Vector3(0.125f, 0.0f, 0.0f)).x;
        // 뷰포트 좌하단에서부터 조이패드까지의 ScreenPoint 가로거리 : screenPointXRangeOfDefaultJoypad = screenPointOfDefaultJoypadX.x - screenPointOfBottomLeftViewPort.x
        float screenPointXRangeOfDefaultJoypad = screenPointOfDefaultJoypadX - screenPointOfBottomLeftViewPort.x;
        // 뷰포트 0, 0 에서 screenPointXRangeOfDefaultJoypad 만큼 위로 올라간 곳의 ScreenPoint y 좌표 : d = screenPointOfBottomLeftViewPort.y + screenPointXRangeOfDefaultJoypad
        var screenPointOfDefaultJoypadY = screenPointOfBottomLeftViewPort.y + screenPointXRangeOfDefaultJoypad;
        // defaultPosition : screenPointOfDefaultJoypadX.x, screenPointOfDefaultJoypadY.y
        var defaultJoypadPosition = new Vector2(screenPointOfDefaultJoypadX, screenPointOfDefaultJoypadY);

        _joypadSimulator = CGlobal.getJoypadSimulator(_touched, _pushed, activeRange, defaultJoypadPosition);

        {
            var joypadObject = new GameObject("joypad");
            _joypad = joypadObject.AddComponent<Joypad>();
            _joypad.gameObject.SetActive(CGlobal.GameOption.Data.IsPad);
            _updateJoypadPosition();
            joypadObject.transform.SetParent(transform, false);
        }

        _Prop = gameObject.transform.Find(CGlobal.c_PropName);
        _Map = map;
        _RootObject = new CObject2D(CPhysics.GetDefaultTransform(_Map));
    }
    void _updateJoypadPosition()
    {
        var newFramePosition = camera.ScreenToWorldPoint(new Vector3(_joypadSimulator.centerPosition.x, _joypadSimulator.centerPosition.y));
        _joypad.transform.position = new Vector3(newFramePosition.x, newFramePosition.y, 2.0f);

        var newStickPosition = camera.ScreenToWorldPoint(new Vector3(_joypadSimulator.touchPosition.x, _joypadSimulator.touchPosition.y));
        _joypad.setStickLocalPosition(newStickPosition - newFramePosition);
    }
    protected void _showItemGetEffect(Vector3 localPosition)
    {
        var prefab = Resources.Load("FX/00_FXPrefab/FX_Coin");
        var obj = (GameObject)UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, _Prop);
        obj.name = "ItemEffect";
        obj.transform.localPosition = localPosition;
        obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        CGlobal.Sound.PlayOneShot((Int32)ESound.item_Pickup);
    }
    protected virtual bool _touched(InputTouch.TouchState state, Int32 direction)
    {
        if (!_Engine.IsStarted())
            return false;

        return _getMyBattlePlayer().IsAlive();
    }
    protected virtual bool _pushed(InputTouch.TouchState state)
    {
        if (!_Engine.IsStarted())
            return false;

        return _getMyBattlePlayer().IsAlive();
    }
    protected abstract CBattlePlayer _getMyBattlePlayer();
    protected abstract void _fixedUpdate();
}
