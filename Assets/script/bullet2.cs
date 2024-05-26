using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theLastHope
{
public class bullet2 : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;

     private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("obscale")){
            Invoke("Destroy", 0.1f);
            //Destroy();
            //Destroy(gameObject);
        }
        if(collision.gameObject.TryGetComponent<movement>(out movement playerhp)){
            playerhp.gotDamage(1);
            Invoke("Destroy", 0.1f);
        }
 
        //private void OnEnable() {
       // Invoke("Destroy", 3f);
   // }
    }

         
    void Start()
    {
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir){
        moveDirection = dir;
    }
    
    private void Destroy() {
        gameObject.SetActive(false);
    }
    
    private void OnDisable() {
        CancelInvoke();
    }
}
}