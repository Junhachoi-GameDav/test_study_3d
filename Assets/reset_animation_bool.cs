using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset_animation_bool : StateMachineBehaviour
{
    public string target_bool;
    public bool status;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(target_bool, status);
    }

}
