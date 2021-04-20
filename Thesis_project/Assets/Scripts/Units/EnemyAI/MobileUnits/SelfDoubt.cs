using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDoubt : MobileUnit {
    
    protected override void Start() {
        maxHP = 100;
        attackDamage = 10;
        base.Start();
    }

     protected override void Update() {
        base.Update(); 
    }
}
