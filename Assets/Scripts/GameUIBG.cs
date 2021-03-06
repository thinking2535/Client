using System;
using UnityEngine;
using Random = UnityEngine.Random;

class GameUIBG : MonoBehaviour
{
    public GameObject[] _Clouds = null;

    private float[] _StartVelocitys = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    private readonly float _StartVelocity = 0.0005f;
    private readonly float _MinYPos = -5.0f;
    private readonly float _MaxYPos = 5.0f;
    private readonly float _MinXPos = 15.0f;
    private readonly float _MaxXPos = -15.0f;
    private readonly float _StartZPos = 19.0f;

    private void Awake()
    {
        for (Int32 i = 0; i < _Clouds.Length; ++i)
        {
            InitCloud(i);
            MixCloud(i);
        }
    }
    private void Update()
    {
        for (Int32 i = 0; i < _Clouds.Length; ++i)
            MoveCloud(i);
    }
    void InitCloud(Int32 idx_)
    {
        var scale = Random.Range(1.0f, 5.0f);
        var startYpos = Random.Range(_MinYPos, _MaxYPos);
        _StartVelocitys[idx_] = _StartVelocity * scale;
        _Clouds[idx_].transform.localPosition = new Vector3(_MinXPos, startYpos, _StartZPos - (scale - 1.0f));
        _Clouds[idx_].transform.localScale = new Vector3(scale, scale, 1);
    }
    void MixCloud(Int32 idx_)
    {
        var xPos = Random.Range(-15.0f, 15.0f);
        _Clouds[idx_].transform.localPosition = new Vector3(xPos, _Clouds[idx_].transform.localPosition.y, _Clouds[idx_].transform.localPosition.z);
    }
    void MoveCloud(Int32 idx_)
    {
        _Clouds[idx_].transform.localPosition = new Vector3(_Clouds[idx_].transform.localPosition.x - _StartVelocitys[idx_], _Clouds[idx_].transform.localPosition.y, _Clouds[idx_].transform.localPosition.z);
        if (_Clouds[idx_].transform.localPosition.x < _MaxXPos)
        {
            InitCloud(idx_);
        }
    }
}
