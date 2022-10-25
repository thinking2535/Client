using bb;
using UnityEngine;
using UnityEngine.UI;

public class MultiBattleEndWinStage : MultiBattleEndStage
{
    [SerializeField] Text _bestPlayerText;

    private void Awake()
    {
        _bestPlayerText.text = CGlobal.MetaData.getText(EText.SceneBattleEnd_BestPlayer);
    }
}
