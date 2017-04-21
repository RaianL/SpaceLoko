﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;

namespace Assets._Abstract
{
    public abstract class PhysicObject : MonoBehaviour
    {
        protected Rigidbody2D body;
        protected StaticController controller;
        public abstract void Start();
        public abstract void Update();
        protected void getControllerInstance()
        {
            controller = GameObject.FindGameObjectWithTag("GravityController").GetComponent<StaticController>();
            // Consigue el coso que almacena todos los objetos gravitatorios estaticos
        }
        protected void PhysicsUpdate()
        {
            CalculateGravity(controller.ClosestBody(body.position)); // Actualiza la gravedad teniendo en cuenta el objeto estatico mas cercano
        }
        protected void CalculateGravity(StaticObject body)
        {
            float G = Mathf.Pow(6.674f * 10.0f, -11.0f); // Constante Gravitacional de niuton
            float force = G * this.body.mass * body.Mass / Mathf.Pow(Mathf.Abs((this.body.position - body.position).magnitude), 2.0f);
            // (Arriba) G * m1 * m2 / R*R
            Vector2 direction = (body.position - this.body.position).normalized; // Calcula la direccion hacia el otro objeto
            this.body.AddForce(force * direction); // Aplica la fuerza como yo aplico la mafia
        }
    }
}