using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class laserSight : MonoBehaviour
{

    public LayerMask layerToHit;
    private Vector2 moveDir;
    public Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnLook(InputValue input){
        moveInput = input.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = moveInput.normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, 50f, layerToHit);
        if (hit.collider != null)
        {
            transform.localScale = new Vector3(transform.localScale.x, hit.distance , 1);
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
        }else{
         transform.localScale = new Vector3(transform.localScale.x, 50f, 1);
        }

    }
}
