using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDoorSensor : MonoBehaviour {
    
    [SerializeField] private ElectricDoor electricDoor;

    private void OnTriggerEnter(Collider other) {
        if (electricDoor.GetIfAIUnit() == true) {
            if (other.gameObject.tag == "EnemyUnit") {
                OpenDoors();
            } else {
                CloseDoors();
            }
        } else {
            if (other.gameObject.tag == "Unit" && electricDoor.IsLocked() == false) {
                OpenDoors();
            } else {
                CloseDoors();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        CloseDoors();
    }

    private void OpenDoors() {
        electricDoor.GetDoorA().SetActive(false);
        electricDoor.GetDoorB().SetActive(false);
    }

    private void CloseDoors() {
        electricDoor.GetDoorA().SetActive(true);
        electricDoor.GetDoorB().SetActive(true);
    }
}
