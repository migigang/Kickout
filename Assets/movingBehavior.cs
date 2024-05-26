using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBehavior : StateMachineBehaviour
{
     private Transform target;
     [SerializeField]
     private float speed;
     [SerializeField]
     Vector2 moveDir;
     [SerializeField]
     private float stoppingDistance;
     [SerializeField]
     private float laserShot;
      [SerializeField]
    private float retreatDistance;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target.position, speed * Time.deltaTime);
        moveDir = target.position.normalized;
        animator.SetFloat("moveX",moveDir.x);
        animator.SetFloat("moveY",moveDir.y);

         if(Vector2.Distance(animator.transform.position,target.position) <  stoppingDistance && Vector2.Distance(animator.transform.position,target.position) > retreatDistance){
            animator.transform.position = animator.transform.position;
            //animator.SetBool("moving", false);
    }//else if(Vector2.Distance(animator.transform.position, target.position)< retreatDistance){
        //    animator.transform.position = Vector2.MoveTowards(animator.transform.position, target.position, -speed * Time.deltaTime);
       //     Debug.Log("ok");

      //  }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

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
