using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MythicsPlayerMovement : MonoBehaviour
{
    private CharacterController playersCharacterController;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float walkSeed = 12;
    [SerializeField] private float runSpeed = 20f;
	[SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityMagnitude = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    // Start is called before the first frame update
    void Awake()
    {
        playersCharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleGrounded();
		HandleInput();
        velocity.y += gravity * Time.deltaTime;
        playersCharacterController.Move((gravityMagnitude * velocity) * Time.deltaTime);//△y = (1/2)gravity * (time)^2
	}
    private void HandleGrounded()
    {
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		if (isGrounded && velocity.y < 0)
		{//Fix for being grounded to smoothMore:
			velocity.y = -2f;
		}
	}
    private void HandleInput()
    {
        HandleMouse();
        HandleSprint();
        HandleJump();
	}
    private void HandleJump()
    {
		if (Input.GetButtonDown("Jump") && isGrounded)
		{//V = sqrt(h * -2 * g):

			velocity.y = Mathf.Sqrt((jumpHeight) * -2 * (gravity * gravityMagnitude));
		}
	}
    private void HandleSprint()
    {
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			speed = runSpeed;
			return;
		}
		speed = walkSeed;
	}
    private void HandleMouse()
    {
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		Vector3 move = transform.right * x + transform.forward * z;
		playersCharacterController.Move(move * speed * Time.deltaTime);
	}
}
