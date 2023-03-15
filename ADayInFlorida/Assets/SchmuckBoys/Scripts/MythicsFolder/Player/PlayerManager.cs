using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class PlayerManager : MonoBehaviour
{
	[System.Serializable]
	public class PlayerMovementData
	{//Fedding this to PlayerMovement:
		public float moveSpeed;
		public float groundDrag;
		public float jumpForce;
		public float jumpCoolDown;
		public float airMultiplier;
		public KeyCode jumpKeyBind = KeyCode.Space;
		public float playerHeight;
		public LayerMask whatIsGorund;
	}
	public Transform playerPrefab;
	public PlayerMovementData movementData;
	public PlayerMovement currentPlayer = null;
	public void InstantiatePlayer(Transform location)
	{
		currentPlayer = Instantiate(playerPrefab, location.position, location.rotation).GetComponentInChildren<PlayerMovement>();
		currentPlayer.InitializePlayer(this);
	}
}
#if UNITY_EDITOR
[CustomEditor(typeof(PlayerManager))]
class PlayerManagerEditor: Editor
{
	SerializedProperty pMovementData;
	SerializedProperty playerPrefab;
	
	SerializedProperty currentPlayer;
	private void OnEnable()
	{
		currentPlayer = serializedObject.FindProperty("currentPlayer");
		pMovementData = serializedObject.FindProperty("movementData");
		playerPrefab = serializedObject.FindProperty("playerPrefab");
	}
	public override void OnInspectorGUI()
	{
		var component = (PlayerManager)target;
		if (component == null) return;
		Undo.RecordObject(component, "Change Component");
		setColor(true, Color.cyan);
			if (GUILayout.Button("PlayerManager Script")) { }
			setColor(false, Color.white);
			setColor(true, Color.black);
			serializedObject.Update();
			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.PropertyField(playerPrefab);
			EditorGUILayout.PropertyField(currentPlayer);
			EditorGUILayout.PropertyField(pMovementData);
			serializedObject.ApplyModifiedProperties();
			setColor(true, Color.cyan);
		if (GUILayout.Button("End Of Script")) { }
	}
	private void setColor(bool isBackground, Color color)
	{
		if (isBackground) { GUI.backgroundColor = color; return; }
		GUI.color = color;
	}
}
#endif