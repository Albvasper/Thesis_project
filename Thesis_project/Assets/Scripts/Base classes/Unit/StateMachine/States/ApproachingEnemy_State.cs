using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachingEnemy_State : State {

    private Unit enemyUnit;
    private int enemyProximity;

    public ApproachingEnemy_State(MobileUnit mu, Unit enemyUnit) : base(mu) {
        this.enemyUnit = enemyUnit;
        enemyProximity = 3;
    }

    public override void OnStateEnter() {
        mobileUnit.CurrentlyAttacking();
    }

    public override void Update() {
        if (enemyUnit != null) {
            mobileUnit.MoveUnit(enemyUnit.transform.position);
            if (Vector3.Distance(mobileUnit.transform.position, enemyUnit.transform.position) < enemyProximity) {
                // Change state to attack
                mobileUnit.SetState(new Attacking_State(mobileUnit, enemyUnit));
            }
        } else {
            mobileUnit.SetState(new IDLE_State(mobileUnit));
        }
    }
    
    public override void OnStateExit() {
        if (mobileUnit.isActiveAndEnabled == true) {
            mobileUnit.NotAttacking();
        }
    }
}
