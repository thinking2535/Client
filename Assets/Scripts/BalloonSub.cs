using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BalloonSub : MonoBehaviour
{
    [SerializeField] Transform[] BalloonTrans = null;

    //상하 운동만 계산이 필요함 그래서 Y 좌표만 별도로 정리.
    private static readonly float MinBounceVelocity = -0.0005f;
    private static readonly float BounceRandomRange = 0.0005f;
    private float[] StartVelocity = { 0.0f, 0.0f };
    private readonly float Acceleration = 0.005f;
    private float[] StartPosY = { 0.0f, 0.0f };   //풍선의 시작 위치.
    private float EndUpPosY = 0.01f;            //풍선의 최고 높이.
    private float EndDownPosY = -0.01f;           //풍선의 최소 높이.

    public void Init(Material material)
    {
        foreach (var i in GetComponentsInChildren<MeshRenderer>())
            i.material = material;

        if (BalloonTrans.Length > 1)
        {
            StartVelocity[0] = MinBounceVelocity - Random.Range(0.0f, BounceRandomRange);
            InitMoveBalloon(0);
            StartVelocity[1] = MinBounceVelocity - Random.Range(0.0f, BounceRandomRange);
            InitMoveBalloon(1);
        }
        else
        {
            //풍선이 1개일때 최고 높이와 최소 높이 보정이 필요함.
            EndUpPosY = 0.00f;
            EndDownPosY = -0.02f;
            StartVelocity[0] = MinBounceVelocity - Random.Range(0.0f, BounceRandomRange);
            InitMoveBalloon(0);
        }
    }
    private void Update()
    {
        for (Int32 i = 0; i < BalloonTrans.Length; i++)
        {
            if (BalloonTrans[i] == null) continue;

            MoveBalloon(i);
        }
    }

    //풍선 움직이는 방향 및 위치 초기화.
    private void InitMoveBalloon(Int32 idx_)
    {
        StartPosY[idx_] = EndDownPosY;
        BalloonTrans[idx_].localPosition = new Vector3(BalloonTrans[idx_].localPosition.x, StartPosY[idx_]);
    }

    //풍선 움직이는 실제 함수.
    private void MoveBalloon(Int32 idx_)
    {
        StartVelocity[idx_] += Acceleration * Time.deltaTime;
        var posY = BalloonTrans[idx_].localPosition.y + StartVelocity[idx_];
        if(posY >= EndUpPosY)
        {
            StartVelocity[idx_] = MinBounceVelocity - Random.Range(0.0f, BounceRandomRange);
            posY = EndUpPosY;
        }
        BalloonTrans[idx_].localPosition = new Vector3(BalloonTrans[idx_].localPosition.x, posY);
    }
}
