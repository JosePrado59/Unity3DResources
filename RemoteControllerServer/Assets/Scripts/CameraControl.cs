using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform playerTransform;
    public Vector3 cameraOffset = new Vector3(0, 3, 5);
    public float yPosition = 0;

    public float yaw = 180f;
    public float pitch = 20f;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        transform.position = playerTransform.position + cameraOffset;
        transform.rotation = new Quaternion(
            transform.rotation.x,
            transform.rotation.y - yPosition,
            transform.rotation.z, transform.rotation.w);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}