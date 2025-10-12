using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement1 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float rotationSpeed = 180f;
    [SerializeField] float jumpForce = 5f;

    [Header("Animation")]
    [SerializeField] Animator animator;         // Asign� el Animator del personaje
    [SerializeField] string speedParam = "Speed";
    [SerializeField] string groundedParam = "Grounded";
    [SerializeField] string jumpTrigger = "Jump";
    [SerializeField] float animDamp = 0.1f;     // Suavizado para el par�metro Speed

    Rigidbody rb;
    bool isGrounded = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // evita que la f�sica lo rote
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        // --- Input ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool run = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = run ? runSpeed : walkSpeed;

        // Direcci�n de movimiento en el plano XZ (strafe + adelante/atr�s)
        Vector3 move = (transform.forward * v) + (transform.right * h);
        if (move.sqrMagnitude > 1f) move.Normalize();

        // --- Movimiento ---
        if (move.sqrMagnitude > 0f)
        {
            rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);
        }

        // --- Rotaci�n con A/D (opcional) ---
        // Si prefer�s strafe SIN rotar con A/D, coment� estas dos l�neas.
        float rotationAmount = h * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotationAmount, 0f));

        // --- Salto ---
        if (Input.GetButton("Jump") && isGrounded)
        {
            // limpiamos la velocidad vertical para un salto consistente
            Vector3 vel = rb.linearVelocity;
            vel.y = 0f;
            rb.linearVelocity = vel;

            // impulso instant�neo hacia arriba
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

            isGrounded = false;
            if (animator) animator.SetTrigger(jumpTrigger);
        }

        // --- Par�metros de animaci�n ---
        if (animator)
        {
            // Velocidad "deseada" en el plano, para controlar Move/BlendTree
            float planarSpeed = move.magnitude * currentSpeed; // 0 .. runSpeed
            animator.SetFloat(speedParam, planarSpeed, animDamp, Time.fixedDeltaTime);
            animator.SetBool(groundedParam, isGrounded);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
            isGrounded = false;
    }
}