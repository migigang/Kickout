using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;


    public void setMaxHealth (int health){
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void setHealth (int health){
        slider.value = health;
    }
}
