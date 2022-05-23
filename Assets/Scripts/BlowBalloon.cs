using UnityEngine;
using rso.physics;
using System;

public class BlowBalloon : MonoBehaviour
{
    CEngineGameMode _GameMode;
    public void Init(CEngineGameMode GameMode_, SByte TeamIdx_, SByte MyTeamIdx_)
    {
        _GameMode = GameMode_;

        var m = Resources.Load<Material>(_GameMode.GetBalloonMaterialName(TeamIdx_, MyTeamIdx_));

        foreach (var i in GetComponentsInChildren<MeshRenderer>())
            i.material = m;

        gameObject.SetActive(false);
    }
    public void SingleInit()
    {
        Material m = Resources.Load<Material>("Material/Balloon_01_Blue");
        var r = GetComponentsInChildren<MeshRenderer>();
        foreach (var i in r)
            i.material = m;

        gameObject.SetActive(false);
    }
}
