using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Classes
{
    struct PlanetData // Estructura que almacena datos de planeta
    {
        public float size;
        public float mass;
    }
    class PlanetGenerator
    {
        private float tiendaBound = 7.43801652893f;
        private static Sprite[] sprites;
        private static Sprite tiendaSprite;
        private static Sprite flechaSprite;
        public static void initializeSpriteArray()
        {
            sprites = Resources.LoadAll<Sprite>(@"Imagenes\Planetas");
            tiendaSprite = Resources.Load<Sprite>(@"Imagenes\asteroideTienda");
            flechaSprite = Resources.Load<Sprite>(@"Imagenes\circulo");
        }
        public static PlanetData GenerateData(float sizeLowerBound, float sizeUpperBound)  // Genera aleatoriamente los datos
        {
            PlanetData data;

            float x = UnityEngine.Random.Range(sizeLowerBound, sizeUpperBound); // aca estoy intentando hacer que los planetas muy chicos sean poco probables y los grandes tambien
            data.size = 1.1f * Mathf.Sqrt(x); // Igual esta en progreso
            data.mass = Mathf.Pow(10, data.size + 21.5f);
            return data;
        }
        public static GameObject GeneratePlanet(PlanetData data, bool sprite) // Genera un planeta y devuelve un rigidbody
        {
            GameObject obj = new GameObject();
            obj.transform.localScale = new Vector3(data.size, data.size, 1.0f);
            Rigidbody2D rbody = obj.AddComponent<Rigidbody2D>();
            rbody.isKinematic = true;
            rbody.gravityScale = 0.0f;
            int spriteIndex = 0;
            if (sprite)
            {
                SpriteRenderer srenderer = obj.AddComponent<SpriteRenderer>();
                if (sprites == null) initializeSpriteArray(); // si no estan cargados los sprites
                spriteIndex = UnityEngine.Random.Range(0, sprites.Length);
                srenderer.sprite = sprites[spriteIndex];
            }
            CircleCollider2D collider = obj.AddComponent<CircleCollider2D>();
            collider.radius = 10.0f;
            Planet script = obj.AddComponent<Planet>(); // Le agrega el script al objeto
            script.size = data.size;
            script.spriteIndex = spriteIndex;
            if (float.IsInfinity(data.mass)) script.Mass = float.MaxValue;
            else script.Mass = data.mass;
            obj.name = "" + script.Mass;
            return obj;
        }
        public static GameObject reGeneratePlanet(Planet planet)
        {
            GameObject obj = new GameObject();
            obj.transform.localScale = new Vector3(planet.size, planet.size, 1.0f);
            Rigidbody2D rbody = obj.AddComponent<Rigidbody2D>();
            rbody.isKinematic = true;
            rbody.gravityScale = 0.0f;
            CircleCollider2D collider = obj.AddComponent<CircleCollider2D>();
            SpriteRenderer srenderer = obj.AddComponent<SpriteRenderer>();
            if (sprites == null) initializeSpriteArray(); // si no estan cargados los sprites
            if (planet.isTienda)
            {
                srenderer.sprite = tiendaSprite;
                collider.radius = 5.0f;
                collider = obj.AddComponent<CircleCollider2D>();
                collider.radius = 6.0f;
                collider.isTrigger = true;
                Tienda script = obj.AddComponent<Tienda>();
                script.amplitudX = planet.amplitudX;
                script.amplitudY = planet.amplitudY;
                script.offset = planet.offset;
                script.speed = planet.speed;
                script.size = planet.size;
                script.spriteIndex = planet.spriteIndex;
                script.isTienda = planet.isTienda;
                script.Mass = planet.Mass;
                script.btime = planet.btime;
                script.position = planet.position;
                obj.tag = "Tienda";
                obj.name = "" + script.Mass;
            }
            else
            {
                srenderer.sprite = sprites[planet.spriteIndex];
                collider.radius = 10.0f;
                Planet script = obj.AddComponent<Planet>();
                script.amplitudX = planet.amplitudX;
                script.amplitudY = planet.amplitudY;
                script.offset = planet.offset;
                script.speed = planet.speed;
                script.size = planet.size;
                script.spriteIndex = planet.spriteIndex;
                script.isTienda = planet.isTienda;
                script.Mass = planet.Mass;
                script.btime = planet.btime;
                script.position = planet.position;
                GameObject flecha = new GameObject();
                srenderer = flecha.AddComponent<SpriteRenderer>();
                srenderer.sprite = flechaSprite;
                srenderer.color = Color.red;
                flecha.transform.parent = obj.transform;
                flecha.layer = 8;
                float flechaScale = 3.0f;
                flecha.transform.localScale = new Vector3(flechaScale, flechaScale, 1.0f);
                obj.name = "" + script.Mass;

            }
            return obj;
        }
        public static GameObject GeneratePlanet() { return GeneratePlanet(GenerateData(0.1f, 4.0f), true); } // Overload

        public static GameObject GenerateCentre(float otherMax, float upperBound, bool sprite) // genera un cuerpo bien pija bien poderoso
        {   // otherMax es la masa del cuerpo mas masivo del sistema solar
            GameObject p = GeneratePlanet(GenerateData(otherMax + 0.5f, upperBound), sprite);
            p.name = "centre";
            Planet plt = p.GetComponent<Planet>();
            plt.amplitudX = 0.0f;
            plt.amplitudY = 0.0f;
            return p;
        }
        public static GameObject GenerateCentre(float otherMax) { return GenerateCentre(otherMax, 0.1f + otherMax, true); }
        public static GameObject tienda()
        {
            PlanetData tiendadata;
            tiendadata.size = 3f;
            tiendadata.mass = Mathf.Pow(10, tiendadata.size + 20.5f);
            GameObject obj = new GameObject();
            obj.transform.localScale = new Vector3(tiendadata.size, tiendadata.size, 1.0f);
            Rigidbody2D rbody = obj.AddComponent<Rigidbody2D>();
            rbody.isKinematic = true;
            rbody.gravityScale = 0.0f;
            SpriteRenderer srenderer = obj.AddComponent<SpriteRenderer>();
            if (sprites == null) initializeSpriteArray(); // si no estan cargados los sprites
            srenderer.sprite = tiendaSprite;
            CircleCollider2D collider = obj.AddComponent<CircleCollider2D>();
            collider.radius = 5.0f;
            collider = obj.AddComponent<CircleCollider2D>();
            collider.radius = 6.0f;
            collider.isTrigger = true;
            Tienda script = obj.AddComponent<Tienda>(); // Le agrega el script al objeto
            script.size = tiendadata.size;
            if (float.IsInfinity(tiendadata.mass)) script.Mass = float.MaxValue;
            else script.Mass = tiendadata.mass;
            obj.name = "Tienda " + script.Mass;
            //flecha
            GameObject flecha = new GameObject();
            srenderer = flecha.AddComponent<SpriteRenderer>();
            srenderer.sprite = flechaSprite;
            srenderer.color = Color.red;
            flecha.transform.parent = obj.transform;
            flecha.layer = 8;
            float flechaScale = 3.0f;
            flecha.transform.localScale = new Vector3(flechaScale, flechaScale, 1.0f);
            obj.tag = "Tienda";
            return obj;
        }
    }
}