using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible to sync the unity environment to the Model class
namespace theLastHope{

public class View
{
    // The dictionaries and lists contains the unity gameObjects
    private GameObject AgentsObject;
    private Dictionary<int,GameObject> AgentsObjectDict;

    //private GameObject bombObject;
   // private Dictionary<int,GameObject> bombObjectDict;
    
    //private GameObject wallObject;
   // private List<GameObject> wallObjectList;
    
   // private GameObject breakableObject;
  //  private Dictionary<Vector2, GameObject> breakableObjectDict;

  //  private GameObject fireObject;

  //  private P2Input mode;
    // TEMPORARY VARIABLES
  //  private Dictionary<int, Bomb> tempDict;
    //private List<int> idList;
  //  private Map tempMap;
   // private List<Vector2> tempVectorList;

    // View constructor
    public View(Dictionary<string, object> gameState, GameObject boss)
    {
        // We add the prefab so it can be generated
        AgentsObject = boss;
        AgentsObjectDict = new Dictionary<int, GameObject>();

       
        // For each player from Model we instantiate a new Player model
        
        foreach (Boss bossInfo in (IEnumerable) gameState["AgentsInfo"])
        {
            if (AgentsObjectDict.ContainsKey(bossInfo.BossID))
            {
                 AgentsObjectDict[bossInfo.BossID].transform.position =
                    new Vector2(bossInfo.position.x, bossInfo.position.y);
            }else{
            GameObject newBoss = null;
            newBoss = AgentsObject;
            newBoss.transform.position = new Vector2(bossInfo.position.x, bossInfo.position.y);
            newBoss.name = bossInfo.BossID.ToString();
            AgentsObjectDict.Add(bossInfo.BossID, newBoss);

            }
            
        }
        // Visual Map generation
      //  tempMap = (Map) gameState["MapInfo"];
      //  for (int i=0;  i < tempMap.mapSizeX; i++)
     //   {
       //     for (int j = 0;  j<tempMap.mapSizeY; j++)
       //     {
       //         if (tempMap.myMapLayout[i,j] == MapEnvironment.Wall)
       //         {
       //             wallObjectList.Add(GameObject.Instantiate(wallObject, new Vector3(i, 0, j), Quaternion.identity));
       //         }
       //         else if (tempMap.myMapLayout[i,j] == MapEnvironment.Breakable)
       //         {
       //             breakableObjectDict.Add(new Vector2(i,j),GameObject.Instantiate(breakableObject,new Vector3(i,0,j),Quaternion.identity));
       //         }
       //     }
        //}
    }
    
    // Update every model with the positions from Model
    public void UpdateView(Dictionary<string, object> gameState)
    {
        
        // Update Players Position
        foreach (Boss bossInfo in (IEnumerable) gameState["AgentsInfo"])
        {
            AgentsObjectDict[bossInfo.BossID].transform.position =
                new Vector2(bossInfo.position.x, bossInfo.position.y);
        }
        
        // Update the bombs state and display an explosion if exploding
     //   foreach (KeyValuePair<int,Bomb> bombItem in (IEnumerable) gameState["BombsInfo"])
       // {
            // if new bomb add
       //     if (!bombObjectDict.ContainsKey(bombItem.Key))
       //     {
      //          GameObject newBomb = GameObject.Instantiate(bombObject);
      //          newBomb.transform.position = new Vector3(bombItem.Value.position.x, 0, bombItem.Value.position.y);
       //         bombObjectDict.Add(bombItem.Key,newBomb);
       //     }
            
            // Create an explosion
      //      if (bombItem.Value.exploding)
      //      {
       //         foreach (Vector2 explosionPosition in bombItem.Value.explosionSquares)
      //          {
       //             GameObject.Instantiate(fireObject, new Vector3(explosionPosition.x, 0, explosionPosition.y),
      //                  Quaternion.identity);
       //         }
        //        GameObject.Destroy(bombObjectDict[bombItem.Key]);
        //        bombObjectDict.Remove(bombItem.Key);
                
        //        SoundManager.Instance.PlaySoundEffectBoom();
       //     }
      //  }
        
        // Update breakable environment
       // tempMap = (Map) gameState["MapInfo"];
       // tempVectorList = new List<Vector2>(breakableObjectDict.Keys);
      //  foreach (Vector2 breakPos in tempVectorList)
     //   {
     //       if (tempMap.myMapLayout[(int) breakPos.x, (int) breakPos.y] == MapEnvironment.Empty)
      //      {
      //          GameObject.Destroy(breakableObjectDict[breakPos]);
      //          breakableObjectDict.Remove(breakPos);
      //      }
      //  }

    }
}



}
