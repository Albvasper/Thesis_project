using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeResource : StationaryResource {
   
    /*
    Nombre temporal de clase!
    */

    protected override void Start() {
        base.Start();
        type = ResourceType.LINEOFCODE;
    }

    protected override void Update() {
        base.Update();
    }
}
