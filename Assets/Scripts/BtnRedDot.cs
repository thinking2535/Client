using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnRedDot : MonoBehaviour
{
    [SerializeField] GameObject RedDot = null;

    public void SetRedDot(bool enable_)
    {
        RedDot.SetActive(enable_);
    }
}
