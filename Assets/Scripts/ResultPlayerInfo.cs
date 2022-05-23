using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPlayerInfo : MonoBehaviour
{
    [SerializeField] GameObject _MyBG = null;
    [SerializeField] Text _UserName = null;

    public void Init(string UserName_, bool IsMe_)
    {
        _UserName.text = UserName_;
        _MyBG.SetActive(IsMe_);
        gameObject.SetActive(true);
    }
}
