using bb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankPointInfoPopup : MonoBehaviour
{
    [SerializeField] Text _TitleText = null;
    [SerializeField] GameObject _OnoOnOneObject = null;
    [SerializeField] GameObject _TeamObject = null;
    [SerializeField] GameObject _FreeForAllObject = null;
    [SerializeField] GameObject _FreeForAllSmallObject = null;
    [SerializeField] GameObject _TeamSmallObject = null;
    [SerializeField] List<GameObject> _ControlParent = null;
    public void OneOnOneView()
    {
        gameObject.SetActive(true);
        _TitleText.text = CGlobal.MetaData.getText(EText.LobbyScene_Solo);
        _OnoOnOneObject.SetActive(true);
        _TeamObject.SetActive(false);
        _FreeForAllObject.SetActive(false);
        _FreeForAllSmallObject.SetActive(false);
        _TeamSmallObject.SetActive(false);
        foreach (var i in _ControlParent)
        {
            i.SetActive(false);
        }
    }
    public void DodgeView()
    {
        gameObject.SetActive(true);
        _TitleText.text = CGlobal.MetaData.getText(EText.LobbyScene_DodgeArrowRace);
        _OnoOnOneObject.SetActive(true);
        _TeamObject.SetActive(false);
        _FreeForAllObject.SetActive(false);
        _FreeForAllSmallObject.SetActive(false);
        _TeamSmallObject.SetActive(false);
        foreach (var i in _ControlParent)
        {
            i.SetActive(false);
        }
    }
    public void IslandView()
    {
        gameObject.SetActive(true);
        _TitleText.text = CGlobal.MetaData.getText(EText.LobbyScene_FlyAwayRace);
        _OnoOnOneObject.SetActive(true);
        _TeamObject.SetActive(false);
        _FreeForAllObject.SetActive(false);
        _FreeForAllSmallObject.SetActive(false);
        _TeamSmallObject.SetActive(false);
        foreach (var i in _ControlParent)
        {
            i.SetActive(false);
        }
    }
    public void TeamView()
    {
        gameObject.SetActive(true);
        _TitleText.text = CGlobal.MetaData.getText(EText.LobbyScene_Team);
        _OnoOnOneObject.SetActive(false);
        _TeamObject.SetActive(true);
        _FreeForAllObject.SetActive(false);
        _FreeForAllSmallObject.SetActive(false);
        _TeamSmallObject.SetActive(false);
        foreach (var i in _ControlParent)
        {
            i.SetActive(false);
        }
    }
    public void TeamSmallView()
    {
        gameObject.SetActive(true);
        _TitleText.text = CGlobal.MetaData.getText(EText.LobbyScene_TeamSmall);
        _OnoOnOneObject.SetActive(false);
        _TeamObject.SetActive(false);
        _FreeForAllObject.SetActive(false);
        _FreeForAllSmallObject.SetActive(false);
        _TeamSmallObject.SetActive(true);
        foreach (var i in _ControlParent)
        {
            i.SetActive(false);
        }
    }
    public void FreeForAllView()
    {
        gameObject.SetActive(true);
        _TitleText.text = CGlobal.MetaData.getText(EText.LobbyScene_Survival);
        _OnoOnOneObject.SetActive(false);
        _TeamObject.SetActive(false);
        _FreeForAllObject.SetActive(true);
        _FreeForAllSmallObject.SetActive(false);
        _TeamSmallObject.SetActive(false);
        foreach (var i in _ControlParent)
        {
            i.SetActive(false);
        }
    }
    public void FreeForAllSmallView()
    {
        gameObject.SetActive(true);
        _TitleText.text = CGlobal.MetaData.getText(EText.LobbyScene_3PSurvival);
        _OnoOnOneObject.SetActive(false);
        _TeamObject.SetActive(false);
        _FreeForAllObject.SetActive(false);
        _FreeForAllSmallObject.SetActive(true);
        _TeamSmallObject.SetActive(false);
        foreach (var i in _ControlParent)
        {
            i.SetActive(false);
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
        foreach (var i in _ControlParent)
        {
            i.SetActive(true);
        }
    }
}
