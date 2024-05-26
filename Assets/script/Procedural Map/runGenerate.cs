using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runGenerate : MonoBehaviour
{
    public abstrakDungeonGenerator dungeonGenerator;

    void Start()
    {
        dungeonGenerator.GenerateDungeon();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
