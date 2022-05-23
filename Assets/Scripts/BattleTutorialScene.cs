using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTutorialScene : MonoBehaviour
{
    public Camera Camera = null;
    public GameObject ParticleParent = null;
    public GameObject CharacterParent = null;
    public GameObject JoyPad = null;
    public GameObject Hand_L = null;
    public GameObject Hand_R = null;
    public GameObject Point = null;
    public GameObject TouchAreaR = null;
    public GameObject TouchAreaL = null;
    public void ExitClick()
    {
        CGlobal.SystemPopup.ShowPopup(EText.Tutorial_Popup_Skip, PopupSystem.PopupType.CancelOk, (PopupSystem.PopupBtnType type_) => {
            if (type_ == PopupSystem.PopupBtnType.Ok)
            {
                CGlobal.MusicStop();
                CGlobal.SceneSetNext(new CSceneLobby());
            }
        });
    }
}
