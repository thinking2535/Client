using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanel : MonoBehaviour
{
    [SerializeField] Text NickName = null;
    [SerializeField] Text Message = null;

    public void InitChatPanel(string Nick_, string Msg_)
    {
        NickName.text = Nick_;
        Message.text = Msg_;
    }
}
