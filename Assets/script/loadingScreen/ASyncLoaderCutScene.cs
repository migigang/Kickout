using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ASyncLoaderCutScene : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Slider loadingSlider;
   // [SerializeField]
   // private Text progressText;

    [SerializeField]
    private string sceneLoad;

    private void OnEnable() {
        StartCoroutine(LoadAsynchorously());
        loadingScreen.SetActive(true);
    }

     IEnumerator LoadAsynchorously (){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneLoad);

        while (!loadOperation.isDone){
            float progress = Mathf.Clamp01(loadOperation.progress / .9f);
            loadingSlider.value = progress;
           // progressText.text = (progress * 100).ToString("F0") + "%";

            yield return null;
        }
     }
}
