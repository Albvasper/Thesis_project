using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming_State : State {

    public Farming_State(Developer developer) : base(developer) {
    }

    public override void Update() {
        /* Main Loop: The unit will get the 
        resource until it cant carry anymore */
        if (developer.GetCurrentResource() != null) {
            developer.ReceiveResource(developer.GetCurrentResource().GiveResource(developer.GetEfficiency()));
            developer.SetState(new Returning_State(developer));
        } else {
            developer.SetState(new IDLE_State(developer));
        }
    }

    public override void OnStateExit() {
        developer.GetCurrentResource().CheckQuantity();
    }
}
