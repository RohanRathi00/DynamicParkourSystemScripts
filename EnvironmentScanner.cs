using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScanner : MonoBehaviour
{
    [SerializeField] private Vector3 rayOffset = new Vector3(0, 0.25f, 0);
    [SerializeField] private float rayLength = 0.8f;
    [SerializeField] private float verticalRayLength = 5f;
    [SerializeField] private LayerMask obstracleLayer;

    public ObstracleHitData CheckObstracle()
    {
        var hitData = new ObstracleHitData();

        var rayOrigin = transform.position + rayOffset;
        hitData.obstracleDetected = Physics.Raycast(rayOrigin, transform.forward, out hitData.obstracleHitInfo, rayLength, obstracleLayer);

        Debug.DrawRay(rayOrigin, transform.forward * rayLength, (hitData.obstracleDetected) ? Color.red : Color.green);

        if (hitData.obstracleDetected)
        {
            var verticalRayOrigin = hitData.obstracleHitInfo.point + Vector3.up * verticalRayLength;
            hitData.obstracleHeightDetected = Physics.Raycast(verticalRayOrigin, Vector3.down, out hitData.obstracleHeightHitInfo, verticalRayLength, obstracleLayer);

            Debug.DrawRay(verticalRayOrigin, Vector3.down * verticalRayLength, (hitData.obstracleHeightDetected) ? Color.red : Color.green);
        }

        return hitData;
    }
}

public struct ObstracleHitData
{
    public bool obstracleDetected;
    public bool obstracleHeightDetected;
    public RaycastHit obstracleHitInfo;
    public RaycastHit obstracleHeightHitInfo;
}
