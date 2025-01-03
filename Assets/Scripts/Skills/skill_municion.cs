using UnityEngine;
using UnityEngine.UI;

public class SkillMunicion : MonoBehaviour
{
    public float duracionBase = 5f;
    public float cooldownBase = 10f;
    public float multiplicadorVelocidadAtaqueBase = 1.05f;
    public float multiplicadorDañoBase = 1.1f;
    public float multiplicadorVelocidadProyectil = 1.05f;

    public LogicaBlaster blaster;
    public LogicaDisparos disparoPrefab;
    public GameObject botonHabilidad;

    private bool habilidadActiva = false;
    private bool enCooldown = false;

    private GestionSkills gestionSkills;
    private int nivelHabilidad;

    void Start()
    {
        gestionSkills = FindObjectOfType<GestionSkills>();
        if (gestionSkills != null)
        {
            nivelHabilidad = gestionSkills.nivelMunicion;
        }
        else
        {
            Debug.LogError("No se encontró el script GestionSkills en la escena.");
            nivelHabilidad = 1;
        }
    }

    public void ActivarHabilidad()
    {
        if (!habilidadActiva && !enCooldown)
        {
            StartCoroutine(MejorarAtaqueCompleto());
        }
    }

    private System.Collections.IEnumerator MejorarAtaqueCompleto()
    {
        habilidadActiva = true;

        float duracion = duracionBase + (nivelHabilidad - 1) * 0.5f;
        float cooldown = Mathf.Max(cooldownBase - (nivelHabilidad - 1) * 0.5f, 2f);
        float multiplicadorVelocidadAtaque = multiplicadorVelocidadAtaqueBase + (nivelHabilidad* 1.05f) ;
        float multiplicadorDaño = multiplicadorDañoBase + (nivelHabilidad - 1) * 1f;

        float velocidadAtaqueOriginal = blaster.atkSpeed;
        int dañoOriginal = disparoPrefab.daño;
        float velocidadProyectilOriginal = disparoPrefab.GetComponent<Rigidbody2D>().linearVelocity.magnitude;

        blaster.atkSpeed *= multiplicadorVelocidadAtaque;
        disparoPrefab.daño = Mathf.RoundToInt(disparoPrefab.daño * multiplicadorDaño);

        Rigidbody2D rb = disparoPrefab.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity *= multiplicadorVelocidadProyectil;
        }

        if (botonHabilidad != null)
        {
            botonHabilidad.GetComponent<Button>().interactable = false;
            CambiaTransparenciaBoton(botonHabilidad, 0.5f);
        }

        yield return new WaitForSeconds(duracion);

        blaster.atkSpeed = velocidadAtaqueOriginal;
        disparoPrefab.daño = dañoOriginal;

        if (rb != null)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * velocidadProyectilOriginal;
        }

        StartCoroutine(CooldownHabilidad(cooldown));
    }

    private System.Collections.IEnumerator CooldownHabilidad(float cooldown)
    {
        enCooldown = true;

        yield return new WaitForSeconds(cooldown);

        if (botonHabilidad != null)
        {
            botonHabilidad.GetComponent<Button>().interactable = true;
            CambiaTransparenciaBoton(botonHabilidad, 1f);
        }

        enCooldown = false;
        habilidadActiva = false;
    }

    private void CambiaTransparenciaBoton(GameObject boton, float alpha)
    {
        Image botonImagen = boton.GetComponent<Image>();
        if (botonImagen != null)
        {
            Color colorActual = botonImagen.color;
            colorActual.a = alpha;
            botonImagen.color = colorActual;
        }
    }
}
