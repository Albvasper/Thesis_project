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
        max_R_Amount = 2000;
        current_R_Amount = max_R_Amount;
        EnemyAI.Instance.GetResourcesAvailable().Add(gameObject);
    }

    protected virtual void Update() {
    }

    public void CheckQuantity() {
        if (current_R_Amount <= 0) {
            Delete();
        }
    }

    public int GiveResource(int amount) {
        current_R_Amount -= amount;
        int result = amount;
        if (current_R_Amount < 0) {
            result = amount + current_R_Amount;
            return result;
        } else {
            return result;
        }
    }

    public string GetResourceType() {
        if (type == ResourceType.MONEY) {
            return "MONEY";
        } 
        else if (type == ResourceType.LINEOFCODE) {
            return "LINEOFCODE";
        } else {
            return "ASSET";
        }
    }

    private void Delete() {
        EnemyAI.Instance.GetResourcesAvailable().Remove(gameObject);
        Destroy(gameObject);
    }
    
}
