using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningEnemy_State : State {

    private float coreProximity;

    public ReturningEnemy_State(EnemyRecolectors enemyRecolector) : base(enemyRecolector) {
        coreProximity = 12f;
    }

    public override void OnStateEnter() {
        enemyRecolector.SetIsFarming(true);
    }

    public override void Update() {
        /* Main Loop: The unit will head to the core until it gets there.*/
        enemyRecolector.MoveUnit(VideogameCore.Instance.transform.position);
        if (Vector3.Distance(enemyRecolector.transform.position, VideogameCore.Instance.transform.position) < coreProximity) {
            VideogameCore.Instance.ReceiveResource(enemyRecolector);
            // Change state to return to resource
            if (enemyRecolector.GetCurrentResource() == null) {
                enemyRecolector.SetState(new IDLE_State(enemyRecolector));
            } else {
                enemyRecolector.SetState(new GoingToFarmEnemy_State(enemyRecolector, enemyRecolector.GetCurrentResource()));
            }
        }
    }

    public override void OnStateExit() {
        enemyRecolector.SetIsFarming(false);
    }
}
