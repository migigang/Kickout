using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeonClear : MonoBehaviour
{
    menuBuff MenuBuff;

    [Header("----------UpgradeData------------")]
    [SerializeField]
    List<upgradeData> upgrades;
    [Header("----------Selected------------")]
    [SerializeField]
    List<upgradeData> selectedUpgrades;
    [Header("----------acquiredUpgrade------------")]
    [SerializeField]
    List<upgradeData> acquiredUpgrades;

    weaponManager WeaponManager;
    PlayerManager playerManager;
    
    private void Awake() {
        WeaponManager = GetComponent<weaponManager>();
        playerManager = GetComponent<PlayerManager>();
    }
    void Start()
    {
        MenuBuff = GetComponent<menuBuff>();
        if(selectedUpgrades == null){selectedUpgrades = new List<upgradeData>();}
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(3));
        Debug.Log("Buff Menu!");
        MenuBuff.OpenMenuBuff(selectedUpgrades);
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<upgradeData> GetUpgrades(int count){
        List<upgradeData> upgradesList = new List<upgradeData>();
        
        if(count > upgrades.Count){
            count = upgrades.Count;
        }

        for (int i = 0; i < count; i++){
        upgradesList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }

        return upgradesList;
    }
    public void Upgrade(int SelectedUpgradeID){
        upgradeData UpgradeData = selectedUpgrades[SelectedUpgradeID];
        if(acquiredUpgrades == null){acquiredUpgrades = new List<upgradeData>();}

        switch(UpgradeData.upgradeType){
            case UpgradeType.HealthUpgrade:
                playerManager.UpgradePlayer(UpgradeData);
                break;
            case UpgradeType.damageUpgrade:
                WeaponManager.UpgradeWeapon(UpgradeData);
                break;
            case UpgradeType.fireRateUpgrade:
                WeaponManager.UpgradeWeapon(UpgradeData);
                break;
            case UpgradeType.ammoUpgrade:
                WeaponManager.UpgradeWeapon(UpgradeData);
                break;
            case UpgradeType.reloadSpeedUpgrade:
                WeaponManager.UpgradeWeapon(UpgradeData);
                break;
            case UpgradeType.movementSpeedUpgrade:
                playerManager.UpgradePlayer(UpgradeData);
                break;
            case UpgradeType.cdDashUpgrade:
                playerManager.UpgradePlayer(UpgradeData);
                break;
            case UpgradeType.dashVelocityUpgrade:
                playerManager.UpgradePlayer(UpgradeData);
                break;
        }

        acquiredUpgrades.Add(UpgradeData);
        upgrades.Remove(UpgradeData);
    }

}
