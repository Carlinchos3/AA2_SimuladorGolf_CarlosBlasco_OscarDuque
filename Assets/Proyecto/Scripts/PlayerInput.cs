using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public BallController ball;
    public LineRenderer line;

    private Vector3 startMouse;
    private Vector3 endMouse;

    private bool dragging = false;

    void Update()
    {
        // EMPEZAR DRAG
        if (Input.GetMouseButtonDown(0))
        {
            startMouse = Input.mousePosition;
            dragging = true;
        }

        // MIENTRAS ARRASTRAS
        if (dragging)
        {
            Vector3 currentMouse = Input.mousePosition;

            Vector3 drag = currentMouse - startMouse;

            Vector3 force = new Vector3(
                drag.x,
                0,
                drag.y
            ) * 0.02f;

            force = -force;

            DrawLine(force);
        }

        // SOLTAR
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;

            endMouse = Input.mousePosition;

            Vector3 drag = endMouse - startMouse;

            Vector3 force = new Vector3(
                drag.x,
                0,
                drag.y
            ) * 0.1f;

            force = -force;

            ball.HitBall(force);

            line.enabled = false;
        }
    }

    void DrawLine(Vector3 force)
    {
        line.enabled = true;

        Vector3 startPos = ball.transform.position;
        Vector3 endPos = startPos + force;

        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }
}