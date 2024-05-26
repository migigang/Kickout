using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class cutScene : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private double initialTime;

    private void Start()
    {
        director.Play();
    }
}
