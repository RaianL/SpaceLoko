using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;

namespace Assets._Abstract
{
    public abstract class PhysicObject : MonoBehaviour
    {
        protected Rigidbody2D body;
        protected StaticObject closest;
        protected float btime = 0;
        public abstract void Start();
        public abstract void Update();
        protected void PhysicsUpdate()
        {
            btime += Time.deltaTime;
            StaticObject closest = StaticController.ClosestBody(this);
            if (closest == null) return;
            CalculateGravity(closest); // Actualiza la gravedad teniendo en cuenta el objeto estatico mas cercano
            this.closest = closest;
        }
        protected void CalculateGravity(StaticObject body)
        {
            float G = Mathf.Pow(6.674f * 10.0f, -11.0f); // Constante Gravitacional de niuton
            float force = G * this.body.mass * body.Mass / Mathf.Pow(Mathf.Abs((this.body.position - body.position).magnitude), 2.0f);
            // (Arriba) G * m1 * m2 / R*R
            Vector2 direction = (body.position - this.body.position).normalized; // Calcula la direccion hacia el otro objeto
            this.body.AddForce(force * direction); // Aplica la fuerza como yo aplico la mafia
        }
        public float getMass() { return body.mass; }
        public Vector2 getPosition() { return body.position; }
    }
}