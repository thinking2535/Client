using bb;
using rso.physics;
using rso.unity;
using System.Collections.Generic;

public class CPlayerCollider
{
    public CRectCollider2D Body { get; private set; }
    public CRectCollider2D Balloon { get; private set; }
    public CRectCollider2D Parachute { get; private set; }
    public List<CCollider2D> Colliders{ get; private set; } = new List<CCollider2D>();
    public CPlayerCollider(SCharacter Character_)
    {
        Body = new CRectCollider2D(
            CPhysics.ZeroTransform,
            CEngineGlobal.c_BodyNumber,
            CEngineGlobal.GetPlayerRect());
        Colliders.Add(Body);

        Balloon = new CRectCollider2D(
            new STransform(
                new SPoint(), new SPoint3(), new SPoint(global.c_BalloonLocalScale, global.c_BalloonLocalScale)),
            CEngineGlobal.c_BalloonNumber,
            CEngineGlobal.GetBalloonRect(Character_.BalloonCount));
        Colliders.Add(Balloon);

        Parachute = new CRectCollider2D(
            new STransform(new SPoint(), new SPoint3(), new SPoint(Character_.ParachuteInfo.Scale, Character_.ParachuteInfo.Scale)),
            CEngineGlobal.c_ParachuteNumber,
            CEngineGlobal.GetParachuteRect());
        Colliders.Add(Parachute);
    }
}