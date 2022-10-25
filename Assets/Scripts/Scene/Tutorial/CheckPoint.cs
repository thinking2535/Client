using rso.physics;
using rso.unity;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    SPoint _localPosition;

    public void init(SPoint localPosition)
    {
        _localPosition = localPosition;
    }
    public void Update()
    {
        _updateLocalPosition();
    }
    void _updateLocalPosition()
    {
        transform.localPosition = _localPosition.ToVector2();
    }
    public void setLocalPosition(Vector2 localPosition)
    {
        _localPosition.X = localPosition.x;
        _localPosition.Y = localPosition.y;

        _updateLocalPosition();
    }
}
