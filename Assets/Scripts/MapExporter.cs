#if UNITY_EDITOR

using bb;
using Folder;
using rso.core;
using rso.physics;
using rso.unity;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MapExporter : MonoBehaviour
{
    static string _resourcesPath = "/Assets/Resources/";
    static string _getPathWithoutResourcesPath(string path)
    {
        return path.Substring(path.IndexOf(_resourcesPath) + _resourcesPath.Length);
    }
    [SerializeField] [Folder] string _oneOnOneFolder;

    [SerializeField] [Folder] string _arrowDodgeFolder;
    [SerializeField] GameObject _arrowDodgeArrowPrefab;
    [SerializeField] GameObject _arrowDodgeCoinPrefab;
    [SerializeField] GameObject _arrowDodgeGoldBarPrefab;
    [SerializeField] GameObject _arrowDodgeShieldPrefab;
    [SerializeField] GameObject _arrowDodgeStaminaPrefab;

    [SerializeField] [Folder] string _flyAwayFolder;
    [SerializeField] GameObject _flyAwayApplePrefab;
    [SerializeField] GameObject _flyAwayChickenPrefab;
    [SerializeField] GameObject _flyAwayCoinPrefab;
    [SerializeField] GameObject _flyAwayGoldBarPrefab;
    [SerializeField] GameObject _flyAwayMeatPrefab;
    [SerializeField] GameObject _flyAwayLand0Prefab;
    [SerializeField] GameObject _flyAwayLand1Prefab;
    [SerializeField] GameObject _flyAwayLand2Prefab;
    [SerializeField] GameObject _flyAwayLand3Prefab;
    [SerializeField] GameObject _flyAwayLand4Prefab;

    [SerializeField] [Folder] string _tutorialFolder;

    void Awake()
    {
        Debug.Log("Begin");

        var MapMeta = new SMapMeta(
            _getMultiMaps(),
            _getArrowDodgeMapInfo(),
            _getFlyAwayMapInfo());

        {
            var Stream = new CStream();
            Stream.Push(MapMeta);
            Stream.SaveFile("../Server/Bin/GameServer/MetaData/Map.bin");
            Stream.SaveFile("Assets/Resources/MetaData/Map.bytes");
        }

        Debug.Log("End");
    }
    void Update()
    {
    }
    private void OnGUI()
    {
    }
    private void OnApplicationQuit()
    {
    }
    public struct SMapObject
    {
        public string PrefabName;
        public GameObject MapObject;

        public SMapObject(string PrefabName_, GameObject MapObject_)
        {
            PrefabName = PrefabName_;
            MapObject = MapObject_;
        }
    }
    List<SMapObject> _getMapObjects<T>(string prefabFolder) where T : MonoBehaviour
    {
        var maps = prefabFolder.LoadFolder<T>();
        var mapObjects = new List<SMapObject>();
        var path = prefabFolder.ToFolderInfo().path;

        foreach (var map in maps)
        {
            var filePath = path + "/" + map.name;
            var prefabPathFile = _getPathWithoutResourcesPath(filePath);
            mapObjects.Add(new SMapObject(prefabPathFile, map.gameObject));
        }

        return mapObjects;
    }
    List<SMultiMap> _getMultiMaps()
    {
        var Maps = new List<SMultiMap>();

        var MapObjects = _getMapObjects<MultiBattleScene>(_oneOnOneFolder);
        foreach (var o in MapObjects)
        {
            var Prop = o.MapObject.transform.Find(CGlobal.c_PropName);
            Debug.Assert(Prop != null);

            var PropPosition = Prop.position.ToSPoint();

            var PlayerPos = o.MapObject.GetComponentInChildren<PlayerPos>();
            var Poses = PlayerPos.Get();
            var Structures = new List<SBoxCollider2D>();
            var StructureMoves = new List<SStructureMove>();

            for (Int32 i = 0; i < Prop.childCount; ++i)
            {
                var tf = Prop.GetChild(i);
                var us = tf.gameObject.GetComponentInChildren<UnityMovingStructure>();
                if (us == null)
                {
                    var BoxCollider2Ds = tf.gameObject.GetComponentsInChildren<BoxCollider2D>();
                    if (BoxCollider2Ds.Length == 0)
                        continue;

                    foreach (var c in BoxCollider2Ds)
                        Structures.Add(c.ToSBoxCollider2D());
                }
                else
                {
                    var BoxCollider2Ds = tf.gameObject.GetComponentsInChildren<BoxCollider2D>();
                    if (BoxCollider2Ds.Length == 0)
                        throw new Exception("EngineUnityStructure Need BoxCollider2D");

                    var Colliders = new List<SRectCollider2D>();

                    foreach (var c in BoxCollider2Ds)
                        Colliders.Add(c.ToSRectCollider2D());

                    StructureMoves.Add(new SStructureMove(Colliders, us.BeginPos.ToSPoint(), us.EndPos.ToSPoint(), us.Velocity, us.Delay));
                }
            }

            Maps.Add(new SMultiMap(PropPosition, Maps.Count, o.PrefabName, Poses, Structures, StructureMoves));
        }

        return Maps;
    }
    SArrowDodgeMapInfo _getArrowDodgeMapInfo()
    {
        var Maps = new List<SArrowDodgeMap>();

        var MapObjects = _getMapObjects<ArrowDodgeBattleScene>(_arrowDodgeFolder);
        foreach (var o in MapObjects)
        {
            var Prop = o.MapObject.transform.Find(CGlobal.c_PropName);
            Debug.Assert(Prop != null);

            var PropPosition = Prop.position.ToSPoint();
            var Structures = new List<SBoxCollider2D>();

            for (Int32 i = 0; i < Prop.childCount; ++i)
            {
                var tf = Prop.GetChild(i);
                var BoxCollider2Ds = tf.gameObject.GetComponentsInChildren<BoxCollider2D>();
                if (BoxCollider2Ds.Length == 0)
                    continue;

                foreach (var c in BoxCollider2Ds)
                    Structures.Add(c.ToSBoxCollider2D());
            }

            Maps.Add(new SArrowDodgeMap(PropPosition, o.PrefabName, Structures));
        }

        return new SArrowDodgeMapInfo(
            Maps,
            _getSPrefabNameCollider(_arrowDodgeArrowPrefab),
            _getSPrefabNameCollider(_arrowDodgeCoinPrefab),
            _getSPrefabNameCollider(_arrowDodgeGoldBarPrefab),
            _getSPrefabNameCollider(_arrowDodgeShieldPrefab),
            _getSPrefabNameCollider(_arrowDodgeStaminaPrefab));
    }
    SFlyAwayMapInfo _getFlyAwayMapInfo()
    {
        var Maps = new List<SFlyAwayMap>();

        var MapObjects = _getMapObjects<FlyAwayBattleScene>(_flyAwayFolder);
        foreach (var o in MapObjects)
        {
            var Prop = o.MapObject.transform.Find(CGlobal.c_PropName);
            Debug.Assert(Prop != null);

            var PropPosition = Prop.position.ToSPoint();
            var Structures = new List<SBoxCollider2D>();
            {
                Structures.Add(
                    Prop.transform.Find("roofCollider")
                    .GetComponent<BoxCollider2D>()
                    .ToSBoxCollider2D());
            }

            var deadZones = new List<SBoxCollider2D>();
            {
                deadZones.Add(
                    Prop.transform.Find("leftDeadZoneCollider")
                    .GetComponent<BoxCollider2D>()
                    .ToSBoxCollider2D());

                deadZones.Add(
                    Prop.transform.Find("rightDeadZoneCollider")
                    .GetComponent<BoxCollider2D>()
                    .ToSBoxCollider2D());
            }

            SBoxCollider2D ocean;
            {
                ocean = Prop.transform.Find("oceanCollider")
                    .GetComponent<BoxCollider2D>()
                    .ToSBoxCollider2D();
            }

            Maps.Add(new SFlyAwayMap(PropPosition, o.PrefabName, Structures, deadZones, ocean));
        }

        return new SFlyAwayMapInfo(
            Maps,
            new List<SPrefabNameCollider> {
                _getSPrefabNameCollider(_flyAwayLand0Prefab),
                _getSPrefabNameCollider(_flyAwayLand1Prefab),
                _getSPrefabNameCollider(_flyAwayLand2Prefab),
                _getSPrefabNameCollider(_flyAwayLand3Prefab),
                _getSPrefabNameCollider(_flyAwayLand4Prefab),
            },
            _getSPrefabNameCollider(_flyAwayCoinPrefab),
            _getSPrefabNameCollider(_flyAwayGoldBarPrefab),
            _getSPrefabNameCollider(_flyAwayApplePrefab),
            _getSPrefabNameCollider(_flyAwayMeatPrefab),
            _getSPrefabNameCollider(_flyAwayChickenPrefab));
    }
    SPrefabNameCollider _getSPrefabNameCollider(GameObject prefab)
    {
        var fullPrefabFilePathWithoutExtension = prefab.getFullPrefabPathNameWithoutExtension();
        var prefabFilePath = _getPathWithoutResourcesPath(fullPrefabFilePathWithoutExtension);

        var BoxCollider2D = prefab.GetComponentInChildren<BoxCollider2D>();
        if (BoxCollider2D == null)
            throw new System.Exception();

        return new SPrefabNameCollider(prefabFilePath, BoxCollider2D.ToSRectCollider2D());
    }
}
#endif