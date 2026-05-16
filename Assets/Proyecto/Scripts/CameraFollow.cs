using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0, 5, -8);

    public Vector3 rotation = new Vector3(30, 0, 0);

    void LateUpdate()
    {
        transform.position = target.position + offset;

        transform.rotation = Quaternion.Euler(rotation);
    }
}