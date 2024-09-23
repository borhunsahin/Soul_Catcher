using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float positionX;
    [SerializeField] private float positionY;
    [SerializeField] private float positionZ;

    [SerializeField] private float smoothValue;

    private Vector3 camPos;
    void LateUpdate()
    {
        camPos = new Vector3(
            target.position.x - positionX,
            target.position.y - positionY,
            target.position.z - positionZ);

        Vector3 smoothedPos = Vector3.Lerp(transform.position, camPos, smoothValue);
        transform.position = smoothedPos;
    }
}