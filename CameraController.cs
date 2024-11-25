using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;

    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float zOffset;

    [SerializeField] private float minVerticalAngle = -45f;
    [SerializeField] private float maxVerticalAngle = 45f;

    [SerializeField] private Vector2 framingOffset;

    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        rotationX += Input.GetAxis("Camera Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY += Input.GetAxis("Camera X") * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        var lookAt = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        transform.position = lookAt - targetRotation * new Vector3(0, 0, zOffset);
        transform.rotation = targetRotation;
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
