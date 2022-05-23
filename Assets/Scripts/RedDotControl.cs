using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDotControl
{
    public enum EReddotType
    {
        Character,
        Quest,
        Rank,
        Shop,
        Max
    }
    Dictionary<EReddotType, bool> ReddotButtonList = new Dictionary<EReddotType, bool>();
    public List<int> NewChar = new List<int>();

    public RedDotControl()
    {
        foreach(var type in Enum.GetValues(typeof(EReddotType)))
        {
            AddReddotButton((EReddotType)type);
        }
    }

    public void AddReddotButton(EReddotType EReddotType_)
    {
        ReddotButtonList.Add(EReddotType_, false);
    }
    public bool IsReddotCheck(EReddotType EReddotType_)
    {
        return ReddotButtonList[EReddotType_];
    }
    public void SetReddotOff(EReddotType EReddotType_)
    {
        ReddotButtonList[EReddotType_] = false;
        if (EReddotType_ == EReddotType.Character)
            NewChar.Clear();
    }
    public void SetReddotDeleteChar(Int32 Code_)
    {
        NewChar.Remove(Code_);
        if(NewChar.Count > 0)
            ReddotButtonList[EReddotType.Character] = true;
        else
            ReddotButtonList[EReddotType.Character] = false;
    }
    public void SetReddotOn(EReddotType EReddotType_)
    {
        ReddotButtonList[EReddotType_] = true;
    }
    public void SetReddotChar(Int32 Code_)
    {
        NewChar.Add(Code_);
        ReddotButtonList[EReddotType.Character] = true;
    }
}
