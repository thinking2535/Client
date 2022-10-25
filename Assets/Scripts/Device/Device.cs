using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class CDevice
{
    public abstract string GetDataPath();
    public abstract string GetUpdateUrl();
    public virtual Task<string> CheckVersion()
    {
        return null;
    }
}
