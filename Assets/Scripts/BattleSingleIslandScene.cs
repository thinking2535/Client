using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSingleIslandScene : MonoBehaviour
{
    public Camera Camera = null;
    public SingleIslandModeUI GameUI = null;
    public GameObject ParticleParent = null;
    public GameObject CharacterParent = null;
    public SingleIslandObjectPool SingleOnjectPool = null;
    public RectTransform BG_0 = null;
    public RectTransform BG_1 = null;
    public GameObject ParticleWaterFall = null;

    public bool IsGod = false;
    public Int32 StartIslandCount = 0;
}
