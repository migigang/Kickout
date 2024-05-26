using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace theLastHope{


    public class MCTS 
{
    private Node tree;
    
    private float born;
    private Model model;
    private int BossID = 1;
    private int maxIteration = 10;
    private int iterationCount = 0;
    private Model simumodel;

     public MCTS(Model model) 
    {
        tree = new Node(new Register(0, 0));
        born = 0.0f;
   
        this.model = model;
    }

      public bool trust()
    {
        foreach (Node n in tree.getPossibleAction())
        {
            if (n.data.b > 10)
            {
                Debug.Log("setidaknya salah satu node harus dapat diandalkan");
                return true;
            }
        }

        return false;
    }

    public State interact() //SELECT BEST ACTION IN THREE
    {
        iterationCount = 0;
        for (int i =0;i<maxIteration;i++)
        {
            Debug.Log("melakukan simulasi");   
            simulate(tree); 

    float maxHeuristicScore = float.MinValue;
    Node selectedNode = null;

    foreach (Node child in tree.getPossibleAction())
    {
        if (child.state != State.dead)
        {
            Debug.Log("menghitung skor heuristik");
            // Hitung skor heuristik untuk masing-masing child node
            float heuristicScore = calculateHeuristicScore(child);

            if (heuristicScore > maxHeuristicScore)
            {
                 Debug.Log("jika heuristicScore lebih besar dari maxHeuristicScore");
                maxHeuristicScore = heuristicScore;
                selectedNode = child;
            }
        }
    }

    if (selectedNode != null)
    {
         Debug.Log("select node dari hasil heuristik");   
        tree = selectedNode;
        return selectedNode.state;
    }
    else
    {
        // Jika tidak ada aksi yang dipilih menggunakan heuristik, gunakan UCB1
         Debug.Log("memilih aksi menggunakan UCB1");
        Node bestChild = selectBestChild(tree);
        if (bestChild != null)
        {
            tree = bestChild;
            return bestChild.state;
        }
    }


        }
    return State.dead;
}
    




    void simulate(Node action) //Simulation
    {
        
        simumodel = new Model(model);  //Kami menyalin model saat ini
        GameSimul.copymodel = simumodel;

        //Selama simulasi belum selesai
        while (!GameSimul.isFinished && iterationCount < maxIteration)
        {
            float heuristicScore = calculateHeuristicScore(action);
            System.Array actions = GameSimul.GetNextPossibleAction(action);

            //  Memilih tindakan random
            State choice = (State) GameSimul.GetRandomAction(actions);
            //  encore Buat simpul (oleh karena itu tindakan) jika belum ada
            //  mengecek apakah node sudah ada di daftar atau tidak
            Node exitanteNode = action.Exist(choice);
            if (exitanteNode == null)
            {
                Debug.Log("tindakan baru menjadi tindakan saat ini");   
                Node selectedAction = action.AddChild(new Register(0, 0));
                selectedAction.parent = action;
                selectedAction.setState(choice);

                action = selectedAction; //Tindakan baru menjadi tindakan saat ini
            }
            else
            {
                Debug.Log("tindakan saat ini adalah tindakannya");   
                action = exitanteNode;   //lakukan tindakan yang sudah ada
            }

            // Mulai aksi
             Debug.Log("mulai aksi");   
            GameSimul.PlayAction(action);

            iterationCount++;  
        }

        //  Menerapkan nilai ke lembar terakhir
        action.data.b = 1;
        if (GameSimul.finalSituation == 0) //gameover
        {
            Debug.Log("gameSimul situasi gameOver");
            action.data.a = -1;
           
        }
        
        if (GameSimul.finalSituation == 2) //égalité
        {
            Debug.Log("jika boss dan player masih hidup");
            action.data.a = 0;
           
        }
        
        else if (GameSimul.finalSituation == 1)//win
        {
           Debug.Log("membunuh player");
            action.data.a = 1;
            
        }

        // Retroprograpagation dari tindakan
        Node.Retropropagation(action);
        // reset simulasi
        GameSimul.Reset(); 
    }
    

     private Node selectBestChild(Node node)
    {
        float explorationFactor = 1.4f; // Faktor eksplorasi, dapat disesuaikan sesuai kebutuhan
        float maxHeuristicScore = float.MinValue;
        Node bestChild = null;
        float bestUCB1 = float.MinValue;

        foreach (Node child in node.getPossibleAction())
        {
            if (child.state != State.dead)
        {
            float exploitation = (float)child.data.a / (float)child.data.b;
            float exploration = Mathf.Sqrt(Mathf.Log(tree.data.b) / child.data.b);
            float ucb1 = exploitation + explorationFactor * exploration;

            if (ucb1 > maxHeuristicScore)
            {
                maxHeuristicScore = ucb1;
                bestChild = child;
            }
        }

        if (child.data.a == child.data.b)
        {
            break;
        }


    // memilih node mana yang terbaik
    if (bestChild != null)
    {
        tree = bestChild;
        tree.parent = null;
    }

    }

        return bestChild;

}
private float calculateHeuristicScore(Node node)
{
    float heuristicScore = 0.0f;

    if (node.state == State.shotlaser)
    {
        Debug.Log("menilai aksi shotlaser");
        // menilai aksi laser
        if(GameSimul.TouchAdv == 1){
        heuristicScore += 0.5f;
        }else{
             heuristicScore -= 0.5f;
        }
        
    }
     if (node.state == State.spesialShooting)
    {
        Debug.Log("menilai aksi spesialShot");
        // menilai aksi spesialshot
        if(GameSimul.TouchAdv == 1){
        heuristicScore += 0.5f;
        }else{
             heuristicScore -= 0.5f;
        }
       
    }
    // Implementasikan logika perhitungan skor heuristik di sini
    // Menggunakan informasi dari node untuk menghitung skor heuristik

    return heuristicScore; // Mengembalikan skor heuristik, sesuaikan dengan logika Anda
}

}
}
