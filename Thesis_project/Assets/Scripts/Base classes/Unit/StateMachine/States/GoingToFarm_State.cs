using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToFarm_State : State {

    private float resourceProximity;

    public GoingToFarm_State(Intern intern, StationaryResource currentRes) : base(intern) {
        intern.SetCurrentResource(currentRes);
        resourceProximity = 5f;
    }
    
    public override void Update() {
        // Main Loop: Go to resource until something happens
        if (intern.GetCurrentResource() != null) {
            intern.MoveUnit(intern.GetCurrentResource().transform.position);
            if (Vector3.Distance(intern.transform.position, intern.GetCurrentResource().transform.position) < resourceProximity) {
                // Change state to return to base
                intern.SetState(new Farming_State(intern));
            }
        } else {
            intern.SetState(new IDLE_State(intern));
        }
    }
}
