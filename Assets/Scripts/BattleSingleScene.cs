using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSingleScene : MonoBehaviour
{
    public Camera Camera = null;
    public SingleModeUI GameUI = null;
    public GameObject ParticleParent = null;
    public GameObject CharacterParent = null;
    public SinglePlayObjectPool SingleOnjectPool = null;

    public bool IsGod = false;
    public Int32 StartStage = 0;
}
