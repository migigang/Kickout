using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void bulletShot(Vector2 position, Quaternion rotation){
        Instantiate(bulletPrefab, position, Quaternion.identity);
    }
}
