using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -8);

    public float rotationSpeed = 8f;

    private float currentYRotation = 0f;
    private float targetYRotation = 0f;

    void LateUpdate()
    {
        // Rotación suave
        currentYRotation = Mathf.Lerp(
            currentYRotation,
            targetYRotation,
            Time.deltaTime * rotationSpeed
        );

        Quaternion rot = Quaternion.Euler(30, currentYRotation, 0);

        // Rotamos el offset alrededor del jugador
        Vector3 rotatedOffset = rot * offset;

        transform.position = target.position + rotatedOffset;
        transform.LookAt(target.position);
    }

    public void SetCameraDirection(Vector3 direction)
    {
        if (direction.magnitude < 0.1f)
            return;

        targetYRotation = Mathf.Atan2(
            direction.x,
            direction.z
        ) * Mathf.Rad2Deg;
    }
}