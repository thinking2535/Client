using rso.physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineUnityStructure : MonoBehaviour
{
    public Vector3 BeginPos = Vector3.zero;
    public Vector3 EndPos = Vector3.zero;
    public float Velocity = 1.0f;
    public float Delay = 0.0f;

    public CObject2D EngineObject;

    private void Update()
    {
        transform.position = EngineObject.Position.ToVector3();
    }
}
