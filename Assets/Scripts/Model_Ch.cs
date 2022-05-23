using rso.physics;
using System;
using UnityEngine;

public class Model_Ch : MonoBehaviour
{
    enum _EState
    {
        Ready,
        Move,
        Fly,
        Pump,
        Parachute,
        Landing,
        Win,
        Lose,
        Fall
    }
    public CBattlePlayer BattlePlayer { get; private set; } = null;
    Animator _Animator = null;

    Vector2 _SingleCollBoxOffset = new Vector2(0.0f, 0.057f);
    Vector2 _SingleCollBoxSize = new Vector2(0.100697f, 0.1202696f);
    public void Awake()
    {
        _Animator = GetComponentInChildren<Animator>();
    }
    public void Init(CBattlePlayer BattlePlayer_, float MoveSpeed_, float PumpSpeed_)
    {
        BattlePlayer = BattlePlayer_;
        Init(MoveSpeed_, PumpSpeed_);
    }
    public void Init(float MoveSpeed_, float PumpSpeed_)
    {
        _Animator.SetFloat("MoveSpeed", MoveSpeed_);
        _Animator.SetFloat("PumpSpeed", PumpSpeed_);
    }
    public void Stop()
    {
        _Animator.SetInteger("State", (Int32)_EState.Ready);
    }
    public void Move()
    {
        _Animator.SetInteger("State", (Int32)_EState.Move);
    }
    public void Flap()
    {
        _Animator.SetInteger("State", (Int32)_EState.Fly);
        _Animator.SetTrigger("Flap");
    }
    public void Fly()
    {
        _Animator.SetInteger("State", (Int32)_EState.Fly);
    }
    public void Pump()
    {
        _Animator.SetInteger("State", (Int32)_EState.Pump);
        _Animator.SetTrigger("Blow");
    }
    public void Die()
    {
        _Animator.SetInteger("State", (Int32)_EState.Fall);
    }
    public void Regen()
    {
        Stop();
    }
    public void Win()
    {
        _Animator.SetInteger("State", (Int32)_EState.Win);
    }
    public void Lose()
    {
        _Animator.SetInteger("State", (Int32)_EState.Lose);
    }
}
