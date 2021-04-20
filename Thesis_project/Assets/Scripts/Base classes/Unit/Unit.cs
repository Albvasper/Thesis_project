using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour {

    public Slider healthBar;
    [SerializeField] protected GameObject selectionSprite;
    protected bool aiUnit;
    protected bool isAttacking;
    protected bool selected;
    protected int maxHP;
    protected int currentHP;
    protected List<Action> actions;
    protected int level;
    protected int attackDamage;
    
    protected virtual void Start() {
        actions = new List<Action>();
        selected = false;
        currentHP = maxHP;
        healthBar.maxValue = maxHP;
        if (gameObject.tag == "EnemyUnit") {
            aiUnit = true;
        }
    }

    protected virtual void Update() {
        CheckHP();
        CheckLevel();
    }

    public GameObject GetGameObject() {
        return gameObject;
    }

    public void Attack(Unit enemyUnit) {
        enemyUnit.TakeDamage(attackDamage, this);
    }

    public void TakeDamage(int dmg, Unit attacker) {
        currentHP -= dmg;
        if (aiUnit == true) {
            EnemyAI.Instance.IsBeingAttacked(attacker);
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
            Debug.Log("Wat");
            Die();
        }
    }

    public bool isAIUnit() {
        return aiUnit;
    }

    public bool IsAttacking() {
        return isAttacking;
    }

    public void CurrentlyAttacking() {
        isAttacking = true;
    }

    public void NotAttacking() {
        isAttacking = false;
    }

    protected virtual void Die() {
        if (aiUnit == false) {
            Player.Instance.GetSelectedUnits().Remove(gameObject);
            EnemyAI.Instance.GetCurrentAttackers().Remove(gameObject);
        }
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