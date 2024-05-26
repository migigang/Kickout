using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redzone : MonoBehaviour
{
    [SerializeField]
    private GameObject Redzone;
   // [SerializeField]
  // private float startBetweenMissiles;

    private Transform player;
    private Vector2 target;
  //  private float timeBetweenMissiles;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    //    timeBetweenMissiles = startBetweenMissiles;
        
    }   
    
    private void Update() {
        //redzone();
    }
    
    public void redzoneArea()
    {
       //  if(timeBetweenMissiles <=0){
            Instantiate(Redzone, player.position, Quaternion.identity);
       //     timeBetweenMissiles = startBetweenMissiles;

       // }else{
       //     timeBetweenMissiles -= Time.deltaTime;
       // }
    }
}
