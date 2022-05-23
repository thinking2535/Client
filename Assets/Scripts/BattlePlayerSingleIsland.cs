using rso.core;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBattlePlayerSingleIsland : CSinglePlayer
{
    float _Stamina = 0.0f;
    float _StaminaMax = 0.0f;
    bool _IsGameOver = false;

    private CSceneBattleSingleIsland _SceneBattleSingle = null;
    public void Init(SCharacterClientMeta Meta_, SSinglePlayer SinglePlayer_, SSingleCharacter Character_, TimePoint Now_, GameObject ParticleParent_, Camera Camera_, CSceneBattleSingleIsland SceneBattleSingle_)
    {
        _SceneBattleSingle = SceneBattleSingle_;
        base.Init(Meta_, SinglePlayer_, Character_, Now_, ParticleParent_, Camera_, false);

        _Rigidbody.sharedMaterial = new PhysicsMaterial2D { friction = 0.0f, bounciness = 1.0f };
        _BlowBalloonControl = new CSingleBlowBalloonControl(Meta_.PumpSpeed, null);
        InitBlowBalloonControl();

        _Stamina = Meta.StaminaMax * CGlobal.MetaData.SingleIslandBalanceMeta.StaminaBonus;
        _StaminaMax = CGlobal.MetaData.SingleIslandBalanceMeta.StaminaMax;
        if (_Stamina > _StaminaMax)
            _Stamina = _StaminaMax;
        _SceneBattleSingle.SetStaminaBar(0, _StaminaMax);

        _Face(1);
        _IsGameOver = false;
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
    public new void Flap()
    {
        if (Character.BalloonCount <= 0)
            return;

        if (_Stamina < 1.0f)
            return;

        if(!_SceneBattleSingle.IsGod)
            _Stamina -= 1.0f;

        _SceneBattleSingle.SetStaminaBar(_Stamina, _StaminaMax);

        _Flap();

        if (!base.Flap())
            return;

        CGlobal.Sound.PlayOneShot((Int32)ESound.Flap);
    }
    private void OnCollisionEnter2D(Collision2D Collision_)
    {
        if (_IsGameOver == false)
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
            else if (Collision_.collider.CompareTag(CGlobal.c_TagTrap))
            {
                GameOver();
                Collision_.collider.gameObject.SetActive(false);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D Collision_)
    {
        if (_IsGameOver == false)
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
    }
    private void OnCollisionExit2D(Collision2D Collision_)
    {
        if(_IsGameOver == false)
        {
            if (Collision_.otherCollider.CompareTag(CGlobal.c_TagPlayer) && // 내 몸과 부딪혔으면
                Collision_.collider.CompareTag(CGlobal.c_TagGround) && IsGround)
            {
                DetachGround(Collision_);
            }
        }
    }
    private void AttachGround(Collision2D Collision_)
    {
        Character.Land(Meta);
        _Land();
        var ObjScript = Collision_.gameObject.GetComponentInParent<SingleIslandObject>();
        if (ObjScript.GetIsSpike() && !_SceneBattleSingle.IsGod)
        {
            GameOver();
            Collision_.collider.gameObject.SetActive(false);
        }
        else
        {
            _SceneBattleSingle.SetIsland(ObjScript.GetIslandCount());
            
            _Stamina += ObjScript.GetStaminaRecovery();
            if (_Stamina > _StaminaMax)
                _Stamina = _StaminaMax;
            _SceneBattleSingle.SetStaminaBar(_Stamina, _StaminaMax);
            ObjScript.SetIsLanding(true);
        }
    }
    private void DetachGround(Collision2D Collision_)
    {
        Character.Fly();// = false;
        _Fly();
        var ObjScript = Collision_.gameObject.GetComponentInParent<SingleIslandObject>();
        if (ObjScript != null)
            ObjScript.SetIsLanding(false);
    }

    private void OnTriggerEnter2D(Collider2D Collision_)
    {
        if (Collision_.CompareTag(CGlobal.c_TagCoin))
        {
            Collision_.gameObject.SetActive(false);

            //파티클 오브젝트는 알아서 파괴됨.
            var ObjScript = Collision_.gameObject.GetComponentInParent<SingleIslandObject>();
            if (ObjScript.GetItemType() == SingleIslandObject.EItemType.Coin)
            {
                _SceneBattleSingle.GetCoin();
                CGlobal.Sound.PlayOneShot((Int32)ESound.single_Coin);
                var Prefab = Resources.Load("FX/00_FXPrefab/FX_Coin");
                Debug.Assert(Prefab != null);
                var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
                Obj.name = "CoinEffect";
                Obj.transform.SetParent(_ParticleParent.transform);
                Obj.transform.position = new Vector3(Collision_.gameObject.transform.position.x, Collision_.gameObject.transform.position.y, 0.0f);
                Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
            if (ObjScript.GetItemType() == SingleIslandObject.EItemType.GoldBar)
            {
                _SceneBattleSingle.GetGoldBar();
                CGlobal.Sound.PlayOneShot((Int32)ESound.single_Coin);
                var Prefab = Resources.Load("FX/00_FXPrefab/FX_GoldBar");
                Debug.Assert(Prefab != null);
                var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
                Obj.name = "CoinEffect";
                Obj.transform.SetParent(_ParticleParent.transform);
                Obj.transform.position = new Vector3(Collision_.gameObject.transform.position.x, Collision_.gameObject.transform.position.y, 0.0f);
                Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                var Text = Obj.GetComponentsInChildren<TextMesh>();
                foreach(var i in Text)
                    i.text = "+"+CGlobal.MetaData.SingleIslandBalanceMeta.GoldBarCount.ToString();
            }
        }
        else if (Collision_.CompareTag(CGlobal.c_TagItem))
        {
            _SceneBattleSingle.GetItem();
            Collision_.gameObject.SetActive(false);

            //파티클 오브젝트는 알아서 파괴됨.
            var Prefab = Resources.Load("FX/00_FXPrefab/FX_Dia");
            Debug.Assert(Prefab != null);
            var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
            Obj.name = "DiaEffect";
            Obj.transform.SetParent(_ParticleParent.transform);
            Obj.transform.position = new Vector3(Collision_.gameObject.transform.position.x, Collision_.gameObject.transform.position.y, 0.0f);
            Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

            var ObjScript = Collision_.gameObject.GetComponentInParent<SingleIslandObject>();
            if (ObjScript.GetItemType() == SingleIslandObject.EItemType.Item_Apple)
            {
                CGlobal.Sound.PlayOneShot((Int32)ESound.getitem_3);
                AddStamina(CGlobal.MetaData.SingleIslandBalanceMeta.StaminaApple);
            }
            if (ObjScript.GetItemType() == SingleIslandObject.EItemType.Item_Meat)
            {
                CGlobal.Sound.PlayOneShot((Int32)ESound.getitem_2);
                AddStamina(CGlobal.MetaData.SingleIslandBalanceMeta.StaminaMeat);
            }
            if (ObjScript.GetItemType() == SingleIslandObject.EItemType.Item_Chicken)
            {
                CGlobal.Sound.PlayOneShot((Int32)ESound.getitem_2);
                AddStamina(CGlobal.MetaData.SingleIslandBalanceMeta.StaminaChicken);
            }
        }
        else if (Collision_.CompareTag(CGlobal.c_TagGround))
        {
            if (!_SceneBattleSingle.IsGod)
            {
                GameOver();
                Collision_.gameObject.SetActive(false);
            }
        }
        else if (Collision_.CompareTag(CGlobal.c_TagWater))
        {
            if (!_SceneBattleSingle.IsGod)
            {
                GameOver();
                Collision_.gameObject.SetActive(false);
                _SceneBattleSingle.FallEffect(transform.position);
            }
        }
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
    private void GameOver()
    {
        _IsGameOver = true;
        Character.BalloonCount = 0;
        _SetHitBalloon(BalloonCount, 0);
        _Die();
        _SceneBattleSingle.GameOver();
    }
    public float GetStamina()
    {
        return _Stamina;
    }
    public void AddStamina(float Add_)
    {
        _Stamina += Add_;
        if (_Stamina > _StaminaMax)
            _Stamina = _StaminaMax;
        _SceneBattleSingle.SetStaminaBar(_Stamina, _StaminaMax);
    }
    //public void Update()
    //{
    //    base.Update();
    //    if (IsGround)
    //    {
    //        transform.position -= new Vector3(CGlobal.MetaData.SingleIslandBalanceMeta.IslandVelocity * Time.deltaTime, 0.0f, 0.0f);
    //    }
    //}
}
