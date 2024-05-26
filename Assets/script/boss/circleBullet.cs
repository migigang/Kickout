using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theLastHope
{
public class circleBullet : MonoBehaviour
{
    [SerializeField]
    private int bulletAmount = 10;
    [SerializeField]
    private float circleBull;

    [SerializeField]
    private float Distance;

    [SerializeField]
    private float starAngle = 90f, endAngle =270f;

    private Vector2 bulletMoveDirection;
    private Transform target;

    public bullet2 bul;



    void Start()
    {
      //  animator = GetComponent<Animator>();
        //target = GameObject.FindGameObjectWithTag("Player").transform;
       // InvokeRepeating("Fire", 0f,2f);
         
    }
     public void CanShooting()
    {
        Debug.Log("circle");
  
        
    }

    private void Fire(){
        Debug.Log("invoke");
        //if(Vector2.Distance(transform.position, target.position) > circleBull){
        //animator.SetTrigger("circleShot"); 
        float angleStep = (endAngle - starAngle) / bulletAmount;
        float angle = starAngle;

        for(int i=0; i < bulletAmount + 1; i++){
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f );
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f );

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = bulletPool.bulletPoolInstance.getBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation= transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<bullet2>().SetMoveDirection(bulDir);

            angle+=angleStep;
         }
       // }

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