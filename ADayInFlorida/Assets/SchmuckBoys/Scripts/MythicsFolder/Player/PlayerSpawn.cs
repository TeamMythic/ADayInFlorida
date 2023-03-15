using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
	public PlayerManager PlayerManager;
	private void Start()
	{
		PlayerManager.InstantiatePlayer(transform);
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerSpawn))]
class PlayerSpawnEditor : Editor
{
	SerializedProperty PlayerManager;
	private void OnEnable()
	{
		PlayerManager = serializedObject.FindProperty("PlayerManager");
	}
	public override void OnInspectorGUI()
	{
		var component = (PlayerSpawn)target;
		if (component == null) return;
		Undo.RecordObject(component, "Change Component");
		setColor(true, Color.cyan);
		if (GUILayout.Button("PlayerSpawn Script")) { }
		setColor(false, Color.white);
		setColor(true, Color.black);
		serializedObject.Update();
		serializedObject.ApplyModifiedProperties();
		EditorGUILayout.PropertyField(PlayerManager);
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
