using bb;
using rso.physics;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SCharacterMeta
{
    public readonly CharacterClientMeta meta;
    public readonly CharacterTypeClientMeta typeMeta;

    public SCharacterMeta(CharacterClientMeta meta, CharacterTypeClientMeta typeMeta)
    {
        this.meta = meta;
        this.typeMeta = typeMeta;
    }
    public Int32 Code => meta.Code;
    public EGrade grade => typeMeta.grade;
    public Sprite getCostTypeSprite()
    {
        var costType = typeMeta.getCostType();
        if (costType == EResource.Null)
        {
            if (typeMeta.isNFTCharacter())
                return CGlobal.nftInfo.sprite;
            else
                return null;
        }

        return CGlobal.GetResourceSprite(costType);
    }
    public Int32 CostValue => typeMeta.CostValue;
    public string PrefabName => meta.PrefabName;
    public ResourceTypeData getCost() => typeMeta.getCost();
    public Single MaxVelAir => typeMeta.MaxVelAir;
    public Single StaminaMax => typeMeta.StaminaMax;
    public Single PumpSpeed => typeMeta.PumpSpeed;
    public Single MaxVelXGround => typeMeta.MaxVelXGround;
    public bool isNFTCharacter() => typeMeta.isNFTCharacter();
    public bool isShopCharacter() => typeMeta.isShopCharacter();
    public bool isRewardCharacter() => typeMeta.isRewardCharacter();
    public bool canBuy() => typeMeta.canBuy();
    public EResource RefundType => typeMeta.RefundType;
    public Int32 RefundValue => typeMeta.RefundValue;
    public Int32 SkyStatus => typeMeta.SkyStatus;
    public Int32 LandStatus => typeMeta.LandStatus;
    public Int32 StaminaStatus => typeMeta.StaminaStatus;
    public Int32 PumpStatus => typeMeta.PumpStatus;
    public EStatusType Post_Status => typeMeta.Post_Status;
    public Int32 subGrade => typeMeta.subGrade;
}
public class Character : MonoBehaviour
{
    CBattlePlayer _battlePlayer;
    GameObject _CharacterBoby = null;
    Model_Ch _Model_Ch = null;
    Balloon _Balloon = null;
    BlowBalloon _BlowBalloon = null;
    Parachute _Parachute = null;
    CharacterName _CharacterName = null;
    BalloonEffect[] _BalloonEffect = null;
    readonly float _NameYFixed = 0.35f;
    KillScore _KillScore = null;
    ParticleSystem _DustPaticle = null;

    public void Init(CBattlePlayer BattlePlayer_, TeamMaterial teamMaterial, bool IsMe_, Transform Prop_, Camera Camera_)
    {
        _battlePlayer = BattlePlayer_;

        //Model_Ch Rotate Object
        _CharacterBoby = new GameObject();
        _CharacterBoby.transform.SetParent(transform);
        _CharacterBoby.transform.localPosition = Vector3.zero;
        _CharacterBoby.transform.localScale = Vector3.one;

        // Model_Ch
        var PrefabPath = "Prefabs/Char/" + _battlePlayer.Meta.PrefabName;
        var Prefab = Resources.Load(PrefabPath);
        Debug.Assert(Prefab != null);

        var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity, _CharacterBoby.transform);
        Obj.name = "Model_Ch";
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
        _Model_Ch = Obj.GetComponent<Model_Ch>();

        // Balloon
        Prefab = Resources.Load("Prefabs/Char/Balloon");
        Debug.Assert(Prefab != null);
        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity, transform);
        Obj.name = "Balloon";
        Obj.transform.localPosition = Vector3.zero;
        _Balloon = Obj.GetComponentInChildren<Balloon>();

        //Balloon Effect
        Prefab = Resources.Load("Prefabs/Char/BalloonEffect");
        _BalloonEffect = new BalloonEffect[global.c_BalloonCountForRegen];
        for (sbyte i = 0; i < global.c_BalloonCountForRegen; ++i)
        {
            Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity, Prop_);
            Obj.name = "BalloonEffect";
            Obj.transform.localPosition = new Vector3(0.0f, 0.2f, 0.3f);
            _BalloonEffect[i] = Obj.GetComponent<BalloonEffect>();

            _BalloonEffect[i].init(_Balloon.GetHalfWidth());

            _BalloonEffect[i].gameObject.SetActive(false);
        }

        Prefab = Resources.Load("Prefabs/UI/KillScore");
        Debug.Assert(Prefab != null);
        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity, Prop_);
        Obj.name = "KillScore";
        Obj.transform.localPosition = Vector3.zero;
        _KillScore = Obj.GetComponent<KillScore>();


        // BlowBalloon
        Prefab = Resources.Load("Prefabs/Char/BlowBalloon");
        Debug.Assert(Prefab != null);
        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity, _CharacterBoby.transform);
        Obj.name = "BlowBalloon";
        Obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.12f);
        Obj.transform.localScale = Vector3.zero;
        _BlowBalloon = Obj.GetComponentInChildren<BlowBalloon>();

        // Parachutes
        Prefab = Resources.Load("Prefabs/Char/Parachute");
        Debug.Assert(Prefab != null);
        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity, _CharacterBoby.transform);
        Obj.name = "Parachute";
        _Parachute = Obj.GetComponentInChildren<Parachute>();

        // Player Name
        Prefab = Resources.Load("Prefabs/UI/CharacterName");
        Debug.Assert(Prefab != null);

        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity, transform);
        Obj.name = "Name";
        Obj.transform.localPosition = Vector3.zero;
        _CharacterName = Obj.GetComponentInChildren<CharacterName>();
        _CharacterName.transform.localPosition = Vector3.zero;
        _CharacterName.init(_battlePlayer, Camera_);

        Prefab = Resources.Load("FX/00_FXPrefab/FX_Dust01");
        Debug.Assert(Prefab != null);
        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity, Prop_);
        Obj.name = "Dust";
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        _DustPaticle = Obj.GetComponent<ParticleSystem>();
        _DustPaticle.gameObject.SetActive(false);

        _Model_Ch.Init(_battlePlayer.Meta.PumpSpeed);
        _Balloon.Init(teamMaterial.balloon, _battlePlayer.BalloonCount);
        _BlowBalloon.Init(teamMaterial.balloon);
        _Parachute.Init(teamMaterial.parachute);
        _KillScore.gameObject.SetActive(false);
    }
    public void updateAnimationMoveSpeed()
    {
        _Model_Ch.setAnimationMoveSpeed(1.0f + Math.Abs(_battlePlayer.PlayerObject.Velocity.X));
    }
    public void Face(sbyte Dir_)
    {
        _CharacterBoby.transform.localEulerAngles = new Vector3(0.0f, 180.0f + (40.0f * -Dir_), 0.0f);
    }
    public void FaceUpDown(float VelY_)
    {
        _Model_Ch.transform.localEulerAngles = new Vector3(VelY_ * -10.0f, _Model_Ch.transform.localEulerAngles.y, 0.0f);
    }
    public void Stop()
    {
        _Model_Ch.Stop();
    }
    public void Move(sbyte Dir_)
    {
        Face(Dir_);
        _Model_Ch.Move();
    }
    public void Flap()
    {
        _Model_Ch.Flap();
    }
    public void Fly()
    {
        _Model_Ch.Fly();
    }
    public void Pump()
    {
        _Model_Ch.Pump();
    }
    public void ShowPoint(Int32 Point_)
    {
        _KillScore.init(Point_);
        _KillScore.transform.position = transform.position;
        _KillScore.gameObject.SetActive(true);
    }
    public void Die()
    {
        _Model_Ch.Die();
    }
    public void Regen(sbyte FaceDir_, sbyte BalloonCount_)
    {
        Face(FaceDir_);
        SetBalloonCount(BalloonCount_);
        _Model_Ch.Regen();
    }
    public void Win()
    {
        _Model_Ch.Win();
    }
    public void Lose()
    {
        _Model_Ch.Lose();
    }
    public void Land(sbyte Dir_)
    {
        if (Dir_ == 0)
            _Model_Ch.Stop();
        else
            _Model_Ch.Move();

        _DustPaticle.gameObject.SetActive(true);
        _DustPaticle.transform.position = transform.position;
        _DustPaticle.Play();
    }
    public void SetBalloonCountAndShowPopEffect(sbyte BalloonCount_, sbyte AttackerRelativeDir_)
    {
        SetBalloonCount(BalloonCount_);

        _BalloonEffect[BalloonCount_].gameObject.SetActive(true);
        _BalloonEffect[BalloonCount_].transform.position = transform.position;
        _BalloonEffect[BalloonCount_].ShowPopEffect(BalloonCount_ >= 1 ? AttackerRelativeDir_ : (sbyte)0);
    }
    public void ShowPoint(sbyte BalloonCount_, Int32 Point_)
    {
        if (BalloonCount_ < 0 || BalloonCount_ >= _BalloonEffect.Length)
            BalloonCount_ = 0;

        _BalloonEffect[BalloonCount_].ShowPoint(Point_);
    }
    public void SetBalloonCount(sbyte Count_)
    {
        _Balloon.SetCount(Count_);
    }
    public void BlowBalloonSetActive(bool Active_)
    {
        _BlowBalloon.gameObject.SetActive(Active_);
    }
    public void BlowBalloonSetScale(float Scale_)
    {
        _BlowBalloon.transform.localScale = new Vector3(Scale_, Scale_, Scale_);
    }
    public void FixedUpdate()
    {
        var screenPos = new Vector3(transform.position.x, transform.position.y + _NameYFixed, -0.1f);
        _CharacterName.transform.position = screenPos;
    }
    public void Show(bool Show_)
    {
        foreach (var i in gameObject.GetComponentsInChildren<Renderer>())
            i.enabled = Show_;
    }
    public void SetParachuteScale(float Scale_)
    {
        _Parachute.Scale = Scale_;
    }
    public void SetEmotion(Int32 Code_)
    {
        _CharacterName.SetEmotion(Code_);
    }
    public void SetStaminaItem(float ItemRetantionMax_)
    {
        _CharacterName.SetStaminaItem(ItemRetantionMax_);
    }
    public void SetShieldItem(float ItemRetantionMax_)
    {
        _CharacterName.SetShieldItem(ItemRetantionMax_);
    }
}
