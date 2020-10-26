using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    public GameObject obj;
    protected int max_R_Amount;
    protected int current_R_Amount;
    protected Player playerScript;
    protected ResourceType type;

    protected enum ResourceType {
        MONEY, LINEOFCODE, ASSET
    }

    protected virtual void Start() {
        GameObject player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        max_R_Amount = 1000;
        current_R_Amount = max_R_Amount;
    }

    protected virtual void Update() {
        CheckQuantity();
    }

    protected void CheckQuantity() {
        if (current_R_Amount <= 0) {
            Delete();
        }
    }

    protected void GiveResource(int amount) {
        current_R_Amount -= amount;
        if (type == ResourceType.MONEY) {
            playerScript.money += amount;
        } 
        else if (type == ResourceType.LINEOFCODE) {
            playerScript.linesOfCode += amount;

        } else {
            playerScript.assets += amount;
        }
    }

    private void Delete() {
        Destroy(obj);
    }
}
