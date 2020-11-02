using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobileUnit : Unit {

    protected float movementSpeed;
    protected State currentState;
    protected NavMeshAgent agent;

    protected enum State {
        IDLE, WALKING, ATTACKING
    }

    protected override void Start() {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        currentState = State.IDLE;
    }

    protected override void Update() {
        base.Update();
        CheckState();
    }

    protected void CheckState() {
        if (currentState == State.IDLE) {
            // IDLE Animation
        }
        else if (currentState == State.WALKING) {
            // WALKING Animation
        } else {
            // ATTACKING Animation
        }
    }

    public void MoveUnit(Vector3 point) {
        agent.SetDestination(point);
    }
}
