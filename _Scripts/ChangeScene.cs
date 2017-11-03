using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void changeToScene (int SceneToChangeTo)
    {
        SceneManager.LoadScene(SceneToChangeTo);
    }
    public void load ()
    {
        Vacio.load = true;
        GameState.generated = true;
        SceneManager.LoadScene(2);
    }
}