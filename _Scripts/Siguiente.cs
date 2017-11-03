using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Siguiente : MonoBehaviour {
    TextBoxManager Sig;
    public void Siguien()
    {
        Sig = FindObjectOfType<TextBoxManager>();
        if(Sig.CurrentLine == 7)
        {
            Sig.imgVida.SetActive(true);
        }
        if(Sig.CurrentLine < 7 || Sig.CurrentLine > 7)
        {
            Sig.imgVida.SetActive(false);
        }
        if (Sig.CurrentLine >= Sig.EndLine)
        {
			Sig.ElTexto.text = Sig.Textlines[Sig.EndLine];
			Sig.btnSiguiente.SetActive(false);
			Sig.btnComenzar.SetActive(true);
        }
        else
        {
            Sig.CurrentLine++;
            Sig.ElTexto.text = Sig.Textlines[Sig.CurrentLine];
        }
    }
    
}
