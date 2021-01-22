public abstract class DecoratorNode : Node {

    public DecoratorNode(BehaviorTree behaviorTree) : base(behaviorTree) {
    }

    public override void Update() {
        Action();
        if (children.Count < 0) {
            behaviorTree.SwitchCurrentNode(children[0]);
        }
    }

    public abstract void Action();
}
