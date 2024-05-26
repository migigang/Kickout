using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;
     public int MaxHp => maxHealth;
    
    private Shake shake;

    [SerializeField]
    HealthBarFloating healthBar;

    public bool isFinishing;

    public int Hp{
        get => health;
        private set {
            var isDamage = value < health;
            var healthChangeData = new HealthChangeArgs{
                newValue = Mathf.Clamp(value, 0, maxHealth),
                oldValue = health,
                attemptedChange = value - health,
            };
            health = healthChangeData.newValue;

            if(isDamage){
                Damaged?.Invoke(healthChangeData);
            }

            if(health <= 0){
                Died?.Invoke();
                Debug.Log("EnemyDead");
            }
        }
    }

    public UnityEvent<HealthChangeArgs> Healed;
    public UnityEvent<HealthChangeArgs> Damaged;
    public UnityEvent Died;



    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("screenShake").GetComponent<Shake>();
        health = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamages(int DamageAmount){
        Hp -= DamageAmount;
        healthBar.UpdateHealthBar(Hp,maxHealth);
        shake.camShake();
    }

    public void Heal(int amount){
        Hp += amount;
    }

    public void healthFull(){
        Hp=maxHealth;
    }

    public void dead(){
        Hp=0;
    }


    public void Adjust(int value){
        Hp=value;
    }

      private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.TryGetComponent<movement>(out var player)){
            player.gotDamage(1);
            player.knockBack(transform);

        }  
      }
      
      public void healthbars(){
        healthBar = GetComponentInChildren<HealthBarFloating>();
        healthBar.UpdateHealthBar(health,maxHealth);
      }



    public Vector3 GetPosition() {
      return transform.position;
    }

      public void eliminated(){
         isFinishing=true;
      }
      






}
