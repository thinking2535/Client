using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyUser : MonoBehaviour
{
    [SerializeField] GameObject On = null;
    [SerializeField] GameObject Off = null;

    public void OnReady()
    {
        On.SetActive(true);
        Off.SetActive(false);
    }
    public void OffReady()
    {
        On.SetActive(false);
        Off.SetActive(true);
    }
}
