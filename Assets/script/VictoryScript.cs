using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace theLastHope{
public class VictoryScript : MonoBehaviour
{

    public string sceneLoad;
    [SerializeField]
    private Slider loadingSlider;
    [SerializeField]
    private GameObject loadingScreen;
    public bossHealthq bossHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealth.currentHealth < 2){
            StartCoroutine(LoadAsynchorously(sceneLoad));
            Debug.Log("tes");
        }
    }

         IEnumerator LoadAsynchorously (string sceneLoad){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneLoad);
        while (!loadOperation.isDone){
            float progress = Mathf.Clamp01(loadOperation.progress / .9f);
            loadingSlider.value = progress;
           // progressText.text = (progress * 100).ToString("F0") + "%";

            yield return null;
        }
     }
}

}
