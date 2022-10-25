using bb;
using rso.core;
using rso.physics;
using System;
using UnityEngine;

public class TutorialPlayer : CBattlePlayer
{
    public delegate void FGetItem();

    FGetItem _fGetItem;

    public void init(
        SPoint InitialPos_,
        SCharacterMeta Meta_,
        SCharacterNet Character_,
        TeamMaterial teamMaterial,
        SBattlePlayer BattlePlayer_,
        Transform Prop_,
        Camera Camera_,
        FGetItem fGetItem_)
    {
        base.Init(
            0,
            InitialPos_,
            Meta_,
            Character_,
            teamMaterial,
            0,
            BattlePlayer_,
            this,
            true,
            Prop_,
            Camera_);

        _fGetItem = fGetItem_;
        PlayerObject.fTriggerEnter = _TriggerEnter;
    }
    protected override bool _TriggerEnter(CCollider2D Collider_)
    {
        if (Collider_.Number == CEngineGlobal.c_ItemNumber)
            _fGetItem();

        return false;
    }
}
