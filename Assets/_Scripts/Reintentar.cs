using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Reintentar : MonoBehaviour {
    
	public void reiniciar()
    { 
	    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
	
}
