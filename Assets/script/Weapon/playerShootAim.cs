using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

public class playerShootAim : weaponBase//MonoBehaviour
{
    [SerializeField] 
    private float rotationSpeed;
    [SerializeField] 
    private Transform gunPoint;
    [SerializeField] 
    private GameObject bulletPrefab;
    //[SerializeField] 
    //private float weaponRange = 10f;
    [SerializeField] 
    private float bulletSpeed = 10f;
    [SerializeField] 
    private Animator muzzleFlashAnim;
    [SerializeField] 
    private Animator aiming;
    [SerializeField] 
    private Animator gunAnim;
    //[SerializeField]
   // private float timeBetweenShots;
    public bool onShooting;
  //  public delegate void OnShoot();
   // public static OnShoot onShoot;

   // private Shake shake;
    private Vector2 moveDir;
    public Vector2 moveInput;
    //public int damage;
    private float lastFireTime;
    audioManager audioManager;
    [SerializeField]
    private float shotDelay;
    [Header("----------Reload gun----------")]
    public Image reloadIcon;
    //[SerializeField]
   // private float reloadTime;
    [SerializeField]
    private float reloadDelaying;
    [SerializeField]
    private bool isReloading;
    [SerializeField]
    private bool StartReload;
    public int currentClip;
    //public int maxClipSize;
    public int currentAmmo;
    public int maxAmmoSize;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }
    private void Start()
    {
        reloadIcon=GameObject.Find("Reloadicon").GetComponent<Image>();
        reloadDelaying=WeaponStats.reloadTime;
       // shake = GameObject.FindGameObjectWithTag("screenShake").GetComponent<Shake>();
    }

    public void OnLook(InputValue input){
        moveInput = input.Get<Vector2>();
    }

    public void OnReload(InputValue input){
        StartReload=true;
    }
    
    private void Update(){
        moveDir = moveInput.normalized;
        if(moveInput != Vector2.zero){
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            onShooting = true;
            //onShoot();
            aiming.SetBool("onAiming", true);
            gunAnim.SetFloat("MoveX", moveInput.x);
            gunAnim.SetFloat("MoveY", moveInput.y);
            if(currentClip > 0 && isReloading==false){
            float timeSinceLastFire= Time.time - lastFireTime;
                if (timeSinceLastFire >= WeaponStats.timeBetweenShots){
                    //shake.camShake();
                    StartCoroutine(fireBullet());
                    currentClip--;
                    lastFireTime = Time.time;
                    }
            }
        }else{
                aiming.SetBool("onAiming", false);
                onShooting = false;
            }

     if(StartReload && reloadDelaying<0){ 
        reloadIcon.fillAmount = 0;
        if(currentClip==WeaponStats.maxClipSize){
            isReloading=false;
            StartReload=false;
        }else{
        isReloading=true;
        reloadDelaying=WeaponStats.reloadTime;
        audioManager.playSFX(audioManager.Delayreload);
        gunAnim.SetTrigger("isReload");
        StartCoroutine(reloadDelay());
        }
        }if(reloadDelaying>0){
        reloadDelaying-=Time.deltaTime;
        }else if(currentClip == 0){
        isReloading=true;
        StartReload=true;
        reloadDelaying=WeaponStats.reloadTime;
        audioManager.playSFX(audioManager.Delayreload);
        gunAnim.SetTrigger("isReload");
        StartCoroutine(reloadDelay());
        }if(isReloading==true){
        StartReload=false;
        float fillAmount = reloadDelaying/WeaponStats.reloadTime; 
        reloadIcon.fillAmount=fillAmount;
        }
    
 }

    IEnumerator reloadDelay(){
    Debug.Log("reload");
    yield return new WaitForSeconds(WeaponStats.reloadTime);
    audioManager.playSFX(audioManager.reload);
    reloadIcon.fillAmount = 0;
    isReloading=false;
    int reloadAmount = WeaponStats.maxClipSize - currentClip;
    reloadAmount=(currentAmmo - reloadAmount) >= 0 ? reloadAmount : currentAmmo;
    currentClip+= reloadAmount;
    //currentAmmo-= reloadAmount;
 }

 public void AddAmmo(int ammoAmount){
    currentAmmo += ammoAmount;
    if(currentAmmo > maxAmmoSize){
        currentAmmo = maxAmmoSize;
    }
 }

    IEnumerator fireBullet(){
        yield return new WaitForSeconds(shotDelay);
        muzzleFlashAnim.SetTrigger("Shoot");
        audioManager.playSFX(audioManager.Shooting);
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, transform.rotation);
        Rigidbody2D rigidbody= bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = bulletSpeed * transform.up;
        bulletPrefab.GetComponent<bulletPlayer>().takesDamage = WeaponStats.Damage;
    }

    
       
    

   



    


  
}
