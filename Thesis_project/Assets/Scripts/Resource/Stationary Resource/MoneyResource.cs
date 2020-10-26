using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyResource : StationaryResource {

    /*
    Nombre temporal de clase!
    */

    protected override void Start() {
        base.Start();
        type = ResourceType.MONEY;
    }

    protected override void Update() {
        base.Update();
    }
}
