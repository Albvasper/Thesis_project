using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    protected int max_R_Amount;
    protected int current_R_Amount;
    protected ResourceType type;

    protected enum ResourceType {
        MONEY, LINEOFCODE, ASSET
    }

    protected virtual void Start() {
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
            Player.Instance.AddMoney(amount);
        } 
        else if (type == ResourceType.LINEOFCODE) {
            Player.Instance.AddLinesOfCode(amount);

        } else {
            Player.Instance.AddAssets(amount);
        }
    }

    private void Delete() {
        Destroy(gameObject);
    }
}
