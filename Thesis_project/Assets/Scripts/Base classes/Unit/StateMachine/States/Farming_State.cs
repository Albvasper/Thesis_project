using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming_State : State {

    private float farmingRate;

    public Farming_State(Intern intern) : base(intern) {
        farmingRate = 10f;
    }

    public override void OnStateEnter() {
        intern.GetAgent().isStopped = true;
    }

    public override void Update() {
        /* Main Loop: The unit will get the 
        resource until it cant carry anymore */
        farmingRate -= Time.deltaTime;
        if (intern.GetCurrentResource() != null) {
            if (farmingRate <= 0f) {
                intern.ReceiveResource(intern.GetCurrentResource().GiveResource(intern.GetEfficiency()));
                intern.SetState(new Returning_State(intern));
                farmingRate = 10f;
            }
        } else {
            intern.SetState(new IDLE_State(intern));
        }
    }

    public override void OnStateExit() {
        intern.GetCurrentResource().CheckQuantity();
        intern.GetAgent().isStopped = false;
    }
}
