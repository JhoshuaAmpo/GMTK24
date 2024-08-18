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

    public float XMax { get; private set; } = 0;
    public float XMin { get; private set; } = 0;
    public float YMin { get; private set; } = 0;
    public float YMax { get; private set; } = 1;

    private void Awake() {
        bc = GetComponent<BoxCollider2D>();
        bc.size = boundsDimension;
        XMin = -bc.size.x / 2;
        XMax = bc.size.x / 2;
        YMin = -bc.size.y / 2;
        YMax = bc.size.y / 2;
    }

    private void OnTriggerExit2D(Collider2D other) {
        // Debug.Log(other.name +  " has left the border");
        Vector3 objPos = other.transform.position;
        Vector2 newPos = objPos;
        if (objPos.x > XMax) {
            newPos.x = XMin + innerOffset;
        }
        if (objPos.x < XMin) {
            newPos.x = XMax - innerOffset;
        }
        if (objPos.y > YMax) {
            newPos.y = YMin + innerOffset;
        }
        if (objPos.y < YMin) {
            newPos.y = YMax - innerOffset;
        }
        // Debug.Log("Tp object to: " + newPos);
        other.transform.position = newPos;
    }
}
