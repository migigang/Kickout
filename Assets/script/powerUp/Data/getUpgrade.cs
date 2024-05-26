using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getUpgrade : MonoBehaviour
{

     [Header("----------UpgradeData------------")]
    [SerializeField]
    List<upgradeData> upgrades;

    
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
}
