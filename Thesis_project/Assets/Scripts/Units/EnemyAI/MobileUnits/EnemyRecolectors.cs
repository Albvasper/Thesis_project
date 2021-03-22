using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecolectors : MobileUnit {

    private int efficiency;
    private bool farming;
    private StationaryResource currentResource;
    private string resourceType;
    private int resourceAmount;  

    protected override void Start() {
        base.Start();
        farming = false;
        efficiency = 10;
        resourceAmount = 0;
        resourceType = "";
        currentResource = null;
    }

     protected override void Update() {
        base.Update(); 
    }
    
    public int GetEfficiency() {
        return efficiency;
    }

    public void SetCurrentResource(StationaryResource currentRes) {
        currentResource = currentRes;
    }

    public void ReturnToBase() {
        MoveUnit(EnemyAI.Instance.transform.position);
    }

    public void SetIsFarming(bool isFarming) {
        farming = isFarming;
    }

    public bool IsFarming() {
        return farming;
    }

    public StationaryResource GetCurrentResource() {
        return currentResource;
    }

    public void ReceiveResource(int amount) {
        resourceAmount = amount;
        resourceType = currentResource.GetResourceType();
    }

    public int GiveResource() {
        int result = resourceAmount;
        resourceAmount = 0;
        return result;
    }

    public string GetResourceType() {
        return resourceType;
    }
}
