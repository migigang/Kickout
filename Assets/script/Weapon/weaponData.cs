using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class weaponStats{
    public int Damage;
    public float timeBetweenShots;
    public int maxClipSize;
    public float reloadTime;

    public weaponStats(int Damage,float timeBetweenShots, int maxClipSize, float reloadTime)
    {
        this.Damage = Damage;
        this.timeBetweenShots = timeBetweenShots;
        this.maxClipSize = maxClipSize;
        this.reloadTime = reloadTime;
    }

    internal void Sum(weaponStats WeaponUpgradeStats){
        this.Damage += WeaponUpgradeStats.Damage;
        this.timeBetweenShots += WeaponUpgradeStats.timeBetweenShots;
        this.maxClipSize += WeaponUpgradeStats.maxClipSize;
        this.reloadTime += WeaponUpgradeStats.reloadTime;
    }
}
[CreateAssetMenu]
public class weaponData : ScriptableObject {
    public string Name;
    public weaponStats stats;
    public GameObject weaponBasePrefabs;
    public List<upgradeData> upgrades;
}