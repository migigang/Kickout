using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransisi : MonoBehaviour
{
    public Animator transisiAnim;
    public string namaScene;
    public float changeTime;



    // Update is called once per frame
    public void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <0){
        SceneManager.LoadScene(namaScene);
        }
        
    }

  // IEnumerator LoadScene()
   // {
        //FindObjectOfType<soundManager>().play("button");
        //transisiAnim.SetTrigger("end");
      //  yield return new WaitForSeconds(1.5f);
        //    StartCoroutine(LoadScene());
        
    //}
}
