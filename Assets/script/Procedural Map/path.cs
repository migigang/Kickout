using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{
    public string targetTag = "Target"; // Tag dari target

    void Update()
    {
        // Temukan objek dengan tag target
        GameObject target = GameObject.FindWithTag(targetTag);

        // Jika objek ditemukan
        if (target != null)
        {
            // Hitung arah dari objek ini ke target
            Vector2 direction = target.transform.position - transform.position;

             // Hitung sudut yang akan membuat objek ini menghadap target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Terapkan rotasi ke objek ini
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
