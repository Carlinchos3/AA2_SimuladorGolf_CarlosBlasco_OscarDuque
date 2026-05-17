using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Physics")]
    public float mass = 1f;
    public float radius = 0.15f;

    [Header("Movement")]
    public Vector3 velocity;
    public Vector3 acceleration;

    public void Move(Vector3 movement)
    {
        transform.position += movement;
    }

    public void RotateBall()
    {
        if (velocity.magnitude > 0.01f)
        {
            Vector3 axis = Vector3.Cross(Vector3.up, velocity.normalized);

            float rotationSpeed = (velocity.magnitude / radius) * Mathf.Rad2Deg;

            transform.Rotate(axis, rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    public void HitBall(Vector3 force)
    {
        velocity += force / mass;
    }

    public void StopBall()
    {
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
    }
}