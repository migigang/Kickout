using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theLastHope{
public class bossHealthq : MonoBehaviour
{
    public enum state{
        idle,
        Stage1,
        circleShot,
        shotlaser,
        walking,
        goAway,
        Stage2,
        spiral,
        doubleSpiral,
        shotlaserStage2,
        dead,
    }
    
    [SerializeField]
    private int maxHealth;
    public int currentHealth;

    private state currentState;

    [SerializeField]
    private float timer;
    [SerializeField]
    private float minTime;
    [SerializeField]
    private float maxTime;

    [SerializeField]
    private float stoppingDistance;
    [SerializeField]
    private float retreatDistance;

    [SerializeField]
    private float circleBull;
   
    [SerializeField]
    private float moveSpeed;
    
    public healthBar healthBar;
    private Animator animator;
    private Shake shake;
    private bool canshot =false;
    private Transform target;
    [SerializeField]
    private int transition;
    [SerializeField]
    Vector2 moveDir;

    [SerializeField] 
    private GameObject rocket;
    [SerializeField]
    private float startBetweenMissile;
    private float timeBetweenMissile;
     [Header("-------laserShot----------")]
    public float startBetweenShots;
    private float timeBetweenShots;
    //[SerializeField] 
   // private Transform laserPoint;
  //  [SerializeField]
    // private float laserShot;
    [SerializeField] 
    private GameObject bulletPrefab;
    public float laserRange;
    public BoxCollider2D boxCollider2d;
    audioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }

    void Start()
    {
        currentState = state.idle;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        shake = GameObject.FindGameObjectWithTag("screenShake").GetComponent<Shake>();
        timer= Random.Range(minTime, maxTime);
        timeBetweenMissile = startBetweenMissile;
    }

    // Update is called once per frame
    void Update()
    {
    transition = Random.Range(0,7);
        switch(currentState){
            case state.idle:
                if(currentState==state.idle){
                Debug.Log("idle");
                gameObject.GetComponent<circleBullet>().StopShooting();
                
                canshot =false;
                if(transition == 0){
                    if(timer<=0) {
                    currentState= state.Stage1;
                    animator.SetTrigger("stage1");
                    canshot =false;
                    }
                }
                else{
                    timer -= Time.deltaTime;
                }

                }
                break;

            case state.Stage1:
                if(currentState==state.Stage1){
                Debug.Log("stage 1");
                timer= Random.Range(minTime, maxTime);
                canshot =false;
                gameObject.GetComponent<circleBullet>().StopShooting();
                if(transition==0){
                     currentState= state.circleShot;
                     gameObject.GetComponent<circleBullet>().StopShooting();
                     canshot =false;
                     
                }else if(transition == 1){
                    Debug.Log("laser");
                    currentState= state.shotlaser;
                }
                 if(transition == 2){
                    Debug.Log("pergi!");
                    currentState = state.goAway;
                }if(transition == 3){
                    Debug.Log("jalan!");
                    currentState= state.walking;
                }
                if(transition == 5){
                    Debug.Log("DoubleSpiral!");
                    currentState= state.doubleSpiral;
                    canshot =false;
                }
                if(transition == 6){
                    Debug.Log("DoubleSpiral!");
                    currentState= state.spiral;
                    canshot =false;
                }

                }   
                break;
            case state.circleShot:
                Debug.Log("circleShoot");
                timer -= Time.deltaTime;
                if(!canshot){
                    audioManager.playSFX(audioManager.SpesialShot); 
                    gameObject.GetComponent<circleBullet>().CanShooting();
                    canshot =true;
                    animator.SetTrigger("circleShot");
                }else if (timer<=0){
                        gameObject.GetComponent<circleBullet>().StopShooting();
                        canshot =false;
                        currentState = state.Stage1;
                    }
                break;
                case state.shotlaser:
                timer -= Time.deltaTime;
                 Debug.Log("laser");
                if(Vector2.Distance(transform.position, target.position)<laserRange){
                audioManager.playSFX(audioManager.BossShot); 
                Debug.Log("laserShoot");
                animator.SetBool("laser", true);
                if(timeBetweenShots <=0){
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                timeBetweenShots = startBetweenShots;
                }else{
                timeBetweenShots -= Time.deltaTime;
                }
                }else{
                animator.SetBool("laser", false);
                }
                if(timer<=0){
                currentState=state.Stage1;
                animator.SetBool("laser", false);
                }
                break;
            case state.walking:
                timer -= Time.deltaTime;
                moveDir = target.position.normalized;
                animator.SetFloat("moveX",moveDir.x);
                animator.SetFloat("moveY",moveDir.y);
                if(Vector2.Distance(transform.position, target.position) > stoppingDistance){
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                } else if(Vector2.Distance(transform.position,target.position) <  stoppingDistance && Vector2.Distance(transform.position,target.position) > retreatDistance){
                transform.position = this.transform.position;
                }else if(Vector2.Distance(transform.position, target.position) < stoppingDistance){
                    goAway();
                }
                if(timer <= 0){
                currentState = state.Stage1;
                }
                break;
            case state.goAway:
                goAway();
                break;

            case state.spiral:
                Debug.Log("Spiral");
                timer -= Time.deltaTime;
                if(!canshot){
                    audioManager.playSFX(audioManager.SpesialShot); 
                    gameObject.GetComponent<spiral>().CanShooting();
                    canshot =true;
                    animator.SetTrigger("spiral");
                }else if (timer<=0){
                        gameObject.GetComponent<spiral>().StopShooting();
                        canshot =false;
                        currentState = state.Stage1;
                    }
                break;
            case state.doubleSpiral:
                Debug.Log("doubleSpiral");
                timer -= Time.deltaTime;
                if(!canshot){
                    audioManager.playSFX(audioManager.SpesialShot); 
                    gameObject.GetComponent<doubleSpiral>().CanShooting();
                    canshot =true;
                    animator.SetTrigger("doubleSpiral");
                }else if (timer<=0){
                        gameObject.GetComponent<doubleSpiral>().StopShooting();
                        canshot =false;
                        currentState = state.Stage1;
                    }
                break;
            
            
        }

        void goAway(){
             if(timer >= 0){
            transform.position = Vector2.MoveTowards(transform.position, target.position, -moveSpeed * Time.deltaTime);
            timer -= Time.deltaTime;
            }
            else if(timer <= 0){
                 currentState = state.Stage1;
            }
         }

        void Missile(){
            if(timeBetweenMissile <=0){
            Instantiate(rocket, transform.position, Quaternion.identity);
            timeBetweenMissile = startBetweenMissile;            
            GetComponent<redzone>().redzoneArea();
        }else{
            timeBetweenMissile -= Time.deltaTime;
        }
        }





       // if(currentHealth >= 250){
       //     gameObject.GetComponent<spiral>().StopShooting();
       //     gameObject.GetComponent<doubleSpiral>().StopShooting();
       //     canshot =false;
      //  }
      //  else if(currentHealth <= 250 && !canshot){
      //       gameObject.GetComponent<circleBullet>().StopShooting();
      //       animator.SetTrigger("stage2");
      //       gameObject.GetComponent<spiral>().CanShooting();
      //       gameObject.GetComponent<doubleSpiral>().CanShooting();
      //       canshot =true;
      //  }
    }

    public void TakeDamage(int damage){
        currentHealth -=damage;
        healthBar.setHealth(currentHealth);
        shake.camShake();

         if(currentHealth <=1){
            animator.SetTrigger("dead");
            //result=RESULT_PLAYERWIN;
            boxCollider2d.enabled = false;
            //isDead=true;
        }
    }
    public void destroyed(){
    Destroy(gameObject);
    }
    private void MoveRandomly()
    {
        // Boss moves randomly around the room
        transform.Translate(new Vector2(Random.Range(-1.0f, 1.0f) * moveSpeed * Time.deltaTime,
                                         Random.Range(-1.0f, 1.0f) * moveSpeed * Time.deltaTime));
    }
}
}

