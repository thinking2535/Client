using bb;
using rso.Base;
using rso.gameutil;
using rso.physics;
using rso.unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public struct SFlyAwayLandObject
{
    public CFlyAwayLand Land;
    public CList<CCollider2D>.SIterator LandIterator;
    public CObjectPool.SIterator ObjectPoolIterator;

    public SFlyAwayLandObject(CFlyAwayLand Land_, CList<CCollider2D>.SIterator LandIterator_, CObjectPool.SIterator ObjectPoolIterator_)
    {
        Land = Land_;
        LandIterator = LandIterator_;
        ObjectPoolIterator = ObjectPoolIterator_;
    }
}
public struct SFlyAwayItemObject
{
    public CFlyAwayItem Item;
    public CList<CCollider2D>.SIterator ItemIterator;
    public CObjectPool.SIterator ObjectPoolIterator;

    public SFlyAwayItemObject(CFlyAwayItem Item_, CList<CCollider2D>.SIterator ItemIterator_, CObjectPool.SIterator ObjectPoolIterator_)
    {
        Item = Item_;
        ItemIterator = ItemIterator_;
        ObjectPoolIterator = ObjectPoolIterator_;
    }
}

public class FlyAwayBattleScene : SingleBattleScene
{
    readonly float _objectAreaLeftHalf = global.c_ScreenWidth_2 + global.flyAwayMinLandDistance;

    [SerializeField] GameObject _particleWaterFall;

    SFlyAwayMap _FlyAwayMap;
    SFlyAwayBattleBeginNetSc _Proto;
    CFlyAwayBattlePlayer _FlyAwayBattlePlayer;
    float _XMoveDelta;
    float _itemXDistance;
    float _itemLandXDistance;

    CFixedRandom32 _FixedRandom;
    FlyAwayPathMaker _pathMaker;
    Int32 _LandCounter;
    CList<SFlyAwayLandObject> _LandIts = new CList<SFlyAwayLandObject>();
    CList<SFlyAwayItemObject> _ItemIts = new CList<SFlyAwayItemObject>();
    SPoint _LastLandPosition;
    PointText _LandingPointText;
    public void init(SFlyAwayBattleBeginNetSc Proto_)
    {
        base.init(
            Proto_.Tick,
            CGlobal.MetaData.GetFlyAwayMap(),
            () => { return string.Format("{2}\n{0}/{1}", CGlobal.GetSingleIslandPlayCountLeftTime().Item1, CGlobal.MetaData.flyAwayConfigMeta.PlayCountMax, CGlobal.MetaData.getText(EText.SceenSingle_Replay)); },
            CGlobal.MetaData.arrowDodgeConfigMeta.scorePerGold,
            Proto_.BattleInfo);
        _FlyAwayMap = (SFlyAwayMap)_Map;
        _LastLandPosition = Proto_.LastLandPosition;

        // Structure /////////////////////////////////
        foreach (var s in _FlyAwayMap.Structures)
            _Engine.AddObject(new CRectCollider2D(s, CEngineGlobal.c_StructureNumber, s.RectCollider2D, _RootObject));

        foreach (var s in _FlyAwayMap.deadZones)
            _Engine.AddObject(new CRectCollider2D(s, CEngineGlobal.c_DeadZoneNumber, s.RectCollider2D, _RootObject));

        _Engine.AddObject(new CRectCollider2D(_FlyAwayMap.ocean, CEngineGlobal.c_OceanNumber, _FlyAwayMap.ocean.RectCollider2D, _RootObject));
        _FixedRandom = new CFixedRandom32(Proto_.RandomSeed);
        _pathMaker = new FlyAwayPathMaker(global.flyAwayMinY, global.flyAwayMaxY, global.flyAwayMainLevelCount, global.flyAwaySubLevelCount, _FixedRandom, Proto_.pathMakerState);

        var minVelocityY = Math.Min(CGlobal.MetaData.MinVelAir, global.c_MaxVelDown);
        var itemPathSlope = minVelocityY * global.flyAwayItemSlopeFactor / CGlobal.MetaData.MinVelAir;
        _itemXDistance = _pathMaker.verticalStep / itemPathSlope;
        _itemLandXDistance = _itemXDistance * global.flyAwayItemCoinXDistanceMultiplier;
        _LandCounter = Proto_.LandCounter;
        _Proto = Proto_;
        _joypad.gameObject.SetActive(false);

        // Player ////////////////////////////
        var Character = _Proto.Character;
        var Obj = new GameObject(CGlobal.NickName);
        Obj.transform.SetParent(_Prop);

        _FlyAwayBattlePlayer = Obj.AddComponent<CFlyAwayBattlePlayer>();
        _FlyAwayBattlePlayer.Init(
                new SPoint(),
                CGlobal.MetaData.Characters[_Proto.Player.CharCode],
                Character,
                _teamMaterials.Last(),
                _Engine.Tick,
                _Proto.Player,
                _Prop,
                camera,
                _GetItemCallback,
                _LandCallback,
                _deadCallback,
                _Proto.BattleInfo);
        _XMoveDelta = _FlyAwayBattlePlayer.Meta.MaxVelAir * CEngine.DeltaTime;
        
        _FlyAwayBattlePlayer.PlayerObject.SetParent(_RootObject);
        _AddBattlePlayer(_FlyAwayBattlePlayer);


        // 섬, 아이템 캐싱
        {
            // 생성될 수 있는 지형의 최대 개수 = c_ScreenWidth / 가장 가까울 때의 거리 + 1(화면 내에서 최초 1개) + 2(화면 바깥 양쪽에 하나씩)
            var maxActiveLandCount = (Int32)(global.c_ScreenWidth / global.flyAwayMinLandDistance + 1 + 2);
            foreach (var l in CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Lands)
                _ObjectPool.Reserve(l.PrefabName, maxActiveLandCount);

            // 코인 (유저가 한계점까지 진행했다고 했을 때 화면 중간에 섬은 없고 코인으로 꽉 차 있을 상황 일 것이므로
            // 코인이 보여질 좌우 간격 = flyAwayMinLandDistance(left) + c_ScreenWidth + 한계점에서의 섬 간격
            var activeItemWidth = global.flyAwayMinLandDistance + global.c_ScreenWidth + 2.0f * global.c_ScreenWidth;
            // 코인 최대 개수 = 코인이 보여질 좌우 간격 / 코인 간격
            var maxActiveItemCount = (Int32)(activeItemWidth / _itemXDistance);
            _ObjectPool.Reserve(CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Coin.PrefabName, maxActiveItemCount);
            _ObjectPool.Reserve(CGlobal.MetaData.MapMeta.FlyAwayMapInfo.GoldBar.PrefabName, 6);
            _ObjectPool.Reserve(CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Apple.PrefabName, 6);
            _ObjectPool.Reserve(CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Meat.PrefabName, 4);
            _ObjectPool.Reserve(CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Chicken.PrefabName, 2);
        }

        foreach (var i in _Proto.Lands)
            _AddLand(new CFlyAwayLand(i.LocalPosition, i.Number, i.Index, i.State, i.NextActionTick));

        foreach (var i in _Proto.Items)
            _AddItem(_MakeItem(i.LocalPosition, i.ItemType));

        if (_Proto.Started)
            _Engine.Start();
        else
            _gameUI.ShowGameStart();

        var PointTextObj = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefabs/UI/PointText"), Vector3.zero, Quaternion.identity, _Prop);
        _LandingPointText = PointTextObj.GetComponent<PointText>();
    }
    protected override CBattlePlayer _getMyBattlePlayer()
    {
        return _FlyAwayBattlePlayer;
    }
    void _GetItemCallback(CFlyAwayItem Item_)
    {
        Item_.Proc(_FlyAwayBattlePlayer, this);
        _showItemGetEffect(Item_.LocalPosition.ToVector3());
        _RemoveItem(Item_.Iterator);
    }
    void _LandCallback(CFlyAwayLand Land_)
    {
        if (Land_.LandNumber == 0)
            return;

        _FlyAwayBattlePlayer.addStamina(CGlobal.MetaData.flyAwayConfigMeta.landingAddedStamina);

        var Diff = _FlyAwayBattlePlayer.PlayerObject.LocalPosition.X - Land_.LocalPosition.X;
        if (Diff < 0.0f)
            Diff = -Diff;

        float Emphasis;
        Int32 AddedPoint;
        string PerfectString;
        if (Diff < 0.05f)
        {
            Emphasis = 2.0f;

            ++_FlyAwayBattlePlayer.BattleInfo.PerfectCombo;

            if (_FlyAwayBattlePlayer.BattleInfo.PerfectCombo > CGlobal.LoginNetSc.User.IslandComboBest)
                CGlobal.LoginNetSc.User.IslandComboBest = _FlyAwayBattlePlayer.BattleInfo.PerfectCombo;

            AddedPoint = 100 * (_FlyAwayBattlePlayer.BattleInfo.PerfectCombo < CGlobal.MetaData.flyAwayConfigMeta.maxComboMultiplier ? _FlyAwayBattlePlayer.BattleInfo.PerfectCombo : CGlobal.MetaData.flyAwayConfigMeta.maxComboMultiplier);

            if (_FlyAwayBattlePlayer.BattleInfo.PerfectCombo > 1)
                PerfectString = "\n" + _FlyAwayBattlePlayer.BattleInfo.PerfectCombo.ToString() + "Combo";
            else
                PerfectString = "\nPerfect";

            CGlobal.Sound.PlayOneShot((Int32)ESound.Combo);
        }
        else
        {
            _FlyAwayBattlePlayer.BattleInfo.PerfectCombo = 0;

            if (Diff < 0.1f)
            {
                Emphasis = 1.2f;
                AddedPoint = 50;
                PerfectString = "\nNice";
            }
            else
            {
                Emphasis = 1.0f;
                AddedPoint = 10;
                PerfectString = "";
            }
        }

        _FlyAwayBattlePlayer.BattleInfo.Point += AddedPoint;
        _LandingPointText.Show(Land_.LocalPosition.ToVector3(), AddedPoint.ToString() + PerfectString, Emphasis);
        _gameUI.setScore(_FlyAwayBattlePlayer.BattleInfo.Point);
    }
    void _deadCallback(bool isIntoWater)
    {
        if (isIntoWater)
        {
            _particleWaterFall.transform.position = _FlyAwayBattlePlayer.PlayerObject.Position.ToVector3() - new Vector3(0.0f, 0.05f, 0.0f);
            _particleWaterFall.SetActive(true);
        }
    }
    protected override bool _touched(InputTouch.TouchState state, Int32 direction)
    {
        return false;
    }
    public void battleEnd(SFlyAwayBattleEndNetSc Proto_)
    {
        _battleEnd(Proto_);

        if (_FlyAwayBattlePlayer.BattleInfo.Point >= CGlobal.LoginNetSc.User.IslandPointBest)
            CGlobal.LoginNetSc.User.IslandPointBest = _FlyAwayBattlePlayer.BattleInfo.Point;
    }

    protected override void _fixedUpdate()
    {
        foreach (var i in _LandIts)
            i.Land.FixedUpdate(_Engine.Tick);

        if (!_FlyAwayBattlePlayer.Character.IsGround)
        {
            var itFirstLand = _LandIts.Begin();
            if (itFirstLand != _LandIts.End())
            {
                if (itFirstLand.Data.Land.LocalPosition.X < -_objectAreaLeftHalf)
                    _RemoveLand(itFirstLand);
            }

            var itFirstItem = _ItemIts.Begin();
            if (itFirstItem != _ItemIts.End())
            {
                if (itFirstItem.Data.Item.LocalPosition.X < -_objectAreaLeftHalf)
                    _RemoveItem(itFirstItem);
            }

            _LateAddLand();

            foreach (var i in _LandIts)
                i.Land.LocalPosition.X -= _XMoveDelta;

            foreach (var i in _ItemIts)
                i.Item.LocalPosition.X -= _XMoveDelta;

            if (_particleWaterFall.activeSelf)
                _particleWaterFall.transform.position = new Vector3(
                    _particleWaterFall.transform.position.x - _XMoveDelta,
                    _particleWaterFall.transform.position.y,
                    _particleWaterFall.transform.position.z);

            _LastLandPosition.X -= _XMoveDelta;
            _LandingPointText.transform.localPosition = new Vector2(_LandingPointText.transform.localPosition.x - _XMoveDelta, _LandingPointText.transform.localPosition.y);
        }
    }
    void _updateScore()
    {
        _setScore(_FlyAwayBattlePlayer.BattleInfo.Point);
    }
    void _AddLand(CFlyAwayLand Land_)
    {
        Land_.SetParent(_RootObject);
        var LandIt = _Engine.AddObject(Land_);
        var it = _ObjectPool.New(Land_.GetPrefabName(), Land_.LocalPosition.ToVector2(), _Prop);
        var LandComponent = it.gameObject.GetComponent<FlyAwayLand>();
        LandComponent.Init(Land_);
        Land_.Iterator = _LandIts.Add(new SFlyAwayLandObject(Land_, LandIt, it));
    }
    void _RemoveLand(CList<SFlyAwayLandObject>.SIterator LandObjectIt_)
    {
        _ObjectPool.Delete(LandObjectIt_.Data.ObjectPoolIterator);
        _Engine.RemoveObject(LandObjectIt_.Data.LandIterator);
        _LandIts.Remove(LandObjectIt_);
    }
    bool _LateAddLand()
    {
        if (_LastLandPosition.X > global.c_ScreenWidth_2)
            return false;

        ++_LandCounter;

        var RangeFactor = (float)(_LandCounter) / (float)global.flyAwayLandDistanceBase;
        var Distance = global.flyAwayMinLandDistance + RangeFactor;

        // Add Items
        {
            _LastLandPosition.X += _itemLandXDistance;
            _LateAddItem(new SPoint(_LastLandPosition.X, _pathMaker.getCurrentY()));

            float itemsDistanse = 0.0f;
            while (true)
            {
                if (itemsDistanse > Distance)
                {
                    _LastLandPosition.X += _itemLandXDistance;
                    _LastLandPosition.Y = _pathMaker.getCurrentY() - _getItemLandYDistance();
                    break;
                }

                _LastLandPosition.X += _itemXDistance;
                itemsDistanse += _itemXDistance;
                _LateAddItem(new SPoint(_LastLandPosition.X, _pathMaker.getNextY()));
            }
        }

        float normalizedLandNumber = RangeFactor > 1.0f ? 0.0f : 1.0f - RangeFactor;
        var Index = (Int32)(float)((float)CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Lands.Count * normalizedLandNumber);
        if (Index >= CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Lands.Count)
            Index = CGlobal.MetaData.MapMeta.FlyAwayMapInfo.Lands.Count - 1;

        _AddLand(new CFlyAwayLand(_LastLandPosition.GetCopy(), _LandCounter, Index));

        return true;
    }
    void _AddItem(CFlyAwayItem Item_)
    {
        Item_.SetParent(_RootObject);
        var ItemIt = _Engine.AddObject(Item_);

        var it = _ObjectPool.New(Item_.GetPrefabName(), Item_.LocalPosition.ToVector2(), _Prop);
        var LandComponent = it.gameObject.GetComponent<FlyAwayItem>();
        LandComponent.Init(Item_);
        Item_.Iterator = _ItemIts.Add(new SFlyAwayItemObject(Item_, ItemIt, it));
    }
    void _LateAddItem(SPoint LocalPosition_)
    {
        _AddItem(_MakeItem(LocalPosition_, CGlobal.MetaData.GetRandomFlyAwayStaminaItemType(_FixedRandom.Get())));
    }
    void _RemoveItem(CList<SFlyAwayItemObject>.SIterator ItemObjectIt_)
    {
        _ObjectPool.Delete(ItemObjectIt_.Data.ObjectPoolIterator);
        _Engine.RemoveObject(ItemObjectIt_.Data.ItemIterator);
        _ItemIts.Remove(ItemObjectIt_);
    }
    CFlyAwayItem _MakeItem(SPoint LocalPosition_, EFlyAwayItemType ItemType_)
    {
        switch (ItemType_)
        {
            case EFlyAwayItemType.Coin:
                return new CFlyAwayCoin(LocalPosition_);

            case EFlyAwayItemType.GoldBar:
                return new CFlyAwayGoldBar(LocalPosition_);

            case EFlyAwayItemType.Apple:
                return new CFlyAwayApple(LocalPosition_);

            case EFlyAwayItemType.Meat:
                return new CFlyAwayMeat(LocalPosition_);

            case EFlyAwayItemType.Chicken:
                return new CFlyAwayChicken(LocalPosition_);

            default:
                throw new Exception();
        }
    }
    protected override void _sendBattleEnd()
    {
        CGlobal.NetControl.Send(new SFlyAwayBattleEndNetCs());
    }
    protected override void _retry()
    {
        Main.FlyAwayBattleJoin();
    }
    public void UpdateGold()
    {
        _gameUI.setGold(_FlyAwayBattlePlayer.BattleInfo.Gold);
    }
    public void UpdateScoreGold()
    {
        _updateScore();
        UpdateGold();
    }
    float _getItemLandYDistance()
    {
        return _pathMaker.verticalStep * global.flyAwayItemCoinXDistanceMultiplier;
    }
}
