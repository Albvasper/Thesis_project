using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking_State : State {

    private Unit enemyUnit;
    private float attackCoolDown;

    public Attacking_State(MobileUnit mu, Unit enemyUnit) : base(mu) {
        this.enemyUnit = enemyUnit;
        attackCoolDown = 1f;
    }

    public override void OnStateEnter() {
        if (mobileUnit.isActiveAndEnabled == true) {
            mobileUnit.GetAgent().isStopped = true;
            mobileUnit.CurrentlyAttacking();
        }
    }

    public override void Update() {
        attackCoolDown -= Time.deltaTime;
        if (enemyUnit != null) {
            if (attackCoolDown <= 0f) {
                if (Vector3.Distance(enemyUnit.transform.position, mobileUnit.transform.position) < 5 || enemyUnit.gameObject.layer == 9) {
                    mobileUnit.Attack(enemyUnit);
                    // Make the other unit fight back
                    MobileUnit enemyMobileUnitScript = enemyUnit.GetComponent<MobileUnit>();
                    if (enemyMobileUnitScript == true) {
                        enemyMobileUnitScript.SetState(new Attacking_State(enemyMobileUnitScript, mobileUnit));
                    }
                    attackCoolDown = 1f;
                } else {
                    mobileUnit.SetState(new ApproachingEnemy_State(mobileUnit, enemyUnit));
                }
            }
        } else {
            mobileUnit.SetState(new IDLE_State(mobileUnit));
        }
    }

    public override void OnStateExit() {
        if (mobileUnit.isActiveAndEnabled == true) {
            mobileUnit.GetAgent().isStopped = false;
            mobileUnit.NotAttacking();
        }
    }
}
