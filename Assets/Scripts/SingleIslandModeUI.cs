using bb;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SingleIslandModeUI : SingleModeUI
{
    [SerializeField] Image _StaminaBar = null;

    public void SetStaminaBar(float Value_)
    {
        _StaminaBar.transform.localScale = new Vector3(Value_, 1.0f, 1.0f);
    }
}
