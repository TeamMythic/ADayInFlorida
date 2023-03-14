using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform FollowObject;
    private void Update()
    {
        transform.position = FollowObject.position;
    }
}
