using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
     [SerializeField] playerData PlayerDatas;
    [SerializeField] List<playerBase> PlayerBases;
   
   
   private void Awake() {
     PlayerBases =new List<playerBase>();
   }

   private void Start() {
     AddPlayer(PlayerDatas);
   }

    public void AddPlayer(playerData PlayerData){
        playerBase Playerbase = GameObject.FindWithTag("Player").GetComponent<playerBase>();

        Playerbase.SetData(PlayerData);
        PlayerBases.Add(Playerbase);
    }

    internal void UpgradePlayer(upgradeData UpgradeData){
        playerBase playerToUpgrade = PlayerBases.Find(pd => pd.PlayerData == UpgradeData.PlayerData);
        playerToUpgrade.Upgrade(UpgradeData);
    }
}
