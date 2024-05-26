using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class room : MonoBehaviour
{
    public int width;
    public int height;
    public int X;
    public int Y;
    // Start is called before the first frame update
    void Start()
    {
        if(roomController.instance == null){
            Debug.Log("WrongScene");
            return;
        }
    }

    public Vector3 GetRoomCenter(){
        return new Vector3(X * width, Y * height);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width,height,0));
    }
}
