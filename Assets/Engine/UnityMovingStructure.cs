using rso.physics;
using rso.unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMovingStructure : MonoBehaviour
{
    public Vector3 BeginPos = Vector3.zero;
    public Vector3 EndPos = Vector3.zero;
    public float Velocity = 1.0f;
    public float Delay = 0.0f;

    public CObject2D EngineObject;

    private void Update()
    {
        transform.localPosition = new Vector3(EngineObject.LocalPosition.X, EngineObject.LocalPosition.Y, transform.localPosition.z);
    }
}
