using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingEnemy_State : State {

    private float farmingRate;

    public FarmingEnemy_State(EnemyRecolectors enemyRecolector) : base(enemyRecolector) {
        farmingRate = 10f;
    }

    public override void OnStateEnter() {
        enemyRecolector.GetAgent().isStopped = true;
        enemyRecolector.SetIsFarming(true);
    }

    public override void Update() {
        /* Main Loop: The unit will get the 
        resource until it cant carry anymore */
        farmingRate -= Time.deltaTime;
        if (enemyRecolector.GetCurrentResource() != null) {
            if (farmingRate <= 0f) {
                enemyRecolector.ReceiveResource(enemyRecolector.GetCurrentResource().GiveResource(enemyRecolector.GetEfficiency()));
                enemyRecolector.SetState(new ReturningEnemy_State(enemyRecolector));
                farmingRate = 10f;
            }
        } else {
            enemyRecolector.SetState(new IDLE_State(enemyRecolector));
        }
    }

    public override void OnStateExit() {
        enemyRecolector.GetCurrentResource().CheckQuantity();
        enemyRecolector.GetAgent().isStopped = false;
        enemyRecolector.SetIsFarming(false);
    }
}
