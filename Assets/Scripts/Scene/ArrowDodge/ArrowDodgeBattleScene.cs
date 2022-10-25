using bb;
using rso.Base;
using rso.core;
using rso.gameutil;
using rso.physics;
using rso.unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ArrowDodgeBattleScene : SingleBattleScene
{
    static readonly UInt32 _intItemScreenWidth = (UInt32)(float)(global.arrowDodgeItemScreenWidth * global.arrowDodgePositionPrecision);
    static readonly UInt32 _intItemScreenHeight = (UInt32)(float)(global.arrowDodgeItemScreenHeight * global.arrowDodgePositionPrecision);
    static float _getRandomItemPointX(CFixedRandom32 fixedRandom)
    {
        var widthRandom = fixedRandom.Get() % _intItemScreenWidth;
        float floatWidthRandom = (float)widthRandom;
        return floatWidthRandom / global.arrowDodgePositionPrecision - global.arrowDodgeHalfItemScreenWidth;
    }
    static float _getRandomItemPointY(CFixedRandom32 fixedRandom)
    {
        var heightRandom = fixedRandom.Get() % _intItemScreenHeight;
        float floatHeightRandom = (float)heightRandom;
        return floatHeightRandom / global.arrowDodgePositionPrecision;
    }
    public float getRandomItemPointX()
    {
        return _getRandomItemPointX(_FixedRandom);
    }
    public float getRandomItemPointY()
    {
        return _getRandomItemPointY(_FixedRandom);
    }
    public SPoint getRandomItemPoint()
    {
        // 서버와 랜덤함수 호출 순서 맞추기 위해서
        var x = _getRandomItemPointX(_FixedRandom);
        var y = _getRandomItemPointY(_FixedRandom);
        return new SPoint(x, y);
    }

    SArrowDodgeMap _ArrowDodgeMap;
    SArrowDodgeBattleBeginNetSc _Proto;
    CArrowDodgeBattlePlayer _ArrowDodgeBattlePlayer;
    CFixedRandom32 _FixedRandom;
    CArrowDodgeArrowMaker _ArrowMaker;
    CArrowDodgeItemMaker _ItemMaker;
    CList<SArrowObject> _ArrowIts = new CList<SArrowObject>();
    CList<SArrowDodgeItemObject> _ItemIts = new CList<SArrowDodgeItemObject>();

    public void init(SArrowDodgeBattleBeginNetSc Proto_)
    {
        base.init(
            Proto_.Tick,
            CGlobal.MetaData.GetArrowDodgeMap(),
            () => { return string.Format("{2}\n{0}/{1}", CGlobal.GetSinglePlayCountLeftTime().Item1, CGlobal.MetaData.arrowDodgeConfigMeta.PlayCountMax, CGlobal.MetaData.getText(EText.SceenSingle_Replay)); },
            CGlobal.MetaData.arrowDodgeConfigMeta.scorePerGold,
            Proto_.BattleInfo);
        _ArrowDodgeMap = (SArrowDodgeMap)_Map;
        // 화살, 아이템 캐싱
        {
            // 화살이 화면에 머무는 시간 = 거리 / 가장 느린 속도
            // 화면에 보여질 최대 화살의 개수(캐싱할 화살 수) = 화살이 화면에 머무는 시간 / 가장 빠를때의 발사 주기
            var DownArrowStayDuration = global.arrowDodgeArrowActiveAreaHalfHeight * 2.0f / global.arrowDodgeMinDownVelocity;
            var MaxDownArrowInScreen = (Int32)(DownArrowStayDuration / CPhysics.TickToFloatTime(CArrowDodgeArrowMaker._GetDownArrowDuration(Int64.MaxValue)));

            var LeftArrowStayDuration = global.arrowDodgeArrowActiveAreaHalfWidth * 2.0f / global.arrowDodgeMinHorizontalVelocity;
            var MaxLeftArrowInScreen = (Int32)(LeftArrowStayDuration / CPhysics.TickToFloatTime(CArrowDodgeArrowMaker._GetLeftArrowDuration(Int64.MaxValue)));

            var RightArrowStayDuration = global.arrowDodgeArrowActiveAreaHalfWidth * 2.0f / global.arrowDodgeMinHorizontalVelocity;
            var MaxRightArrowInScreen = (Int32)(RightArrowStayDuration / CPhysics.TickToFloatTime(CArrowDodgeArrowMaker._GetRightArrowDuration(Int64.MaxValue)));

            _ObjectPool.Reserve(CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Arrow.PrefabName, MaxDownArrowInScreen + MaxLeftArrowInScreen + MaxRightArrowInScreen);

            // 화면에 보여지는 최대 아이템 개수 * 아이템 종류 만큼 캐싱
            for (Int32 i = 0; i < (Int32)EArrowDodgeItemType.Max; ++i)
            {
                var Item = CArrowDodgeItemMaker.MakeItem(new SPoint(), (EArrowDodgeItemType)i);
                _ObjectPool.Reserve(Item.GetPrefabName(), CGlobal.MetaData.arrowDodgeConfigMeta.maxItemCount);
            }
        }

        // Structure /////////////////////////////////
        foreach (var s in _ArrowDodgeMap.Structures)
            _Engine.AddObject(new CRectCollider2D(s, CEngineGlobal.c_StructureNumber, s.RectCollider2D, _RootObject));

        _FixedRandom = new CFixedRandom32(Proto_.RandomSeed);
        _ArrowMaker = new CArrowDodgeArrowMaker(_FixedRandom, Proto_.NextDownArrowTick, Proto_.NextLeftArrowTick, Proto_.NextRightArrowTick);
        _ItemMaker = new CArrowDodgeItemMaker(_FixedRandom, Proto_.NextItemTick);
        _Proto = Proto_;


        // Player ////////////////////////////
        var Character = _Proto.Character;

        var Obj = new GameObject(CGlobal.NickName);
        Obj.transform.SetParent(_Prop);

        _ArrowDodgeBattlePlayer = Obj.AddComponent<CArrowDodgeBattlePlayer>();
        _ArrowDodgeBattlePlayer.Init(
                new SPoint(),
                CGlobal.MetaData.Characters[_Proto.Player.CharCode],
                Character,
                _teamMaterials.Last(),
                _Engine.Tick,
                _Proto.Player,
                _Prop,
                camera,
                _HitArrowCallback,
                _GetItemCallback,
                _Proto.BattleInfo,
                _Proto.Bufs);

        _ArrowDodgeBattlePlayer.PlayerObject.SetParent(_RootObject);

        _AddBattlePlayer(_ArrowDodgeBattlePlayer);

        foreach (var a in _Proto.Arrows)
            _AddArrow(CArrowDodgeArrowMaker.MakeArrow(a.LocalPosition, a.Velocity));

        foreach (var i in _Proto.Items)
            _AddItem(CArrowDodgeItemMaker.MakeItem(i.LocalPosition, i.ItemType));

        if (_Proto.Started)
            _Engine.Start();
        else
            _gameUI.ShowGameStart();
    }
    protected override CBattlePlayer _getMyBattlePlayer()
    {
        return _ArrowDodgeBattlePlayer;
    }
    public void battleEnd(SArrowDodgeBattleEndNetSc Proto_)
    {
        _battleEnd(Proto_);

        if (_ArrowDodgeBattlePlayer.BattleInfo.Point >= CGlobal.LoginNetSc.User.SinglePointBest)
            CGlobal.LoginNetSc.User.SinglePointBest = _ArrowDodgeBattlePlayer.BattleInfo.Point;
    }
    void _HitArrowCallback(CArrow Arrow_, bool IsDefended_)
    {
        if (IsDefended_)
        {
            _addScore(CGlobal.MetaData.arrowDodgeConfigMeta.ArrowGetPoint);
            _showItemGetEffect(Arrow_.LocalPosition.ToVector3());
        }

        _ObjectPool.Delete(Arrow_.Iterator.Data.ObjectPoolIterator);
        _ArrowIts.Remove(Arrow_.Iterator);
    }
    void _GetItemCallback(CArrowDodgeItem Item_)
    {
        Item_.Proc(_Engine.Tick, _ArrowDodgeBattlePlayer, this);
        _showItemGetEffect(Item_.LocalPosition.ToVector3());

        _ObjectPool.Delete(Item_.Iterator.Data.ObjectPoolIterator);
        _ItemIts.Remove(Item_.Iterator);
    }
    protected override void _fixedUpdate()
    {
        _ArrowMaker.FixedUpdate(_Engine.Tick, this, _AddArrow);

        if (_ItemIts.Count < CGlobal.MetaData.arrowDodgeConfigMeta.maxItemCount)
            _ItemMaker.FixedUpdate(_Engine.Tick, this, _AddItem);

        for (var it = _ArrowIts.Begin(); it;)
        {
            var itCheck = it;
            it.MoveNext();

            if (!itCheck.Data.Arrow.FixedUpdate())
            {
                _RemoveArrow(itCheck);

                if (_ArrowDodgeBattlePlayer.IsAlive())
                    _addScore(CGlobal.MetaData.arrowDodgeConfigMeta.ArrowDodgePoint);
            }
        }
    }
    void _addScore(Int32 AddedPoint_)
    {
        _ArrowDodgeBattlePlayer.BattleInfo.Point += AddedPoint_;
        _updateScore();
    }
    void _updateScore()
    {
        _setScore(_Proto.BattleInfo.Point);
    }
    void _AddArrow(CArrow Arrow_)
    {
        Arrow_.SetParent(_RootObject);
        var ArrowIt = _Engine.AddMovingObject(Arrow_);

        var it = _ObjectPool.New(CGlobal.MetaData.MapMeta.ArrowDodgeMapInfo.Arrow.PrefabName, Arrow_.LocalPosition.ToVector2(), _Prop);

        var ArrowComponent = it.gameObject.GetComponent<Arrow>();
        ArrowComponent.Init(Arrow_);
        Arrow_.Iterator = _ArrowIts.Add(new SArrowObject(Arrow_, ArrowIt, it));
    }
    void _RemoveArrow(CList<SArrowObject>.SIterator ArrowObjectIt_)
    {
        _ObjectPool.Delete(ArrowObjectIt_.Data.ObjectPoolIterator);
        _Engine.RemoveMovingObject(ArrowObjectIt_.Data.ArrowIterator);
        _ArrowIts.Remove(ArrowObjectIt_);
    }
    void _AddItem(CArrowDodgeItem Item_)
    {
        Item_.SetParent(_RootObject);
        _Engine.AddObject(Item_);

        var it = _ObjectPool.New(Item_.GetPrefabName(), Item_.LocalPosition.ToVector2(), _Prop);
        Item_.Iterator = _ItemIts.Add(new SArrowDodgeItemObject(Item_, it));
    }
    protected override void _sendBattleEnd()
    {
        CGlobal.NetControl.Send(new SArrowDodgeBattleEndNetCs());
    }
    protected override void _retry()
    {
        Main.ArrowDodgeBattleJoin();
    }
    public void UpdateGold()
    {
        _gameUI.setGold(_Proto.BattleInfo.Gold);
    }
    public void UpdateScoreGold()
    {
        _updateScore();
        UpdateGold();
    }
}
public struct SArrowObject
{
    public CArrow Arrow;
    public CList<CMovingObject2D>.SIterator ArrowIterator;
    public CObjectPool.SIterator ObjectPoolIterator;

    public SArrowObject(CArrow Arrow_, CList<CMovingObject2D>.SIterator ArrowIterator_, CObjectPool.SIterator ObjectPoolIterator_)
    {
        Arrow = Arrow_;
        ArrowIterator = ArrowIterator_;
        ObjectPoolIterator = ObjectPoolIterator_;
    }
}
public struct SArrowDodgeItemObject
{
    public CArrowDodgeItem Item;
    public CObjectPool.SIterator ObjectPoolIterator;

    public SArrowDodgeItemObject(CArrowDodgeItem Item_, CObjectPool.SIterator ObjectPoolIterator_)
    {
        Item = Item_;
        ObjectPoolIterator = ObjectPoolIterator_;
    }
}
