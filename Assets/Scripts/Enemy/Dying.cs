using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dying : StateMachineBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    float t = 0;
    float T = 1.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Start anim");
        startPos = animator.transform.position; 
        endPos = startPos + animator.GetComponent<Enemy>().dyingVector.normalized * 4.0f;
        animator.transform.LookAt(startPos - endPos);
    }

// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Dying anim");
        t += Time.deltaTime;
        animator.transform.position = Vector3.Lerp(startPos, endPos, Mathf.Sqrt(Mathf.Clamp(t, 0, T)));
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
