using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            Debug.Log("Ball touched Area");
            //Recarga la escena en caso de tocar una fallZone
            //Area donde pierdes (vacío) colocada a mano
            Scene escenaActual = SceneManager.GetActiveScene();
            SceneManager.LoadScene(escenaActual.name);
        }
    }
}
