using rso.unity;
using System;
using UnityEngine;

public abstract class CSceneBase : CScene
{
    public CSceneBase(string PrefabName_, Vector3 Pos_, bool Active_) :
        base(PrefabName_, Pos_, Active_)
    {
    }
    public virtual void ResourcesUpdate()
    {
    }
    public virtual void ResourcesAction(Int32[] Resources_)
    {
    }
}
