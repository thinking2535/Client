using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListScene : MonoBehaviour
{
    public GameObject MoneyUI = null;
    public GameObject ActiveCharListParent = null;
    public GameObject DeactiveCharListParent = null;
    public CharacterInfoPopup CharacterInfoPopup = null;
    public CharacterPanel CharacterPanelOrigin = null;
    public GameObject UserCharacter = null;
    public Camera MainCamera = null;
    public GameObject CharacterListContent = null;
    public Text TipText = null;
    public void Back()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        CGlobal.SceneSetNext(new CSceneLobby());
    }
}
