using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CSeedValue
{
    Int32 _Seed = 0;
    Int32 _SeedAddValue = 0;
    public CSeedValue(Int32 Value_)
    {
        _Seed = Random.Range(1, 20000);
        _SeedAddValue = Value_ + _Seed;
    }
    public void Set(Int32 Value_)
    {
        _Seed = Random.Range(1, 20000);
        _SeedAddValue = Value_ + _Seed;
    }
    public void Add(Int32 Value_)
    {
        Int32 Value = Get() + Value_;
        _Seed = Random.Range(1, 20000);
        _SeedAddValue = Value + _Seed;
    }
    public Int32 Get()
    {
        return _SeedAddValue - _Seed;
    }
}
