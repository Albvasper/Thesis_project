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
    [SerializeField] private bool aiElectricDoor;

    protected override void Start() {
        maxHP = 790;
        attackDamage = 0;
        base.Start();
        aiUnit = aiElectricDoor;
        EnableNavMeshPath();
    }

    protected override void Update() {
        base.Update(); 
    }

    private void OnTriggerStay(Collider other) {
        if (aiUnit == true) {
            if (other.tag == "EnemyUnit") {
                OpenDoors();
                EnableNavMeshPath();
            } else {
                CloseDoors();
                DisableNavMeshPath();
            }
        } else {
            if (other.tag == "Unit") {
                OpenDoors();
                EnableNavMeshPath();
            } else {
                CloseDoors();
                DisableNavMeshPath();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        CloseDoors();
        EnableNavMeshPath();
    }

    private void OpenDoors() {
        doorA.SetActive(false);
        doorB.SetActive(false);
    }

    private void CloseDoors() {
        doorA.SetActive(true);
        doorB.SetActive(true);
    }

    private void DisableNavMeshPath() {
        doorAobstacle.enabled = true;
        doorBobstacle.enabled = true;
    }

    private void EnableNavMeshPath() {
        doorAobstacle.enabled = false;
        doorBobstacle.enabled = false;
    }

    protected override void Die() {
        Destroy(parentGO);
    }
}
