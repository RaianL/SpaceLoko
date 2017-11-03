using UnityEngine;
using Assets._Abstract;
using System.Collections;
using System.Collections.Generic;
//guardado
public class StaticController
{

    static List<StaticObject> bodies = new List<StaticObject>();
	// Use this for initialization
	public static void Start()
    {
        bodies = new List<StaticObject>();
    }
	// Update is called once per frame
    public static void AddBody(StaticObject body)
    {
        bodies.Add(body);
    }
    public static void RemoveBody(StaticObject body)
    {
        bodies.Remove(body);
    }
    public static StaticObject ClosestBody(PhysicObject pObject)
    {
        float G = Mathf.Pow(6.674f * 10.0f, -11.0f); // Constante Gravitacional de niuton
        float force;
        float maxForce = 0.0f;
        StaticObject closest = null;
        foreach(StaticObject body in bodies)
        {
            force = G * body.Mass * pObject.getMass() / Mathf.Pow(Mathf.Abs((body.position - pObject.getPosition()).magnitude), 2.0f); // calcula la fuerza que el objeto actual ejerceria sobre 
            if (force > maxForce) // si la fuerza calculada es mayor al mayor, pasa a ser el nuevo mayor
            {
                maxForce = force;
                closest = body;
            }
        }
        return closest;
    }
}