using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    private Transform player;

    //[SerializeField]
   // private float startBetweenShots;
    [SerializeField]
    private float speed;
    //private float timeBetweenShots;

    private Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        Vector3 direction = player.transform.position - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
       // timeBetweenShots = startBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyMissile();
        }
    }

    private void DestroyMissile(){
        Destroy(gameObject);
   }
   private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")){
            DestroyMissile();
        }
        if(collision.CompareTag("obscale")){
            DestroyMissile();
        }
        if(collision.CompareTag("Bullet")){
            DestroyMissile();
        }
        if(collision.gameObject.TryGetComponent<movement>(out movement playerhp)){
            playerhp.gotDamage(1);
            DestroyMissile();
        }
    }
}
