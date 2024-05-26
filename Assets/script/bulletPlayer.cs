using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using theLastHope;

public class bulletPlayer : weaponBase //MonoBehaviour
{

    public int takesDamage;
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("obscale")){
            DestroyBullet();
        }  
         else if(other.gameObject.TryGetComponent<bossHealth>(out bossHealth bossHealthComponent)){
           bossHealthComponent.TakeDamage(takesDamage);
            DestroyBullet();
         }
         if(other.gameObject.TryGetComponent<EnemyHealthBar>(out var EnemyHealthBar)){
            EnemyHealthBar.TakeDamages(takesDamage);
            DestroyBullet();
        }  
     }

    void DestroyBullet(){
        Destroy(gameObject);
    }

}


   // private void OnTriggerEnter2D(Collider2D other) {
        
   //     if(other.gameObject.CompareTag("obscale")){
   //         DestroyBullet();
   //     }  
   //      else if(other.gameObject.TryGetComponent<bossHealth>(out bossHealth bossHealthComponent)){
   //         bossHealthComponent.TakeDamage(251);
    //        DestroyBullet();
   //      }

   //      if(other.gameObject.CompareTag("Enemy")){
   //         DestroyBullet();
    //    }  
   //  }
    