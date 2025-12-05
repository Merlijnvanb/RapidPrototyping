using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Camera")]
    public Camera PlayerCamera;
    public float MouseSensitivity;
    public float MaxLookAngle;

    [Header("Movement")]
    public float BaseWalkSpeed;
    public float MaxVelocityChange;
    public float AirManeuverMultiplier;

    [Header("Jumping")]
    public float JumpForce;
    public float JumpCooldown;
    
    [Header("Miscellaneous")]
    public float GroundCheckDistance;
    
    private Rigidbody rb;
    private float pitch = 0f;
    private float yaw = 0f;
    private bool isGrounded = false;
    private float jumpTimer = 0f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        CheckJump();
        CheckGrounded();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    private void HandleMouseInput()
    {
        yaw += MouseSensitivity * Input.GetAxis("Mouse X");
        pitch -= MouseSensitivity * Input.GetAxis("Mouse Y");
        
        PlayerCamera.transform.rotation = Quaternion.Euler(Mathf.Clamp(pitch, -MaxLookAngle, MaxLookAngle), yaw, 0.0f);
        rb.MoveRotation(Quaternion.Euler(0, yaw, 0));
    }

    private void ApplyMovement()
    {
        Vector3 velocity = rb.linearVelocity;
        Vector3 velocityChange = (GetMoveDir() * BaseWalkSpeed) - velocity;
        velocityChange = Vector3.ClampMagnitude(velocityChange, MaxVelocityChange);
        velocityChange.y = 0;

        if (!isGrounded)
            velocityChange *= AirManeuverMultiplier; // can change this to square root/power or something gotta look it up

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private Vector3 GetMoveDir()
    {
        Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        inputDir = Vector3.ClampMagnitude(inputDir, 1f);

        return transform.TransformDirection(inputDir);
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpTimer <= 0)
        {
            Jump();
        }
        
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
        else
        {
            jumpTimer = 0;
        }
    }

    private void CheckGrounded()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(origin, direction, GroundCheckDistance))
        {
            Debug.DrawRay(origin, direction * GroundCheckDistance, Color.red);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        rb.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        jumpTimer = JumpCooldown;
    }
}
