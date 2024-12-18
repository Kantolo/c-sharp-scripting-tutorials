using UnityEngine;

public class FirePointMouseRotate : MonoBehaviour
{
    private Transform parent;
    private Vector3 _mouseDirection;
    private float _maxDistance;

    private Transform pivot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = transform.parent;
        pivot = parent.transform;
        transform.position += Vector3.up * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 parentVector = Camera.main.WorldToScreenPoint(parent.position);
        parentVector = Input.mousePosition - parentVector;
        float angle = Mathf.Atan2(parentVector.y, parentVector.x) * Mathf.Rad2Deg;

        pivot.position = parent.position;

        pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
