using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class SingleIslandObjectPool : MonoBehaviour
{
    [SerializeField] SingleIslandObject SingleObjectOrigin = null;
    [SerializeField] GameObject SingleObjectParent = null;
    [SerializeField] bool _IsMulti = false;
    List<SingleIslandObject> ObjectPoolList = new List<SingleIslandObject>();
    
    public void Init(Int32 InitCount_)
    {
        for (Int32 i = 0; i < InitCount_; ++i)
        {
            var Obj = Object.Instantiate<SingleIslandObject>(SingleObjectOrigin);
            ObjectPoolList.Add(Obj);
            Obj.transform.SetParent(SingleObjectParent.transform);
            Obj.DisableObject();
        }
    }
    public void UseObject(Int32 IslandType_, Vector3 Pos_, Int32 IslandCount_, float LandDelayTimeMax_, bool IsSpike_, Int32 SpikeCount_, float StaminaRecovery_, SingleIslandObject.EItemType Type_)
    {
        SingleIslandObject ReturnObj = null;
        foreach (var Obj in ObjectPoolList)
        {
            if (!Obj.GetActive())
            {
                ReturnObj = Obj;
            }
        }
        if (ReturnObj == null)
        {
            ReturnObj = Object.Instantiate<SingleIslandObject>(SingleObjectOrigin);
            ObjectPoolList.Add(ReturnObj);
            ReturnObj.transform.SetParent(SingleObjectParent.transform);
            ReturnObj.DisableObject();
        }
        ReturnObj.EnableObject(IslandType_, Pos_, IslandCount_, LandDelayTimeMax_, IsSpike_, SpikeCount_, StaminaRecovery_, Type_, _IsMulti);
    }
    public void UseObject(Int32 IslandType_, Vector3 Pos_, Int32 IslandCount_, float LandDelayTimeMax_, bool IsSpike_, Int32 SpikeCount_, float StaminaRecovery_)
    {
        UseObject(IslandType_, Pos_, IslandCount_, LandDelayTimeMax_, IsSpike_, SpikeCount_, StaminaRecovery_, SingleIslandObject.EItemType.Null);
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
                IsActvie = true;
                return IsActvie;
            }
        }
        return IsActvie;
    }
    public void MoveIsland(Vector3 Pos_)
    {
        foreach (var Obj in ObjectPoolList)
        {
            if (Obj.GetActive())
            {
                Obj.transform.localPosition += Pos_;
            }
        }
    }
    public SingleIslandObject GetIsland(Int32 Count_)
    {
        foreach (var Obj in ObjectPoolList)
        {
            if (Obj.GetActive())
            {
                if(Obj.GetIslandCount() == Count_)
                {
                    return Obj;
                }
            }
        }
        return null; //이쪽으로는 오면 안됩니다.
    }
    public List<SingleIslandObject> GetActiveList()
    {
        List<SingleIslandObject> returnList = new List<SingleIslandObject>();
        foreach (var Obj in ObjectPoolList)
        {
            if (Obj.GetActive())
                returnList.Add(Obj);
        }
        return returnList;
    }
}
