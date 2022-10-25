using bb;
using rso.core;
using rso.unity;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public abstract class CMobile : CDevice
{
    public override string GetDataPath()
    {
        return Application.persistentDataPath + "/";
    }
}
