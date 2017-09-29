using UnityEngine;
using System.Collections;


public enum Mejora
{
    thrustForce, torqueForce, fuelCapacity, maxElectricity, maxHp, dmgMultiplier
}
public class InfoCompra : MonoBehaviour {
    public float Precio;
    public float cuantoMejora;
    public Mejora queMejora;
    public bool Mejorar()
    {
        if(GameState.player.money >= Precio)
        {
            switch(queMejora)
            {
                case Mejora.thrustForce:
                    GameState.player.thrustForce += cuantoMejora;
                    break;
                case Mejora.torqueForce:
                    GameState.player.torqueForce += cuantoMejora;
                    break;
                case Mejora.fuelCapacity:
                    GameState.player.fuelCapacity += (uint) cuantoMejora;
                    break;
                case Mejora.maxElectricity:
                    GameState.player.maxElectricity += (uint) cuantoMejora;
                    break;
                case Mejora.maxHp:
                    GameState.player.maxHp += cuantoMejora;
                    break;
                case Mejora.dmgMultiplier:
                    GameState.player.dmgMultiplier *= cuantoMejora;
                    break;
            }
            GameState.player.money -= Precio;
            return true;
        }
        return false;
    }
}
