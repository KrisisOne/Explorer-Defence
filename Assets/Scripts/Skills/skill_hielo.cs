using UnityEngine;
using UnityEngine.UI;

public class SkillHielo : MonoBehaviour
{
    public float duracionBase = 5f;
    public float cooldownBase = 10f;
    public float efectoRalentizacionBase = 0.9f;
    public float incrementoPorNivel = 0.25f;

    public GameObject botonHabilidad;

    private bool efectoActivo = false;
    private bool enCooldown = false;

    private GestionSkills gestionSkills;
    private int nivelHabilidad;

    void Start()
    {

        gestionSkills = FindObjectOfType<GestionSkills>();
        if (gestionSkills == null)
        {
            Debug.LogError("No se encontró el script GestionSkills en la escena.");
        }
        else
        {
            nivelHabilidad = gestionSkills.nivelHielo;
        }
    }

    public void ActivarHabilidad()
    {
        if (!efectoActivo && !enCooldown)
        {
            StartCoroutine(RalentizarAliens());
        }
    }

    private System.Collections.IEnumerator RalentizarAliens()
    {
        efectoActivo = true;

        float duracion = duracionBase;
        float cooldown = cooldownBase - (nivelHabilidad - 1) * 0.5f;
        float factorRalentizacion = CalcularFactorRalentizacion(nivelHabilidad);

        GameObject[] aliens = GameObject.FindGameObjectsWithTag("Enemigo");
        foreach (GameObject alien in aliens)
        {
            if (alien != null)
            {
                AplicarEfectoRalentizacion(alien, factorRalentizacion, true);
            }
        }

        if (botonHabilidad != null)
        {
            botonHabilidad.GetComponent<Button>().interactable = false;
            CambiaTransparenciaBoton(botonHabilidad, 0.5f);
        }

        yield return new WaitForSeconds(duracion);

        foreach (GameObject alien in aliens)
        {
            if (alien != null)
            {
                AplicarEfectoRalentizacion(alien, factorRalentizacion, false);
            }
        }

        StartCoroutine(CooldownHabilidad(cooldown));
    }

    private float CalcularFactorRalentizacion(int nivel)
    {
        float maxRalentizacion = 0.4f;
        float minRalentizacion = efectoRalentizacionBase;
        return Mathf.Max(minRalentizacion - (nivel - 1) * incrementoPorNivel, maxRalentizacion);
    }

    private void AplicarEfectoRalentizacion(GameObject alien, float factorRalentizacion, bool aplicar)
    {
        float factor = aplicar ? factorRalentizacion : 1f / factorRalentizacion;

        LogicaAlienTipo1 logica1 = alien.GetComponent<LogicaAlienTipo1>();
        LogicaAlienTipo2 logica2 = alien.GetComponent<LogicaAlienTipo2>();
        LogicaAlienTipo3 logica3 = alien.GetComponent<LogicaAlienTipo3>();
        LogicaAlienTipo4 logica4 = alien.GetComponent<LogicaAlienTipo4>();

        if (logica1 != null) logica1.velocidadDescenso *= factor;
        if (logica2 != null) logica2.velocidadDescenso *= factor;
        if (logica3 != null) logica3.velocidadDescenso *= factor;
        if (logica4 != null) logica4.velocidadDescenso *= factor;
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
        efectoActivo = false;
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
