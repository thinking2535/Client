using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEventEmpty : MonoBehaviour
{
    public virtual void Footstep(Int32 Num)
    {
    }
}
public class CharacterAnimationEvent : CharacterAnimationEventEmpty
{
    public override void Footstep(Int32 Num)
    {
        //CGlobal.Sound.PlayOneShot(Num);
    }
}