using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet2 : MonoBehaviour
{
    //[SerializeField] private Cooldown cooldown;
    public float shootingRange;

    private float angle = 0f;
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
            float bulDirX = transform. position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform. position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = PeluruPool.bulletPoolInstance.GetBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<bullet3>().SetMoveDirection(bulDir);

            angle += 5f;

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }


    //private void Fire()
    //{
        //if (cooldown.IsCoolingDown) return;

    //    float bulDirX = transform. position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
    //    float bulDirY = transform. position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

    //    Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
    //    Vector2 bulDir = (bulMoveVector - transform.position).normalized;

    //    GameObject bul = PeluruPool.bulletPoolInstance.GetBullet();
    //    bul.transform.position = transform.position;
    //    bul.transform.rotation = transform.rotation;
    //    bul.SetActive(true);
    //    bul.GetComponent<bullet3>().SetMoveDirection(bulDir);

    //    angle += 36f;

        //cooldown.StartCooldown();
    //}
}
