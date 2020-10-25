using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Player resources
    public int money;
    public int assets;
    public int linesOfCode;
    // Player units
    public List<GameObject> units;
    public List<GameObject> selectedUnits;

    //public List<Unit> units = new List<Unit>();
    //public List<Unit> selectedUnits = new List<Unit>();

    private void Start() {
        units = new List<GameObject>();
        selectedUnits = new List<GameObject>();
    }

    private void Update() {
    //     foreach (Unit unit in selectedUnits) {
    //         unit.selected = true;
    //     }
    }
}
