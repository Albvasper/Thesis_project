using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : StationaryUnit {

    protected override void Start() {
        base.Start();
        Player.Instance.AddSpaces(5); // Only do this when the house is placed!!!
    }

    protected override void Update() {
        CheckHP();
        CheckSelected();
        CheckLevel();
    }    

    protected override void CheckHP() {
        healthBar.value = currentHP;
        if (currentHP > maxHP) {
            currentHP = maxHP;
        }
        if (currentHP <= 0) {
            Die();
        }
    }

    protected override void Die() {
        Player.Instance.unitSpaces -= 5;
        base.Die();
    }
}
