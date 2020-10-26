using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsResource : StationaryResource {
    
    /*
    Nombre temporal de clase!
    */

    protected override void Start() {
        base.Start();
        type = ResourceType.ASSET;
    }

    protected override void Update() {
        base.Update();
    }
}
