using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float positionX;
    [SerializeField] private float positionY;
    [SerializeField] private float positionZ;

    [SerializeField] private float rotationX;
    [SerializeField] private float rotationY;
    [SerializeField] private float rotationZ;

    [SerializeField] private float smoothValue;

    private Vector3 camPos;
    private Quaternion camRot;
    void LateUpdate()
    {
        camPos = new Vector3(
            target.position.x - positionX,
            target.position.y - positionY,
            target.position.z - positionZ);

        Vector3 smoothedPos = Vector3.Lerp(transform.position, camPos, smoothValue);
        transform.position = smoothedPos;

        camRot = Quaternion.Euler(
            target.rotation.eulerAngles.x - rotationX,
            target.rotation.eulerAngles.y - rotationY,
            target.rotation.eulerAngles.z - rotationZ);

        transform.rotation = camRot;
    }
}