using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Returning_State : State {

    private float coreProximity;

    public Returning_State(Intern intern) : base(intern) {
        coreProximity = 12f;
    }

    public override void Update() {
        /* Main Loop: The intern will head to the Studio until it gets there.*/
        intern.MoveUnit(Studio.Instance.transform.position);
        if (Vector3.Distance(intern.transform.position, Studio.Instance.transform.position) < coreProximity) {
            Studio.Instance.ReceiveResource(intern);
            // Change state to return to resource
            if (intern.GetCurrentResource() == null) {
                intern.SetState(new IDLE_State(intern));
            } else {
                intern.SetState(new GoingToFarm_State(intern, intern.GetCurrentResource()));
            }
        }
    }
}
