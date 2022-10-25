using bb;
using System;
using UnityEngine;

public abstract class MoneyUIScene : BaseScene
{
    [SerializeField] internal GameObject[] _ToolTipButtons;
    bool _isToolTipOn = false;

    public new void init()
    {
        base.init();

        _updateToolTip();
        CGlobal.MoneyUI.show(camera, toggleToolTip);
    }
    public void toggleToolTip()
    {
        _isToolTipOn = !_isToolTipOn;
        _updateToolTip();
    }
    void _updateToolTip()
    {
        foreach (var i in _ToolTipButtons)
            i.SetActive(_isToolTipOn);
    }
    public virtual void buyCharacterNetSc(Int32 characterCode)
    {
    }
    public virtual void ShowCharacterInfoPopup(SCharacterMeta CharacterMeta_)
    {
    }
}
