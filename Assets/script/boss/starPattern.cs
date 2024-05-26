using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starPattern : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int numBullets = 5;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private GameObject bulletPrefab;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delay)
        {
            timer = 0f;

            for (int i = 0; i < numBullets; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                float angle = i * (360f / numBullets) + (360f / numBullets) / 2f;
                Vector2 bulletDirection = Quaternion.Euler(0f, 0f, angle) * Vector2.up;
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
            }

           // transform.Rotate(Vector3.forward * rotationSpeed);
        }
    }
}
