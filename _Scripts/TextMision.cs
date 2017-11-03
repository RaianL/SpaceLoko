using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextMision : MonoBehaviour {

    public GameObject TextBox;
    public Text ElTexto;
    public TextAsset Textfile;
    public GameObject btnComenzar;
    public GameObject btnSiguiente;
    public string[] Textlines;

    public int CurrentLine = 0;
    public int EndLine;
    private int i = 0;
    bool siguientePresionado = false;
    // Use this for initialization
    void Start()
    {
        Textlines = (Textfile.text.Split('\n'));
        EndLine = Textlines.Length - 1;
        ElTexto.text = Textlines[CurrentLine];
    }
    void Update()
    {
        Cont();
    }
    public void SiguienteBTN()
    {
        siguientePresionado = true;
    }
    public void Cont()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) siguientePresionado = true;
        if (siguientePresionado)
        {
            siguientePresionado = false;
            if (CurrentLine >= EndLine - 1)
            {
                ElTexto.text = Textlines[EndLine];
                btnSiguiente.SetActive(false);
                btnComenzar.SetActive(true);
            }
            else
            {
                CurrentLine++;
                ElTexto.text = Textlines[CurrentLine];
            }
        }
    }
}
