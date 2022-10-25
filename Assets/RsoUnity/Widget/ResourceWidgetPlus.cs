using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResourceWidgetPlus : ResourceWidget
{
    [SerializeField] Button _PlusButton;
    public void Init(Sprite Icon_, Int32 MaxValue_, Int32 Value_, UnityAction fCallback_)
    {
        base.Init(Icon_, MaxValue_, Value_);
        _PlusButton.onClick.AddListener(fCallback_);
    }
}