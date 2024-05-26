using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBehavior : StateMachineBehaviour
{
    private float timeBetweenShots;
    [SerializeField] 
    private float startBetweenShots;
    //[SerializeField] 
   // private Transform laserPoint;
  //  [SerializeField]
    // private float laserShot;
    [SerializeField] 
    private GameObject bulletPrefab;
    [SerializeField]
    Vector2 moveDir;
    private Transform target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         target = GameObject.FindGameObjectWithTag("Player").transform;
         timeBetweenShots = startBetweenShots;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveDir = target.position.normalized;
        animator.SetFloat("moveX",moveDir.x);
        animator.SetFloat("moveY",moveDir.y);
       if(timeBetweenShots <=0){
            Instantiate(bulletPrefab, animator.transform.position, Quaternion.identity);
            timeBetweenShots = startBetweenShots;
        }else{
            timeBetweenShots -= Time.deltaTime;
        }
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
