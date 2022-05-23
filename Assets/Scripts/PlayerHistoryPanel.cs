using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHistoryPanel : MonoBehaviour
{
    [SerializeField] Text _InformationText = null;
    public void Init(string InforText_)
    {
        _InformationText.text = InforText_;
    }
}
