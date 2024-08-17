using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    [SerializeField]
    private Vector2 boundsDimension;
    [SerializeField]
    private float innerOffset;
    BoxCollider2D bc;

    private float xMax = 0;
    private float xMin = 0;
    private float yMin = 0;
    private float yMax = 1;

    private void Awake() {
        bc = GetComponent<BoxCollider2D>();
        bc.size = boundsDimension;
        xMin = -bc.size.x / 2;
        xMax = bc.size.x / 2;
        yMin = -bc.size.y / 2;
        yMax = bc.size.y / 2;
    }

    private void OnTriggerExit2D(Collider2D other) {
        // Debug.Log(other.name +  " has left the border");
        Vector3 objPos = other.transform.position;
        Vector2 newPos = objPos;
        if (objPos.x > xMax) {
            newPos.x = xMin + innerOffset;
        }
        if (objPos.x < xMin) {
            newPos.x = xMax - innerOffset;
        }
        if (objPos.y > yMax) {
            newPos.y = yMin + innerOffset;
        }
        if (objPos.y < yMin) {
            newPos.y = yMax - innerOffset;
        }
        // Debug.Log("Tp object to: " + newPos);
        other.transform.position = newPos;
    }
}
