using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ModalDialog : Dialog
{
    public override Task backButtonPressed()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.curScene.popDialog();
        return Task.CompletedTask;
    }
    protected virtual void _okWithReturnValue(object returnValue)
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        CGlobal.curScene.popDialog(returnValue);
    }
}
