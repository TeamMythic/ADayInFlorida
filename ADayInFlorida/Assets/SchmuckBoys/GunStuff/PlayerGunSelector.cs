using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[DisallowMultipleComponent]
public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField] private GunType Gun;
    [SerializeField] Transform GunParent;
    [SerializeField] private List<GunScriptableObject> Guns;
    //[SerializeField] private PlayerIK InverseKinematics; cannot use [inctroduced in Unity 2020 rip].
    [Space]
    [Header("Runtime Filled: ")]
    public GunScriptableObject ActiveGun;
    private void OnDrawGizmos()
    {
        if(ActiveGun != null)
        {
            Gizmos.color = Color.red;
			Gizmos.DrawRay(ActiveGun.ShootSystem.transform.position, ActiveGun.ShootSystem.transform.forward * ActiveGun.ShootConfig.rayHitDistance);
        }
    }
    private void Start()
    {
        //GunScriptableObject gun = Guns.Find(gun => gun.Type == Gun);
        ActiveGun = Guns[0];
        ActiveGun.Spawn(GunParent, this);
    }
}
