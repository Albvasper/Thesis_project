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
    public RectTransform selectionBox;
    private Vector2 selectA;

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
    
        if (Input.GetMouseButtonDown(0)) {
            selectA = Input.mousePosition;
            if (Physics.Raycast(ray, out hit)) {
                if (Input.GetKey(KeyCode.LeftControl)) {
                    DeselectStationaryUnits();
                    mobileUnit_Script = hit.collider.GetComponent<MobileUnit>();
                    // Select multiple mobile units (Only mobile units)
                    if (mobileUnit_Script == true) {
                        if (Player.Instance.GetSelectedUnits().Contains(hit.collider.gameObject) == false) {
                            mobileUnit_Script.Select();
                            Player.Instance.GetSelectedUnits().Add(hit.collider.gameObject);
                        } else {
                            mobileUnit_Script.Deselect();
                            Player.Instance.GetSelectedUnits().Remove(hit.collider.gameObject);
                        }
                    }
                } else {
                    DeselectUnits();
                    unit_Script = hit.collider.GetComponent<Unit>();
                    // Select only one unit (Of any type)
                    if (unit_Script == true) {
                        unit_Script.Select();
                        Player.Instance.GetSelectedUnits().Add(hit.collider.gameObject);
                    }
                }
            }
        }
        else if (Input.GetMouseButton(0)) {
            ResizeSelectionBox(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0)) {
            ReleaseSelectionBox();
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
                        }
                        else if (hit.collider.tag == "Unit") {
                            mobileUnit_Script.SetState(new ApproachingEnemy_State(mobileUnit_Script, hit.collider.GetComponent<Unit>()));
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
                stationaryUnit_Script.Deselect();
                Player.Instance.GetSelectedUnits().Remove(Player.Instance.GetSelectedUnits()[i]);
            }
        }
    }

    private void DeselectUnits() {
        foreach (GameObject unit in Player.Instance.GetSelectedUnits()) {
            unit.GetComponent<Unit>().Deselect();
        }
        Player.Instance.GetSelectedUnits().Clear();
    }

    private void ResizeSelectionBox(Vector2 mousePos) {
        selectionBox.gameObject.SetActive(true);
        float w = mousePos.x - selectA.x;
        float h = mousePos.y - selectA.y;
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(w), Mathf.Abs(h));
        selectionBox.anchoredPosition = selectA + new Vector2(w / 2, h / 2);
    }

    private void ReleaseSelectionBox() {
        selectionBox.gameObject.SetActive(false);
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);
        foreach (GameObject unit in Player.Instance.GetMobileUnits()) {
            MobileUnit mobileUnit_Script = unit.GetComponent<MobileUnit>();
            Vector3 screenPos = mainCam.WorldToScreenPoint(unit.transform.position);
            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y) {
                if (Player.Instance.GetSelectedUnits().Contains(unit) == false) {
                    mobileUnit_Script.Select();
                    Player.Instance.GetSelectedUnits().Add(unit);
                }
            }
        }
    }
}