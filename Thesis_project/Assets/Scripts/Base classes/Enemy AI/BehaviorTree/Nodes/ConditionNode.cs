using UnityEngine;

public abstract class ConditionNode : Node {

    public ConditionNode(BehaviorTree behaviorTree) : base(behaviorTree) {
    }

    public override void Update() {
        if (Condition() == true && children.Count > 0) {
            behaviorTree.SwitchCurrentNode(children[0]);
        } else if (/*Condition() == false && */children.Count == 2) {
            behaviorTree.SwitchCurrentNode(children[1]);
        }
    }

    public abstract bool Condition();
}