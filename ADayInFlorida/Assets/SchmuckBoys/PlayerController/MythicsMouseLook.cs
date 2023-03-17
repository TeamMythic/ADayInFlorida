 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MythicsMouseLook : MonoBehaviour
{
	public float mouseSensitivity = 100f;
	[SerializeField] private Transform playersBody;
	private float xRotation = 0f;
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	private void Update()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
		xRotation -= mouseY;//every frame decrease x rotation by mouse y:
		xRotation = Mathf.Clamp(xRotation, -90, 90);
		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		playersBody.Rotate(Vector3.up * mouseX);
	}
}
