using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEditor : CDevice
{
    public override string GetDataPath()
    {
        return Application.dataPath + "/../";
    }
    public override string GetUpdateUrl()
    {
        return "http://play.google.com/store/apps/details?id=com.stairgames.balloonstars";
    }
}
