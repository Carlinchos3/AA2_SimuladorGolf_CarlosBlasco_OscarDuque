using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void WinLevel()
    {
        Debug.Log("YOU WIN");
    }
}