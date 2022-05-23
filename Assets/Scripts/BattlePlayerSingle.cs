using rso.core;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBattlePlayerSingle : CSinglePlayer
{
    private float _BoundingFixValue = 1.1f;//바닥에서 튕길 경우 중력 가속도 때문에 지속적인 튕김처리를 위해 보정값 추가.

    private CSceneBattleSingle _SceneBattleSingle = null;

    public void Init(SCharacterClientMeta Meta_, SSinglePlayer SinglePlayer_, SSingleCharacter Character_, TimePoint Now_, GameObject ParticleParent_, Camera Camera_, CSceneBattleSingle SceneBattleSingle_)
    {
        _SceneBattleSingle = SceneBattleSingle_;
        base.Init(Meta_, SinglePlayer_, Character_, Now_, ParticleParent_, Camera_, true);

        _Rigidbody.sharedMaterial = new PhysicsMaterial2D { friction = 0.0f, bounciness = 1.0f };
        _BlowBalloonControl = new CSingleBlowBalloonControl(Meta_.PumpSpeed, null);
        InitBlowBalloonControl();

        _Face(1);
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

        if (!_Character.GetStaminaItem())
            Character.Stamina = Stamina - 1.0f;
        else
            Character.Stamina = Meta.StaminaMax;

        _LastStaminaUpdateTime = Now;

        _Flap();

        if (!base.Flap())
            return;

        CGlobal.Sound.PlayOneShot((Int32)ESound.Flap);
    }
    private void OnCollisionEnter2D(Collision2D Collision_)
    {
        if (Collision_.collider.CompareTag(CGlobal.c_TagGround))
        {
            _Bounce(Collision_);
        }
        else if (Collision_.collider.CompareTag(CGlobal.c_TagTrap))
        {
            if (_SceneBattleSingle.IsGod) return;
            if (!_Character.GetShieldItem())
            {
                if (Collision_.otherCollider.CompareTag(CGlobal.c_TagBalloon))
                {
                    Character.BalloonCount -= 1;
                    sbyte Dir = (sbyte)(Collision_.transform.position.x < Collision_.otherCollider.transform.position.x ? 1 : -1);
                    _SetHitBalloon((sbyte)(BalloonCount), Dir);
                    if (BalloonCount <= 0)
                    {
                        GameOver();
                    }
                }
                else if (Collision_.otherCollider.CompareTag(CGlobal.c_TagPlayer))
                {
                    Character.BalloonCount = 0;
                    _SetHitBalloon(BalloonCount, 0);

                    GameOver();
                }
            }
            else
            {
                //Shield Effect
                var Prefab = Resources.Load("FX/00_FXPrefab/FX_Shield");
                Debug.Assert(Prefab != null);
                var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
                Obj.name = "ShieldEffect";
                Obj.transform.SetParent(_ParticleParent.transform);
                Obj.transform.position = new Vector3(Collision_.gameObject.transform.position.x, Collision_.gameObject.transform.position.y, 0.0f);
                Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                CGlobal.Sound.PlayOneShot((Int32)ESound.browken_A2);
            }
            Collision_.collider.gameObject.SetActive(false);
        }
        else if (Collision_.collider.CompareTag(CGlobal.c_TagCoin))
        {
            _SceneBattleSingle.GetCoin();
            Collision_.collider.gameObject.SetActive(false);
        }
    }
    public void SetStaminaItem(float ItemRetantionMax_)
    {
        _Character.SetStaminaItem(ItemRetantionMax_);
    }
    public bool GetStaminaItem()
    {
        return _Character.GetStaminaItem();
    }
    public void SetShieldItem(float ItemRetantionMax_)
    {
        _Character.SetShieldItem(ItemRetantionMax_);
    }
    public bool GetShieldItem()
    {
        return _Character.GetShieldItem();
    }

    private void OnTriggerEnter2D(Collider2D Collision_)
    {
        var ObjScript = Collision_.gameObject.GetComponentInParent<SinglePlayObject>();
        var Pos = new Vector3(Collision_.gameObject.transform.position.x, Collision_.gameObject.transform.position.y, 0.0f);
        if(ObjScript != null)
        {
            ObjScript.DisableObject();

            if (Collision_.CompareTag(CGlobal.c_TagCoin))
            {
                _SceneBattleSingle.GetCoin();

                //파티클 오브젝트는 알아서 파괴됨.
                var Prefab = Resources.Load("FX/00_FXPrefab/FX_Coin");
                Debug.Assert(Prefab != null);
                var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
                Obj.name = "CoinEffect";
                Obj.transform.SetParent(_ParticleParent.transform);
                Obj.transform.position = Pos;
                Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                CGlobal.Sound.PlayOneShot((Int32)ESound.single_Coin);
            }
            else if (Collision_.CompareTag(CGlobal.c_TagShield))
            {
                SetShieldItem(CGlobal.MetaData.SingleBalanceMeta.ShieldTime);

                //파티클 오브젝트는 알아서 파괴됨.
                var Prefab = Resources.Load("FX/00_FXPrefab/FX_Coin");
                Debug.Assert(Prefab != null);
                var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
                Obj.name = "ShieldEffect";
                Obj.transform.SetParent(_ParticleParent.transform);
                Obj.transform.position = Pos;
                Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                CGlobal.Sound.PlayOneShot((Int32)ESound.item_Pickup);
            }
            else if (Collision_.CompareTag(CGlobal.c_TagItem))
            {
                SetStaminaItem(CGlobal.MetaData.SingleBalanceMeta.StaminaTime);

                //파티클 오브젝트는 알아서 파괴됨.
                var Prefab = Resources.Load("FX/00_FXPrefab/FX_Coin");
                Debug.Assert(Prefab != null);
                var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
                Obj.name = "StaminaEffect";
                Obj.transform.SetParent(_ParticleParent.transform);
                Obj.transform.position = Pos;
                Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                CGlobal.Sound.PlayOneShot((Int32)ESound.item_Pickup);
            }
            else if (Collision_.CompareTag(CGlobal.c_TagGoldBar))
            {
                _SceneBattleSingle.GetGoldBar();

                //파티클 오브젝트는 알아서 파괴됨.
                var Prefab = Resources.Load("FX/00_FXPrefab/FX_GoldBar");
                Debug.Assert(Prefab != null);
                var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
                Obj.name = "CoinEffect";
                Obj.transform.SetParent(_ParticleParent.transform);
                Obj.transform.position = Pos;
                Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                CGlobal.Sound.PlayOneShot((Int32)ESound.single_Coin);
                var Text = Obj.GetComponentsInChildren<TextMesh>();
                foreach (var i in Text)
                    i.text = "+" + CGlobal.MetaData.SingleBalanceMeta.GoldBarCount.ToString();
            }
        }
    }
    void _Bounce(Collision2D Collision_) // 내가 충돌체에 가한 힘의 방향, 동쪽부터 시계방향으로 1, 2, 3, 4 (0은 방향없음)
    {
        if (Collision_.contacts[0].normal.x != 0.0f)
        {
            Collision_.otherRigidbody.velocity = new Vector2(Collision_.relativeVelocity.x, Collision_.otherRigidbody.velocity.y);
        }
        else if (Collision_.contacts[0].normal.y > 0.0f) //바닥에서 튕길 경우 중력 가속도 때문에 지속적인 튕김처리를 위해 보정값 추가.
        {
            Collision_.otherRigidbody.velocity = new Vector2(Collision_.otherRigidbody.velocity.x, Collision_.relativeVelocity.y* _BoundingFixValue);
        }
        else if (Collision_.contacts[0].normal.y <= 0.0f)
        {
            Collision_.otherRigidbody.velocity = new Vector2(Collision_.otherRigidbody.velocity.x, Collision_.relativeVelocity.y);
        }

        CGlobal.Sound.PlayOneShot((Int32)ESound.Bounce);
    }
    private void GameOver()
    {
        _Die();
        _SceneBattleSingle.GameOver();
    }
}
