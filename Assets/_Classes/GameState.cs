using UnityEngine;
using System.Collections;

public class GameState
{
    public static bool generated = false;
    public static Planet[] planets;
    public static float time;
    public static naveState player;
    public static void saveState(Planet[] p, float t, naveState pstate, Vector2 position) { planets = p; time = t; player = pstate; player.position = position; }
}