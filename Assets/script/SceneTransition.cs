using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;
    public string sceneBuildIndex;
    [SerializeField]
    private GameObject mainMenu;
     [SerializeField]
    private Slider loadingSlider;

    private void OnTriggerEnter2D(Collider2D other) {
        print("Trigger Entered");

        if(other.tag == "Player") {
            mainMenu.SetActive(false);
            print("Switching Scene to " + sceneBuildIndex);
            StartCoroutine(LoadAsynchorously(sceneBuildIndex));
            loadingScreen.SetActive(true);
        }
    }

    IEnumerator LoadAsynchorously (string sceneBuildIndex){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);
        while (!loadOperation.isDone){
            float progress = Mathf.Clamp01(loadOperation.progress / .9f);
            loadingSlider.value = progress;
           // progressText.text = (progress * 100).ToString("F0") + "%";

            yield return null;
        }
     }
}
