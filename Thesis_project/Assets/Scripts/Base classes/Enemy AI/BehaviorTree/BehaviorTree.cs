using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree {

    private Node root;
    private List<Node> children = new List<Node>();
    private Node currentNode;

    public BehaviorTree(Node root) {
        this.root = root;
        currentNode = root;
    }

    public void Update() {
        currentNode.Update();
    }

    public Node GetRoot() {
        return root;
    }

    public void SwitchCurrentNode(Node newNode) {
        currentNode = newNode;
    }

    public void AddNode(Node father, Node newNode) {
        newNode.SetFather(father);
        children.Add(newNode);
    }
}