using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

    #region Singleton Pattern
    private static UnitController instance;
    public static UnitController Instance { 
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
    #endregion

    private Camera mainCam;

    private void Start() {
        mainCam = Camera.main;
    }

    private void Update() {
        ClickOnUnit();
    }

    private void ClickOnUnit() {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        MobileUnit mobileUnit_Script;
        Unit unit_Script;
        Developer dev_Script;
    
        // Con control puede seleccionar mobile units
        // Con el click izquierdo puedes seleccionar solo una unit del tipo que sea
        // con el click derecho puedes decirle a donde ir a las unidades moviles seleccionadas
        // Con la cajita puedes seleccionar varias unidades moviles nada mas
        
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit)) {
                if (Input.GetKey(KeyCode.LeftControl)) {
                    DeselectStationaryUnits();
                    mobileUnit_Script = hit.collider.GetComponent<MobileUnit>();
                    // Select multiple mobile units (Only mobile units)
                    if (mobileUnit_Script == true && Player.Instance.GetSelectedUnits().Contains(hit.collider.gameObject) == false) {
                        mobileUnit_Script.SetSelect(true);
                        Player.Instance.GetSelectedUnits().Add(hit.collider.gameObject);
                    }
                } else {
                    DeselectUnits();
                    unit_Script = hit.collider.GetComponent<Unit>();
                    // Select only one unit (Of any type)
                    if (unit_Script == true) {
                        unit_Script.SetSelect(true);
                        Player.Instance.GetSelectedUnits().Add(hit.collider.gameObject);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1)) {
            if (Physics.Raycast(ray, out hit)) { 
                // Move mobile units that are selected
                foreach (GameObject unit in Player.Instance.GetSelectedUnits()) {
                    mobileUnit_Script = unit.GetComponent<MobileUnit>();
                    if (mobileUnit_Script == true) { 
                        if (hit.collider.tag == "Resource") {
                            dev_Script = unit.GetComponent<Developer>();
                            /* If units are selected and the player clicks a resource, 
                            the unit will start the farming routine */
                            if (dev_Script == true) {
                                dev_Script.SetState(new GoingToFarm_State(dev_Script, hit.collider.GetComponent<StationaryResource>()));
                            }
                        } else {
                            mobileUnit_Script.SetState(new Walking_State(mobileUnit_Script, hit.point));
                        }
                    }
                }
            }
        }
    }

    private void DeselectStationaryUnits() {
        StationaryUnit stationaryUnit_Script;

        for (int i = 0; i < Player.Instance.GetSelectedUnits().Count; i++) {
            stationaryUnit_Script = Player.Instance.GetSelectedUnits()[i].GetComponent<StationaryUnit>();
            if (stationaryUnit_Script == true) { 
                stationaryUnit_Script.SetSelect(false);
                Player.Instance.GetSelectedUnits().Remove(Player.Instance.GetSelectedUnits()[i]);
            }
        }
    }

    private void DeselectUnits() {
        foreach (GameObject unit in Player.Instance.GetSelectedUnits()) {
            unit.GetComponent<Unit>().SetSelect(false);
        }
        Player.Instance.GetSelectedUnits().Clear();
    }
}