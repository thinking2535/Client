using bb;
using System;
using System.Collections.Generic;

public class MultiBattleEndDrawScene : MultiBattleEndBaseScene
{
    SMultiBattleEndDrawNetSc _MultiBattleEndDrawNetSc;
    public void init(SMultiBattleEndDrawNetSc MultiBattleEndDrawNetSc_, SMultiBattleBeginNetSc BattleProto_, SBattleType BattleType_, List<CMultiBattlePlayer> MultiBattlePlayers_, CBattlePlayer MyBattlePlayer_)
    {
        base.init(MultiBattleEndDrawNetSc_, BattleProto_, BattleType_, MultiBattlePlayers_, MyBattlePlayer_);
        _MultiBattleEndDrawNetSc = MultiBattleEndDrawNetSc_;

        var currentStage = _instantiateLoseStage(EText.ResultScene_Text_Draw);

        // rso todo �Ʒ� �ڵ� �����Ұ�.
        var MyTeamPlayers = new List<CMultiBattlePlayer>();

        for (Int32 i = 0; i < _MultiBattlePlayers.Count; ++i)
        {
            var Player = _MultiBattlePlayers[i];
            if (Player.TeamIndex != _MyBattlePlayer.TeamIndex)
                continue;

            MyTeamPlayers.Add(Player);
        }

        Int32 PlayerIndexInMyTeam = 0;
        foreach (var p in MyTeamPlayers)
        {
            if (PlayerIndexInMyTeam >= currentStage.getPlayersLength()) // �����տ��� ������ �ִ� 3���̹Ƿ� �� �̻��� ������ ������ �� ����.
                break;

            currentStage.makeResultPlayerCharacter(PlayerIndexInMyTeam, p, 0, false);
            ++PlayerIndexInMyTeam;
        }

        CGlobal.Sound.PlayOneShot((Int32)ESound.Lose);
    }
}
