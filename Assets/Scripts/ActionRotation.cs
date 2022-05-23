using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionRotation : MonoBehaviour
{
    [SerializeField] GameObject RotationObject = null;
    [SerializeField] Vector3 RotationVelocity = Vector3.zero;

    private void Update()
    {
        RotationObject.transform.eulerAngles += RotationVelocity;
    }
}
