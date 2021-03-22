public class SequenceNode : Node {

    public SequenceNode() {
    }

    public override void Update() {
        foreach (Node node in children) {
            //behaviorTree.SwitchCurrentNode(node);
            node.Update();
        }
    }
}