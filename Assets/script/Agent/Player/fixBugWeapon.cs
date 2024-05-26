using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixBugWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject weapons;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       weapons = GameObject.FindWithTag("Weapon");
    }

    
    public void SetResume(){
        weapons.SetActive(false);
        weapons.SetActive(true);
    }
}
