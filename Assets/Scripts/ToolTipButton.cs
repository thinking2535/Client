using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipButton : MonoBehaviour
{
    [SerializeField] EText ToolTipText = EText.Null;
    public async void OnClick()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        await CGlobal.curScene.pushToolTipPopup(ToolTipText);
    }
}
