using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NetDelayScene : MonoBehaviour
{
    public Text VersionText = null;
    public GameObject TitleImg0 = null;
    public GameObject TitleImg1 = null;
}
public class CSceneNetDelay : CSceneBase
{
    Text _VersionText = null;
    GameObject _TitleImg0 = null;
    GameObject _TitleImg1 = null;

    public CSceneNetDelay() :
        base("Prefabs/NetDelayScene", Vector3.zero, true)
    {
    }
    public override void Dispose()
    {
    }

    public override void Enter()
    {
        _VersionText = (_Object.GetComponent<NetDelayScene>()).VersionText;
        _TitleImg0 = (_Object.GetComponent<NetDelayScene>()).TitleImg0;
        _TitleImg1 = (_Object.GetComponent<NetDelayScene>()).TitleImg1;
        _VersionText.text = "Version : " + CGlobal.VersionText();
        if (CGlobal.IntroTitle == 0)
        {
            _TitleImg0.SetActive(true);
            _TitleImg1.SetActive(false);
        }
        else
        {
            _TitleImg0.SetActive(false);
            _TitleImg1.SetActive(true);
        }
    }

    public override bool Update()
    {
        if (_Exit)
        {
            return false;
        }
        if (rso.unity.CBase.BackPushed())
        {
            if (CGlobal.SystemPopup.gameObject.activeSelf)
            {
                CGlobal.SystemPopup.OnClickCancel();
                return true;
            }
        }
        return true;
    }
}
