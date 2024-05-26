using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet3 : MonoBehaviour
{
    public float shootingRange;

    private float angle = 0f;

    private Vector2 bulletMoveDirection;
    private Transform player;

    void Start()
    {
        InvokeRepeating("Update", 0f, 0.1f);
         player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= shootingRange)
        {
           for (int i = 0; i <= 1; i++)
            {
                float bulDirX = transform.position.x + Mathf.Sin(((angle + 180f * i) * Mathf.PI) / 180f);
                float bulDirY = transform.position.y + Mathf.Cos(((angle + 180f * i) * Mathf.PI) / 180f);

                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector2 bulDir = (bulMoveVector - transform.position).normalized;

                GameObject bul = PeluruPool.bulletPoolInstance.GetBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<bullet3>().SetMoveDirection(bulDir);
            }

            angle += 10f;
        
            if (angle >= 360f)
            {
                angle = 0f;
            } 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    //private void Fire()
    //{
    //    for (int i = 0; i <= 1; i++)
    //    {
    //        float bulDirX = transform.position.x + Mathf.Sin(((angle + 180f * i) * Mathf.PI) / 180f);
    //        float bulDirY = transform.position.y + Mathf.Cos(((angle + 180f * i) * Mathf.PI) / 180f);

    //        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
    //        Vector2 bulDir = (bulMoveVector - transform.position).normalized;

    //        GameObject bul = PeluruPool.bulletPoolInstance.GetBullet();
    //        bul.transform.position = transform.position;
    //        bul.transform.rotation = transform.rotation;
    //        bul.SetActive(true);
    //        bul.GetComponent<bullet3>().SetMoveDirection(bulDir);
    //    }

    //    angle += 36f;
        
    //    if (angle >= 360f)
    //    {
    //        angle = 0f;
    //    }
    //}
}
