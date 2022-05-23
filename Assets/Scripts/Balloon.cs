using bb;
using UnityEngine;
using rso.physics;
using System;

public class Balloon : MonoBehaviour
{
    static readonly float BalloonUnitSizeWidth = 0.2f;
    [SerializeField] BalloonSub[] _BalloonSubs = new BalloonSub[global.c_BalloonCountForRegen];
    public CBattlePlayer BattlePlayer { get; private set; } = null;
    public void Init(CEngineGameMode GameMode_, CBattlePlayer BattlePlayer_, SByte MyTeamIdx_)
    {
        BattlePlayer = BattlePlayer_;

        foreach (var i in _BalloonSubs)
            i.Init(GameMode_, BattlePlayer_.BattlePlayer.TeamIndex, MyTeamIdx_);

        SetCount(BattlePlayer_.BalloonCount);
    }
    public void SingleInit(sbyte BalloonCount_)
    {
        foreach (var i in _BalloonSubs)
            i.SingleInit();
        SetCount(BalloonCount_);
    }
    public void SetCount(sbyte Count_)
    {
        for (Int32 i = 0; i < _BalloonSubs.Length; ++i)
        {
            if (i == Count_ - 1)
                _BalloonSubs[i].gameObject.SetActive(true);
            else
                _BalloonSubs[i].gameObject.SetActive(false);
        }
    }
    public float GetHalfWidth()
    {
        return transform.localScale.x * BalloonUnitSizeWidth * 0.5f;
    }
}
