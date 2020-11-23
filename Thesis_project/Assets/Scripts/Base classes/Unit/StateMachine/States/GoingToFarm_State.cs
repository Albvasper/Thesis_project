using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToFarm_State : State {

    private float resourceProximity;

    public GoingToFarm_State(Developer developer, StationaryResource currentRes) : base(developer) {
        developer.SetCurrentResource(currentRes);
        resourceProximity = 5f;
    }
    
    public override void Update() {
        // Main Loop: Go to resource until something happens
        if (developer.GetCurrentResource() != null) {
            developer.MoveUnit(developer.GetCurrentResource().transform.position);
            if (Vector3.Distance(developer.transform.position, developer.GetCurrentResource().transform.position) < resourceProximity) {
                // Change state to return to base
                developer.SetState(new Farming_State(developer));
            }
        } else {
            developer.SetState(new IDLE_State(developer));
        }
    }
}
