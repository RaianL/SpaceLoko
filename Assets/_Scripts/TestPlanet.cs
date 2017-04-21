using UnityEngine;
using Assets._Abstract;
using System.Collections;

public class TestPlanet : StaticObject {

    Rigidbody2D body;
	// Use this for initialization
	public override void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        UpdatePosition();
        GameObject.FindGameObjectWithTag("GravityController").GetComponent<StaticController>().AddBody(this);
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}
    public override void UpdatePosition()
    {
        position = body.position;
    }
}