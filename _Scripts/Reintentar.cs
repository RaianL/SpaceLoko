using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Reintentar : MonoBehaviour {
    public static float lastRestartTime = 0.0f;
	public void reiniciar()
    {
        Time.timeScale = 1.0f;
        lastRestartTime = Time.time;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}