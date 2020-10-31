using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour { 
    
    //private Vector3 pos;
    //private int rows;
    //private int cols;
    private float size;

    private void Awake() {
        size = 4f;
    }

    public Vector3 GetGridPoint(Vector3 pos) {
        pos -= transform.position;
        int x = Mathf.RoundToInt(pos.x / size);
        int y = Mathf.RoundToInt(pos.y / size);
        int z = Mathf.RoundToInt(pos.z / size);
        Vector3 r = new Vector3 (
                                (float)x * size, 
                                (float)y * size,
                                (float)z * size
                                );
        r += transform.position;
        return r;
    }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.white;
    //     for (float x = 0; x < 30; x += size) {
    //         for (float z = 0; z < 30; z += size) {
    //             var point = GetGridPoint(new Vector3(x, 0f, z));
    //             Gizmos.DrawSphere(point, 0.1f);
    //         }
    //     }
    // }
}
    