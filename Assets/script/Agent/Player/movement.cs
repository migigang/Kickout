using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;


public class movement : playerBase// MonoBehaviour
{
    //public float speed;
    [SerializeField]
    private Rigidbody2D myRigidbody;
    private Animator animator;
    public Vector2 moveInput;
    private TrailRenderer trailRenderer;
    public bool isDashButtonDown=false;
   // [SerializeField]
   // private float dashVelocity;
    [SerializeField]
    private float dashTime;
    public bool isCooldown=false;
    //[SerializeField]
    //private float startDashTime;
    [SerializeField]
    private coinManager cm;

    public Image dashIcon;
    [SerializeField] 
    private healthPlayer HealthPlayer;

    [Header("-------footstep----------")]
    [SerializeField]
    private float footstepDelay;
    private float footstepTimer;
    audioManager audioManager;
    [Header("-------atribut tambahan----------")]
    public bool isDead;
    public GameObject panelDead;
    public bool terkenaHit =false;
    float myFloat;
    Vector2 moveDir;
    bool isMoving;
    public float dashDuration;
    public bool isDash;
    [Header("-------joyStick----------")]
    [SerializeField] private Vector2 joystickSize = new Vector2(300, 300);
    [SerializeField] private FloatingJoystick joystick;
    private Finger movementFinger;
    [Header("-------knockback----------")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private float strength = 16f, delay=0.15f;
    [SerializeField] private bool knockbacked;



    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
        terkenaHit = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        dashTime -= Time.deltaTime; //PlayerStats.startDashTime;
        trailRenderer = GetComponent<TrailRenderer>();
        isDead=false;
        
    }

   // public void OnMove(InputValue input){
   //     moveInput = input.Get<Vector2>();
    //}
    public void OnDash(InputValue input){
        //moveInput = input.Get<Vector2>();
        if(isMoving){
        isDashButtonDown = true;  
        }
        
    }
    // Update is called once per frame
    void Update()
    {

        HealthPlayer.takesHealth = PlayerStats.health;
        HealthPlayer.takesNumOfHealths = PlayerStats.NumOfHealths;
       // moveDir = moveInput.normalized;
        if(isDash){
            return;
        }
        isMoving = Convert.ToBoolean(moveDir.magnitude);
        UpdateAnimationAndMove();
        if(isMoving){
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepDelay)
            {
                audioManager.playSFX(audioManager.footStep); 
                footstepTimer = 0f;
            }
           
        }
        if(terkenaHit){
            terkenaHit=false;
        }
    }
    void UpdateAnimationAndMove(){
           if(moveDir != Vector2.zero)
        {
            animator.SetFloat("moveX", moveDir.x);
            animator.SetFloat("moveY", moveDir.y);
            animator.SetBool("moving", true);
        }else{
            animator.SetBool("moving", false);
        }
    }
    

    void FixedUpdate(){
        if(isDash){
            return;
        }
        if (myRigidbody != null){
            if(!knockbacked){
            myRigidbody.MovePosition(myRigidbody.position + moveDir * PlayerStats.speed * Time.fixedDeltaTime);
            float Angle = Mathf.Atan2(moveDir.x , moveDir.y) * Mathf.Rad2Deg;
            float Smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref myFloat, 0.1f);
            //transform.rotation = Quaternion.Euler(0, Angle, 0);
            }

        }
            
        if (isDashButtonDown && dashTime<0) {
            Debug.Log("dash");
            audioManager.playSFX(audioManager.dash);
            dashTime = PlayerStats.startDashTime;
            //isDashButtonDown = false;
            trailRenderer.emitting = true;
             if (myRigidbody != null){
                StartCoroutine(Dash());
                
             dashIcon.fillAmount = 0;
             }
        }else if(isDashButtonDown == false){
            trailRenderer.emitting = false; 
        }

        if(dashTime>0){
             dashTime -= Time.deltaTime;
            isCooldown=true;
             
        }else{
            isCooldown=false;
        }
        
        if(isCooldown==true){
             isDashButtonDown = false;
             float fillAmount= dashTime/PlayerStats.startDashTime;
             dashIcon.fillAmount = fillAmount;
        }
            
        
        }

    private IEnumerator Dash(){
        isDash = true;
        animator.SetTrigger("dashMove");
        //myRigidbody.AddForce( new Vector2(moveDir.x * PlayerStats.dashVelocity, moveDir.y *PlayerStats.dashVelocity), ForceMode2D.Impulse); 
        myRigidbody.velocity = new Vector2(moveDir.x * PlayerStats.dashVelocity, moveDir.y *PlayerStats.dashVelocity);
        yield return new WaitForSeconds(dashDuration);
        isDash = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("BulletEnemy")) {
            SceneManager.LoadScene("GameOverScreen");
          //  DestroyCharacter();
        }
    }
    
    void DestroyCharacter(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Coin")){
            audioManager.playSFX(audioManager.coin); 
            Destroy(other.gameObject);
            cm.coinCount++;
        }
    }

    public Vector3 GetPosition() {
      return transform.position;
    }

    public void gotDamage(int damage){
        PlayerStats.health -=damage;
        Debug.Log("kenaa");
        terkenaHit = true;
        if (PlayerStats.health<1){
            panelDead.SetActive(true);
        }

    }

    
    private void OnEnable() {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandlerFingerMove;
    }


    private void OnDisable() {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandlerFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandlerFingerMove(Finger MovedFinger)
    {
        if(MovedFinger == movementFinger){
            Vector2 knobPosition;
            float maxMovement = joystickSize.x / 2f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            if(Vector2.Distance(
                currentTouch.screenPosition,
                joystick.RectTransform.anchoredPosition
                ) > maxMovement){
                    knobPosition = (
                        currentTouch.screenPosition - joystick.RectTransform.anchoredPosition
                    ).normalized
                    * maxMovement;
            }
            else{
                knobPosition = currentTouch.screenPosition - joystick.RectTransform.anchoredPosition;
            }

            joystick.Knob.anchoredPosition = knobPosition;
            moveDir = knobPosition / maxMovement;
        }
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if(LostFinger == movementFinger){
            movementFinger = null;
            joystick.Knob.anchoredPosition = Vector2.zero;
            joystick.gameObject.SetActive(false);
            moveDir = Vector2.zero;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if(movementFinger == null && TouchedFinger.screenPosition.x <= Screen.width/ 2f){
            movementFinger = TouchedFinger;
            moveDir = Vector2.zero;
            joystick.gameObject.SetActive(true);
            joystick.RectTransform.sizeDelta = joystickSize;
            joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
    }

    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        if(startPosition.x < joystickSize.x / 2){
            startPosition.x = joystickSize.x / 2;
        }
        if(startPosition.y < joystickSize.y /2){
            startPosition.y = joystickSize.y /2;
        }else if(startPosition.y > Screen.height - joystickSize.y / 2){
            startPosition.y = Screen.height - joystickSize.y / 2;
        }

        return startPosition;
    }

    public void knockBack(Transform t)
    {
        var dir = t.position - playerPos.position;
        knockbacked = true;
        myRigidbody.velocity = -dir.normalized * strength;
        StartCoroutine(unKnockback());
    }

    private IEnumerator unKnockback()
    {
        yield return new WaitForSeconds(delay);
        knockbacked=false;
    }
}
    
    
