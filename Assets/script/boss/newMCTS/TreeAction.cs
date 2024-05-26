using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace theLastHope{

public class Node
{
    private List<Node> children;

    public Node parent;

    public State state; //state pada permodelan
    public Register data;

    public Node(Register data){
        this.data = data;
        this.children = new List<Node>();
    }
    public Node(Node parent, Register data){
        this.parent = parent;
        this.data = data;
        this.children = new List<Node>();
    }
    public Node AddChild(Register data)
    {
        Node child = new Node(data);
        children.Add(child);
        return child;
    }

    public List<Node> getPossibleAction(){ //kumpulkan node yang sudah dibuat
        return children;
    }
    public int nbChilden(){ //mengambil sejumlah anak pada anak pohon
        return children.Count;
    }

    public void setState(State p){ //menetapkan state
        this.state = p;
    }

    public static void Retropropagation(Node node){  
        int i = 0;
        int validate = node.data.a;
        while(node.parent != null){
            Debug.Log("melakukan Retropropagation");
            node.parent.data.a += validate; 
            node.parent.data.b++;
            node = node.parent;
          //  if(i++ > 10000) break;

        }
    }

    public Node Exist(State p ){
        if(children != null){
            foreach(var child in children){
                if(child.state == p){
                    return child;
                }
            }
        }
        return null;
    }
    public Node GetChild(int i)
    {
        foreach (Node n in children)
            if (--i == 0)
                return n;
        return null;
    }

}


}
