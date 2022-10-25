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