using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDLE_State : State { 

    StationaryResource currentResource;

    public IDLE_State(MobileUnit unit) : base(unit) {
        
    }

    public override void OnStateEnter() {
        unit.GetAgent().isStopped = true;
    }

    public override void Update() {
    }

    public override void OnStateExit() {
        unit.GetAgent().isStopped = false;
    }
}
