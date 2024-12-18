using UnityEngine;

public class DestroyOnLeavingCamera : MonoBehaviour
{
    [SerializeField] private float _despawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, _despawnTime);
    }
}
