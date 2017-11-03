using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public GameObject btnIniciarMision;
    public GameObject MensajeFallo;
    public GameObject Pausa;
    public GameObject MensajeExito;
    public GameObject tutorial;
    public Reintentar reint;
    public TextMision textboxmanager;
    public Text timer;
    public GameObject timerOBJ;
    public juegoData jData;
    public ControlNave nave;
    public float startTime;
    bool Falloxd = false;
    bool FalloExito = false;
    float falloStart;
    public bool activado = false; //si esta en mision xD
    bool primeraVez = true;
    bool tutorialpause = false;
    bool gamepause = false;
    float tiempoDisponible;
    void Start()
    {

    }
    public float getTiempoDisponible()
    {
        return tiempoDisponible;
    }
	void Update()
    {
        if (activado) timer.text = "Tiempo Restante: " + (tiempoDisponible - (Time.time - startTime));
        if(activado && jData.hayLimiteDeTiempo && (Time.time - startTime > tiempoDisponible))
        {
            Debug.Log("MISSION FAILED, WE'LL GET EM NEXT TIME");
            activado = false;
            mostrarMensajeDeFallo();
        }
        if(Falloxd && Time.time >= falloStart + 5.0f)
        {
            Falloxd = false;
            if (FalloExito)
            {
                MensajeExito.SetActive(false);
            }
            else
            {
                MensajeFallo.SetActive(false);
            }
        }
        pause();
	}
    public void pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamepause)
            {
                Time.timeScale = 0.0f;
                gamepause = true;
                Pausa.SetActive(true);
            }
            else if (!tutorialpause)
            {
                Time.timeScale = 1.0f;
                gamepause = false;
                Pausa.SetActive(false);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.tag)
        {
            case "Da":
                showMissionGUI();
                if(primeraVez && Time.time > Reintentar.lastRestartTime + 1.0f)
                {
                    Time.timeScale = 0.0f;
                    tutorialpause = true;
                    mostrarTutorial();
                }
                break;
            case "Recibe":
                if (activado)
                {
                    completarMision();
                }
                break;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Da")
        {
            hideMissionGUI();
        }
    }
    void completarMision()
    {
        timerOBJ.SetActive(false);
        switch (jData.tipo)
        {
            case juego.nada:
                Debug.Log("gil, como que termino una mision que no hay? ahora te crasheo todo papu");
                throw new System.Exception();
                break;
            case juego.tiempo:
                float timeDiff = Time.time - startTime;
                float premio = Mathf.Floor(1000 * ((tiempoDisponible - timeDiff) / tiempoDisponible));
                gameObject.GetComponent<ControlNave>().money += premio;
                mostrarMensajeDeExito();
                reset();
                break;
        }
    }
    public void StartMission()
    {
        activado = true;
        jData.flechaDa.SetActive(false);
        jData.flechaRecibe.SetActive(true);
        startTime = Time.time;
        timerOBJ.SetActive(true);
        tiempoDisponible = Vector3.Magnitude(jData.pDaTransform.position - jData.pRecibeTransform.position) / 10.0f;
    }
    void reset()
    {
        jData.flechaRecibe.SetActive(false);
        activado = false;
        jData.flechaDa.SetActive(true);
    }
    public void Continuar()
    {
        if (!tutorialpause) Time.timeScale = 1.0f;
        Pausa.SetActive(false);
    }
    public void Reiniciar()
    {
        Time.timeScale = 1.0f;
        reint.reiniciar();
    }
    public void changeToScene(int SceneToChangeTo)
    {
        SceneManager.LoadScene(SceneToChangeTo);
    }
    public void Comenzar()
    {
        tutorial.SetActive(false);
        cerrarTutorial();
    }
    public void cerrarTutorial()
    {
        primeraVez = false;
        tutorialpause = false;
        Time.timeScale = 1.0f;
    }
    void mostrarTutorial()
    {
        tutorial.SetActive(true);
    }
    void showMissionGUI()
    {
        btnIniciarMision.SetActive(true);
    }
    void hideMissionGUI()
    {
        btnIniciarMision.SetActive(false);
    }
    void mostrarMensajeDeFallo()
    {
        MensajeFallo.SetActive(true);
        Falloxd = true;
        falloStart = Time.time;
        FalloExito = false;
        timerOBJ.SetActive(false);
        jData.flechaRecibe.SetActive(false);
        jData.flechaDa.SetActive(true);
    }
    void mostrarMensajeDeExito()
    {
        MensajeExito.SetActive(true);
        Falloxd = true;
        falloStart = Time.time;
        FalloExito = true;
    }
}
public struct juegoData
{
    public juego tipo;
    public bool hayLimiteDeTiempo;
    public GameObject da;
    public GameObject recibe;
    public GameObject flechaDa;
    public GameObject flechaRecibe;
    public Transform pDaTransform;
    public Transform pRecibeTransform;
}