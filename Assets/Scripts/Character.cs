using bb;
using rso.physics;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
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

    public void Init(CEngineGameMode GameMode_, CBattlePlayer BattlePlayer_, 
                     SCharacterMeta Meta_, SByte MyTeamIdx_, 
                     bool IsMe_,
                     GameObject ParticleParent_, Camera Camera_)
    {
        //Model_Ch Rotate Object
        _CharacterBoby = new GameObject();
        _CharacterBoby.transform.SetParent(transform);
        _CharacterBoby.transform.localPosition = Vector3.zero;
        _CharacterBoby.transform.localScale = Vector3.one;

        // Model_Ch
        var PrefabPath = "Prefabs/Char/" + BattlePlayer_.Meta.PrefabName;
        var Prefab = Resources.Load(PrefabPath);
        Debug.Assert(Prefab != null);

        var Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "Model_Ch";
        Obj.transform.SetParent(_CharacterBoby.transform);
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
        _Model_Ch = Obj.GetComponent<Model_Ch>();

        // Balloon
        Prefab = Resources.Load("Prefabs/Char/Balloon");
        Debug.Assert(Prefab != null);
        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "Balloon";
        Obj.transform.SetParent(transform);
        Obj.transform.localPosition = Vector3.zero;
        _Balloon = Obj.GetComponentInChildren<Balloon>();

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

            _BalloonEffect[i].init(_Balloon.GetHalfWidth());

            _BalloonEffect[i].gameObject.SetActive(false);
        }

        Prefab = Resources.Load("Prefabs/UI/KillScore");
        Debug.Assert(Prefab != null);
        Obj = (GameObject)UnityEngine.Object.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        Obj.name = "KillScore";
        Obj.transform.SetParent(ParticleParent_.transform);
        Obj.transform.localPosition = Vector3.zero;
        _KillScore = Obj.GetComponent<KillScore>();


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

        _Model_Ch.Init(BattlePlayer_, Meta_.RunAcc / 1.6f, Meta_.PumpSpeed);
        _Balloon.Init(GameMode_, BattlePlayer_, MyTeamIdx_);
        _BlowBalloon.Init(GameMode_, BattlePlayer_.BattlePlayer.TeamIndex, MyTeamIdx_);
        _Parachute.Init(GameMode_, BattlePlayer_.BattlePlayer.TeamIndex, MyTeamIdx_);
        _CharacterName.SetName(BattlePlayer_, Camera_, IsMe_);
        _KillScore.gameObject.SetActive(false);
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
    public void Die(Int32 AddPoint_)
    {
        Die();
        _KillScore.init(AddPoint_);
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
    public void SetHitBalloon(sbyte Count_, sbyte AttackerRelativeDir_, Int32 AddPoint_)
    {
        _BalloonEffect[Count_].gameObject.SetActive(true);
        _BalloonEffect[Count_].transform.position = transform.position;
        _BalloonEffect[Count_].ViewEffectBallon(Count_ >= 1 ? AttackerRelativeDir_ : (sbyte)0, AddPoint_);
        _Balloon.SetCount(Count_);

        if (Count_ >= global.c_BalloonCountForRegen) Debug.Assert(true);
    }
    public void SetHitBalloon(sbyte Count_, sbyte AttackerRelativeDir_)
    {
        _BalloonEffect[Count_].gameObject.SetActive(true);
        _BalloonEffect[Count_].transform.position = transform.position;
        _BalloonEffect[Count_].ViewEffectBallon(Count_ >= 1 ? AttackerRelativeDir_ : (sbyte)0);
        _Balloon.SetCount(Count_);

        if (Count_ >= global.c_BalloonCountForRegen) Debug.Assert(true);
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
}
