using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBoss : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float stoppingDistance;
    [SerializeField]
    private float retreatDistance;
    [SerializeField]
    private float startBetweenShots;
    [SerializeField]
    private float startBetweenMissile;
    [SerializeField] 
    private GameObject bulletPrefab;
    [SerializeField] 
    private GameObject rocket;
    [SerializeField]
    private float rocketDistance;
    
    
    private float timeBetweenShots;
    private float timeBetweenMissile;
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = startBetweenShots;
        timeBetweenMissile = startBetweenMissile;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance){
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        } else if(Vector2.Distance(transform.position,target.position) <  stoppingDistance && Vector2.Distance(transform.position,target.position) > retreatDistance){
            transform.position = this.transform.position;
                    
        }else if(Vector2.Distance(transform.position, target.position)< retreatDistance){
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);

        }if(Vector2.Distance(transform.position, target.position) > rocketDistance){
            if(timeBetweenMissile <=0){
            Instantiate(rocket, transform.position, Quaternion.identity);
            timeBetweenMissile = startBetweenMissile;            
            GetComponent<redzone>().redzoneArea();
        }else{
            timeBetweenMissile -= Time.deltaTime;
        
        }
        }
        if(timeBetweenShots <=0){
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            timeBetweenShots = startBetweenShots;
        }else{
            timeBetweenShots -= Time.deltaTime;
        }
    }

}

