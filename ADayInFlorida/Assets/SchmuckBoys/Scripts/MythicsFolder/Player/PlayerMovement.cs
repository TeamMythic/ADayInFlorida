using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerManager playerManager;
    bool readyToJump;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    public List<Transform> groundChecks;
    Vector3 moveDirection;

    Rigidbody playersRigidbody;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform ray in groundChecks)
        {
            Gizmos.DrawRay(ray.position, transform.TransformDirection(Vector3.down * 2.5f));
        }
    }
    public void InitializePlayer(PlayerManager pManRef)
    {
		playerManager = pManRef;
        playersRigidbody = GetComponent<Rigidbody>();
        playersRigidbody.freezeRotation = true;
        readyToJump = true;
        
    }
    private void Update()
    {
        foreach(Transform ray in groundChecks)
        {
            grounded = Physics.Raycast(ray.position, Vector3.down, playerManager.movementData.playerHeight * 0.5f + 0.2f, playerManager.movementData.whatIsGorund);
            if (grounded) break;
        }
        MyInput();
        SpeedControl();
        if (grounded) playersRigidbody.drag = playerManager.movementData.groundDrag;
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
        if(Input.GetKeyDown(playerManager.movementData.jumpKeyBind) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), playerManager.movementData.jumpCoolDown);
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded) playersRigidbody.AddForce(moveDirection.normalized * playerManager.movementData.moveSpeed * 10f, ForceMode.Force);
        else if (!grounded) playersRigidbody.AddForce(moveDirection.normalized * playerManager.movementData.moveSpeed * 10f * playerManager.movementData.airMultiplier, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(playersRigidbody.velocity.x, 0f, playersRigidbody.velocity.z);
        if(flatVelocity.magnitude > playerManager.movementData.moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * playerManager.movementData.moveSpeed;
            playersRigidbody.velocity = new Vector3(limitedVelocity.x, playersRigidbody.velocity.y, limitedVelocity.z);
        }
    }
    private void Jump()
    {
        playersRigidbody.velocity = new Vector3(playersRigidbody.velocity.x, 0f, playersRigidbody.velocity.z);
        playersRigidbody.AddForce(transform.up * playerManager.movementData.jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
