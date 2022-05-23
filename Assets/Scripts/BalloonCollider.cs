using bb;
using UnityEngine;
using rso.physics;
using System;

public class BalloonCollider : MonoBehaviour
{
    static readonly float BalloonUnitSizeWidth = 0.2f;
    static readonly float BalloonUnitSizeHeight = 0.27f;
    BoxCollider2D _Collider = null;
    public void Init(sbyte BalloonCount_)
    {
        _Collider = gameObject.AddComponent<BoxCollider2D>();
        _Collider.offset = new Vector2(0, 0.5f);
        _Collider.size = new Vector2(BalloonUnitSizeWidth * global.c_BalloonCountForRegen, BalloonUnitSizeHeight);

        SetCount(BalloonCount_);
    }
    public void SetCount(sbyte Count_)
    {
        // 충돌박스 변경에 따라 CollisionEnter, CollisionExit 호출되지 않도록
        if (Count_ > 0)
        {
            if (!_Collider.enabled)
                _Collider.enabled = true;

            _Collider.size = new Vector2(BalloonUnitSizeWidth * Count_, BalloonUnitSizeHeight);
        }
        else
        {
            if (_Collider.enabled)
                _Collider.enabled = false;
        }
    }
    public float GetHalfWidth()
    {
        return transform.localScale.x * BalloonUnitSizeWidth *0.5f;
    }
}
