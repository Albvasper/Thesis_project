using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int baseLevel;
    // Player resources
    public int money;
    public int assets;
    public int linesOfCode;
    // Player units
    public List<GameObject> units;
    public List<GameObject> selectedUnits;
    public int unitSpaces;
    public int maxUnitSpaces;
    //public List<Unit> units = new List<Unit>();
    //public List<Unit> selectedUnits = new List<Unit>();

    private void Start() {
        baseLevel = 1;
        money = 0;
        assets = 0;
        linesOfCode = 0;
        units = new List<GameObject>();
        selectedUnits = new List<GameObject>();
        unitSpaces = 5;
        maxUnitSpaces = 300;
    }

    private void Update() {
    //     foreach (Unit unit in selectedUnits) {
    //         unit.selected = true;
    //     }
        CheckSpaces();
    }

    private void CheckSpaces () {
        if (unitSpaces < 0) {
            unitSpaces = 0;
        }
        if (unitSpaces > maxUnitSpaces) {
            unitSpaces = maxUnitSpaces;
        }
    }
}
