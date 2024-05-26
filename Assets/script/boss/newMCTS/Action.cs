
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;


namespace theLastHope{

public class GameSimul{

    public static bool isFinished = false;

    public static int finalSituation = -1; // 0 = gameover; 1 = win
    
    public static int TouchAdv = -1; // 0 = jika player tidak terkena hit; 1 = jika player terkena hit
    
    public static int TouchME = -1; // 0 = jika npc boss tidak terkena hit; 1 = jika npc boss terkena hit   

    public static int[] ppMe = new int[4], ppAdv = new int[4];

    public static Model copymodel = null;
    

 

    public static void Reset(){
        TouchAdv = 0;
        TouchME = 0;
        isFinished = false;
        finalSituation = -1;
      
    }

    public static void PlayAction(Node state){

        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player"); 
    GameObject bossGameObject = GameObject.FindGameObjectWithTag("Boss");
        //if (Time.time >= nextActionTime) {
        Debug.Log("Pilih Aksi");
        // memilih aksi yang disimulasikan
       copymodel.actionHandler(state.state,1);
       
       //melakukan aksi random
       State action = (State)Random.Range(0, 5);
       copymodel.actionHandler(action,0); 
       
       movement player = playerGameObject.GetComponent<movement>();
       bossHealth boss = bossGameObject.GetComponent<bossHealth>();
       if (player.health <= 0 && boss.currentHealth <= 0)
       {
            Debug.Log("tidak ada yang mati");
           finalSituation = 2; //tidak ada yang mati
           isFinished = true;
       }else if(player.health <= 0){//jika player dikalahkan
            finalSituation = 1;
            isFinished = true;
       }
       
        else if(boss.currentHealth <=0){ //jika boss dikalahkan
            finalSituation = 0;
            isFinished = true;
        }

        if(player.terkenaHit == true){   //jika boss mengenai player
            Debug.Log("kena hit");
            TouchAdv = 1;
        }else{
             TouchAdv = 0;
        }
        if(boss.bossTerkenaHit == true){
            Debug.Log("boss kena hit");
            TouchME = 1;   
        }else{
            TouchME=0;
        }
        
        
    }

    public static System.Array GetNextPossibleAction(Node n){ //mengembalikan kemungkinan tindakan yang dilakukan
        Debug.Log("mengembalikan kembali tindakan");
        return State.GetValues(typeof(State));
    }

    public static object GetRandomAction(System.Array actions){
        Debug.Log("melakukan aksi random");
        System.Random rand = new System.Random();
        int i = 0;
        if(i >= 1){
            return State.wait;
        }else{
            return actions.GetValue(rand.Next(actions.Length-1));
        }

    }
}

public struct Register{
    public int a;
    public int b;

    public Register(int a,int b){
        Debug.Log("register");
        this.a = a;
        this.b = b;
    }
}


}
