using UnityEngine;
using System.Collections;
using Assets._Classes;

public enum juego
{
    nada, tiempo
}
public class SSGenerator
{
    private static bool spriteInitialized = false;
    private static Sprite flechaSprite;
    private static void initializeSprites()
    {
        flechaSprite = Resources.Load<Sprite>(@"Imagenes\circulo");
        spriteInitialized = true;
    }
    static private GameObject[] generatePlanets(int count, int additional)
    {
        GameObject[] ret = new GameObject[count + additional]; // porque usan tanto el heap lenguajes como c#? no tienen en cuenta que los syscalls son mas costosos? o hay un sistema de optimizacion que detecta cuando es beneficioso usar el stack? deberia fijarme, suena interesante. talvez deberia escribir un garbage collector, seria un proyecto interesante. ya sabes, lou level, lo hago en c, optimizacion. creo que ya extendi demasiado el MEM. (tomi podes ignorar este MEM)
        for (int i = 0; i < count; ++i)
            ret[i] = PlanetGenerator.GeneratePlanet(); // genera todos los planetas
        return ret;
    }

    static private float findMaxSize(GameObject[] planets)
    {
        float max = 0.0f;
        for (int i = 0; i < planets.Length - 1; ++i)
        {
            if (planets[i].transform.localScale.x > max) max = planets[i].transform.localScale.x;
        }
        return max;
    }

    static public GameObject[] generateSolarSystem(Vector2 centrePoint, float minSize, float maxSize, float time, ref juegoData jData)
    {
        //system generation
        float G = 6.674f * Mathf.Pow(10.0f, -11.0f);
        float planetCount = Random.Range(Mathf.Ceil(minSize), Mathf.Ceil(maxSize));
        GameObject[] planets = generatePlanets(Mathf.CeilToInt(planetCount), 2);
        //tienda generation
        GameObject tienda = PlanetGenerator.tienda();
        planets[planets.Length - 2] = tienda;
        //centre generation
        GameObject centre = PlanetGenerator.GenerateCentre(findMaxSize(planets));
        planets[planets.Length - 1] = centre;
        centre.GetComponent<Planet>().amplitudX = 0.0f;
        centre.GetComponent<Planet>().amplitudY = 0.0f;
        centre.transform.position = centrePoint;
        //system organization
        GameObject prevObj = centre;
        Planet prevPlanet = centre.GetComponent<Planet>();
        Planet currentPlanet;
        float r;
        foreach (GameObject o in planets)
        {
            if (o.name != "centre")
            {
                currentPlanet = o.GetComponent<Planet>();
                r = prevObj.transform.localScale.x * 75.0f;
                currentPlanet.amplitudX = prevPlanet.amplitudX + r;
                currentPlanet.amplitudY = prevPlanet.amplitudY + r;
                currentPlanet.speed = currentPlanet.amplitudX / Random.Range(1.0f, 3.0f);
                currentPlanet.offset = Random.Range(-currentPlanet.amplitudX * 100.0f, currentPlanet.amplitudX * 100.0f);
                currentPlanet.centre = centrePoint;
                currentPlanet.btime = time;
                prevPlanet = currentPlanet;
                prevObj = o;
            }
        }
        switch(jData.tipo)
        {
            case juego.tiempo:
                int recibe = 0;// Random.Range(0, planets.Length - 1);
                int da = 1;// Random.Range(0, planets.Length - 1);
                while(recibe == da)
                {
                    recibe = Random.Range(0, planets.Length - 1);
                }
                Planet pRecibe = planets[recibe].GetComponent<Planet>(); Planet pDa = planets[da].GetComponent<Planet>();
                planets[recibe].tag = "Recibe"; planets[da].tag = "Da";
                CircleCollider2D collider = planets[recibe].AddComponent<CircleCollider2D>();
                collider.radius = 15.0f;
                collider.isTrigger = true;
                collider = planets[da].AddComponent<CircleCollider2D>();
                collider.radius = 15.0f;
                collider.isTrigger = true;
                pRecibe.rol = rolDeMision.recibir;
                pDa.rol = rolDeMision.dar;
                float amplitudDiff = Mathf.Abs(pRecibe.amplitudX - pDa.amplitudX);
                float raceTime = amplitudDiff * 0.5f;
                jData.da = planets[da];
                jData.recibe = planets[recibe];
                jData.hayLimiteDeTiempo = true;
                jData.tipo = juego.tiempo;
                jData.pDaTransform = planets[da].transform;
                jData.pRecibeTransform = planets[recibe].transform;
                SpriteRenderer srenderer;

                GameObject flecha = new GameObject();
                jData.flechaRecibe = flecha;
                srenderer = flecha.AddComponent<SpriteRenderer>();
                if (!spriteInitialized) initializeSprites();
                srenderer.sprite = flechaSprite;
                srenderer.color = Color.magenta;
                flecha.transform.parent = planets[recibe].transform;
                flecha.layer = 8;
                float flechaScale = 10.0f;
                flecha.transform.localScale = new Vector3(flechaScale, flechaScale, 1.0f);
                flecha.SetActive(false);

                flecha = new GameObject();
                jData.flechaDa = flecha;
                srenderer = flecha.AddComponent<SpriteRenderer>();
                if (!spriteInitialized) initializeSprites();
                srenderer.sprite = flechaSprite;
                srenderer.color = Color.magenta;
                flecha.transform.parent = planets[da].transform;
                flecha.layer = 8;
                flecha.transform.localScale = new Vector3(flechaScale, flechaScale, 1.0f);
                break;
        }
        return planets;
    }
    static public GameObject[] reGenerateSolarSystem(Planet[] planets)
    {
        GameObject[] objs = new GameObject[planets.Length];
        for(int i = 0; i < planets.Length; ++i)
        {
            objs[i] = PlanetGenerator.reGeneratePlanet(planets[i]);
        }
        return objs;
    }
}
