using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SurfaceEffect", menuName = "Mythics ImapctSystem/Surface Effect")]
public class SurfaceEffect : ScriptableObject
{
	public List<SpawnObjectEffects> SpawnObjectEffects = new List<SpawnObjectEffects>();
	public List<PlayAudioEffects> PlayAudioEffects = new List<PlayAudioEffects>();
}
