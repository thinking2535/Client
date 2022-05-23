using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ButtonEvent :
    MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    [SerializeField]Vector3 moveVector = Vector3.zero;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        for (var i = 0; i< gameObject.transform.childCount; ++i )
        {
            gameObject.transform.GetChild(i).transform.localPosition += moveVector;
        }
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        for (var i = 0; i < gameObject.transform.childCount; ++i)
        {
            gameObject.transform.GetChild(i).transform.localPosition -= moveVector;
        }
    }
}
