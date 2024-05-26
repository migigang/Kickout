using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public float shootingRange;
    public float fireRate = 2f;
    private float nextFireTime;

    public GameObject bullet;
    public GameObject bulletParent;

    private Transform player;

    [SerializeField] private AudioSource bulletSoundEffect;

    //float fireRate;
    //float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        //fireRate = 2f;
        //nextFire = Time.time;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckIfTimeToFire();
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate (bullet, bulletParent.transform.position, Quaternion.identity);
            bulletSoundEffect.Play();
            nextFireTime = Time.time + fireRate;
        }
        
    }

    //void CheckIfTimeToFire()
    //{
    //    if (Time.time > nextFire) {
    //        bulletSoundEffect.Play();
    //        Instantiate (bullet, transform.position, Quaternion.identity);
    //        nextFire = Time.time + fireRate;
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
