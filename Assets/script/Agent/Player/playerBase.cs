using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class playerBase : MonoBehaviour
{
   public playerData PlayerData;
   public playerStats PlayerStats;
   public int health;
    public int NumOfHealths;
    public float speed;
    public float startDashTime;
    public float dashVelocity;
    

    public virtual void SetData(playerData pd){
        PlayerData=pd;

        PlayerStats= new playerStats(pd.stats.health, pd.stats.NumOfHealths, pd.stats.speed, pd.stats.startDashTime, pd.stats.dashVelocity);
    }

    public void Upgrade(upgradeData UpgradeData){
        PlayerStats.Sum(UpgradeData.PlayerUpgradeStats);
    }
    
}
