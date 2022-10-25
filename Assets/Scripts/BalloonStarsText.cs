using bb;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/BalloonStarsText - Text", 12)]
public class BalloonStarsText : Text
{
    [SerializeField] public ELanguage _eLanguage;
    [SerializeField] public EText _eText;

    protected override void Awake()
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
        text = data.getText(_eText);
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
                balloonText.text = CGlobal.MetaData.getText(balloonText._eText, balloonText._eLanguage);
            }
            else
            {
                balloonText.text = ComponentMetaData.GetMetaData().getText(balloonText._eText, balloonText._eLanguage);
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
                balloonText.text = CGlobal.MetaData.getText(balloonText._eText, balloonText._eLanguage);
            }
            else
            {
                balloonText.text = ComponentMetaData.GetMetaData().getText(balloonText._eText, balloonText._eLanguage);
            }
#else
            CGlobal.MetaData.ChangeLanguage(balloonText._eLanguage);
            balloonText.text = CGlobal.MetaData.GetText(balloonText._eText);
#endif
        }
    }
}
#endif