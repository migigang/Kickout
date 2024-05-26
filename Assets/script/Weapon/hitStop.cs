using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitStop : MonoBehaviour
{
    private float speed;
    private bool restoreTime;


    // Start is called before the first frame update
    private void Start()
    {
        restoreTime = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(restoreTime){
            if(Time.timeScale < 1f){
                Time.timeScale += Time.deltaTime * speed;
            }else{
                Time.timeScale = 1f;
                restoreTime = false;
            }
        }
    }


    public void StopTime(float changeTime, int restoreSpeed, float delay){
        speed = restoreSpeed;

        if(delay > 0){
            StopCoroutine(startTimeAgain(delay));
            StartCoroutine(startTimeAgain(delay));
        }else{
            restoreTime= true;
        }

        Time.timeScale = changeTime;


    }

    IEnumerator startTimeAgain(float amt){
        restoreTime = false;
        yield return new WaitForSeconds(amt);
    }



}
