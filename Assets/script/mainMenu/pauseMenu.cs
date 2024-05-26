using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume(){
        Time.timeScale = 1f;
    }
    public void Pause(){
        Time.timeScale = 0f;
    }

    
}
