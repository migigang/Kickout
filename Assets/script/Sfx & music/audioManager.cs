using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    [Header("--------------Audio Source--------------")]
   [SerializeField]
   AudioSource musicSource;
   [SerializeField]
   AudioSource SFXSource;
    [Header("--------------Audio Clip--------------")]
   public AudioClip backgroundMusic;
   public AudioClip Shooting;
   public AudioClip reload;
   public AudioClip Delayreload;
   public AudioClip damage;
   public AudioClip dash;
   public AudioClip useItem;
   public AudioClip footStep;
   public AudioClip coin;
    [Header("--------------NPC Sfx--------------")]
   public AudioClip NPCShop;
   public AudioClip BossShot;
   public AudioClip SpesialShot;



   private void Start() {
    musicSource.clip = backgroundMusic;
    musicSource.Play();
   }

   public void playSFX(AudioClip clip){
    SFXSource.PlayOneShot(clip);
   }   
}
