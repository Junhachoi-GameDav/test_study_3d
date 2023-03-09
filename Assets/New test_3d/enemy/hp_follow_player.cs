using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp_follow_player : MonoBehaviour
{
    public Transform player_cam;

    Vector3 dir;
    private void FixedUpdate()
    {
        dir = transform.position - player_cam.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = rotation;
    }
}
