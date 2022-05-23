using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 MoveStartPos = Vector3.zero;
    public Vector3 MoveEndPos = Vector3.zero;
    public float Velocity = 0.1f;
    public float Delay = 0.0f;
    public Transform MoveTransform = null;
    bool _IsMoveFlip = false;
    bool _IsMoveDelay = false;
    float _DelayCount = 0.0f;
    public bool IsMove = true;

    private void Start()
    {
        MoveTransform.localPosition = MoveStartPos;
        _IsMoveFlip = false;
        _DelayCount = 0.0f;
    }
    public void StartMove()
    {
        MoveTransform.localPosition = MoveStartPos;
        _IsMoveFlip = false;
        _DelayCount = 0.0f;
        IsMove = true;
    }
    void FixedUpdate()
    {
        if(IsMove)
        {
            if (!_IsMoveDelay)
            {
                if (!_IsMoveFlip)
                {
                    MoveTransform.localPosition = Vector3.MoveTowards(MoveTransform.localPosition, MoveEndPos, Velocity * Time.deltaTime);
                    if (MoveTransform.localPosition == MoveEndPos)
                    {
                        MoveTransform.localPosition = MoveEndPos;
                        _IsMoveFlip = true;
                        if (Delay > 0.0f)
                        {
                            _IsMoveDelay = true;
                            _DelayCount = 0.0f;
                        }
                    }
                }
                else
                {
                    MoveTransform.localPosition = Vector3.MoveTowards(MoveTransform.localPosition, MoveStartPos, Velocity * Time.deltaTime);
                    if (MoveTransform.localPosition == MoveStartPos)
                    {
                        MoveTransform.localPosition = MoveStartPos;
                        _IsMoveFlip = false;
                        if (Delay > 0.0f)
                        {
                            _IsMoveDelay = true;
                            _DelayCount = 0.0f;
                        }
                    }
                }
            }
            else
            {
                _DelayCount += Time.deltaTime;
                if (_DelayCount > Delay)
                {
                    _IsMoveDelay = false;
                    _DelayCount = 0.0f;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D Collision_)
    {
        if (Collision_.collider.CompareTag(CGlobal.c_TagPlayer))
        {
            Debug.Log("Ground!!!! Tag!!!");
        }
    }
}
