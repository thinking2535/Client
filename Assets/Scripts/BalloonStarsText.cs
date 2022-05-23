using bb;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/BalloonStarsText - Text", 12)]
public class BalloonStarsText : Text
{
    public ELanguage _eLanguage;
    public EText _eText;

    private new void Awake()
    {
        base.Awake();
        CMetaData data = null;
#if UNITY_EDITOR
        if (Application.isPlaying == true)
            data = CGlobal.MetaData;
        else
            data = ComponentMetaData.GetMetaData();
#else
        data = CGlobal.MetaData;
#endif
        text = data.GetText(_eText);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BalloonStarsText))]
public class BalloonStarsTextEditor : Editor
{
    BalloonStarsText balloonText;

    private void Awake()
    {
        balloonText = (BalloonStarsText)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        base.OnInspectorGUI();

        if (EditorGUI.EndChangeCheck())
        {
#if UNITY_EDITOR
            if (Application.isPlaying == true)
            {
                CGlobal.MetaData.ChangeLanguage(balloonText._eLanguage);
                balloonText.text = CGlobal.MetaData.GetText(balloonText._eText);
            }
            else
            {
                ComponentMetaData.GetMetaData().ChangeLanguage(balloonText._eLanguage);
                balloonText.text = ComponentMetaData.GetMetaData().GetText(balloonText._eText);
            }
#else
            CGlobal.MetaData.ChangeLanguage(balloonText._eLanguage);
            balloonText.text = CGlobal.MetaData.GetText(balloonText._eText);
#endif
        }
    }
    public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
    {
        EditorGUI.BeginChangeCheck();

        base.OnInteractivePreviewGUI(r,background);

        if (EditorGUI.EndChangeCheck())
        {
#if UNITY_EDITOR
            if (Application.isPlaying == true)
            {
                CGlobal.MetaData.ChangeLanguage(balloonText._eLanguage);
                balloonText.text = CGlobal.MetaData.GetText(balloonText._eText);
            }
            else
            {
                ComponentMetaData.GetMetaData().ChangeLanguage(balloonText._eLanguage);
                balloonText.text = ComponentMetaData.GetMetaData().GetText(balloonText._eText);
            }
#else
            CGlobal.MetaData.ChangeLanguage(balloonText._eLanguage);
            balloonText.text = CGlobal.MetaData.GetText(balloonText._eText);
#endif
        }
    }
}
#endif