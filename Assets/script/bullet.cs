using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Transform player;
    private Vector2 target;
    private Rigidbody2D myRigidBody;

    void Start() {
        myRigidBody=GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        target = new Vector2(player.position.x, player.position.y);
        Vector3 direction = player.transform.position - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void Update(){
        
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyBullet();
        }
    }

 

    private void DestroyBullet(){
        Destroy(gameObject);
    }

      private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.CompareTag("obscale")){
            DestroyBullet();
        }
         if(collision.gameObject.TryGetComponent<movement>(out movement playerhp)){
            playerhp.gotDamage(1);
            DestroyBullet();
        }
    }


}
