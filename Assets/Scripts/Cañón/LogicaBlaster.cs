using UnityEngine;

public class LogicaBlaster : MonoBehaviour
{
    public GameObject disparos;
    public Transform cañon;
    public float atkSpeed = 2f;
    public float cooldownDisparos = 0f;
    public float rangoDeteccion = 10f;

    void Update()
    {
        if (Time.time >= cooldownDisparos)
        {
            Transform enemigoMasCercano = ObtenerEnemigoMasCercano();
            if (enemigoMasCercano != null) {

                Shoot(enemigoMasCercano);
                cooldownDisparos = Time.time + 1f / atkSpeed;
                
            }
        }
    }

Transform ObtenerEnemigoMasCercano()
{
    GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
    Transform enemigoMasCercano = null;
    float posicionMasBaja = Mathf.Infinity;

    foreach (GameObject enemigo in enemigos) {

        float posicionY = enemigo.transform.position.y;

        if (posicionY < posicionMasBaja) {

            posicionMasBaja = posicionY;
            enemigoMasCercano = enemigo.transform;

        }

    }

    return enemigoMasCercano;
}

    void Shoot(Transform objetivo)
    {

        if (cañon != null) {

            GameObject proyectil = Instantiate(disparos, cañon.position, Quaternion.identity);
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();

            if (rb != null && objetivo != null) {

                Vector2 direccion = (objetivo.position - cañon.position).normalized;

                rb.linearVelocity = direccion * 10f;

                float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
                proyectil.transform.rotation = Quaternion.Euler(0, 0, angulo);
                
            }
        }
    }
}