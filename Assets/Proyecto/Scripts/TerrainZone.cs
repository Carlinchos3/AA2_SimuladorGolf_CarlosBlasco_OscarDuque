using UnityEngine;

public class TerrainZone : MonoBehaviour
{
    public GroundType groundType;

    public float GetFriction()
    {
        switch (groundType)
        {
            case GroundType.Grass:
                return 0.4f;

            case GroundType.Ice:
                return 0.1f;

            case GroundType.Sand:
                return 0.6f;

            default:
                return 0.4f;
        }
    }
}