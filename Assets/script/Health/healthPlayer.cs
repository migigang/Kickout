using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class healthPlayer : playerBase//MonoBehaviour
{
    public int takesHealth;
    public int takesNumOfHealths;


    [SerializeField]
    private Image[] hearts;

    [SerializeField]
    private Sprite fullHeart;

    [SerializeField]
    private Sprite yellowHeart;

    [SerializeField]
    private Sprite emptyHeart;

    private Shake shake;
    

    void Update()
    {
        if(takesHealth > takesNumOfHealths){
            takesHealth = takesNumOfHealths;
        }
        for (int i = 0; i < hearts.Length; i++){
            if(i < takesHealth){
                if(i> 5 && takesHealth>5){
                hearts[i].sprite = yellowHeart;
                }else{
                hearts[i].sprite = fullHeart;
                }
            }
            else{
                hearts[i].sprite = emptyHeart;               
            }


            if(i < takesNumOfHealths){
                hearts[i].enabled = true;
            }else{
                hearts[i].enabled = false;
            }
        }
    }
}
