using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public BallController ball;

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Header("Ground")]
    public bool grounded;

    [Header("Terrain")]
    public float frictionCoefficient = 0.4f;

    private const float gravity = 9.81f;

    private RaycastHit groundHit;

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        CheckGround();

        ApplyGravity();
        ApplyFriction();

        Integrate(dt);

        ball.RotateBall();
    }

    void ApplyGravity()
    {
        if (!grounded)
        {
            ball.acceleration += Vector3.down * gravity;
        }
    }

    void ApplyFriction()
    {
        if (!grounded)
            return;

        if (ball.velocity.magnitude > 0.01f)
        {
            Vector3 frictionForce = -ball.velocity.normalized * frictionCoefficient * ball.mass * gravity;

            ball.acceleration += frictionForce / ball.mass;
        }
    }

    void Integrate(float dt)
    {
        // Integración física
        ball.velocity += ball.acceleration * dt;

        Vector3 movement = ball.velocity * dt;

        // Colisión paredes ANTES de mover
        ResolveWallCollision(ref movement);

        // Mover bola
        ball.Move(movement);

        // Resolver suelo
        ResolveGroundCollision();

        // Reset aceleración
        ball.acceleration = Vector3.zero;

        // Dormir bola si va lenta
        if (ball.velocity.magnitude < 0.05f)
            ball.StopBall();
    }

    void ResolveWallCollision(ref Vector3 movement)
    {
        if (movement.magnitude <= 0.001f)
            return;

        RaycastHit hit;

        bool collided = Physics.SphereCast(ball.transform.position, ball.radius, movement.normalized, out hit, movement.magnitude, wallLayer);

        if (collided)
        {
            // Material pared
            WallMaterial material = hit.collider.GetComponent<WallMaterial>();

            float restitution = 0.8f;

            if (material != null)
                restitution = material.GetRebote();

            // Colocar fuera de la pared
            ball.transform.position = hit.point + hit.normal * ball.radius;

            // Rebote
            ball.velocity = Vector3.Reflect(ball.velocity, hit.normal) * restitution;

            // Recalcular movimiento tras rebote
            movement = ball.velocity * Time.fixedDeltaTime;
        }
    }

    void ResolveGroundCollision()
    {
        if (!grounded)
            return;

        float desiredY = groundHit.point.y + ball.radius;

        if (ball.transform.position.y < desiredY)
        {
            Vector3 pos = ball.transform.position;

            pos.y = desiredY;

            ball.transform.position = pos;

            if (ball.velocity.y < 0)
                ball.velocity.y = 0;
        }
    }

    void CheckGround()
    {
        grounded = Physics.Raycast(ball.transform.position,Vector3.down, out groundHit, ball.radius + 0.1f, groundLayer);

        if (grounded)
        {
            TerrainZone terrain = groundHit.collider.GetComponent<TerrainZone>();

            if (terrain != null)
                frictionCoefficient = terrain.GetFriction();
        }
    }
}