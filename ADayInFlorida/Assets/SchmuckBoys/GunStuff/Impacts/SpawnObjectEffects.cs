using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnObjectEffect", menuName = "Mythics ImapctSystem/Spawn Object Effect")]
public class SpawnObjectEffects : ScriptableObject
{
	public GameObject Prefab;
	public float Probability = 1;
	public bool RandomizeRotation;
	[Tooltip("Zero Values will lock the rotation on that axis ")]
	public Vector3 RandomizedRotationMultiplier = Vector3.zero;
}
