using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artist : MobileUnit {
    
    protected override void Start() {
        maxHP = 100;
        attackDamage = 23;
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }
}
