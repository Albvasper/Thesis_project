using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour {

    public Slider healthBar;
    [SerializeField] protected GameObject selectionSprite;
    protected bool selected;
    protected int maxHP;
    protected int currentHP;
    protected List<Action> actions;
    protected int level;
    protected int attackDamage;
    [SerializeField] protected bool aiUnit;

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
        if (aiUnit == true) {
            EnemyAI.Instance.IsBeingAttacked(true);
        }
    }

    protected void CheckLevel() {
        if (aiUnit == false) {
            level = Player.Instance.GetBaseLvl();
        } else {
            level = EnemyAI.Instance.GetBaseLvl();
        }
    }

    protected void CheckHP() {
        healthBar.value = currentHP;
        if (currentHP > maxHP) {
            currentHP = maxHP;
        }
        if (currentHP <= 0) {
            Die();
        }
    }

    public bool isAIUnit() {
        return aiUnit;
    }

    protected abstract void Die();

    public void Select() {
        selected = true;
        selectionSprite.SetActive(true);
    }

    public void Deselect() {
        selected = false;
        selectionSprite.SetActive(false);
    }
}