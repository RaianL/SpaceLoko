using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets._Abstract;
using System.Collections;
using System.Runtime.InteropServices;

public class ControlNave : PhysicObject
{
    KeyCode foward = KeyCode.LeftControl, left = KeyCode.LeftArrow, right = KeyCode.RightArrow;
    public Vacio generator;
    public GameObject btnTienda;
    public float money = 0;
    public Text moneyText;
    public float thrustForce;
    public float torqueForce;
    public float shipMass;
    public uint fuelCapacity;
    public uint fuel;
    public uint electricity;
    public uint maxElectricity;
    public float fuelMass;
    public bool infiniteFuel;
	public float hp = 100;
    public float maxHp = 100;
    public float minSpeedDmg = 15.0f;
    public float dmgMultiplier = 0.5f;
    public GameObject cosas;
    public Camera camera;
    public Camera minimap;
    private Vector2 PlanetVel = Vector2.zero;
    // Use this for initialization
    public override void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        UpdateMass();
		hp = maxHp;
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
        float scrollDif = Input.GetAxis("Mouse ScrollWheel");
        camera.orthographicSize -= scrollDif * 10.0f;
    }

    private void updateCamera()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
        minimap.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
    }
	private void updateHP()
    {
        Mathf.Clamp(hp, -1, maxHp);
    }
    private void updateGUI()
    {
        moneyText.text = money + "$";
    }
    public override void Update()
    {
        ManageInput();
        PhysicsUpdate();
        updateCamera();
		updateHP();
        updateGUI();
        //PlanetCorrection();
    }
	public void muere()
    {
        cosas.SetActive(true);
        Time.timeScale = 0.0f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
	void OnCollisionEnter2D(Collision2D col)
    {
       if (body.velocity.magnitude > minSpeedDmg) hp -= body.velocity.magnitude*dmgMultiplier;
       if (hp <= 0) muere();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Tienda") setBotonTienda(true);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Tienda") setBotonTienda(false);
    }
    public void setBotonTienda(bool isActive)
    {
        btnTienda.SetActive(isActive);
    }
    public void btnTiendaEvent()
    {
        generator.saveState();
        SceneManager.LoadScene(3);
    }
    /*public void PlanetCorrection()
    {
        Planet p = (Planet)closest;
        if (!(Vector2.Distance(body.position, p.position) - (10.0f * p.transform.localScale.x) < 2.0f))
        {
            PlanetVel = Vector2.zero;
            return;
        }
        body.velocity -= PlanetVel;
        float time = (Time.time + p.offset) / p.speed;
        Vector2 newVel = new Vector2(p.amplitudX * -Mathf.Sin(time), p.amplitudY * Mathf.Cos(time));
        PlanetVel = newVel;
        body.velocity += PlanetVel;
        Debug.Log(PlanetVel);
    }*/
}
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct naveState
{
    public float money;
    public float thrustForce;
    public float torqueForce;
    public float shipMass;
    public uint fuelCapacity;
    public uint fuel;
    public uint electricity;
    public uint maxElectricity;
    public float fuelMass;
    public bool infiniteFuel;
    public float hp;
    public float maxHp;
    public float minSpeedDmg;
    public float dmgMultiplier;
    public Vector2 position;
    public Vector2 velocity;
    public float rotation;
}