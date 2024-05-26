using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet5 : MonoBehaviour
{
    [SerializeField] private AudioSource exploSoundEffect;

    public GameObject tower;
    public GameObject target;

    public float speed = 10f;

    private float towerX;
    private float targetX;

    private float dist;
    private float nextX;
    private float baseY;
    private float height;

    void Start()
    {
        tower = GameObject.FindGameObjectWithTag("Senjata");
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        towerX = tower.transform.position.x;
        targetX = target.transform.position.x;

        dist = targetX - towerX;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        baseY = Mathf.Lerp(tower.transform.position.y, target.transform.position.y, (nextX - towerX) / dist);
        height = 2 * (nextX - towerX) * (nextX - targetX) / (-0.25f * dist * dist);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;

        if(transform.position == target.transform.position)
        {
            exploSoundEffect.Play();
            Destroy(gameObject);
        }
    }

    public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0,0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }


     private void OnCollisionEnter2D(Collision2D collision) {

        if(collision.gameObject.TryGetComponent<movement>(out movement playerhp)){
            playerhp.gotDamage(1);
            DestroyBomb();
        }

        if(collision.gameObject.CompareTag("obscale")){
            DestroyBomb();
        }
        if(collision.gameObject.CompareTag("Bullet")){
            DestroyBomb();
        }
    }

    void DestroyBomb(){
        Destroy(gameObject);
    }
}
