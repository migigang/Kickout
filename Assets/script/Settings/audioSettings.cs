using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer myMixer;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Slider masterSlider;


    private void Start() {
        if(PlayerPrefs.HasKey("musicVolume")){
            loadVolume();
        }else{
            musicVolume();
            SFXVolume();
            MasterVolume();
        }
        
    }

   public void musicVolume(){
    float volume = musicSlider.value;
    myMixer.SetFloat("music", Mathf.Log10(volume)*20);
    PlayerPrefs.SetFloat("musicVolume", volume);
   }

   public void SFXVolume(){
    float volume = sfxSlider.value;
    myMixer.SetFloat("sfx", Mathf.Log10(volume)*20);
    PlayerPrefs.SetFloat("sfxVolume", volume);
   }

   public void MasterVolume(){
    float volume = masterSlider.value;
    myMixer.SetFloat("master", Mathf.Log10(volume)*20);
    PlayerPrefs.SetFloat("masterVolume", volume);
   }

   private void loadVolume(){
    musicSlider.value=PlayerPrefs.GetFloat("musicVolume");
    musicSlider.value=PlayerPrefs.GetFloat("sfxVolume");
    musicSlider.value=PlayerPrefs.GetFloat("masterVolume");
    musicVolume();
    MasterVolume();
    SFXVolume();
   }
}
