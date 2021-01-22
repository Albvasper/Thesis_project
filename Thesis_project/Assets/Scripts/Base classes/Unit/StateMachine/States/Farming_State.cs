using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming_State : State {

    private float farmingRate;

    public Farming_State(Developer developer) : base(developer) {
        farmingRate = 10f;
    }

    public override void OnStateEnter() {
        developer.GetAgent().isStopped = true;
    }

    public override void Update() {
        /* Main Loop: The unit will get the 
        resource until it cant carry anymore */
        farmingRate -= Time.deltaTime;
        if (developer.GetCurrentResource() != null) {
            if (farmingRate <= 0f) {
                developer.ReceiveResource(developer.GetCurrentResource().GiveResource(developer.GetEfficiency()));
                developer.SetState(new Returning_State(developer));
                farmingRate = 10f;
            }
        } else {
            developer.SetState(new IDLE_State(developer));
        }
    }

    public override void OnStateExit() {
        developer.GetCurrentResource().CheckQuantity();
        developer.GetAgent().isStopped = false;
    }
}
