using bb;
using System;
using System.Collections.Generic;

public class MultiBattleEndScene : MultiBattleEndBaseScene
{
    SMultiBattleEndNetSc _MultiBattleEndNetSc;

    public async void init(
        SMultiBattleEndNetSc MultiBattleEndNetSc_,
        SMultiBattleBeginNetSc BattleProto_,
        SBattleType BattleType_,
        List<CMultiBattlePlayer> MultiBattlePlayers_,
        CBattlePlayer MyBattlePlayer_,
        Int32 addedPoint,
        List<CUnitReward> unitRewards)
    {
        base.init(MultiBattleEndNetSc_, BattleProto_, BattleType_, MultiBattlePlayers_, MyBattlePlayer_);

        _MultiBattleEndNetSc = MultiBattleEndNetSc_;
        bool DoesMyTeamWin = _MultiBattleEndNetSc.myTeamRanking == 0;
        var currentStage = DoesMyTeamWin ? _instantiateWinStage(EText.ResultScene_Text_Win) : _instantiateLoseStage(EText.ResultScene_Text_Lose);
        var MyTeamPlayers = new SortedDictionary<Int32, CMultiBattlePlayer>(new CDescendingComparer<Int32>());

        for (Int32 i = 0; i < _MultiBattlePlayers.Count; ++i)
        {
            var Player = _MultiBattlePlayers[i];
            if (Player.TeamIndex != _MyBattlePlayer.TeamIndex)
                continue;

            MyTeamPlayers.Add(MultiBattlePlayers_[i].BattleInfo.Point, Player);
        }

        Int32 PlayerIndexInMyTeam = 0;
        foreach (var p in MyTeamPlayers)
        {
            if (PlayerIndexInMyTeam >= currentStage.getPlayersLength()) // 프리팹에는 한팀에 최대 3명이므로 그 이상의 팀원은 보여줄 수 없음.
                break;

            currentStage.makeResultPlayerCharacter(PlayerIndexInMyTeam, p.Value, addedPoint, DoesMyTeamWin);
            ++PlayerIndexInMyTeam;
        }

        if (DoesMyTeamWin)
            CGlobal.Sound.PlayOneShot((Int32)ESound.Win);
        else
            CGlobal.Sound.PlayOneShot((Int32)ESound.Lose);

        if (unitRewards.Count > 0)
            await pushRewardPopup(EText.Global_Button_Receive, unitRewards);
    }
}
