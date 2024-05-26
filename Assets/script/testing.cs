using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class testing : MonoBehaviour
{
    public GameObject[] totalEnemy;

    [SerializeField]
  private GameObject gates;
  [SerializeField]
  private GameObject path;
  public string enemyTag = "Enemy";

  public bool enemyDone=false;
    //[SerializeField]
    //private GameObject mainMenu;
   //  [SerializeField]
   // private Slider loadingSlider;
  //   public string sceneBuildIndex;

       void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      totalEnemy = GameObject.FindGameObjectsWithTag(enemyTag);
          RemoveNullElementsFromTotalEnemy();
        if(totalEnemy.Length==0){
            gates.SetActive(true);
            enemyDone=true;
            path.SetActive(true);
              //StartCoroutine(LoadAsynchorously(sceneBuildIndex));
        }
        
    }

     void RemoveNullElementsFromTotalEnemy()
    {
        List<GameObject> enemyList = new List<GameObject>(totalEnemy);
        enemyList.RemoveAll(item => item == null);
        totalEnemy = enemyList.ToArray();
    }


    // IEnumerator LoadAsynchorously (string sceneBuildIndex){
      //  AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);
      //  while (!loadOperation.isDone){
       //     float progress = Mathf.Clamp01(loadOperation.progress / .9f);
          //  loadingSlider.value = progress;
           // progressText.text = (progress * 100).ToString("F0") + "%";

       //     yield return null;
     //   }
  //   }
     
}
