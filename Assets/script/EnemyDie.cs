using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDie : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision){
       if(collision.gameObject.CompareTag("Bullet")){
            DestroyEnemy();
        }  
    }

    void DestroyEnemy(){
        Destroy(gameObject);
    }
}
