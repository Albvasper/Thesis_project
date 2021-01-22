using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : StationaryUnit {

    protected override void Start() {
        base.Start();
        Player.Instance.AddUnitSpaces(5); // Only do this when the house is placed!!!
    }

    protected override void Update() {
        CheckHP();
        CheckLevel();
    }    

    protected override void Die() {
        Player.Instance.SubstractUnitSpaces(5);
        base.Die();
    }
}
