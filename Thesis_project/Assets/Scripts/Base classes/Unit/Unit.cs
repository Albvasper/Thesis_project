using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

    public GameObject obj;
    public Slider healthBar;
    protected bool selected;
    protected int maxHP;
    protected int currentHP;
    protected List<Action> actions;

    protected virtual void Start() {
        actions = new List<Action>();
        selected = false;
        maxHP = 100;
        currentHP = maxHP;
        healthBar.maxValue = maxHP;
    }

    protected virtual void Update() {
        CheckHP();
        CheckSelected();
    }

    protected void CheckSelected() {

    }
    
    public void TakeDamage(int dmg) {
        currentHP -= dmg;
    }

    protected void CheckHP() {
        healthBar.value = currentHP;
        if (currentHP <= 0) {
            Die();
        }
    }

    protected void Die() {
        Destroy(obj);
    }

}