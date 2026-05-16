using UnityEngine;

public class TerrainZone : MonoBehaviour
{
    public TerrainType terrainType;

    public float GetFriction()
    {
        switch (terrainType)
        {
            case TerrainType.Ice:
                return 0.1f;

            case TerrainType.Sand:
                return 0.6f;

            default:
                return 0.4f;
        }
    }
}