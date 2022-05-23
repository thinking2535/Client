using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    [SerializeField] Text _VersionText = null;
    [SerializeField] GameObject _TitleImg0 = null;
    [SerializeField] GameObject _TitleImg1 = null;
    Int32 _State = -1;
    float _EndTime = 0.0f;
    float _Duration = 1.0f;
    public void Update()
    {
        if (Time.time >= _EndTime)
        {
            _State = Int32.MaxValue;
        }
    }
    public void Enter()
    {
        _State = 0;
        _Duration = 2.0f;
        _EndTime = Time.time + _Duration;
        _VersionText.text = "Version : " + CGlobal.VersionText();
        CGlobal.IntroTitle = UnityEngine.Random.Range(0, 10) < 5 ? 0 : 1;
        if(CGlobal.IntroTitle == 0)
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
    public bool Ended()
    {
        return (_State == Int32.MaxValue);
    }
}
