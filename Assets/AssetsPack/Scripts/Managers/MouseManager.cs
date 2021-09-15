using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseManager : MonoBehaviour {

    public static MouseManager instance;

    private RaycastHit hitInfo;
    public event Action<Vector3> onMouseClicked;

    public Texture2D point, doorway, attack, target, arrow;

    private void Awake() {
        if (instance != null) {
            Destroy(instance);
        }

        instance = this;
    }

    private void Update() {
        SetCursorTexture();
        ControlMouse();
    }

    private void SetCursorTexture() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo)) {
            switch (hitInfo.collider.gameObject.tag) {
                case "Ground":
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;
            }
        }
    }

    private void ControlMouse() {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null) {
            if (hitInfo.collider.gameObject.CompareTag("Ground")) {
                onMouseClicked?.Invoke(hitInfo.point);
            }
        }
    }
}
