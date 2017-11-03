using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public void changeToScene(int SceneToChangeTo)
    {
        SceneManager.LoadScene(SceneToChangeTo);
    }
}
