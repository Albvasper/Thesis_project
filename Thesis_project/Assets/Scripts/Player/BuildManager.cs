using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildManager : MonoBehaviour { 

    #region Singleton Pattern
        private static BuildManager instance;
        public static BuildManager Instance { 
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

    private GameObject placeableObj;
    private GameObject placeableObjPreview;
    private Vector3 offset;
    private Camera mainCam;
    private bool initBuilding;
    private bool  building = true;

    [SerializeField] private NavMeshSurface navMeshSurface;
    
    private void Start() {
        placeableObj = null;
        mainCam = Camera.main;
        initBuilding = false;
        building = true;
    }

    private void Update() {
        BuildStructure();
    }

    public void InitBuilding(GameObject go, GameObject goPrev) {
        placeableObjPreview = go;
        initBuilding = true;
    }

    private void TrackMouse() {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            var finalPos = Grid.Instance.GetGridPoint(hit.point);
            placeableObj.transform.position = finalPos + new Vector3(0, placeableObj.transform.localScale.y / 2, 0);
            if (Input.GetKey(KeyCode.R)) {
                // Rotate structure
                float r = 0.0f;
                r += 0.3f;
                placeableObj.transform.Rotate(0, r, 0);
            }
            //placeableObj.transform.position = hit.point + new Vector3(0, placeableObj.transform.localScale.y / 2, 0);
        }
    }

    public void BuildStructure() {
        if (initBuilding == true) {
            if (placeableObj == null) {
                placeableObj = Instantiate(placeableObjPreview);
                placeableObj.layer = 2;
            }
        }
        if (placeableObj != null) {
            TrackMouse();
            Build();
        }
    }

    private void Build() {
        if (Input.GetMouseButtonDown(0)) {
            placeableObj.layer = 0;
            placeableObj = null;
            building = true;
            // Quit building mode
            initBuilding = false;
        }
    }
}
   