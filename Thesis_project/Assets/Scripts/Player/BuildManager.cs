using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour { 

    //Singleton pattern
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

    [SerializeField]
    private GameObject placeableObjPreview;
    private GameObject placeableObj;
    private Vector3 offset;
    private Camera mainCam;
    
    private void Start() {
        placeableObj = null;
        mainCam = Camera.main;
    }

    private void Update() {
        PreviewBuild();
    }

    private void TrackMouse() {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            var finalPos = Grid.Instance.GetGridPoint(hit.point);
            placeableObj.transform.position = finalPos + new Vector3(0, placeableObj.transform.localScale.y / 2, 0);
            //placeableObj.transform.position = hit.point + new Vector3(0, placeableObj.transform.localScale.y / 2, 0);
        }
    }

    private void PreviewBuild() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (placeableObj == null) {
                placeableObj = Instantiate(placeableObjPreview);
                placeableObj.layer = 2;
            } else {
                // Salir del modo construcción
                Destroy(placeableObj);
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
        }
    }
}
   