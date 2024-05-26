using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet4 : MonoBehaviour
{
    public float shootingRange;

    [SerializeField] private AudioSource bulletSoundEffect;
    [SerializeField] private Cooldown cooldown;

    [SerializeField]
    private int bulletAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 bulletMoveDirection;
    private Transform player;

    public void Start(){
        InvokeRepeating("Update", 0f, 1f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= shootingRange)
        {
            bulletSoundEffect.Play();
            
            float angleStep = (endAngle - startAngle) / bulletAmount;
            float angle = startAngle;

            if (cooldown.IsCoolingDown) return;
        
            for (int i = 0; i < bulletAmount + 1; i++)
            {
                float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector2 bulDir = (bulMoveVector - transform.position).normalized;

                GameObject bul = PeluruPool.bulletPoolInstance.GetBullet();
                    bul.transform.position = transform.position;
                    bul.transform.rotation = transform.rotation;
                    bul.SetActive(true);
                    bul.GetComponent<bullet3>().SetMoveDirection(bulDir);

                angle += angleStep;
            }

            cooldown.StartCooldown();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    //private void Fire()
    //{
    //    float angleStep = (endAngle - startAngle) / bulletAmount;
    //    float angle = startAngle;

    //    if (cooldown.IsCoolingDown) return;
        
    //    for (int i = 0; i < bulletAmount + 1; i++)
    //    {
    //        float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
    //        float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

    //        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
    //        Vector2 bulDir = (bulMoveVector - transform.position).normalized;

    //        GameObject bul = PeluruPool.bulletPoolInstance.GetBullet();
    //            bul.transform.position = transform.position;
    //            bul.transform.rotation = transform.rotation;
    //            bul.SetActive(true);
    //            bul.GetComponent<bullet3>().SetMoveDirection(bulDir);

    //        angle += angleStep;
    //    }

    //    cooldown.StartCooldown();
    //}

    
}
