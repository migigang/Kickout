using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redzoneTime : MonoBehaviour
{
    [SerializeField]
    private float timeRedzone = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeRedzone+=1f;
        if(timeRedzone >= 5f){
            timeRedzone=0f;
            Destroy(gameObject);
        }
    }
}
