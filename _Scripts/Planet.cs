using UnityEngine;
using Assets._Abstract;
using System.Collections;
using System;
public enum rolDeMision
{
    nada, dar, recibir
}
[Serializable]
public class Planet : StaticObject
{

    public Rigidbody2D body;
    public Vector2 centre = Vector2.zero;
    public float amplitudX = 14.0f; // de la orbita
    public float amplitudY = 14.0f;
    public float offset = 0.0f;
    public float speed = 4.0f; // de la orbita
    public float size;
    public int spriteIndex;
    public bool isTienda = false;
    public rolDeMision rol = rolDeMision.nada;
    // Use this for initialization
    public override void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        StaticController.AddBody(this);
        transform.position = new Vector3(transform.position.x, transform.position.y, 200);
        float time = (btime + offset) / speed;
        body.position = new Vector3(amplitudX * Mathf.Cos(time), amplitudY * Mathf.Sin(time), 200);
        centre = body.position;
        UpdatePosition();
    }

    // Update is called once per frame
    public override void Update()
    {
        btime += Time.deltaTime;
        UpdatePosition();
    }
    public override void UpdatePosition()
    {
        float time = (btime + offset) / speed;
        Vector3 newPos = new Vector3(amplitudX * Mathf.Cos(time), amplitudY * Mathf.Sin(time), 0);
        body.position = newPos;
        position = body.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, 200);
    }
}