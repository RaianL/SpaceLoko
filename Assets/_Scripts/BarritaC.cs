using UnityEngine;
using System.Collections;

public class BarritaC : MonoBehaviour {
    ControlNave Combustible;
    // Use this for initialization
    void Start () {
        Combustible = GameObject.Find("Nave").GetComponent<ControlNave>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Combustible.fuel < 0)
        {
            transform.localScale = new Vector3(0f, 0.5f, 1f);
        }
        else
        {
            transform.localScale = new Vector3( 
                ( ( ((float) Combustible.fuel) / ((float) Combustible.fuelCapacity) ) 
                            * 0.9871244f )
                , 0.5f
                , 1f
                );
        }
    }
}
