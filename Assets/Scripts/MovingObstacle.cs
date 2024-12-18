using Unity.VisualScripting;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private bool _isSawBlade;
    [SerializeField] private float _sawDamage = 20;
    [SerializeField] private Transform[] _waypoints;


    private Transform _target;


    void Start()
    {
        _target = _waypoints[0];
        _speed = _speed / 100;
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingObstacleWaypoint"))
        {
            ChangeTarget();
        }

        if (collision.CompareTag("Player"))
        {
            if (_isSawBlade)
            {
                collision.GetComponent<PlayerHealthHandler>().TakeDamage(_sawDamage);

            }
        }
    }

    private int index = 0;

    void ChangeTarget()
    {
        index++;
        index %= _waypoints.Length;
        _target = _waypoints[index];
    }
}
