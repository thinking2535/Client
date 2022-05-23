using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPercentPanel : MonoBehaviour
{
    [SerializeField] Image _Icon = null;
    [SerializeField] Text _Grade = null;
    [SerializeField] Text _Name = null;
    [SerializeField] Text _Percent = null;
    [SerializeField] GameObject _Event = null;

    public void Init(Int32 CharCode_, double Percent_, bool IsEvent )
    {
        _Icon.sprite = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + CGlobal.MetaData.GetCharacterIconName(CharCode_));
        _Name.text = CGlobal.MetaData.GetCharacterName(CharCode_);
        _Grade.text = CGlobal.MetaData.GetCharacterGrade(CharCode_);
        _Percent.text = string.Format("{0:N5}%", Percent_);

        _Name.color = CGlobal.MetaData.GetCharacterGradeColor(CharCode_);
        _Grade.color = CGlobal.MetaData.GetCharacterGradeColor(CharCode_);
        _Percent.color = CGlobal.MetaData.GetCharacterGradeColor(CharCode_);

        _Event.SetActive(IsEvent);
    }
}
