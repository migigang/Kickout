using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class itemShop : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public Sprite sprite;
    [TextArea]
    [SerializeField]
    private string itemDescription;

    //itemdesc
    public TMP_Text itemDescName;
    public TMP_Text itemDescText;
    
    //itemcos
    [SerializeField]
    private coinManager cm;
    //[SerializeField]
   // private GameObject buyBtn;

    [SerializeField]
    private int costOfItem;

    //selected
    public GameObject selectedShader;
    public bool itemSelected;
    private shopManager ShopManager;


    void Start()
    {
        ShopManager = GameObject.Find("ShopItemSlot").GetComponent<shopManager>();
        itemDescName.text = itemName;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buyItem(){
        if(cm.coinCount >= costOfItem){
            Debug.Log("coin cukup");
           // buyBtn.SetActive(true);
            ShopManager.AddItem(itemName, sprite, costOfItem);
        } else{
            Debug.Log("coin kurs");
          //  buyBtn.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        if(eventData.button==PointerEventData.InputButton.Left){
            OnLeftClick();
        }
        if(eventData.button==PointerEventData.InputButton.Right){
            OnRightClick();
        }
    }

    public void OnLeftClick(){
       // ShopManager.deSelectAllSlots();
        selectedShader.SetActive(true);
        itemSelected = true;
        itemDescText.text = itemDescription;
    }

    public void OnRightClick(){
        
    }
}
