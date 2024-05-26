using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class weaponBase : MonoBehaviour
{
    public weaponData WeaponData;
    public weaponStats WeaponStats;
    public int Damage;
    public int maxClipSize;
    public float reloadTime;
    public float timeBetweenShots;


    public void Update() {
        
    }
    public virtual void SetData(weaponData wd){
        WeaponData = wd;

        WeaponStats= new weaponStats(wd.stats.Damage, wd.stats.timeBetweenShots, wd.stats.maxClipSize, wd.stats.reloadTime);

    }

    public void Upgrade(upgradeData UpgradeData){
        WeaponStats.Sum(UpgradeData.WeaponUpgradeStats);
    }
}
