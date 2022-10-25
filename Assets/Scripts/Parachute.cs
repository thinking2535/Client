using rso.unity;
using System;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    public float Scale = 0.0f;
    readonly float _Offset = 0.06f;

    public void Init(Material material)
    {
        GetComponentInChildren<MeshRenderer>().material = material;
        transform.localPosition = new Vector3(0.0f, 0.0f, _Offset);
        transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        transform.localPosition = new Vector3(0.0f, 0.0f, _Offset * Scale);
        transform.localScale = new Vector3(Scale, Scale, Scale);
    }
}
