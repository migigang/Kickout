using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuBuff : MonoBehaviour
{
    [SerializeField] 
    GameObject buffMenu;

    [SerializeField]
    List<upgradeButton> upgradeButtons;



    void Start()
    {
        // hideButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clean(){
        for (int i = 0; i < upgradeButtons.Count; i++){
            upgradeButtons[i].Clean();
        }
    }

    public void upgrade(int PressedUpgradeID){
        Debug.Log("playerPress:" + PressedUpgradeID.ToString());
        GetComponent<dungeonClear>().Upgrade(PressedUpgradeID);
        CloseMenuBuff();
    }

    public void OpenMenuBuff(List<upgradeData> upgradeDatas){
        Clean();
        Time.timeScale = 0f;
        buffMenu.SetActive(true);

        for(int i = 0; i < upgradeDatas.Count; i++){
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void CloseMenuBuff(){
        Time.timeScale = 1f;
        buffMenu.SetActive(false);
        for (int i = 0; i < upgradeButtons.Count; i++){
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }

}
