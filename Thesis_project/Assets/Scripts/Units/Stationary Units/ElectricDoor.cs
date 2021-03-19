using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDoor : StationaryUnit {
    
    [SerializeField] private GameObject doorA;
    [SerializeField] private GameObject doorB;
    [SerializeField] private Transform openedDoorPosA;
    [SerializeField] private Transform openedDoorPosB;
    [SerializeField] private Collider sensor;
    private Transform initPosA;
    private Transform initPosB;

    protected override void Start() {
        base.Start();
        initPosA = doorA.transform;
        initPosB = doorB.transform;
    }

    protected override void Update() {
        base.Update(); 
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "MobileUnit") {
            OpenDoors();
        }
    }

    private void OnTriggerExit(Collider other) {
        CloseDoors();
    }

    private void OpenDoors() {
        doorA.transform.Translate(openedDoorPosA.position);
        doorB.transform.Translate(openedDoorPosB.position);
    }

    private void CloseDoors() {
        doorA.transform.Translate(initPosA.position);
        doorB.transform.Translate(initPosB.position);
    }
}
