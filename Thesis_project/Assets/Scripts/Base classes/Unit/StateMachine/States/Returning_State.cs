using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Returning_State : State {

    private float studioProximity;

    public Returning_State(Developer developer) : base(developer) {
        studioProximity = 12f;
    }

    public override void Update() {
        /* Main Loop: The developer will head to the Studio until it gets there.*/
        developer.MoveUnit(Studio.Instance.transform.position);
        if (Vector3.Distance(developer.transform.position, Studio.Instance.transform.position) < studioProximity) {
            Studio.Instance.ReceiveResource(developer);
            // Change state to return to resource
            if (developer.GetCurrentResource() == null) {
                developer.SetState(new IDLE_State(developer));
            } else {
                developer.SetState(new GoingToFarm_State(developer, developer.GetCurrentResource()));
            }
        }
    }
}
