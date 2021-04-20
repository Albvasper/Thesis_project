using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Developer : MobileUnit {
    
    protected override void Start() {
        maxHP = 100;
        attackDamage = 15;
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }
}
