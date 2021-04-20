using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : StationaryUnit {
    
    [SerializeField] private bool aiWall;
    [SerializeField] private GameObject parentGO;

    protected override void Start() {
        maxHP = 800;
        attackDamage = 0;
        base.Start();
        aiUnit = aiWall;
    }

    protected override void Update() {
        base.Update(); 
    }

    protected override void Die() {
        Destroy(parentGO);
    }
}