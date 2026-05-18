using UnityEngine;

public class Hole : MonoBehaviour
{
    public BallController ball;

    public float holeRadius = 0.3f;
    public float maxGoalSpeed = 0.5f;

    private bool levelCompleted;

    void Update()
    {
        if (levelCompleted)
            return;

        float distance = Vector3.Distance(ball.transform.position,transform.position);
        bool insideHole = distance <= holeRadius;
        bool slowEnough = ball.velocity.magnitude <= maxGoalSpeed;

        if (insideHole && slowEnough)
            Win();
    }

    void Win()
    {
        levelCompleted = true;

        ball.StopBall();

        Debug.Log("LEVEL COMPLETE");
    }
}