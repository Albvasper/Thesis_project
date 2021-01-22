public abstract class ActionNode : Node {

    public ActionNode(BehaviorTree behaviorTree) : base(behaviorTree) {

    }

    public override void Update() {
        Action();
        behaviorTree.SwitchCurrentNode(behaviorTree.GetRoot());
    }

    public abstract void Action();
}