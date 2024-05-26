using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theLastHope{

public class BossHelper
{
    public int SKILL_START_INDEX;
	public int SHOT_INDEX;
	public int MOVE_INDEX;
	public int SPESIAL_SHOT_INDEX;
	public bool[] allActions;


    
    public BossHelper(bossHealth boss){
        this.allActions = new bool[3 + 1 + 1 + 2];
		this.SKILL_START_INDEX = 0;
		this.SHOT_INDEX = this.SKILL_START_INDEX + 3;
		this.MOVE_INDEX = this.SHOT_INDEX + 1;
		this.SPESIAL_SHOT_INDEX = this.MOVE_INDEX + 1;


    }

    public int FillAvailableActions(bossHealth boss, bossHealth player){
        int availableCount=0;
      //  float distance = bossHealth.GetDistance(boss,player);
        bool [] myActions =  allActions;
        for (int i = 0; i < myActions.Length; i++)
		{
            if(!boss.IsAvailable() || boss.currentHealth > 0){
                allActions[SKILL_START_INDEX + i] = false;
            }else{
                allActions[SKILL_START_INDEX + i] = true;
                }
        }

        if(!boss.IsAvailable()|| boss.isShooting){
            allActions[SHOT_INDEX]= false;
        }else{
            Debug.Log("ngapain ini");
            allActions[SHOT_INDEX] = true;
            availableCount++;
        }

        if(!boss.IsAvailable() || availableCount > 0){
            allActions[MOVE_INDEX] = false;
        }else{
            allActions[MOVE_INDEX] = true;
            availableCount++;
        }

       // if(!boss.IsAvailable() || distance < 8){
      //      allActions[SPESIAL_SHOT_INDEX]= false;
     //   }else{
      //      allActions[SPESIAL_SHOT_INDEX] = true;
      //      availableCount++;
     //   }
        return availableCount;
    }

    public void MakeAction(bossHealth boss, bossHealth player, int action)
    {
        if(action ==-1)
        {
            return;
        }

        if(action >= SKILL_START_INDEX && action < SHOT_INDEX)
        {
            //nantinya shield
           // boss.walking();
            //onAction();
            boss.goAway();
            Debug.Log("shield");
        }
        else if (action==SHOT_INDEX){
           //boss.walking();
            //onAction();
                        boss.goAway();
            Debug.Log("laser");
        }
        else if(action== MOVE_INDEX){
            //boss.walking();
            //UnityMainThreadDispatcher.Instance().Enqueue(boss.walking());
            //onAction();
            Debug.Log("maju");
        }else if(action== SPESIAL_SHOT_INDEX){
           //boss.walking();
            //onAction();
            boss.goAway();
            Debug.Log("spesial skill");
        }
    }
}
}
