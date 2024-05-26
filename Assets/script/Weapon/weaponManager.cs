using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer;

    [SerializeField] weaponData startingWeapon;
   // [SerializeField] playerData PlayerDatas;
   // [SerializeField]
   // List<playerBase> PlayerBases;
    [SerializeField]
    List<weaponBase> Weapons;

    private void Awake() {
        Weapons = new List<weaponBase>();
       // PlayerBases =new List<playerBase>();
    }


    private void Start() {
        AddWeapon(startingWeapon);
        //AddPlayer(PlayerDatas);
    }

    public void AddWeapon(weaponData WeaponData){
        GameObject weaponGameObject = Instantiate(WeaponData.weaponBasePrefabs, weaponObjectsContainer);

        weaponBase WeaponBase = weaponGameObject.GetComponent<weaponBase>();

        WeaponBase.SetData(WeaponData);
        Weapons.Add(WeaponBase);
    }

   // public void AddPlayer(playerData PlayerData){
    //    playerBase Playerbase = GameObject.FindWithTag("Player").GetComponent<playerBase>();

    //    Playerbase.SetData(PlayerData);
    //    PlayerBases.Add(Playerbase);
    //}

    internal void UpgradeWeapon(upgradeData UpgradeData){
       weaponBase weaponToUpgrade = Weapons.Find(wd=> wd.WeaponData == UpgradeData.WeaponData);
       weaponToUpgrade.Upgrade(UpgradeData);
    }

    //internal void UpgradePlayer(upgradeData UpgradeData){
   //     playerBase playerToUpgrade = PlayerBases.Find(pd => pd.PlayerData == UpgradeData.PlayerData);
   //     playerToUpgrade.Upgrade(UpgradeData);
   // }
}
