using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileResource : Resource {

    protected float movementSpeed;
    protected State currentState;
    protected Rigidbody rb;

    protected enum State {
        WANDERING, ATTACKING, ESCAPING
    }

    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody>();
        currentState = State.WANDERING;
    }

    protected override void Update() {
        base.Update();
    }
}
