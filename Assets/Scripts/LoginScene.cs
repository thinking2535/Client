using bb;
using rso.core;
using rso.game;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{
    [SerializeField] Text _VersionText = null;
    [SerializeField] GameObject _TitleImg0 = null;
    [SerializeField] GameObject _TitleImg1 = null;
    private void Start()
    {
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
}
