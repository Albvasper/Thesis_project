using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking_State : State {

    Vector3 destination;
    float pointProximity;
    
    public Walking_State(MobileUnit unit, Vector3 point) : base(unit) {
        destination = point;
        pointProximity = 2f;
    }

    public override void Update() {
        unit.MoveUnit(destination);
        if (Vector3.Distance(unit.transform.position, destination) < pointProximity) {
            unit.SetState(new IDLE_State(unit));
        }
    }
}
