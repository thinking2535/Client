using rso.physics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    [SerializeField] GameObject[] _Poses = null;
    public List<SPoint> Get()
    {
        var Poses = new List<SPoint>();

        foreach (var i in _Poses)
            Poses.Add(new SPoint(i.transform.localPosition.x, i.transform.localPosition.y));

        return Poses;
    }
}
