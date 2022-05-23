using rso.core;
using rso.physics;
using rso.unity;
using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBattlePlayer : MonoBehaviour
{
    CEngineGameMode _GameMode;
    public Int32 PlayerIndex { get; private set; } = 0;
    public CEnginePlayer _EnginePlayer = null;

    public SBattlePlayer BattlePlayer { get; private set; } = null;
    public SCharacterClientMeta Meta { get { return _EnginePlayer.Meta; } }
    public SCharacter Character { get { return _EnginePlayer.Character; } } 
    CBattlePlayer _Me = null;
    protected Character[] _Characters = null;
    protected CBlink _Blink = null;
    protected GameObject _ParticleParent;

    protected void _BlinkCallback(bool Show_)
    {
        _Show(Show_);
    }
    protected void _BlinkOn(Int64 DurationTick_)
    {
        if (DurationTick_ <= 0)
            return;

        _Blink.On((float)TimeSpan.FromTicks(DurationTick_).TotalSeconds, 0.1f, 0.05f);
    }
    public Vector2 velocity
    {
        get { return _EnginePlayer.Velocity.ToVector2(); }
    }
    public bool IsGround { get { return Character.IsGround; } }
    public sbyte BalloonCount { get { return Character.BalloonCount; } }
    public bool IsAlive()
    {
        return Character.IsAlive();
    }
    public void Init(CEngineGameMode GameMode_, Int64 Tick_, Int32 PlayerIndex_, sbyte MyTeamIndex_, SBattlePlayer BattlePlayer_, CEnginePlayer EnginePlayer_, CBattlePlayer Me_, TimePoint Now_, GameObject ParticleParent_, Camera Camera_)
    {
        _GameMode = GameMode_;
        PlayerIndex = PlayerIndex_;
        BattlePlayer = BattlePlayer_;
        _EnginePlayer = EnginePlayer_;
        _Me = Me_;
        _Blink = new CBlink(_BlinkCallback);
        _ParticleParent = ParticleParent_;

        var Obj = new GameObject("Char_0");
        Obj.AddComponent<Character>();
        Obj.transform.SetParent(transform);
        Obj.transform.localPosition = Vector3.zero;

        _Characters = GetComponentsInChildren<Character>();
        Debug.Assert(_Characters != null);

        foreach (var i in _Characters)
            i.Init(GameMode_, this, Meta, MyTeamIndex_, IsMe(), ParticleParent_, Camera_);

        var Animator = Obj.GetComponentInChildren<Animator>();
        if (IsMe())
            Animator.gameObject.AddComponent<CharacterAnimationEvent>();
        else
            Animator.gameObject.AddComponent<CharacterAnimationEventEmpty>();

        _BlinkOn(EnginePlayer_.Character.InvulnerableEndTick - Tick_);
        Face(EnginePlayer_.Character.Face);

        if (Character.PumpInfo.CountTo > 0)
        {
            _BlowBalloonOn();
            _Pump();
        }
    }
    protected void _Pump()
    {
        foreach (var i in _Characters)
            i.Pump();

        CGlobal.Sound.PlayOneShot((Int32)ESound.Pump);
    }
    protected void _BlowBalloonOn()
    {
        foreach (var i in _Characters)
            i.BlowBalloonSetActive(true);
    }
    protected void _FaceUpDown(float VelY_)
    {
        foreach (var i in _Characters)
            i.FaceUpDown(VelY_);
    }
    public void Stop()
    {
        foreach (var i in _Characters)
            i.Stop();
    }
    public void Move(sbyte Dir_)
    {
        _BlowBalloonOff();

        foreach (var i in _Characters)
            i.Move(Dir_);

        //        CGlobal.Sound.PlayOneShot((Int32)ESound.Walk);
    }
    public void Face(sbyte Dir_)
    {
        _BlowBalloonOff();

        foreach (var i in _Characters)
            i.Face(Dir_);
    }
    public void Fly()
    {
        _BlowBalloonOff();

        if (IsAlive()) // �׾��� ������ �������� ���� ������ ������ Fly �ݹ��� ���Ƿ�
        {
            foreach (var i in _Characters)
                i.Fly();
        }
    }
    public void Land()
    {
        foreach (var i in _Characters)
            i.Land(Character.Dir);
    }
    public void Flap()
    {
        _BlowBalloonOff();

        foreach (var i in _Characters)
            i.Flap();

        CGlobal.Sound.PlayOneShot((Int32)ESound.Flap);
    }
    public void Pump()
    {
        if (Character.PumpInfo.Count == 0) // ó�� �δ� �Ŷ�� BlowBalloon Ȱ��ȭ
            _BlowBalloonOn();

        _Pump();
    }
    public void PumpDone()
    {
        Stop();
        _BlowBalloonOff();
        _SetBalloonCount(BalloonCount);
    }
    public void ParachuteOn(bool On_)
    {
        if (On_)
            CGlobal.Sound.PlayOneShot((Int32)ESound.Parachute);
    }
    void _SetParachuteScale(float Scale_)
    {
        foreach (var i in _Characters)
            i.SetParachuteScale(Scale_);
    }
    public void Bounce()
    {
        if (BattlePlayer.UID ==  CGlobal.UID)
            CGlobal.Sound.PlayOneShot((Int32)ESound.Bounce);
    }
    public void Hit(Int32 AddedPoint_)
    {
        if (Character.BalloonCount >= 0) // Hit Balloon
            _SetHitBalloon(BalloonCount, (SByte)(_EnginePlayer.Velocity.X < 0.0f ? -1 : 1), AddedPoint_);
        else // Hit Body
            _Die(AddedPoint_);

        if (IsMe())
            CGlobal.VibeOn(0);
    }
    public void Regen(Int64 Tick_, SCharacter Character_)
    {
        _Regen(Character_.Face, Character_.BalloonCount);
        _BlinkOn(Character_.InvulnerableEndTick - Tick_);
    }
    public void SetEmotion(Int32 Code_)
    {
        _SetEmotion(Code_);
    }
    protected void _Die(Int32 AddPoint_)
    {
        _BlowBalloonOff();

        foreach (var i in _Characters)
            i.Die(AddPoint_);

        CGlobal.Sound.PlayOneShot((Int32)ESound.Down);
    }
    protected void _Die()
    {
        _BlowBalloonOff();

        foreach (var i in _Characters)
            i.Die();

        CGlobal.Sound.PlayOneShot((Int32)ESound.Down);
    }
    protected void _Regen(sbyte FaceDir_, sbyte BalloonCount_)
    {
        foreach (var i in _Characters)
            i.Regen(FaceDir_, BalloonCount_);
    }
    protected void _Win()
    {
        foreach (var i in _Characters)
            i.Win();
    }
    protected void _Lose()
    {
        foreach (var i in _Characters)
            i.Lose();
    }
    protected void _SetHitBalloon(sbyte Count_, sbyte AttackerRelativeDir_, Int32 AddPoint_)
    {
        foreach (var i in _Characters)
            i.SetHitBalloon(Count_, AttackerRelativeDir_, AddPoint_);
    }
    protected void _SetHitBalloon(sbyte Count_, sbyte AttackerRelativeDir_)
    {
        foreach (var i in _Characters)
            i.SetHitBalloon(Count_, AttackerRelativeDir_);
    }
    protected void _SetBalloonCount(sbyte Count_)
    {
        foreach (var i in _Characters)
            i.SetBalloonCount(Count_);
    }
    protected void _SetEmotion(Int32 Code_)
    {
        foreach (var i in _Characters)
            i.SetEmotion(Code_);
    }
    protected void _BlowBalloonOff()
    {
        foreach (var i in _Characters)
            i.BlowBalloonSetActive(false);
    }
    protected void _BlowBalloonSetScale(float Scale_)
    {
        foreach (var i in _Characters)
            i.BlowBalloonSetScale(Scale_);
    }
    public void FixedUpdate()
    {
        transform.position = _EnginePlayer.LocalPosition.ToVector2(); // ��Ȯ�� ����ó���ʹ� �������, �ε巴�� ����������

        if (!IsMe())
        {
            // �浹ó���ϱ� ���ؼ��� �����൵ ��ǥ�� �ϳ� �� �ʿ������� �������� �� ĳ���Ͱ� ���ÿ� �������� ���� ������ ��� ������,
            // Ÿ���� ������ �� ĳ������ ������ ����� ĳ���͸� ���̵���
            if (transform.position.y > _Me.transform.position.y)
            {
                if (transform.position.y - _Me.transform.position.y > global.c_ScreenHeight_2) // global.c_ScreenHeight_2 �� �������� ����, �����ϴ� ī�޶� �� ĳ������ �߾��� ������ �Ұ�.
                    transform.position = new Vector2(transform.position.x, transform.position.y - global.c_ScreenHeight);
            }
            else
            {
                if (_Me.transform.position.y - transform.position.y > global.c_ScreenHeight_2) // global.c_ScreenHeight_2 �� �������� ����, �����ϴ� ī�޶� �� ĳ������ �߾��� ������ �Ұ�.
                    transform.position = new Vector2(transform.position.x, transform.position.y + global.c_ScreenHeight);
            }
        }
    }
    public void Update()
    {
        _Blink.Update();
        _FaceUpDown(velocity.y);
        _BlowBalloonSetScale(Character.PumpInfo.Scale);
        _SetParachuteScale(Character.ParachuteInfo.Scale);
    }
    protected void _Show(bool Show_)
    {
        foreach (var i in _Characters)
            i.Show(Show_);
    }
    public float GetY()
    {
        return transform.position.y;
    }
    public bool IsMe() // ing CBattlePlayerMe ��
    {
        return (this == _Me);
    }
}