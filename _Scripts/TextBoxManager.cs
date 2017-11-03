using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextBoxManager : MonoBehaviour {

    public GameObject TextBox;
    public Text ElTexto;
    public TextAsset Textfile;
    public GameObject btnComenzar;
    public GameObject btnSiguiente;
    public GameObject imgVida;
    public string[] Textlines;
    
    public int CurrentLine = 0;
    public int EndLine;
    private int i = 0;
    // Use this for initialization
    void Start()
    {
        btnComenzar = GameObject.Find("Comenzar");
        btnSiguiente = GameObject.Find("Siguiente");
        imgVida = GameObject.Find("Vida");
        imgVida.SetActive(false);
        btnComenzar.SetActive(false);
        Textlines = (Textfile.text.Split('\n'));
        EndLine = Textlines.Length - 1;
        ElTexto.text = Textlines[CurrentLine];
    }
    void Update()
    {
		Cont();
    }
	public void Cont ()
	{
        if(CurrentLine == 7)
        {
            imgVida.SetActive(true);
        }
        if (CurrentLine < 7 || CurrentLine > 7)
        {
            imgVida.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
	{
		if (CurrentLine >= EndLine)
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
