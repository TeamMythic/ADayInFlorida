using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, InteractableInterface
{
	public void ShotByCrazyBill()
	{
		Destroy(this.gameObject);
	}

}
