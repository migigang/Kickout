using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class damageIndicatorSpawner : messageSpawner
{
    public void spawnMessage(HealthChangeArgs args){
        spawnMessage(Mathf.Abs(args.ActualChange).ToString());
    }
}
