using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class upgradeButton : MonoBehaviour
{
    [SerializeField]
    Image icon;

    public TMP_Text BuffDescName;
    public TMP_Text BuffDescText;

    private void Start() {
    }

    public void Set(upgradeData upgradesData){
        icon.sprite = upgradesData.icon;
        BuffDescName.text = upgradesData.Name;
        BuffDescText.text = upgradesData.desc;
    }

    public void Clean(){
        icon.sprite = null;
        BuffDescName.text = null;
        BuffDescText.text = null;
    }
    
}
