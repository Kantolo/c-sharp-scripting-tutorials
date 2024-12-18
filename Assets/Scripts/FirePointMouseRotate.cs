using UnityEngine;

public class FirePointMouseRotate : MonoBehaviour
{
    private Vector2 _mousePos;
    private Vector2 _mouseDirection;
    private float _maxDistance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mouseDirection = new Vector2(transform.position.x, transform.position.y) - _mousePos;
        _mouseDirection.Normalize();

        


    }
}
