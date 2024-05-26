using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType{
    HealthUpgrade,
    damageUpgrade,
    fireRateUpgrade,
    ammoUpgrade,
    reloadSpeedUpgrade,
    movementSpeedUpgrade,
    cdDashUpgrade,
    dashVelocityUpgrade,
}
[CreateAssetMenu]
public class upgradeData : ScriptableObject
{
    public UpgradeType upgradeType;
    public string Name;
    public Sprite icon;
    public string desc;
    [Header("-------Weapon Statistic----------")]
    public weaponData WeaponData;
    public weaponStats WeaponUpgradeStats;
    [Header("-------Player Statistic----------")]
    public playerData PlayerData;
    public playerStats PlayerUpgradeStats;


}
