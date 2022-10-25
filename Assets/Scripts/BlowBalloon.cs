using UnityEngine;
using rso.physics;
using System;

public class BlowBalloon : MonoBehaviour
{
    public void Init(Material material)
    {
        foreach (var i in GetComponentsInChildren<MeshRenderer>())
            i.material = material;

        gameObject.SetActive(false);
    }
}
