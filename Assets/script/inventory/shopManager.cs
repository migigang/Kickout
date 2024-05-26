using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopManager : MonoBehaviour
{
    [SerializeField]
    private coinManager cm;
    [SerializeField]
    private inventoryManager InventoryManager;
    
    public itemShop[] ItemShop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(string itemName, Sprite itemSprite, int costOfItem){
        Debug.Log("nama item= " + itemName + " item Sprite= "  + itemSprite + " harga" + costOfItem);
        
        for (int i = 0; i< InventoryManager.ItemSlot.Length; i++){
            if(InventoryManager.ItemSlot[i].isFull==false){
                cm.coinCount -= costOfItem;
                InventoryManager.AddItem(itemName, itemSprite);
            return;
            }
        }
    }

    public void deSelectAllSlots(){
        for(int i = 0; i< ItemShop.Length; i++){
            ItemShop[i].selectedShader.SetActive(false);
            ItemShop[i].itemSelected = false;
        }
    }
}
