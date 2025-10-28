using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float rotationSpeed = 120f; // velocidad de giro en grados por segundo

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] string speedParam = "Speed";
    [SerializeField] string groundedParam = "Ground";
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
        float h = Input.GetAxis("Horizontal"); // izquierda/derecha → rotación
        float v = Input.GetAxis("Vertical");   // adelante/atrás → movimiento
        bool run = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = run ? runSpeed : walkSpeed;

        // --- Rotación (lenta tipo tanque) ---
        if (Mathf.Abs(h) > 0.01f)
        {
            float turn = h * rotationSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }

        // --- Movimiento hacia adelante/atrás ---
        Vector3 move = transform.forward * -v;
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

        // --- Animación ---
        if (animator)
        {
            float planarSpeed = Mathf.Abs(v) * currentSpeed;
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
