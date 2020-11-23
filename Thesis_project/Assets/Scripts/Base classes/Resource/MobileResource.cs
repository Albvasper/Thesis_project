using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileResource : Resource {

    protected State currentState;

    protected enum State {
        WANDERING, ATTACKING, ESCAPING
    }

    protected override void Start() {
        base.Start();
        currentState = State.WANDERING;
    }

    protected override void Update() {
        base.Update();
    }
}
