using rso.physics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosSave : MonoBehaviour
{
    [SerializeField] GameObject SinglePlayerPos = null;
    [SerializeField] GameObject[] Multi2TeamPlayerPosArray = null;
    [SerializeField] GameObject[] Multi3TeamPlayerPosArray = null;
    [SerializeField] GameObject[] Multi6TeamPlayerPosArray = null;
    [SerializeField] GameObject[] OneOnOnePlayerPosArray = null;
    public Tuple<sbyte, List<SPoint>>  Get2TeamPlayerPosList()
    {
        var Poses = new List<SPoint>();
        foreach(var playerPos in Multi2TeamPlayerPosArray)
            Poses.Add(new SPoint(playerPos.transform.position.x, playerPos.transform.position.y));
        return new Tuple<sbyte, List<SPoint>>(2, Poses);
    }
    public Tuple<sbyte, List<SPoint>> Get3TeamPlayerPosList()
    {
        var Poses = new List<SPoint>();
        foreach (var playerPos in Multi3TeamPlayerPosArray)
            Poses.Add(new SPoint(playerPos.transform.position.x, playerPos.transform.position.y));
        return new Tuple<sbyte, List<SPoint>>(3, Poses);
    }
    public Tuple<sbyte, List<SPoint>> Get6TeamPlayerPosList()
    {
        var Poses = new List<SPoint>();
        foreach (var playerPos in Multi6TeamPlayerPosArray)
            Poses.Add(new SPoint(playerPos.transform.position.x, playerPos.transform.position.y));
        return new Tuple<sbyte, List<SPoint>>(6, Poses);
    }
    public Tuple<sbyte, List<SPoint>> GetOneOnOnePlayerPos()
    {
        var Poses = new List<SPoint>();
        foreach (var playerPos in OneOnOnePlayerPosArray)
            Poses.Add(new SPoint(playerPos.transform.position.x, playerPos.transform.position.y));
        return new Tuple<sbyte, List<SPoint>>(2, Poses);
    }
    public SPoint GetSinglePlayerPos()
    {
        return new SPoint(SinglePlayerPos.transform.position.x, SinglePlayerPos.transform.position.y);
    }
}
