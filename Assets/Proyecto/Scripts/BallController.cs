using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector3 velocity;

    public float friction = 0.98f;

    void Update()
    {
        // Movimiento manual
        transform.position += velocity * Time.deltaTime;

        // Fricción simple
        velocity *= friction;

        // Detener completamente
        if (velocity.magnitude < 0.01f)
        {
            velocity = Vector3.zero;
        }
    }

    public void HitBall(Vector3 force)
    {
        velocity = force;
    }
}