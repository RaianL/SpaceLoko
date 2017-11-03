using UnityEngine;
using System.Collections;

public class TextImporter : MonoBehaviour {

    public TextAsset Textfile;
    public string[] Textlines;
	// Use this for initialization
	void Start () {
	if(Textfile != null)
        {
            Textlines = (Textfile.text.Split('\n'));
        }
	}
}
