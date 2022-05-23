using rso.core;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBattlePlayerTutorial : CSinglePlayer
{
    private CSceneBattleTutorial _SceneBattleTutorial = null;
    public void Init(SCharacterClientMeta Meta_, SSinglePlayer BattlePlayer_, SSingleCharacter Character_, TimePoint Now_, GameObject ParticleParent_, Camera Camera_, CSceneBattleTutorial SceneBattleTutorial_)
    {
        _SceneBattleTutorial = SceneBattleTutorial_;
        base.Init(Meta_, BattlePlayer_, Character_, Now_, ParticleParent_, Camera_, true);

        _Rigidbody.sharedMaterial = new PhysicsMaterial2D { friction = 0.0f, bounciness = 1.0f };
        _BlowBalloonControl = new CSingleBlowBalloonControl(Meta_.PumpSpeed, _PumpDone);
        InitBlowBalloonControl();

        _Face(1);
        Character.BalloonCount = 0;
        _SetBalloonCount(0);
    }
    public void GameStart()
    {
        _Rigidbody.isKinematic = false;
    }
    public void Center()
    {
        if (!Character.IsAlive())
            return;

        Character.Center();

        if (IsGround)
            _Stop();
    }
    public void LeftRight(sbyte Dir_)
    {
        if (!Character.IsAlive())
            return;

        Character.LeftRight(Meta, Dir_);

        if (IsGround)
            _Move(Dir_);
        else
            _Face(Dir_);
    }
    public new void Flap()
    {
        if (Character.BalloonCount <= 0)
            return;

        var Now = CGlobal.GetServerTimePoint();
        var Stamina = GetStamina(Now);

        if (Stamina < 1.0f)
            return;

        Character.Stamina = Stamina - 1.0f;
        _LastStaminaUpdateTime = Now;

        _Flap();

        if (!base.Flap())
            return;

        CGlobal.Sound.PlayOneShot((Int32)ESound.Flap);
    }
    public void Pump()
    {
        if (SinglePlayer.Character.Dir != 0)
            return;

        if (!_BlowBalloonControl.CheckAndBlow())
            return;

        _PumpCore();
    }
    void _PumpDone()
    {
        if (SinglePlayer.Character.RecvPumpDone())
        {
            _Stop();
            _BlowBalloonOff();
            _SetBalloonCount(BalloonCount);
            _Rigidbody.gravityScale = 1.0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D Collision_)
    {
        if (Collision_.collider.CompareTag(CGlobal.c_TagGround))
        {
            if (Collision_.otherCollider.CompareTag(CGlobal.c_TagPlayer) && // 내 몸과 부딪혔으면
                Collision_.contacts[0].normal.y * Collision_.contacts[0].normal.y >= Collision_.contacts[0].normal.x * Collision_.contacts[0].normal.x &&
                Collision_.contacts[0].normal.y > 0.0f) // 땅의 윗면과 부딪혔으면
            {
                AttachGround(Collision_);
            }
            else
            {
                if (!IsGround)
                    _Bounce(Collision_);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D Collision_)
    {
        if (Collision_.collider.CompareTag(CGlobal.c_TagGround) && Collision_.otherCollider.CompareTag(CGlobal.c_TagPlayer))
        {
            if (!IsGround &&
                Collision_.contacts[0].normal.y * Collision_.contacts[0].normal.y >= Collision_.contacts[0].normal.x * Collision_.contacts[0].normal.x &&
                Collision_.contacts[0].normal.y > 0.0f) // 땅의 윗면과 부딪혔으면
            {
                AttachGround(Collision_);
            }
            else if (IsGround &&
                Collision_.contacts[0].normal.y * Collision_.contacts[0].normal.y >= Collision_.contacts[0].normal.x * Collision_.contacts[0].normal.x &&
                Collision_.contacts[0].normal.y > 0.0f)
            {
                //Land Status
            }
            else
            {
                DetachGround(Collision_);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D Collision_)
    {
        if (Collision_.otherCollider.CompareTag(CGlobal.c_TagPlayer) && // 내 몸과 부딪혔으면
            Collision_.collider.CompareTag(CGlobal.c_TagGround) && IsGround)
        {
            DetachGround(Collision_);
        }
    }
    private void OnTriggerEnter2D(Collider2D Collision_)
    {
        if (Collision_.CompareTag(CGlobal.c_TagItem))
        {
            Collision_.gameObject.SetActive(false);

            //파티클 오브젝트는 알아서 파괴됨.
            var Prefab = Resources.Load("FX/00_FXPrefab/FX_Coin");
            Debug.Assert(Prefab != null);
            var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
            Obj.name = "DiaEffect";
            Obj.transform.SetParent(_ParticleParent.transform);
            Obj.transform.position = new Vector3(Collision_.gameObject.transform.position.x, Collision_.gameObject.transform.position.y, 0.0f);
            Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            CGlobal.Sound.PlayOneShot((Int32)ESound.single_Dia);

            _SceneBattleTutorial.NextTutorial();
        }
    }
    private void AttachGround(Collision2D Collision_)
    {
        Character.Land(Meta);
        _Land();
    }
    private void DetachGround(Collision2D Collision_)
    {
        Character.Fly();// = false;
        _Fly();
    }
    void _Bounce(Collision2D Collision_) // 내가 충돌체에 가한 힘의 방향, 동쪽부터 시계방향으로 1, 2, 3, 4 (0은 방향없음)
    {
        if (Collision_.contacts[0].normal.x != 0.0f)
        {
            Collision_.otherRigidbody.velocity = new Vector2(Collision_.relativeVelocity.x, Collision_.otherRigidbody.velocity.y);
        }
        else if (Collision_.contacts[0].normal.y != 0.0f)
        {
            Collision_.otherRigidbody.velocity = new Vector2(Collision_.otherRigidbody.velocity.x, Collision_.relativeVelocity.y);
        }

        CGlobal.Sound.PlayOneShot((Int32)ESound.Bounce);
    }
}
