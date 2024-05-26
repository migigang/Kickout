using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theLastHope
{
public class doubleSpiral : MonoBehaviour
{
    [SerializeField]
    private float angle = 0f;
     [SerializeField]
    private float dobspiralBull;

    private Vector2 bulletMoveDirection;
    private Animator animator;
    private Transform target;


    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
     public void CanShooting()
    {
        //InvokeRepeating("Fire", 0f,0.1f);   
    }

    private void Fire(){
        for(int i=0; i <= 1; i++){
            float bulDirX = transform.position.x + Mathf.Sin(((angle + 180f * i) * Mathf.PI) / 180f );
            float bulDirY = transform.position.y + Mathf.Cos(((angle + 180f * i) * Mathf.PI) / 180f );

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = bulletPool.bulletPoolInstance.getBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation= transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<bullet2>().SetMoveDirection(bulDir);


        }
        angle += 10f;

        if(angle >= 360f){
            angle=0f;
            }

    }
     public void StopShooting()
    {
        CancelInvoke("Fire");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}
