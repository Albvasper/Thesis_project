using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUnit : Unit {

    protected float movementSpeed;
    protected State currentState;
    protected Rigidbody rb;

    protected enum State {
        IDLE, WALKING, ATTACKING
    }

    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody>();
        currentState = State.IDLE;
    }

    protected override void Update() {
        base.Update();
    }
}
