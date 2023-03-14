using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement: ")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump;
    [Header("Keybinds: ")]
    public KeyCode jumpKeyBind = KeyCode.Space;

    [Header("Ground Check: ")]
    public float playerHeight;
    public LayerMask whatIsGorund;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody playersRigidbody;

    private void Start()
    {
        playersRigidbody = GetComponent<Rigidbody>();
        playersRigidbody.freezeRotation = true;
        readyToJump = true;
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGorund);
        MyInput();
        SpeedControl();
        if (grounded) playersRigidbody.drag = groundDrag;
        else playersRigidbody.drag = 0;

    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(Input.GetKeyDown(jumpKeyBind) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded) playersRigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded) playersRigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(playersRigidbody.velocity.x, 0f, playersRigidbody.velocity.z);
        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            playersRigidbody.velocity = new Vector3(limitedVelocity.x, playersRigidbody.velocity.y, limitedVelocity.z);
        }
    }
    private void Jump()
    {
        playersRigidbody.velocity = new Vector3(playersRigidbody.velocity.x, 0f, playersRigidbody.velocity.z);
        playersRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
