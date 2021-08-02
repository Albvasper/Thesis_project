using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElectricDoor : StationaryUnit {

    [SerializeField] private GameObject parentGO;
    [SerializeField] private GameObject doorA;
    [SerializeField] private GameObject doorB;
    [SerializeField] private NavMeshObstacle doorAobstacle;
    [SerializeField] private NavMeshObstacle doorBobstacle;
    [SerializeField] private bool locked;
    [SerializeField] private bool aiElectricDoor;

    protected override void Start() {
        maxHP = 790;
        attackDamage = 0;
        base.Start();
        aiUnit = aiElectricDoor;
    }

    protected override void Update() {
        base.Update(); 
    }

    protected override void Die() {
        Destroy(parentGO);
    }

    public GameObject GetDoorA() {
        return doorA;
    }
    
    public GameObject GetDoorB() {
        return doorB;
    }
    
    public bool GetIfAIUnit() {
        return aiElectricDoor;
    }

    public bool IsLocked() {
        return locked;
    }

    public void Unlock() {
        locked = false;
    }
}
