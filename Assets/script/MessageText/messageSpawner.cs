using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public class messageSpawner: MonoBehaviour
{
    [SerializeField] private Vector2 initialPosition;

    [SerializeField] private GameObject messagePrefabs;

    public void spawnMessage(string msg){
        var msgObj = Instantiate(messagePrefabs, GetSpawnPoint(), Quaternion.identity);
        var inGameMessage = msgObj.GetComponent<IInGameMessage>();
        inGameMessage.SetMessage(msg);
    }

    private Vector3 GetSpawnPoint(){
        return transform.position + (Vector3) initialPosition;
    }
    
}
