using bb;
using rso.core;
using rso.unity;
using UnityEngine;

public class CSceneLogin : CSceneBase
{
    public CSceneLogin() :
        base("Prefabs/LoginScene", Vector3.zero, true)
    {
    }
    public override void Dispose()
    {
    }
    public override void Enter()
    {
        var Stream = new CStream();
        Stream.Push(new SUserLoginOption(rso.unity.CBase.GetOS()));
#if UNITY_EDITOR
        var DataPath = Application.dataPath + "/../";
#else
        var DataPath = Application.persistentDataPath + "/";
#endif
        if (!CGlobal.NetControl.Login(CGlobal.GameIPPort, "", 0, Stream,
                                      DataPath + CGlobal.GameIPPort.Name + "_" + CGlobal.GameIPPort.Port.ToString() + "_" + "Data/"))
        {
            CGlobal.CreatePopup.Show(CGlobal.Create);
            return;
        }
    }
    public override bool Update()
    {
        if (_Exit)
            return false;

        if (rso.unity.CBase.BackPushed())
        {
            if (CGlobal.SystemPopup.gameObject.activeSelf)
            {
                CGlobal.SystemPopup.OnClickCancel();
                return true;
            }
            CGlobal.SystemPopup.ShowGameOut();
        }

        return true;
    }
}
