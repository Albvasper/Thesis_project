/*
=============================================================================
 *  Description: Class that manages the players resources, units and actions.
=============================================================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    //Singleton pattern
    private static Player instance;
    public static Player Instance { 
        get { 
            return instance; 
        } 
    }
    
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    [SerializeField]
    private int baseLevel;
    // Player resources
    [SerializeField]
    private int money;
    [SerializeField]
    private int assets;
    [SerializeField]
    private int linesOfCode;
    // Player units
    [SerializeField]
    private List<GameObject> units;
    [SerializeField]
    private List<GameObject> selectedUnits;
    [SerializeField]
    private int unitSpaces;
    [SerializeField]
    private int maxUnitSpaces;
    // Nav mesh
    //public NavMeshSurface navMeshSurface;
    //navMeshSurface.BuildNavMesh; METHOD THAT REBAKES THE MESH!!!!!!!!!!!!!!
    
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
        BoundSpaces();
        ClickOnUnit();
    }

    private void BoundSpaces () {
        if (unitSpaces < 0) {
            unitSpaces = 0;
        }
        if (unitSpaces > maxUnitSpaces) {
            unitSpaces = maxUnitSpaces;
        }
    }

    // Con control puede seleccionar mobile units
    // Con el click izquierdo puedes seleccionar solo una unit del tipo que sea
    // con el click derecho puedes decirle a donde ir a las unidades moviles seleccionadas

    // Con la cajita puedes seleccionar varias unidades moviles nada mas

    private void ClickOnUnit() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit)) {
                if (Input.GetKey(KeyCode.LeftControl)) {
                    DeselectStationaryUnits();
                    // Select multiple mobile units (Only mobile units)
                    if (hit.collider.GetComponent<MobileUnit>() == true && selectedUnits.Contains(hit.collider.gameObject) == false) {
                                                                                            // Bug: You can put the same object more than once!
                        hit.collider.GetComponent<MobileUnit>().SetSelect(true);
                        selectedUnits.Add(hit.collider.gameObject);
                    }
                } else {
                    DeselectUnits();
                    // Select only one unit (Of any type)
                    if (hit.collider.GetComponent<Unit>() == true) {
                        hit.collider.GetComponent<Unit>().SetSelect(true);
                        selectedUnits.Add(hit.collider.gameObject);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1)) {
            if (Physics.Raycast(ray, out hit)) { 
                // Move mobile units that are selected
                foreach (GameObject mobileUnit in selectedUnits){
                    if (mobileUnit.GetComponent<MobileUnit>() == true) { 
                        mobileUnit.GetComponent<MobileUnit>().MoveUnit(hit.point);
                    }
                }
            }
        }
    }

    private void DeselectStationaryUnits() {
        for (int i = 0; i < selectedUnits.Count; i++) {
             if (selectedUnits[i].GetComponent<StationaryUnit>() == true) { 
                selectedUnits[i].GetComponent<StationaryUnit>().SetSelect(false);
                selectedUnits.Remove(selectedUnits[i]);
            }
        }
    }

    private void DeselectUnits() {
        foreach (GameObject unit in selectedUnits) {
            unit.GetComponent<Unit>().SetSelect(false);
        }
        selectedUnits.Clear();
    }

    public void AddBaseLvl() {
        baseLevel += 1;
    }

    public int GetBaseLvl() {
        return baseLevel;
    }

    public void AddMoney(int amount) {
        money += amount;
    }

    public void AddLinesOfCode(int amount) {
        linesOfCode += amount;
    }

    public void AddAssets(int amount) {
        assets += amount;
    }

    public void AddUnitSpaces(int amount) {
        unitSpaces += amount;
    }

    public void SubstractUnitSpaces(int amount) {
        unitSpaces -= amount;
    }
}
