#if UNITY_EDITOR

using rso.core;
using rso.physics;
using bb;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public static class ComponentMetaData
{
    private static CMetaData MetaData = null;
    public static CMetaData GetMetaData()
    {
        if (MetaData == null)
            return new CMetaData();
        else
            return MetaData;
    }
}

class BalloonStarsComponent
{
    //    static string[] menuPath = { "SelectServer/SelectServer/External", "SelectServer/SelectServer/Internal", "SelectServer/SelectServer/AWS", "SelectServer/SelectServer/Dev" };
    //    [MenuItem("SelectServer/SelectServer/External")]
    //    public static void SelectServerExternal()
    //    {
    //        MenuSelectServer(0);
    //    }
    //    [MenuItem("SelectServer/SelectServer/Internal")]
    //    public static void SelectServerInternal()
    //    {
    //        MenuSelectServer(1);
    //    }
    //    [MenuItem("SelectServer/SelectServer/AWS")]
    //    public static void SelectServerAWS()
    //    {
    //        MenuSelectServer(2);
    //    }
    //    [MenuItem("SelectServer/SelectServer/Dev")]
    //    public static void SelectServerDev()
    //    {
    //        MenuSelectServer(3);
    //    }

    //    static void MenuSelectServer(Int32 idx_)
    //    {
    //        foreach (var path in menuPath)
    //        {
    //            Menu.SetChecked(path, false);
    //        }
    //        Menu.SetChecked(menuPath[idx_], true);
    //        Main._Server = (Main._EServer)idx_;
    //    }

    static List<SMapMulti> _GetMapInfo(string RelativePath_)
    {
        Debug.Log("GetMapInfo Begin");

        var Maps = new List<SMapMulti>();
        var prefabPath = "Prefabs/Maps/" + RelativePath_;

        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo("./Assets/Resources/" + prefabPath);

        FileInfo[] fi = di.GetFiles("*.prefab");

        Debug.Log("GetMapInfo GetFiles");

        foreach (var item in fi)
        {
            Debug.Log("GetMapInfo item : " + item.Name);

            var prefabPathFile = Path.Combine(prefabPath, item.Name.Replace(".prefab", ""));
            var battleMultiScene = Resources.Load(prefabPathFile) as GameObject;

            // Player Pos
            var playerPosLayer = battleMultiScene.GetComponentInChildren<PlayerPosSave>();

            var MapMeta = new Dictionary<sbyte, SPlayerPos>();
            var Multi2Team = playerPosLayer.Get2TeamPlayerPosList();
            var Multi3Team = playerPosLayer.Get3TeamPlayerPosList();
            var Multi6Team = playerPosLayer.Get6TeamPlayerPosList();

            MapMeta.Add(Multi2Team.Item1, new SPlayerPos(Multi2Team.Item2));
            MapMeta.Add(Multi3Team.Item1, new SPlayerPos(Multi3Team.Item2));
            MapMeta.Add(Multi6Team.Item1, new SPlayerPos(Multi6Team.Item2));

            var Structures = new List<SStructure>();
            var StructureMoves = new List<SStructureMove>();

            Debug.Log("GetMapInfo  GetStructures Begin");
            SPoint PropPosition;
            battleMultiScene.GetStructures(out PropPosition, Structures, StructureMoves);
            Debug.Log("GetMapInfo  GetStructures End");

            Maps.Add(new SMapMulti(prefabPathFile, MapMeta, PropPosition, Structures, StructureMoves));
            Debug.Log("GetMapInfo Add SMapMulti");
        }

        Debug.Log("GetMapInfo End");

        return Maps;
    }

    [MenuItem("MapEditor/PlayerPosSave")]
    static void PlayerPosSave()
    {
        Debug.Log("PlayerPosSave Begin");
        var MapInfoOneOnOne = _GetMapInfo("OneOnOne/");
        Debug.Log("PlayerPosSave 0");
        var MapInfo = _GetMapInfo("");
        Debug.Log("PlayerPosSave 1");
        var mapMeta = new SMapMeta(MapInfoOneOnOne, MapInfo);

        Debug.Log("PlayerPosSave 2");

        var Stream = new CStream();
        Stream.Push(mapMeta);

        Debug.Log("PlayerPosSave 3");

        Stream.SaveFile("../../Server/Bin/GameServer/MetaData/Map.bin");
        Debug.Log("PlayerPosSave 4");
        Stream.SaveFile("Assets/Resources/MetaData/Map.bytes");
        Debug.Log("PlayerPosSave 5");

        Debug.Log("MetaData/Map.bin Export Complete!!!");
    }
    [MenuItem("VersionCheck/GetVersion")]
    static void GetVersion()
    {
        ulong Checksum = 0;
        TextAsset t = Resources.Load<TextAsset>("MetaData/Checksum");
        new CStream(t.bytes).Pop(ref Checksum);
        Debug.Log("NetProtocal Version : " + global.c_Ver_Main.ToString());
        Debug.Log("MetaData/Checksum : "+ Checksum.ToString());
    }


    [MenuItem("GameObject/UI/Text - Ballonstars", false, 2001)]
    static void CreateTextBalloonStars(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("BalloonStarsText");

        go.transform.parent = ((GameObject)menuCommand.context).transform;
        // Override text color and font size
        var text = go.AddComponent<BalloonStarsText>();
        text.rectTransform.sizeDelta = new Vector2(100.0f, 100.0f);
        text.rectTransform.localPosition = Vector3.zero;
        text.rectTransform.localScale = Vector3.one;
        text.rectTransform.localRotation = Quaternion.identity;

        Selection.activeObject = go;
    }

    [MenuItem("GameObject/UI/Button - Ballonstars", false, 2001)]
    static void CreateButtonBalloonStars(MenuCommand menuCommand)
    {
        GameObject button = new GameObject("BalloonStarsButton");
        button.transform.parent = ((GameObject)menuCommand.context).transform;

        var rectTransform = button.AddComponent<RectTransform>();
        button.AddComponent<CanvasRenderer>();
        button.AddComponent<Button>();
        button.AddComponent<Image>();

        rectTransform.sizeDelta = new Vector2(100.0f, 100.0f);
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.localRotation = Quaternion.identity;

        GameObject go = new GameObject("BalloonStarsText");

        go.transform.parent = button.transform;
        // Override text color and font size
        var text = go.AddComponent<BalloonStarsText>();
        text.rectTransform.sizeDelta = new Vector2(100.0f, 100.0f);
        text.rectTransform.localPosition = Vector3.zero;
        text.rectTransform.localScale = Vector3.one;
        text.rectTransform.localRotation = Quaternion.identity;

        Selection.activeObject = go;
    }

    [MenuItem("GameObject/UI/InputText - Ballonstars", false, 2001)]
    static void CreateInputTextBalloonStars(MenuCommand menuCommand)
    {
        GameObject inputText = new GameObject("BalloonStarsInputText");
        inputText.transform.parent = ((GameObject)menuCommand.context).transform;

        var rectTransform = inputText.AddComponent<RectTransform>();
        inputText.AddComponent<CanvasRenderer>();
        inputText.AddComponent<Image>();
        var inputField = inputText.AddComponent<InputField>();

        rectTransform.sizeDelta = new Vector2(100.0f, 100.0f);
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.localRotation = Quaternion.identity;

        GameObject PlaceHolder = new GameObject("PlaceHolder");

        PlaceHolder.transform.parent = inputText.transform;
        var PlaceHolderText = PlaceHolder.AddComponent<BalloonStarsText>();
        PlaceHolderText.rectTransform.anchorMax = new Vector2(1.0f, 1.0f);
        PlaceHolderText.rectTransform.localPosition = Vector3.zero;
        PlaceHolderText.rectTransform.localScale = Vector3.one;
        PlaceHolderText.rectTransform.localRotation = Quaternion.identity;

        GameObject textObj = new GameObject("Text");
        textObj.transform.parent = inputText.transform;
        var text = textObj.AddComponent<Text>();
        text.rectTransform.anchorMax = new Vector2(1.0f, 1.0f);
        text.rectTransform.localPosition = Vector3.zero;
        text.rectTransform.localScale = Vector3.one;
        text.rectTransform.localRotation = Quaternion.identity;
        text.supportRichText = false;

        inputField.textComponent = text;
        inputField.placeholder = PlaceHolderText;

        Selection.activeObject = inputText;
    }
}
#endif