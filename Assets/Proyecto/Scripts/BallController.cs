using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Physics")]
    public float mass = 1f;
    public float radius = 0.15f;

    [Header("Movement")]
    public Vector3 velocity;
    public Vector3 acceleration;

    [Header("Ground")]
    public bool grounded;
    public LayerMask groundLayer;

    [Header("Terrain")]
    public float frictionCoefficient = 0.4f;

    private const float gravity = 9.81f;

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        CheckGround();
        ApplyGravity();
        ApplyFriction();
        Integrate(dt);
        RotateBall();
    }

    void ApplyGravity()
    {
        if (!grounded)
        {
            acceleration += Vector3.down * gravity;
        }
    }

    void ApplyFriction()
    {
        if (!grounded) return;

        // Fricción de rodadura
        Vector3 friction = -velocity.normalized * frictionCoefficient;

        // Evitar NaN
        if (velocity.magnitude > 0.01f)
        {
            acceleration += friction;
        }
    }

    void Integrate(float dt)
    {
        velocity += acceleration * dt;

        transform.position += velocity * dt;

        ResolveGroundCollision();

        acceleration = Vector3.zero;

        if (velocity.magnitude < 0.05f)
        {
            velocity = Vector3.zero;
        }
    }

    void ResolveGroundCollision()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            transform.position + Vector3.up,
            Vector3.down,
            out hit,
            5f,
            groundLayer))
        {
            float desiredY =
                hit.point.y + radius;

            // Si atraviesa el suelo
            if (transform.position.y < desiredY)
            {
                Vector3 pos = transform.position;

                pos.y = desiredY;

                transform.position = pos;

                // Cancelar caída
                if (velocity.y < 0)
                {
                    velocity.y = 0;
                }
            }
        }
    }

    void CheckGround()
    {
        grounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            radius + 0.1f,
            groundLayer
        );
    }



    void RotateBall()
    {
        if (velocity.magnitude > 0.01f)
        {
            Vector3 axis = Vector3.Cross(Vector3.up, velocity.normalized);

            float rotationSpeed =
                (velocity.magnitude / radius) * Mathf.Rad2Deg;

            transform.Rotate(
                axis,
                rotationSpeed * Time.deltaTime,
                Space.World
            );
        }
    }

    public void HitBall(Vector3 force)
    {
        // F = ma
        velocity += force / mass;
    }
}