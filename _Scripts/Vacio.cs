using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets._Classes;
using System.IO;
using System;
using System.Reflection;
using System.Text;

public class Vacio : MonoBehaviour
{
    public static bool load = false;
    GameObject[] planets;
    public GameObject player;
    public juegoData jData;
    public float min;
    public float max;

    // Use this for initialization
    void Start()
    {
        jData.tipo = juego.tiempo;
        GameObject playera = GameObject.FindGameObjectWithTag("Player");
        if (!GameState.generated)
        {
            planets = SSGenerator.generateSolarSystem(transform.position, min, max, Time.time, ref jData);
        }
        else if(Vacio.load)
        {
            planets = SSGenerator.generateSolarSystem(transform.position, min, max, Time.time, ref jData);
            fileToState();
            ControlNave nave = playera.GetComponent<ControlNave>();
            PasarEstadoAObjeto(nave, GameState.player);
            Vacio.load = false;
        }
        else
        {
            planets = SSGenerator.reGenerateSolarSystem(GameState.planets);
            ControlNave nave = playera.GetComponent<ControlNave>();
            PasarEstadoAObjeto(nave, GameState.player);
            playera.transform.position = GameState.player.position;
        }
        GameManager manager = playera.GetComponent<GameManager>();
        manager.jData = jData;
    }
    // Update is called once per frame
    public void SaveAndQuit()
    {
        stateToFile();
        Application.Quit();
    }
    void Update()
    {
    }

    void PasarEstadoAObjeto(ControlNave nave, naveState state)
    {
        nave.money = state.money;
        nave.thrustForce = state.thrustForce;
        nave.torqueForce = state.torqueForce;
        nave.shipMass = state.shipMass;
        nave.fuelCapacity = state.fuelCapacity;
        nave.fuel = state.fuel;
        nave.electricity = state.electricity;
        nave.maxElectricity = state.maxElectricity;
        nave.fuelMass = state.fuelMass;
        nave.maxHp = state.maxHp;
        nave.hp = state.hp;
        nave.minSpeedDmg = state.minSpeedDmg;
        nave.dmgMultiplier = state.dmgMultiplier;
        nave.GetComponent<Rigidbody2D>().position = state.position;
        nave.GetComponent<Rigidbody2D>().velocity = state.velocity;
        nave.GetComponent<Rigidbody2D>().rotation = state.rotation;
    }

    naveState PasarObjetoAEstado(ControlNave nave)
    {
        naveState state = new naveState();
        state.money = nave.money;
        state.thrustForce = nave.thrustForce;
        state.torqueForce = nave.torqueForce;
        state.shipMass = nave.shipMass;
        state.fuelCapacity = nave.fuelCapacity;
        state.fuel = nave.fuel;
        state.electricity = nave.electricity;
        state.maxElectricity = nave.maxElectricity;
        state.fuelMass = nave.fuelMass;
        state.maxHp = nave.maxHp;
        state.hp = nave.hp;
        state.minSpeedDmg = nave.minSpeedDmg;
        state.dmgMultiplier = nave.dmgMultiplier;
        state.rotation = nave.GetComponent<Rigidbody2D>().rotation;
        return state;
    }
    public void saveState()
    {
        Planet[] ps = new Planet[planets.Length];
        for (int i = 0; i < planets.Length; ++i)
        {
            ps[i] = planets[i].GetComponent<Planet>();
        }
        GameState.saveState(ps, Time.time,
            PasarObjetoAEstado(player.GetComponent<ControlNave>()),
            player.transform.position, 
            player.GetComponent<Rigidbody2D>().velocity);
        GameState.generated = true;
    }
    void fileToState(string filepath = "save.sl")
    {
        int naveLines = 16, planetlines = 12, misclines = 1;
        FileStream readstream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        byte[] bread = new byte[readstream.Length];
        readstream.Read(bread, 0, bread.Length);
        string[] sections = Encoding.UTF8.GetString(bread).Split((char) 0);
        string[] navestateLines = sections[0].Split('\n');
        string[] planetLines = sections[1].Split('\n');
        string[] miscLines = sections[2].Split('\n');
        GameState.player = navestateFromString(navestateLines);
        readstream.Close();
    }
    void stateToFile(string filepath = "save.sl")
    {
        FileStream input = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None);
        saveState();
        string strinput = navestateToString(GameState.player);
        input.Write(Encoding.UTF8.GetBytes(strinput), 0, Encoding.UTF8.GetByteCount(strinput));
        input.WriteByte(0);
        foreach (Planet p in GameState.planets)
        {
            strinput = planetToString(p);
            input.Write(Encoding.UTF8.GetBytes(strinput), 0, Encoding.UTF8.GetByteCount(strinput));
        }
        input.WriteByte(0);
        strinput = GameState.time.ToString();
        input.Write(Encoding.UTF8.GetBytes(strinput), 0, Encoding.UTF8.GetByteCount(strinput));
        input.Close();
    }
    string navestateToString(naveState state)
    {
        string ret = "";
        FieldInfo[] fi = typeof(naveState).GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo info in fi)
        {
            //ret += info.Name + ':';
            ret += info.GetValue(state).ToString() + '\n';
        }
        return ret;
    }
    string planetToString(Planet p)
    {
        string ret = "";
        FieldInfo[] fi = typeof(Planet).GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo info in fi)
        {
            if (info.Name != "body")
            {
                //ret += info.Name + ':';
                ret += info.GetValue(p).ToString() + '\n';
            }
        }
        return ret;
    }
    naveState navestateFromString(string[] lines)
    {
        naveState retstate = new naveState();
        retstate.money = Convert.ToInt32(lines[0]);
        retstate.thrustForce = Convert.ToSingle(lines[1]);
        retstate.torqueForce = Convert.ToSingle(lines[2]);
        retstate.shipMass = Convert.ToSingle(lines[3]);
        retstate.fuelCapacity = Convert.ToUInt32(lines[4]);
        retstate.fuel = Convert.ToUInt32(lines[5]);
        retstate.electricity = Convert.ToUInt32(lines[6]);
        retstate.maxElectricity = Convert.ToUInt32(lines[7]);
        retstate.fuelMass = Convert.ToSingle(lines[8]);
        retstate.infiniteFuel = Convert.ToBoolean(lines[9]);
        retstate.hp = Convert.ToSingle(lines[10]);
        retstate.maxHp = Convert.ToSingle(lines[11]);
        retstate.minSpeedDmg = Convert.ToSingle(lines[12]);
        retstate.dmgMultiplier = Convert.ToSingle(lines[13]);
        retstate.position = vectorfromstring(lines[14]);
        retstate.velocity = vectorfromstring(lines[15]);
        retstate.rotation = Convert.ToSingle(lines[16]);
        return retstate;
    }
    Planet[] solarSystemFromString(string[] lines, int planetlines)
    {
        Planet[] ret = new Planet[lines.Length / planetlines];
        for(int i = 0; i < lines.Length; i += planetlines)
        {
            
        }
    }
    string[] takeFromTo(string[] take, int from, int to)
    {

    }
    Planet planetFromString(string[] lines)
    {
        Planet p = new Planet();
        p.centre = vectorfromstring(lines[0]);
        p.amplitudX = Convert.ToSingle(lines[1]);
        p.amplitudY = Convert.ToSingle(lines[2]);
        p.offset = Convert.ToSingle(lines[3]);
        p.speed = Convert.ToSingle(lines[4]);
        p.size = Convert.ToSingle(lines[5]);
        p.spriteIndex = Convert.ToInt32(lines[6]);
        p.isTienda = Convert.ToBoolean(lines[7]);
        p.rol = rolFromString(lines[8]);
        p.position = vectorfromstring(lines[9]);
        p.Mass = Convert.ToSingle(lines[10]);
        p.btime = Convert.ToSingle(lines[11]);
        return p;
    }
    Vector2 vectorfromstring(string strvec)
    {
        string[] posline = strvec.Split(',');
        float x = Convert.ToSingle(posline[0].Substring(1, posline[0].Length - 1));
        float y = Convert.ToSingle(posline[0].Substring(1, posline[0].Length - 1));
        return new Vector2(x, y);
    }
    rolDeMision rolFromString(string strrol)
    {
        switch(strrol)
        {
            case "recibir":
                return rolDeMision.recibir;
            case "dar":
                return rolDeMision.dar;
            default:
                return rolDeMision.nada;
        }
    }
    public void saveGame()
    {
        saveState();
    }
}