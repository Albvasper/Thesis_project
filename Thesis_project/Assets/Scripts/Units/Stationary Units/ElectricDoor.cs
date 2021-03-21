using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDoor : StationaryUnit {
    
    [SerializeField] private GameObject doorA;
    [SerializeField] private GameObject doorB;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update(); 
    }

    private void OnTriggerEnter(Collider other) {
        OpenDoors();
    }

    private void OnTriggerExit(Collider other) {
        CloseDoors();
    }

    private void OpenDoors() {
        doorA.SetActive(false);
        doorB.SetActive(false);
    }

    private void CloseDoors() {
        doorA.SetActive(true);
        doorB.SetActive(true);
    }
}
