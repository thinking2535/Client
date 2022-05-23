using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour
{
    public GameObject[] ToolTipButtons = null;
    [SerializeField] Button _ToolTipOnBtn = null;
    [SerializeField] Button _ToolTipOffBtn = null;
    public void Awake()
    {
        ToolTipOff();
    }
    public void ToolTipOn()
    {
        foreach(var i in ToolTipButtons)
        {
            i.SetActive(true);
        }
        _ToolTipOnBtn.gameObject.SetActive(false);
        _ToolTipOffBtn.gameObject.SetActive(true);
    }
    public void ToolTipOff()
    {
        foreach (var i in ToolTipButtons)
        {
            i.SetActive(false);
        }
        _ToolTipOnBtn.gameObject.SetActive(true);
        _ToolTipOffBtn.gameObject.SetActive(false);
    }
}
