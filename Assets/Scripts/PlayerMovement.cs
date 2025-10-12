using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _speed = 3f;
    [SerializeField] float _jumpforce = 3f;
    [SerializeField] float _rotationSpeed = 180f;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] string speedParam = "Speed";
    [SerializeField] string groundedParam = "Grounded";
    [SerializeField] string jumpTrigger = "Jump";
    [SerializeField] float animDamp = 0.1f;

    [Header("Ground Detection")]
    [SerializeField] float _playerHeight = 2f;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _radioSphereCheck = 0.1f;



    bool _isGrounded = true;
    float _moveH, _moveV;
    Vector3 _movement;
    Vector3 _moveDirection;
    Vector3 _moveSideways;
    float _rotationAmmount;
    Quaternion _turnOffset;
    private Rigidbody rb;
    private Camera _mainCamera;

    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        rb.freezeRotation = true;
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    
    void FixedUpdate()
    {

        _isGrounded = Physics.CheckSphere(transform.position,_radioSphereCheck,_groundLayer);

        Inputs();
        Movement();
       
        if (animator)
        {
            float planarSpeed = _movement.magnitude * _speed;
            animator.SetFloat(speedParam, planarSpeed, animDamp, Time.fixedDeltaTime);
            animator.SetBool(groundedParam, _isGrounded);
        }



    }

    void Movement() 
    {
        _moveH = Input.GetAxis("Horizontal");
        _moveV = Input.GetAxis("Vertical");

        _moveDirection = transform.forward * _moveV * _speed * Time.deltaTime;
        _moveSideways = transform.right * _moveH * _speed * Time.deltaTime;




        _movement = rb.position + _moveDirection + _moveSideways;
        rb.MovePosition(_movement);


        _rotationAmmount = _moveH * _rotationSpeed * Time.deltaTime;
    }
    void Inputs() 
    {
        if (_rotationAmmount <= -100)
        {
            _rotationSpeed = 0;
        }
        else if (_rotationAmmount >= 100)
        {
            _rotationSpeed = 0;
        }
        _turnOffset = Quaternion.Euler(0, _rotationAmmount, 0);
        rb.MoveRotation(rb.rotation * _turnOffset);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 8f;
        }
        else
        {
            _speed = 3f;
        }

        if (Input.GetButton("Jump") && _isGrounded)
        {
            rb.linearVelocity += (Vector3.up * _jumpforce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (_playerHeight > 0) 
        {
            Vector3 groundCheckPosition = transform.position;
            Gizmos.DrawWireSphere(groundCheckPosition, _radioSphereCheck);
        }
    }


}
