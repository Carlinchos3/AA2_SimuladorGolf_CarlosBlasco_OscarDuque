using UnityEngine;

public class AirZone : MonoBehaviour
{
    public Vector3 localDirection = Vector3.forward;

    public float strength = 15f;

    public Vector3 GetDirection()
    {
        return transform.TransformDirection(localDirection.normalized);
    }
}