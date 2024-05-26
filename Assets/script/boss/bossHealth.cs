using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace theLastHope
{

public class bossHealth: MonoBehaviour
{
    public const int RESULT_NONE = 0;
    public const int RESULT_PLAYERWIN = 1;
    public const int RESULT_BOSSWIN = 2;
       
    [Header("-------Statistic Boss----------")]
    [SerializeField]
    private int maxHealth;
    public int currentHealth;
    public float stoppingDistance;
    public float laserRange;
    public float retreatDistance;
    public float moveSpeed;
    [Header("-------attribut tambahan----------")]
    public healthBar healthBar;
    [SerializeField]
    private Animator animator;
    private Shake shake;
    private Transform target;
    public Vector2 moveDir;
    public Vector2 BossPosition;
   // public Point lastPositionPlayer, lastPositionBoss;
    public int result;
    public BoxCollider2D boxCollider2d;
    public BossState state;
    public bool isDead;
    public bool isShooting =true;
  //  public BossHelper helper;
    public movement PlayerStat;
    public int attackrange=6;

    [Header("-------atribut Player----------")]
    public int playerHealth;

    [Header("-------rocket----------")]
    [SerializeField] 
    private GameObject rocket;
    public float startBetweenMissile;
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
    private Model model;
    [SerializeField]
    private float timer;
    public bool canshot=false;
    public bool bossTerkenaHit = false;


    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        result = RESULT_NONE;
        playerHealth = PlayerStat.health;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        shake = GameObject.FindGameObjectWithTag("screenShake").GetComponent<Shake>();
        timeBetweenMissile = startBetweenMissile;
        timeBetweenShots = startBetweenShots;
        moveDir = target.position.normalized;
         timer= Random.Range(20f, 30f);
        //lastPositionPlayer = null;
       // lastPositionBoss = null;
        isDead=false;
        //BossHelper.onAction +=walking;
        //UnityMainThreadDispatcher.Instance().Enqueue(walking());
        //UnityMainThreadDispatcher.Instance().Enqueue(laserShot());
        //UnityMainThreadDispatcher.Instance().Enqueue(circleBullet());
        
    }
    public bossHealth(){

    }

private void Update() {
   // circleBullet();
   if(bossTerkenaHit){
    bossTerkenaHit=false;
   }
}


    public void walking(){
    animator.SetTrigger("walk");
    animator.SetFloat("moveX",moveDir.x);
    animator.SetFloat("moveY",moveDir.y);
    if(Vector2.Distance(transform.position, target.position) > stoppingDistance){
    transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    } else if(Vector2.Distance(transform.position,target.position) <  stoppingDistance && Vector2.Distance(transform.position,target.position) > retreatDistance){
        transform.position = this.transform.position;
    }
        //yield return null;
    }

   public void laserShot(){
    if(Vector2.Distance(transform.position, target.position)>laserRange){
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
      //  yield return null;
    }
    public void goAway(){
        if(Vector2.Distance(transform.position, target.position) < retreatDistance){
        animator.SetTrigger("walk");
        transform.position = Vector2.MoveTowards(transform.position, target.position, -moveSpeed * Time.deltaTime);
        }
     }

    public void Missile(){
        animator.SetTrigger("spesialShot");
        if(timeBetweenMissile <=0){
        Instantiate(rocket, transform.position, Quaternion.identity);
        timeBetweenMissile = startBetweenMissile;            
        GetComponent<redzone>().redzoneArea();
    }else{
        timeBetweenMissile -= Time.deltaTime;
        }
    }

    public void shotshot(){
         if(isShooting){
            Debug.Log("yoooo");
        StartCoroutine(spesialShot());
        }
    }


     private IEnumerator spesialShot()
        {
            isShooting = false;
            Debug.Log("circle");
            doubleSpiral();
            yield return new WaitForSeconds(5f); // Ubah nilai jeda di sini (dalam detik)
            isShooting = true;
        }
    public void circleBullet(){
       timer -= Time.deltaTime;
        if(!canshot){
       gameObject.GetComponent<circleBullet>();
          }else if (timer<=0){
        gameObject.GetComponent<circleBullet>().StopShooting();
        canshot =false;
          }

       // yield return null;
    }

    public void Spiral(){
        gameObject.GetComponent<spiral>().CanShooting();
    }
    public void doubleSpiral(){
        gameObject.GetComponent<doubleSpiral>().CanShooting();
    }

    public void SetModelReference(Model modelRef)
        {
            model = modelRef;
        }

    public void TakeDamage(int damage){
        currentHealth -=damage;
        //model.bossList[BossID].health -= damage;
        healthBar.setHealth(currentHealth);
        shake.camShake();
        bossTerkenaHit = true;

        if(currentHealth <=1){
            animator.SetTrigger("dead");
            result=RESULT_PLAYERWIN;
            boxCollider2d.enabled = false;
            isDead=true;
        }
    }
    public void destroyed(){
        Destroy(gameObject);
    }
    


    public bool IsAvailable()
	{
		return !isDead && !isShooting;
	}

    public bossHealth CloneBoss()
	{
        bossHealth clone = new bossHealth();
        return clone;
    }
     public bossHealth ClonePlayer()
	{
        bossHealth clone = new bossHealth();
        return clone;
    }

   ///  private void OnDrawGizmos()
   // {
   //     Gizmos.color = Color.red; // Warna gizmo (misalnya, biru)
//
        // Menggambar lingkaran berdasarkan posisi dan jari-jari objek
  //      Gizmos.DrawWireSphere(transform.position, retreatDistance);
 //   }
         
    


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