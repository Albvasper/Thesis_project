using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MobileUnit : Unit {

    protected State currentState;
    [SerializeField]
    protected NavMeshAgent agent;

    protected override void Start() {
        base.Start();
        SetState(new IDLE_State(this));
        Player.Instance.AddMobileUnit(gameObject);
    }

    protected override void Update() {
        base.Update();
        currentState.Update();
    }

    public void SetState(State state) {
        if (currentState != null) {
            // Get out of the current state
            currentState.OnStateExit();
        }
        // Replace current state with new state
        currentState = state;
        //Debug.Log("current state: " + state.GetType().Name);
        if (currentState != null) {
            // Initialize new state
            currentState.OnStateEnter();
        }
    }

    public void MoveUnit(Vector3 point) {
        agent.SetDestination(point);
    }

    public NavMeshAgent GetAgent() {
        return agent;
    }

    protected override void Die() {
        Player.Instance.GetMobileUnits().Remove(gameObject);
        Player.Instance.GetSelectedUnits().Remove(gameObject);
        Destroy(gameObject);
    }
}
