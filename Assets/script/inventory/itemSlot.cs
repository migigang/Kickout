using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class itemSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // item data
    public string itemName;
    public Sprite itemSprite;
    public bool isFull;

    //item slot
    [SerializeField]
    private Image itemImage;

    //selected
    public bool itemSelected;
    private inventoryManager InventoryManager;

     //drag
    private RectTransform rectTransform;
    [SerializeField]
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Start() {
    InventoryManager = GameObject.Find("gameManager").GetComponent<inventoryManager>();
    }

     private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }    

    public void AddItem(string itemName, Sprite itemSprite){
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        isFull= true;

        itemImage.sprite = itemSprite;
    }


    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("onbegin");
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
    }
    
    public void OnDrag(PointerEventData eventData){
        Debug.Log("ondrag");
        rectTransform.anchoredPosition += eventData.delta / rectTransform.lossyScale;
    }

    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("onenddrag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("infomazeee");
    }



   
}
