using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonEffectAnimationControl : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, Int32 layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, Int32 layerIndex)
    //{
    //    
    //}

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, Int32 layerIndex)
    {
        animator.gameObject.GetComponentInParent<BalloonEffect>().EndEffect();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, Int32 layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, Int32 layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
