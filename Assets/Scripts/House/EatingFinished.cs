﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingFinished : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Eater>().Devour();
    }
}
