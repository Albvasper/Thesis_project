using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking_State : State {

    Vector3 destination;
    float pointProximity;
    
    public Walking_State(MobileUnit mb, Vector3 point) : base(mb) {
        destination = point;
        pointProximity = 2f;
    }

    public override void Update() {
        mobileUnit.MoveUnit(destination);
        if (Vector3.Distance(mobileUnit.transform.position, destination) < pointProximity) {
            mobileUnit.SetState(new IDLE_State(mobileUnit));
        }
    }
}
