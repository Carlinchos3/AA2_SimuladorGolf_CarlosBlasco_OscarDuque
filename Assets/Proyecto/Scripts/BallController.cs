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

    [Header("Wall Collision")]
    public LayerMask wallLayer;

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
            acceleration += Vector3.down * gravity;
    }

    void ApplyFriction()
    {
        if (!grounded) return;

        // Evitar NaN
        if (velocity.magnitude > 0.01f)
        {
            // Fricción de rodadura
            Vector3 friction = -velocity.normalized * frictionCoefficient * gravity;
            acceleration += friction;
        }
    }

    void Integrate(float dt)
    {
        velocity += acceleration * dt;
        transform.position += velocity * dt;

        ResolveGroundCollision();
        ResolveWallCollision();

        acceleration = Vector3.zero;

        if (velocity.magnitude < 0.05f)
            velocity = Vector3.zero;
    }

    void ResolveGroundCollision()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 5f, groundLayer))
        {
            float desiredY = hit.point.y + radius;

            // Si atraviesa el suelo
            if (transform.position.y < desiredY)
            {
                Vector3 pos = transform.position;
                pos.y = desiredY;
                transform.position = pos;

                // Cancelar caída
                if (velocity.y < 0)
                    velocity.y = 0;
            }
        }
    }

    void ResolveWallCollision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, wallLayer);

        foreach (Collider wall in hits)
        {
            Vector3 closestPoint =  wall.ClosestPoint(transform.position);
            Vector3 normal = (transform.position - closestPoint).normalized;

            float distance = Vector3.Distance(transform.position, closestPoint);
            float penetration = radius - distance;

            if (penetration > 0)
                transform.position += normal * penetration;

            // Leer material de la pared
            WallMaterial material = wall.GetComponent<WallMaterial>();

            float restitution = 0.8f;

            if (material != null)
                restitution = material.GetRebote();

            // Rebote
            velocity = Vector3.Reflect(velocity, normal) * restitution;
        }
    }
    void CheckGround()
    {
        RaycastHit hit;

        grounded = Physics.Raycast(transform.position, Vector3.down, out hit, radius + 0.1f, groundLayer);

        if (grounded)
        {
            TerrainZone terrain = hit.collider.GetComponent<TerrainZone>();

            if (terrain != null)
                frictionCoefficient = terrain.GetFriction();
        }
    }

    void RotateBall()
    {
        if (velocity.magnitude > 0.01f)
        {
            Vector3 axis = Vector3.Cross(Vector3.up, velocity.normalized);

            float rotationSpeed = (velocity.magnitude / radius) * Mathf.Rad2Deg;

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