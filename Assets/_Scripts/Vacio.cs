using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets._Classes;

public class Vacio : MonoBehaviour
{
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
    void Update()
    {
    }

    void PasarEstadoAObjeto(ControlNave nave, naveState state)
    {
        nave.money =            state.money;
        nave.thrustForce =      state.thrustForce;
        nave.torqueForce =      state.torqueForce;
        nave.shipMass =         state.shipMass;
        nave.fuelCapacity =     state.fuelCapacity;
        nave.fuel =             state.fuel;
        nave.electricity =      state.electricity;
        nave.maxElectricity =   state.maxElectricity;
        nave.fuelMass =         state.fuelMass;
        nave.hp =               state.hp;
        nave.maxHp =            state.maxHp;
        nave.minSpeedDmg =      state.minSpeedDmg;
        nave.dmgMultiplier =    state.dmgMultiplier;
    }

    naveState PasarObjetoAEstado(ControlNave nave)
    {
        naveState state = new naveState();
        state.money =            nave.money;
        state.thrustForce =      nave.thrustForce;
        state.torqueForce =      nave.torqueForce;
        state.shipMass =         nave.shipMass;
        state.fuelCapacity =     nave.fuelCapacity;
        state.fuel =             nave.fuel;
        state.electricity =      nave.electricity;
        state.maxElectricity =   nave.maxElectricity;
        state.fuelMass =         nave.fuelMass;
        state.hp =               nave.hp;
        state.maxHp =            nave.maxHp;
        state.minSpeedDmg =      nave.minSpeedDmg;
        state.dmgMultiplier =    nave.dmgMultiplier;
        return state;
    }


    public void saveState()
    {
        Planet[] ps = new Planet[planets.Length];
        for(int i = 0; i < planets.Length; ++i)
        {
            ps[i] = planets[i].GetComponent<Planet>();
        }
        GameState.saveState(ps, Time.time, 
            PasarObjetoAEstado(player.GetComponent<ControlNave>()), 
            player.transform.position);
        GameState.generated = true;
    }
}