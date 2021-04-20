using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecluterArtist : StationaryUnit {
    
    protected override void Start() {
        maxHP = 200;
        attackDamage = 0;
        base.Start();
    }

    protected override void Update() {
        base.Update(); 
    }
}
