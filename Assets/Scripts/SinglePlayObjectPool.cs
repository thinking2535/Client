using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class SinglePlayObjectPool : MonoBehaviour
{
    [SerializeField] SinglePlayObject SingleObjectOrigin = null;
    [SerializeField] GameObject SingleObjectParent = null;
    List<SinglePlayObject> ObjectPoolList = new List<SinglePlayObject>();
    
    public void Init(Int32 InitCount_)
    {
        for (Int32 i = 0; i < InitCount_; ++i)
        {
            var Obj = Object.Instantiate<SinglePlayObject>(SingleObjectOrigin);
            ObjectPoolList.Add(Obj);
            Obj.transform.SetParent(SingleObjectParent.transform);
            Obj.DisableObject();
        }
    }
    public void UseObject(SinglePlayObject.EItemType ItemType_, Vector3 StartPos_, Vector3 EndPos_, float Velocity_, float Angle_)
    {
        SinglePlayObject ReturnObj = null;
        foreach (var Obj in ObjectPoolList)
        {
            if(!Obj.GetActive())
            {
                ReturnObj = Obj;
            }
        }
        if(ReturnObj == null)
        {
            ReturnObj = Object.Instantiate<SinglePlayObject>(SingleObjectOrigin);
            ObjectPoolList.Add(ReturnObj);
            ReturnObj.transform.SetParent(SingleObjectParent.transform);
            ReturnObj.DisableObject();
        }
        ReturnObj.EnableObject(ItemType_, StartPos_, EndPos_, Velocity_, Angle_);
    }
    public void UseObject(SinglePlayObject.EItemType ItemType_, Vector3 Pos_)
    {
        SinglePlayObject ReturnObj = null;
        foreach (var Obj in ObjectPoolList)
        {
            if (!Obj.GetActive())
            {
                ReturnObj = Obj;
            }
        }
        if (ReturnObj == null)
        {
            ReturnObj = Object.Instantiate<SinglePlayObject>(SingleObjectOrigin);
            ObjectPoolList.Add(ReturnObj);
            ReturnObj.transform.SetParent(SingleObjectParent.transform);
            ReturnObj.DisableObject();
        }
        ReturnObj.EnableObject(ItemType_, Pos_);
    }
    public void UnuseObject(SinglePlayObject Obj_)
    {
        Obj_.DisableObject();
    }
    public void AllUnuseObject()
    {
        foreach (var Obj in ObjectPoolList)
        {
            Obj.DisableObject();
        }
    }
    public bool IsActive()
    {
        bool IsActvie = false;
        foreach (var Obj in ObjectPoolList)
        {
            if (Obj.GetActive())
            {
                if(Obj.GetMove())
                {
                    IsActvie = true;
                    return IsActvie;
                }
            }
        }
        return IsActvie;
    }
    public bool GetUseObjectCheck(SinglePlayObject.EItemType ItemType_)
    {
        bool IsCheck = false;
        foreach (var Obj in ObjectPoolList)
        {
            if (Obj.GetActive() && Obj.GetItemType() == ItemType_)
                IsCheck = true;
        }
        return IsCheck;
    }
}
