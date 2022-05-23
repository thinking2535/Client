using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectorScene : MonoBehaviour
{
    public GameObject MapContentsObject = null;

    public void Back()
    {
        CGlobal.SceneSetNext(new CSceneLobby());
    }
}
public class CSceneMapSelector : CSceneBase
{
    MapSelectorScene _MapSelectorScene = null;
    GameObject _MapContentsObject = null;
    public CSceneMapSelector() :
        base("Prefabs/MapSelectorScene", Vector3.zero, true)
    {

    }
    public override void Dispose()
    {
    }

    public override void Enter()
    {
        _MapSelectorScene = _Object.GetComponent<MapSelectorScene>();
        _MapContentsObject = _MapSelectorScene.MapContentsObject;

        //MapButton
        MapButton MapButtonPrefab = Resources.Load<MapButton>("Prefabs/UI/MapButton");

        var newObj = UnityEngine.Object.Instantiate<MapButton>(MapButtonPrefab);
        newObj.transform.SetParent(_MapContentsObject.transform);
        newObj.transform.localPosition = new Vector3(newObj.transform.localPosition.x, newObj.transform.localPosition.y, 0.0f);
        newObj.transform.localScale = Vector3.one;
        newObj.Init("Random", -1);

        foreach (var Map in CGlobal.MetaData.Maps.MapMulties)
        {
            newObj = UnityEngine.Object.Instantiate<MapButton>(MapButtonPrefab);
            newObj.transform.SetParent(_MapContentsObject.transform);
            newObj.transform.localPosition = new Vector3(newObj.transform.localPosition.x, newObj.transform.localPosition.y, 0.0f);
            newObj.transform.localScale = Vector3.one;
            newObj.Init(Map.PrefabName, CGlobal.MetaData.Maps.MapMulties.IndexOf(Map));
        }
    }

    public override bool Update()
    {
        if (_Exit)
            return false;

        if (rso.unity.CBase.BackPushed())
        {
            CGlobal.SceneSetNext(new CSceneLobby());
            return false;
        }

        return true;
    }

    public void SetMapSetting(Int32 Index_)
    {
        CGlobal.NetControl.Send(new SChatNetCs("/setmapindex " + Index_.ToString()));
        CGlobal.SceneSetNext(new CSceneLobby());
    }
}
