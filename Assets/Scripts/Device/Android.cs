using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAndroid : CMobile
{
    public override string GetUpdateUrl()
    {
        return "http://play.google.com/store/apps/details?id=com.stairgames.balloonstars";
    }
}
