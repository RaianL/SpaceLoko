using UnityEngine;
using Assets._Abstract;
using System.Collections;
using System.Collections.Generic;

public class StaticController : MonoBehaviour {

    List<StaticObject> bodies;
	// Use this for initialization
	void Start () {
        bodies = new List<StaticObject>(10);
    }
	
	// Update is called once per frame
	void Update ()
    {
    }
    public void AddBody(StaticObject body)
    {
        bodies.Add(body);
    }
    public void RemoveBody(StaticObject body)
    {
        bodies.Remove(body);
    }
    public StaticObject ClosestBody(Vector2 position)
    {
        return bodies[0];
    }
}