using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPool : MonoBehaviour
{
    public static bulletPool bulletPoolInstance;

    [SerializeField]
    private GameObject poolBullet;
    private bool notEnoughBulletInPool = true;

    private List<GameObject> bullets;
    

    private void Awake() {
    bulletPoolInstance = this;    
    }

    void Start()
    {
        bullets = new List<GameObject>();
    }

    public GameObject getBullet(){
        if (bullets.Count > 0){
            for(int i = 0; i < bullets.Count; i++){
                if(!bullets[i].activeInHierarchy){
                    return bullets[i];
                }
            }
        }
        if (notEnoughBulletInPool){
        GameObject bul = Instantiate(poolBullet);
        bul.SetActive(false);
        bullets.Add(bul);
        return bul;
    }
        return null;
    }
}
