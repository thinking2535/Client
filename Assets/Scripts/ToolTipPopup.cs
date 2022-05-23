using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipPopup : MonoBehaviour
{
    [SerializeField] GameObject ToolTipScroll = null;
    [SerializeField] Text ToolTipText = null;
    [SerializeField] Text ToolTipScrollText = null;
    [SerializeField] GameObject ToolTipScrollContent = null;

    public static int ToolTipCheckLength = 200;

    public void ShowToolTip(string Msg)
    {
        if(Msg.Length > ToolTipCheckLength)
        {
            ToolTipScroll.SetActive(true);
            ToolTipText.gameObject.SetActive(false);
            ToolTipScrollText.gameObject.SetActive(true);
            ToolTipScrollText.text = Msg;
            ToolTipScrollContent.transform.localPosition = new Vector3(0.0f, ToolTipScrollContent.GetComponent<RectTransform>().rect.height * -1, 0.0f);
        }
        else
        {
            ToolTipScroll.SetActive(false);
            ToolTipText.gameObject.SetActive(true);
            ToolTipScrollText.gameObject.SetActive(false);
            ToolTipText.text = Msg;
        }
        gameObject.SetActive(true);
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
    }
}
