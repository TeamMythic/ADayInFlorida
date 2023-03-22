using UnityEngine;
[CreateAssetMenu(fileName = "Trail Config", menuName = "Mythics Guns/Gun Trail Configuration", order = 3)]
public class TrailConfigurationScriptableObject : ScriptableObject
{
    public Material Material;
    public AnimationCurve WidthCurve;
    public float Duration = 0.5f;
    public float MinVertexDistance = 0.1f;
    public Gradient Color;

    public float MissDistance = 100f;
    public float SimilationSpeed = 100f;

}
