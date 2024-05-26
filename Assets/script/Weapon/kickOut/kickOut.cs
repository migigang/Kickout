using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class kickOut : MonoBehaviour
{
   
   public static void Create(Transform prefabs, Vector3 spawnPosition, Vector3 flyDir){
   Transform kickOuts = Instantiate(prefabs, spawnPosition, Quaternion.identity);
   kickOut KickOut = kickOuts.gameObject.AddComponent<kickOut>();
   KickOut.Setup(flyDir);
   }
   private Vector3 flyDir;
   private float timer;
   private float eulerZ;
   private void Setup(Vector3 flyDir){
    this.flyDir = flyDir;
    transform.localScale = Vector3.one * 2f;
    eulerZ = 0f;
   }

   private void Update() {
      float flySpeed = 50f;
    transform.position += flyDir * flySpeed * Time.deltaTime;
    float scaleSpeed = 3f;
    transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
    float eulerSpeed = 360f  * 4f;
    eulerZ += eulerSpeed * Time.deltaTime;
    transform.localEulerAngles = new Vector3(0, 0, eulerZ);

    timer+=Time.deltaTime;
    if(timer >=1f){
      //Destroy(gameObject);
    }
   }



}
