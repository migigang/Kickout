using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class playerStats{
    public int health;
    public int NumOfHealths;
    public float speed;
    public float startDashTime;
    public float dashVelocity;

    public playerStats(int health, int NumOfHealths, float speed, float startDashTime, float dashVelocity)
    {
        this.health = health;
        this.NumOfHealths = NumOfHealths;
        this.speed = speed;
        this.startDashTime = startDashTime;
        this.dashVelocity = dashVelocity;
    }

    internal void Sum(playerStats PlayerUpgradeStats){
        this.health += PlayerUpgradeStats.health;
        this.NumOfHealths += PlayerUpgradeStats.NumOfHealths;
        this.speed += PlayerUpgradeStats.speed;
        this.startDashTime += PlayerUpgradeStats.startDashTime;
        this.dashVelocity += PlayerUpgradeStats.dashVelocity;
    }
}
[CreateAssetMenu]
public class playerData : ScriptableObject
{
    public string Name;
    public playerStats stats;
    //public GameObject playerBasePrefabs;
    public List<upgradeData> upgrades;
}
