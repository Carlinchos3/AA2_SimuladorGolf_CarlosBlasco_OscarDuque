using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //Singleton
    public static LevelLoader Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void GoToLevel1() 
    { 
        SceneManager.LoadScene("Level1");
    }
    public void GoToLevel2() 
    {
        SceneManager.LoadScene("Level2");
    }
    public void GoToLevel3() 
    {
        SceneManager.LoadScene("Level3");
    }

}
