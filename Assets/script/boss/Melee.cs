using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField]
    private float timeMelee = 0;
    //public Animator animator;
  //  private Vector2 posisi;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeMelee+=1f;
        if(timeMelee >= 60f){
            timeMelee=0f;
           // Destroy(gameObject);
        }

    }
}
