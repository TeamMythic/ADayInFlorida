using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioEffect", menuName = "Mythics ImapctSystem/Audio Effect")]

public class PlayAudioEffects : ScriptableObject
{
    public AudioSource AudioSourcePrefab;
    public List<AudioClip> AudioClips = new List<AudioClip>();
    [Tooltip("Values are clamped to 0-1")]
    public Vector2 VolumeRange = new Vector2(0.5f, 1);
}
