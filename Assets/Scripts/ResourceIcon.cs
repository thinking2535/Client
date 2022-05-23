using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceIcon : MonoBehaviour
{
    public Image Icon = null;
    public bool IsActive{ get { return gameObject.activeSelf; } set { gameObject.SetActive(value); } }
    public EResource ResourceEnum { get { return Resource; }  set { SetResourceIcon(value, 0); } }
    private EResource Resource = EResource.Null;
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public bool IsAdd = false;
    public bool IsMove = false;
    public float Velocity = 12.0f;
    public Int32 ResourceCount = 0;
    public void SetResourceIcon(EResource EResource_, Int32 ResourceCount_)
    {
        Velocity = 12.0f;
        Resource = EResource_;
        ResourceCount = ResourceCount_;
        Icon.sprite = Resources.Load<Sprite>(CGlobal.GetResourcesIconFile(Resource));
    }
}
