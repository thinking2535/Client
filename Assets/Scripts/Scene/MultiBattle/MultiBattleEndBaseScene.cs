using bb;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class MultiBattleEndBaseScene : NoMoneyUIScene
{
    [SerializeField] protected MultiBattleEndStage _multiBattleEndWinStagePrefab;
    [SerializeField] protected MultiBattleEndStage _multiBattleEndLoseStagePrefab;
    [SerializeField] Button _BackButton;

    protected SMultiBattleBeginNetSc _MultiBattleBeginNetSc;
    SMultiBattleEndNet _MultiBattleEndNet;
    protected SBattleType _BattleType;
    protected List<CMultiBattlePlayer> _MultiBattlePlayers;
    protected CBattlePlayer _MyBattlePlayer;

    public void init(SMultiBattleEndNet MultiBattleEndNet_, SMultiBattleBeginNetSc MultiBattleBeginNetSc_, SBattleType BattleType_, List<CMultiBattlePlayer> MultiBattlePlayers_, CBattlePlayer MyBattlePlayer_)
    {
        base.init();

        _MultiBattleEndNet = MultiBattleEndNet_;
        _MultiBattleBeginNetSc = MultiBattleBeginNetSc_;
        _MultiBattlePlayers = MultiBattlePlayers_;
        _MyBattlePlayer = MyBattlePlayer_;
        _BattleType = BattleType_;

        _BackButton.onClick.AddListener(Close);
    }
    protected override void OnDestroy()
    {
        _BackButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    protected override async Task<bool> _backButtonPressed()
    {
        if (await base._backButtonPressed())
            return true;

        SceneMoveLobby();

        return true;
    }
    public void Close()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Cancel);
        SceneMoveLobby();
    }
    public void SceneMoveLobby()
    {
        CGlobal.MusicStop();
        CGlobal.setLobbyScene();
    }
    protected MultiBattleEndStage _instantiateWinStage(EText resultTextName)
    {
        var stage = UnityEngine.Object.Instantiate(_multiBattleEndWinStagePrefab, Vector3.zero, Quaternion.identity, transform);
        stage.init(camera, resultTextName);
        return stage;
    }
    protected MultiBattleEndStage _instantiateLoseStage(EText resultTextName)
    {
        var stage = UnityEngine.Object.Instantiate(_multiBattleEndLoseStagePrefab, Vector3.zero, Quaternion.identity, transform);
        stage.init(camera, resultTextName);
        return stage;
    }
}
