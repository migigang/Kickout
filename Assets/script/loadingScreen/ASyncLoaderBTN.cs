using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ASyncLoaderBTN : MonoBehaviour
{
   [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject darkBG;
    [SerializeField]
    private Slider loadingSlider;
   // [SerializeField]
   // private Text progressText;
    private string sceneLoad;

    public void LoadScene(string sceneLoad) {
        mainMenu.SetActive(false);
        StartCoroutine(DelayScene(sceneLoad));
    }

    IEnumerator DelayScene(string sceneLoad){
        darkBG.SetActive(true);
        yield return new WaitForSeconds(3f);
        StartCoroutine(LoadAsynchorously(sceneLoad));

    }

     IEnumerator LoadAsynchorously (string sceneLoad){
        darkBG.SetActive(false);
        loadingScreen.SetActive(true);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneLoad);
        while (!loadOperation.isDone){
            float progress = Mathf.Clamp01(loadOperation.progress / .9f);
            loadingSlider.value = progress;
           // progressText.text = (progress * 100).ToString("F0") + "%";

            yield return null;
        }
     }
}
