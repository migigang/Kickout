using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace theLastHope
{
public class spesialShot : MonoBehaviour
{
    public const int SKILL_EMPTY = 0;
    public const int CIRCLE_SHOT = 1;
    public const int SPIRAL = 2;
    public const int DOUBLE_SPIRAL = 3;


    public int status;
    // Start is called before the first frame update
    void Start()
    {
        status = SKILL_EMPTY;
    }

    // Update is called once per frame
    void Update()
    {
         switch (status)
        {
            case SKILL_EMPTY:
            {
                gameObject.GetComponent<circleBullet>().StopShooting();
                gameObject.GetComponent<spiral>().StopShooting();
                gameObject.GetComponent<doubleSpiral>().StopShooting();
                break;
            }
            case CIRCLE_SHOT:
            {
                gameObject.GetComponent<circleBullet>().CanShooting();
                break;
            }
            case SPIRAL:
            {
                gameObject.GetComponent<spiral>().CanShooting();
                break;
            }
            case DOUBLE_SPIRAL:
            {
                gameObject.GetComponent<doubleSpiral>().CanShooting();
                break;
            }
        }
    }
}
}