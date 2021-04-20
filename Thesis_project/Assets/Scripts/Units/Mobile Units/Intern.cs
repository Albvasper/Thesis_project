using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intern : MobileUnit {

    private int efficiency;
    private bool farming;
    private StationaryResource currentResource;
    private string resourceType;
    private int resourceAmount;                     // Amount of resource that the Intern is carrying right now

    protected override void Start() {
        maxHP = 100;
        attackDamage = 5;
        base.Start();
        efficiency = 10;
        //farming = false;
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

    // public void GatherResource(StationaryResource resource) {
    //     if (resource != null) {
    //         farming = true;
    //         currentResource = resource;
    //         MoveUnit(resource.transform.position);
    //     } else {
    //         farming = false;
    //     }
    // }

    public void SetCurrentResource(StationaryResource currentRes) {
        currentResource = currentRes;
    }

    public void ReturnToStudio() {
        MoveUnit(Studio.Instance.transform.position);
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
