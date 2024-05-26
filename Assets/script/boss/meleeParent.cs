using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeParent : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController animController;
    [SerializeField]
    private float delay = 0.3f;
    [SerializeField]
    private bool attackBlocked;
    void Start()
    {
        animator.runtimeAnimatorController = animController;
    }

    // Update is called once per frame
    void Update()
    {

}
        public void Attack(){
            if(attackBlocked)
                return;
            animator.SetTrigger("Swings");
            attackBlocked = true;
            StartCoroutine(delayAttack());  
        }

        private IEnumerator delayAttack(){
           yield return new WaitForSeconds(delay);
           attackBlocked = false;
        }
    }
