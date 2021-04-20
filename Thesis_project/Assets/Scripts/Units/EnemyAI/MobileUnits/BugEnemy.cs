using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugEnemy : MobileUnit {

    protected override void Start() {
        maxHP = 100;
        attackDamage = 20;
        base.Start();
    }

     protected override void Update() {
        base.Update(); 
    }
}