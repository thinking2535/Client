using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    public Text MapText = null;
    Int32 MapIndex = -1;

    public void Init(string MapName_, Int32 Index_)
    {
        MapText.text = MapName_;
        MapIndex = Index_;
    }

    public void Click()
    {
        var Scene = CGlobal.GetScene<CSceneMapSelector>();
        if (Scene == null) return;
        Scene.SetMapSetting(MapIndex);
    }
}
