using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchFinished : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = Time.time + .755f;
        animator.SetBool("RightPunch", false);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    float time;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (time > 0 && time < Time.time)
        {
            time = 0;
            animator.SetBool("RightPunch", true);
        }
    }
}
