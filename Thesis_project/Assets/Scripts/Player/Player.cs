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

    private Camera mainCam;
    private int baseLevel;
    // Player resources
    private int money;
    private int assets;
    private int linesOfCode;
    // Player units
    private List<GameObject> units;
    private List<GameObject> selectedUnits;
    private int unitSpaces;
    private int maxUnitSpaces;
    // Nav mesh
    //public NavMeshSurface navMeshSurface;
    //navMeshSurface.BuildNavMesh; METHOD THAT REBAKES THE MESH!!!!!!!!!!!!!!
    
    private void Start() {
        mainCam = Camera.main;
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
    }

    private void BoundSpaces () {
        if (unitSpaces < 0) {
            unitSpaces = 0;
        }
        if (unitSpaces > maxUnitSpaces) {
            unitSpaces = maxUnitSpaces;
        }
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

    public List<GameObject> GetSelectedUnits() {
        return selectedUnits;
    }
}
