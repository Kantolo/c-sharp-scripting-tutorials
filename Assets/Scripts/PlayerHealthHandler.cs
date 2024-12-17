using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 50;
    [SerializeField] private float _currentHealth;

    private bool isDead = false;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Heal(0.1f);

        if (isDead)
        {
            Debug.Log("Player dead");

            
        }
    }

    public void Heal(float healing)
    {
        if (!isDead)
        {
            if (_currentHealth + healing > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            else
            {
                _currentHealth += healing;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("player takes " + damage + " damage");

        if (!isDead)
        {
            if (_currentHealth - damage <= 0)
            {
                isDead = true;
            }
            else
            {
                _currentHealth -= damage;
            }
        }
    }

    public float GetHealth()
    {
        return _currentHealth;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
