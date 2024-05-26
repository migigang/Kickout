using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace theLastHope{
public class Controller
{
    public Model activeModel;
    public MCTS mcts;
    private float actionInterval = 0.1f; // Interval waktu antara pemanggilan aksi
    private float nextActionTime = 0.0f; // Waktu kapan aksi berikutnya dapat dipanggil

    public Controller(MCTS mcts){
        this.mcts = mcts;
    }

    public void listenMCTSAI()
    {
        if (Time.time >= nextActionTime)
        {
        activeModel.actionHandler(mcts.interact(),1);
        //Debug.Log("tes pang");
        nextActionTime = Time.time + actionInterval;
        }
    }
}
}
