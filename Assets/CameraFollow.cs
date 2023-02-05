using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        Vector3 follow = new Vector3(playerTransform.position.x, playerTransform.position.y, Camera.main.transform.position.z);
        transform.position = follow;
    }
}
