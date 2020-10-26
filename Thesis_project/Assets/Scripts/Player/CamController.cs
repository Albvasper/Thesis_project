/*
=============================================================================
 *  Description: Script that manages the camera movement ingame and cursor 
 *  tracking.
=============================================================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamController : MonoBehaviour {

    //Singleton pattern
    private static CamController instance;
    public static CamController Instance {
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
    private float camVel;
    private int zoomBounds;
    private float zoomRate;
    private Vector2 mousePos;

    private void Start() {
        mainCam = GetComponent<Camera>();
        zoomBounds = 5;
        camVel = 0.05f;
        zoomRate = 4.0f;
        mousePos = Vector2.zero;
    }

    private void Update() {
        GetInput();
        UpdateCursor();
        ZoomCam();
    }
    
    private void GetInput() {
        /* Move camera using keys (Arrows by default) or 
        by moving the cursor close to the bounds of the screen. */

        if (Input.GetKey("up") || mousePos.y > Screen.width / 5) {
            //Move Up
            MoveCamera("up");
        }
        else if (Input.GetKey("down") || mousePos.y < -Screen.width / 5) {
            //Move Down
            MoveCamera("down");
        }
        if (Input.GetKey("left") || mousePos.x < -Screen.width / 3) {
            //Move Left
            MoveCamera("left");
        }
        else if (Input.GetKey("right") || mousePos.x > Screen.width / 3) {
            //Move Right
            MoveCamera("right");
        }
    }

    private void UpdateCursor() {
        // Update mouse position
        mousePos = Input.mousePosition;
        mousePos.x = mousePos.x - Screen.width / 2;
        mousePos.y = mousePos.y - Screen.height / 2;
    }

    private void MoveCamera(string input) {
        switch (input) {
            case "up":
                mainCam.transform.position = mainCam.transform.position + new Vector3(0, 0, camVel);
            break;

            case "down":
                mainCam.transform.position = mainCam.transform.position + new Vector3(0, 0, -camVel);
            break;

            case "left":
                mainCam.transform.position = mainCam.transform.position + new Vector3(-camVel, 0, 0);
            break;

            case "right":
                mainCam.transform.position = mainCam.transform.position + new Vector3(camVel, 0, 0);
            break;
        }
    }

    private void ZoomCam() {
        // Camera zoom in 
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            if (zoomBounds > 1){
                mainCam.transform.position = mainCam.transform.position + new Vector3(0, zoomRate, -zoomRate);
                zoomBounds--;
            } 
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            // Camera zoom out
            if (zoomBounds < 10){
                mainCam.transform.position = mainCam.transform.position + new Vector3(0, -zoomRate, zoomRate);
                zoomBounds++;
            } 
        }
    }
}
