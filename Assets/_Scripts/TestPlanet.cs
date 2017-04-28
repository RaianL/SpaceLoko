using UnityEngine;
using Assets._Abstract;
using System.Collections;

public class TestPlanet : StaticObject {

    Rigidbody2D body;
    Vector2 centre;
    public float amplitudX = 14.0f;
    public float amplitudY = 14.0f;
    public float speed = 7.0f;
    public bool stayStill = false;
	// Use this for initialization
	public override void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        StaticController.AddBody(this);
        centre = body.position;
        UpdatePosition();
	}
	
	// Update is called once per frame
	public override void Update () {
        UpdatePosition();
	}
    public override void UpdatePosition()
    {
        if (!stayStill)
        {
            float time = Time.time / speed;
            Vector2 newPos = new Vector2(amplitudX * Mathf.Cos(time), amplitudY * Mathf.Sin(time));
            body.position = newPos;
            position = newPos;
            position = body.position;
        }
        else
        {
            position = body.position;
        }
    }
}