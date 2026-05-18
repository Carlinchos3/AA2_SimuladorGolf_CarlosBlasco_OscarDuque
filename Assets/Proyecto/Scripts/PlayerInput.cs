using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public BallController ball;
    public LineRenderer line;

    public float forceMultiplier = 0.02f;
    public float maxForce = 15f;

    private Vector3 startMouse;
    private bool dragging;

    public CameraFollow cam;

    void Update()
    {
        // EMPEZAR DRAG
        if (Input.GetMouseButtonDown(0))
        {
            // No permitir disparar si la bola se mueve
            if (ball.velocity.magnitude > 0.1f)
                return;

            startMouse = Input.mousePosition;
            dragging = true;
        }

        // MIENTRAS ARRASTRAS
        if (dragging)
        {
            Vector3 currentMouse = Input.mousePosition;
            Vector3 drag = currentMouse - startMouse;
            Vector3 force = new Vector3(drag.x, 0, drag.y);

            force *= forceMultiplier;
            force = Vector3.ClampMagnitude(force, maxForce);
            force = -force;

            DrawLine(force);

            cam.SetCameraDirection(force);
        }

        // SOLTAR
        if (Input.GetMouseButtonUp(0) && dragging)
        {
            // Seguridad extra
            if (ball.velocity.magnitude > 0.1f)
            {
                dragging = false;
                line.enabled = false;
                return;
            }

            dragging = false;

            Vector3 endMouse = Input.mousePosition;
            Vector3 drag = endMouse - startMouse;
            Vector3 force = new Vector3(drag.x, 0, drag.y );

            force *= forceMultiplier;
            force = Vector3.ClampMagnitude(force, maxForce);
            force = -force;

            ball.HitBall(force);

            line.enabled = false;
        }
    }

    void DrawLine(Vector3 force)
    {
        line.enabled = true;

        Vector3 startPos = ball.transform.position;

        line.SetPosition(0, startPos);
        line.SetPosition(1, startPos + force);
    }
}