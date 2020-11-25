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
        mobileUnit.GetAgent().isStopped = true;
    }

    public override void Update() {
        attackCoolDown -= Time.deltaTime;
        if (enemyUnit != null) {
            if(attackCoolDown <= 0f){
                mobileUnit.Attack(enemyUnit);
                attackCoolDown = 1f;
            }
        } else {
            mobileUnit.SetState(new IDLE_State(mobileUnit));
        }
    }

    public override void OnStateExit() {
        mobileUnit.GetAgent().isStopped = false;
    }
}
