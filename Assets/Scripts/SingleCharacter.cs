using rso.unity;
using bb;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SingleCharacter : MonoBehaviour
{
    GameObject _CharacterBoby = null;
    Model_Ch _Model_Ch = null;
    Balloon _Balloon = null;
    BlowBalloon _BlowBalloon = null;
    Parachute _Parachute = null;
    CharacterName _CharacterName = null;
    BalloonEffect[] _BalloonEffect = null;
    readonly float _NameYFixed = 0.35f;
    ParticleSystem _DustPaticle = null;
    BalloonCollider _BalloonCollider = null;
    BoxCollider2D _ModelCollider = null;
    BoxCollider2D _ParachuteCollider = null;

    Vector2 _ModelCollBoxOffset = new Vector2(0.0f, 0.065f);
    Vector2 _ModelCollBoxSize = new Vector2(0.1258713f, 0.150337f);

    Vector2 _ParachuteCollBoxOffset = new Vector2(0.0f, 0.37f);
    Vector2 _ParachuteCollBoxSize = new Vector2(0.45f, 0.25f);
    
    bool _IsParachuteOn = false;
    bool _IsParachuteScaling = false;
    CLinear _ParachuteLinear = new CLinear();
    public void Init(CSinglePlayer SinglePlayer_, SCharacterMeta Meta_,
                     GameObject ParticleParent_, Camera Camera_, bool IsStamina_)
    {
        //Model_Ch Rotate Object
        _CharacterBoby = new GameObject();
        _CharacterBoby.transform.SetParent(transform);
        _CharacterBoby.transform.localPosition = Vector3.zero;
        _CharacterBoby.transform.localScale = Vector3.one;

        // Model_Ch
        Int32 CharCode = SinglePlayer_.Meta.Code;

        string PrefabPath = string.Format("Prefabs/Char/{0}", SinglePlayer_.Meta.PrefabName);
        var Prefab = Resources.Load(PrefabPath);
        Debug.Assert(Prefab != null);

        var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "Model_Ch";
        Obj.transform.SetParent(_CharacterBoby.transform);
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
        _Model_Ch = Obj.GetComponent<Model_Ch>();
        _ModelCollider = _Model_Ch.gameObject.AddComponent<BoxCollider2D>();
        _ModelCollider.offset = _ModelCollBoxOffset;
        _ModelCollider.size = _ModelCollBoxSize * new Vector2(0.869565f, 0.869565f);

        // Balloon
        Prefab = Resources.Load("Prefabs/Char/Balloon");
        Debug.Assert(Prefab != null);
        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "Balloon";
        Obj.transform.SetParent(transform);
        Obj.transform.localPosition = new Vector3(0.0f, 0.02f, 0.0f);
        _Balloon = Obj.GetComponentInChildren<Balloon>();
        _BalloonCollider = _Balloon.gameObject.AddComponent<BalloonCollider>();

        //Balloon Effect
        Prefab = Resources.Load("Prefabs/Char/BalloonEffect");
        _BalloonEffect = new BalloonEffect[global.c_BalloonCountForRegen];
        for (sbyte i = 0; i < global.c_BalloonCountForRegen; ++i)
        {
            Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
            Obj.name = "BalloonEffect";
            Obj.transform.SetParent(ParticleParent_.transform);
            Obj.transform.localPosition = new Vector3(0.0f, 0.2f, 0.3f);
            _BalloonEffect[i] = Obj.GetComponent<BalloonEffect>();

            _BalloonEffect[i].init(_BalloonCollider.GetHalfWidth());

            _BalloonEffect[i].gameObject.SetActive(false);
        }

        // BlowBalloon
        Prefab = Resources.Load("Prefabs/Char/BlowBalloon");
        Debug.Assert(Prefab != null);
        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "BlowBalloon";
        Obj.transform.SetParent(_CharacterBoby.transform);
        Obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.12f);
        Obj.transform.localScale = Vector3.zero;
        _BlowBalloon = Obj.GetComponentInChildren<BlowBalloon>();

        // Parachutes
        Prefab = Resources.Load("Prefabs/Char/Parachute");
        Debug.Assert(Prefab != null);
        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "Parachute";
        Obj.transform.SetParent(_CharacterBoby.transform);
        _Parachute = Obj.GetComponentInChildren<Parachute>();
        _ParachuteCollider = _Parachute.gameObject.AddComponent<BoxCollider2D>();
        _ParachuteCollider.offset = _ParachuteCollBoxOffset;
        _ParachuteCollider.size = _ParachuteCollBoxSize;

        // Player Name
        Prefab = Resources.Load("Prefabs/UI/CharacterName");
        Debug.Assert(Prefab != null);

        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "Name";

        Obj.transform.SetParent(transform);
        Obj.transform.localPosition = Vector3.zero;
        _CharacterName = Obj.GetComponentInChildren<CharacterName>();
        _CharacterName.transform.localPosition = Vector3.zero;
        _CharacterName.Init(Meta_);

        Prefab = Resources.Load("FX/00_FXPrefab/FX_Dust01");
        Debug.Assert(Prefab != null);
        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "Dust";
        Obj.transform.SetParent(ParticleParent_.transform);
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        _DustPaticle = Obj.GetComponent<ParticleSystem>();
        _DustPaticle.gameObject.SetActive(false);

        _Model_Ch.Init(Meta_.RunAcc / 1.6f, Meta_.PumpSpeed);
        _Balloon.SingleInit(SinglePlayer_.BalloonCount);
        _BalloonCollider.Init(SinglePlayer_.BalloonCount);
        _BlowBalloon.SingleInit();
        _Parachute.SingleInit();
        _CharacterName.SetName(SinglePlayer_, Camera_, IsStamina_);
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
    public void Fly(sbyte BalloonCount_)
    {
        _Model_Ch.Fly();

        if (BalloonCount_ == 0)
            ParachuteOn();
    }
    public void Pump()
    {
        _Model_Ch.Pump();
    }
    public void Die()
    {
        _ModelCollider.enabled = false;
        _Model_Ch.Die();
        ParachuteOff();
    }
    public void Regen(sbyte FaceDir_, sbyte BalloonCount_)
    {
        _ModelCollider.enabled = true;
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
    public void ParachuteOn()
    {
        _Model_Ch.Fly();
        
        if (_IsParachuteOn)
            return;

        _IsParachuteOn = true;
        _IsParachuteScaling = true;
        float StartScale = 0.0f;
        _ParachuteLinear.Get(ref StartScale);
        _ParachuteLinear.SetVelocity(1.0f, StartScale, 0.5f);
    }
    public void ParachuteOff()
    {
        if (!_IsParachuteOn)
            return;

        _IsParachuteOn = false;
        _IsParachuteScaling = true;
        float StartScale = 0.0f;
        _ParachuteLinear.Get(ref StartScale);
        _ParachuteLinear.SetVelocity(-1.0f, StartScale, 0.0f);
    }
    public void Land(sbyte Dir_)
    {
        if (Dir_ == 0)
            _Model_Ch.Stop();
        else
            _Model_Ch.Move();

        ParachuteOff();
        _DustPaticle.gameObject.SetActive(true);
        _DustPaticle.transform.position = transform.position;
        _DustPaticle.Play();
    }
    public void SetHitBalloon(sbyte Count_, sbyte AttackerRelativeDir_, Int32 AddPoint_)
    {
        _BalloonEffect[Count_].gameObject.SetActive(true);
        _BalloonEffect[Count_].transform.position = transform.position;
        _BalloonEffect[Count_].ViewEffectBallon(Count_ >= 1 ? AttackerRelativeDir_ : (sbyte)0, AddPoint_);
        _Balloon.SetCount(Count_);
        _BalloonCollider.SetCount(Count_);

        if (Count_ >= global.c_BalloonCountForRegen) Debug.Assert(true);
    }
    public void SetHitBalloon(sbyte Count_, sbyte AttackerRelativeDir_)
    {
        if (Count_ < 0) return;
        _BalloonEffect[Count_].gameObject.SetActive(true);
        _BalloonEffect[Count_].transform.position = transform.position;
        _BalloonEffect[Count_].ViewEffectBallon(Count_ >= 1 ? AttackerRelativeDir_ : (sbyte)0);
        _Balloon.SetCount(Count_);
        _BalloonCollider.SetCount(Count_);

        if (Count_ >= global.c_BalloonCountForRegen) Debug.Assert(true);
    }
    public void SetBalloonCount(sbyte Count_)
    {
        _Balloon.SetCount(Count_);
        _BalloonCollider.SetCount(Count_);
    }
    public void BlowBalloonSetActive(bool Active_)
    {
        _BlowBalloon.gameObject.SetActive(Active_);
    }
    public void BlowBalloonSetScale(float Scale_)
    {
        _BlowBalloon.transform.localScale = new Vector3(Scale_, Scale_, Scale_);
    }
    public void SetStaminaItem(float ItemRetantionMax_)
    {
        _CharacterName.SetStaminaItem(ItemRetantionMax_);
    }
    public bool GetStaminaItem()
    {
        return _CharacterName.GetStaminaItem();
    }
    public void SetShieldItem(float ItemRetantionMax_)
    {
        _CharacterName.SetShieldItem(ItemRetantionMax_);
    }
    public bool GetShieldItem()
    {
        return _CharacterName.GetShieldItem();
    }
    public void FixedUpdate()
    {
        var screenPos = new Vector3(transform.position.x, transform.position.y + _NameYFixed, -0.1f);
        _CharacterName.transform.position = screenPos;

        if (!_IsParachuteScaling)
            return;

        float Scale = 0.0f;
        if (!_ParachuteLinear.Get(ref Scale))
            _IsParachuteScaling = false;
        else
            _Parachute.Scale = Scale;
    }
    public void Show(bool Show_)
    {
        foreach (var i in gameObject.GetComponentsInChildren<Renderer>())
            i.enabled = Show_;
    }
}
