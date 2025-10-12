using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpForce = 5f;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] string speedParam = "Speed";
    [SerializeField] string groundedParam = "Grounded";
    [SerializeField] string jumpTrigger = "Jump";
    [SerializeField] float animDamp = 0.1f;

    Rigidbody rb;
    bool isGrounded = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        // --- Input ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool run = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = run ? runSpeed : walkSpeed;

        // Invertimos direcciones para que coincidan con WASD
        Vector3 move = (transform.forward * -v) + (transform.right * -h);
        if (move.sqrMagnitude > 1f) move.Normalize();

        // --- Movimiento ---
        if (move.sqrMagnitude > 0f)
        {
            rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);
        }

        // --- Salto ---
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
            if (animator) animator.SetTrigger(jumpTrigger);
        }

        // --- Par�metros de animaci�n ---
        if (animator)
        {
            float planarSpeed = move.magnitude * currentSpeed;
            animator.SetFloat(speedParam, planarSpeed, animDamp, Time.fixedDeltaTime);
            animator.SetBool(groundedParam, isGrounded);
        }
    }
}
