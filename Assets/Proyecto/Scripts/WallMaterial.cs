using UnityEngine;

public class WallMaterial : MonoBehaviour
{
    public WallType wallType;

    public float GetRebote()
    {
        switch (wallType)
        {
            case WallType.Madera:
                return 0.5f;

            case WallType.Goma:
                return 0.8f;

            case WallType.Foam:
                return 0.2f;

            default:
                return 0.5f;
        }
    }
}