using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    // Start is called before the first frame update
    void Start()
    {
        //playerShootAim.onShoot +=ChangeCam;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position != target.position){
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);   
        }
    }
   


}











// [SerializeField]
   // private Transform aim;
    //public string objectTag = "Weapon";
   // public playerShootAim onAim;



//if(aim !=null && onAim.onShooting){
        //if(onAim.onShooting && transform.position != aim.position){
           // Debug.Log("oiii");
        //    Vector3 aimPosition = new Vector3(aim.position.x, aim.position.y, transform.position.z);
        //    aimPosition.x = Mathf.Clamp(aimPosition.x, minPosition.x, maxPosition.x);
        //    aimPosition.y = Mathf.Clamp(aimPosition.y, minPosition.y, maxPosition.y);
          //  transform.position = Vector3.Lerp(transform.position, aimPosition, smoothing);
       // }

       // void ChangeCam(){
    //    aim=GameObject.Find("aim").transform;
    //    onAim=GameObject.FindWithTag(objectTag).GetComponent<playerShootAim>();
   // }