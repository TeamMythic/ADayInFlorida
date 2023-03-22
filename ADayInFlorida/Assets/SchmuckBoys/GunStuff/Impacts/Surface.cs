using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Surface", menuName = "Mythics ImapctSystem/SurfaceGeneric")]
public class Surface : ScriptableObject
{
	[Serializable]
	public class SurfaceImpactTypeEffect
	{
		public ImpactType ImpactType;
		public SurfaceEffect SurfaceEffect;
	}
	public List<SurfaceImpactTypeEffect> ImpactTypeEffects = new List<SurfaceImpactTypeEffect>();

}
