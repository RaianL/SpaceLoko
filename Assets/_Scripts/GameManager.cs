using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject btnIniciarMision;
    public GameObject MensajeFallo;
    public GameObject MensajeExito;
    public Text timer;
    public GameObject timerOBJ;
    public juegoData jData;
    public float startTime;
    bool Falloxd = false;
    bool FalloExito = false;
    float falloStart;
    public bool activado = false; //si esta en mision xD
    bool primeraVez = true;
	void Start()
    {

	}
	void Update()
    {
        if (activado) timer.text = "Tiempo Restante: " + (jData.tiempoDisponible - (Time.time - startTime));
        if(activado && jData.hayLimiteDeTiempo && (Time.time - startTime > jData.tiempoDisponible))
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
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.tag)
        {
            case "Da":
                showMissionGUI();
                /*if(primeraVez)
                {
                    Time.timeScale = 0.0f;
                    mostrarTutorial();
                }*/
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
                float premio = Mathf.Floor(1000 * ((jData.tiempoDisponible - timeDiff) / jData.tiempoDisponible));
                gameObject.GetComponent<ControlNave>().money += premio;
                mostrarMensajeDeExito();
                activado = false;
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
    }
    public void cerrarTutorial()
    {
        primeraVez = false;
        Time.timeScale = 1.0f;
    }
    void mostrarTutorial()
    {

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
    public float tiempoDisponible;
    public bool hayLimiteDeTiempo;
    public GameObject da;
    public GameObject recibe;
    public GameObject flechaDa;
    public GameObject flechaRecibe;
}