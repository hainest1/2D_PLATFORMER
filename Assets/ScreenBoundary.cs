using UnityEngine;
using System.Collections;

public class ScreenBoundary : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

        var cam = Camera.main;
        if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

        var dist = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight / 2, 0));
        var col = GetComponent<BoxCollider2D>() == null ? gameObject.AddComponent<BoxCollider2D>() : GetComponent<BoxCollider2D>();
        col.transform.position = dist;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
