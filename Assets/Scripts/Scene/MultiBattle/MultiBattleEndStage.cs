using bb;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MultiBattleEndStage : MonoBehaviour
{
    [SerializeField] MultiBattleEndPlayer[] Players;
    [SerializeField] Text _resultText;
    [SerializeField] Text _rankText;

    public void init(Camera camera, EText resultTextName)
    {
        var canvas = GetComponent<Canvas>();

        canvas.worldCamera = camera;
//        canvas.sortingLayerName = _sortingLayerName;
        canvas.planeDistance = 90;
        canvas.sortingOrder = 0;

        _resultText.text = CGlobal.MetaData.getText(resultTextName);
    }
    public void makeResultPlayerCharacter(Int32 playerIndex, CBattlePlayer Player_, Int32 addedPoint, bool IsWin_)
    {
        var player = Players[playerIndex];

        player.gameObject.SetActive(true);

        Int32 CharCode = Player_.Meta.Code;

        string PrefabPath = string.Format("Prefabs/Char/{0}", Player_.Meta.PrefabName);
        var Prefab = Resources.Load(PrefabPath);
        Debug.Assert(Prefab != null);

        var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "Model_Ch";
        Obj.transform.SetParent(player.CharacterBody.transform);
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localScale = new Vector3(1, 1, 1);
        Obj.transform.localEulerAngles = new Vector3(0, 0, 0);
        var CharacterModel = Obj.GetComponent<Model_Ch>();

        player.NickName.text = Player_.BattlePlayer.Nick;

        if (Player_.BattlePlayer.UID == CGlobal.UID)
        {
            player.NickName.color = Color.green;
            _rankText.text = addedPoint.ToString("+#;-#;0");
        }

        if (IsWin_)
            CharacterModel.Win();
        else
            CharacterModel.Lose();
    }
    public Int32 getPlayersLength()
    {
        return Players.Length;
    }
}
