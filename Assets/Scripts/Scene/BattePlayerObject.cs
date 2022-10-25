using rso.physics;
using System.Collections.Generic;

public class CBattlePlayerObject : CPlayerObject2D
{
    public CBattlePlayer BattlePlayer;
    public CBattlePlayerObject(STransform Transform_, List<CCollider2D> Colliders_, SPoint Velocity_, CBattlePlayer BattlePlayer_) :
        base(Transform_, Colliders_, Velocity_)
    {
        BattlePlayer = BattlePlayer_;
        Mass = BattlePlayer.Meta.typeMeta.Weight;
    }
}