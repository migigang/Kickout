using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcDialog2 : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private string[] dialogue;
    [SerializeField]
    private int index;

    [SerializeField]
    private float wordSpeed;
    [SerializeField]
    private bool playerIsClose;
    [SerializeField]
    private GameObject dialogueBtn;
    [SerializeField]
    private GameObject contDialogueBtn;
    [SerializeField]
    private GameObject UIplayer;


    
    void Update()
    {
        //startDialog();
        if(dialogueText.text == dialogue[index]){
            contDialogueBtn.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            playerIsClose = true;
            dialogueBtn.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            playerIsClose = false;
            dialogueBtn.SetActive(false);
            zeroText();
        }    
    }

    public void startDialog(){
        if(dialoguePanel.activeInHierarchy){
            zeroText();
        }else{
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }
    public void NextLine(){

        contDialogueBtn.SetActive(false);
        if(index < dialogue.Length - 1){
            index++;
            dialogueText.text="";
            StartCoroutine(Typing());
        }else{
            zeroText();
            UIplayer.SetActive(true);
        }
    }

    public void zeroText(){
        dialogueText.text = "";
        index=0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing(){
        foreach(char letter in dialogue[index].ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
 
}
