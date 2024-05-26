using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{

    public itemSlot[] ItemSlot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(string itemName, Sprite itemSprite){
        Debug.Log("itemName = " + itemName+ "itemSprite= "  + itemSprite);
        
        for (int i = 0; i< ItemSlot.Length; i++){
            if(ItemSlot[i].isFull==false){
                ItemSlot[i].AddItem(itemName,itemSprite);
                return;
            }
        }
    }

    public void deSelectAllSlots(){
        for(int i = 0; i< ItemSlot.Length; i++){
            //ItemSlot[i].selectedShader.SetActive(false);
            ItemSlot[i].itemSelected = false;
        }
    }
}
