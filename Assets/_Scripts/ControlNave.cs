﻿using UnityEngine;
using Assets._Abstract;
using System.Collections;

public class ControlNave : PhysicObject {
    KeyCode foward = KeyCode.Space, left = KeyCode.LeftArrow, right = KeyCode.RightArrow;
    public float thrustForce;
    public float torqueForce;
    public float shipMass;
    public uint fuelCapacity;
    public uint fuel;
    public uint electricity;
    public uint maxElectricity;
    public float fuelMass;
    public bool infiniteFuel;
	// Use this for initialization
	public override void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        UpdateMass();
	}
    private void UpdateMass()
    {
        body.mass = shipMass + fuel * fuelMass;
    }
    private void Thrust() // en terminos coloquiales, significa "darle duro mamasita al propulsor"
    {
        if (fuel > 0 || infiniteFuel)
        {
            body.AddForce(transform.up * thrustForce);
            if (!infiniteFuel)
            {
                fuel--;
                UpdateMass();
            }
        }
    }
    private void Rotate(float force)
    {
        body.AddTorque(force);
    }
	private void SolarPanel()
    {

    }

	// Update is called once per frame
    private void ManageInput()
    {
        if (Input.GetKey(foward))
        {
            Thrust();
        }
        if (Input.GetKey(left))
        {
            Rotate(torqueForce);
        }
        if (Input.GetKey(right))
        {
            Rotate(-torqueForce);
        }
    }
	public override void Update () {
        ManageInput();
        getControllerInstance();
        PhysicsUpdate();
    }
}