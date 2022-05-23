using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using rso.unity;
using System.Linq;

public class LogPanel : MonoBehaviour
{
    [SerializeField] Text LogText_ = null;
    [SerializeField] GameObject LogView_ = null;
    public static List<string> DebugLogs;
    private CInputKey _InputKey = null;

    private string CommandString = "";

    void _Callback(KeyCode KeyCode_, bool Down_)
    {
        if (Down_) return;
        switch (KeyCode_)
        {
            case KeyCode.UpArrow:
                {
                    CommandString += "u";
                }
                break;
            case KeyCode.DownArrow:
                {
                    CommandString += "d";
                }
                break;
            case KeyCode.LeftArrow:
                {
                    CommandString += "l";
                }
                break;
            case KeyCode.RightArrow:
                {
                    CommandString += "r";
                }
                break;
            case KeyCode.X:
                {
                    CommandString += "x";
                }
                break;
            case KeyCode.Z:
                {
                    CommandString += "z";
                }
                break;
        }
        CheckCommand();
    }
    public void init()
    {
        DebugLogs = new List<string>();
        _InputKey = new CInputKey(_Callback);
        _InputKey.Add(KeyCode.UpArrow);
        _InputKey.Add(KeyCode.DownArrow);
        _InputKey.Add(KeyCode.LeftArrow);
        _InputKey.Add(KeyCode.RightArrow);
        _InputKey.Add(KeyCode.X);
        _InputKey.Add(KeyCode.Z);
    }
    public void ViewDebugPanel(bool isView_)
    {
        LogView_.SetActive(isView_);
    }
    public void AddLog(string log)
    {
        DebugLogs.Add(log);
        string logs = "";
        foreach (var logString in DebugLogs)
        {
            logs += logString;
        }
        LogText_.text = logs;
    }

    private void CheckCommand()
    {
        if(CommandString.Length > 10)
        {
            CommandString = CommandString.Substring(1);
        }
        if(CommandString.Equals("uuddlrlrxz"))
        {
            CGlobal.ViewLogPanel = !CGlobal.ViewLogPanel;
            ViewDebugPanel(CGlobal.ViewLogPanel);
        }
    }
    public void Update()
    {
        if(_InputKey!= null)
            _InputKey.Update();
    }
}
