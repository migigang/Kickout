using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;
using Random = UnityEngine.Random;


namespace theLastHope{


    public enum State{
        idle,
        walking,
        shotlaser,
        spesialShooting,
        dead,
        wait,
    }

    //stats NPC Boss
    public struct Boss {
        private static int nbBoss = 0;
        public static float moveSpeed = 7f;
        public static float stoppingDistance=5f;
        public static float laserRange=6f;
        public static float spesialShotRange=10f;
        public static float startBetweenShots=0.3f;
        public static float timeBetweenShots=0f;
        public static float timeBetweenMissile=0f;


        public int BossID;
        public Vector2 position;
        public int health;


        public Boss(Vector2 pos){
            BossID = nbBoss;
            nbBoss++;
            position = pos;
            health = 750;
        }

        public Boss(Boss BossToCopy){
            BossID = BossToCopy.BossID;
            position = BossToCopy.position;
            health = BossToCopy.health;
        }

        public static void setnbBoss(int i){
            nbBoss = i;
        }
    }
    
public class Model 
{
   private Boss [] bossList;
   public Transform target;
   private Animator animator;
   private GameObject bulletPrefab;
   private GameObject RocketPrefab;
   private GameObject rocket;
   private circleBullet spesialShot;
   private spiral spesialShot2;
   private doubleSpiral spesialShot3;
   private redzone redzones;
   public float inGameTimer;
   public float inGameDeltaTime;
    private Dictionary<string, object> myGameState;

   //jika ke 2 player masih hidup
   public bool isBothAgentAlive;

    public Boss getWinner()
    {
        foreach (Boss boss in bossList)
        {
            if (boss.health > 0) return boss;
        }
        return bossList[0];
    }

     // TEMPORARY VARIABLES
    private List<int> idList;
    private Vector2 tempPosition = new Vector2(-1.66f, 10.83f);
    private int tempInt;
    private int spesialShotState =0;
    private float delayTimer = 3f;


    public Model(Transform targetTransform, GameObject boss, int numberOfBoss, GameObject ammo, GameObject rocket){
        Boss.setnbBoss(0);
        target = targetTransform;
        bulletPrefab = ammo;
        RocketPrefab = rocket;
        spesialShot = boss.GetComponent<circleBullet>();
        spesialShot2 = boss.GetComponent<spiral>();
        spesialShot3 = boss.GetComponent<doubleSpiral>();
        redzones = boss.GetComponent<redzone>();
        animator = boss.GetComponent<Animator>();
        myGameState = new Dictionary<string, object>();
        bossList = new Boss[numberOfBoss];
        tempPosition = new Vector2(-1.66f, 10.83f);
        isBothAgentAlive=true;
       
    }

    public Model(Model modelToCopy){
        inGameTimer = 0f;
        target = modelToCopy.target;
        bulletPrefab = modelToCopy.bulletPrefab;
        spesialShot = modelToCopy.spesialShot;
        spesialShot2 = modelToCopy.spesialShot2;
        spesialShot3 = modelToCopy.spesialShot3;
        RocketPrefab = modelToCopy.RocketPrefab;
        redzones = modelToCopy.redzones;
        tempPosition = modelToCopy.tempPosition;
        animator = modelToCopy.animator;
        bossList = new Boss[modelToCopy.bossList.Length];
        foreach (Boss boss in modelToCopy.bossList){
            bossList[boss.BossID] = new Boss(boss);
        }
        isBothAgentAlive = true;
        myGameState = new Dictionary<string, object>();
    }


     public void actionHandler(State state, int BossID)
    {
        if (state != State.dead)
        {

            bossList[BossID].position = tempPosition;
            spesialShot.CancelInvoke("Fire");
            spesialShot2.CancelInvoke("Fire");
            spesialShot3.CancelInvoke("Fire");
            switch (state)
            {
                case State.walking:
                 Debug.Log("walking");
                 if(Vector2.Distance(tempPosition, target.position) > Boss.stoppingDistance){
                tempPosition = Vector2.MoveTowards(tempPosition, target.position, Boss.moveSpeed * Time.deltaTime);
                 }  
               if(Vector2.Distance(tempPosition, target.position) < Boss.stoppingDistance){
                tempPosition = Vector2.MoveTowards(tempPosition, target.position, -Boss.moveSpeed * Time.deltaTime);
               }
                    break;
            
                case State.shotlaser:
                if(Vector2.Distance(tempPosition, target.position)<Boss.laserRange){
                if(Boss.timeBetweenShots <=0){
                Debug.Log("laserShoot");
                animator.SetBool("laser", true);
               GameObject.Instantiate(bulletPrefab, tempPosition, Quaternion.identity);
                Boss.timeBetweenShots = Boss.startBetweenShots;
                }else{
                Boss.timeBetweenShots -= Time.deltaTime;
                animator.SetBool("laser", false);
                 }
                }else{
                    Debug.Log("player diluar jangkauan");
                    animator.SetBool("laser", false);
                 }
                    break;

            
                case State.spesialShooting:
                if(Vector2.Distance(tempPosition, target.position)<Boss.spesialShotRange){
                if (spesialShotState == 0)
                {
                Debug.Log("circleShoot");
                animator.SetTrigger("spesialShot");
                spesialShot.InvokeRepeating("Fire", 0f, 1f);
                spesialShotState = 1;
                }
                else if (spesialShotState == 1)
                {
                if(delayTimer>=0){
                delayTimer-=Time.deltaTime;
                Debug.Log("spiralShoot");
                animator.SetTrigger("spesialShot");
                spesialShot2.InvokeRepeating("Fire", 0f, 0.1f);
                }else{
                spesialShotState = 2;
                delayTimer = 3f;
                }
                }
                else if (spesialShotState == 2)
                {
                if(delayTimer>=0){
                delayTimer-=Time.deltaTime;
                Debug.Log("doubleSPiralShoot");
                animator.SetTrigger("spesialShot");
                spesialShot3.InvokeRepeating("Fire", 0f, 0.1f);
                }else{
                spesialShotState = 3;
                delayTimer = 3f;
                }
                }else if(spesialShotState == 3){
                if(Boss.timeBetweenMissile <=0){
                    if(Vector2.Distance(tempPosition, target.position)>Boss.laserRange){
                 Debug.Log("rocket");
                 animator.SetTrigger("spesialShot");
                GameObject.Instantiate(RocketPrefab, tempPosition, Quaternion.identity);
                Boss.timeBetweenMissile = Boss.startBetweenShots;                 
                redzones.redzoneArea();
                spesialShotState = 0;
                    }
                }else{
                    Boss.timeBetweenMissile -= Time.deltaTime;
                }
                }
                }else{
                    state = State.idle;
                }
                    break;
                default:
                    break;
            
            }
          
        }
    }

     public Dictionary<string, object> getGameState()
    {
        myGameState["AgentsInfo"] = bossList;
        
        return myGameState;
    }
    

}




}
 