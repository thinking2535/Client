using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUserCharacter : MonoBehaviour
{
    private GameObject _UserCharacter = null;
    public void MakeCharacter(Int32 CharCode_)
    {
        var character = CGlobal.MetaData.Chars[CharCode_];
        string prefabDir = string.Format("Prefabs/Char/{0}", character.PrefabName);
        var Prefab = Resources.Load(prefabDir);
        Debug.Assert(Prefab != null);

        _UserCharacter = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        _UserCharacter.name = "Model_Ch";//나중에 변경
        _UserCharacter.transform.SetParent(transform);
        _UserCharacter.transform.position = transform.position;
        _UserCharacter.transform.localScale = Vector3.one;
        _UserCharacter.transform.Rotate(new Vector3(0, -140, 0));
    }
    public void MakeCharacter(string PrefabName_)
    {
        // Model_Ch
        var Prefab = Resources.Load("Prefabs/Char/" + PrefabName_);
        Debug.Assert(Prefab != null);

        _UserCharacter = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        _UserCharacter.name = "Model_Ch";
        _UserCharacter.transform.SetParent(transform);
        _UserCharacter.transform.localPosition = Vector3.zero;
        _UserCharacter.transform.localScale = Vector3.one;
        _UserCharacter.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    public void DeleteCharacter()
    {
        if(_UserCharacter) Destroy(_UserCharacter);
    }
    public void Win()
    {
        _UserCharacter.GetComponent<Model_Ch>().Win();
    }
    public void Lose()
    {
        _UserCharacter.GetComponent<Model_Ch>().Lose();
    }
    public void Stop()
    {
        _UserCharacter.GetComponent<Model_Ch>().Stop();
    }
}
