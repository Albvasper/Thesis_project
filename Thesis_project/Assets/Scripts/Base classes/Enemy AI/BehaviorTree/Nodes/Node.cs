using System.Collections.Generic;

public abstract class Node {

    protected Node father;
    protected List<Node> children = new List<Node>();
    protected BehaviorTree behaviorTree;

    public Node() {
    }

    public Node(BehaviorTree behaviorTree) {
        this.behaviorTree = behaviorTree;
    }

    public abstract void Update();

    public void SetFather(Node node) {
        father = node;
        father.AddChild(this);
    }

    public Node GetFather() {
        return father;
    }

    private void AddChild(Node child) {
        children.Add(child);
    }
}