using UnityEngine;
[CreateAssetMenu(fileName = "Shoot Config", menuName = "Mythics Guns/Shoot Configuration", order = 2)]
public class ShootConfigurationScriptableObject : ScriptableObject
{
    public LayerMask Hitmask;
    public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.1f);
    public float FireRate = 0.25f;
    public float rayHitDistance = 15f;
}
