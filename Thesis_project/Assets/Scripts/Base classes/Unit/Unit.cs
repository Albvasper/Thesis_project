using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

    public Slider healthBar;
    [SerializeField]
    protected GameObject selectionSprite;
    protected bool selected;
    protected int maxHP;
    protected int currentHP;
    protected List<Action> actions;
    protected int level;
    protected int attackDamage;
    
    protected virtual void Start() {
        actions = new List<Action>();
        selected = false;
        maxHP = 100;
        currentHP = maxHP;
        healthBar.maxValue = maxHP;
        attackDamage = 25;
    }

    protected virtual void Update() {
        CheckHP();
        CheckLevel();
    }

    public void Attack(Unit enemyUnit) {
        enemyUnit.TakeDamage(attackDamage);
    }

    public void TakeDamage(int dmg) {
        currentHP -= dmg;
    }

    protected void CheckLevel() {
        // Change unit look based on the player's base level
        level = Player.Instance.GetBaseLvl();
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
        Player.Instance.GetMobileUnits().Remove(gameObject);    // Only do this on mobile units
        Destroy(gameObject);
    }

    public void Select() {
        selected = true;
        selectionSprite.SetActive(true);
    }

    public void Deselect() {
        selected = false;
        selectionSprite.SetActive(false);
    }
}