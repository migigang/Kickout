using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet1 : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    private float speed;

    private Vector2 target;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyBomb();
        }
    }

    private void DestroyBomb(){
        Destroy(gameObject);
   }
}
