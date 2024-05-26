using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theLastHope{


public class hideScript : MonoBehaviour
{
    public GameObject MCTShide;
    void Start()
    {
        MCTShide.GetComponent<App>().enabled =false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}

