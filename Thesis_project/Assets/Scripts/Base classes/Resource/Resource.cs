using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    public GameObject obj;
    protected int max_R_Amount;
    protected int current_R_Amount;
    protected Player playerScript;
    
    protected virtual void Start() {
        max_R_Amount = 1000;
        current_R_Amount = max_R_Amount;
        GameObject player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
    }

    protected virtual void Update() {
        CheckQuantity();
    }

    protected void CheckQuantity() {
        if (current_R_Amount <= 0) {
            Delete();
        }
    }

    public void GiveResource(int amount) {
        current_R_Amount -= amount;
    }

    private void Delete() {
        Destroy(obj);
    }
}
