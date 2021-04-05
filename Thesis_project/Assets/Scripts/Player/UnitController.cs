using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.EventSystems;

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
    [SerializeField] private GameObject unitPropertiesWindow;
    [SerializeField] private Text unitPropertiesWindowNameField;
    [SerializeField] private GameObject unitCredentialPanel;
    [SerializeField] private GameObject stationaryPropertiesPanel;
    [SerializeField] private List<GameObject> TaskListsForUnits = new List<GameObject>();
    [SerializeField] private EventSystem eventSystem;
    
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
        Intern intern_Script;
    
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
                    if (eventSystem.IsPointerOverGameObject() == false) { 
                        DeselectUnits();
                    }
                    unit_Script = hit.collider.GetComponent<Unit>();
                    // Select only one unit (Of any type)
                    if (unit_Script == true) {
                        unit_Script.Select();
                        // Modify unit properties window fields
                        unitPropertiesWindowNameField.text = hit.collider.name;
                        unitPropertiesWindow.SetActive(true);
                        // Hide unit properties window
                        unitCredentialPanel.SetActive(false);
                        stationaryPropertiesPanel.SetActive(false);
                        // Check unit type
                        mobileUnit_Script = hit.collider.GetComponent<MobileUnit>();
                        StationaryUnit stationaryUnit_Script = hit.collider.GetComponent<StationaryUnit>();
                        ShowUnitTaskList(unit_Script);
                        if (mobileUnit_Script == true) {
                            unitCredentialPanel.SetActive(true);
                        } 
                        else if (stationaryUnit_Script == true) {
                            stationaryPropertiesPanel.SetActive(true);
                        }
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
                            intern_Script = unit.GetComponent<Intern>();
                            /* If units are selected and the player clicks a resource, 
                            the unit will start the farming routine */
                            if (intern_Script == true) {
                                intern_Script.SetState(new GoingToFarm_State(intern_Script, hit.collider.GetComponent<StationaryResource>()));
                            }
                        }
                        else if (hit.collider.tag == "EnemyUnit") {
                            mobileUnit_Script.SetState(new ApproachingEnemy_State(mobileUnit_Script, hit.collider.GetComponent<Unit>()));
                        } else {
                            mobileUnit_Script.SetState(new Walking_State(mobileUnit_Script, hit.point));
                        }
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            DeselectUnits();
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
        for (int i = 0; i < Player.Instance.GetSelectedUnits().Count; i++) {
            Player.Instance.GetSelectedUnits()[i].GetComponent<Unit>().Deselect();
        }
        unitPropertiesWindow.SetActive(false);
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

    private void ShowUnitTaskList(Unit unit) {
        foreach (GameObject go in TaskListsForUnits) {
            go.SetActive(false);
        }
        if (unit.gameObject.layer == 9) {
            // If unit is stationary unit
            if (unit.GetComponent<Studio>() == true) {
                TaskListsForUnits[0].SetActive(true);
            }
            else if (unit.GetComponent<RecluterDev>() == true) {
                TaskListsForUnits[2].SetActive(true);
            }
            else if (unit.GetComponent<RecluterDesigner>() == true) {
                TaskListsForUnits[3].SetActive(true);
            }
            else if (unit.GetComponent<RecluterArtist>() == true) {
                TaskListsForUnits[4].SetActive(true);
            }
        } else if (unit.gameObject.layer == 8) {
            // If unit is mobile unit
            if (unit.GetComponent<Intern>() == true) {
                TaskListsForUnits[1].SetActive(true);
            }
        }
    }
}