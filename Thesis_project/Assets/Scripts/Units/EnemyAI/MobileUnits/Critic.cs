using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critic : MobileUnit {
    
    protected override void Start() {
        maxHP = 150;
        attackDamage = 20;
        base.Start();
    }

     protected override void Update() {
        base.Update(); 
    }
}
