using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDLE_State : State { 

    StationaryResource currentResource;

    public IDLE_State(MobileUnit mb) : base(mb) {
        
    }

    public override void OnStateEnter() {
        Player.Instance.GetIdleUnits().Add(mobileUnit.gameObject);
        mobileUnit.GetAgent().isStopped = true;
    }

    public override void Update() {
    }

    public override void OnStateExit() {
        Player.Instance.GetIdleUnits().Remove(mobileUnit.gameObject);
        mobileUnit.GetAgent().isStopped = false;
    }
}
