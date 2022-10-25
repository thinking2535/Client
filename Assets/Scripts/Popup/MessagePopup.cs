using bb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePopup : BasePopup
{
    [SerializeField] Text _message;

    public void init(EText titleName, bool isError, string message)
    {
        base.init(titleName, isError);

        _message.text = message;
    }
}
