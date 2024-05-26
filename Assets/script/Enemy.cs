using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private Rigidbody2D rb;
    public float rotateSpeed = 0.0025f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!target) 
        {
            GetTarget();
        } else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
    }

    private void GetTarget () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}

    //private void Update() {
    //    if (!target) 
    //    {
    //        GetTarget();
    //    } else
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    //    }
        
    //}

    //private void GetTarget () {
    //    if (GameObject.FindGameObjectWithTag("Player")) {
    //        target = GameObject.FindGameObjectWithTag("Player").transform;
    //    }
    //}
