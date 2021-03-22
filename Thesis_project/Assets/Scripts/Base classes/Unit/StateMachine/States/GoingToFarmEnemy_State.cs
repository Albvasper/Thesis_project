using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToFarmEnemy_State : State {

    private float resourceProximity;

    public GoingToFarmEnemy_State(EnemyRecolectors enemyRecolector, StationaryResource currentRes) : base(enemyRecolector) {
        enemyRecolector.SetCurrentResource(currentRes);
        resourceProximity = 5f;
    }

    public override void OnStateEnter() {
        enemyRecolector.SetIsFarming(true);
    }

    public override void Update() {
        // Main Loop: Go to resource until something happens
        if (enemyRecolector.GetCurrentResource() != null) {
            enemyRecolector.MoveUnit(enemyRecolector.GetCurrentResource().transform.position);
            if (Vector3.Distance(enemyRecolector.transform.position, enemyRecolector.GetCurrentResource().transform.position) < resourceProximity) {
                // Change state to return to base
                enemyRecolector.SetState(new FarmingEnemy_State(enemyRecolector));
            }
        } else {
            enemyRecolector.SetState(new IDLE_State(enemyRecolector));
        }
    }

    public override void OnStateExit() {
        enemyRecolector.SetIsFarming(false);
    }
}
