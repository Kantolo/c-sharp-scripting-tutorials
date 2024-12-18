using UnityEngine;

public class BasicProjectileHandler : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("PlayerProjectile") && !collision.gameObject.CompareTag("MovingObstacleWaypoint") && !collision.gameObject.CompareTag("Untagged"))
        {
            Debug.Log(collision.name);
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
