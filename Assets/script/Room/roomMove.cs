using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomMove : MonoBehaviour
{
    public Vector2 maxCameraChange;
    public Vector2 minCameraChange;
    public Vector3 playerChange;
    private cameraMovement cam;
    
    void Start()
    {
        cam = Camera.main.GetComponent<cameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            cam.minPosition += minCameraChange;
            cam.maxPosition += maxCameraChange;
            other.transform.position += playerChange;
        }   
    }
}
