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
    protected Player playerScript;
    protected int level;

    protected virtual void Start() {
        actions = new List<Action>();
        GameObject player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        selected = false;
        maxHP = 100;
        currentHP = maxHP;
        healthBar.maxValue = maxHP;
    }

    protected virtual void Update() {
        CheckHP();
        CheckSelected();
        CheckLevel();
    }

    protected void CheckSelected() {

    }
    
    public void TakeDamage(int dmg) {
        currentHP -= dmg;
    }

    protected void CheckLevel() {
        level = playerScript.baseLevel;
        // Change unit look based on the player's base level
    }

    protected virtual void CheckHP() {
        healthBar.value = currentHP;
        if (currentHP > maxHP) {
            currentHP = maxHP;
        }
        if (currentHP <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        Destroy(obj);
    }
}