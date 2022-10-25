using rso.core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProgressCircle : MonoBehaviour
{
    [SerializeField] GameObject _Body = null;
    [SerializeField] GameObject _Circle = null;
    DateTime _ShowBodyTime;

    void Awake()
    {
        Deactivate();
    }
    void Update()
    {
        if (_Body.activeSelf)
        {
            _Circle.transform.Rotate(new Vector3(0.0f, 0.0f, 360.0f) * Time.deltaTime);
        }
        else
        {
            if (DateTime.Now >= _ShowBodyTime)
                _Body.SetActive(true);
        }
    }
    public void Activate()
    {
        if (gameObject.activeSelf)
            return;

        _ShowBodyTime = DateTime.Now + TimeSpan.FromSeconds(1.0);
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        if (!gameObject.activeSelf)
            return;

        _Body.SetActive(false);
        gameObject.SetActive(false);
    }
}
