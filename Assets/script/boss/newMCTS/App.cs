using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theLastHope{


public class App : MonoBehaviour
{
    private View myView;
    private Controller myController;
    private Model myModel;
    private Transform Target;
    public GameObject boss;
   // public GameObject ComponentBoss;
    public string targetTag = "Player";
    public int numberOfBoss = 1;
    public GameObject ammoShot;
    public GameObject rocket;
    
    private void Awake()
    {
        //Time.timeScale = 1.0f;
        Target = GameObject.FindGameObjectWithTag(targetTag).transform;
        myModel = new Model(Target,boss,numberOfBoss, ammoShot, rocket);
       // CharacterRender charrender = new CharacterRender();
        MCTS mcts = new MCTS(myModel);
        myController = new Controller(mcts);
       
        myController.activeModel = myModel;
        
        myView = new View(myModel.getGameState(),boss);
    }

    // Update is called once per frame
    private void Update()
    {
       // myModel.inGameDeltaTime = Time.deltaTime;
       myModel.target = Target;
       myController.listenMCTSAI();

    }


    private void FixedUpdate() {
        myView.UpdateView(myModel.getGameState());
    }
    

   
}


}
