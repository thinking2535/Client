using rso.unity;
using System;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    CEngineGameMode _GameMode;
    public float Scale = 0.0f;
    readonly float _Offset = 0.06f;

    public void Init(CEngineGameMode GameMode_, SByte TeamIndex_, SByte MyTeamIdx_)
    {
        _GameMode = GameMode_;
        SetMaterial(Resources.Load<Material>(GameMode_.GetParachuteMaterialName(TeamIndex_, MyTeamIdx_)));
    }
    public void SingleInit()
    {
        SetMaterial(Resources.Load<Material>("Material/Parachute_Blue"));
    }
    private void SetMaterial(Material Material_)
    {
        var r = GetComponentInChildren<MeshRenderer>();
        r.material = Material_;
        transform.localPosition = new Vector3(0.0f, 0.0f, _Offset);
        transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        transform.localPosition = new Vector3(0.0f, 0.0f, _Offset * Scale);
        transform.localScale = new Vector3(Scale, Scale, Scale);
    }
}
