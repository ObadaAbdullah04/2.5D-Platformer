using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] float _speed = 1f;
    [SerializeField] float _gravity = 1f;
    [SerializeField] float _jumpHeight = 1f;
    float _yVelocity; // Caching the vertical velocity so it doesn't reset every frame

    bool _canDoubleJump = false;

    // UI
    private UIManager _uiManager;
    private int _score = 0;
    private int _lifes = 3;

    private Vector3 _startPosition;
    private Vector3 _velocity;

    private bool _canWallJump;
    private Vector3 _wallSurfaceNormal;

    [SerializeField] private float _pushPower = 1f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = FindObjectOfType<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene.");
        }
        _uiManager.UpdateScore(_score);
        _uiManager.UpdateLives(_lifes);


        _startPosition = transform.position;
    }

    void Update()
    {
        Movement();
        TakeDamage();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        
        if (!_canWallJump) _velocity.x = horizontal * _speed; // You can't controll xAxis while wall jumping

        if (_controller.isGrounded)
        {
            _canWallJump = false;
            _canDoubleJump = true;

            if (jumpPressed)
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            if (jumpPressed)
            {
                if (_canWallJump)
                {
                    _yVelocity = _jumpHeight;
                    _velocity = _wallSurfaceNormal * _speed;
                }
                else if (_canDoubleJump)
                {
                    _yVelocity = _jumpHeight;
                    _canDoubleJump = false;
                }
            }
            _yVelocity -= _gravity * Time.deltaTime;
        }

        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);

        // If we hit the ceiling, cancel upward velocity
        if ((_controller.collisionFlags & CollisionFlags.Above) != 0 && _yVelocity > 0)
        {
            _yVelocity = 0;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Moving_Box"))
        {
            Rigidbody box_rb = hit.collider.GetComponent<Rigidbody>();

            if (box_rb != null)
            {
                // We don't push objects below us
                if (hit.moveDirection.y < -0.3f) return;

                Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, 0);
                box_rb.velocity = pushDirection * _pushPower; // Apply the push
            }
        }

        if (!_controller.isGrounded && hit.transform.CompareTag("Wall"))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _wallSurfaceNormal = hit.normal;
            _canWallJump = true;
            Debug.Log("Wall_Jumping");
        }
    }

    public int GetScore()
    {
        return _score;
    }

    public void AddScore()
    {
        _score++;
        _uiManager.UpdateScore(_score);
        Debug.Log("Score: " + _score);
    }

    public void TakeDamage()
    {
        if (transform.position.y < -10)
        {
            if (_lifes > 0)
            {
                _lifes--;
                _uiManager.UpdateLives(_lifes);
                Debug.Log("Lives: " + _lifes);
                transform.position = _startPosition;
            }

            else if (_lifes <= 0)
            {
                _lifes = 0;
                Debug.Log("Game Over");
                Destroy(gameObject);
            }
            _controller.enabled = false;
        }
        else
        {
            _controller.enabled = true;
        }
    }
}
