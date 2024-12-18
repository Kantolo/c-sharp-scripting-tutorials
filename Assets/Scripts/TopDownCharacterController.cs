using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A class to control the top down character.
/// Implements the player controls for moving and shooting.
/// Updates the player animator so the character animates based on input.
/// </summary>
public class TopDownCharacterController : MonoBehaviour
{
    #region Framework Variables

    //The inputs that we need to retrieve from the input system.
    private InputAction _moveAction;
    private InputAction _attackAction;

    //The components that we need to edit to make the player move smoothly.
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    
    //The direction that the player is moving in.
    private Vector2 _playerDirection;
   

    [Header("Movement parameters")]
    //The speed at which the player moves
    [SerializeField] private float _playerSpeed = 200f;
    //The maximum speed the player can move
    [SerializeField] private float _playerMaxSpeed = 1000f;

    [Header("Projectile Parameters")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _fireRate;
    private float _fireTimeout = 0;
    private Vector2 mousePos;
    private Vector2 mouseDirection;

    #endregion

    private bool _isDead = false;

    /// <summary>
    /// When the script first initialises this gets called.
    /// Use this for grabbing components and setting up input bindings.
    /// </summary>
    private void Awake()
    {
        //bind movement inputs to variables
        _moveAction = InputSystem.actions.FindAction("Move");
        _attackAction = InputSystem.actions.FindAction("Attack");
        
        //get components from Character game object so that we can use them later.
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// Called after Awake(), and is used to initialize variables e.g. set values on the player
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// When a fixed update loop is called, it runs at a constant rate, regardless of pc performance.
    /// This ensures that physics are calculated properly.
    /// </summary>
    private void FixedUpdate()
    {
        _isDead = GetComponent<PlayerHealthHandler>().IsDead();

        //clamp the speed to the maximum speed for if the speed has been changed in code.
        float speed = _playerSpeed > _playerMaxSpeed ? _playerMaxSpeed : _playerSpeed;
        
        if(!_isDead)
        {
            //apply the movement to the character using the clamped speed value.
            _rigidbody.linearVelocity = _playerDirection * (speed * Time.fixedDeltaTime);
        }
        else
        {
            _rigidbody.linearVelocity = new Vector2(0,0);
            _animator.SetFloat("Speed", 0);
        }
        
    }
    
    /// <summary>
    /// When the update loop is called, it runs every frame.
    /// Therefore, this will run more or less frequently depending on performance.
    /// Used to catch changes in variables or input.
    /// </summary>
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDirection = mousePos - new Vector2(transform.position.x, transform.position.y);
        mouseDirection = mouseDirection.normalized;

        if(!_isDead)
        {
            // store any movement inputs into m_playerDirection - this will be used in FixedUpdate to move the player.
            _playerDirection = _moveAction.ReadValue<Vector2>();

            // ~~ handle animator ~~
            // Update the animator speed to ensure that we revert to idle if the player doesn't move.
            _animator.SetFloat("Speed", _playerDirection.magnitude);

            // If there is movement, set the directional values to ensure the character is facing the way they are moving.
            if (_playerDirection.magnitude > 0)
            {
                _animator.SetFloat("Horizontal", _playerDirection.x);
                _animator.SetFloat("Vertical", _playerDirection.y);
            }

            // check if an attack has been triggered.
            if (_attackAction.IsPressed() && Time.time > _fireTimeout)
            {
                _fireTimeout = Time.time + _fireRate;
                Fire();
            }
        }
        
    }

    void Fire()
    {
        GameObject projectileToSpawn = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);

        if(projectileToSpawn.GetComponent<Rigidbody2D>() != null)
        {
            projectileToSpawn.GetComponent<Rigidbody2D>().AddForce(mouseDirection * _projectileSpeed, ForceMode2D.Impulse);
        }
    }
}
