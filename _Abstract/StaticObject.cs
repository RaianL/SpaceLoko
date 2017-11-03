using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;

namespace Assets._Abstract
{
    public abstract class StaticObject : MonoBehaviour
    {
        public Vector2 position;
        public float Mass;
        public float btime = 0;
        public abstract void UpdatePosition();
        public abstract void Update();
        public abstract void Start();
    }
}